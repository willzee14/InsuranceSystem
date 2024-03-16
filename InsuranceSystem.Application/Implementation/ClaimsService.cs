using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Application.Utility;
using InsuranceSystem.Infrastructure.Abstraction;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
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
            if (result.Count == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseData = result, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> GetClaimsByNationalID(string claimsDto)
        {
            var reqObj = RequestHandler.SplitRequest<ClaimsDto>(claimsDto);            

            var claims = new InsuranceSystem.Infrastructure.Dto.ClaimsDto
            {
               
                NationalIDOfPolicyHolder = reqObj.ResponseData.NationalIDOfPolicyHolder                
            };
            Log.Information("About to retrieve all ClaimsByNationalID");
            var result = await _claimsRepository.GetClaimsByNationalID(claims);
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

        public async Task<ServiceResponse> InsetClaim(string claimsDto)
        {
            
            var reqObj = RequestHandler.SplitRequest<ClaimsDto>(claimsDto);            

            var claims = new InsuranceSystem.Infrastructure.Dto.ClaimsDto
            {
                Amount = reqObj.ResponseData.Amount,
                NationalIDOfPolicyHolder = reqObj.ResponseData.NationalIDOfPolicyHolder,
                ClaimsId = reqObj.ResponseData.ClaimsId,                
                DateOfExpense = Convert.ToDateTime(reqObj.ResponseData.DateOfExpense),
                ExpenseId = reqObj.ResponseData.ExpenseId,
            };
            var result = await _claimsRepository.InsetClaim(claims);
            if(result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if(result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> UpdateClaim(string claimsDto)
        {            
            var reqObj = RequestHandler.SplitRequest<ClaimsDto>(claimsDto);
           
            var claims = new InsuranceSystem.Infrastructure.Dto.ClaimsDto
            {
                Amount = reqObj.ResponseData.Amount,
                NationalIDOfPolicyHolder = reqObj.ResponseData.NationalIDOfPolicyHolder,
                ClaimsId = reqObj.ResponseData.ClaimsId,
                DateOfExpense = Convert.ToDateTime(reqObj.ResponseData.DateOfExpense),
                ExpenseId = reqObj.ResponseData.ExpenseId,
            };            
            var result = await _claimsRepository.UpdateClaim(claims);
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
