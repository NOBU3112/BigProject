using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_OutstandingMember
    {
        private readonly AppDbContext _context;

        public Converter_OutstandingMember(AppDbContext context)
        {
            _context = context;
        }
        public DTO_OutstandingMember EntityToDTO(RequestToBeOutStandingMember requestToBeOutStandingMember)
        {
            return new DTO_OutstandingMember
            {
                Id = requestToBeOutStandingMember.Id,
                CreatedDate = requestToBeOutStandingMember.CreatedDate,
                MemberInfoMaSV = _context.users.FirstOrDefault(x=>x.Id == requestToBeOutStandingMember.MemberInfoId).MaSV,
                MemberInfoName = _context.memberInfos.FirstOrDefault(x=>x.UserId == requestToBeOutStandingMember.MemberInfoId).FullName,
                Status = requestToBeOutStandingMember.Status,
                RejectReason = requestToBeOutStandingMember.RejectReason,
            };
        }
            
    }
}
