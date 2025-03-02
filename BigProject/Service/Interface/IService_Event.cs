using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BigProject.Service.Interface
{
    public interface IService_Event
    {
        Task<ResponseObject<DTO_Event>> AddEvent(Request_AddEvent request);
        Task<ResponseObject<DTO_Event>> UpdateEvent(Request_UpdateEvent request);
        Task<ResponseBase> DeleteEvent(int Id);
        IQueryable<DTO_Event> GetListEvent(int pageSize, int pageNumber);
        Task<ResponseObject<DTO_EventJoin>> JoinAnEvent(int userId, int eventId);
        Task<ResponseBase> WithdrawFromAnEvent(int eventJoinId);
        IQueryable<DTO_EventJoin> GetListAllParticipantInAnEvent(int pageSize, int pageNumber,int eventId);
        IQueryable<DTO_EventJoin> GetListAllEventUserJoin(int pageSize, int pageNumber, int userId);
    }
}
