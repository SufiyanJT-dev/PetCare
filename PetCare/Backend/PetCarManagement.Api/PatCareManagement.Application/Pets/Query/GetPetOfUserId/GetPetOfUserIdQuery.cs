using MediatR;
using PetCareManagement.Application.Pets.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Query.GetPetOfUserId
{
    public class GetPetOfUserIdQuery:IRequest<List<PetDto>>
    {
        public int Id { get; set; }
    }
}
