using Microsoft.EntityFrameworkCore;
using PatCareManagement.Infrastucture.Persistance.Data;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class ReminderEmailService
    {
        private readonly PetCareDbContext _db;  
        private readonly IEmailSender _emailSender;

        public ReminderEmailService(PetCareDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public async Task SendReminderEmail(int reminderId)
        {
            
            var reminder = await _db.Reminders
                .Include(r => r.Pet)
                    .ThenInclude(p => p.Owner)
                .FirstOrDefaultAsync(r => r.ReminderId == reminderId);

            if (reminder == null || reminder.Pet?.Owner == null)
                return;

            var user = reminder.Pet.Owner;
            if (string.IsNullOrEmpty(user.Email))
                return;

            string subject = "Pet Reminder Notification";
            string body = $"Hi {reminder.Pet.Owner.Name}" +
                $"Reminder for your pet {reminder.Pet.Name}: {reminder.Description} scheduled at {reminder.DateTime}";

            await _emailSender.SendAsync(user.Email, subject, body);
        }
    }
}
