using System;

namespace PetCareManagement.Domain.Entity
{
    public class Documents
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }

        public string Title { get; set; }
        public string DocType { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        public Pets Pet { get; set; }
    }
}
