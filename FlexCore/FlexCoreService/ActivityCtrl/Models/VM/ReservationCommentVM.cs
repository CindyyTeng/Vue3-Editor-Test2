namespace FlexCoreService.ActivityCtrl.Models.VM
{
    public class ReservationCommentVM
    {
        public int fk_speakerId { get; set; }
        public int fk_memberId { get; set; }
        public string content { get; set; }

        public int id { get; set; }

        public DateTime creationTime { get; set; }
        public string Account { get; set; }

        public int rating { get; set; }



    }
}
