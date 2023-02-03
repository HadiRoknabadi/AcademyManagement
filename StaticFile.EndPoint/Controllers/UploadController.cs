using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Infrastructure.Uploader;
using AcademyManagement.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;


namespace StaticFile.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        #region Constructor

        private readonly IImageUploaderService _imageUploaderService;

        public UploadController(IImageUploaderService imageUploaderService)
        {
            _imageUploaderService = imageUploaderService;
        }

        #endregion

        [HttpPost]
        public IActionResult Upload(string fileName,int? width,int?height,string? deleteFileName)
        {
            var image = Request.Form.Files[0];
            if (_imageUploaderService.IsImage(image) == false) return BadRequest(new UploadResultDTO
            {
                IsSuccess=false,
                Message="تصویر انتخاب شده قابل بارگذاری نمی باشد"
            });

            if (string.IsNullOrEmpty(deleteFileName))
            {
                _imageUploaderService.AddImageToServer(image, fileName, PathExtension.UserAvatarOriginServer, width, height, PathExtension.UserAvatarThumbServer);

            }
            else
            {
                _imageUploaderService.AddImageToServer(image, fileName, PathExtension.UserAvatarOriginServer, width, height, PathExtension.UserAvatarThumbServer, deleteFileName);

            }

            return Ok(new UploadResultDTO
            {
                IsSuccess=true,
                Message="تصویر با موفقیت آپلود شد"
            });
        }
        
    }
}
