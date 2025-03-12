using Azure.Core;
using BigProject.Entities;
using BigProject.PayLoad.Converter;
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
    public class Controller_MemberInfo : ControllerBase
    {
        private readonly IService_MemberInfo memberInfo;

        public Controller_MemberInfo(IService_MemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }
       
        [HttpGet("Get_List_Menber_Info")]   
        [Authorize(Roles = "Liên chi đoàn khoa,Bí thư đoàn viên")]
        public IActionResult GetListProductFull( int pageSize = 10, int pageNumber = 1)
        {
            return Ok(memberInfo.GetListMenberInfo(pageSize, pageNumber));
        }
    }
}
