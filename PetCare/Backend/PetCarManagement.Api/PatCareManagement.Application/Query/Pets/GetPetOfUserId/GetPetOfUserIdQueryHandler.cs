using MediatR;
using PetCareManagement.Application.Dos.Pets;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetPetOfUserId
{
    public class GetPetOfUserIdQueryHandler : IRequestHandler<GetPetOfUserIdQuery, List<PetDto>>
    {
        private readonly IPetRepository petRepository;

        public GetPetOfUserIdQueryHandler(IPetRepository petRepository)
        {
            this.petRepository = petRepository;
        }
        public async Task<List<PetDto>> Handle(GetPetOfUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entity.Pets> pet = await petRepository.GetPetByOwnerIdAsync(request.Id);
            var petDtos = pet.Select(p => new PetDto
            {
                PetId = p.PetId,
                Name = p.Name,

                Breed = p.Breed,
                DateOfBirth = p.DateOfBirth,
                Age = p.GetAge(),
                OwnerId = p.UserId,
              
            }).ToList();

            return petDtos;
        }


    }
}

