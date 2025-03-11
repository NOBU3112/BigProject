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
       
        [HttpPut("Update_member_info")]
        public async Task<IActionResult> UpdateMenberInfo([FromForm] Request_UpdateMemberInfo request)
        {
            return Ok(await memberInfo.UpdateMenberInfo(request));
        }
        [HttpGet("Get_List_Menber_Info")]
        public IActionResult GetListProductFull( int pageSize = 10, int pageNumber = 1)
        {
            return Ok(memberInfo.GetListMenberInfo(pageSize, pageNumber));
        }
    }
}
