using System;

namespace PetCareManagement.Domain.Entity
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }

        public DateTime ReminderTime { get; set; }
        public string Type { get; set; }
        public string RelatedTable { get; set; }
        public Guid? RelatedId { get; set; }
        public string Status { get; set; }
        public DateTime? SentAt { get; set; }

        
        public Pets Pet { get; set; }
    }
}
