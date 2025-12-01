using FluentValidation;
using PetCareManagement.Application.Command.User.command;
using PetCareManagement.Application.IRepository;
using Users=PetCareManagement.Domain.Entity.User;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IAuth _auth;

    public CreateUserCommandValidator(IAuth auth)
    {
        _auth = auth;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, cancellation) =>
            {
                Users user = await _auth.FindByEmailAsync(email, cancellation);
                return user == null; 
            })
            .WithMessage("Email is already in use.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Fullname)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid phone number format.");
    }
}
