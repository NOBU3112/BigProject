using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Helper;
using BigProject.PayLoad.DTO;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
            var cloudinary = new CloudinaryService();
            return new DTO_RewardDiscipline()
            {
                Id = rewardDiscipline.Id,
                Description = rewardDiscipline.Description,
                CreateDate = rewardDiscipline.CreateDate,
                ProposerName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == rewardDiscipline.ProposerId).FullName,
                ProposerMaSV = appDbContext.users.FirstOrDefault(x => x.Id == rewardDiscipline.ProposerId).MaSV,
                Class = rewardDiscipline.Class,
                UrlFile = rewardDiscipline.UrlFile,
                UrlDecodeFile = cloudinary.GetEmbeddedUrl(rewardDiscipline.UrlFile),
                Status = rewardDiscipline.Status,
                RejectReason = rewardDiscipline.RejectReason,
                RewardOrDiscipline = rewardDiscipline.RewardOrDiscipline,
            };
        }
    }
}
