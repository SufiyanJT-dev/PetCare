using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace PetCareManagement.Application.Command.Attachment.UpdateAttachmentCommand
{
    public class UpdateAttachmentCommand : IRequest<int>
    {
       public int Id { get; set; }
        public int MedicalEventId { get; set; }
        public IFormFile File { get; set; }

        public string? FileName { get; set; }

        public string Description { get; set; }
    }
}
