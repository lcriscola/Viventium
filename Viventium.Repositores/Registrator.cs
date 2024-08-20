using Microsoft.EntityFrameworkCore;
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
        public static void AddViventiumRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<Infrastructure.IGenericRepository, GenericRepository>();



            services.AddDbContext<Viventium.Repositores.ViventiumDataContext>(b =>
            {
                b.UseSqlServer(connectionString);
            });

        }
    }
}
