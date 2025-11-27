using FluentValidation;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Pets.Query.GetPetOfUserId
{
    public class GetPetOfUserIdQueryValidators:AbstractValidator<GetPetOfUserIdQuery>
    {
        private readonly IUserRepository<Domain.Entity.User> userRepository;

        public GetPetOfUserIdQueryValidators(IUserRepository<Domain.Entity.User> userRepository)
        {
            this.userRepository = userRepository;
            RuleFor(x => x.Id).NotEmpty().WithMessage("UserId should not be empty").MustAsync(async (id, cancellation) =>
            {
               var user= userRepository.GetByIdAsync(id);
                return await user != null;
            }).WithMessage("User must be present.");
           
        }
    }
}
