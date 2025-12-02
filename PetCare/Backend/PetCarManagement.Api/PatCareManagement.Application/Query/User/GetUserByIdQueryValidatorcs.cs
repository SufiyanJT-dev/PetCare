using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.User
{
    public class GetUserByIdQueryValidatorcs:AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidatorcs(IGenericRepo<Domain.Entity.User> genericRepo)
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MustAsync(async (userId, ct) =>
                {
                    var users = await genericRepo.FindAsync(u => u.UserId == userId);

                    return users != null && users.Any();
                })
                .WithMessage("User with the given UserId does not exist.");
        }
    }
}
