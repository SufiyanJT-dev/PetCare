using System;

namespace PetCareManagement.Domain.Entity
{
    public class WeightHistory
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }

        public int WeightGrams { get; set; }
        public DateTime RecordedAt { get; set; }

        // Navigation
        public Pets Pet { get; set; }
    }
}
