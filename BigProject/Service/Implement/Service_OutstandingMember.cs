namespace BigProject.Service.Implement;
using BigProject.DataContext;
using BigProject.Entities;
using BigProject.Enums;
using BigProject.Payload.Response;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Service.Interface;
using Microsoft.EntityFrameworkCore;

public class Service_OutstandingMember : IService_OutstandingMember
    {
    private readonly AppDbContext _appDbContext;
    private readonly ResponseObject<DTO_OutstandingMember> responseObject;
    private readonly Converter_OutstandingMember converter_OutstandingMember;
    private readonly ResponseObject<DTO_OutstandingMemberApproval> responseObject1;
    private readonly Converter_OutstandingMemberApproval converter_OutstandingMember1;
    private readonly ResponseBase responseBase;

    public Service_OutstandingMember(AppDbContext appDbContext, ResponseObject<DTO_OutstandingMember> responseObject, Converter_OutstandingMember converter_OutstandingMember, ResponseObject<DTO_OutstandingMemberApproval> responseObject1, Converter_OutstandingMemberApproval converter_OutstandingMember1, ResponseBase responseBase)
    {
        _appDbContext = appDbContext;
        this.responseObject = responseObject;
        this.converter_OutstandingMember = converter_OutstandingMember;
        this.responseObject1 = responseObject1;
        this.converter_OutstandingMember1 = converter_OutstandingMember1;
        this.responseBase = responseBase;
    }

    public async Task<ResponseObject<DTO_OutstandingMemberApproval>> AcceptOutstandingMember(Request_acceptOutstandingMember request,int UserId )
    {

        var Check_MenberInfoId = await _appDbContext.requestToBeOutStandingMembers.FirstOrDefaultAsync(x => x.MemberInfoId == request.MemberInfoId);
        if (Check_MenberInfoId == null)
        {
            return responseObject1.ResponseObjectError(StatusCodes.Status404NotFound, "Không có đoàn viên ", null);

        }
        if (Check_MenberInfoId.Status == RequestEnum.accept.ToString())
        {
            return responseObject1.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên đã được duyệt rồi ", null);
        }
        Check_MenberInfoId.MemberInfoId = request.MemberInfoId;
        Check_MenberInfoId.Status = RequestEnum.accept.ToString();
        _appDbContext.requestToBeOutStandingMembers.Update(Check_MenberInfoId);
        await _appDbContext.SaveChangesAsync();

 
        
        var IsAccept = new ApprovalHistory();
        IsAccept.IsAccept = true;
        IsAccept.ApprovedDate = DateTime.Now;
        IsAccept.ApprovedById = UserId;
        IsAccept.RequestToBeOutstandingMemberId = Check_MenberInfoId.Id;
        IsAccept.HistoryType = HistoryEnum.outstandingMember.ToString();
        _appDbContext.approvalHistories.Add(IsAccept);
        await _appDbContext.SaveChangesAsync();

        return responseObject1.ResponseObjectSuccess("Chấp nhận đoàn viên ưu tú thành công", converter_OutstandingMember1.EntityToDTO(Check_MenberInfoId));

    }

    public async Task<ResponseObject<DTO_OutstandingMember>> AddOutstandingMenber(Request_AddOutstandingMember request)
    {
        var Check_Id = await _appDbContext.memberInfos.FirstOrDefaultAsync(x=>x.Id == request.MemberInfoId);
        if (Check_Id == null)
        {
            return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên không tồn tại", null);
        }
        var Check_MenberInfoId = await _appDbContext.requestToBeOutStandingMembers.FirstOrDefaultAsync(x=>x.MemberInfoId == request.MemberInfoId);
        if (Check_MenberInfoId != null)
        {
            return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên đã được đề xuất", null);

        }

        var outstandingMenber = new RequestToBeOutStandingMember();
        outstandingMenber.MemberInfoId = request.MemberInfoId;
        _appDbContext.requestToBeOutStandingMembers.Add(outstandingMenber);
        await _appDbContext.SaveChangesAsync();
        return responseObject.ResponseObjectSuccess("Thêm đoàn viên ưu tú thành công",converter_OutstandingMember.EntityToDTO(outstandingMenber));


    }

    public async Task<ResponseObject<DTO_OutstandingMemberApproval>> RejectOutstandingMember(Request_rejectOutstandingMember request, int UserId)
    {
       var Check_MenberInfoId = await _appDbContext.requestToBeOutStandingMembers.FirstOrDefaultAsync(x=>x.MemberInfoId == request.MemberInfoId);
        if (Check_MenberInfoId == null)
        {
            return responseObject1.ResponseObjectError(StatusCodes.Status404NotFound, "Không có đoàn viên", null);
        }
        if (Check_MenberInfoId.Status == RequestEnum.reject.ToString())
        {
            return responseObject1.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên đã từ chối rồi ", null);
        }

        Check_MenberInfoId.MemberInfoId = request.MemberInfoId;
        Check_MenberInfoId.RejectReason = request.RejectReason;
        Check_MenberInfoId.Status = RequestEnum.reject.ToString();
        _appDbContext.requestToBeOutStandingMembers.Update(Check_MenberInfoId);
        await _appDbContext.SaveChangesAsync();

       
        var IsAccept = new ApprovalHistory();
        IsAccept.IsAccept = false;
        IsAccept.ApprovedDate = DateTime.Now;
        IsAccept.ApprovedById = UserId;
        IsAccept.RequestToBeOutstandingMemberId = Check_MenberInfoId.Id;
        IsAccept.HistoryType = HistoryEnum.outstandingMember.ToString();
        //IsAccept.RejectReason = request.RejectReason;
        _appDbContext.approvalHistories.Add(IsAccept);
        await _appDbContext.SaveChangesAsync();
        return responseObject1.ResponseObjectSuccess("Đã từ chối đoàn viên ưu tú", converter_OutstandingMember1.EntityToDTO(Check_MenberInfoId));
    }

    //public async Task<ResponseObject<DTO_OutstandingMember>> WaitingOutstandingMenber(Request_waitingOutstandingMember request)
    //{
    //    var Check_MenberInfoId = await _appDbContext.requestToBeOutStandingMembers.FirstOrDefaultAsync(x=>x.MemberInfoId == request.MemberInfoId);
    //    if(Check_MenberInfoId == null)
    //    {
    //        return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không có đoàn viên ", null);

    //    }
    //    if(Check_MenberInfoId.Status == RequestEnum.waiting.ToString() )
    //    {
    //        return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Đoàn viên đã được duyệt rồi ", null);
    //    }    

    //    Check_MenberInfoId.MemberInfoId = request.MemberInfoId;
    //    Check_MenberInfoId.Status = RequestEnum.waiting.ToString();
    //    _appDbContext.requestToBeOutStandingMembers.Update(Check_MenberInfoId);
    //    await _appDbContext.SaveChangesAsync();
    //    return responseObject.ResponseObjectSuccess("Chấp nhận đoàn viên ưu tú thành công", converter_OutstandingMember.EntityToDTO(Check_MenberInfoId));

    //}
}

