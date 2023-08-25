using EFModels.Models;
using FlexCoreService.UserCtrl.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Principal;
using Newtonsoft.Json.Linq;

namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 取得會員信箱
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("account/{account}")]
        public async Task<ActionResult<string>> GetUserEmail(string account)
        {
            //檢查帳號是否存在
            Member member = await _db.Members.FirstOrDefaultAsync(x => x.Account == account);

            if (_db.Members == null)
            {
                return NotFound();
            }

            string userEmail = member.Email;
            return Ok(userEmail);
        }

        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet("{memberId}")]
        //[Authorize]
        public async Task<ProfileDto> GetUserProfil(int memberId)
        {
            ClaimsPrincipal user = HttpContext.User;

            if (_db.Members == null)
            {
                return null;
            }
            ProfileDto proDto = _db.Members.Include(m => m.fk_Level).Where(m => m.MemberId == memberId).Select(m => new ProfileDto
            {
                MemberId = m.MemberId,
                fk_Level = m.fk_LevelId,
                LevelName = m.fk_Level.LevelName,
                Name = m.Name,
                Email = m.Email,
                Mobile = m.Mobile,
                Gender = m.Gender,
                Birthday = m.Birthday,
                CommonAddress = m.CommonAddress,
                AlternateAddress1 = m.AlternateAddress.AlternateAddress1,
                AlternateAddress2 = m.AlternateAddress.AlternateAddress2,
                IsSubscribeNews = m.IsSubscribeNews
            }).First();


            return proDto;
        }

        /// <summary>
        /// 登入中
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto value)
        {
            var userData = (from m in _db.Members
                            where m.Account == value.Account
                            select m).SingleOrDefault();

            var userPassword = string.Empty;


            if (userData == null)
            {
                //欄位或帳號驗證失敗
                return Ok(null);
            }
            else
            {
                //驗證密碼
                userPassword = userData.EncryptedPassword;

                if (userPassword == value.EncryptedPassword)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, userData.Account),
                    new Claim("UserPassword", userData.EncryptedPassword),
                    new Claim("FullName", userData.Name),
                    new Claim("MemberId",userData.MemberId.ToString())//MemberId為自訂宣告的Type名稱
                   // new Claim(ClaimTypes.Role, "Administrator")//管理角色
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7),
                    };

                    //控制登入狀態
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return Ok(JsonConvert.SerializeObject(claims));
                }
                return Ok(userData.Account);
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        [HttpDelete("Logout")]
        public string Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "ok";
        }

        /// <summary>
        /// 測試登入狀態
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool IsLogin()
        {
            return User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 未登入
        /// </summary>
        /// <returns></returns>
        [HttpGet("NoLogin")]
        public string NoLogin()
        {
            return "未登入";
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="regdto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<RegisterDto> Register([FromBody] RegisterDto regdto)
        {
            Member member = new Member
            {
                Account = regdto.Account,
                EncryptedPassword = regdto.EncryptedPassword,
                Name = regdto.Name,
                Email = regdto.Email,
                Birthday = regdto.Birthday,
                Mobile = regdto.Mobile,
                CommonAddress = regdto.CommonAddress,
                fk_LevelId = 1//一般會員
            };



            //todo發送驗證信

            _db.Members.Add(member);
            await _db.SaveChangesAsync();
            return regdto;
        }

        /// <summary>
        /// 編輯會員資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut("Id")]
        public async Task<ActionResult<string>> EditUserProfile(int id, ProfileDto prodto)
        {

            //檢查id是否存在
            Member member = await _db.Members.FindAsync(id); //FindAsync 根據主键查找對應的紀錄

            if (member == null)
            {
                return NotFound("找不到對應的會員資料");
            }

            member.Email = prodto.Email;
            member.Mobile = prodto.Mobile;
            member.Gender = prodto.Gender;
            member.Birthday = prodto.Birthday;
            member.CommonAddress = prodto.CommonAddress;
            member.IsSubscribeNews = prodto.IsSubscribeNews;

            //AlternateAddress 
            if (member.AlternateAddress == null)
            {
                member.AlternateAddress = new AlternateAddress(); // 建立新的 AlternateAddress 物件
            }


            AlternateAddress address = await _db.AlternateAddresses.FirstOrDefaultAsync(x => x.fk_MemberId == id);

            //沒有新增備用地址
            if (address == null)
            {

            }
            else
            {
                //新增備用地址
                address.AlternateAddress1 = prodto.AlternateAddress1;
                address.AlternateAddress2 = prodto.AlternateAddress2;
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return "編輯會員資料失敗!";
                }
                else
                {
                    throw;
                }
            }
            //檢查資料都填寫

            //跳更新成功回到本頁
            return Ok("編輯會員資料成功");
        }

        /// <summary>
        /// 重新設定密碼
        /// </summary>
        /// <param name="logindto"></param>
        /// <returns></returns>
        [HttpPut("ResetPwd")]
        public async Task<ActionResult<string>> ResetPwd(LoginDto logindto)
        {
            var member = (from m in _db.Members
                          where m.Account == logindto.Account
                          select m).SingleOrDefault();


            if (member == null)
            {
                return NotFound("找不到對應的會員資料");
            }

            member.EncryptedPassword = logindto.EncryptedPassword;

            try
            {
                //雜湊密碼
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.Account))
                {
                    return "重設密碼失敗!";
                }
                else
                {
                    throw;
                }
            }
            return Ok("重設密碼成功");
        }

        /// <summary>
        /// 驗證會員身分
        /// </summary>
        /// <param name="logindto"></param>
        /// <returns></returns>
        [HttpPost("Verify")]
        public async Task<ActionResult<string>> VerifyUser(LoginDto logindto)
        {
            Member member = await _db.Members.FirstOrDefaultAsync(x => x.Account == logindto.Account);
            if (member == null)
            {
                return NotFound("找不到對應的會員資料");
            }
            else if (member.EncryptedPassword == logindto.EncryptedPassword)
            {
                return Ok("驗證通過");
            }
            else
            {
                return BadRequest("密碼不正確");
            }

        }

        /// <summary>
        /// 註冊驗證信(改成忘記密碼驗證信?)
        /// </summary>
        /// <param name="email"></param>
        //[HttpGet]
        //public void SendEmail(string email)
        //{
        //    var senderEmail = "";
        //    var password = "";
        //    //var senderEmail = _congig["Gmail:fuen28flex@gmail.com"];
        //    //var password = _congig["Gmail:flexfuen28"];

        //    MailMessage mms = new MailMessage();
        //    mms.From = new MailAddress(senderEmail);
        //    mms.To.Add(email);
        //    mms.Subject = "Flex 註冊驗證信";
        //    mms.Body = "感謝您註冊成為 Flex 的會員!請點擊連結...來啟用您的帳戶";

        //    //設定郵件主機
        //    SmtpClient client = new SmtpClient("flex.gmail.com");
        //    client.Port = 587;
        //    client.Credentials = new NetworkCredential(senderEmail, password);
        //    client.EnableSsl = true;

        //    //寄出郵件
        //    try
        //    {
        //        client.Send(mms);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }

        //}

        private bool MemberExists(int id)
        {
            return (_db.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
        private bool MemberExists(string account)
        {
            return (_db.Members?.Any(e => e.Account == account)).GetValueOrDefault();
        }

    }
}
