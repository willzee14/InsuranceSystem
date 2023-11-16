using InsuranceSystem.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Application.Abstraction
{
    public interface ICalimsService
    {
        Task<ServiceResponse> UpdateClaim(string claimsDto);
        Task<ServiceResponse> InsetClaim(string claimsDto);
        Task<ServiceResponse> GetAllClaims();
        Task<ServiceResponse> GetClaimsByNationalID(string claimsDto);
    }
}
