using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Command.DeleteWeightHistory
{
    public class DeleteWeightHistoryCommand: IRequest<bool>
    {
        public int WeightHistoryId { get; set; }
    }
}
