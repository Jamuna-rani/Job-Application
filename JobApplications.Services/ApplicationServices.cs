using JobApplications.Core.IRepository;
using JobApplications.Core.IServices;
using JobApplications.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplications.Services
{
    public class ApplicationServices: IApplicationService
    {
        private readonly IApplicationRepository ApplicantEntry;
        public ApplicationServices(IApplicationRepository ApplicantDetails)
        {
            ApplicantEntry = ApplicantDetails;
        }
        #region Create
        public void CreateMethod(ApplicantModel applicantDetail)
        {
            ApplicantEntry.CreateMethod(applicantDetail);
        }
        #endregion

        #region Read
        public List<ApplicantModel> ReadMethod()
        {
            return ApplicantEntry.ReadMethod();
        }
        #endregion

        #region Partial
        public ApplicantModel PartialMethod(int applicantId)
        {
            return ApplicantEntry.PartialMethod(applicantId);
        }
        #endregion
    }
}
