using Hangfire;
using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MedicalEvents = PetCareManagement.Domain.Entity.MedicalEvent;
using Reminders = PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Command.MedicalEvent.UpdateMedicalEventCommand
{
    public class UpdateMedicalEventCommandHandler : IRequestHandler<UpdateMedicalEventCommand, int>
    {
        private readonly IGenericRepo<MedicalEvents> _genericRepo;
        private readonly IReminderScheduler _scheduler;
        private readonly IGenericRepo<Reminders> _reminderRepo;

        public UpdateMedicalEventCommandHandler(
            IGenericRepo<MedicalEvents> genericRepo,
            IReminderScheduler scheduler,
            IGenericRepo<Reminders> reminderRepo)
        {
            _genericRepo = genericRepo;
            _scheduler = scheduler;
            _reminderRepo = reminderRepo;
        }

        public async Task<int> Handle(UpdateMedicalEventCommand request, CancellationToken cancellationToken)
        {
            MedicalEvents medicalEvent = await _genericRepo.GetByIdAsync(request.Id);
            if (medicalEvent == null)
                return 0;

            IEnumerable<Reminders> reminders = await _reminderRepo.FindAsync(r =>
                r.SourceType == "MedicalEvent" && r.LinkedEntityId == medicalEvent.Id);

            foreach (Reminders reminder in reminders)
            {
                if (!string.IsNullOrEmpty(reminder.JobId))
                {
                     _scheduler.CancelReminder(reminder.JobId);
                }
            }

            medicalEvent.update(
                petId: request.PetId,
                date: request.EventDate,
                type: request.Type,
                vetName: request.Veterinarian,
                notes: request.Notes,
                nextFollowup: request.NextFollowupDate
            );

            await _genericRepo.UpdateAsync(medicalEvent);

            foreach (Reminders reminder in reminders)
            {
                string jobId;

                if (medicalEvent.NextFollowupDate.HasValue)
                {
                    DateTime followup = medicalEvent.NextFollowupDate.Value;
                    TimeSpan timeUntilFollowup = followup - DateTime.UtcNow;

                    if (timeUntilFollowup.TotalDays < 1)
                    {
                        jobId =  _scheduler.EnqueueReminder(reminder.ReminderId);
                    }
                    else
                    {
                        DateTime scheduledTime = followup.AddDays(-1);
                        jobId =  _scheduler.ScheduleReminder(reminder.ReminderId, scheduledTime);
                    }

                    reminder.SetJobId(jobId);
                    await _reminderRepo.UpdateAsync(reminder);
                }
            }

            return medicalEvent.Id;
        }
    }
}
