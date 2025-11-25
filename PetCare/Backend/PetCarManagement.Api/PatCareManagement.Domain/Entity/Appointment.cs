using System;

namespace PetCareManagement.Domain.Entity
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string Purpose { get; set; }
        public string VetName { get; set; }
        public string Clinic { get; set; }
        public string Status { get; set; }

        // Navigation
        public Pets Pet { get; set; }
    }
}
