using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Controller_OutstandingMember : ControllerBase
    {
        private readonly IService_OutstandingMember service_OutstandingMember;

        public Controller_OutstandingMember(IService_OutstandingMember service_OutstandingMember)
        {
            this.service_OutstandingMember = service_OutstandingMember;
        }

        [HttpPost("Request_OutstandingMember")]
        public async Task<IActionResult> AddOutstandingMember([FromForm] Request_AddOutstandingMember request)
        {
            return Ok(await service_OutstandingMember.AddOutstandingMenber(request));
        }

        //[HttpPut("Waiting_OutstandingMember")]

        //public async Task<IActionResult> WaitingOutstandingMenber([FromForm] Request_waitingOutstandingMember request)
        //{
        //    return Ok(await service_OutstandingMember.WaitingOutstandingMenber(request));
        //}

        [HttpPut("Accept_OutstandingMember")]
        [Authorize(Roles = "Liên chi đoàn khoa")]
        public async Task<IActionResult> AcceptOutstandingMember([FromForm] Request_acceptOutstandingMember request )
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_OutstandingMember.AcceptOutstandingMember(request,userId));
        }

        [HttpPut("Reject_OutstandingMember")]
        [Authorize(Roles = "Liên chi đoàn khoa")]
        public async Task<IActionResult> RejectOutstandingMember([FromForm] Request_rejectOutstandingMember request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_OutstandingMember.RejectOutstandingMember(request,userId));
        }
    }
}
