using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetCareManagement.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Auth.Query
{
    public class ValidateUserQuery: IRequest<ActionResult<AuthResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
