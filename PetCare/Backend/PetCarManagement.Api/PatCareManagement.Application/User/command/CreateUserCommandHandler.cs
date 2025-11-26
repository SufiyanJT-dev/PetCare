using MediatR;
using Microsoft.AspNetCore.Identity;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.User.command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IGenericRepo<Domain.Entity.User> genericRepo;
        private readonly IPasswordHasher<Domain.Entity.User> _passwordHasher;
        public CreateUserCommandHandler(IGenericRepo<Domain.Entity.User> genericRepo, IPasswordHasher<Domain.Entity.User> passwordHasher)
        {
            this.genericRepo = genericRepo;
            this._passwordHasher = passwordHasher;
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
            await genericRepo.AddAsync(user);
            return  user.UserId;
        }
    }
}
