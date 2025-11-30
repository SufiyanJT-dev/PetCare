using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.MedicalEvent.Command.DeleteMedicalEventCommand
{
    public class DeleteMedicalEventCommand:IRequest<bool>
    {
        public int EventId { get; set; }
    }
}
