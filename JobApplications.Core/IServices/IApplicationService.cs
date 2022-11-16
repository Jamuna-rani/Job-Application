using JobApplications.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplications.Core.IServices
{
   public interface IApplicationService
    {
        void CreateMethod(ApplicantModel applicantDetail);
        List<ApplicantModel> ReadMethod();
        ApplicantModel PartialMethod(int applicantId);
    }
}
