using EFModels.Models;
using FlexCoreService.ProductCtrl.Exts;
using FlexCoreService.ProductCtrl.Interface;
using FlexCoreService.ProductCtrl.Models.VM;
using FlexCoreService.ProductCtrl.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _repo;
        private AppDbContext _db;
        public CategoryController(AppDbContext db, ICategoryRepository repo)
        {
            _db = db;
            _repo = repo;
        }

        //Post: api/Category/Men
        [HttpPost("men")]
        public async Task<ActionResult<CategoryVM>> GetCategoryMen()
        {
            int categoryId = 1;
            var server = new CategoryService(_repo);
            var category = server.SearchCategory(categoryId).ToCategoryVM();
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        //Post: api/Category/Women
        [HttpPost("women")]
        public async Task<ActionResult<CategoryVM>> GetCategoryWomen()
        {
            int categoryId = 2;
            var server = new CategoryService(_repo);
            var category = server.SearchCategory(categoryId).ToCategoryVM();
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        //Post: api/Category/Kid
        [HttpPost("kid")]
        public async Task<ActionResult<CategoryVM>> GetCategoryKid()
        {
            int categoryId = 3;
            var server = new CategoryService(_repo);
            var category = server.SearchCategory(categoryId).ToCategoryVM();
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }
    }
}
