using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attachments=PetCareManagement.Domain.Entity.Attachment;
namespace PetCareManagement.Application.Command.Attachment.UpdateAttachmentCommand
{
    public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, int>
    {
        private readonly IGenericRepo<Attachments> genericRepo;
        private readonly IFileStorageService fileStorageService;

        public UpdateAttachmentCommandHandler(IGenericRepo<Attachments> genericRepo,IFileStorageService fileStorageService)
        {
            this.genericRepo = genericRepo;
            this.fileStorageService = fileStorageService;
        }

        public async Task<int> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
        {
              var fileUrl = await fileStorageService.UploadFileAsync(request.File);

           
            var fileName = request.FileName ?? request.File.FileName;

           Attachments attachment=  await genericRepo.GetByIdAsync(request.Id);
            attachment.Update(
                                   
                 medicalEventId: request.MedicalEventId,
                fileUrl :fileUrl,
            fileName: fileName,
            description:request.Description


               );
           await genericRepo.UpdateAsync(attachment);
            return await Task.FromResult(attachment.AttachId);
        }
    }
}
