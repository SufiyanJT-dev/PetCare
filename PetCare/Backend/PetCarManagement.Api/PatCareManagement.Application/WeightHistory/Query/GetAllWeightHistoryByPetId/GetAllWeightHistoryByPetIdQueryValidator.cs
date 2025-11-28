using FluentValidation;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Query.GetAllWeightHistoryByPetId
{
    public class GetAllWeightHistoryByPetIdQueryValidator
        : AbstractValidator<GetAllWeightHistoryByPetIdQuery>
    {
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.Pets> _petRepo;

        public GetAllWeightHistoryByPetIdQueryValidator(IGenericRepo<PetCareManagement.Domain.Entity.Pets> petRepo)
        {
            _petRepo = petRepo;

            RuleFor(x => x.PetId)
                .GreaterThan(0).WithMessage("PetId must be greater than 0.")
                .MustAsync(PetExists).WithMessage("Pet with given Id does not exist.");
        }

        private async Task<bool> PetExists(int petId, CancellationToken cancellationToken)
        {
            var pet = await _petRepo.GetByIdAsync(petId);
            return pet != null;
        }
    }
}