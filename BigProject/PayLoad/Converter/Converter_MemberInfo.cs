using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;
using System.Net.WebSockets;


namespace BigProject.PayLoad.Converter
{
    public class Converter_MemberInfo
    {
        private readonly AppDbContext _context;

        public Converter_MemberInfo(AppDbContext context)
        {
            _context = context;
        }

        public DTO_MemberInfo EntityToDTO( MemberInfo memberInfo) {
            var user = _context.users.FirstOrDefault(x=>x.Id==memberInfo.UserId);
            return new DTO_MemberInfo
            {
                Class = memberInfo.Class,
               Birthdate = memberInfo.Birthdate,
               DateOfJoining = memberInfo.DateOfJoining,
               FullName = memberInfo.FullName,
               Id = memberInfo.Id,
               IsOutstandingMember = memberInfo.IsOutstandingMember,
               Nation = memberInfo.Nation  ,
               PhoneNumber = memberInfo.PhoneNumber ,
               PlaceOfJoining = memberInfo.PlaceOfJoining,
               PoliticalTheory = memberInfo.PoliticalTheory ,
               religion = memberInfo.religion,
               Status = memberInfo.Status,
               UrlAvatar = memberInfo.UrlAvatar,
               UserName = user.Username,
               Email = user.Email,
               IsActive = user.IsActive,
               MaSV = user.MaSV,
               RoleName = _context.roles.FirstOrDefault(x=>x.Id==user.RoleId).Name,
            };
            }
    }
}
