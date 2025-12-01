using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Reminder.CreateCommand
{
    public class CreateReminderCommandValidator:AbstractValidator<CreateReminderCommand>
    {
        public  CreateReminderCommandValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            RuleFor(x => x.PetId)
                .MustAsync(async (PetId,Cancellation) =>
                {
                    Domain.Entity.Pets? pets = await genericRepo.GetByIdAsync(PetId);
                    return pets != null;
                }).WithMessage("Pet Not Present ");
            RuleFor(x => x.DateTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Reminder date and time must be in the future.");
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid reminder type.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
