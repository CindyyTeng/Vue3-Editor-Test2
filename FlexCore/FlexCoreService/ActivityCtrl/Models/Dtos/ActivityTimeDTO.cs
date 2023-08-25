using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexCoreService.ActivityCtrl.Models.Dtos
{
    public class ActivityBookingTimeDTO
    {
        public DateTime ActivityBookStartTime { get; set; }
  
        public DateTime ActivityBookEndTime { get; set; }
    }
}
