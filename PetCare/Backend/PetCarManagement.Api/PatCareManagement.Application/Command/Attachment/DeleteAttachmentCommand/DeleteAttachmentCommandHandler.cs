using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Attachment.DeleteAttachmentCommand
{
    public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, bool>
    {
        private readonly IGenericRepo<Domain.Entity.Attachment> genericRepo;

        public DeleteAttachmentCommandHandler(IGenericRepo<Domain.Entity.Attachment> genericRepo) {
            this.genericRepo = genericRepo;
        }
        public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {

            return await genericRepo.DeleteAsync(request.AttachmentId);
        }
    }
}
