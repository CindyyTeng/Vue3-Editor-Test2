using Dapper;
using EFModels.Models;
using FlexCoreService.ActivityCtrl.Interface;
using FlexCoreService.ActivityCtrl.Models.Dtos;
using FlexCoreService.ActivityCtrl.Models.VM;
using Microsoft.Data.SqlClient;

namespace FlexCoreService.ActivityCtrl.Infra.DPRepository
{
    public class ReservationDPRepositorycs : IReservationDPRepository
    {
        private string _connStr;
        private IConfiguration _configuration;

        public ReservationDPRepositorycs(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("AppDbContext");
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllAsync()
        {
            string sql = @"select SpeakerId, SpeakerName, FieldName, SpeakerImg 
From Speakers
Join SpeakerFields
ON Speakers.fk_SpeakerFieldId = SpeakerFields.FieldId";

            using (var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryAsync<ReservationDTO>(sql);
            }
        }
        public async Task<SpeakerDetailDTO> GetSpeakerInfoAsync(int id)
        {
            string sql = @"select SpeakerId, SpeakerName, FieldName, SpeakerImg, SpeakerDescription, BranchName, BranchAddress, BranchId
From Speakers
Join SpeakerFields
ON Speakers.fk_SpeakerFieldId = SpeakerFields.FieldId
Join Branches
On Branches.BranchId = Speakers.fk_SpeakerBranchId
Where SpeakerId = @id";

            using (var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryFirstOrDefaultAsync<SpeakerDetailDTO>(sql, new { id });
            }

        }
        public async Task<IEnumerable<ReservationHistoryDTO>> GetReservationHistoryAsync(int id)
        {
            string sql = @"select ReservationStartTime From OneToOneReservations WHERE fk_ReservationSpeakerId = @id";

            using (var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryAsync<ReservationHistoryDTO>(sql, new { id });
            }
        }

        public async Task AddReservationAsync(AddReservationDTO dto)
        {
            await Task.Run(() =>
            {
                string sql = @"INSERT INTO OneToOneReservations (fk_BookerId, ReservationStartTime, ReservationEndTime, fk_BranchId, fk_ReservationSpeakerId, fk_ReservationStatusId)
                  VALUES (@bookerId, @startTime, @endTime, @branchId, @speakerId, @statusId);";

                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Execute(sql, new
                    {
                        bookerId = dto.fk_BookerId,
                        startTime = dto.ReservationStartTime,
                        endTime = dto.ReservationEndTime,
                        branchId = dto.fk_BranchId,
                        speakerId = dto.fk_ReservationSpeakerId,
                        statusId = dto.fk_ReservationStatusId
                    });
                }
            });
           
        }

        public async Task AddReservationCommentAsync(AddReservationCommentDTO dto)
        {
            await Task.Run(() =>
            {
                string sql = @"
INSERT INTO ReservationReviews (fk_speakerId, fk_memberId, content, rating )
VALUES (@speakerId, @memberId, @content, @rating)
";
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Execute(sql, new
                    {
                        speakerId = dto.fk_speakerId,
                        memberId = dto.fk_memberId,
                        content = dto.content,
                        rating = dto.rating
                    });
                }
            });
           

        }

        public async Task<IEnumerable<ReservationCommentDTO>> GetAllCommentAsync(int id)
        {
            string sql = @"
SELECT 
  id,
  fk_speakerId,
  fk_memberId,
  content,
  creationTime,
  Account, rating
FROM Flex.dbo.ReservationReviews
JOIN Members ON Members.MemberId = ReservationReviews.fk_memberId
WHERE fk_speakerId = @id
ORDER BY creationTime DESC";


            using (var conn = new SqlConnection(_connStr))
            {
                return await conn.QueryAsync<ReservationCommentDTO>(sql, new { id});
            }
        }

       
    }
}
