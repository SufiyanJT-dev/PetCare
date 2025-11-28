using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Application.WeightHistory.Dto;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetCareManagement.Application.WeightHistory.Command.CreateWeightHistory
{
    public class CreateWeightHistoryCommandHandler : IRequestHandler<CreateWeightHistoryCommand, WeightHistoryDto>
    {
        private readonly IRepository.IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> _repo;

        public CreateWeightHistoryCommandHandler(IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;

        }
        public async Task<WeightHistoryDto> Handle(CreateWeightHistoryCommand request, CancellationToken cancellationToken)
        {
            var weightHistory = new PetCareManagement.Domain.Entity.WeightHistory(
                 petId: request.PetId,
                 date: request.Date,
                 weightKg: request.WeightKg
             );
            var created = await _repo.AddAsync(weightHistory); // MUST AWAIT
            await _repo.SaveChangesAsync();                    // MUST AWAIT

            return new WeightHistoryDto
            {
                WhId = created.WhId,
                PetId = created.PetId,
                Date = created.Date,
                WeightKg = created.WeightKg
            };
        }
    }
}
