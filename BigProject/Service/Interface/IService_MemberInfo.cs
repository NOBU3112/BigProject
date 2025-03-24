using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Payload.Response;

namespace BigProject.Service.Interface
{
    public interface IService_MemberInfo
    {
        //Task<ResponseObject<DTO_MemberInfo>> AddMenberInfo(Request_AddMemberInfo request, int userId);
        Task<ResponseObject<DTO_MemberInfo>> UpdateMenberInfo(Request_UpdateMemberInfo request, int userId);
        Task<ResponseObject<DTO_MemberInfo>> UpdateUserImg(IFormFile? UrlAvatar, int userId);
        IEnumerable<DTO_MemberInfo> GetListMenberInfo(int pageSize, int pageNumber);
        Task<ResponseObject<DTO_MemberInfo>> GetMemberInfo(int userId); 
        Task<ResponseObject<List<DTO_MemberInfo>>> SearchMembers(Request_Search_Member request);
    }
}
