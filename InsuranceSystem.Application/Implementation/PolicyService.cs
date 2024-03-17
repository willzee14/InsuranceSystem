using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Application.Utility;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Infrastructure.Dto;
using InsuranceSystem.Infrastructure.Implementation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Application.Implementation
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;
        private readonly ServiceResponseSettings _serviceResponseSettings;
        public PolicyService(IPolicyHolderRepository policyHolderRepository, IOptions<ServiceResponseSettings> serviceResponseSettings)
        {
            _policyHolderRepository = policyHolderRepository;
            _serviceResponseSettings = serviceResponseSettings.Value;
        }
        public async Task<ServiceResponse> GetByPolicyNumber(string policyHolderDto)
        {
            var reqObj = RequestHandler.SplitRequest<PolicyHolderDto>(policyHolderDto);           

            Log.Information("About to retrieve all ClaimsByNationalID");
            var result = await _policyHolderRepository.GetByPolicyNumber(reqObj.ResponseData);
            Log.Information($"response from getall claims: {JsonConvert.SerializeObject(result)}");
            if (result == null)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> GetPolicies()
        {           

            Log.Information("About to retrieve all Claims");
            var result = await _policyHolderRepository.GetPolicies();
            Log.Information($"response from getall claims: {JsonConvert.SerializeObject(result)}");
            if (result.Count == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseData = result, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> InsetPolicy(string policyHolderDto)
        {
            var reqObj = RequestHandler.SplitRequest<PolicyHolderDto>(policyHolderDto);
           
            var result = await _policyHolderRepository.InsetPolicy(reqObj.ResponseData);
            if (result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if (result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> UpdatePolicy(string policyHolderDto)
        {
            var reqObj = RequestHandler.SplitRequest<PolicyHolderDto>(policyHolderDto);
            
            var result = await _policyHolderRepository.UpdatePolicy(reqObj.ResponseData);
            if (result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if (result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }
    }
}
