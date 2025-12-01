using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders = PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Query.Reminder.GetAllReminderByPetId
{
    public class GetAllReminderByPetIdQuery:IRequest<IEnumerable<Reminders>>
    {
        public int PetId { get; set; }
     
    }
}
