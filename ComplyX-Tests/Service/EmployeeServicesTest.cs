using AutoMapper;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Repositories.Interface;
using Microsoft.Extensions.Logging;

namespace ComplyX_Tests.Service
{
    public class EmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly ICompanyRespository _companyRepository;
        private readonly IPartnerApiAuthConfigRepository _partnerApiAuthConfigRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            ILogger<EmployeeService> logger,
            IEmployeeRepository companyEmployeeRepository,
            ICompanyRespository companyRepository,
            IPartnerApiAuthConfigRepository partnerApiAuthConfigRepository,
            IMapper mapper)
        {
            _logger = logger;
            _EmployeeRepository = companyEmployeeRepository;
            _companyRepository = companyRepository;
            _partnerApiAuthConfigRepository = partnerApiAuthConfigRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<Employees>> GetEmployeesAsync(
            Employees request,
            string userId)
        {
            _logger.LogInformation(
                "GetEmployeesByCompanyId called for companyId {companyId} by user {userId}",
                request.EmployeeID,
                userId
            );

            // Check if the user has access to the company
            bool hasAccess = await _partnerApiAuthConfigRepository
                .CheckUserCompanyAccess(userId, request.EmployeeID);
            if (!hasAccess)
            {
                throw new ApiExceptionForbidden(
                    $"User {userId} does not have access to company {request.EmployeeID}."
                );
            }

            _logger.LogInformation(
                "GetCompanyByIdAsync called with companyId: {companyId}, userId: {userId}",
                request.EmployeeID,
                userId
            );
            var company = await _companyRepository
                .GetCompanyByIdAsync(request.CompanyID)
                ?? throw new ApiExceptionNotFound(
                    $"Company with ID {request.CompanyID} not found."
                );

            _logger.LogInformation(
                "GetEmployeesAsync called with companyId: {companyId}, status: {status}",
                request.CompanyID,
                request.ActiveStatus
            );
            var employees = await _EmployeeRepository.GetEmployeesAsync(request);

            _logger.LogInformation(
                "GetEmployeeCurrentMeasurementInfoAsync called with companyId: {companyId}, monthlyMeasurement: {monthlyMeasurement}",
                request.EmployeeID,
                company.IsActive
            );
            
            return employees;
        }

        internal async Task GetEmployeesAsync(Company request, string userId)
        {
            throw new NotImplementedException();
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
