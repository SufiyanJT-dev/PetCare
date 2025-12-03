using MediatR;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Reminders = PetCareManagement.Domain.Entity.Reminder;

namespace PetCareManagement.Application.Query.Reminder.GetAllpreviousReminderByPetId
{
    public class GetAllpreviousReminderByPetIdQueryHandler : IRequestHandler<GetAllpreviousReminderByPetIdQuery, IEnumerable<MedicalEvent>>
    {
        private readonly IGenericRepo<MedicalEvent> _genericRepo;

        public GetAllpreviousReminderByPetIdQueryHandler(IGenericRepo<MedicalEvent> genericRepo)
        {
            this._genericRepo = genericRepo;
        }
        public async Task<IEnumerable<MedicalEvent>> Handle(GetAllpreviousReminderByPetIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<MedicalEvent, bool>> upcoming = wh => wh.PetId == request.PetId && wh.NextFollowupDate<DateTime.Now;
           IEnumerable<MedicalEvent> reminders = await _genericRepo.FindAsync(upcoming);
            return reminders.OrderBy(r => r.NextFollowupDate);

             

        }
    }
}
