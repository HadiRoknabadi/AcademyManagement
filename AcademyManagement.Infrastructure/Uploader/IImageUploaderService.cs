using Microsoft.AspNetCore.Http;

namespace AcademyManagement.Infrastructure.Uploader
{
    public interface IImageUploaderService
    {
        bool AddImageToServer(IFormFile image, string fileName, string orginalPath, int? width, int? height, string thumbPath = null, string deletefileName = null);
        void DeleteImage(string imageName, string OriginPath, string ThumbPath = null);
        bool IsImage(IFormFile postedFile);
        void ImageResizer(string inputImagePath, string outputImagePath, int? width, int? height);
    }
}
