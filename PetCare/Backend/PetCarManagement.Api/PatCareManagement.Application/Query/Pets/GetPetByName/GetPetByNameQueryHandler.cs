using MediatR;
using PetCareManagement.Application.Dos.Pets;
using PetCareManagement.Application.Dtos;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Pets.GetPetByName
{
    public class GetPetByNameQueryHandler
        : IRequestHandler<GetPetByNameQuery, List<PetDto>>
    {
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.Pets> _repo;

        public GetPetByNameQueryHandler(IGenericRepo<PetCareManagement.Domain.Entity.Pets> repo)
        {
            _repo = repo;
        }

        public async Task<List<PetDto>> Handle(GetPetByNameQuery request, CancellationToken cancellationToken)
        {
            var pets = await _repo.FindAsync(p =>
                p.UserId == request.UserId &&
                p.Name.ToLower().Contains(request.Name.ToLower())
            );

            return pets.Select(p => new PetDto
            {
                PetId = p.PetId,
                OwnerId = p.UserId,
                Name = p.Name,
                Breed = p.Breed,
                DateOfBirth = p.DateOfBirth,
 
            }).ToList();
        }
    }
}
