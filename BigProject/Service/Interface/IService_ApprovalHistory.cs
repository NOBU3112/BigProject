using BigProject.Entities;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_ApprovalHistory
    {
        PagedResult<DTO_ApprovalHistory> GetListApprovalHistories(int pageSize, int pageNumber);
        Task<ResponseObject<DTO_ApprovalHistory>> GetApprovalHistoryDetail(int id);
        Task<ResponseObject<PagedResult<DTO_ApprovalHistory>>> SearchApprovalHistory(Request_Search_ApprovalHistory request);
    }
}
