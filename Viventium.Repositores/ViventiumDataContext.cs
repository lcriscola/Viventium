using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Repositores
{
    public class ViventiumDataContext: DbContext
    {
        public ViventiumDataContext(DbContextOptions<ViventiumDataContext> options):base(options)
        {
                
        }

        public ViventiumDataContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.DB.Employee>()
                    .HasKey(x=> new {x.CompanyId, x.EmployeeNumber })
                    ;
 

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Models.DB.Company> Companies { get; set; }
        public virtual DbSet<Models.DB.Employee> Employees{ get; set; }
    }
}
