using MediatR;
using Microsoft.AspNetCore.Http; // for IFormFile

namespace PetCareManagement.Application.Command.Attachment.CreateAttachmentCommand
{
    public class CreateAttachmentCommand : IRequest<int>
    {
        
        public int MedicalEventID { get; set; }
        public IFormFile File { get; set; }  

       
        public string? FileName { get; set; }

        
        public string Description { get; set; }
    }
}
