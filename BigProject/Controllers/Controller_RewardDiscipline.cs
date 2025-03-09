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
    public class Controller_RewardDiscipline : ControllerBase
    {
        private readonly IService_RewardDiscipline service_RewardDiscipline;

        public Controller_RewardDiscipline(IService_RewardDiscipline service_RewardDiscipline)
        {
            this.service_RewardDiscipline = service_RewardDiscipline;
        }
        [HttpGet("Get_List_Reward")]
        public IActionResult GetListProposeRewardFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_RewardDiscipline.GetListReward(pageSize, pageNumber));
        }
        [HttpGet("Get_List_Discipline")]
        public IActionResult GetListProposeDisciplineFull(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_RewardDiscipline.GetListDiscipline(pageSize, pageNumber));
        }
        [HttpPost("Propose_Reward")]
        public async Task<IActionResult> ProposeReward(Request_ProposeRewardDiscipline request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_RewardDiscipline.ProposeReward(request,userId));
        }
        [HttpPost("Propose_Discipline")]
        public async Task<IActionResult> ProposeDiscipline(Request_ProposeRewardDiscipline request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_RewardDiscipline.ProposeDiscipline(request, userId));
        }
        [HttpPut("Accept_Propose")]
        public async Task<IActionResult> AcceptPropose(int proposeId)
        {
            return Ok(await service_RewardDiscipline.AcceptPropose(proposeId));
        }
        [HttpPut("Reject_Propose")]
        public async Task<IActionResult> RejectPropose(int proposeId)
        {
            return Ok(await service_RewardDiscipline.RejectPropose(proposeId));
        }
        [HttpDelete("Delete_Reward_Discipline")]
        public async Task<IActionResult> DeletePropose(int proposeId)
        {
            return Ok(await service_RewardDiscipline.DeleteRewardDiscipline(proposeId));
        }
    }
}
