using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Command.DeleteWeightHistory
{
    public class DeleteWeightHistoryCommandValidator: AbstractValidator<DeleteWeightHistoryCommand>
    {
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> _repo;

        public DeleteWeightHistoryCommandValidator(IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;

            RuleFor(x => x.WhId)
                .GreaterThan(0).WithMessage("WhId must be greater than 0.")
                .MustAsync(RecordExists).WithMessage("Weight history record does not exist.");
        }

        private async Task<bool> RecordExists(int whId, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(whId);
            return entity != null;
        }
    }
}