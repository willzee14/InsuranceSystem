using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Application.Implementation
{
    public class ClaimsService : ICalimsService
    {
        private readonly IClaimsRepository _claimsRepository;
        public ClaimsService(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }
    }
}
