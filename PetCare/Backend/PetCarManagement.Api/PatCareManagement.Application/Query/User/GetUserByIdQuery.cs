using MediatR;
using PetCareManagement.Application.Dos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.User
{
    public class GetUserByIdQuery:IRequest<UserDtos>
    {
      [JsonIgnore]  public int UserId { get; set; }
       
    }
}
