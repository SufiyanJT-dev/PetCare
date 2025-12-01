using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders= PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Command.Reminder.DeleteCommand
{
    public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, int>
    {
        private readonly IGenericRepo<Reminders> genericRepo;
        private readonly IReminderScheduler scheduler;

        public DeleteReminderCommandHandler(IGenericRepo<Reminders> genericRepo,IReminderScheduler scheduler)
        {
            this.genericRepo = genericRepo;
            this.scheduler = scheduler;
        }
        public async Task<int> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = await genericRepo.GetByIdAsync(request.Id);


            scheduler.CancelReminder(reminder.JobId);

            await genericRepo.DeleteAsync(request.Id);
      
            return request.Id;
        }
    }
}
