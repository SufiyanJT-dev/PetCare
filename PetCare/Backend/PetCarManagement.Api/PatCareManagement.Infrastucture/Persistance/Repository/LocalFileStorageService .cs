using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;


namespace PetCareManagement.Infrastucture.Persistance.Repository
{

    public class LocalFileStorageService : PetCareManagement.Application.IRepository.IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService()
        {

            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "attachments");
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Ensure unique filename
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_basePath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return $"/uploads/attachments/{uniqueFileName}";
        }
    }
}