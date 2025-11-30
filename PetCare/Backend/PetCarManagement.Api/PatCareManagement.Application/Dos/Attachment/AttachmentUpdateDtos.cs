using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Dos.Attachment
{
    public class AttachmentUpdateDtos
    {
        public int MedicalEventId { get; set; }
        public IFormFile File { get; set; }

        public string? FileName { get; set; }

        public string Description { get; set; }
    }
}
