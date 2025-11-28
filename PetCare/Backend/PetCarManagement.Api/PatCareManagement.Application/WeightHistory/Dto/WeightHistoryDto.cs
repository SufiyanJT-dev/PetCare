using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Dto
{
    public class WeightHistoryDto
    {
        public int WhId { get; set; }
        public int PetId { get; set; }
        public DateTime Date { get; set; }
        public decimal WeightKg { get; set; }
    }
}
