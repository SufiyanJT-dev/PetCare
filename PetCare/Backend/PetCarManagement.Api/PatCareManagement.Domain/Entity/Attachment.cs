
using System;
using System.ComponentModel.DataAnnotations;


namespace PetCareManagement.Domain.Entity
{

    public class Attachment
    {
        [Key]
        public int AttachId { get; private set; }
        public int MedicalEventId { get; private set; }
        [Required]
        public string FileUrl { get; private set; }

        [Required]
        public string FileName { get; private set; }

      
       
        public string Description { get; private set; }

        public MedicalEvent MedicalEvent { get; private set; }
        

      
        private Attachment() { }
        public Attachment(int medicalEventId,string fileUrl, string fileName, string description)
        {
            MedicalEventId = medicalEventId;
              FileUrl = fileUrl;
            FileName = fileName;
            Description = description;
        }
        public void Update(int medicalEventId, string fileUrl, string fileName, string description)
        {
            MedicalEventId = medicalEventId;
            FileUrl = fileUrl;
            FileName = fileName;
            Description = description;
        }

    }
}
