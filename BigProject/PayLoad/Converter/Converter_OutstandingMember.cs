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
                MemberInfoId = requestToBeOutStandingMember.MemberInfoId,
                Status = requestToBeOutStandingMember.Status,
                RejectReason = requestToBeOutStandingMember.RejectReason,
            };
        }
            
    }
}
