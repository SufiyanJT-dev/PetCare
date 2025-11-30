using MediatR;
using PetCareManagement.Application.Command.WeightHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.WeightHistory.UpdateWeightHistory
{
    public class UpdateWeightHistoryCommand:IRequest<WeightHistoryDto>
    {
        public int WhId { get; set; }
        public DateTime Date { get; set; }
        public decimal WeightKg { get; set; }
    }
}
