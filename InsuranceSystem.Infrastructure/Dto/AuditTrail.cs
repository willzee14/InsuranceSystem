using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Infrastructure.Dtos
{
    public class AuditTrail
    {
        public string? Action { get; set; }
        public string? ClientName { get; set; }
        public DateTime DateGenerated { get; set; }
        public string? IPAddress { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
    }
}
