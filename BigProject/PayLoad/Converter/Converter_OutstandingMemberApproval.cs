using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;
using Microsoft.EntityFrameworkCore;

namespace BigProject.PayLoad.Converter
{
    public class Converter_OutstandingMemberApproval
    {
        private readonly AppDbContext appDbContext;

        public Converter_OutstandingMemberApproval(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public DTO_OutstandingMemberApproval EntityToDTO(RequestToBeOutStandingMember requestToBeOutStandingMember)
        {
            var approvalHistory = appDbContext.approvalHistories.FirstOrDefault(x => x.RequestToBeOutstandingMemberId == requestToBeOutStandingMember.Id);
            if (approvalHistory == null)
            {
                return null;
            }
            return new DTO_OutstandingMemberApproval
            {
                Id = requestToBeOutStandingMember.Id,
                CreatedDate = requestToBeOutStandingMember.CreatedDate,
                MemberInfoMaSV = appDbContext.users.FirstOrDefault(x => x.Id == requestToBeOutStandingMember.MemberInfoId).MaSV,
                MemberInfoName = appDbContext.memberInfos.FirstOrDefault(x => x.UserId == requestToBeOutStandingMember.MemberInfoId).FullName,
                Status = requestToBeOutStandingMember.Status,
                RejectReason = requestToBeOutStandingMember.RejectReason,
                HistoryType = approvalHistory.HistoryType,
                IsAccept = approvalHistory.IsAccept,
                ApprovedDate = approvalHistory.ApprovedDate,
                ApprovedByMaSV = appDbContext.users.FirstOrDefault(x=>x.Id == approvalHistory.ApprovedById).MaSV,
                ApprovedByName = appDbContext.memberInfos.FirstOrDefault(x=>x.UserId == approvalHistory.ApprovedById).FullName,
            };
        }
    }
}
