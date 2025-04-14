using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_RewardDisciplineApproval
    {
        private readonly AppDbContext appDbContext;

        public Converter_RewardDisciplineApproval(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public DTO_RewardDisciplineApproval EntityToDTO(RewardDiscipline rewardDiscipline)
        {
            var approvalHistory = appDbContext.approvalHistories.FirstOrDefault(x => x.RewardDisciplineId == rewardDiscipline.Id);
            return new DTO_RewardDisciplineApproval()
            {
                Id = rewardDiscipline.Id,
                Description = rewardDiscipline.Description,
                ProposerName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == rewardDiscipline.ProposerId).FullName,
                RecipientName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == rewardDiscipline.RecipientId).FullName,
                ProposerMaSV = appDbContext.users.FirstOrDefault(x => x.Id == rewardDiscipline.ProposerId).MaSV,
                RecipientMaSV = appDbContext.users.FirstOrDefault(X => X.Id == rewardDiscipline.RecipientId).MaSV,
                RejectReason = rewardDiscipline.RejectReason,
                RewardOrDiscipline = rewardDiscipline.RewardOrDiscipline,
                ApprovedDate = approvalHistory.ApprovedDate,
                HistoryType = approvalHistory.HistoryType,
                IsAccept = approvalHistory.IsAccept,
                ApprovedByName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == approvalHistory.ApprovedById).FullName,
                ApprovedByMaSV = appDbContext.users.FirstOrDefault(x=> x.Id == approvalHistory.ApprovedById).MaSV,
            };
        }
    }
}
