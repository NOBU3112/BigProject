using Azure.Core;
using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_OutstandingMember
    {
        Task<ResponseObject<DTO_OutstandingMember>> AddOutstandingMenber(Request_AddOutstandingMember request);

        //Task<ResponseObject<DTO_OutstandingMember>> WaitingOutstandingMenber(Request_waitingOutstandingMember request);

        Task<ResponseObject<DTO_OutstandingMemberApproval>> AcceptOutstandingMember(Request_acceptOutstandingMember request,int UserId);

        Task<ResponseObject<DTO_OutstandingMemberApproval>> RejectOutstandingMember(Request_rejectOutstandingMember request, int UserId);

    }
}
