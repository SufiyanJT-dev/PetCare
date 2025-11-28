using MediatR;
using PetCareManagement.Application.WeightHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Command.UpdateWeightHistory
{
    public class UpdateWeightHistoryCommand:IRequest<WeightHistoryDto>
    {
        public int WhId { get; set; }
        public DateTime Date { get; set; }
        public decimal WeightKg { get; set; }
    }
}
