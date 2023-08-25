using EFModels.Models;
using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.CustomeShoes.Service
{
	public class CustomeShoesService
	{
		private ICustomeShoesRepository _repo;
		private AppDbContext _db;
		public CustomeShoesService(ICustomeShoesRepository repo)
		{
			_repo = repo;
			_db = new AppDbContext();
		}

		public IEnumerable<CustomeShoesDto> SearchCustomeShoes()
		{
			var customeshoes = _repo.SearchCustomeShoes();
			return customeshoes;
		}
        public CustomeShoesDto SearchOneCustomeShoes(int Id)
        {
            var onecustomeshoes = _repo.SearchOneCustomeShoes(Id);
            return onecustomeshoes;
        }

        public ShoesDetailDto GetShoesDetail(int shoesproductId)
        {
            var shoesDetails = _repo.GetShoesDetail(shoesproductId);
            return shoesDetails;
        }

        public IEnumerable<ShoesImgsDto> GetShoesImgs(int shoesproductId)
        {
            var imgs = _repo.GetShoesImgs(shoesproductId);
            return imgs;
        }
    }
}
