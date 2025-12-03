using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Pets.Command.DeletePetCommand
{
    public class DeletePetCommandValidator:AbstractValidator<DeletePetCommand>
    {

        public DeletePetCommandValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            RuleFor(x => x.PetId).MustAsync(async (PetId, CancellationToken) =>
            {
                var pet = await genericRepo.GetByIdAsync(PetId);
                return pet != null;
            }).WithMessage("Pet must be Present");
          
        }
    }
}
