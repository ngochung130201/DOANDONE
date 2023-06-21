using Microsoft.AspNetCore.Http;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Share.Hepers.Upload
{
    public static class UploadImage 
    {
       
        public static async Task<string> UploadFile(string folderName,string modelName, IFormFile formFile)
        {
            var pathImage = "";

            if (formFile.Length > 0)
            {
                var path =  Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{modelName}", $"{folderName}", formFile.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await formFile.CopyToAsync(stream);

                }
                pathImage = "wwwroot" + "/Images/" + $"{folderName}/" + formFile.FileName;

            }
            return pathImage;
        }

        public static async Task<string> UploadFiles(IFormFile[] formFiles)
        {
            var pathImage = "";

            if (formFiles.Length > 0)
            {
                foreach (var file in formFiles)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images", file.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);

                    }
                    pathImage += "/Images/" + file.FileName + ";";
                }

            }
            return pathImage;
        }

        public static async Task RemoveImageAsync(string folderName, string imageName)
        {
            var folderPath = Path.Combine("wwwroot", "uploads", $"{folderName}");
            var pathToRemove = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            var filePath = Path.Combine(pathToRemove, imageName);
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
