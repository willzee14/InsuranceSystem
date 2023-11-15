using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Infrastructure.Abstraction
{
    public interface IClaimsRepository
    {
        Task<int> InsetAudit(AuditTrail auditTrail);
    }
}
