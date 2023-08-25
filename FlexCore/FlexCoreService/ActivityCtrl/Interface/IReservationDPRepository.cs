using FlexCoreService.ActivityCtrl.Models.Dtos;
using FlexCoreService.ActivityCtrl.Models.VM;
using System.Threading.Tasks;

namespace FlexCoreService.ActivityCtrl.Interface
{
    public interface IReservationDPRepository
    {
        Task<IEnumerable<ReservationDTO>> GetAllAsync();

        Task<SpeakerDetailDTO> GetSpeakerInfoAsync(int id);

        Task<IEnumerable<ReservationHistoryDTO>> GetReservationHistoryAsync(int id);

        Task AddReservationAsync(AddReservationDTO dto);

        Task AddReservationCommentAsync(AddReservationCommentDTO dto);

        Task<IEnumerable<ReservationCommentDTO>> GetAllCommentAsync(int id);
    }
}
