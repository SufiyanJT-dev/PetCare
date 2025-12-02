using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pet=PetCareManagement.Domain.Entity.Pets;
namespace PetCareManagement.Application.Query.Reminder.GetAllpreviousReminderByPetId
{
    public  class GetAllpreviousReminderByPetIdQueryValidators:AbstractValidator<GetAllpreviousReminderByPetIdQuery>
    {
        public GetAllpreviousReminderByPetIdQueryValidators(IGenericRepo<Pet> genericRepo)
        {
            RuleFor(x => x.PetId)
                .NotEmpty().WithMessage("PetId is required.")
                .MustAsync(async (petId, cancellation) =>
                {
                    var pet = await genericRepo.GetByIdAsync(petId);
                    return pet != null;
                }).WithMessage("Pet with the given PetId does not exist.");
        }
    }
}
