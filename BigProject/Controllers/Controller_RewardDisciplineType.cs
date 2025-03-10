//using BigProject.PayLoad.Request;
//using BigProject.Service.Interface;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BigProject.Controllers
//{
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Controller_RewardDisciplineType : ControllerBase
//    {
//        private readonly IService_RewardDisciplineType service_RewardDisciplineType;

//        public Controller_RewardDisciplineType(IService_RewardDisciplineType service_RewardDisciplineType)
//        {
//            this.service_RewardDisciplineType = service_RewardDisciplineType;
//        }

//        [HttpGet("Get_List_Reward_Type")]
//        public IActionResult GetListRewardTypeFull( int pageSize = 10, int pageNumber = 1)
//        {
//            return Ok(service_RewardDisciplineType.GetListRewardType(pageSize, pageNumber));
//        }
//        [HttpGet("GetList_Discipline_Type")]
//        public IActionResult GetListDisciplineTypeFull( int pageSize = 10, int pageNumber = 1)
//        {
//            return Ok(service_RewardDisciplineType.GetListDisciplineType(pageSize, pageNumber));
//        }
//        [HttpPost("Add_Reward_Type")]
//        public async Task<IActionResult> AddRewardType([FromForm] string rewardTypeName)
//        {
//            return Ok(await service_RewardDisciplineType.AddRewardType(rewardTypeName));
//        }

//        [HttpPost("Add_Discipline_Type")]
//        public async Task<IActionResult> AddDisciplineType([FromForm] string disciplineTypeName)
//        {
//            return Ok(await service_RewardDisciplineType.AddDisciplineType(disciplineTypeName));
//        }

//        [HttpDelete("Delete_Reward_Discipline_Type")]
//        public async Task<IActionResult> DeleteRewardDisciplineType([FromForm] int proposeId)
//        {
//            return Ok(await service_RewardDisciplineType.DeleteRewardDisciplineType(proposeId));
//        }
//    }
//}
