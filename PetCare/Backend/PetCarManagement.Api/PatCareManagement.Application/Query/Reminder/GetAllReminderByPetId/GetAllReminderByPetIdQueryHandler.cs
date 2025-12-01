using MediatR;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reminders = PetCareManagement.Domain.Entity.Reminder;

namespace PetCareManagement.Application.Query.Reminder.GetAllReminderByPetId
{
    public class GetAllReminderByPetIdQueryHandler : IRequestHandler<GetAllReminderByPetIdQuery, IEnumerable<Reminders>>
    {
        private readonly IGenericRepo<Reminders> genericRepo;

        public GetAllReminderByPetIdQueryHandler(IGenericRepo<Reminders> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public async Task<IEnumerable<Reminders>> Handle(GetAllReminderByPetIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Reminders, bool>> predicate = wh => wh.PetId == request.PetId && wh.DateTime>=DateTime.Now;


            return await genericRepo.FindAsync(predicate);

        }
    }
}
