using EFModels.Models;
using FlexCoreService.CustomeShoes.Exts;
using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.VMs;
using FlexCoreService.CustomeShoes.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesChoosesController : ControllerBase
    {
        private ICustomerChooseRepository _repo;
        private AppDbContext _db;
        public ShoesChoosesController(AppDbContext db, ICustomerChooseRepository repo)
        {
            _db = db;
            _repo = repo;
        }


        [HttpGet("Color/{id}")]
        public async Task<ActionResult<ShoesColorCategory>> GetOneColor(int id)
        {
            if (_db.ShoesColorCategories == null)
            {
                return NotFound();
            }
            var shoesColor = await _db.ShoesColorCategories.FindAsync(id);

            if (shoesColor == null)
            {
                return NotFound();
            }

            return shoesColor;
        }

        [HttpGet("Option/{id}")]
        public async Task<ActionResult<ShoesChoose>> GetOneOptions(int id)
        {
            if (_db.ShoesChooses == null)
            {
                return NotFound();
            }
            var shoesOption = await _db.ShoesChooses.FindAsync(id);

            if (shoesOption == null)
            {
                return NotFound();
            }

            return shoesOption;
        }


        [HttpGet("Material/{id}")]
        public async Task<ActionResult<Customized_material>> GetOneMaterial(int id)
        {
            if (_db.Customized_materials == null)
            {
                return NotFound();
            }
            var shoesMaterial = await _db.Customized_materials.FindAsync(id);

            if (shoesMaterial == null)
            {
                return NotFound();
            }

            return shoesMaterial;
        }

        [HttpGet("Size/{id}")]
        public async Task<ActionResult<ShoesSize>> GetOneSize(int id)
        {
            if (_db.ShoesSizes == null)
            {
                return NotFound();
            }
            var shoesSize = await _db.ShoesSizes.FindAsync(id);

            if (shoesSize == null)
            {
                return NotFound();
            }

            return shoesSize;
        }
    }
}
