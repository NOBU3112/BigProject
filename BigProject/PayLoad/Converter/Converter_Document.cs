using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;

namespace BigProject.PayLoad.Converter
{
    public class Converter_Document
    {
        private readonly AppDbContext _context;
        public Converter_Document(AppDbContext context)
        {
            _context = context;
        }
        public DTO_Document EntityToDTO(Document document)
        {
            return new DTO_Document
            {
                Id = document.Id,
                DocumentContent = document.DocumentContent,
                DocumentTitle = document.DocumentTitle,
                CreateAt = document.CreateAt,
                UrlAvatar = document.UrlAvatar,
                UserId = document.UserId,
                UserName = _context.users.SingleOrDefault(x => x.Id == document.Id).Username,
            };
        }
    }
}
