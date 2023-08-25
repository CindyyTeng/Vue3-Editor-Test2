using EFModels.Models;
using FlexCoreService.ActivityCtrl.Models.Dtos;
using FlexCoreService.ActivityCtrl.Models.VM;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Principal;

namespace FlexCoreService.ActivityCtrl.Exts
{
    public static class ReservationExts
    {
        public static SpeakerDetailVM ToDetailVM(this SpeakerDetailDTO dto)
        {
            return new SpeakerDetailVM
            {
                SpeakerId = dto.SpeakerId,
                SpeakerName = dto.SpeakerName,
                SpeakerDescription = dto.SpeakerDescription,
                SpeakerImg = dto.SpeakerImg,
                FieldName = dto.FieldName,
                BranchName = dto.BranchName,
                BranchAddress = dto.BranchAddress,
                BranchId = dto.BranchId
            };
        }

        public static ReservationHistoryVM ToVM (this ReservationHistoryDTO dto)
        {
            return new ReservationHistoryVM
            {
                ReservationStartTime = dto.ReservationStartTime
            };
        }

        public static AddReservationCommentDTO ToDto (this AddReservationCommentVM vm)
        {
            return new AddReservationCommentDTO
            {
                fk_speakerId = vm.fk_speakerId,
                fk_memberId = vm.fk_memberId,
                content = vm.content,
                rating = vm.rating

            };
        }

        public static ReservationCommentVM ToVM ( this ReservationCommentDTO dto)
        {
            return new ReservationCommentVM
            {
                fk_speakerId = dto.fk_speakerId,
                fk_memberId = dto.fk_memberId,
                content = dto.content,
                id = dto.id,
                creationTime = dto.creationTime,
                Account = dto.Account,
                rating = dto.rating
            };
        }
    }
}
