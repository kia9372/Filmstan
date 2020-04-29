using Common.Operation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UploadUtility
{
    public static class UploadUtiltie
    {
        public static async Task<OperationResult<string>> UploadInCustomePath(IFormFile file, string formatextExtention, string fileName, params string[] Paths)
        {
            if (file.Length > 0)
            {
                var name = fileName + formatextExtention;
                if (!Directory.Exists(UploadFolderPath.PathUploadFolder()))
                {
                    Directory.CreateDirectory(UploadFolderPath.PathUploadFolder());
                }
                foreach (var item in Paths)
                {
                    if (!Directory.Exists(item))
                    {
                        Directory.CreateDirectory(Paths.Last());
                    }
                }

                var result = await Upload(file, name, Paths.Last());
                if (result.Success)
                {
                    return OperationResult<string>.BuildSuccessResult(result.Result);
                }
            }
            return OperationResult<string>.BuildFailure("File Not Exist");
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        private static async Task<OperationResult<string>> Upload(IFormFile file, string fileName, string path)
        {
            try
            {
                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return OperationResult<string>.BuildSuccessResult(fileName);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<string>.BuildFailure(ex.Message);
            }
        }


    }
}
