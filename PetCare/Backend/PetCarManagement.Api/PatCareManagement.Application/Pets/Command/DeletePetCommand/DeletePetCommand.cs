using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Command.DeletePetCommand
{
    public class DeletePetCommand:IRequest<ActionResult<bool>>
    {
        public int PetId { get; set; }
    }
}
