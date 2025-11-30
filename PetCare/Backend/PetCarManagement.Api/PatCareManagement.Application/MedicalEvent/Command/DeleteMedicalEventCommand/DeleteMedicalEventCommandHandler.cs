using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.DeleteMedicalEventCommand
{
    public class DeleteMedicalEventCommandHandler : IRequestHandler<DeleteMedicalEventCommand, bool>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> genericRepo;

        public DeleteMedicalEventCommandHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public Task<bool> Handle(DeleteMedicalEventCommand request, CancellationToken cancellationToken)
        {
          
            return genericRepo.DeleteAsync(request.EventId); 
        }
    }
}
