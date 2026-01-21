using EntityLayer.Enumerates;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Generic
{
    public class ImageHelper : IImageHelper
    {
        public IHostEnvironment _environment;
        public string wwwroot;

        public ImageHelper(IHostEnvironment environment, string wwwroot)
        {
            _environment = environment;
            this.wwwroot = _environment.ContentRootPath +"wwroot/";
        }
        public const string imageFolder = "images"; 
        public const string identityFolder = "identity"; 
        public const string aboutFolder = "about"; 
        public const string portifolioFolder = "portifolios"; 
        public const string teamFolder = "team"; 
        public const string testimonialsFolder = "testimonials"; 

        public async Task<UploadImageModel> UploadImage(string name, string folderName, IFormFile imageFile, imageType imageType)
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
            
            if (!Directory.Exists($"{wwwroot}/{imageFolder}/{folderName}"))
                Directory.CreateDirectory($"{wwwroot}/{imageFolder}/{folderName}");

            // check for jpg or jpeg or png
            string fileExtention = Path.Combine(imageFile.Name).ToLower();
            if (fileExtention == null && fileExtention != "jpg" || fileExtention != "jpeg"||fileExtention != "png")
                return  new UploadImageModel { Error = "Photo Must be in jpg or jpeg or png" };

            // get file name
            DateTime currentDate = DateTime.UtcNow;
            string newFileName = folderName + "_" + currentDate.Microsecond.ToString();


            string path = Path.Combine($"{wwwroot}/{imageFolder}/{folderName}", newFileName);
            // open file stream and dispose
            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);
            // copy image to server
            await imageFile.CopyToAsync(stream);
            // Make sure that Buffer is Empty
            await stream.FlushAsync();
            
            return new UploadImageModel {FileName = newFileName,FileType= imageFile.ContentType};
        }
    }
}
