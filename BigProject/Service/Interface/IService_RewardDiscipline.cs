using BigProject.Entities;
using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_RewardDiscipline
    {
        Task<ResponseObject<DTO_RewardDiscipline>> ProposeReward(Request_ProposeRewardDiscipline request,int proposerId);
        Task<ResponseObject<DTO_RewardDiscipline>> ProposeDiscipline(Request_ProposeRewardDiscipline request,int proposerId);
        Task<ResponseBase> DeleteRewardDiscipline(int id);
        PagedResult<DTO_RewardDiscipline> GetListReward(int pageSize, int pageNumber);
        PagedResult<DTO_RewardDiscipline> GetListDiscipline(int pageSize, int pageNumber);
        PagedResult<DTO_RewardDiscipline> GetListWaiting(int pageSize, int pageNumber);
        Task<ResponseObject<DTO_RewardDisciplineApproval>> AcceptPropose(int proposeId, int userId);
        Task<ResponseObject<DTO_RewardDisciplineApproval>> RejectPropose(int proposeId, int userId, string reject);
    }
}
