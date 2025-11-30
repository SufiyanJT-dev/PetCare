using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllAttachment
{
    public class GetAllAttachmentQueryHandler : IRequestHandler<GetAllAttachmentQuery, IEnumerable<Domain.Entity.Attachment>>
    {
        private readonly IGenericRepo<Domain.Entity.Attachment> genericRepo;

        public GetAllAttachmentQueryHandler(IGenericRepo<Domain.Entity.Attachment> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public Task<IEnumerable<Domain.Entity.Attachment>> Handle(GetAllAttachmentQuery request, CancellationToken cancellationToken)
        {
            return genericRepo.GetAllAsync();
           
        }
    }
}
