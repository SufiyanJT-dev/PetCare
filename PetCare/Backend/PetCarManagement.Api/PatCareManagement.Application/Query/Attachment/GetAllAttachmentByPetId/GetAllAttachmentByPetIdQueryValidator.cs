using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Attachment.GetAllAttachmentByPetId
{
    public class GetAllAttachmentByPetIdQueryValidator:AbstractValidator<GetAllAttachmentByPetIdQuery>
    {
        public GetAllAttachmentByPetIdQueryValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            RuleFor(x => x.PetId).MustAsync(async (PetId, cancellationToken) =>
            {
                Domain.Entity.Pets? pet = await genericRepo.GetByIdAsync(PetId);
                return pet != null;
            }).WithMessage("Event with given Id does not exist.");
        }
    }
}
