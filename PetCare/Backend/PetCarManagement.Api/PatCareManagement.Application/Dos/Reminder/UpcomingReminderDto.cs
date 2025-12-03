using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Dos.Reminder
{
    public class UpcomingReminderDto
    {
        public int ReminderId { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
