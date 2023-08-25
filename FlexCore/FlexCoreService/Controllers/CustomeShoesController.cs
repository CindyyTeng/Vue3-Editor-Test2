using EFModels.Models;
using FlexCoreService.CustomeShoes.Exts;
using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.Dtos;
using FlexCoreService.CustomeShoes.Models.VMs;
using FlexCoreService.CustomeShoes.Service;
using FlexCoreService.ProductCtrl.Models.VM;
using FlexCoreService.ProductCtrl.Service;
using FlexCoreService.UserCtrl.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexCoreService.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
	[ApiController]
	public class CustomeShoesController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ICustomeShoesRepository _repo;
        private ICustomerChooseRepository _chooserepo;
        public CustomeShoesController(AppDbContext context, ICustomeShoesRepository repo, ICustomerChooseRepository chooserepo)
		{
			_db = context;
			_repo = repo;
            _chooserepo = chooserepo;
        }

		// POST: api/CustomizedShoesPo/
		[HttpGet("GetAll")]
		public async Task<ActionResult<IEnumerable<CustomeShoesVM>>> GetAllCustomeShoes()
		{
			var server = new CustomeShoesService(_repo);
			var shoes = server.SearchCustomeShoes().Select(p => p.ToCardVM()).ToList();
			if (shoes.Count == 0)
			{
				return NotFound();
			}
			return shoes;
		}

		// GET: api/CustomizedShoesPo
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomizedShoesPo>>> GetShoes()
		{
			if (_db.CustomizedShoesPos == null)
			{
				return NotFound();
			}
			return await _db.CustomizedShoesPos.ToListAsync();
		}

        // GET: api/CustomizedShoesPo/5
        [HttpGet("shoes/Customization/{shoesProductId}")]
        public async Task<ActionResult<CustomeShoesVM>> GetShoes(int shoesProductId)
        {
            var server = new CustomeShoesService(_repo);

            var shoes = server.SearchOneCustomeShoes(shoesProductId);

            var chooseserver = new ShoesChoosesService(_chooserepo);

            var chooses = chooseserver.GetOptions().ToList();
            var chooses2 = chooseserver.GetColor().ToList();
            var chooses3 = chooseserver.GetMaterial().ToList();

            var vm = shoes.ToAllVM(chooses, chooses2, chooses3);

            if (vm == null)
            {
                return NotFound();
            }

            return vm;
        }

        [HttpGet("shoes/detail/{shoesProductId}")]
        public async Task<ActionResult<ShoesDetailVM>> GetShoesDetail(int shoesProductId)
        {
            var server = new CustomeShoesService(_repo);

            var shoes = server.GetShoesDetail(shoesProductId);

            var chooseserver = new ShoesChoosesService(_chooserepo);

            var chooses = chooseserver.GetSizes().ToList();
            
            var vm = shoes.ToDetailVM(chooses);

            if (vm == null)
            {
                return NotFound();
            }

            return vm;
        }

        //GET: api/Products/Detial/productId
        [HttpGet("Imgs/{shoesProductId}")]
        public async Task<ActionResult<IEnumerable<ShoesImgsVM>>> GetShoesImgs(int shoesProductId)
        {
            var service = new CustomeShoesService(_repo);

            var shoes = service.GetShoesImgs(shoesProductId).Select(i => i.ToImgVM());

            if (shoes == null)
            {
                return BadRequest();
            }

            return Ok(shoes);
        }

		[HttpPost("EnterCustomerChoose")]
		public async Task<ShoesOrderDto> Register([FromBody] ShoesOrderDto dto)
		{
			ShoesOrder order = new ShoesOrder
			{
				ShoesOrderId = dto.ShoesOrderId,
				Qty = dto.Qty,
				fk_ShoesSizeId = dto.fk_ShoesSizeId,
				Remark = dto.Remark,
			};

			_db.ShoesOrders.Add(order);
			await _db.SaveChangesAsync();
			return dto;
		}

		//[HttpGet("Detail/{ShoesProductId}")]
		//public async Task<ActionResult<CustomizedShoesPo>> GetShoesDetail(int shoesId)
		//{
		//    if (_db.CustomizedShoesPos == null)
		//    {
		//        return NotFound();
		//    }
		//    var shoes = await _db.CustomizedShoesPos.FirstOrDefaultAsync(p => p.ShoesProductId == shoesId);

		//    if (shoes == null)
		//    {
		//        return NotFound();
		//    }

		//    return shoes;
		//}

		//// PUT: api/CustomizedShoesPo/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutShoes(int id, CustomizedShoesPo shoes)
		//{
		//	if (id != shoes.ShoesProductId)
		//	{
		//		return BadRequest();
		//	}

		//	_db.Entry(shoes).State = EntityState.Modified;

		//	try
		//	{
		//		await _db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!ShoesExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		//// POST: api/CustomizedShoesPo
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPost]
		//public async Task<ActionResult<CustomizedShoesPo>> PostShoes(CustomizedShoesPo shoes)
		//{
		//	if (_db.CustomizedShoesPos == null)
		//	{
		//		return Problem("Entity set 'AppDbContext.CustomizedShoesPo'  is null.");
		//	}
		//	_db.CustomizedShoesPos.Add(shoes);
		//	await _db.SaveChangesAsync();

		//	return CreatedAtAction("GetShoes", new { id = shoes.ShoesProductId }, shoes);
		//}

		// DELETE: api/CustomizedShoesPo/5
		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteShoes(int id)
		//{
		//	if (_db.CustomizedShoesPos == null)
		//	{
		//		return NotFound();
		//	}
		//	var shoes = await _db.CustomizedShoesPos.FindAsync(id);
		//	if (shoes == null)
		//	{
		//		return NotFound();
		//	}

		//	_db.CustomizedShoesPos.Remove(shoes);
		//	await _db.SaveChangesAsync();

		//	return NoContent();
		//}

		private bool ShoesExists(int id)
		{
			return (_db.CustomizedShoesPos?.Any(e => e.ShoesProductId == id)).GetValueOrDefault();
		}
	}
}
