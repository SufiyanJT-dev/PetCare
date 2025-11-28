using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareManagement.Domain.Entity
{
    public class WeightHistory
    {
        [Key]
        public int WhId { get; private set; }

        [Required]
        public int PetId { get; private set; }

        public DateTime Date { get; private set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal WeightKg { get; private set; }



        public WeightHistory(int petId, DateTime date, decimal weightKg)
        {
            if (weightKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(weightKg));

            PetId = petId;
            Date = date;
            WeightKg = weightKg;
        }

    }


}
