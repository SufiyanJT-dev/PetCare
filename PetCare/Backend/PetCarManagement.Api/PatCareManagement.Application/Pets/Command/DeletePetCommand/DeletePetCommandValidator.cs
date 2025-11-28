using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Command.DeletePetCommand
{
    public class DeletePetCommandValidator:AbstractValidator<DeletePetCommand>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> genericRepo;

        public DeletePetCommandValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            this.genericRepo = genericRepo;
            RuleFor(x => x.PetId).MustAsync(async (PetId, CancellationToken) =>
            {
                var pet = await genericRepo.GetByIdAsync(PetId);
                return pet != null;
            }).WithMessage("Pet must be Present");
          
        }
    }
}
