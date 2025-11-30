using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace PetCareManagement.Application.Command.Attachment.CreateAttachmentCommand
{
    public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
    {
        public CreateAttachmentCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(IsAllowedFileType).WithMessage("Only PDF, JPG, JPEG, PNG files are allowed.")
                .Must(f => f.Length <= 5 * 1024 * 1024) // 5MB limit
                .WithMessage("File size cannot exceed 5MB.");

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
