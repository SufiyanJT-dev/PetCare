using MediatR;
using PetCareManagement.Application.Dos.Pets;
using PetCareManagement.Application.Dtos;
using System.Collections.Generic;

namespace PetCareManagement.Application.Query.Pets.GetPetByName
{
    public class GetPetByNameQuery : IRequest<List<PetDto>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public GetPetByNameQuery(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
