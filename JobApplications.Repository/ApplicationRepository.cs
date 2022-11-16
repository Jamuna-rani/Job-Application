
using JobApplications.Core.IRepository;
using JobApplications.Core.Model;
using JobApplications.Entity.Models;
using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static JobApplications.Repository.ApplicationRepository;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace JobApplications.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        #region CreateEntry 
        public void CreateMethod(ApplicantModel applicantDetail)
        {
            using (Job_ApplicationContext entity = new Job_ApplicationContext())
            {
                if (applicantDetail != null)
                {
                    if (applicantDetail.ApplicantId == 0)
                    {
                        Applicant_Details info = new Applicant_Details();
                        info.Applicant_Full_Name = applicantDetail.ApplicantFullName;
                        info.Applicant_Age = applicantDetail.ApplicantAge;
                        info.Applicant_Gender = applicantDetail.ApplicantGender;
                        info.Applicant_Location = applicantDetail.ApplicantLocation;
                        info.Applicant_Email = applicantDetail.ApplicantEmail;
                        info.Applicant_file =applicantDetail.uploadFile;
                        info.Name = applicantDetail.Name;
                        info.Applicant_10th_Mark = int.Parse(applicantDetail.Applicant10thMark);
                        info.Applicant_12th_Mark = int.Parse(applicantDetail.Applicant12thMark);
                        info.Applicant_CGPA = int.Parse(applicantDetail.CGPA);
                        info.Applicant_Interest = applicantDetail.Interest;
                        info.Applicant_Skills = applicantDetail.Skills;
                        entity.Add(info);
                        entity.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region ReadDetails
        public List<ApplicantModel> ReadMethod()
        {
            List<ApplicantModel> ApplicantInfos = new List<ApplicantModel>();
            using (Job_ApplicationContext entities = new Job_ApplicationContext())
            {
                var details = entities.Applicant_Details.Where(i => i.Is_Deleted == false).ToList();
                foreach (var info in details)
                {
                    ApplicantModel data = new ApplicantModel();
                    data.ApplicantId = info.Applicant_Id;
                    data.ApplicantFullName = info.Applicant_Full_Name;
                    data.ApplicantEmail = info.Applicant_Email;
                    data.ApplicantGender = info.Applicant_Gender;
                    data.ApplicantAge = info.Applicant_Age;
                    data.ApplicantLocation = info.Applicant_Location;
                    data.uploadFile = info.Applicant_file.ToString();
                    ApplicantInfos.Add(data);
                }
                return ApplicantInfos;
            }
        }
        #endregion

        #region Partial
        public ApplicantModel PartialMethod(int applicantId)
        {
            ApplicantModel applicantModel = new ApplicantModel();
            using (Job_ApplicationContext entity = new Job_ApplicationContext())
            {
                var data = entity.Applicant_Details.Where(x => x.Applicant_Id == applicantId).SingleOrDefault();
                applicantModel.Name = data.Name;
                applicantModel.Applicant10thMark = data.Applicant_10th_Mark.ToString();
                applicantModel.Applicant12thMark = data.Applicant_12th_Mark.ToString();
                applicantModel.CGPA = data.Applicant_CGPA.ToString();
                applicantModel.Interest = data.Applicant_Interest;
                applicantModel.Skills = data.Applicant_Skills;
            }
            return applicantModel;
        }
        #endregion

    }
}
