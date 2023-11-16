using InsuranceSystem.API.Controllers;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Moq;
#pragma warning disable

namespace InsuranceSystem.Test
{
    public class PolicyControllerTest
    {
        private AutoMocker _mocker;
        private PolicyHolderController _policyController;


        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _policyController = _mocker.CreateInstance<PolicyHolderController>();

        }

        [Test]
        public async Task TestReturnPolicies()
        {
            // Arrange
            var response = new ServiceResponse();

            _mocker.GetMock<IPolicyService>()
                    .Setup(x => x.GetPolicies())
                    .ReturnsAsync(new ServiceResponse());

            // Act
            var result = await _policyController.GetPolicies();

            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            // Assert
            Assert.That(okResult.StatusCode.Equals(200), Is.True);
        }

        [Test]
        public async Task TestShouldInsertpolicy()
        {
            var policy = new EncryptClass();

            var result = await _policyController.InsetPolicy(policy);

            _mocker.GetMock<IPolicyService>()
                    .Verify(x => x.InsetPolicy(policy.Data), Times.Once);


            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }

        [Test]
        public async Task TestShouldUpdatePolicy()
        {

            var policy = new EncryptClass();

            var result = await _policyController.UpdatePolicy(policy);

            _mocker.GetMock<IPolicyService>()
                    .Verify(x => x.UpdatePolicy(policy.Data), Times.Once);


            var okResult = result as OkObjectResult;

            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }

        [Test]
        public async Task TestToGetClaimByNationalID()
        {
            var policy = new EncryptClass();
            var response = new ServiceResponse();

            var result = await _policyController.GetByPolicyNumber(policy);

            _mocker.GetMock<IPolicyService>()
                     .Setup(x => x.GetByPolicyNumber(policy.Data))
                     .ReturnsAsync(new ServiceResponse());


            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }
    }
}