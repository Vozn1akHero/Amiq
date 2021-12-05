using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Amiq.WebApi.Core.Files
{
    public interface ILocalFileStorage
    {
        Task<LocalFilesStorageUploadResult> UploadFileAsync(IFormFile file);
    }
}
