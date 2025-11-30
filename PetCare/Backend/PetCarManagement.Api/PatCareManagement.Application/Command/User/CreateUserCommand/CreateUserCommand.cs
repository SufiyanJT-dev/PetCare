using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.User.command
{
   
        public class CreateUserCommand : IRequest<int>
    {
       public string Email { get; set; }
       public string Password { get; set; }
        public string PhoneNumber { get; set; }
        
        public string Fullname { get; set; }
    }
    
}
