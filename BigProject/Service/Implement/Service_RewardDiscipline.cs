using BigProject.DataContext;
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
        private readonly ResponseObject<DTO_RewardDisciplineApproval> responseObject2;
        private readonly Converter_RewardDiscipline converter_RewardDiscipline;
        private readonly Converter_RewardDisciplineApproval converter_RewardDiscipline2;
        private readonly ResponseBase responseBase;

        public Service_RewardDiscipline(AppDbContext dbContext, ResponseObject<DTO_RewardDiscipline> responseObject, ResponseObject<DTO_RewardDisciplineApproval> responseObject2, Converter_RewardDiscipline converter_RewardDiscipline, Converter_RewardDisciplineApproval converter_RewardDiscipline2, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.responseObject2 = responseObject2;
            this.converter_RewardDiscipline = converter_RewardDiscipline;
            this.converter_RewardDiscipline2 = converter_RewardDiscipline2;
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
            var query = dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == false && x.Status == RequestEnum.accept);

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
            var query = dbContext.rewardDisciplines.Where(x => x.RewardOrDiscipline == true && x.Status == RequestEnum.accept);

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
            if (proposerCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người đề xuất không tồn tại!", null);
            }

            // Tìm kiếm người nhận bằng MaSV
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.MaSV == request.RecipientMaSV);
            if (recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }

            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = true;
            proposer.Description = request.Description;
            proposer.Status = RequestEnum.waiting;
            proposer.RecipientId = recipientCheck.Id;  // Lưu Id của người nhận
            proposer.ProposerId = proposerId;  // Giữ nguyên proposerId
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

            // Tìm kiếm người nhận bằng MaSV
            var recipientCheck = await dbContext.users.FirstOrDefaultAsync(x => x.MaSV == request.RecipientMaSV);
            if (recipientCheck == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Người được đề xuất không tồn tại!", null);
            }

            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = false;
            proposer.Description = request.Description;
            proposer.Status = RequestEnum.waiting;
            proposer.RecipientId = recipientCheck.Id;  // Lưu Id của người nhận
            proposer.ProposerId = proposerId;  // Giữ nguyên proposerId
            dbContext.rewardDisciplines.Add(proposer);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_RewardDiscipline.EntityToDTO(proposer));
        }

        public async Task<ResponseObject<DTO_RewardDisciplineApproval>> AcceptPropose(int proposeId,int userId)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject2.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            if (propose.Status != RequestEnum.waiting)
            {
                return responseObject2.ResponseObjectError(StatusCodes.Status400BadRequest, "Đề xuất đã được kiểm duyệt", null);
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
            return responseObject2.ResponseObjectSuccess("Chấp nhận!", converter_RewardDiscipline2.EntityToDTO(propose));
        }

        public async Task<ResponseObject<DTO_RewardDisciplineApproval>> RejectPropose(int proposeId,int userId, string reject)
        {
            var propose = await dbContext.rewardDisciplines.FirstOrDefaultAsync(x => x.Id == proposeId);
            if (propose == null)
            {
                return responseObject2.ResponseObjectError(StatusCodes.Status404NotFound, "Đề xuất không tồn tại!", null);
            }
            if (propose.Status != RequestEnum.waiting)
            {
                return responseObject2.ResponseObjectError(StatusCodes.Status400BadRequest, "Đề xuất đã được kiểm duyệt", null);
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

            return responseObject2.ResponseObjectSuccess("Từ chối!", converter_RewardDiscipline2.EntityToDTO(propose));
        }

        public PagedResult<DTO_RewardDiscipline> GetListWaiting(int pageSize, int pageNumber)
        {
            var query = dbContext.rewardDisciplines.Where( x=> x.Status == RequestEnum.waiting);

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
    