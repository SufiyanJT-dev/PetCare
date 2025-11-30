using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.IRepository
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
