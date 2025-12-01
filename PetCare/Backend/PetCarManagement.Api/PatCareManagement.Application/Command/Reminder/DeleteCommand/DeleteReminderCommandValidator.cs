using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.Reminder.DeleteCommand
{
    public class DeleteReminderCommandValidator:AbstractValidator<DeleteReminderCommand>
    {
        public DeleteReminderCommandValidator(IGenericRepo<Domain.Entity.Pets> genericRepo)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("PetId is required.")
                .MustAsync(async (petId, cancellation) =>
                {
                    var pet = await genericRepo.GetByIdAsync(petId);
                    return pet != null;
                }).WithMessage("Pet with the given PetId does not exist.");
        }
    }

}
