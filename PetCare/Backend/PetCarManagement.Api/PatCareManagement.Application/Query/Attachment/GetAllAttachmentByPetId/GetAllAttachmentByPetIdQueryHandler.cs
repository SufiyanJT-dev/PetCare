using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllAttachmentByPetId
{
    public class GetAllAttachmentByPetIdQueryHandler:IRequestHandler<GetAllAttachmentByPetIdQuery, IEnumerable<Domain.Entity.Attachment>>
    {
        private readonly IAttachmentRepo attachmentRepo;

        public  GetAllAttachmentByPetIdQueryHandler(IAttachmentRepo attachmentRepo)
        {
          
            AttachmentRepo = attachmentRepo;
        }

        public IAttachmentRepo AttachmentRepo { get; }

        public async Task<IEnumerable<Domain.Entity.Attachment>> Handle(GetAllAttachmentByPetIdQuery request, CancellationToken cancellationToken)
        {
           
            return await AttachmentRepo.GetAllAttachmentsByPetId(request.PetId);

        }
    }
}
