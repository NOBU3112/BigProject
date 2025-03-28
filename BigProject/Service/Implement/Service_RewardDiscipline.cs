﻿using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using BigProject.Entities;
using BigProject.Enums;

namespace BigProject.Service.Implement
{
    public class Service_RewardDiscipline : IService_RewardDiscipline
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_RewardDiscipline> responseObject;
        private readonly Converter_RewardDiscipline converter_RewardDiscipline;
        private readonly ResponseBase responseBase;

        public Service_RewardDiscipline(AppDbContext dbContext, ResponseObject<DTO_RewardDiscipline> responseObject, Converter_RewardDiscipline converter_RewardDiscipline, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_RewardDiscipline = converter_RewardDiscipline;
            this.responseBase = responseBase;
        }

        public async Task<ResponseBase> DeleteRewardDiscipline(int id)
        {
            var Reward = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == id);
            if(Reward == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Đề xuất này không tồn tại!"); 
            };
            dbContext.rewardDisciplines.Remove(Reward);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public PagedResult<DTO_RewardDiscipline> GetListDiscipline(int pageSize, int pageNumber)
        {
            var query = dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == false);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_RewardDiscipline.EntityToDTO(x))
                .ToList(); // Chuyển sang List<T>

            return new PagedResult<DTO_RewardDiscipline>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }


        public PagedResult<DTO_RewardDiscipline> GetListReward(int pageSize, int pageNumber)
        {
            var query = dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == true);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_RewardDiscipline.EntityToDTO(x))
                .ToList(); // Chuyển sang List<T>

            return new PagedResult<DTO_RewardDiscipline>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }


        public async Task<ResponseObject<DTO_RewardDiscipline>> ProposeReward(Request_ProposeRewardDiscipline request, int proposerId)
        {
            var proposerCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == proposerId);
            if(proposerCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người đề xuất không tồn tại!", null);
            }
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == request.RecipientId);
            if(recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }
            //var rewardTypeCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.Id == request.RewardDisciplineTypeId && x.RewardOrDiscipline == true);
            //if(rewardTypeCheck == null)
            //{
            //    return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại giải thưởng không tồn tại!", null);
            //}
            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = true;
            proposer.Description = request.Description;
            //proposer.RewardDisciplineTypeId = request.RewardDisciplineTypeId;
            proposer.Status = RequestEnum.waiting;
            proposer.RecipientId = request.RecipientId;
            proposer.ProposerId = proposerId;
            dbContext.rewardDisciplines.Add(proposer);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDiscipline.EntityToDTO(proposer));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> ProposeDiscipline(Request_ProposeRewardDiscipline request, int proposerId)
        {
            var proposerCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == proposerId);
            if (proposerCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người đề xuất không tồn tại!", null);
            }
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.Id == request.RecipientId);
            if (recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }
            ////var disciplineTypeCheck = await dbContext.rewardDisciplineTypes.FirstOrDefaultAsync(x => x.Id == request.RewardDisciplineTypeId && x.RewardOrDiscipline == false);
            //if (disciplineTypeCheck == null)
            //{
            //    return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại kỷ luật không tồn tại!", null);
            //}
            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = false;
            proposer.Description = request.Description;
            //proposer.RewardDisciplineTypeId = request.RewardDisciplineTypeId;
            proposer.Status = RequestEnum.waiting;
            proposer.RecipientId = request.RecipientId;
            proposer.ProposerId = proposerId;
            dbContext.rewardDisciplines.Add(proposer);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDiscipline.EntityToDTO(proposer));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> AcceptPropose(int proposeId,int userId)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            propose.Status = RequestEnum.accept;
            dbContext.rewardDisciplines.Update(propose);
            await dbContext.SaveChangesAsync();

            var history = new ApprovalHistory();
            history.IsAccept = true;
            history.ApprovedDate = DateTime.Now;
            history.ApprovedById = userId;
            history.RewardDisciplineId = propose.Id;
            if (propose.RewardOrDiscipline)
            {
                history.HistoryType = HistoryEnum.reward.ToString();
            }
            else
            {
                history.HistoryType = HistoryEnum.discipline.ToString();
            }
            dbContext.approvalHistories.Add(history);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Chấp nhận!", converter_RewardDiscipline.EntityToDTO(propose));
        }

        public async Task<ResponseObject<DTO_RewardDiscipline>> RejectPropose(int proposeId,int userId, string reject)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            propose.Status = RequestEnum.reject;
            propose.RejectReason = reject;
            dbContext.rewardDisciplines.Update(propose);
            await dbContext.SaveChangesAsync();

            var history = new ApprovalHistory();
            history.IsAccept = false;
            history.ApprovedDate = DateTime.Now;
            history.ApprovedById = userId;
            history.RewardDisciplineId = propose.Id;
            if (propose.RewardOrDiscipline)
            {
                history.HistoryType = HistoryEnum.reward.ToString();
            }
            else
            {
                history.HistoryType = HistoryEnum.discipline.ToString();
            }
            //history.RejectReason = reject;
            dbContext.approvalHistories.Add(history);

            await dbContext.SaveChangesAsync();

            return responseObject.ResponseObjectSuccess("Từ chối!", converter_RewardDiscipline.EntityToDTO(propose));
        }

        public PagedResult<DTO_RewardDiscipline> GetListWaiting(int pageSize, int pageNumber)
        {
            var query = dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == true);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_RewardDiscipline.EntityToDTO(x))
                .ToList(); // Chuyển thành List<T>

            return new PagedResult<DTO_RewardDiscipline>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }

    }
}
    