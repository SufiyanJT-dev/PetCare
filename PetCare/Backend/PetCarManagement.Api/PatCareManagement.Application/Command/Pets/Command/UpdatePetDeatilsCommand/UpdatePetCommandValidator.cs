using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Pets.Command.UpdatePetDeatilsCommand
{
    public class UpdatePetCommandValidator: AbstractValidator<UpdatePetCommand>
    {
        public UpdatePetCommandValidator() { 
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");
            RuleFor(x => x.Breed).NotEmpty().WithMessage("Breed must not be empty.");
            RuleFor(x => x.PetId).NotEmpty().WithMessage("Pet Id should not be empty");
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("Owner Id should not be empty");
            RuleFor(x => x.Species).IsInEnum().WithMessage("Species must be a valid enum value.");  
            
        }
    }
}
