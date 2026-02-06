using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Helper;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.ComplianceDeadline;
using ComplyX_Businesss.Models.ComplianceFiling;
using ComplyX_Businesss.Models.ComplianceSchedule;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing compliance operations.
    /// Provides endpoints to manage compliance data, track status, and handle compliance-related processes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComplianceMgmtController : BaseController
    {
        private readonly AppContext _context;

        private readonly JwtTokenService _tokenService;

        private readonly ComplianceMgmtService _ComplianceMgmtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplianceMgmtController"/> class.
        /// </summary>
        /// <param name="tokenservice">Service for managing JWT token operations.</param>
        /// <param name="context">The application database context.</param>
        /// <param name="ComplianceMgmtService">The service for managing compliance operations.</param>
        public ComplianceMgmtController(JwtTokenService tokenservice, AppContext context, ComplianceMgmtService ComplianceMgmtService)
        {
            _tokenService = tokenservice;
            _context = context;
            _ComplianceMgmtService = ComplianceMgmtService;
        }
        /// <summary>
        /// Saves or updates compliance management deadline data.
        /// </summary>
        /// <param name="ComplianceDeadlines">
        /// The compliance deadline details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is saved successfully.
        /// Returns <c>400 Bad Request</c> if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Compliance management data saved successfully.</response>
        /// <response code="400">Invalid request or error occurred while saving the compliance management data.</response>
        [HttpPost("SaveComplianceMgmtData")]
        public async Task<IActionResult> SaveComplianceMgmtData([FromBody] ComplianceDeadlineRequestModel ComplianceDeadlines)
        {
            return ResponseResult(await _ComplianceMgmtService.SaveComplianceMgmtData(ComplianceDeadlines));
        }
        /// <summary>
        /// Removes compliance management deadline data based on the specified deadline identifier.
        /// </summary>
        /// <param name="DeadlineID">
        /// The unique identifier of the compliance deadline to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is removed successfully.
        /// Returns <c>204 No Content</c> if the specified deadline is not found.
        /// Returns <c>400 Bad Request</c> if an error occurs during the removal process.
        /// </returns>
        /// <response code="200">Compliance management data removed successfully.</response>
        /// <response code="204">Compliance management data not found.</response>
        /// <response code="400">Error occurred while removing compliance management data.</response>
        [HttpPut("RemoveComplianceMgmtData")]
        public async Task<IActionResult> RemoveComplianceMgmtData(string DeadlineID)
        {
            return ResponseResult(await _ComplianceMgmtService.RemoveComplianceMgmtData(DeadlineID));
        }
        /// <summary>
        /// Retrieves employment type data based on the specified employment type identifier.
        /// </summary>
        /// <param name="DeadlineID">
        /// The unique identifier of the employment type to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the employment type data if found.
        /// If the specified employment type is not found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Employment type data retrieved successfully.</response>
        /// <response code="204">If the specified employment type data is not found.</response>
        /// <response code="400">If there is an error while retrieving the employment type data.</response>
        [HttpGet("GetAllComplianceMgmtData")]
        public async Task<IActionResult> GetAllComplianceMgmtData(string DeadlineID)
        {
            return ResponseResult(await _ComplianceMgmtService.GetAllComplianceMgmtData(DeadlineID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of employment types based on the specified criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria for filtering, paging, and sorting the employment types.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employment types.
        /// If no data matches the criteria, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filtered employment type data retrieved successfully.</response>
        /// <response code="204">No employment types found matching the filter criteria.</response>
        /// <response code="400">If there is an error while retrieving the employment type data.</response>
        [HttpGet("GetComplianceMgmtFilter")]
        public async Task<IActionResult> GetComplianceMgmtFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ComplianceMgmtService.GetComplianceMgmtFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates compliance schedule data.
        /// </summary>
        /// <param name="ComplianceSchedules">
        /// The compliance schedule details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is saved successfully.
        /// Returns <c>400 Bad Request</c> if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Compliance schedule data saved successfully.</response>
        /// <response code="400">Invalid request or error occurred while saving the compliance schedule data.</response>
        [HttpPost("SaveComplianceSchedulesData")]
        public async Task<IActionResult> SaveComplianceSchedulesData([FromBody] ComplianceScheduleRequestModel ComplianceSchedules)
        {
            return ResponseResult(await _ComplianceMgmtService.SaveComplianceSchedulesData(ComplianceSchedules));
        }

        /// <summary>
        /// Removes compliance schedule data based on the specified schedule identifier.
        /// </summary>
        /// <param name="ScheduleID">
        /// The unique identifier of the compliance schedule to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is removed successfully.
        /// Returns <c>204 No Content</c> if the specified schedule is not found.
        /// Returns <c>400 Bad Request</c> if an error occurs during the removal process.
        /// </returns>
        /// <response code="200">Compliance schedule data removed successfully.</response>
        /// <response code="204">Compliance schedule data not found.</response>
        /// <response code="400">Error occurred while removing compliance schedule data.</response>
        [HttpPut("RemoveComplianceSchedulesData")]
        public async Task<IActionResult> RemoveComplianceSchedulesData(string ScheduleID)
        {
            return ResponseResult(await _ComplianceMgmtService.RemoveComplianceSchedulesData(ScheduleID));
        }

        /// <summary>
        /// Retrieves compliance schedule data based on the specified schedule identifier.
        /// </summary>
        /// <param name="ScheduleID">
        /// The unique identifier of the compliance schedule to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the compliance schedule data if found.
        /// Returns <c>200 OK</c> if the data is retrieved successfully.
        /// Returns <c>204 No Content</c> if the specified schedule data is not found.
        /// Returns <c>400 Bad Request</c> if an error occurs during the retrieval process.
        /// </returns>
        /// <response code="200">Compliance schedule data retrieved successfully.</response>
        /// <response code="204">Compliance schedule data not found.</response>
        /// <response code="400">Error occurred while retrieving compliance schedule data.</response>
        [HttpGet("GetAllComplianceSchedulesData")]
        public async Task<IActionResult> GetAllComplianceSchedulesData(string ScheduleID)
        {
            return ResponseResult(await _ComplianceMgmtService.GetAllComplianceSchedulesData(ScheduleID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of employment types based on the specified criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria for filtering, paging, and sorting the employment types.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employment types.
        /// If no data matches the criteria, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filtered employment type data retrieved successfully.</response>
        /// <response code="204">No employment types found matching the filter criteria.</response>
        /// <response code="400">If there is an error while retrieving the employment type data.</response>
        [HttpGet("GetComplianceSchedulesFilter")]
        public async Task<IActionResult> GetComplianceSchedulesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ComplianceMgmtService.GetComplianceSchedulesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates compliance filing data.
        /// </summary>
        /// <param name="ComplianceFilings">
        /// The compliance filing details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is saved successfully.
        /// Returns <c>400 Bad Request</c> if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Compliance filing data saved successfully.</response>
        /// <response code="400">Invalid request or error occurred while saving the compliance filing data.</response>
        [HttpPost("SaveComplianceFilingsData")]
        public async Task<IActionResult> SaveComplianceFilingsData([FromBody] ComplianceFilingRequestModel ComplianceFilings)
        {
            return ResponseResult(await _ComplianceMgmtService.SaveComplianceFilingsData(ComplianceFilings));
        }

        /// <summary>
        /// Removes compliance filing data based on the specified filing identifier.
        /// </summary>
        /// <param name="FilingID">
        /// The unique identifier of the compliance filing to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns <c>200 OK</c> if the data is removed successfully.
        /// Returns <c>204 No Content</c> if the specified filing is not found.
        /// Returns <c>400 Bad Request</c> if an error occurs during the removal process.
        /// </returns>
        /// <response code="200">Compliance filing data removed successfully.</response>
        /// <response code="204">Compliance filing data not found.</response>
        /// <response code="400">Error occurred while removing compliance filing data.</response>
        [HttpPut("RemoveComplianceFilingsData")]
        public async Task<IActionResult> RemoveComplianceFilingsData(string FilingID)
        {
            return ResponseResult(await _ComplianceMgmtService.RemoveComplianceFilingsData(FilingID));
        }

        /// <summary>
        /// Retrieves compliance filing data based on the specified filing identifier.
        /// </summary>
        /// <param name="FilingID">
        /// The unique identifier of the compliance filing to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the compliance filing data if found.
        /// Returns <c>200 OK</c> if the data is retrieved successfully.
        /// Returns <c>204 No Content</c> if the specified filing data is not found.
        /// Returns <c>400 Bad Request</c> if an error occurs during the retrieval process.
        /// </returns>
        /// <response code="200">Compliance filing data retrieved successfully.</response>
        /// <response code="204">Compliance filing data not found.</response>
        /// <response code="400">Error occurred while retrieving compliance filing data.</response>
        [HttpGet("GetAllComplianceFilingsData")]
        public async Task<IActionResult> GetAllComplianceFilingsData(string FilingID)
        {
            return ResponseResult(await _ComplianceMgmtService.GetAllComplianceFilingsData(FilingID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of compliance filings based on the specified criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria for filtering, paging, and sorting the compliance filings.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of compliance filings.
        /// Returns <c>200 OK</c> if data is found.
        /// Returns <c>204 No Content</c> if no data matches the criteria.
        /// Returns <c>400 Bad Request</c> if an error occurs during the retrieval process.
        /// </returns>
        /// <response code="200">Filtered compliance filing data retrieved successfully.</response>
        /// <response code="204">No compliance filings found matching the filter criteria.</response>
        /// <response code="400">Error occurred while retrieving compliance filing data.</response>
        [HttpGet("GetComplianceFilingsFilter")]
        public async Task<IActionResult> GetComplianceFilingsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ComplianceMgmtService.GetComplianceFilingsFilter(PagedListCriteria));
        }
    }
}
