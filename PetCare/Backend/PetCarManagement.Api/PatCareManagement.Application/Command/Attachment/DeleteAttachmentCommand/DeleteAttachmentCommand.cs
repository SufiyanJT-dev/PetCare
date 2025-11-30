using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Attachment.DeleteAttachmentCommand
{
    public class DeleteAttachmentCommand: IRequest<bool>
    {
        public int AttachmentId { get; set; }
    }
}
