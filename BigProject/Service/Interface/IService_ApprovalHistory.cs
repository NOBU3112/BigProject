using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_ApprovalHistory
    {

        IEnumerable<DTO_ApprovalHistory> GetListApprovalHistories(int pageSize, int pageNumber);
    }
}
