﻿using BigProject.DataContext;
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
                ProposerName = appDbContext.memberInfos.Include(x=> x.User).AsNoTracking().FirstOrDefault(x => x.UserId == rewardDiscipline.ProposerId).FullName,
                RecipientName = appDbContext.memberInfos.Include(x => x.User).AsNoTracking().FirstOrDefault(x => x.UserId == rewardDiscipline.RecipientId).FullName,
                RewardDisciplineType = appDbContext.rewardDisciplineTypes.SingleOrDefault(x=>x.Id == rewardDiscipline.RewardDisciplineTypeId).RewardDisciplineTypeName,
                Status = rewardDiscipline.Status,
            };
        }
    }
}
