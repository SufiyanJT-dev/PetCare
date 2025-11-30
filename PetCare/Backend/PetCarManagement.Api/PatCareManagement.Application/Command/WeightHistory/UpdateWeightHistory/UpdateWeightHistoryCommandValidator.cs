using FluentValidation;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.WeightHistory.UpdateWeightHistory
{
    public class UpdateWeightHistoryCommandValidator
        : AbstractValidator<UpdateWeightHistoryCommand>
    {
        public UpdateWeightHistoryCommandValidator(
            IGenericRepo<Domain.Entity.WeightHistory> repo,
            IGenericRepo<Domain.Entity.Pets> petRepo)
        {
            RuleFor(x => x.WhId)
                .GreaterThan(0).WithMessage("WhId must be greater than 0.")
                .MustAsync(async (whId, ct) => await repo.GetByIdAsync(whId) != null)
                .WithMessage("Weight history record does not exist.");
            RuleFor(x => x.WeightKg)
                .GreaterThan(0).WithMessage("Weight must be greater than 0 kg.");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Date cannot be in the future.");
        }
    }
}