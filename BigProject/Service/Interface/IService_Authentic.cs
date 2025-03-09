using BigProject.Payload.Response;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;

namespace BigProject.Service.Interface
{
    public interface IService_Authentic
    {
        Task <ResponseObject<DTO_Register>> Register(Request_Register request);

        Task<ResponseObject<DTO_Register>> ForgotPassword(Request_forgot request);


        Task<ResponseObject<DTO_Token>> Login(Request_Login request);


        ResponseBase Activate(string Opt);

        ResponseBase Activate_Password (string code, string email);

        ResponseBase ChangePassword(Request_ChangePassword requset, int userId);

        Task<ResponseObject<List<DTO_Register>>> Authorization(string KeyRole);

        IEnumerable<DTO_Register> GetListMember(int pageSize, int pageNumber);
    }
}
