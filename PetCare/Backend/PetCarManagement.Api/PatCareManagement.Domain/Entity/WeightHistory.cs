using PetCareManagement.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        private WeightHistory() { }

        public WeightHistory(int petId, DateTime date, decimal weightKg)
        {
            if (weightKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(weightKg));

            PetId = petId;
            Date = date;
            WeightKg = weightKg;
        }
        public void UpdateWeightHistory(DateTime date, decimal weightKg)
        {
            Date = date;
            WeightKg = weightKg;

        }
    }


}
