using InsuranceSystem.API.Extensions;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _calimsService.InsetClaim(reslt);
            
                return Ok(result);            
        }

        [HttpPut("UpdateClaim")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> UpdateClaim([FromBody] EncryptClass data)
        {
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _calimsService.UpdateClaim(reslt);
           
                return Ok(result);            
        }

        [HttpPost("ClaimsByNationalID")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> ClaimsByNationalID([FromBody] EncryptClass data)
        {
            var reslt = _contextAccessor.HttpContext?.Items?["data"]?.ToString();
            var result = await _calimsService.GetClaimsByNationalID(reslt);
            
                return Ok(result);            
        }

        [HttpGet("Claims")]
        [ServiceFilter(typeof(EncryptionActionFilter))]
        public async Task<IActionResult> Claims()
        {           
            var result = await _calimsService.GetAllClaims();
           
                return Ok(result);           
            
        }
    }
}
