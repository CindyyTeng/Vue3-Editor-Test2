using EFModels.Models;
using FlexCoreService.ActivityCtrl.Models.Dtos;
using FlexCoreService.CustomeShoes.Exts;
using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.VMs;
using FlexCoreService.CustomeShoes.Service;
using FlexCoreService.ProductCtrl.Interface;
using FlexCoreService.ProductCtrl.Models.VM;
using FlexCoreService.ProductCtrl.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoreService.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
	[ApiController]
	public class ShoesCategoryController : ControllerBase
    {
		private IShoesCategoryRepository _repo;
		private AppDbContext _db;
		public ShoesCategoryController(AppDbContext db, IShoesCategoryRepository repo)
		{
			_db = db;
			_repo = repo;
		}
		// Get: api/ShoesCategory/
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ShoesCategoryVM>>> GetAllCategory()
		{
			var server = new ShoesCategoryService(_repo);
			var category = server.GetAllCategory().Select(p => p.ToVM()).ToList();
			if (category.Count == 0)
			{
				return NotFound();
			}
			return category;
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoesCategory>> GetCategory(int id)
        {
            if (_db.ShoesCategories == null)
            {
                return NotFound();
            }
            var shoesCategory = await _db.ShoesCategories.FindAsync(id);

            if (shoesCategory == null)
            {
                return NotFound();
            }

            return shoesCategory;
        }


    }
}
