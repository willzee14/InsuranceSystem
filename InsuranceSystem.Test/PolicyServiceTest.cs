
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceSystem.Infrastructure.Abstraction;
using InsuranceSystem.Application.Dtos;
using InsuranceSystem.Application.Implementation;

namespace InsuranceSystem.Test
{
    public class PolicyServiceTest
    {
        private AutoMocker _mocker;
        private Application.Implementation.PolicyService _policyService;
        private Mock<IPolicyHolderRepository> _policyRepositoryMock;
        private Mock<IOptions<ServiceResponseSettings>> _serviceResponseSettingsMock;


        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _policyService = _mocker.CreateInstance<PolicyService>();

            _policyRepositoryMock = new Mock<IPolicyHolderRepository>();
            _serviceResponseSettingsMock = new Mock<IOptions<ServiceResponseSettings>>();

            // Setup default response settings
            var defaultServiceResponseSettings = new ServiceResponseSettings
            {
                SuccessCode = "00",
                SuccessMessage = "Success",
                NotFoundCode = "404",
                NotFoundMessage = "Not Found",
                ErrorOccuredCode = "500",
                ErrorOccuredMessage = "Error Occurred",
                FailureCode = "400",
                FailureMessage = "Failure"
            };
            _serviceResponseSettingsMock.Setup(x => x.Value).Returns(defaultServiceResponseSettings);
            _policyService = new PolicyService(_policyRepositoryMock.Object, _serviceResponseSettingsMock.Object);
        }


        [Test]
        public async Task GetAllClaims_Returns_SuccessResponse_When_RepositoryReturnsData()
        {
            var response = new Infrastructure.Dto.PolicyHolderDto();

            _policyRepositoryMock.Setup(x => x.GetPolicies()).ReturnsAsync(new List<Infrastructure.Dto.PolicyHolderDto> { response });      


            var result = await _policyService.GetPolicies();

            Assert.That(result.ResponseData as List<Infrastructure.Dto.PolicyHolderDto>, Contains.Item(response));
        }
    }
}
