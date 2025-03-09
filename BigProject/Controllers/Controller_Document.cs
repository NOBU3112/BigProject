using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Document : ControllerBase
    {
        private readonly IService_Document service_Document;

        public Controller_Document(IService_Document service_Document)
        {
            this.service_Document = service_Document;
        }

        [HttpPost("Add_Document")]
        public async Task<IActionResult> AddDocument([FromForm] Request_AddDocument request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int userId = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await service_Document.AddDocument(request,userId));
        }

        [HttpPut("Update_Document")]
        public async Task<IActionResult> UpdateDocument([FromForm] Request_UpdateDocument request)
        {
            return Ok(await service_Document.UpdateDocument(request));
        }

        [HttpDelete("Delete_Document")]
        public async Task<IActionResult> DeleteDocument([FromForm] int Id)
        {
            return Ok(await service_Document.DeleteDocument(Id));
        }

        [HttpGet("Get_List_Document")]
        public IActionResult GetListDocument([FromForm] int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Document.GetListDocument(pageSize, pageNumber));
        }
    }
}
