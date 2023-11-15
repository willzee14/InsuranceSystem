using InsuranceSystem.API.Extensions;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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

            if (result.ResponseCode.Equals(_serviceResponseSettings.SuccessCode))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
