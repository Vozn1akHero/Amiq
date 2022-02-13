using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Amiq.Static.Core.Files
{
    public class LocalFilesStorage : ILocalFileStorage
    {
        private IWebHostEnvironment _webHostEnvironment;
        const string DIRECTORY = "wwwroot";

        public LocalFilesStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<LocalFilesStorageUploadResult> UploadFileAsync(IFormFile file)
        {
            LocalFilesStorageUploadResult localFilesStorageOperation = new();
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            //var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            localFilesStorageOperation.IsSuccess = true;
            localFilesStorageOperation.GeneratedFileName = fileName;
            return localFilesStorageOperation;
        }
    }
}
