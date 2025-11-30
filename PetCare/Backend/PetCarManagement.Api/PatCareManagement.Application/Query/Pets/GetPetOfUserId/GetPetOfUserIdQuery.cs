using MediatR;
using PetCareManagement.Application.Dos.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetPetOfUserId
{
    public class GetPetOfUserIdQuery:IRequest<List<PetDto>>
    {
        public int Id { get; set; }
    }
}
