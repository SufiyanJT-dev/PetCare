using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.UpdateMedicalEventCommand
{
    public class UpdateMedicalEventCommand: IRequest<int>
    {
       [JsonIgnore] public int Id { get; set; }
        
        public int PetId { get; set; }
        public DateTime EventDate { get; set; }

        public string Veterinarian { get; set; }
        public string Notes { get; set; }
        public MedicalEventType Type { get; set; }

        public DateTime? NextFollowupDate { get; set; }

    }
}
