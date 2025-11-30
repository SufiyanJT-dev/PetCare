using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Dos.Pets
{
    public class PetDto
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; } 
        public int Species { get; set; }
        public string? Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        
       

    }
}
