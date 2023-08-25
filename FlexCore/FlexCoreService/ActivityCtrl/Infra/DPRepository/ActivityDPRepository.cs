using Dapper;
using FlexCoreService.ActivityCtrl.Interface;
using FlexCoreService.ActivityCtrl.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.VM;
using FlexWebAPI.Models.DTO;
using Microsoft.Data.SqlClient;


namespace FlexCoreService.ActivityCtrl.Infra.DPRepository
{
    public class ActivityDPRepository : IActivityDPRepository
    {
        private string _connStr;
        private IConfiguration _configuration;

        public ActivityDPRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("AppDbContext");
        }
        public ActivityInfoDto GetActivityInfo(int id)
        {
            string sql = @"select TOP(1)  ActivityName, ActivityDate, ActivityCategoryName, ActivityAge, ActivitySalePrice, ActivityOriginalPrice, ActivityPlace, ActivityDescription, SpeakerName, ImgPath
FROM
    Activities
JOIN
    ActivityCategories ON Activities.fk_ActivityCategoryId = ActivityCategories.ActivityCategoryId
JOIN
    Speakers ON Activities.fk_SpeakerId = Speakers.SpeakerId
Join ActivityImg ON ActivityImg.fk_ActivityId = Activities.ActivityId
WHERE ActivityId = @id
";

            using(var conn = new SqlConnection(_connStr))
            {
                return conn.QueryFirstOrDefault<ActivityInfoDto>(sql, new {id});
            }
        }

        public async Task<IEnumerable<ActivityIndexDTO>> GetAllAsync()
        {
            string sql = @"SELECT
    Activities.ActivityId,
    Activities.ActivityName,
    ActivityCategoryName,
    Activities.ActivityDate,
    ActivityPlace,
    MIN(ActivityImg.ImgPath) AS ImgPath
FROM
    Activities
JOIN
    ActivityImg ON ActivityImg.fk_ActivityId = Activities.ActivityId
JOIN
     ActivityCategories ON ActivityCategories.ActivityCategoryId = Activities.fk_ActivityCategoryId
GROUP BY
    Activities.ActivityId, Activities.ActivityName, ActivityCategories.ActivityCategoryName, ActivityPlace, Activities.ActivityDate";


            using(var conn =new SqlConnection(_connStr))
            {
                return await conn.QueryAsync<ActivityIndexDTO>(sql);
            }
        }

        public async Task<MemberDTO> GetMembreInfoAsync(int id)
        {
            string sql = @" select Name , Gender, CommonAddress, Mobile,  Email,  Birthday
 From Members
 Where MemberId =@id";


            using (var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryFirstOrDefaultAsync<MemberDTO>(sql, new {id});
            }
        }

        public async Task<ActivityBookingTimeDTO> GetActivityBookingTimeAsync(int id)
        {
            string sql = @"
select ActivityBookStartTime, ActivityBookEndTime
from 
Activities
where ActivityId =@id
";

            using(var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryFirstOrDefaultAsync<ActivityBookingTimeDTO>(sql, new { id});
            }
        }


        //public async Task<IEnumerable<ActivityIndexDTO>> SearchByPlaceAndCategoryAsync(ActivitySearchDTO dto)
        //{
        //    var option1 = dto.Option1;
        //    var option2 = dto.Option2;
        //    var place = dto.SearchArea;
        //    string sql = @"
        //SELECT
        //    Activities.ActivityId,
        //    Activities.ActivityName,
        //    Activities.ActivityDate,
        //    MIN(ActivityImg.ImgPath) AS ImgPath
        //FROM
        //    Activities
        //JOIN
        //    ActivityImg ON ActivityImg.fk_ActivityId = Activities.ActivityId
        //JOIN 
        //    ActivityCategories ON ActivityCategories.ActivityCategoryId = Activities.fk_ActivityCategoryId
        //WHERE 
        //    (ActivityCategoryName IN ( '"+@option1 + "','"+ @option2 + " ) OR " + @option1 + " IS NULL OR "+ @option2 + " IS NULL) " +
        //"AND (ActivityPlace LIKE "+ @place + " OR "+@place+" IS NULL)" +
        //" GROUP BY Activities.ActivityId, Activities.ActivityName, Activities.ActivityDate"+
        //" ORDER BY ActivityDate";

        //    using (var conn = new SqlConnection(_connStr))
        //    {
        //        var result = await conn.QueryAsync<ActivityIndexDTO>(sql, new
        //        {
        //            option1,
        //            option2,
        //            place
        //        });

        //        return result;
        //    }
        //}


    }
}

