using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Reminder.CreateCommand
{
    public class CreateReminderCommand:IRequest<int>
    {
        public int PetId { get;  set; }

        public DateTime DateTime { get;  set; }

        public ReminderType Type { get;  set; }

        public string? Description { get;  set; }

        
    }
}
