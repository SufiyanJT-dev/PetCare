using MediatR;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders = PetCareManagement.Domain.Entity.Reminder;
namespace PetCareManagement.Application.Query.Reminder.GetAllpreviousReminderByPetId
{
    public class GetAllpreviousReminderByPetIdQuery:IRequest<IEnumerable<MedicalEvent>>
    {
        public int PetId { get; set; }
     
    }
}
