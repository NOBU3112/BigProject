using BigProject.Entities;
using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Authenic : ControllerBase
    {
        private readonly IService_Authentic service_Authentic;

        public Controller_Authenic(IService_Authentic service_Authentic)
        {
            this.service_Authentic = service_Authentic;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromForm] Request_Register request)
        {
            return Ok(service_Authentic.Register(request));
        }

        [HttpPut("Forget_Password")]
        public IActionResult ForgotPassword([FromForm] Request_forgot request)
        {
            return Ok(service_Authentic.ForgotPassword(request));
        }
        [HttpPost("Login")]
        public IActionResult Login([FromForm] Request_Login request)
        {
            return Ok(service_Authentic.Login(request));
        }
        [HttpPut("Active_Account")]
        public IActionResult Activate([FromForm] string Opt)
        {
            return Ok(service_Authentic.Activate(Opt));
        }
        [HttpGet("phân quyền")]

        public IActionResult Authorization([FromForm] string KeyRole)
        {
            return Ok(service_Authentic.Authorization(KeyRole));
        }
        [HttpPut("Change_Password")]
        public IActionResult ChangePassword([FromForm] Request_ChangePassword request)
        {
            return Ok(service_Authentic.ChangePassword(request));
        }
        [HttpGet("Get_List_Member")]
        public IActionResult GetListMember(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Authentic.GetListMember(pageSize, pageNumber));
        }
    }
    

}
