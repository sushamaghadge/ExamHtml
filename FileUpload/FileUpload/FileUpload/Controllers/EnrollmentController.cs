using FileUpload.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FileUpload.Controllers
{
    public class EnrollmentController : ApiController
    {
        //HiltonAvayaEntities db = new HiltonAvayaEntities();

        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            Debug.WriteLine(httpRequest.ApplicationPath);

            if (httpRequest.Files.Count > 0 && httpRequest.Files.Count == 1)
            {
                if (String.IsNullOrWhiteSpace((String)httpRequest.Form["fileHHID"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["fileDescription"]))
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    var docfiles = new List<string>();

                    var postedFile = httpRequest.Files[0];
                    string fileFromPath = (String)httpRequest.Form["fileHHID"] + ".wav";
                    String filePath;

#if DEBUG
                    filePath = HttpContext.Current.Server.MapPath(Path.Combine("~/UploadedFiles/" + fileFromPath));
#else
                    filePath = @"\\dalrsinfoweb1dev\AvayaWebApi\Enrollment\" + fileFromPath;
#endif
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);

                    //FileUpload fUpload = new FileUpload()
                    //{
                    //    HHIDNumber = (String)httpRequest.Form["fileHHID"],
                    //    Description = (String)httpRequest.Form["fileDescription"],
                    //    AddedOn = DateTime.Now
                    //};

                    //db.FileUploads.Add(fUpload);
                    //db.SaveChanges();

                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;

        }
    }
}
