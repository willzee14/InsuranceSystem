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
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> GetClaimsByNationalID(string claimsDto)
        {
            var reqObj = RequestHandler.SplitRequest(claimsDto);
            if (reqObj.ResponseCode != "00")
            {
                return reqObj;
            }
            var jsonReq = reqObj.ResponseData as InsuranceSystem.Infrastructure.Dto.ClaimsDto;
            
            Log.Information("About to retrieve all ClaimsByNationalID");
            var result = await _claimsRepository.GetClaimsByNationalID(jsonReq);
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
            //var claims = new InsuranceSystem.Infrastructure.Dto.ClaimsDto
            //{
            //    Amount = claimsDto.Amount,
            //    NationalIDOfPolicyHolder = claimsDto.NationalIDOfPolicyHolder,
            //    ClaimsId = claimsDto.ClaimsId,
            //    ClaimStatus = Infrastructure.Enum.ClaimStatus.Submitted,
            //    DateOfExpense = claimsDto.DateOfExpense,
            //    ExpenseId = claimsDto.ExpenseId,
            //};

            var reqObj = RequestHandler.SplitRequest(claimsDto);
            if (reqObj.ResponseCode != "00")
            {
                return reqObj;
            }
            var jsonReq = reqObj.ResponseData as InsuranceSystem.Infrastructure.Dto.ClaimsDto;
            var result = await _claimsRepository.InsetClaim(jsonReq);
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
            //var claims = new InsuranceSystem.Infrastructure.Dto.ClaimsDto
            //{
            //    Amount = claimsDto.Amount,
            //    NationalIDOfPolicyHolder = claimsDto.NationalIDOfPolicyHolder,
            //    ClaimsId = claimsDto.ClaimsId,
            //    ClaimStatus = Infrastructure.Enum.ClaimStatus.Submitted,
            //    DateOfExpense = claimsDto.DateOfExpense,
            //    ExpenseId = claimsDto.ExpenseId,
            //};
            var reqObj = RequestHandler.SplitRequest(claimsDto);
            if (reqObj.ResponseCode != "00")
            {
                return reqObj;
            }
            var jsonReq = reqObj.ResponseData as InsuranceSystem.Infrastructure.Dto.ClaimsDto;
            var result = await _claimsRepository.UpdateClaim(jsonReq);
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
