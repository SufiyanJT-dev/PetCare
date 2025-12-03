using MediatR;
using PetCareManagement.Application.Dos.Reminder;
using PetCareManagement.Application.IRepository;
using PetCareManagement.Domain.Entity;

namespace PetCareManagement.Application.Query.Reminder.GetAllReminderByUserId
{
    public class GetUpcomingRemindersByUserIdQueryHandler
        : IRequestHandler<GetUpcomingRemindersByUserIdQuery, List<UpcomingReminderDto>>
    {
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.Reminder> _reminderRepo;
        private readonly IGenericRepo<PetCareManagement.Domain.Entity.Pets> _petRepo;

        public GetUpcomingRemindersByUserIdQueryHandler(
            IGenericRepo<PetCareManagement.Domain.Entity.Reminder> reminderRepo,
            IGenericRepo<PetCareManagement.Domain.Entity.Pets> petRepo)
        {
            _reminderRepo = reminderRepo;
            _petRepo = petRepo;
        }

        public async Task<List<UpcomingReminderDto>> Handle(
            GetUpcomingRemindersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            // Get all active pets of the user
            var userPets = await _petRepo.FindAsync(p => p.UserId == request.UserId && p.IsActive);

            var petIds = userPets.Select(p => p.PetId).ToList();

            // Get upcoming reminders of these pets
            var reminders = await _reminderRepo.FindAsync(r =>
                petIds.Contains(r.PetId) &&
                r.DateTime >= DateTime.UtcNow &&
                !r.IsCompleted
            );

            // Map to DTO
            var result = reminders
                .Select(r => new UpcomingReminderDto
                {
                    ReminderId = r.ReminderId,
                    PetId = r.PetId,
                    PetName = userPets.FirstOrDefault(p => p.PetId == r.PetId)?.Name ?? "",
                    DateTime = r.DateTime,
                    Type = r.Type.ToString(),
                    Description = r.Description,
                    IsCompleted = r.IsCompleted
                })
                .OrderBy(r => r.DateTime)
                .ToList();

            return result;
        }
    }
}
