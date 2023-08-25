namespace FlexCoreService.ProductCtrl.Models.VM
{
    public class ProductCommentVM
    {
        public int CommentId { get; set; }
        public string MemberName { get; set; }
        public string? MemberNameText 
        {
            get
            {
                if (!string.IsNullOrEmpty(MemberName))
                {
                    var firstword = MemberName[0];
                    var lastword= MemberName[MemberName.Length-1];
                    return $"{firstword}*{lastword}";
                }
                return string.Empty;
            }
        }
        public string Description { get; set; }
        public int Score { get; set; }
        public decimal AverageScore { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateTimeText
        {
            get
            {
                return CreateTime.ToString("d");
            }
        }
        public int? totalPage { get; set; }
    }
}
