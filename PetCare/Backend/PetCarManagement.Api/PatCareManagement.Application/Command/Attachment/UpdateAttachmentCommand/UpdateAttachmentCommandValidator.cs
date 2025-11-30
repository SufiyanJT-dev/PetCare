using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Attachment.UpdateAttachmentCommand
{
    public class UpdateAttachmentCommandValidator : AbstractValidator<UpdateAttachmentCommand>
    {
        public UpdateAttachmentCommandValidator() {
            RuleFor(x => x.File)
                    .NotNull().WithMessage("File is required.")
                    .Must(IsAllowedFileType).WithMessage("Only PDF, JPG, JPEG, PNG files are allowed.");



        RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

        }
        private bool IsAllowedFileType(IFormFile file)
        {
            if (file == null) return false;

            var allowedTypes = new[]
            {
                "application/pdf",
                "image/jpeg",
                "image/png"
            };

            return allowedTypes.Contains(file.ContentType);
        }
    }
}
