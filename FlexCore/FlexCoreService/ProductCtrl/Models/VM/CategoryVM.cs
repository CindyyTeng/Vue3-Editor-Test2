namespace FlexCoreService.ProductCtrl.Models.VM
{
    public class CategoryVM
    {
        public Dictionary<string, List<string>> Categories { get; set; }
        public CategoryVM()
        {
            Categories = new Dictionary<string, List<string>>();
        }
    }
}
