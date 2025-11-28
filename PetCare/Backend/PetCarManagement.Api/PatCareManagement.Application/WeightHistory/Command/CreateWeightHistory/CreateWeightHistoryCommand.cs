using MediatR;
using PetCareManagement.Application.WeightHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Command.CreateWeightHistory
{
    public class CreateWeightHistoryCommand:IRequest<WeightHistoryDto>
    {
        public int PetId { get; set; }
        public DateTime Date { get; set; }
        public decimal WeightKg { get; set; }
    }
}
