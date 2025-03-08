using Azure;
using BigProject.DataContext;
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

        public IEnumerable<DTO_ApprovalHistory> GetListApprovalHistories(int pageSize, int pageNumber)
        {
            return dbContext.approvalHistories.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_ApprovalHistory.EntityToDTO(x));
        }
    }
}
