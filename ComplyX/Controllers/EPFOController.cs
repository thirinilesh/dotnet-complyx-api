using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.CompanyEPFO;
using ComplyX_Businesss.Models.EmployeeEPFO;
using ComplyX_Businesss.Models.EPFOECRFile;
using ComplyX_Businesss.Models.EPFOPeriod;
using ComplyX_Businesss.Models.EPFOMonthWage;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.CompanyBranches;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing EPFO (Employee Provident Fund Organization) related operations.
    /// Provides endpoints to handle EPFO transactions, contributions, and related data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EPFOController : BaseController
    {
        private readonly EPFOServices _EPFOServices;
        /// <summary>
        /// Initializes a new instance of the <see cref="EPFOController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="EPFOServices">The service for managing EPFO operations.</param>
        public EPFOController(AppContext context, EPFOServices EPFOServices)
        {
            _EPFOServices = EPFOServices;
        }

        /// <summary>
        /// Saves the details of a company's EPFO information. 
        /// If the EPFO data already exists, updates the existing record.
        /// </summary>
        /// <param name="EPFOEstablishment">
        /// The company EPFO details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The company EPFO data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the company EPFO data.</response>
        [HttpPut("SaveEPFOEstablishmentData")]
        public async Task<IActionResult> SaveEPFOEstablishmentData([FromBody] EPFOEstablishmentRequestModel CompanyEPFO)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOEstablishmentData(CompanyEPFO));
        }
        /// <summary>
        /// Deletes a company's EPFO data based on the provided CompanyEPFOId.
        /// </summary>
        /// <param name="CompanyEPFOId">
        /// The unique identifier of the company EPFO data to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The company EPFO data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the company EPFO data.</response>
        /// <response code="404">If no company EPFO data with the given CompanyEPFOId is found.</response>
        [HttpPut("RemoveEPFOEstablishmentData")]
        public async Task<IActionResult> RemoveEPFOEstablishmentData(string CompanyEPFOId)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOEstablishmentData(CompanyEPFOId));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of company EPFO data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the company EPFO data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of company EPFO data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of company EPFO data.</response>
        /// <response code="204">If no company EPFO data is found.</response>
        /// <response code="400">If there is an error while fetching the company EPFO data.</response>
        [HttpGet("GetAllEPFOEstablishmentFilter")]
        public async Task<IActionResult> GetAllEPFOEstablishmentFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllEPFOEstablishmentFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of an employee's EPFO data. 
        /// If the employee EPFO data already exists, updates the existing record.
        /// </summary>
        /// <param name="EmployeeEPFO">
        /// The employee EPFO details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The employee EPFO data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the employee EPFO data.</response>
        [HttpPost("SaveEmployeeEPFOData")]
        public async Task<IActionResult> SaveEmployeeEPFOData([FromBody] EmployeeEPFORequestModel EmployeeEPFO)
        {
            return ResponseResult(await _EPFOServices.SaveEmployeeEPFOData(EmployeeEPFO));
        }


        /// <summary>
        /// Deletes an employee's EPFO data based on the provided EmployeeEPFOId.
        /// </summary>
        /// <param name="EmployeeEPFOId">
        /// The unique identifier of the employee EPFO data to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The employee EPFO data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the employee EPFO data.</response>
        /// <response code="404">If no employee EPFO data with the given EmployeeEPFOId is found.</response>
        [HttpPut("RemoveEmployeeEPFOData")]
        public async Task<IActionResult> RemoveEmployeeEPFOData(string EmployeeEPFOId)
        {
            return ResponseResult(await _EPFOServices.RemoveEmployeeEPFOData(EmployeeEPFOId));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of employee EPFO data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the employee EPFO data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employee EPFO data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of employee EPFO data.</response>
        /// <response code="204">If no employee EPFO data is found.</response>
        /// <response code="400">If there is an error while fetching the employee EPFO data.</response>
        [HttpGet("GetAllEmployeeEPFOFilter")]
        public async Task<IActionResult> GetAllEmployeeEPFOFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllEmployeeEPFOFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of an EPFO ECR (Electronic Challan cum Return) file. 
        /// If the ECR data already exists, updates the existing record.
        /// </summary>
        /// <param name="EPFOECRFile">
        /// The EPFO ECR file details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The EPFO ECR data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the EPFO ECR data.</response>
        [HttpPut("SaveEPFOECRData")]
        public async Task<IActionResult> SaveEPFOECRData([FromBody] EPFOECRFileRequestModel EPFOECRFile)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOECRData(EPFOECRFile));
        }


        /// <summary>
        /// Deletes an EPFO ECR (Electronic Challan cum Return) file based on the provided ECRFileId.
        /// </summary>
        /// <param name="ECRFileId">
        /// The unique identifier of the EPFO ECR file to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The EPFO ECR data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the EPFO ECR data.</response>
        /// <response code="404">If no EPFO ECR data with the given ECRFileId is found.</response>
        [HttpPut("RemoveEPFOECRData")]
        public async Task<IActionResult> RemoveEPFOECRData(string ECRFileId)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOECRData(ECRFileId));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of EPFO ECR (Electronic Challan cum Return) data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the EPFO ECR data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of EPFO ECR data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of EPFO ECR data.</response>
        /// <response code="204">If no EPFO ECR data is found.</response>
        /// <response code="400">If there is an error while fetching the EPFO ECR data.</response>
        [HttpGet("GetEPFOECRDataFilter")]
        public async Task<IActionResult> GetEPFOECRDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetEPFOECRDataFilter(PagedListCriteria));
        }


        /// <summary>
        /// Saves the details of an EPFO period. 
        /// If the EPFO period data already exists, updates the existing record.
        /// </summary>
        /// <param name="EPFOPeriod">
        /// The EPFO period details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The EPFO period data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the EPFO period data.</response>
        [HttpPut("SaveEPFOPeriodData")]
        public async Task<IActionResult> SaveEPFOPeriodData([FromBody] EPFOPeriodRequestModel EPFOPeriod)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOPeriodData(EPFOPeriod, User.Claims.GetUserId()));
        }


        /// <summary>
        /// Deletes an EPFO period based on the provided EPFOPeriodId.
        /// </summary>
        /// <param name="EPFOPeriodId">
        /// The unique identifier of the EPFO period to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The EPFO period data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the EPFO period data.</response>
        /// <response code="404">If no EPFO period data with the given EPFOPeriodId is found.</response>
        [HttpPut("RemoveEPFOPeriodData")]
        public async Task<IActionResult> RemoveEPFOPeriodData(string EPFOPeriodId)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOPeriodData(EPFOPeriodId));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of EPFO period data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the EPFO period data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of EPFO period data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of EPFO period data.</response>
        /// <response code="204">If no EPFO period data is found.</response>
        /// <response code="400">If there is an error while fetching the EPFO period data.</response>
        [HttpGet("GetEPFOPeriodDataFilter")]
        public async Task<IActionResult> GetEPFOPeriodDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetEPFOPeriodDataFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a company's EPFO information. 
        /// If the EPFO data already exists, updates the existing record.
        /// </summary>
        /// <param name="EPFOMonthlyWage">
        /// The company EPFO details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The company EPFO data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the company EPFO data.</response>
        [HttpPut("SaveEPFOMonthlyWageData")]
        public async Task<IActionResult> SaveEPFOMonthlyWageData([FromBody] EPFOMonthWageRequestModel EPFOMonthlyWage)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOMonthlyWageData(EPFOMonthlyWage));
        }
        /// <summary>
        /// Deletes a company's EPFO data based on the provided wage ID.
        /// </summary>
        /// <param name="WageID">
        /// The unique identifier of the company EPFO data to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The company EPFO data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the company EPFO data.</response>
        /// <response code="404">If no company EPFO data with the given wageId is found.</response>
        [HttpPut("RemoveEPFOMonthlyWageData")]
        public async Task<IActionResult> RemoveEPFOMonthlyWageData(string WageID)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOMonthlyWageData(WageID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of company EPFO data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the company EPFO data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of company EPFO data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of company EPFO data.</response>
        /// <response code="204">If no company EPFO data is found.</response>
        /// <response code="400">If there is an error while fetching the company EPFO data.</response>
        [HttpGet("GetAllEPFOMonthlyWageFilter")]
        public async Task<IActionResult> GetAllEPFOMonthlyWageFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllEPFOMonthlyWageFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates company branch details.
        /// If the branch already exists, the existing record will be updated.
        /// </summary>
        /// <param name="CompanyBranches">
        /// The company branch details to be saved or updated.
        /// </param>
        /// <returns>
        /// Returns 200 OK if successful; otherwise 400 BadRequest.
        /// </returns>
        /// <response code="200">Company branch data saved or updated successfully.</response>
        /// <response code="400">Error occurred while saving or updating company branch data.</response>
        [HttpPost("SaveCompanyBranchesData")]
        public async Task<IActionResult> SaveCompanyBranchesData([FromBody] CompanyBranchesRequestModel CompanyBranches)
        {
            return ResponseResult(await _EPFOServices.SaveCompanyBranchesData(CompanyBranches));
        }
        /// <summary>
        /// Deletes a company's branch EPFO data based on the provided branch ID.
        /// </summary>
        /// <param name="BranchId">
        /// The unique identifier of the company branch EPFO data to be deleted.
        /// </param>
        /// <returns>
        /// Returns 200 OK if deleted successfully, 400 BadRequest if an error occurs,
        /// or 404 NotFound if no data with the given branchId is found.
        /// </returns>
        /// <response code="200">The company branch EPFO data was successfully deleted.</response>
        /// <response code="400">Error occurred while deleting the company branch EPFO data.</response>
        /// <response code="404">No company branch EPFO data found with the given branchId.</response>
        [HttpPut("RemoveCompanyBranchesData")]
        public async Task<IActionResult> RemoveCompanyBranchesData(string BranchId)
        {
            return ResponseResult(await _EPFOServices.RemoveCompanyBranchesData(BranchId));
        }


        /// <summary>
        /// Retrieves a filtered and paginated list of company branch EPFO data based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the company branch EPFO data.
        /// </param>
        /// <returns>
        /// Returns a filtered list of company branch EPFO data as <see cref="IActionResult"/>.
        /// Returns 204 No Content if no data is found, or 400 Bad Request if an error occurs.
        /// </returns>
        /// <response code="200">Returns the filtered list of company branch EPFO data.</response>
        /// <response code="204">No company branch EPFO data found.</response>
        /// <response code="400">Error occurred while fetching the company branch EPFO data.</response>
        [HttpGet("GetAllCompanyBranchesFilter")]
        public async Task<IActionResult> GetAllCompanyBranchesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllCompanyBranchesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of company branch EPFO establishments.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// Criteria used to filter, search, sort, and paginate the EPFO establishment list.
        /// </param>
        /// <returns>
        /// Returns a filtered list of EPFO establishments.
        /// </returns>
        /// <response code="200">Returns the filtered list of EPFO establishments.</response>
        /// <response code="204">No EPFO establishment data found.</response>
        /// <response code="400">An error occurred while fetching the EPFO establishment data.</response>
        [HttpGet("GetEPFOEstablishmentList")]
        public async Task<IActionResult> GetEPFOEstablishmentList([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetEPFOEstablishmentList(PagedListCriteria));
        }
    }
}
