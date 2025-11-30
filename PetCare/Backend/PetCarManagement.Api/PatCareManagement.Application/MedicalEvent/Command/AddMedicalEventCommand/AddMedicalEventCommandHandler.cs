using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.AddMedicalEventCommand
{
    public class AddMedicalEventCommandHandler : IRequestHandler<AddMedicalEventCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> genericRepo;

        public AddMedicalEventCommandHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public async Task<int> Handle(AddMedicalEventCommand request, CancellationToken cancellationToken)
        {
           Domain.Entity.MedicalEvent medicalEvent = new Domain.Entity.MedicalEvent(
                petId: request.PetId,
                date: request.Date,
                type: request.Type,
                vetName: request.VetName,
                notes: request.Notes,
                nextFollowup: request.NextFollowupDate
               );
            await genericRepo.AddAsync(medicalEvent);
            return medicalEvent.Id;

        }
    }
}

