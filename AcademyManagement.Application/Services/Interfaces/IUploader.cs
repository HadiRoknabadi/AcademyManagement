using AcademyManagement.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface IUploader
    {
        Task<UploadResult> UploadImage(UploadFileType fileType,IFormFile image, string fileName, int? width, int? height, string deletefileName = null);
        Task<UploadResult> UploadPdf(UploadFileType fileType,IFormFile pdf,string fileName,string deletefileName=null);
    }
}
