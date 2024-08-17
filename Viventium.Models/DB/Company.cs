using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Models.DB
{
    [Table("Companies")]
    public class Company
    {
        [Key]
        public required int CompanyId { get; set; }

        public required string Code { get; set; }

        public required string Description { get; set; }
    }
}
