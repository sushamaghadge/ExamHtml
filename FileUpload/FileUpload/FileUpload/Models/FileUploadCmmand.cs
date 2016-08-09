using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUpload.Models
{
    public class FileUploadCmmand
    {
        public HttpPostedFile InputFile { get; set; }
        public String HHID { get; set; }
        public String Description { get; set; }
        public String UploadedURL { get; set; }
    }
}