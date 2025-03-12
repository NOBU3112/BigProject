﻿using Azure.Core;
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
       
        [HttpPut("Update_member_info")]
        public async Task<IActionResult> UpdateMenberInfo([FromForm] Request_UpdateMemberInfo request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await memberInfo.UpdateMenberInfo(request,userId));
        }

        [HttpPut("Update_user_img")]
        public async Task<IActionResult> UpdateUserImg(IFormFile? UrlAvatar)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await memberInfo.UpdateUserImg(UrlAvatar, userId));
        }

        [HttpGet("Get_List_Menber_Info")]
        [Authorize(Roles = "Liên chi đoàn khoa,Bí thư đoàn viên")]
        public IActionResult GetListMenberInfo( int pageSize = 10, int pageNumber = 1)
        {
            return Ok(memberInfo.GetListMenberInfo(pageSize, pageNumber));
        }

        [HttpGet("Get_Menber_Info")]
        public async Task<IActionResult> GetMemberInfo()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await memberInfo.GetMemberInfo(userId));
        }

    }
}
