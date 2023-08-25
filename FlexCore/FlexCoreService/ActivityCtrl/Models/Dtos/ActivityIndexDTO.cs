namespace FlexWebAPI.Models.DTO
{
    public class ActivityIndexDTO
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        public DateTime ActivityDate { get; set; }

        public string ImgPath { get; set; }

        public string ActivityCategoryName { get; set; }

        public string ActivityPlace { get; set; }
    }
}
