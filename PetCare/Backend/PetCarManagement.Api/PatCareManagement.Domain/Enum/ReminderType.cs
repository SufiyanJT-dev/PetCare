using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Domain.Enum
{
    public enum ReminderType
    {
        Appointment=0,
        Medication=1,
        Vaccination=2,
        MedicalEvents=3,
        Custom=4
    }
}
