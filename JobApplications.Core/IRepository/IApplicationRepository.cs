using JobApplications.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplications.Core.IRepository
{
   public interface IApplicationRepository
    {
        void CreateMethod(ApplicantModel applicantDetail);
        List<ApplicantModel> ReadMethod();
        ApplicantModel PartialMethod(int applicantId);
    }
}
