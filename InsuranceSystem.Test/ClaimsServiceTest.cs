using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Application.Implementation;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Infrastructure.Dto;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace InsuranceSystem.Test
{
    [TestFixture]
    public class ClaimsServiceTest
    {
        private AutoMocker _mocker;
        private ClaimsService _claimsService;
        private Mock<IClaimsRepository> _claimsRepositoryMock;
        private Mock<IOptions<ServiceResponseSettings>> _serviceResponseSettingsMock;


        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _claimsService = _mocker.CreateInstance<ClaimsService>();

            _claimsRepositoryMock = new Mock<IClaimsRepository>();
            _serviceResponseSettingsMock = new Mock<IOptions<ServiceResponseSettings>>();

            // Setup default response settings
            var defaultServiceResponseSettings = new ServiceResponseSettings
            {
                 SuccessCode = "00",
                 SuccessMessage = "Operation was successful!",
                 NotFoundCode = "02",
                 NotFoundMessage = "Record not found!",
                 FailureCode= "01",
                 FailureMessage = "Operation was unsuccessful",
                 ErrorOccuredCode = "-1",
                 ErrorOccuredMessage = "service is temporarily unavailable"
            };
            _serviceResponseSettingsMock.Setup(x => x.Value).Returns(defaultServiceResponseSettings);
            _claimsService = new ClaimsService(_claimsRepositoryMock.Object, _serviceResponseSettingsMock.Object);
        }


        [Test]
        public async Task GetAllClaims_Returns_SuccessResponse_When_RepositoryReturnsData()
        {
            var response = new Infrastructure.Dto.ClaimsDto();

            _claimsRepositoryMock.Setup(x => x.GetAllClaims()).ReturnsAsync(new List<Infrastructure.Dto.ClaimsDto> { response });
                      

            var result = await _claimsService.GetAllClaims();

            Assert.That(result.ResponseData as List<Infrastructure.Dto.ClaimsDto>, Contains.Item(response));
        }       


    }
}
