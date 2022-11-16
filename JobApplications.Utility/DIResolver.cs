using JobApplications.Core.IRepository;
using JobApplications.Core.IServices;
using JobApplications.Repository;
using JobApplications.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplications.Utility
{
  public class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Http context
            //for accessing the http context by interface in view level
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Service
            //for service accesssing
            services.AddScoped<IApplicationService, ApplicationServices>();
            #endregion

            #region Repository
            //for Repository accessing 
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            #endregion
        }
    }
}

