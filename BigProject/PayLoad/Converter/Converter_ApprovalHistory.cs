using BigProject.DataContext;
using BigProject.Entities;
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
            return new  DTO_ApprovalHistory
            {
                ApprovedById = approvalHistory.ApprovedById,
                Id = approvalHistory.Id,
                IsAccept = approvalHistory.IsAccept,
                //RejectReason = approvalHistory.RejectReason,
                RequestToBeOutstandingMemberId = approvalHistory.RequestToBeOutstandingMemberId,
                RewardDisciplineId = approvalHistory.RewardDisciplineId,
                HistoryType = approvalHistory.HistoryType,
            };
        }
    }
}
