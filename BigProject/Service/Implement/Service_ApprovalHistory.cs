using Azure;
using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BigProject.Service.Implement
{
    public class Service_ApprovalHistory : IService_ApprovalHistory
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_ApprovalHistory> responseObject;
        private readonly ResponseBase ResponseBase;
        private readonly Converter_ApprovalHistory converter_ApprovalHistory;

        public Service_ApprovalHistory(AppDbContext dbContext, ResponseObject<DTO_ApprovalHistory> responseObject, ResponseBase responseBase, Converter_ApprovalHistory converter_ApprovalHistory)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            ResponseBase = responseBase;
            this.converter_ApprovalHistory = converter_ApprovalHistory;
        }
        public PagedResult<DTO_ApprovalHistory> GetListApprovalHistories(int pageSize, int pageNumber)
        {
            int totalItems = dbContext.approvalHistories.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = dbContext.approvalHistories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_ApprovalHistory.EntityToDTO(x))
                .ToList(); // Chuyển sang List<T>

            return new PagedResult<DTO_ApprovalHistory>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }

        public async Task<ResponseObject<PagedResult<DTO_ApprovalHistory>>> SearchApprovalHistory(Request_Search_ApprovalHistory request)
        {
            var listHistory = dbContext.approvalHistories.AsQueryable(); // Danh sách chưa lọc
                                                                         // Lọc dữ liệu theo điều kiện
            if (!string.IsNullOrEmpty(request.MaSV))
            {
                var member = dbContext.users.FirstOrDefault(x => x.MaSV == request.MaSV);

                listHistory = listHistory.Where(x =>
                    (x.RequestToBeOutstandingMemberId != null
                        && x.RequestToBeOutstandingMember != null
                        && x.RequestToBeOutstandingMember.MemberInfoId == member.Id)
                    || (x.RewardDisciplineId != null
                        && x.RewardDiscipline != null
                        && x.RewardDiscipline.RecipientId == member.Id)
                );
            }

            if (!string.IsNullOrEmpty(request.HistoryType))
                listHistory = listHistory.Where(x => x.HistoryType == request.HistoryType);

            if (request.IsAccept == true)
                listHistory = listHistory.Where(x => x.IsAccept == true);

            if (request.IsRejecet == true)
                listHistory = listHistory.Where(x => x.IsAccept == false);

            // Tổng số phần tử sau khi lọc
            int totalItems = await listHistory.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

            // Lấy danh sách sau khi phân trang
            var items = await listHistory
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => converter_ApprovalHistory.EntityToDTO(x))
                .ToListAsync();

            // Trả về kết quả dưới dạng `PagedResult<T>`
            var pagedResult = new PagedResult<DTO_ApprovalHistory>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber
            };

            return new ResponseObject<PagedResult<DTO_ApprovalHistory>>().ResponseObjectSuccess("Danh sách lịch sử chấp thuận:", pagedResult);
        }

        public async Task<ResponseObject<DTO_ApprovalHistory>> GetApprovalHistoryDetail(int id)
        {
            var approvalHistory = await dbContext.approvalHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (approvalHistory == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Lịch sử không tồn tại", null);
            }
            return responseObject.ResponseObjectSuccess("Lấy thông tin thành công!", converter_ApprovalHistory.EntityToDTO(approvalHistory));
        }
    }
}
