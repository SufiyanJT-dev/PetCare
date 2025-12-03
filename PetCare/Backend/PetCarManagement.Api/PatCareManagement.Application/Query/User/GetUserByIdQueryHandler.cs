using MediatR;
using PetCareManagement.Application.Dos.User;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Application.Query.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDtos>
    {
        private readonly IGenericRepo<Domain.Entity.User> _genericRepo;

        public GetUserByIdQueryHandler(IGenericRepo<Domain.Entity.User> genericRepo)
        {
            this._genericRepo = genericRepo;
        }
        public async Task<UserDtos> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entity.User? user = await _genericRepo.GetByIdAsync(request.UserId);
            UserDtos userDtos = new UserDtos();
            userDtos.Email=user.Email;
            userDtos.Name=user.Name;
            userDtos.PhoneNumber=user.PhoneNumber;
            return  userDtos;
        }
    }
}
