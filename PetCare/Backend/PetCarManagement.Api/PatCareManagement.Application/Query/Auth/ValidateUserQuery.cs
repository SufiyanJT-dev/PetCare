using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Dos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.Auth
{
    public class ValidateUserQuery: IRequest<AuthResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
