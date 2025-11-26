using PetCareManagement.Domain.Enum;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareManagement.Domain.Entity
{

    public class Reminder
    {
        [Key]
        public int ReminderId { get; private set; }

        [Required]
        public int PetId { get; private set; }

        public DateTime DateTime { get; private set; }

        public ReminderType Type { get; private set; }

        public string? Description { get; private set; }

        public bool IsCompleted { get; private set; }

      

        public Reminder(int petId, DateTime dateTime, ReminderType type, string? description)
        {
          
            PetId = petId;
            DateTime = dateTime;
            Type = type;
            Description = description;
            IsCompleted = false;
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }
    }
}
