using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetAllEventByPetIdQuery
{
    public class GetAllEventByPetIdQueryValidator:AbstractValidator<GetAllEventByPetIdQuery>
    {
        public GetAllEventByPetIdQueryValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            RuleFor(x => x.PetId).MustAsync(async (petId, cancellationToken) =>
            {
                var pet = await genericRepo.GetByIdAsync(petId);
                return pet != null;
            }).WithMessage("Pet with given Id does not exist.");
        }
    }
}
