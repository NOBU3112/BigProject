using Azure.Core;
using BigProject.Entities;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.Request;
using BigProject.Service.Implement;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_MemberInfo : ControllerBase
    {
        private readonly IService_MemberInfo memberInfo;

        public Controller_MemberInfo(IService_MemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }
        [HttpPost("Thêm thông tin  đoàn viên")]
        public async Task<IActionResult> AddMenberInfo(Request_AddMemberInfo request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok( await memberInfo.AddMenberInfo(request,  userId));
        }
        [HttpPut("Sửa thông tin đoàn viên")]
        public async Task<IActionResult> UpdateMenberInfo(Request_UpdateMemberInfo request)
        {
            return Ok(await memberInfo.UpdateMenberInfo(request));
        }
        [HttpGet("Lấy danh thông tin đoàn viên")]
        public IActionResult GetListProductFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(memberInfo.GetListMenberInfo(pageSize, pageNumber));
        }
    }
}
