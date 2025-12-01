using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using PetCareManagement.Application.IRepository;
using Hangfire;
using System.Text;

using System.Threading.Tasks;
using Reminders= PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Command.Reminder.CreateCommand
{
    public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, int>
    {
        private readonly IGenericRepo<Reminders> _genericRepo;
        private readonly IReminderScheduler _scheduler;

        public CreateReminderCommandHandler(
            IGenericRepo<Reminders> genericRepo,
            IReminderScheduler scheduler)
        {
            _genericRepo = genericRepo;
            _scheduler = scheduler;
        }

        public async Task<int> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {
            string jobId;
            string reminderSourceType = "Manual";
            Reminders reminder = new Reminders(request.PetId, request.DateTime, request.Type, request.Description, reminderSourceType);
            await _genericRepo.AddAsync(reminder);

            DateTime emailTime = reminder.DateTime.AddDays(-1);

            if (emailTime > DateTime.UtcNow)
            {
                jobId = _scheduler.ScheduleReminder(reminder.ReminderId, emailTime);
                reminder.MarkCompleted();
                reminder.SetJobId(jobId);
               
            }
            else
            {
                 jobId = _scheduler.ScheduleReminder(reminder.ReminderId, emailTime); 
                reminder.SetJobId(jobId);
               
            }

            return reminder.ReminderId;
        }
    }
    }
