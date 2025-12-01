using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface IEmailSender
    {
       public Task SendAsync(string to, string subject, string body);
    }
}
