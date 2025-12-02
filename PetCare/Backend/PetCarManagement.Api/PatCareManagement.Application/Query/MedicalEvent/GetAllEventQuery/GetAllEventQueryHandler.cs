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

namespace PetCareManagement.Application.Query.Pets.GetAllEventQuery
{
    public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, IEnumerable<Domain.Entity.MedicalEvent>>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> genericRepo;

        public GetAllEventQueryHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public async Task<IEnumerable<Domain.Entity.MedicalEvent>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {

           return await genericRepo.GetAllAsync();
        }
    }
}
