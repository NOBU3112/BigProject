using BigProject.DataContext;
using BigProject.Entities;
using BigProject.PayLoad.DTO;
using Microsoft.EntityFrameworkCore;

namespace BigProject.PayLoad.Converter
{
    public class Converter_EventJoin
    {
        private readonly AppDbContext _context;

        public Converter_EventJoin(AppDbContext context)
        {
            _context = context;
        }
        public DTO_EventJoin EntityToDTO(EventJoin eventJoint) 
        {
            return new DTO_EventJoin()
            {
                Id = eventJoint.Id,
                Class = _context.memberInfos.SingleOrDefault(x => x.UserId == eventJoint.UserId).Class,
                EventName = _context.events.SingleOrDefault(x => x.Id == eventJoint.EventId).EventName,
                FullName = _context.memberInfos.SingleOrDefault(x => x.UserId == eventJoint.UserId).FullName,
                MaSV = _context.users.SingleOrDefault(x => x.Id == eventJoint.UserId).MaSV,
                Status = eventJoint.Status,
            };
        }
    }
}
