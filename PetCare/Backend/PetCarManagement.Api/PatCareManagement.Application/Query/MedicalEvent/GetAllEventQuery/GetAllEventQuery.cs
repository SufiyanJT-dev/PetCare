using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetAllEventQuery
{
    public class GetAllEventQuery:IRequest<IEnumerable<Domain.Entity.MedicalEvent>>
    {

    }
   
}
