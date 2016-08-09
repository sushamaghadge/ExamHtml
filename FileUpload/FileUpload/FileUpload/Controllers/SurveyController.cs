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
    public class SurveyController : ApiController
    {
        //HiltonAvayaEntities db = new HiltonAvayaEntities();

        [HttpPost]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            Debug.WriteLine(httpRequest.ApplicationPath);
            if (httpRequest.Files.Count > 0 && httpRequest.Files.Count == 4)
            {
                if (String.IsNullOrWhiteSpace((String)httpRequest.Form["file1HHID"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file2HHID"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file3HHID"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file4HHID"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file1Description"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file2Description"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file3Description"]) ||
                    String.IsNullOrWhiteSpace((String)httpRequest.Form["file4Description"]))
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    var docfiles = new List<string>();
                    int index = 1;

                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string fileNameFromPath = (String)httpRequest.Form["file" + index + "HHID"] + ".wav";
                        String filePath;

#if DEBUG
                        filePath = HttpContext.Current.Server.MapPath(Path.Combine("~/UploadedFiles/" + fileNameFromPath));
#else
                    filePath = @"\\dalrsinfoweb1dev\AvayaWebApi\Survey\" + fileNameFromPath;
#endif

                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                        //FileUpload fUpload = new FileUpload()
                        //{
                        //    HHIDNumber = (String)httpRequest.Form["file" + indx + "HHID"],
                        //    Description = (String)httpRequest.Form["file" + indx + "Description"],
                        //    AddedOn = DateTime.Now
                        //};

                        //db.FileUploads.Add(fUpload);
                        //db.SaveChanges();

                        index++;
                    }

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