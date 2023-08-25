namespace FlexCoreService.ActivityCtrl.Models.VM
{
    public class AddReservationVM
    {
        public int fk_BookerId { get; set; }
        public DateTime ReservationStartTime { get; set; }
        public int fk_ReservationSpeakerId { get; set; }
        public int fk_BranchId { get; set; }
        
    }
}
