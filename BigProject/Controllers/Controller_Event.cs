using Azure.Core;
using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Event : ControllerBase
    {
        private readonly IService_Event service_Event;

        public Controller_Event(IService_Event service_Event)
        {
            this.service_Event = service_Event;
        }

        [HttpPost("Thêm hoạt động")]
        public async Task<IActionResult> AddEvent(Request_AddEvent request)
        {
            return Ok(await service_Event.AddEvent(request));
        }

        [HttpPut("Sửa đổi hoạt động")]
        public async Task<IActionResult> UpdateEvent(Request_UpdateEvent request)
        {
            return Ok(await service_Event.UpdateEvent(request));
        }

        [HttpDelete("Xóa hoạt động")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            return Ok(await service_Event.DeleteEvent(eventId));
        }

        [HttpGet("Lấy danh sách hoạt động")]
        public IActionResult GetListProductFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Event.GetListEvent(pageSize, pageNumber));
        }

        [HttpPost("Đăng ký tham gia hoạt động")]
        public async Task<IActionResult> JoinAnEvent(int eventId)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return  Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_Event.JoinAnEvent(eventId, userId));
        }

        [HttpDelete("Bỏ đăng ký tham gia hoạt động")]
        public async Task<IActionResult> WithdrawFromAnEvent(int eventJoinId)
        {
            return Ok(await service_Event.WithdrawFromAnEvent(eventJoinId));
        }

        [HttpGet("Lấy danh sách các sinh viên tham gia hoạt động")]
        public IActionResult GetListAllParticipantInAnEvent(int eventId,int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Event.GetListAllParticipantInAnEvent(pageSize, pageNumber,eventId));
        }

        [HttpGet("Lấy danh sách các hoạt động tham gia")]
        public IActionResult GetListAllParticipantInAnEvent(int pageSize = 10, int pageNumber = 1)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(service_Event.GetListAllEventUserJoin(pageSize, pageNumber, userId));
        }
    }
}
