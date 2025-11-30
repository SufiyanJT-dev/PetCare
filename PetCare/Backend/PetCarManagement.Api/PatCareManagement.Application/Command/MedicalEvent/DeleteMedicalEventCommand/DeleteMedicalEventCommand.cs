using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.MedicalEvent.DeleteMedicalEventCommand
{
    public class DeleteMedicalEventCommand:IRequest<bool>
    {
        public int EventId { get; set; }
    }
}
