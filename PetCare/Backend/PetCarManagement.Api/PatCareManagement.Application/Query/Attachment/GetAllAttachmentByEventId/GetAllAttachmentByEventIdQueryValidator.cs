using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllByEventIdAttachment
{
    public class GetAllAttachmentByEventIdQueryValidator:AbstractValidator<GetAllAttachmentByEventIdQuery>
    {
        public GetAllAttachmentByEventIdQueryValidator(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            RuleFor(x => x.EventId).MustAsync(async (eventId, cancellationToken) =>
            {
                var attachment = await genericRepo.GetByIdAsync(eventId);
                return attachment != null;
            }).WithMessage("Event with given Id does not exist.");
        }
    }
}
