using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Command.AddPetComand
{
    public class AddPetComandValidators:AbstractValidator<AddPetComand>
    {
       

        public AddPetComandValidators( )
        {
            
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("Id Should Not be empty");
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");
         
            RuleFor(x => x.Breed).NotEmpty().WithMessage("Breed must not be empty.");

        }
    }
}
