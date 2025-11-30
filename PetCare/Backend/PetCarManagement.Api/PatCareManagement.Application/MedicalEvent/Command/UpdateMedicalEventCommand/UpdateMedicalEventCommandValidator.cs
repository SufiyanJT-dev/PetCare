using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.UpdateMedicalEventCommand
{
    public class UpdateMedicalEventCommandValidator:AbstractValidator<UpdateMedicalEventCommand>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> genericRepo;

       public UpdateMedicalEventCommandValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            this.genericRepo = genericRepo;
            RuleFor(x => x.PetId).MustAsync(async (petId, cancellationToken) =>
            {
                var pet = await genericRepo.GetByIdAsync(petId);
                return pet != null;
            }).WithMessage("Pet with given Id does not exist.");
            RuleFor(x => x.EventDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Event date cannot be in the future.");
            RuleFor(x => x.Veterinarian).NotEmpty().WithMessage("Veterinarian name is required.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid medical event type.");
            RuleFor(x => x.NextFollowupDate)
                .GreaterThan(x => x.EventDate)
                .When(x => x.NextFollowupDate.HasValue)
                .WithMessage("Next follow-up date must be after the event date.");
            
        }
    }
}
