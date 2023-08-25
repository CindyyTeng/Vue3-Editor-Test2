namespace FlexCoreService.ActivityCtrl.Models.VM
{
    public class AddReservationDTO
    {
        public int fk_BookerId { get; set; }
        public DateTime ReservationStartTime { get; set; }

        public DateTime ReservationEndTime { get; set; }
        public int fk_ReservationSpeakerId { get; set; }

        public int fk_BranchId { get; set; }

        public int fk_ReservationStatusId { get; set; }
    }
}
