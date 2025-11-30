using MediatR;
using PetCareManagement.Application.Command.WeightHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.WeightHistory.GetAllWeightHistoryByPetId
{
    public class GetAllWeightHistoryByPetIdQuery: IRequest<IEnumerable<WeightHistoryDto>>
    {
        public int PetId { get; set; }
    }
}
