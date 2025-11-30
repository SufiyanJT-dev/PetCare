using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllAttachment
{
    public class GetAllAttachmentQuery:IRequest<IEnumerable<Domain.Entity.Attachment>>
    {
    }
}
