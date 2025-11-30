using MediatR;
using PetCareManagement.Application.Command.WeightHistory.Dto;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.WeightHistory.UpdateWeightHistory
{
    public class UpdateWeightHistoryCommandHandler : IRequestHandler<UpdateWeightHistoryCommand, WeightHistoryDto>
    {
        private readonly IGenericRepo<Domain.Entity.WeightHistory> _repo;

        public UpdateWeightHistoryCommandHandler(IGenericRepo<Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;
        }
        public async Task<WeightHistoryDto> Handle(UpdateWeightHistoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.WhId);
            if (entity == null)
            {
                throw new Exception("Weight history record not found.");
            }
            entity.UpdateWeightHistory(
               date: request.Date,
               weightKg: request.WeightKg
           );
           await _repo.UpdateAsync(entity);
           await _repo.SaveChangesAsync();
            return new WeightHistoryDto
              {
                WhId = entity.WhId,
                PetId = entity.PetId,
                Date = entity.Date,
                WeightKg = entity.WeightKg
              };
        }
    }
}
