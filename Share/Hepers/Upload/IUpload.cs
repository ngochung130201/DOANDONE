using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Hepers.Upload
{
    public  interface IUpload
    {
        public  Task<string> UploadFile(string folderName, string modelName, IFormFile formFile);
        public  Task<string> UploadFiles(IFormFile[] formFiles);
        public  Task RemoveImageAsync(string folderName, string imageName);
     //   public  Task<string> UploadFileGlobal(IFormFile[] formFiles);

    }
}
