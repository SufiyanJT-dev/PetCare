using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.MedicalEvent.AddMedicalEventCommand
{
    public class AddMedicalEventCommand: IRequest<int>
    {
        public int PetId { get;  set; }

        public DateTime Date { get;  set; }

        
        public MedicalEventType Type { get;  set; }

        public string? VetName { get;  set; }

        public string? Notes { get;  set; }

        public DateTime? NextFollowupDate { get;  set; }

    }
}
