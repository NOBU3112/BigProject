using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;


namespace BigProject.PayLoad.Converter
{
    public class Converter_MemberInfo
    {
        private readonly AppDbContext _context;

        public Converter_MemberInfo(AppDbContext context)
        {
            _context = context;
        }

        public DTO_MemberInfo EntityToDTO(MemberInfo memberInfo) {
            return new DTO_MemberInfo
            {
                Class = memberInfo.Class,
               Birthdate = memberInfo.Birthdate,
               DateOfJoining = memberInfo.DateOfJoining,
               FullName = memberInfo.FullName,
               Id = memberInfo.Id,
               IsOutstandingMember = memberInfo.IsOutstandingMember,
               MemberId = memberInfo.MemberId,
               Nation = memberInfo.Nation  ,
               PhoneNumber = memberInfo.PhoneNumber ,
               PlaceOfJoining = memberInfo.PlaceOfJoining,
               PoliticalTheory = memberInfo.PoliticalTheory ,
               religion = memberInfo.religion,
               Status = memberInfo.Status,
               UrlAvatar = memberInfo.UrlAvatar,
               UserId = memberInfo.UserId,   
            };
            }
    }
}
