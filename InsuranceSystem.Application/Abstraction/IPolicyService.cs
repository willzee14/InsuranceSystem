using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Application.Abstraction
{
    public interface IPolicyService
    {
        Task<ServiceResponse> InsetPolicy(string policyHolderDto);
        Task<ServiceResponse> UpdatePolicy(string policyHolderDto);

        Task<ServiceResponse> GetPolicies();
        Task<ServiceResponse> GetByPolicyNumber(string policyHolderDto);
    }
}
