using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllByEventIdAttachment
{
    public class GetAllAttachmentByEventIdQuery:IRequest<IEnumerable<Domain.Entity.Attachment>>
    {
        public int EventId { get; set; }
    }
    
}
