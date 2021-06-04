using BlazorInputFile;
using BookStore.UI.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.UI.Services
{
    //public class FileUpload : IFileUpload
    //{
    //    private readonly IWebHostEnvironment _webHostEnvironment;

    //    public FileUpload(IWebHostEnvironment webHostEnvironment)
    //    {
    //        _webHostEnvironment = webHostEnvironment;
    //    }

    //    public void RemoveFile(string imageName)
    //    {
    //        var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{imageName}";

    //        if (File.Exists(path))
    //        {
    //            File.Delete(path);
    //        }
    //    }

    //    public async Task UploadFile(IFileListEntry file, MemoryStream memoryStream, string imageName)
    //    {
    //        try
    //        {
    //            await file.Data.CopyToAsync(memoryStream);

    //            // _webHostEnvironment.WebRootPath = wwwhost in web host 
    //            var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{imageName}";

    //            using (FileStream fileStream = new FileStream(path, FileMode.Create))
    //            {
    //                memoryStream.WriteTo(fileStream);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw;
    //        }
    //    }
    //}
}
