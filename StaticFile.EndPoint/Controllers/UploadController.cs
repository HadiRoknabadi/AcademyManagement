using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Application.Utils;
using AcademyManagement.Infrastructure.Uploader;
using Microsoft.AspNetCore.Mvc;


namespace StaticFile.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        #region Constructor

        private readonly IImageUploaderService _imageUploaderService;
        private readonly IPdfUploaderService _pdfUploaderService;

        public UploadController(IImageUploaderService imageUploaderService, IPdfUploaderService pdfUploaderService)
        {
            _imageUploaderService = imageUploaderService;
            _pdfUploaderService = pdfUploaderService;
        }



        #endregion

        [HttpPost]
        public IActionResult Upload(UploadFileType fileType, string fileName, int? width, int? height, string? deleteFileName)
        {
            switch (fileType)
            {
                case UploadFileType.Image:
                    var image = Request.Form.Files[0];
                    if (_imageUploaderService.IsImage(image) == false) return BadRequest(new UploadResultDTO
                    {
                        IsSuccess = false,
                        Message = "فایل انتخاب شده قابل بارگذاری نمی باشد"
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
                        IsSuccess = true,
                        Message = "فایل با موفقیت آپلود شد"
                    });

                case UploadFileType.PDF:
                var pdf = Request.Form.Files[0];
                    if (_pdfUploaderService.IsPdf(pdf) == false) return BadRequest(new UploadResultDTO
                    {
                        IsSuccess = false,
                        Message = "فایل انتخاب شده قابل بارگذاری نمی باشد"
                    });
                    if (string.IsNullOrEmpty(deleteFileName))
                    {
                        _pdfUploaderService.AddPdfToServer(pdf, fileName, PathExtension.PdfLesson);

                    }
                    else
                    {
                        _pdfUploaderService.AddPdfToServer(pdf, fileName, PathExtension.PdfLessonServer, deleteFileName);

                    }
                    return Ok(new UploadResultDTO
                    {
                        IsSuccess = true,
                        Message = "فایل با موفقیت آپلود شد"
                    });

                    default :
                    return BadRequest(new UploadResultDTO
                    {
                        IsSuccess = false,
                        Message = "خطایی رخ داد"
                    });

            }



        }

    }
}
