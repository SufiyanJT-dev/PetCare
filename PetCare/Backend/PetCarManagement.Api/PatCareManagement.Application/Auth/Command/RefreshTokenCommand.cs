using MediatR;
using PetCareManagement.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Auth.Command
{
    public class RefreshTokenCommand: IRequest<AuthResult>
    {
        public string RefreshToken { get; set; } = default!;
    
    }
}
