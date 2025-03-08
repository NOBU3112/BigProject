using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using BigProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigProject.Service.Implement
{
    public class Service_Event : IService_Event
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_Event> responseObject;
        private readonly ResponseObject<DTO_EventJoin> responseObjectEventJoin;
        private readonly Converter_Event converter_Event;
        private readonly Converter_EventJoin converter_EventJoin;
        private readonly ResponseBase responseBase;

        public Service_Event(AppDbContext dbContext, ResponseObject<DTO_Event> responseObject, ResponseObject<DTO_EventJoin> responseObjectEventJoin, Converter_Event converter_Event, Converter_EventJoin converter_EventJoin, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.responseObjectEventJoin = responseObjectEventJoin;
            this.converter_Event = converter_Event;
            this.converter_EventJoin = converter_EventJoin;
            this.responseBase = responseBase;
        }

        public async Task<ResponseObject<DTO_Event>> AddEvent(Request_AddEvent request)
        {
            var eventType_check = await dbContext.eventTypes.FirstOrDefaultAsync(x => x.Id == request.EventTypeId);
            if (eventType_check == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, " Loại hoạt động không tồn tại! ", null);
            }
            var eventName_check = await dbContext.events.FirstOrDefaultAsync(x => x.EventName == request.EventName);
            if (eventName_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, " Tên hoạt động không được trùng! ", null);
            }
            var event1 = new Event();
            event1.EventName = request.EventName;
            event1.EventLocation = request.EventLocation;
            event1.EventStartDate = request.EventStartDate;
            event1.EventEndDate = request.EventEndDate;
            event1.Description = request.Description;
            event1.EventTypeId = request.EventTypeId;
            dbContext.events.Add(event1);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_Event.EntityToDTO(event1));
        }

        public async Task<ResponseBase> DeleteEvent(int eventId)
        {
            var event1 = await dbContext.events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (event1 == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound,"Hoạt động không tồn tại!");
            }
            dbContext.events.Remove(event1);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public IEnumerable<DTO_Event> GetListEvent(int pageSize, int pageNumber)
        {
            return dbContext.events.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_Event.EntityToDTO(x));
        }

        public async Task<ResponseObject<DTO_Event>> UpdateEvent(Request_UpdateEvent request)
        {
            var event1 = await dbContext.events.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (event1 == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Hoạt động không tồn tại!", null);
            }
            var eventType_check = await dbContext.eventTypes.FirstOrDefaultAsync(x => x.Id == request.EventTypeId);
            if (eventType_check == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Loại hoạt động không tồn tại!", null);
            }
            var eventName_check = await dbContext.events.FirstOrDefaultAsync(x => x.EventName == request.EventName);
            if (eventName_check != null && event1.EventName != request.EventName)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Tên hoạt động không được trùng! ", null);
            }
            event1.EventName = request.EventName;
            event1.EventLocation = request.EventLocation;
            event1.EventStartDate = request.EventStartDate;
            event1.EventEndDate = request.EventEndDate;
            event1.Description = request.Description;
            event1.EventTypeId = request.EventTypeId;
            dbContext.events.Update(event1);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_Event.EntityToDTO(event1));
        }

        public async Task<ResponseObject<DTO_EventJoin>> JoinAnEvent(int userId, int eventId)
        {
            var eventCheck = await dbContext.events.FirstOrDefaultAsync(x=>x.Id==eventId);
            if(eventCheck == null)
            {
                return responseObjectEventJoin.ResponseObjectError(StatusCodes.Status404NotFound, "Hoạt động này không tồn tại!",null);
            }
            var eventJoinCheck = await dbContext.eventJoins.FirstOrDefaultAsync(x=>x.EventId==eventId && x.UserId==userId);
            if(eventJoinCheck != null)
            {
                return responseObjectEventJoin.ResponseObjectError(StatusCodes.Status400BadRequest, "Bạn đã tham gia hoạt động này!", null);
            }
            var eventJoin = new EventJoin();
            eventJoin.EventId = eventId;
            eventJoin.UserId = userId;
            eventJoin.Status = Enums.EventJointEnum.registered;
            dbContext.eventJoins.Add(eventJoin);
            await dbContext.SaveChangesAsync();
            return responseObjectEventJoin.ResponseObjectSuccess("Tham gia thành công!", converter_EventJoin.EntityToDTO(eventJoin));
        }

        public async Task<ResponseBase> WithdrawFromAnEvent(int eventJoinId)
        {
            var eventJoin = await dbContext.eventJoins.FirstOrDefaultAsync(x=>x.Id == eventJoinId);
            if(eventJoin == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Bạn chưa tham gia hoạt động này!");
            }
            dbContext.eventJoins.Remove(eventJoin);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Bỏ tham gia thành công!");
        }
        public IEnumerable<DTO_EventJoin> GetListAllEventUserJoin(int pageSize, int pageNumber, int userId)
        {
            return dbContext.eventJoins.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(x=>x.UserId == userId).Select(x => converter_EventJoin.EntityToDTO(x));
        }

        public IEnumerable<DTO_EventJoin> GetListAllParticipantInAnEvent(int pageSize, int pageNumber, int eventId)
        {
            return dbContext.eventJoins.Skip((pageNumber - 1) * pageSize).Take(pageSize).Where(x => x.EventId == eventId).Select(x => converter_EventJoin.EntityToDTO(x));
        }
    }
}
