using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllByEventIdAttachment
{
    public class GetAllAttachmentByEventIdQueryHander:IRequestHandler<GetAllAttachmentByEventIdQuery, IEnumerable<Domain.Entity.Attachment>>
    {
        private readonly IGenericRepo<Domain.Entity.Attachment> genericRepo;

        public  GetAllAttachmentByEventIdQueryHander(IGenericRepo<Domain.Entity.Attachment> genericRepo)
        {
            this.genericRepo = genericRepo;
        }

        public async Task<IEnumerable<Domain.Entity.Attachment>> Handle(GetAllAttachmentByEventIdQuery request, CancellationToken cancellationToken)
        {
           

            Expression<Func<Domain.Entity.Attachment, bool>> predicate = wh => wh.MedicalEventId == request.EventId;


            return await genericRepo.FindAsync(predicate);
        }
    }
}
