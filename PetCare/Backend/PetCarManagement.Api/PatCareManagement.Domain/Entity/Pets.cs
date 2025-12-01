
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Domain.Entity
{

    public class Pets
    {
        [Key]
        public int PetId { get; private set; }

        [Required]
        public int UserId { get; private set; } 

        [Required]
        public string Name { get; private set; }

        [Required]
        public Species Species { get; private set; }

        public string? Breed { get; private set; }

        public DateTime? DateOfBirth { get; private set; }

        public bool IsActive { get; private set; } 

        

       
   
        public DateTime CreatedAt { get; private set; }


       
        public User? Owner { get; private set; }
       

        private readonly List<WeightHistory> _weightHistory = new();
        public IReadOnlyCollection<WeightHistory> WeightHistory => _weightHistory.AsReadOnly();

       
        private Pets() { }

        public Pets(int userId, string name, Species species, string breed, DateTime dateOfBirth)
        {
            UserId = userId;
            Name = name;
            Species = species;
            CreatedAt = DateTime.UtcNow;
            Breed = breed;
            DateOfBirth = dateOfBirth;           
            IsActive = true;
        }


        public void UpdateProfile(string name, Species species, string? breed, DateTime? dob)
        {
            Name = name;
            Species = species;
            Breed = breed;
            DateOfBirth = dob;
         
        }
        public void Deactivate()
        {
            IsActive = false;
        }
       public int GetAge()
        {
           return DateOfBirth.HasValue ? DateTime.Now.Year - DateOfBirth.Value.Year : 0;
        }
      

      

        public void AddWeightHistory(WeightHistory wh)
        {
            _weightHistory.Add(wh);
        }

       
    }


}
