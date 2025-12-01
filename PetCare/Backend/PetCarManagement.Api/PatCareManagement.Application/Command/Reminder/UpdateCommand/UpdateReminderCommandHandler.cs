using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders= PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Command.Reminder.UpdateCommand
{
    public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, int>
    {
        private readonly IGenericRepo<Reminders> genericRepo;
        private readonly IReminderScheduler scheduler;

        public UpdateReminderCommandHandler(IGenericRepo<Reminders> genericRepo,IReminderScheduler scheduler)
        {
            this.genericRepo = genericRepo;
            this.scheduler = scheduler;
        }
        public async Task<int> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            Reminders? existingReminder = await genericRepo.GetByIdAsync(request.ReminderId);

             existingReminder?.Update(request.PetId,request.DateTime,request.Type,request.Description);
            if (existingReminder.IsCompleted)
            {
                
                return existingReminder.ReminderId;
            }
            await genericRepo.UpdateAsync(existingReminder);
            DateTime emailTime = existingReminder.DateTime.AddDays(-1);

            if (emailTime > DateTime.UtcNow)
                scheduler.ScheduleReminder(existingReminder.ReminderId, emailTime);
            else
                scheduler.EnqueueReminder(existingReminder.ReminderId);

            return existingReminder.ReminderId;

        }
    }
}
