namespace FlexCoreService.ActivityCtrl.Models.VM
{
    public class AddReservationCommentDTO
    {
        public int fk_speakerId { get; set; }
        public int fk_memberId { get; set; }
        public string content { get; set; }
        public int rating { get; set; }
        
    }
}
