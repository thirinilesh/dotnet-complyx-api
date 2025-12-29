using AutoMapper;

using FluentAssertions;
using ComplyX_Businesss.Models;
using ComplyX;
using ComplyX_Businesss.Helper;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using Moq;
using System.Linq.Expressions;
using Castle.Core.Resource;
using MediaBrowser.Model.Services;
using Xunit;
using ComplyX_Tests.Repositories.Interface;
using System.Net;
using Microsoft.Kiota.Abstractions;

namespace ComplyX_Tests.Service
{
    public class CompanyServicesTest
    {
        private readonly Mock<ILogger<EmployeeService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPartnerApiAuthConfigRepository> _mockPartnerApiAuthConfigRepository;
        private readonly Mock<ICompanyRespository> _mockCompanyRepository;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly EmployeeService _service;

        public CompanyServicesTest()
        {
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<EmployeeService>>();
            _mockCompanyRepository = new Mock<ICompanyRespository>();
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockPartnerApiAuthConfigRepository = new Mock<IPartnerApiAuthConfigRepository>();

            _service = new EmployeeService(
                 _logger.Object,
                _mockEmployeeRepository.Object,
                _mockCompanyRepository.Object,
                _mockPartnerApiAuthConfigRepository.Object,
                _mapper.Object
            );


        }

        [Fact]
        public async Task GetEmployeesAsync_UserLacksAccess_ThrowsApiExceptionForbidden()
        {
            // Arrange
            int companyId = 1;
            string userId = "user123";
            var request = new Company
            {
                CompanyID = companyId,
                IsActive = true
            };

            _mockPartnerApiAuthConfigRepository
                .Setup(repo => repo.CheckUserCompanyAccess(userId, companyId))
                .ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _service.GetEmployeesAsync(request, userId);

            // Assert
            await act.Should().ThrowAsync<ApiExceptionForbidden>()
                .WithMessage($"User {userId} does not have access to company {companyId}.");
        }

        [Fact]
        public async Task GetEmployeesAsync_CompanyNotFound_ThrowsApiExceptionNotFound()
        {
            // Arrange
            int companyId = 1;
            string userId = "user123";
            var request = new Employees
            {
                CompanyID = companyId,
                ActiveStatus = false,
            };

            _mockPartnerApiAuthConfigRepository
                .Setup(repo => repo.CheckUserCompanyAccess(userId, companyId))
                .ReturnsAsync(true);

            _mockCompanyRepository
                .Setup(repo => repo.GetCompanyByIdAsync(companyId))
                .ReturnsAsync((Company?)null);

            // Act
            Func<Task> act = async () => await _service.GetEmployeesAsync(request, userId);

            // Assert
            await act.Should().ThrowAsync<ApiExceptionNotFound>()
                .WithMessage($"Company with ID {companyId} not found.");
        }

    }
    public class ApiExceptionForbidden : ApiException
    {
        public ApiExceptionForbidden()
        : base(HttpStatusCode.Forbidden.ToString())
        {
        }

        /// <summary>
        /// This method instantiates a 403 - Forbidden API exception from an existing exception
        /// </summary>
        /// <param name="exception">The existing exception</param>
        public ApiExceptionForbidden(System.Exception exception)
            : base(HttpStatusCode.Forbidden.ToString(), exception)
        {
        }

        /// <summary>
        /// This method instantiates a throwable 403 - Forbidden API error 
        /// with a <paramref name="message"/> and optional <paramref name="innerException"/>
        /// </summary>
        /// <param name="message">The message describing what happened</param>
        /// <param name="innerException">Optional, exception before this one</param>
        public ApiExceptionForbidden(string message, System.Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
    public class ApiExceptionNotFound : ApiException
    {
        /// <summary>
        /// This method instantiates an empty 404 - Not Found API error
        /// </summary>
        public ApiExceptionNotFound()
            : base(HttpStatusCode.NotFound.ToString())
        {
        }

        /// <summary>
        /// This method instantiates a 404 - Not Found API exception from an existing exception
        /// </summary>
        /// <param name="exception">The existing exception</param>
        public ApiExceptionNotFound(System.Exception exception)
            : base(HttpStatusCode.NotFound.ToString(), exception)
        {
        }

        /// <summary>
        /// This method instantiates a throwable 404 - Not Found API error 
        /// with a <paramref name="message"/> and optional <paramref name="innerException"/>
        /// </summary>
        /// <param name="message">The message describing what happened</param>
        /// <param name="innerException">Optional, exception before this one</param>
        public ApiExceptionNotFound(
            string message,
            System.Exception innerException = null)
            : base(  message, innerException)
        {
        }
    }

}
