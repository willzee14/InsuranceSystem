using InsuranceSystem.Infrastructure.Dto;
using InsuranceSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Infrastructure.Abstraction
{
    public interface IPolicyHolderRepository
    {
        Task<int> InsetPolicy(PolicyHolderDto policyHolderDto);
        Task<int> UpdatePolicy(PolicyHolderDto policyHolderDto);
        
        Task<List<PolicyHolderDto>> GetPolicies();
        Task<PolicyHolderDto> GetByPolicyNumber(PolicyHolderDto policyHolderDto);
    }
}
