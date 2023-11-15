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
        Task<int> UpdateClaim(ClaimsDto claimsDto);
        Task<int> InsetClaim(ClaimsDto claimsDto);
        Task<List<ClaimsDto>> GetAllClaims();
        Task<ClaimsDto> GetClaimsByNationalID(ClaimsDto claimsDto);
    }
}
