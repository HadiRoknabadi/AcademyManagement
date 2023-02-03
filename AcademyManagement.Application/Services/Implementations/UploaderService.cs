using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace AcademyManagement.Application.Services.Implementations
{
    public class UploaderService : IUploader
    {
        public async Task<UploadResult> UploadImage(IFormFile image, string fileName, int? width, int? height, string? deletefileName)
        {
            var client = new RestClient(PathExtension.StaticFileUploaderURL);
            client.Timeout = -1;
            var request = new RestRequest($"/api/upload?fileName={fileName}&width={width}&height={height}&deleteFileName={deletefileName}",Method.POST);

            byte[] bytes;

            using(var ms =new MemoryStream())
            {
                await image.CopyToAsync(ms);
                bytes=ms.ToArray();
            }

            request.AddFile(image.FileName, bytes, image.FileName, image.ContentType);


            var res=client.Post<UploadResultDTO>(request);

            if (res.Data.IsSuccess) return UploadResult.Success;

            return UploadResult.CantUploadImage;
           
        }
    }
}
