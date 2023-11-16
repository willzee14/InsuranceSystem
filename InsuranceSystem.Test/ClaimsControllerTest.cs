using InsuranceSystem.API.Controllers;
using InsuranceSystem.Application.Abstraction;
using InsuranceSystem.Application.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace InsuranceSystem.Test
{
    using System.Threading.Tasks;
    using Azure;
    using InsuranceSystem.Application.Dtos;
    using InsuranceSystem.Infrastructure.Dto;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.AutoMock;
    using NUnit.Framework;


    [TestFixture]
    public class ClaimsControllerTests
    {
        private AutoMocker _mocker;
        private ClaimsController _claimsController;
        
        
        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _claimsController = _mocker.CreateInstance<ClaimsController>();

        }

        [Test]
        public async Task TestReturnClaims()
        {
            // Arrange
            var response = new ServiceResponse();

            _mocker.GetMock<ICalimsService>()
                    .Setup(x => x.GetAllClaims())
                    .ReturnsAsync(new ServiceResponse());

            // Act
            var result = await _claimsController.Claims();

            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            // Assert
            Assert.That(okResult.StatusCode.Equals(200), Is.True);
        }

        [Test]
        public async Task TestShouldCreateClaim()
        {
            var claims = new EncryptClass();

            var result = await _claimsController.InsetClaim(claims);

            _mocker.GetMock<ICalimsService>()
                    .Verify(x => x.InsetClaim(claims.Data), Times.Once);


            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }

        [Test]
        public async Task TestShouldUpdateClaim()
        {
           
            var claims = new EncryptClass();

            var result = await _claimsController.UpdateClaim(claims);

            _mocker.GetMock<ICalimsService>()
                    .Verify(x => x.UpdateClaim(claims.Data), Times.Once);

           
            var okResult = result as OkObjectResult;
            
            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }

        [Test]
        public async Task TestToGetClaimByNationalID()
        {
            var claims = new EncryptClass();
            var response = new ServiceResponse();

            var result = await _claimsController.ClaimsByNationalID(claims);

            _mocker.GetMock<ICalimsService>()
                     .Setup(x => x.GetClaimsByNationalID(claims.Data))
                     .ReturnsAsync(new ServiceResponse());


            var okResult = result as OkObjectResult;
            var actualConfiguration = okResult.Value as ServiceResponse;

            Assert.That(okResult.StatusCode.Equals(200), Is.True);

        }


    }

}
