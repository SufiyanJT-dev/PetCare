using MediatR;
using PetCareManagement.Application.WeightHistory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.WeightHistory.Query.GetAllWeightHistoryByPetId
{
    public class GetAllWeightHistoryByPetIdQuery: IRequest<IEnumerable<WeightHistoryDto>>
    {
        public int PetId { get; set; }
    }
}
