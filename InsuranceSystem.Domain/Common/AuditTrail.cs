using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Domain.Common
{
    public class AuditTrail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Action { get; set; }
        public string? ClientName { get; set; }
        public DateTime DateGenerated { get; set; }
        public string? IPAddress { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
    }
}
