using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Amiq.Static.Core.Files
{
    public interface ILocalFileStorage
    {
        Task<LocalFilesStorageUploadResult> UploadFileAsync(IFormFile file);
    }
}
