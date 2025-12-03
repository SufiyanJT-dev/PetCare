using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.MedicalEvent.DeleteMedicalEventCommand
{
    public class DeleteMedicalEventCommandHandler : IRequestHandler<DeleteMedicalEventCommand, bool>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> _genericRepo;
        private readonly IReminderScheduler _scheduler;
        private readonly IGenericRepo<Domain.Entity.Reminder> _reminder;

        public DeleteMedicalEventCommandHandler(IGenericRepo<Domain.Entity.MedicalEvent> genericRepo,IReminderScheduler scheduler,IGenericRepo<Domain.Entity.Reminder> reminder)
        {
            this._genericRepo = genericRepo;
            this._scheduler = scheduler;
            this._reminder = reminder;
        }
        public async  Task<bool> Handle(DeleteMedicalEventCommand request, CancellationToken cancellationToken)
        {
            Domain.Entity.MedicalEvent? medicalEvent = await _genericRepo.GetByIdAsync(request.EventId);
            IEnumerable< Domain.Entity.Reminder> reminders = await _reminder.FindAsync(r =>
                r.SourceType == "MedicalEvent" && r.LinkedEntityId == request.EventId);
            foreach (Domain.Entity.Reminder rem in reminders)
            {
                if (!string.IsNullOrEmpty(rem.JobId))
                {
                     _scheduler.CancelReminder(rem.JobId);
                }
            }
            return await _genericRepo.DeleteAsync(request.EventId); 
            
        }
    }
}
