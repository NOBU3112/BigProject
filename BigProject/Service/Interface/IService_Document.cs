﻿using BigProject.PayLoad.DTO;
using BigProject.PayLoad.Request;
using BigProject.Payload.Response;

namespace BigProject.Service.Interface
{
    public interface IService_Document
    {
        Task<ResponseObject<DTO_Document>> AddDocument(Request_AddDocument request,int UserId);
        Task<ResponseObject<DTO_Document>> UpdateDocument(Request_UpdateDocument request);
        Task<ResponseBase> DeleteDocument(int Id);
        IEnumerable<DTO_Document> GetListDocument(int pageSize, int pageNumber);
    }
}
