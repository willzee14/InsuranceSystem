using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Infrastructure.Abstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#pragma warning disable


namespace InsuranceSystem.Application.Implementation
{
    public class ClaimsService : ICalimsService
    {
        private readonly IClaimsRepository _claimsRepository;
        private readonly ServiceResponseSettings _serviceResponseSettings;
        public ClaimsService(IClaimsRepository claimsRepository, IOptions<ServiceResponseSettings> serviceResponseSettings)
        {
            _claimsRepository = claimsRepository;
            _serviceResponseSettings = serviceResponseSettings.Value;

        }

        public async Task<ServiceResponse> GetAllClaims()
        {
            Log.Information("About to retrieve all claims");
            var result = await _claimsRepository.GetAllClaims();
            Log.Information($"response from getall claims: {JsonConvert.SerializeObject(result)}");
            if (result == null)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if(result.Count == 0)
            {
                return new ServiceResponse() {ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> GetClaimsByNationalID(ClaimsDto claimsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> InsetClaim(ClaimsDto claimsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> UpdateClaim(ClaimsDto claimsDto)
        {
            throw new NotImplementedException();
        }
    }
}
