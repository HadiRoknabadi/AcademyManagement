using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Utils;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace AcademyManagement.Application.Services.Implementations
{
    public class UploaderService : IUploader
    {
        public async Task<UploadResult> UploadImage(UploadFileType fileType,IFormFile image, string fileName, int? width, int? height, string? deletefileName)
        {
            var client = new RestClient(PathExtension.StaticFileEndPointURL);
            client.Timeout = -1;
            var request = new RestRequest($"/api/upload?fileType={fileType}&fileName={fileName}&width={width}&height={height}&deleteFileName={deletefileName}",Method.POST);

            byte[] bytes;

            using(var ms =new MemoryStream())
            {
                await image.CopyToAsync(ms);
                bytes=ms.ToArray();
            }

            request.AddFile(image.FileName, bytes, image.FileName, image.ContentType);


            var res=client.Post<UploadResultDTO>(request);

            if (res.Data.IsSuccess) return UploadResult.Success;

            return UploadResult.CantUploadFile;
           
        }

        public async Task<UploadResult> UploadPdf(UploadFileType fileType,IFormFile pdf,string fileName,string deletefileName=null)
        {
            var client = new RestClient(PathExtension.StaticFileEndPointURL);
            client.Timeout = -1;
            var request = new RestRequest($"/api/upload?fileType={fileType}&fileName={fileName}&deleteFileName={deletefileName}",Method.POST);

            byte[] bytes;

            using(var ms =new MemoryStream())
            {
                await pdf.CopyToAsync(ms);
                bytes=ms.ToArray();
            }

            request.AddFile(pdf.FileName, bytes, pdf.FileName, pdf.ContentType);


            var res=client.Post<UploadResultDTO>(request);

            if (res.Data.IsSuccess) return UploadResult.Success;

            return UploadResult.CantUploadFile;
        }

    }
}
