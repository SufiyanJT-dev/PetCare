using MediatR;
using PetCareManagement.Application.Dos.Reminder;
using PetCareManagement.Application.Dtos.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Reminder.GetAllReminderByUserId
{
    public class GetUpcomingRemindersByUserIdQuery : IRequest<List<UpcomingReminderDto>>
    {
        public int UserId { get; set; }

        public GetUpcomingRemindersByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
