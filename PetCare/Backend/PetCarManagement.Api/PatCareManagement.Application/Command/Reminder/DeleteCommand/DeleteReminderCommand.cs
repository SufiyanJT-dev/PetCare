using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Reminder.DeleteCommand
{
    public class DeleteReminderCommand:IRequest<int>
    {
        public int Id { get;  set; }   

        
    }
}
