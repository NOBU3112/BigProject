using BigProject.DataContext;
using BigProject.PayLoad.Converter;
using BigProject.PayLoad.DTO;
using BigProject.Payload.Response;
using BigProject.Service.Interface;
using BigProject.PayLoad.Request;
using BigProject.Entities;
using Microsoft.EntityFrameworkCore;
using BigProject.Helper;
using Microsoft.Extensions.Logging;

namespace BigProject.Service.Implement
{
    public class Service_Document : IService_Document
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_Document> responseObject;
        private readonly Converter_Document converter_Document;
        private readonly ResponseBase responseBase;

        public Service_Document(AppDbContext dbContext, ResponseObject<DTO_Document> responseObject, Converter_Document converter_Document, ResponseBase responseBase)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter_Document = converter_Document;
            this.responseBase = responseBase;
        }

        public async Task<ResponseObject<DTO_Document>> AddDocument(Request_AddDocument request,int userId)
        {
            var documentTitle_check = await dbContext.documents.FirstOrDefaultAsync(x => x.DocumentTitle == request.DocumentTitle);
            if (documentTitle_check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, " Tiêu đề văn bản không được trùng! ", null);
            }
            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (request.UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(request.UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
            }
            var document = new Document();
            document.DocumentTitle = request.DocumentTitle;
            document.DocumentContent = request.DocumentContent;
            document.UserId = userId;
            document.UrlAvatar = UrlAvt;
            dbContext.documents.Add(document);   
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Thêm thành công!", converter_Document.EntityToDTO(document));
        }

        public async Task<ResponseBase> DeleteDocument(int Id)
        {
            var document = await dbContext.documents.FirstOrDefaultAsync(x => x.Id == Id);
            if (document == null)
            {
                return responseBase.ResponseBaseError(StatusCodes.Status404NotFound, "Văn bản không tồn tại!");
            }
            dbContext.documents.Remove(document);
            await dbContext.SaveChangesAsync();
            return responseBase.ResponseBaseSuccess("Xóa thành công!");
        }

        public PagedResult<DTO_Document> GetListDocument(int pageSize, int pageNumber)
        {
            var query = dbContext.documents;

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_Document.EntityToDTO(x))
                .ToList(); // Chuyển thành List<T>

            return new PagedResult<DTO_Document>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }


        public async Task<ResponseObject<DTO_Document>> UpdateDocument(Request_UpdateDocument request)
        {
            var document = await dbContext.documents.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (document == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Văn bản không tồn tại!", null);
            }
            var documentTitle_Check = await dbContext.documents.FirstOrDefaultAsync(x => x.DocumentTitle == request.DocumentTitle);
            if (documentTitle_Check != null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Tiêu đề văn bản không được trùng! ", null);
            }
            string UrlAvt = null;
            var cloudinary = new CloudinaryService();
            if (request.UrlAvatar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {
                if (!CheckInput.IsImage(request.UrlAvatar))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Định dạng ảnh không hợp lệ !", null);
                }

                UrlAvt = await cloudinary.UploadImage(request.UrlAvatar);
            }
            document.DocumentTitle = request.DocumentTitle;
            document.DocumentContent = request.DocumentContent;
            document.UrlAvatar = UrlAvt;
            dbContext.documents.Update(document);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseObjectSuccess("Sửa thành công!", converter_Document.EntityToDTO(document));
        }
    }
}
