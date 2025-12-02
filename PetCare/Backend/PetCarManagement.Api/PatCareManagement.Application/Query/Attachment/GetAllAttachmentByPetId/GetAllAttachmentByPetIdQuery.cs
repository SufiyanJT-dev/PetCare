using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllAttachmentByPetId
{
    public class GetAllAttachmentByPetIdQuery:IRequest<IEnumerable<Domain.Entity.Attachment>>
    {
        public int PetId { get; set; }
    }
    
}
