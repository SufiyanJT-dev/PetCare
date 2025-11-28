using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareManagement.Domain.Entity
{
    public class EventAttachment
    {
        [Key]
        public int LinkId { get; private set; }

        [Required]
        public int EventId { get; private set; }

        [Required]
        public int AttachId { get; private set; }

      
        public MedicalEvent? MedicalEvent { get; private set; }
        public Attachment? Attachment { get; private set; }


        private EventAttachment() { }
        public EventAttachment(int eventId, int attachId)
        {
            
            EventId = eventId;
            AttachId = attachId;
        }
    }
}
