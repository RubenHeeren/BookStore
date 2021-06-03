using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.UI.Contracts
{
    interface IFileUpload
    {
        public Task UploadFile(IFileListEntry file, MemoryStream memoryStream, string imageName);
        public void RemoveFile(string imageName);
    }
}
