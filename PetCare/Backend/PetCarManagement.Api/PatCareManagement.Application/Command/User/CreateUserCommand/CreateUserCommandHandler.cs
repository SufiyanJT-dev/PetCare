using MediatR;
using Microsoft.AspNetCore.Identity;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Command.User.command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository<Domain.Entity.User> userRepository;
        private readonly IPasswordHasher<Domain.Entity.User> _passwordHasher;
        public CreateUserCommandHandler(IUserRepository<Domain.Entity.User> userRepository, IPasswordHasher<Domain.Entity.User> passwordHasher)
        {
            this.userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
          
            Domain.Entity.User user = new Domain.Entity.User
            (
            name: request.Fullname,
            email: request.Email,
            phoneNumber: request.PhoneNumber
            
            );
            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);
            user.SetPasswordHash(hashedPassword);
            await userRepository.AddAsync(user);
            return  user.UserId;
        }
    }
}
