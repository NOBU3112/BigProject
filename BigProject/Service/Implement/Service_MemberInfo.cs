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
using Azure.Core;

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

        //public async Task<ResponseObject<DTO_MemberInfo>> AddMenberInfo(Request_AddMemberInfo request, int userId)
        //{
        //    var Check_UserId = await DbContext.users.FirstOrDefaultAsync(x => x.Id == userId);
        //    if (Check_UserId == null)
        //    {
        //        return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
        //    }
        //    string UrlAvt = null;
        //    var cloudinary = new CloudinaryService();
        //    if (request.UrlAvatar == null)
        //    {
        //        UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
        //    }
        //    else
        //    {
        //        if (!CheckInput.IsImage(request.UrlAvatar))
        //        {
        //            return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
        //        }

        //        UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
        //    }
        //    var memberInfo = new MemberInfo();
        //    memberInfo.Class = request.Class;
        //    memberInfo.Birthdate = request.Birthdate;
        //    memberInfo.PhoneNumber = request.PhoneNumber;
        //    memberInfo.Nation = request.Nation;
        //    memberInfo.DateOfJoining = request.DateOfJoining;
        //    memberInfo.FullName = request.FullName;
        //    memberInfo.religion = request.religion;
        //    memberInfo.UrlAvatar = UrlAvt;
        //    memberInfo.PlaceOfJoining = request.PlaceOfJoining;
        //    memberInfo.PoliticalTheory = request.PoliticalTheory;
        //    memberInfo.UserId = userId;

        //    DbContext.memberInfos.Add(memberInfo);
        //    await DbContext.SaveChangesAsync();
        //    return responseObject.ResponseObjectSuccess("Thêm thành công", converter_MemberInfo.EntityToDTO(memberInfo));
        //}


        public IEnumerable<DTO_MemberInfo> GetListMenberInfo(int pageSize, int pageNumber)
        {
            return DbContext.memberInfos.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_MemberInfo.EntityToDTO(x));
        }

        public async Task<ResponseObject<DTO_MemberInfo>> GetMemberInfo(int userId)
        {
            var memberInfo = await DbContext.memberInfos.FirstOrDefaultAsync(x => x.UserId == userId);
            if (memberInfo == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
            }
            return responseObject.ResponseObjectSuccess("Lấy thông tin thành công!", converter_MemberInfo.EntityToDTO(memberInfo));
        }

        public async Task<ResponseObject<DTO_MemberInfo>> UpdateMenberInfo(Request_UpdateMemberInfo request, int userId)
        {
            var memberInfo = await DbContext.memberInfos.FirstOrDefaultAsync(x => x.UserId == userId);

            if (memberInfo == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
            }

            // Chỉ cập nhật nếu request có dữ liệu, giữ lại giá trị cũ nếu request không có
            memberInfo.Class = string.IsNullOrEmpty(request.Class) ? memberInfo.Class : request.Class;
            memberInfo.Birthdate = request.Birthdate ?? memberInfo.Birthdate;
            memberInfo.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? memberInfo.PhoneNumber : request.PhoneNumber;
            memberInfo.Nation = string.IsNullOrEmpty(request.Nation) ? memberInfo.Nation : request.Nation;
            memberInfo.DateOfJoining = request.DateOfJoining ?? memberInfo.DateOfJoining;
            memberInfo.FullName = string.IsNullOrEmpty(request.FullName) ? memberInfo.FullName : request.FullName;
            memberInfo.religion = string.IsNullOrEmpty(request.religion) ? memberInfo.religion : request.religion;
            memberInfo.PlaceOfJoining = string.IsNullOrEmpty(request.PlaceOfJoining) ? memberInfo.PlaceOfJoining : request.PlaceOfJoining;
            memberInfo.PoliticalTheory = string.IsNullOrEmpty(request.PoliticalTheory) ? memberInfo.PoliticalTheory : request.PoliticalTheory;
            
            DbContext.memberInfos.Update(memberInfo);
            await DbContext.SaveChangesAsync(); 
            
            return responseObject.ResponseObjectSuccess("Cập nhật thành công", converter_MemberInfo.EntityToDTO(memberInfo));
        }

        public async Task<ResponseObject<DTO_MemberInfo>> UpdateUserImg(IFormFile? UrlAvatar, int userId)
        {
            var memberInfo = await DbContext.memberInfos.FirstOrDefaultAsync(x => x.UserId == userId);
            if (memberInfo == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
            }
            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(UrlAvatar);
            }
            memberInfo.UrlAvatar = UrlAvt;
            DbContext.memberInfos.Update(memberInfo);
            await DbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công", converter_MemberInfo.EntityToDTO(memberInfo));
        }
    }
}
