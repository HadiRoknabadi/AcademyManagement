using FileSignatures;
using FileSignatures.Formats;
using Microsoft.AspNetCore.Http;

namespace AcademyManagement.Infrastructure.Uploader
{
    public class PdfUploaderService:IPdfUploaderService
    {
        public bool AddPdfToServer(IFormFile pdf, string fileName, string orginalPath, string deletefileName = null)
        {
            if (pdf != null && IsPdf(pdf))
            {
                if (!Directory.Exists(orginalPath))
                    Directory.CreateDirectory(orginalPath);

                if (!string.IsNullOrEmpty(deletefileName))
                {
                    
                    DeletePdf(deletefileName,orginalPath);
                }

                string OriginPath = orginalPath + fileName;

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath)) pdf.CopyTo(stream);
                }



                return true;
            }
            return false;
        }
        public void DeletePdf(string pdfName, string OriginPath)
        {
            if (!string.IsNullOrEmpty(pdfName))
            {
                if (File.Exists(OriginPath + pdfName))
                    File.Delete(OriginPath + pdfName);
            }
        }
        public bool IsPdf(IFormFile postedFile)
        {
             FileFormat format;
            var inspector = new FileFormatInspector();
            using (var stream = postedFile.OpenReadStream())
            {
                format = inspector.DetermineFileFormat(stream);
            }
            if (format is Pdf)
            {
                return true;


            }
            return false;
        }
    }
}