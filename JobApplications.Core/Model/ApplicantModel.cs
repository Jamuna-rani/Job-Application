using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplications.Core.Model
{
    public class ApplicantModel
    {
        public int ApplicantId { get; set; }
        public string? ApplicantFullName { get; set; }
        public string? ApplicantGender { get; set; }
        public string? ApplicantEmail { get; set; }
        public int ApplicantAge { get; set; }
        public string? ApplicantLocation { get; set; }
        public string? uploadFile { get; set; }
        public string? Name { get; set; }
        public string? Applicant10thMark { get; set; }
        public string? Applicant12thMark { get; set; }
        public string? CGPA { get; set; }
        public string? Interest { get; set; }
        public string? Skills { get; set; }
        //public IFormFile FileToUpload { get; set; }
        //public byte[] ApplicantFile { get; set; }
    }
}
