using System;

namespace PetCareManagement.Domain.Entity
{
    public class MedicalEvent
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }

        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string VetName { get; set; }
        public string Clinic { get; set; }
        public string Notes { get; set; }

        // Navigation
        public Pets Pet { get; set; }
    }
}
