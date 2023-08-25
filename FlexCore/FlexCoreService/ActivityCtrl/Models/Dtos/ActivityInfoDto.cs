namespace FlexCoreService.ActivityCtrl.Models.Dtos
{
    public class ActivityInfoDto
    {
        public string ActivityName { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityCategoryName { get; set; }
        public byte ActivityAge { get; set; }
        public int ActivitySalePrice { get; set; }
        public int ActivityOriginalPrice { get; set; }
        public string ActivityPlace { get; set; }
        public string ActivityDescription { get; set; }

        public string SpeakerName { get; set; }

        public string ImgPath { get; set; }

    }
}
