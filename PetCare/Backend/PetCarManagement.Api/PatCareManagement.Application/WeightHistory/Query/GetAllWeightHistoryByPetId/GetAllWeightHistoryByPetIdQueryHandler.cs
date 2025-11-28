using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Application.WeightHistory.Dto;
using PetCareManagement.Application.WeightHistory.Query.GetAllWeightHistoryByPetId;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Query.GetAllWeightHistoryByPetId
{
    public class GetAllWeightHistoryByPetIdQueryHandler : IRequestHandler<GetAllWeightHistoryByPetIdQuery, IEnumerable<WeightHistoryDto>>
    {
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> _repo;

        public GetAllWeightHistoryByPetIdQueryHandler(IGenericRepo<PetCareManagement.Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WeightHistoryDto>> Handle(
            GetAllWeightHistoryByPetIdQuery request,
            CancellationToken cancellationToken)
        {
            // Expression to filter by PetId
            Expression<Func<PetCareManagement.Domain.Entity.WeightHistory, bool>> predicate = wh => wh.PetId == request.PetId;

            // Get all matching records
            var weightHistories = await _repo.FindAsync(predicate);

            // Sort by Date descending (latest first) and map to DTO
            var result = weightHistories
                .OrderByDescending(wh => wh.Date)
                .Select(wh => new WeightHistoryDto
                {
                    WhId = wh.WhId,
                    PetId = wh.PetId,
                    Date = wh.Date,
                    WeightKg = wh.WeightKg
                });

            return result;
        }
    }
}