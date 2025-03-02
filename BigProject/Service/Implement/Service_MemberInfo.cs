using Azure;
using BigProject.DataContext;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using BigProject.Entities;
using BigProject.Helper;

namespace BigProject.Service.Implement
{
    public class Service_MemberInfo : IService_MemberInfo
    {
        private readonly AppDbContext DbContext;
        private readonly ResponseObject<DTO_MemberInfo> responseObject;
        private readonly Converter_MemberInfo converter_MemberInfo;
        private readonly ResponseBase responseBase;

        public Service_MemberInfo(AppDbContext DbContext, ResponseObject<DTO_MemberInfo> responseObject, Converter_MemberInfo converter_MemberInfo, ResponseBase responseBase)
        {
            this.DbContext = DbContext;
            this.responseObject = responseObject;
            this.converter_MemberInfo = converter_MemberInfo;
            this.responseBase = responseBase;
        }

        public async Task<ResponseObject<DTO_MemberInfo>> AddMenberInfo(Request_AddMemberInfo request, int userId)
        {
            var Check_UserId = await DbContext.users.FirstOrDefaultAsync(x => x.Id == userId);
            if (Check_UserId == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
            }
            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (request.UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(request.UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
            }
            var memberInfo = new MemberInfo();
            memberInfo.Class = request.Class;
            memberInfo.Birthdate = request.Birthdate;
            memberInfo.PhoneNumber = request.PhoneNumber;
            memberInfo.Nation = request.Nation;
            memberInfo.DateOfJoining = request.DateOfJoining;
            memberInfo.FullName = request.FullName;
            memberInfo.MemberId = request.MemberId;
            memberInfo.religion = request.religion;
            memberInfo.UrlAvatar = UrlAvt;
            memberInfo.PlaceOfJoining = request.PlaceOfJoining;
            memberInfo.PoliticalTheory = request.PoliticalTheory;
            memberInfo.UserId = userId;

            DbContext.memberInfos.Add(memberInfo);
            await DbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công", converter_MemberInfo.EntityToDTO(memberInfo));
        }


        public IQueryable<DTO_MemberInfo> GetListMenberInfo(int pageSize, int pageNumber)
        {
            return DbContext.memberInfos.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_MemberInfo.EntityToDTO(x));
        }

        public async Task<ResponseObject<DTO_MemberInfo>> UpdateMenberInfo(Request_UpdateMemberInfo request)
        {
            var Check_Id = await DbContext.memberInfos.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (Check_Id == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
            }
            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (request.UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(request.UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
            }
            var memberInfo = new MemberInfo();
            memberInfo.Class = request.Class;
            memberInfo.Birthdate = request.Birthdate;
            memberInfo.PhoneNumber = request.PhoneNumber;
            memberInfo.Nation = request.Nation; 
            memberInfo.DateOfJoining = request.DateOfJoining;
            memberInfo.FullName = request.FullName;
            memberInfo.MemberId = request.MemberId;
            memberInfo.religion = request.religion;
            memberInfo.UrlAvatar = UrlAvt;
            memberInfo.PlaceOfJoining = request.PlaceOfJoining;
            memberInfo.PoliticalTheory = request.PoliticalTheory;
            

            DbContext.memberInfos.Update(memberInfo);
            await DbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công", converter_MemberInfo.EntityToDTO(memberInfo));
        }
    }
}
