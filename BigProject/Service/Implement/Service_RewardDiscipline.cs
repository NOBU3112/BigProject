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
using BigProject.Helper;
using System.Reflection;

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
            var cloudinary = new CloudinaryService();
            try
            {
                bool isDeleted = await cloudinary.DeleteFile(Reward.UrlFile);
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần thiết
                return responseBase.ResponseBaseError(StatusCodes.Status500InternalServerError, $"Lỗi khi xóa file: {ex.Message}");
            }
            dbContext.rewardDisciplines.Remove(Reward);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public PagedResult<DTO_RewardDiscipline> GetListDiscipline(int pageSize, int pageNumber)
        {
            var query = dbContext.rewardDisciplines
                .Where(x => x.RewardOrDiscipline == false && x.Status == RequestEnum.accept);

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
            var query = dbContext.rewardDisciplines
                .Where(x => x.RewardOrDiscipline == true && x.Status == RequestEnum.accept);

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

            string UrlFile = null;
            var cloudinary = new CloudinaryService();
            UrlFile = await cloudinary.UploadFile(request.Url);

            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = true;
            proposer.Description = request.Description;
            proposer.Status = RequestEnum.waiting;
            proposer.Class = request.Class;
            proposer.UrlFile = UrlFile;
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

            string UrlFile = null;
            var cloudinary = new CloudinaryService();
            UrlFile = await cloudinary.UploadFile(request.Url);

            var proposer = new RewardDiscipline();
            proposer.RewardOrDiscipline = false;
            proposer.Description = request.Description;
            proposer.Status = RequestEnum.waiting;
            proposer.Class = request.Class;
            proposer.UrlFile = UrlFile;
            proposer.ProposerId = proposerId; 
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

            history.HistoryType = propose.RewardOrDiscipline
               ? HistoryEnum.reward.ToString()
                 : HistoryEnum.discipline.ToString();
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

            history.HistoryType = propose.RewardOrDiscipline 
                ? HistoryEnum.reward.ToString():HistoryEnum.discipline.ToString();
      
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

        public ResponseObject<DTO_RewardDisciplineApproval> GetRewardDisciplineDetail(int id)
        {
            var rewardDiscipline = dbContext.rewardDisciplines.FirstOrDefault(x => x.Id == id);
            if(rewardDiscipline == null)
            {
                return responseObject2.ResponseObjectError(400, "Không tồn tại!", null);
            }
            return responseObject2.ResponseObjectSuccess("Thành công!", converter_RewardDiscipline2.EntityToDTO(rewardDiscipline));
        }

        public async Task<ResponseObject<PagedResult<DTO_RewardDiscipline>>> SearchReward(Request_Search_RewardDiscipline request)
        {
            var listRewardDiscipline = dbContext.rewardDisciplines.AsQueryable(); // Danh sách chưa lọc

            listRewardDiscipline = listRewardDiscipline.Where(x => x.RewardOrDiscipline == true);
            if (!string.IsNullOrEmpty(request.Description))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Description.Contains(request.Description));
            if (!string.IsNullOrEmpty(request.Class))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Class.Contains(request.Class));
            if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<RequestEnum>(request.Status, ignoreCase: true, out var parsedStatus))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Status == parsedStatus);
            if (request.CreateDay.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Day == request.CreateDay.Value);
            if (request.CreateMonth.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Month == request.CreateMonth.Value);
            if (request.CreateYear.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Year == request.CreateYear.Value);

            // Tổng số phần tử sau khi lọc
            int totalItems = await listRewardDiscipline.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

            // Lấy danh sách sau khi phân trang
            var items = await listRewardDiscipline
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => converter_RewardDiscipline.EntityToDTO(x))
                .ToListAsync();

            // Trả về kết quả dưới dạng `PagedResult<T>`
            var pagedResult = new PagedResult<DTO_RewardDiscipline>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber
            };

            return new ResponseObject<PagedResult<DTO_RewardDiscipline>>().ResponseObjectSuccess("Danh sách khen thưởng :", pagedResult);
        }

        public async Task<ResponseObject<PagedResult<DTO_RewardDiscipline>>> SearchDisciplines(Request_Search_RewardDiscipline request)
        {
            var listRewardDiscipline = dbContext.rewardDisciplines.AsQueryable(); // Danh sách chưa lọc

            listRewardDiscipline = listRewardDiscipline.Where(x => x.RewardOrDiscipline == false);
            if (!string.IsNullOrEmpty(request.Description))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Description.Contains(request.Description));
            if (!string.IsNullOrEmpty(request.Class))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Class.Contains(request.Class));
            if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<RequestEnum>(request.Status, ignoreCase: true, out var parsedStatus))
                listRewardDiscipline = listRewardDiscipline.Where(x => x.Status == parsedStatus);
            if (request.CreateDay.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Day == request.CreateDay.Value);
            if (request.CreateMonth.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Month == request.CreateMonth.Value);
            if (request.CreateYear.HasValue)
                listRewardDiscipline = listRewardDiscipline.Where(x => x.CreateDate.Year == request.CreateYear.Value);

            // Tổng số phần tử sau khi lọc
            int totalItems = await listRewardDiscipline.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

            // Lấy danh sách sau khi phân trang
            var items = await listRewardDiscipline
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => converter_RewardDiscipline.EntityToDTO(x))
                .ToListAsync();

            // Trả về kết quả dưới dạng `PagedResult<T>`
            var pagedResult = new PagedResult<DTO_RewardDiscipline>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber
            };

            return new ResponseObject<PagedResult<DTO_RewardDiscipline>>().ResponseObjectSuccess("Danh sách kỷ luật :", pagedResult);
        }
    }
}
    