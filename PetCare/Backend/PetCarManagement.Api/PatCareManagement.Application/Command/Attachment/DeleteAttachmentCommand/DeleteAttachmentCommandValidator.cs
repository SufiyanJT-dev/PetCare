using FluentValidation;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Attachments = PetCareManagement.Domain.Entity.Attachment;
namespace PetCareManagement.Application.Command.Attachment.DeleteAttachmentCommand
{
    public class DeleteAttachmentCommandValidator:AbstractValidator<DeleteAttachmentCommand>
    {
        private readonly IGenericRepo<Attachments> genericRepo;

        public DeleteAttachmentCommandValidator(IGenericRepo<Attachments> genericRepo)
        {
            this.genericRepo = genericRepo;
            RuleFor(x => x.AttachmentId).MustAsync(async (attachmentId, cancellation) =>
            {
                Attachments attachment = await genericRepo.GetByIdAsync(attachmentId);
                return attachment != null;

            }).WithMessage("Attachment does not exixt");
      
        }
    
    }
}
