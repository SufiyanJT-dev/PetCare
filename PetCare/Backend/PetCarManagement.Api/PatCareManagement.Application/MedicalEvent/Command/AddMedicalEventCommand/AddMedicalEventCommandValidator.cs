using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.AddMedicalEventCommand
{
    public class AddMedicalEventCommandValidator:AbstractValidator<AddMedicalEventCommand>
    {
        public AddMedicalEventCommandValidator()
        {
            RuleFor(x => x.PetId).NotEmpty().WithMessage("PetId should not be empty");
            RuleFor(x => x.Date).NotEmpty().WithMessage("EventType should not be empty");
            RuleFor(x => x.VetName).NotEmpty().WithMessage("VetName should not be empty");
            RuleFor(x => x.Notes).NotEmpty().WithMessage("Notes should not be empty");
            RuleFor(x => x.NextFollowupDate).GreaterThan(DateTime.Now).When(x => x.NextFollowupDate.HasValue).WithMessage("NextFollowupDate must be in the future if provided.");

        }
    }
}
