using Dapper;
using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FlexCoreService.CustomeShoes.Infra.DPRepository
{
    public class ShoesChooseDPRepository : ICustomerChooseRepository
    {
        private string _connStr;
        private IConfiguration _configuration;
        public ShoesChooseDPRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("AppDbContext");
        }
        public IEnumerable<ShoesColorCategoriesDto> GetColor()
        {
            string sql = @"select scc.ShoesColorId, scc.ColorName, scc.ColorCode from ShoesColorCategories as scc
";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ShoesColorCategoriesDto>(sql);
            return result;
        }

        public IEnumerable<CustomizedMaterialsDto> GetMaterial()
        {
            string sql = @"select cm.Shoesmaterial_Id, cm.material_Name from Customized_materials as cm
";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<CustomizedMaterialsDto>(sql);
            return result;
        }

        public IEnumerable<ShoesOptionsDto> GetOptions()
        {
            string sql = @"select scs.OptionId, scs.OptinName from ShoesChooses as scs
";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ShoesOptionsDto>(sql);
            return result;
        }

        public IEnumerable<ShoesSizeDto> GetSize()
        {
            string sql = @"select ss.SizeId, ss.SizeName from ShoesSizes as ss
";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ShoesSizeDto>(sql);
            return result;
        }
    }
}
