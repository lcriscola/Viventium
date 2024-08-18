using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Repositores
{
    public static class RepositoryRegistrator
    {
        public static void AddViventiumRepositories(this IServiceCollection services)
        {
            services.AddScoped<Infrastructure.IGenericRepository, GenericRepository>();
        }
    }
}
