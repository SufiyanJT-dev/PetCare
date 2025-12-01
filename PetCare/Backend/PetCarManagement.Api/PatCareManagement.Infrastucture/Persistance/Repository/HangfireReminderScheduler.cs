using Hangfire;
using PetCareManagement.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Infrastucture.Persistance.Repository
{
    public class HangfireReminderScheduler : IReminderScheduler
    {
        public string ScheduleReminder(int reminderId, DateTime dateTime)
        {
           return BackgroundJob.Schedule<ReminderEmailService>(
                svc => svc.SendReminderEmail(reminderId),
                dateTime
            );
        }

        public string EnqueueReminder(int reminderId)
        {
            return BackgroundJob.Enqueue<ReminderEmailService>(
                svc => svc.SendReminderEmail(reminderId)
            );
        }
        public void CancelReminder(string jobId)
        {
            BackgroundJob.Delete(jobId);
             
        }
    }
    }
