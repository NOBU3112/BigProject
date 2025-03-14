using BigProject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_ApprovalHistory : ControllerBase
    {
        private readonly IService_ApprovalHistory serviceApprovalHistory;

        public Controller_ApprovalHistory(IService_ApprovalHistory serviceApprovalHistory)
        {
            this.serviceApprovalHistory = serviceApprovalHistory;
        }

        [HttpGet("Get_List_ApprovalHistories")]
        public IActionResult GetListApprovalHistories(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(serviceApprovalHistory.GetListApprovalHistories(pageSize, pageNumber));
        }
    }
}
