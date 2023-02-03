using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManagement.Application.DTOs.Common
{
    public class UploadResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public enum UploadResult
    {
        Success,
        CantUploadImage
    }
}
