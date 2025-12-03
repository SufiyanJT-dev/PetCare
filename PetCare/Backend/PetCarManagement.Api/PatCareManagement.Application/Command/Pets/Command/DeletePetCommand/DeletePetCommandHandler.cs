using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Pets.Command.DeletePetCommand
{
    public class DeletePetCommandHandler : IRequestHandler<DeletePetCommand, ActionResult<bool>>
    {
        private readonly IGenericRepo<Domain.Entity.Pets> _genericRepo;

        public DeletePetCommandHandler(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            this._genericRepo = genericRepo;
        }
        public Task<ActionResult<bool>> Handle(DeletePetCommand request, CancellationToken cancellationToken)
        {
            _genericRepo.DeleteAsync(request.PetId);
            return Task.FromResult<ActionResult<bool>>(true);
        }
    }
}
