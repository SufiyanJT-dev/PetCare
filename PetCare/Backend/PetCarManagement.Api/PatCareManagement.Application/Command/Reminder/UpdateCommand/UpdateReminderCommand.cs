using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Reminder.UpdateCommand
{
    public class UpdateReminderCommand:IRequest<int>
    {
        [JsonIgnore] public int ReminderId { get;  set; }
        public int PetId { get;  set; }

        public DateTime DateTime { get;  set; }

        public ReminderType Type { get;  set; }

        public string? Description { get;  set; }

        
    }
}
