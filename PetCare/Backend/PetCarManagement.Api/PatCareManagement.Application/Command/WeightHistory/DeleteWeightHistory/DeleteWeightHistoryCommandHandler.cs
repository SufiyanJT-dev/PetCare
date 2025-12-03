using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.WeightHistory.DeleteWeightHistory
{
    public class DeleteWeightHistoryCommandHandler : IRequestHandler<DeleteWeightHistoryCommand, bool>
    {
        private readonly IGenericRepo<Domain.Entity.WeightHistory> _repo;

        public DeleteWeightHistoryCommandHandler(IGenericRepo<Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteWeightHistoryCommand request, CancellationToken cancellationToken)
        {
             Domain.Entity.WeightHistory? WeightHistory = _repo.GetByIdAsync(request.WhId).Result;
            if (WeightHistory == null)
            {
                return false;
            }
           await _repo.DeleteAsync(WeightHistory.WhId);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
