using MediatR;
using PetCareManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Command.UpdatePetDeatilsCommand
{
    public class UpdatePetCommand: IRequest<int>
    {

        [JsonIgnore] public int PetId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Species Species { get; set; }



    }
}
