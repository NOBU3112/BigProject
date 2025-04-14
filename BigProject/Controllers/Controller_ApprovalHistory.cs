using BigProject.Entities;
using BigProject.PayLoad.Request;
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

        [HttpGet("Search_ApprovalHistories")]
        public async Task<IActionResult> SearchApprovalHistories([FromQuery] Request_Search_ApprovalHistory request)
        {
            var result = await serviceApprovalHistory.SearchApprovalHistory(request);

            // Kiểm tra nếu không có dữ liệu
            if (result?.Data == null || !result.Data.Items.Any())
            {
                return NotFound(new { message = "Không tìm thấy lịch sử chấp thuận phù hợp!" });
            }

            // Trả về kết quả thành công
            return Ok(result);
        }

        [HttpPut("Get_ApprovalHistory_Detail")]
        public async Task<IActionResult> GetApprovalHistoryDetail([FromForm] int id)
        {
            return Ok(await serviceApprovalHistory.GetApprovalHistoryDetail(id));
        }
    }
}
