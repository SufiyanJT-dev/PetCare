using FluentValidation;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.WeightHistory.CreateWeightHistory
{
    public class CreateWeightHistoryCommandValidator : AbstractValidator<CreateWeightHistoryCommand>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> _petRepo;

        public CreateWeightHistoryCommandValidator(IGenericRepo<Domain.Entity.Pets> petRepo)
        {
            _petRepo = petRepo;

            RuleFor(x => x.PetId)
                .GreaterThan(0).WithMessage("PetId must be greater than 0.")
                .MustAsync(PetExists).WithMessage("Pet with given Id does not exist.");

            RuleFor(x => x.WeightKg)
                .GreaterThan(0).WithMessage("Weight must be greater than 0 kg.");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Date cannot be in the future.");
        }

        private async Task<bool> PetExists(int petId, CancellationToken cancellationToken)
        {
            var pet = await _petRepo.GetByIdAsync(petId);
            return pet != null;
        }
    }
}