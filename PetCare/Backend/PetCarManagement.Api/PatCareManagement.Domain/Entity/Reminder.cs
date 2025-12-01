using PetCareManagement.Domain.Enum;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

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
        public Pets? Pet { get; private set; }
        public string? JobId {            get; private set;        }
        public string SourceType { get; private set; }   
        public int? LinkedEntityId { get; private set; }
        private  Reminder() { }

        public Reminder(int petId, DateTime dateTime, ReminderType type, string? description, string sourceType)
        {
          
            PetId = petId;
            DateTime = dateTime;
            Type = type;
            Description = description;
            IsCompleted = false;
            SourceType = sourceType;
        }
        public void Update(int petId, DateTime dateTime, ReminderType type, string? description)
        {

            PetId = petId;
            DateTime = dateTime;
            Type = type;
            Description = description;
            IsCompleted = false;
        }
        public void SetLinkedEntityId(int linkedEntityId)
        {
            LinkedEntityId = linkedEntityId;
        }
       public void SetJobId(string jobId)
        {
            JobId = jobId;
        }
        public void MarkCompleted()
        {
            IsCompleted = true;
        }
    }
}
