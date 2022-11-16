using Elmah;
using Elmah.ContentSyndication;
using ExcelDataReader;
using JobApplications.Core.IServices;
using JobApplications.Core.Model;
using JobApplications.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System.Linq;
using System.Text.RegularExpressions;

namespace JobApplication.Controllers
{
    public class JobApplicantController : Controller
    {
        private readonly IApplicationService _ApplicantDetail;
        public JobApplicantController(IApplicationService _ApplicantDetail)
        {

            this._ApplicantDetail = _ApplicantDetail;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region CREATE AND UPLOAD
        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateForm(IFormFile file, ApplicantModel applicantDetail)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        System.IO.Directory.GetCurrentDirectory(), "wwwroot\\Files",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            applicantDetail.uploadFile = path;
            var i = 0;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        applicantDetail.Name = (reader.GetValue(0) != null) ? reader.GetValue(0).ToString() : " ";
                        applicantDetail.Applicant10thMark = (reader.GetValue(1) != null) ? reader.GetValue(1).ToString() : " ";
                        applicantDetail.Applicant12thMark = (reader.GetValue(2) != null) ? reader.GetValue(2).ToString() : " ";
                        applicantDetail.CGPA = (reader.GetValue(3) != null) ? reader.GetValue(3).ToString() : " ";
                        applicantDetail.Interest = (reader.GetValue(4) != null) ? reader.GetValue(4).ToString() : " ";
                        applicantDetail.Skills = (reader.GetValue(5) != null) ? reader.GetValue(5).ToString() : " ";
                    }
                    if (applicantDetail.Applicant10thMark != " ")
                    {
                        if (!IsNumber(applicantDetail.Applicant10thMark))
                        {
                            i++;
                            ViewBag.Message1 = "Invalid 10th Mark in excel.";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message1 = " Field 10th Mark in excel is empty.";
                    }

                    if (applicantDetail.Name != " ")
                    {
                        if (!IsLetter(applicantDetail.Name))
                        {
                            i++;
                            ViewBag.Message2 = "Invalid Name in excel.";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message2 = " Field Name in excel is empty.";
                    }

                    if (applicantDetail.Applicant12thMark != " ")
                    {
                        if (!IsNumber(applicantDetail.Applicant12thMark))
                        {
                            i++;
                            ViewBag.Message3 = "Invalid 12th Mark in excel.";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message3 = " Field 10th Mark in excel is empty.";
                    }
                    if (applicantDetail.CGPA != " ")
                    {
                        if (!IsNumber(applicantDetail.CGPA))
                        {
                            i++;
                            ViewBag.Message4 = "Invalid CGPA in excel.";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message4 = " Field cgpa in excel is empty.";
                    }
                    if (applicantDetail.Interest != " ")
                    {
                        if (!IsLetter(applicantDetail.Interest))
                        {
                            i++;
                            ViewBag.Message5 = "Invalid Interest in excel.";
                        }
                    }
                    else
                    {
                        i++;
                        ViewBag.Message5 = " Field Interest in excel is empty.";
                    }
                    if (applicantDetail.Skills == " ")
                    {
                        i++;
                        ViewBag.Message6 = " skill is null in excel.";
                    }
                    if (i == 0)
                    {
                        _ApplicantDetail.CreateMethod(applicantDetail);
                        return RedirectToAction("ReadDetails");
                    }
                }
            }

            return View();

        }

        private bool IsNumber(string value)
        {
            return value.All(char.IsDigit);
        }

        private bool IsLetter(string value)
        {
            Regex regexLetter = new Regex("[a-zA-Z ]$");
            return regexLetter.IsMatch(value);
        }
        #endregion

        #region READ
        [HttpGet]
        public IActionResult ReadDetails()
        {
            var info = _ApplicantDetail.ReadMethod();
            return View(info);
        }
        #endregion

        #region PARTIAL
        public IActionResult _DetailPartial(int applicantId)

        {
            var data = _ApplicantDetail.PartialMethod(applicantId);
            return PartialView(data);
        }
        #endregion

        #region DOWNLOAD
        public IActionResult Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
            };
        }
        #endregion
    }

}
