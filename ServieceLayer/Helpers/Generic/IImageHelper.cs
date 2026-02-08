using EntityLayer.Enumerates;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Helpers.Generic
{
    public interface IImageHelper
    {
        public Task<UploadImageModel> UploadImageAsync(string? folderName, IFormFile imageFile, imageType fileType);
        public string DeleteImage(string name);

    }
}
