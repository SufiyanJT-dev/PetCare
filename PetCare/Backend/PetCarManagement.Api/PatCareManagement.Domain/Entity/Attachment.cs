
using System;
using System.ComponentModel.DataAnnotations;


namespace PetCareManagement.Domain.Entity
{

    public class Attachment
    {
        [Key]
        public int AttachId { get; private set; }

        [Required]
        public string FileUrl { get; private set; }

        [Required]
        public string FileName { get; private set; }

      
       
        public string Description { get; private set; }

        private readonly List<EventAttachment> _links = new();
        public IReadOnlyCollection<EventAttachment> Links => _links.AsReadOnly();

      
        private Attachment() { }
        public Attachment(string fileUrl, string fileName, string description)
        {
          
            FileUrl = fileUrl;
            FileName = fileName;
            Description = description;
        }

    }
}
