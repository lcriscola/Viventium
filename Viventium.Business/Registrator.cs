using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Business
{
    public static class BusinessRegistrator
    {
        public static void AddViventiumBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<Infrastructure.ICompanyService, CompanyService>();
        }
    }
}
