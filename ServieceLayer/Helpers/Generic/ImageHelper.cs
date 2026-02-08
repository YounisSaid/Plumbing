using EntityLayer.Enumerates;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Generic
{
    public class ImageHelper : IImageHelper
    {
        private IHostEnvironment _environment;
        private readonly string _wwwroot;

        public ImageHelper(IHostEnvironment environment)
        {
            _environment = environment;
            this._wwwroot = Path.Combine(_environment.ContentRootPath, "wwwroot");
        }
        public const string imageFolder = "images"; 
        public const string identityFolder = "identity"; 
        public const string aboutFolder = "about"; 
        public const string portifolioFolder = "portifolios"; 
        public const string teamFolder = "team"; 
        public const string testimonialsFolder = "testimonials"; 

        public async Task<UploadImageModel> UploadImageAsync(string? folderName, IFormFile imageFile, imageType imageType)
        {
            // write new folderName if not exists
            if (folderName is null)
            {
                switch (imageType)
                {
                    case imageType.identity:
                        folderName = identityFolder; break;
                    case imageType.about:
                        folderName = aboutFolder; break;
                    case imageType.portifolio:
                        folderName = portifolioFolder; break;
                    case imageType.team:
                        folderName = teamFolder; break;
                    case imageType.testimonials:
                        folderName = testimonialsFolder; break;
                }   
            }
            
            if (!Directory.Exists($"{_wwwroot}/{imageFolder}/{folderName}"))
                Directory.CreateDirectory($"{_wwwroot}/{imageFolder}/{folderName}");

            // check for jpg or jpeg or png
            string fileExtention = Path.GetExtension(imageFile.FileName).ToLower();
            if (fileExtention == null || (fileExtention != ".jpg" && fileExtention != ".jpeg" && fileExtention != ".png"))
                return  new UploadImageModel { Error = "Photo Must be in jpg or jpeg or png and cannot be Null" };

            // get file name
            DateTime currentDate = DateTime.UtcNow;
            string newFileName = folderName + "_" + currentDate.Microsecond.ToString() + fileExtention;


            string path = Path.Combine($"{_wwwroot}/{imageFolder}/{folderName}", newFileName);
            // open file stream and dispose
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
            // copy image to server
            await imageFile.CopyToAsync(stream);
            // Make sure that Buffer is Empty
            await stream.FlushAsync();
            
            return new UploadImageModel {FileName = $"{folderName}/{newFileName}",FileType= imageFile.ContentType};
        }

        public string DeleteImage(string name)
        {
            var imageToDelete = Path.Combine($"{_wwwroot}/{imageFolder}/{name}");
            if(File.Exists(imageToDelete))
                File.Delete(imageToDelete);

            return "Image is Deleted Succsessfuly";
        }
    }
}
