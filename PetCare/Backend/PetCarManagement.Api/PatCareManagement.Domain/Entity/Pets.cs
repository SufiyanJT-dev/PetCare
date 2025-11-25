using PatCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Domain.Entity
{
    public class Pets
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public string SpeciesName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CurrentWeightGrams { get; set; }
        public bool? IsActive { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }

   
        public User Owner { get; set; }
        public ICollection<WeightHistory> WeightHistories { get; set; }
        public ICollection<MedicalEvent> MedicalEvents { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<Reminder> Reminders { get; set; }


    }
}
