namespace FlexCoreService.ProductCtrl.Models.Dtos
{
    public class ProductCommentDto
    {
        public int CommentId { get; set; }
        public string MemberName { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public decimal AverageScore { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
