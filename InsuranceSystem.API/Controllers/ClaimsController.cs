using InsuranceSystem.API.Extensions;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
#pragma warning disable

namespace InsuranceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICalimsService _calimsService;
        private readonly ServiceResponseSettings _serviceResponseSettings;
        public ClaimsController(ICalimsService calimsService, IHttpContextAccessor contextAccessor, IOptions<ServiceResponseSettings> serviceResponseSettings)
        {
            _calimsService = calimsService;
            _contextAccessor = contextAccessor;
            _serviceResponseSettings = serviceResponseSettings.Value;

        }

        [HttpPost("InsetClaim")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> InsetClaim([FromBody] EncryptClass data)
        {
            try
            {
                var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
                var result = await _calimsService.InsetClaim(reslt);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Information($"Error at InsetClaim: {ex}");
                return BadRequest(new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage });
            }
        }

        [HttpPut("UpdateClaim")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> UpdateClaim([FromBody] EncryptClass data)
        {
            try
            {
                var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
                var result = await _calimsService.UpdateClaim(reslt);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Information($"Error at UpdateClaim: {ex}");
                return BadRequest(new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage });
            }
        }

        [HttpPost("ClaimsByNationalID")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> ClaimsByNationalID([FromBody] EncryptClass data)
        {
            try
            {
                var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
                var result = await _calimsService.GetClaimsByNationalID(reslt);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Information($"Error at ClaimsByNationalID: {ex}");
                return BadRequest(new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage });
            }
        }

        [HttpGet("Claims")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> Claims()
        {
            try
            {
                var result = await _calimsService.GetAllClaims();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Information($"Error at Claims: {ex}");
                return BadRequest(new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage });
            }

        }
    }
}
