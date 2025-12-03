using MediatR;
using PetCareManagement.Application.Command.WeightHistory.Dto;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.WeightHistory.GetAllWeightHistoryByPetId
{
    public class GetAllWeightHistoryByPetIdQueryHandler : IRequestHandler<GetAllWeightHistoryByPetIdQuery, IEnumerable<WeightHistoryDto>>
    {
        private readonly IGenericRepo<Domain.Entity.WeightHistory> _repo;

        public GetAllWeightHistoryByPetIdQueryHandler(IGenericRepo<Domain.Entity.WeightHistory> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WeightHistoryDto>> Handle(
            GetAllWeightHistoryByPetIdQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entity.WeightHistory, bool>> predicate = wh => wh.PetId == request.PetId;

            var weightHistories = await _repo.FindAsync(predicate);
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