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
using ComplyX_Tests.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using ComplyX_Tests.Service;
using NHibernate.Mapping.ByCode.Impl;
using System;
using ComplyX.Shared.Data;
using ComplyX_Tests.Service;
using AppDbContext = ComplyX_Tests.Service.AppDbContext;
using static ComplyX_Tests.Service.ApiExceptionNotFound;

namespace ComplyX_Tests.Service
{
    public class CompanyServicesTest
    {
        private readonly Mock<ILogger<EmployeeService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPartnerApiAuthConfigRepository> _mockPartnerApiAuthConfigRepository;
        private readonly Mock<ICompanyRespository> _mockCompanyRepository;
        private readonly EmployeeService _service;
        private readonly AppDbContext _appcontext;
        private readonly ICompanyRespository _companyRepository;
        private readonly DbContext _dbContext;
        private readonly Mock<ICompanyRespository> _companyRepoMock;

        public CompanyServicesTest()
        {
            // 1. Create the mock
            _companyRepoMock = new Mock<ICompanyRespository>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<EmployeeService>>();
            _mockCompanyRepository = new Mock<ICompanyRespository>();
            _mockPartnerApiAuthConfigRepository = new Mock<IPartnerApiAuthConfigRepository>();
            _appcontext = new AppDbContext();


            var options = new DbContextOptionsBuilder<AppDbContext>()
.UseInMemoryDatabase(Guid.NewGuid().ToString())
.Options;

            var _context = new Mock<AppDbContext>(options);


            _service = new EmployeeService(
                _logger.Object,
                _mockCompanyRepository.Object,  // ✅ pass the mock object, not the real repository
            _mockPartnerApiAuthConfigRepository.Object,
                _mapper);

        }
         
        [Fact]
        public async Task CompanyService_CreateCompanyWithoutBrand_UsesPartnerDefault()
        {
            // Arrange
            var createCompanyRequest = new CreateCompanyRequest
            {
                Name = "Test Company",
                Address1 = "123 Test St",
                City = "Test City",
                State = "TS",
                Zip = "12345"
            };

            var userId = "test-user-id";
            var defaultBrandName = "DefaultBrand";

            var user = new ApplicationUser
            {
                Id = userId
            };

            _mockPartnerApiAuthConfigRepository
                .Setup(repo => repo.GetApiUsersDefaultBrandName(userId))
                .ReturnsAsync(defaultBrandName);

            _companyRepoMock
                .Setup(repo => repo.CompanyExistsByNameAsync(createCompanyRequest.Name,false,0))
                .ReturnsAsync(false);

            _mapper
                .Setup(m => m.Map<Companies>(It.IsAny<CreateCompanyRequest>()))
                .Returns((CreateCompanyRequest req) => new Companies
                {
                    Name = req.Name,
                    Address1 = req.Address1,
                    City = req.City,
                    State = req.State,
                    Zip = req.Zip
                });

                _mapper
                .Setup(m => m.Map<CreateCompanyResponse>(It.IsAny<Companies>()))
                .Returns((Companies cust) => new CreateCompanyResponse
                {
                    CompanyId = cust.Id,
                    Name = cust.Name,
                    Address1 = cust.Address1,
                    City = cust.City,
                    State = cust.State,
                    Zip = cust.Zip

                });

            // Act
            var companyResponse = await  CreateCompanyAsync(createCompanyRequest, userId);

            companyResponse.Should().NotBeNull();
        }
        public async Task<CreateCompanyResponse> CreateCompanyAsync(CreateCompanyRequest request, string userId)
        {

            if (request == null)
            {
                throw new ApiExceptionBadRequest("Request cannot be null.");
            }

            bool companyExists = await _companyRepoMock.Object.CompanyExistsByNameAsync(request.Name, false,0);
          
            if (companyExists)
            {
                throw new ApiExceptionConflict(
                    $"Company '{request.Name}' already exists.");
            }

            var options = new DbContextOptionsBuilder<AppDbContext>()
.UseInMemoryDatabase(Guid.NewGuid().ToString())
.Options;

            var _appcontext = new Mock<AppDbContext>(options);

            var company = _mapper.Object.Map<Companies>(request);
            company.Name = request.Name;
            company.Address1 = request.Address1;
            company.City = request.City;
            company.State = request.State;
            company.Zip = request.Zip;

            await _appcontext.Object.AddAsync(company);
            await _appcontext.Object.SaveChangesAsync();

            var response = _mapper.Object.Map<CreateCompanyResponse>(company);

            return response;
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
        public class ApiExceptionConflict : ApiException
        {
            /// <summary>
            /// 409 - Conflict
            /// </summary>
            public ApiExceptionConflict()
                : base(HttpStatusCode.Conflict.ToString())
            {
            }

            /// <summary>
            /// 409 - Conflict from existing exception
            /// </summary>
            public ApiExceptionConflict(Exception exception)
                : base(HttpStatusCode.Conflict.ToString(), exception)
            {
            }

            /// <summary>
            /// 409 - Conflict with message
            /// </summary>
            public ApiExceptionConflict(
                string message,
                Exception? innerException = null)
                : base(HttpStatusCode.Conflict.ToString(), innerException)
            {
            }
        }
    }

}
