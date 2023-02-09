using Microsoft.AspNetCore.Http;

namespace AcademyManagement.Infrastructure.Uploader
{
    public interface IPdfUploaderService
    {
        bool AddPdfToServer(IFormFile pdf, string fileName, string orginalPath, string deletefileName = null);
        void DeletePdf(string pdfName, string OriginPath);
        bool IsPdf(IFormFile postedFile);
    }
}