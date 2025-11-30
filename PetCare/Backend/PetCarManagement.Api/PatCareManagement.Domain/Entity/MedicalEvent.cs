using PetCareManagement.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PetCareManagement.Domain.Entity
{
    public class MedicalEvent
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public int PetId { get; private set; }

        public DateTime Date { get; private set; }

        [Required]
        public MedicalEventType Type { get; private set; }

        public string? VetName { get; private set; }

        public string? Notes { get; private set; }

        public DateTime? NextFollowupDate { get; private set; }

     
        public Pets? Pet { get; private set; }

    
     private MedicalEvent() { }

        public MedicalEvent(int petId, DateTime date, MedicalEventType type, string? vetName, string? notes, DateTime? nextFollowup)
        {
            
            PetId = petId;
            Date = date;
            Type = type;
            VetName = vetName;
            Notes = notes;
            NextFollowupDate = nextFollowup;
        }
        public void update(int petId, DateTime date, MedicalEventType type, string? vetName, string? notes, DateTime? nextFollowup)
        {

            PetId = petId;
            Date = date;
            Type = type;
            VetName = vetName;
            Notes = notes;
            NextFollowupDate = nextFollowup;
        }

      
    }

}
