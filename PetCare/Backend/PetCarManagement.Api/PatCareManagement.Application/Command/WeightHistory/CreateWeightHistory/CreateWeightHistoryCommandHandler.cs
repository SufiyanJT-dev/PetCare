using MediatR;
using PetCareManagement.Application.Command.WeightHistory.Dto;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetCareManagement.Application.Command.WeightHistory.CreateWeightHistory
{
    public class CreateWeightHistoryCommandHandler : IRequestHandler<CreateWeightHistoryCommand, WeightHistoryDto>
    {
        private readonly IGenericRepo<Domain.Entity.WeightHistory> _repo;

        public CreateWeightHistoryCommandHandler(IGenericRepo<Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;

        }
        public async Task<WeightHistoryDto> Handle(CreateWeightHistoryCommand request, CancellationToken cancellationToken)
        {
            Domain.Entity.WeightHistory weightHistory = new Domain.Entity.WeightHistory(
                 petId: request.PetId,
                 date: request.Date,
                 weightKg: request.WeightKg
             );
<<<<<<< Updated upstream
            var created = await _repo.AddAsync(weightHistory); 
            await _repo.SaveChangesAsync();                 
=======
            var created = await _repo.AddAsync(weightHistory);
            await _repo.SaveChangesAsync();                  
>>>>>>> Stashed changes

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
