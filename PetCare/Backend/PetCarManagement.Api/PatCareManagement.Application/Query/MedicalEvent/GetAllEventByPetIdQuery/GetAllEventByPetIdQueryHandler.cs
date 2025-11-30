using MediatR;
using PetCareManagement.Application.IRepository;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetAllEventByPetIdQuery
{
    public class GetAllEventByPetIdQueryHandler : IRequestHandler<GetAllEventByPetIdQuery, IEnumerable<Domain.Entity.MedicalEvent>>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> genericRepo;

        public GetAllEventByPetIdQueryHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public async Task<IEnumerable<Domain.Entity.MedicalEvent>> Handle(GetAllEventByPetIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entity.MedicalEvent, bool>> predicate = wh => wh.PetId == request.PetId;

            
            return await genericRepo.FindAsync(predicate);

        }
    }
}
