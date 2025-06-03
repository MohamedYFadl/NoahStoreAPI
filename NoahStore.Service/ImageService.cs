using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using NoahStore.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Service
{
    public class ImageService : IImageService
    {
        private readonly IFileProvider fileProvider;

        public ImageService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string productName)
        {
           List<string> SaveImages = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", productName);
            if(!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (IFormFile item in files)
            {
                if(item.Length > 0)
                {
                    var ImageName = item.FileName;
                    var ImageSrc = $"/Images/{productName}/{ImageName}";
                    var  root = Path.Combine(ImageDirectory, ImageName);
                    using(FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImages.Add(ImageSrc);
                }
            }
            return SaveImages;
        }

        public void DeleteImageAsync(string imagePath)
        {
            var info = fileProvider.GetFileInfo(imagePath);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
