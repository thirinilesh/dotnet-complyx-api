using AutoMapper;
using Moq;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Service;
using ComplyX_Tests.Repositories.Implementation;
using ComplyX_Tests.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using Nest;

namespace ComplyX_Tests.Service
{
    public class EmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly ICompanyRespository _companyRepository;
        private readonly IPartnerApiAuthConfigRepository _partnerApiAuthConfigRepository;
        private readonly Mock<IMapper> _mapper;

    
        public EmployeeService(
            ILogger<EmployeeService> logger,
            ICompanyRespository companyRepository,
            IPartnerApiAuthConfigRepository partnerApiAuthConfigRepository,
            Mock<IMapper> mapper)
        {
            _logger = logger;          
            _companyRepository = companyRepository;
            _partnerApiAuthConfigRepository = partnerApiAuthConfigRepository;
            _mapper = mapper;
        }

      }
    /// <summary>
    /// Represents a paginated API response.
    /// </summary>
    /// <typeparam name="T">Type of items in the response.</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Total number of records available.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// Number of records skipped.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Number of records taken.
        /// </summary>
        public int Take { get; set; }
    }

}
