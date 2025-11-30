using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetAllEventByPetIdQuery
{
    public class GetAllEventByPetIdQuery:IRequest<IEnumerable<Domain.Entity.MedicalEvent>>
    {
        public int PetId { get; set; }
    }
   
}
