using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;
using PetCareManagement.Application.IRepository;
namespace PetCareManagement.Application.Command.Attachment.CreateAttachmentCommand
{
    public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.Attachment> _repo;
        private readonly IFileStorageService _storage;

        public CreateAttachmentCommandHandler(
            IGenericRepo<Domain.Entity.Attachment> repo,
            IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task<int> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null)
                throw new ArgumentException("File is required");

          
            var fileUrl = await _storage.UploadFileAsync(request.File);

           
            var fileName = request.FileName ?? request.File.FileName;

           
            var attachment = new Domain.Entity.Attachment(request.MedicalEventID,fileUrl, fileName, request.Description);

            // Save to database
            await _repo.AddAsync(attachment);

            return attachment.AttachId;
        }
    }
}
