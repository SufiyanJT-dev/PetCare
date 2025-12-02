using PetCareManagement.Domain.Enum; // make sure ReminderType enum is accessible
using System;

namespace PetCareManagement.Application.Dtos.Reminder
{
    public class ReminderDto
    {
        public int ReminderId { get; set; }

        public DateTime DateTime { get; set; }

        public MedicalEventType Type { get; set; } 

        public string? Description { get; set; }

        public string? Name { get; set; } 

        public int? LinkedEntityId { get; set; }
    }
}
