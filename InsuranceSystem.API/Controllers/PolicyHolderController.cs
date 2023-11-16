using InsuranceSystem.API.Extensions;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Application.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
#pragma warning disable

namespace InsuranceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPolicyService _policyService;
        private readonly ServiceResponseSettings _serviceResponseSettings;
        public PolicyHolderController(IPolicyService policyService, IHttpContextAccessor contextAccessor, IOptions<ServiceResponseSettings> serviceResponseSettings)
        {
            _policyService = policyService;
            _contextAccessor = contextAccessor;
            _serviceResponseSettings = serviceResponseSettings.Value;

        }

        [HttpPost("InsetPolicy")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> InsetPolicy([FromBody] EncryptClass data)
        {
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _policyService.InsetPolicy(reslt);

            if (result.ResponseCode.Equals(_serviceResponseSettings.SuccessCode))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdatePolicy")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> UpdatePolicy([FromBody] EncryptClass data)
        {
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _policyService.UpdatePolicy(reslt);

            if (result.ResponseCode.Equals(_serviceResponseSettings.SuccessCode))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetByPolicyNumber")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> GetByPolicyNumber([FromBody] EncryptClass data)
        {
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _policyService.GetByPolicyNumber(reslt);

            if (result.ResponseCode.Equals(_serviceResponseSettings.SuccessCode))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetPolicies")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> GetPolicies([FromBody] EncryptClass data)
        {
            var result = await _policyService.GetPolicies();

            if (result.ResponseCode.Equals(_serviceResponseSettings.SuccessCode))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
