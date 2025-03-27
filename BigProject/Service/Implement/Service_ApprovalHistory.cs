using Azure;
using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;

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

    }
}
