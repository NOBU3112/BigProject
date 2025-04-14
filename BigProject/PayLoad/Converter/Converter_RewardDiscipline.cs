using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;
using Microsoft.EntityFrameworkCore;

namespace BigProject.PayLoad.Converter
{
    public class Converter_RewardDiscipline
    {
        private readonly AppDbContext appDbContext;

        public Converter_RewardDiscipline(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public DTO_RewardDiscipline EntityToDTO(RewardDiscipline rewardDiscipline)
        {
            return new DTO_RewardDiscipline()
            {
                Id = rewardDiscipline.Id,   
                Description = rewardDiscipline.Description,
                CreateDate = rewardDiscipline.CreateDate,
                ProposerName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == rewardDiscipline.ProposerId).FullName,
                RecipientName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == rewardDiscipline.RecipientId).FullName,
                ProposerMaSV = appDbContext.users.FirstOrDefault(x=>x.Id ==  rewardDiscipline.ProposerId).MaSV,
                RecipientMaSV = appDbContext.users.FirstOrDefault(X=> X.Id == rewardDiscipline.RecipientId).MaSV,
                //RewardDisciplineType = appDbContext.rewardDisciplineTypes.SingleOrDefault(x=>x.Id == rewardDiscipline.RewardDisciplineTypeId).RewardDisciplineTypeName,
                Status = rewardDiscipline.Status,
                RejectReason = rewardDiscipline.RejectReason,
                RewardOrDiscipline = rewardDiscipline.RewardOrDiscipline,
            };
        }
    }
}
