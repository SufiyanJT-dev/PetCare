using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.UpdateMedicalEventCommand
{
    public class UpdateMedicalEventCommandHandler : IRequestHandler<UpdateMedicalEventCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> genericRepo;

       
        public UpdateMedicalEventCommandHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public async Task<int> Handle(UpdateMedicalEventCommand request, CancellationToken cancellationToken)
        {

            Domain.Entity.MedicalEvent medicalEvent = await genericRepo.GetByIdAsync(request.Id);
         
            medicalEvent.update(

        petId: request.PetId,
                date: request.EventDate,
                type: request.Type,
                vetName: request.Veterinarian,
                notes: request.Notes,
                nextFollowup: request.NextFollowupDate
               );
            await genericRepo.UpdateAsync(medicalEvent);
            return medicalEvent.Id;
           
        }
    }
}
