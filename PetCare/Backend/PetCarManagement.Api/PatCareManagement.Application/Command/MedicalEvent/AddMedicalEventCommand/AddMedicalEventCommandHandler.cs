using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using PetCareManagement.Domain.Enum;
using System;
using System.Threading;
using System.Threading.Tasks;
using Reminders = PetCareManagement.Domain.Entity.Reminder;

namespace PetCareManagement.Application.Command.MedicalEvent.AddMedicalEventCommand
{
    public class AddMedicalEventCommandHandler : IRequestHandler<AddMedicalEventCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.MedicalEvent> _medicalEventRepo;
        private readonly IReminderScheduler _scheduler;
        private readonly IGenericRepo<Reminders> _reminderRepo;

        public AddMedicalEventCommandHandler(
            IGenericRepo<Domain.Entity.MedicalEvent> medicalEventRepo,
            IReminderScheduler scheduler,
            IGenericRepo<Reminders> reminderRepo)
        {
            _medicalEventRepo = medicalEventRepo;
            _scheduler = scheduler;
            _reminderRepo = reminderRepo;
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

            await _medicalEventRepo.AddAsync(medicalEvent);

            if (medicalEvent.NextFollowupDate.HasValue)
            {
                Reminders reminder = new Reminders(
                    petId: request.PetId,
                    dateTime: medicalEvent.NextFollowupDate.Value,
                    type: ReminderType.MedicalEvents,
                   
                    description: $"Follow-up with {medicalEvent.VetName} for {medicalEvent.Type}",
                    sourceType: "MedicalEvent"

                );

                await _reminderRepo.AddAsync(reminder);

                
                var now = DateTime.UtcNow;
                var followup = medicalEvent.NextFollowupDate.Value;
                var timeUntilFollowup = followup - now;

                string jobId;

                if (timeUntilFollowup.TotalDays < 1)
                {
                    
                    jobId = _scheduler.EnqueueReminder(reminder.ReminderId);
                    reminder.SetJobId(jobId);
                    reminder.MarkCompleted();
                    reminder.SetLinkedEntityId(medicalEvent.Id);
                }
                else
                {
                    
                    DateTime scheduledTime = followup.AddDays(-1);
                    jobId = _scheduler.ScheduleReminder(reminder.ReminderId, scheduledTime);
                    reminder.SetJobId(jobId);
                    reminder.SetLinkedEntityId(medicalEvent.Id);
                }

                reminder.SetJobId(jobId);
                await _reminderRepo.UpdateAsync(reminder);
            }

            return medicalEvent.Id;
        }
    }
}
