using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Enums;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_ApprovalHistory
    {
        private readonly AppDbContext _context;

        public Converter_ApprovalHistory(AppDbContext context)
        {
            _context = context;
        }
        public DTO_ApprovalHistory EntityToDTO(ApprovalHistory approvalHistory)
        {
            if (approvalHistory.HistoryType == HistoryEnum.outstandingMember.ToString())
            {
                var member = _context.requestToBeOutStandingMembers.FirstOrDefault(x => x.Id == approvalHistory.RequestToBeOutstandingMemberId);
                return new DTO_ApprovalHistory
                {
                    ApprovedById = approvalHistory.ApprovedById,
                    Id = approvalHistory.Id,
                    IsAccept = approvalHistory.IsAccept,
                    RequestToBeOutstandingMemberId = approvalHistory.RequestToBeOutstandingMemberId,
                    RewardDisciplineId = approvalHistory.RewardDisciplineId,
                    HistoryType = approvalHistory.HistoryType,
                    ApprovedDate = approvalHistory.ApprovedDate,
                    ApprovedByMaSV = _context.users.FirstOrDefault(x=>x.Id == approvalHistory.ApprovedById).MaSV,
                    ApprovedByName = _context.memberInfos.FirstOrDefault(x=>x.UserId == approvalHistory.ApprovedById).FullName,
                    memberMaSV = _context.users.FirstOrDefault(x => x.Id == member.MemberInfoId).MaSV,
                    MemberName = _context.memberInfos.FirstOrDefault(x => x.UserId == member.MemberInfoId).FullName,
                    rejectReason = member.RejectReason,
                };
            }
            var member1 = _context.rewardDisciplines.FirstOrDefault(x => x.Id == approvalHistory.RewardDisciplineId);
            return new DTO_ApprovalHistory
            {
                ApprovedById = approvalHistory.ApprovedById,
                Id = approvalHistory.Id,
                IsAccept = approvalHistory.IsAccept,
                RequestToBeOutstandingMemberId = approvalHistory.RequestToBeOutstandingMemberId,
                RewardDisciplineId = approvalHistory.RewardDisciplineId,
                HistoryType = approvalHistory.HistoryType,
                ApprovedDate = approvalHistory.ApprovedDate,
                ApprovedByMaSV = _context.users.FirstOrDefault(x => x.Id == approvalHistory.ApprovedById).MaSV,
                ApprovedByName = _context.memberInfos.FirstOrDefault(x => x.UserId == approvalHistory.ApprovedById).FullName,
                memberMaSV = _context.users.FirstOrDefault(x => x.Id == member1.RecipientId).MaSV,
                MemberName = _context.memberInfos.FirstOrDefault(x => x.UserId == member1.RecipientId).FullName,
                description = member1.Description,
                rejectReason = member1.RejectReason,
            };
        }
    }
}
