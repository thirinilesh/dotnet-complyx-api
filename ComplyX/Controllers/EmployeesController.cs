using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Data;
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
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models.Employee;
using ComplyX_Businesss.Models.GratuityPolicy;
using ComplyX_Businesss.Models.GratuityTransaction;
using ComplyX_Businesss.Models.FnFCalculation;


namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing employees.
    /// Provides endpoints to create, read, update, and delete employee records.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeServices _IEmployeeServices;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="IEmployeeServices">
        /// The service used to perform employee-related operations.
        /// </param>
        public EmployeesController(IEmployeeServices IEmployeeServices)
        {
            _IEmployeeServices = IEmployeeServices;
        }
        /// <summary>
        /// Saves the details of an employee. 
        /// If the employee already exists, updates the existing record.
        /// </summary>
        /// <param name="Employees">
        /// The employee details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// </returns>
        /// <response code="200">The employee data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the employee data.</response>
        [HttpPut("SaveEmployeeData")]
        public async Task<IActionResult> SaveEmployeeData([FromBody] EmployeeRequestModel Employees)
        {
            return ResponseResult(await _IEmployeeServices.SaveEmployeeData(Employees));
        }

        /// <summary>
        /// Deletes an employee record based on the provided EmployeeID.
        /// </summary>
        /// <param name="EmployeeID">
        /// The unique identifier of the employee to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The employee was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the employee.</response>
        /// <response code="404">If no employee with the given EmployeeID is found.</response>
        [HttpPut("RemoveEmployeeData")]
        public async Task<IActionResult> RemoveEmployeeData(string EmployeeID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveEmployeeData(EmployeeID));
        }

        /// <summary>
        /// Retrieves a list of employees associated with the specified company.
        /// </summary>
        /// <param name="CompanyID">
        /// The unique identifier of the company whose employees are to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of employees.
        /// </returns>
        /// <response code="200">Returns the list of employees for the specified company.</response>
        /// <response code="204">If no employees are found for the given company.</response>
        /// <response code="400">If there is an error while fetching employee data.</response>
        [HttpPut("GetEmployeesByCompany")]
        public async Task<IActionResult> GetEmployeesByCompany(string CompanyID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompany(CompanyID));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of employees based on the provided criteria.
        /// </summary>
        /// <param name="pagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the employee data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employees.
        /// If no employees are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of employees.</response>
        /// <response code="204">If no employees are found.</response>
        /// <response code="400">If there is an error while fetching the employee data.</response>
        [HttpGet("GetEmployeeData")]
        public async Task<IActionResult> GetEmployeeData()
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeeData());
        }

        /// <summary>
        /// Retrieves a list of employees associated with the specified company and subcontractor.
        /// </summary>
        /// <param name="CompanyID">
        /// The unique identifier of the company whose employees are to be retrieved.
        /// </param>
        /// <param name="SubcontractorID">
        /// The unique identifier of the subcontractor to filter the employees.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of employees.
        /// </returns>
        /// <response code="200">Returns the list of employees for the specified company and subcontractor.</response>
        /// <response code="204">If no employees are found for the given company and subcontractor.</response>
        /// <response code="400">If there is an error while fetching employee data.</response>
        [HttpPut("GetEmployeesByCompanySubcontractor")]
        public async Task<IActionResult> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompanySubcontractor(CompanyID, SubcontractorID));
        }
        /// <summary>
        /// Retrieves a list of employees associated with the specified company and employee ID.
        /// </summary>
        /// <param name="CompanyID">
        /// The unique identifier of the company whose employees are to be retrieved.
        /// </param>
        /// <param name="EmployeeID">
        /// The unique identifier of the employee to filter the results.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employees.
        /// </returns>
        /// <response code="200">Returns the list of employees for the specified company and employee ID.</response>
        /// <response code="204">If no employees are found for the given company and employee ID.</response>
        /// <response code="400">If there is an error while fetching employee data.</response>
        [HttpPut("GetEmployeesByCompanyEmployee")]
        public async Task<IActionResult> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompanyEmployee(CompanyID, EmployeeID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of employees based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the employee data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of employees.
        /// If no employees are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of employees.</response>
        /// <response code="204">If no employees are found.</response>
        /// <response code="400">If there is an error while fetching the employee data.</response>
        [HttpGet("GetEmployeeDataFilter")]
        public async Task<IActionResult> GetEmployeeDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeeDataFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a gratuity policy. 
        /// If the policy already exists, updates the existing record.
        /// </summary>
        /// <param name="Gratuity_Policy">
        /// The gratuity policy details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The gratuity policy was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the gratuity policy.</response>
        [HttpPost("SaveGratuity_PolicyData")]
        public async Task<IActionResult> SaveGratuity_PolicyData([FromBody] GratuityPolicyRequestModel Gratuity_Policy)
        {
            return ResponseResult(await _IEmployeeServices.SaveGratuity_PolicyData(Gratuity_Policy));
        }

        /// <summary>
        /// Deletes a gratuity policy based on the provided PolicyID.
        /// </summary>
        /// <param name="PolicyID">
        /// The unique identifier of the gratuity policy to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The gratuity policy was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the gratuity policy.</response>
        /// <response code="404">If no gratuity policy with the given PolicyID is found.</response>
        [HttpPost("RemoveGratuity_PolicyData")]
        public async Task<IActionResult> RemoveGratuity_PolicyData(string PolicyID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveGratuity_PolicyData(PolicyID));
        }
        /// <summary>
        /// Retrieves the details of a gratuity policy based on the provided PolicyID.
        /// </summary>
        /// <param name="PolicyID">
        /// The unique identifier of the gratuity policy to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the gratuity policy data.
        /// If no policy is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the gratuity policy data for the specified PolicyID.</response>
        /// <response code="204">If no gratuity policy is found with the given PolicyID.</response>
        /// <response code="400">If there is an error while fetching the gratuity policy data.</response>
        [HttpGet("GetGratuity_Policy")]
        public async Task<IActionResult> GetGratuity_Policy(string PolicyID)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_Policy(PolicyID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of gratuity policies based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the gratuity policy data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of gratuity policies.
        /// If no policies are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of gratuity policies.</response>
        /// <response code="204">If no gratuity policies are found.</response>
        /// <response code="400">If there is an error while fetching the gratuity policy data.</response>
        [HttpGet("GetGratuity_PolicyFilter")]
        public async Task<IActionResult> GetGratuity_PolicyFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_PolicyFilter(PagedListCriteria));
        }


        /// <summary>
        /// Saves the details of a gratuity transaction. 
        /// If the transaction already exists, updates the existing record.
        /// </summary>
        /// <param name="Gratuity_Transactions">
        /// The gratuity transaction details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The gratuity transaction was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the gratuity transaction.</response>
        [HttpPost("SaveGratuity_TransactionsData")]
        public async Task<IActionResult> SaveGratuity_TransactionsData([FromBody] GratuityTransactionRequestModel Gratuity_Transactions)
        {
            return ResponseResult(await _IEmployeeServices.SaveGratuity_TransactionsData(Gratuity_Transactions));
        }
        /// <summary>
        /// Deletes a gratuity transaction based on the provided GratuityID.
        /// </summary>
        /// <param name="GratuityID">
        /// The unique identifier of the gratuity transaction to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The gratuity transaction was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the gratuity transaction.</response>
        /// <response code="404">If no gratuity transaction with the given GratuityID is found.</response>
        [HttpPost("RemoveGratuity_TransactionsData")]
        public async Task<IActionResult> RemoveGratuity_TransactionsData(string GratuityID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveGratuity_TransactionsData(GratuityID));
        }

        /// <summary>
        /// Retrieves the details of a gratuity transaction based on the provided GratuityID.
        /// </summary>
        /// <param name="GratuityID">
        /// The unique identifier of the gratuity transaction to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the gratuity transaction data.
        /// If no transaction is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the gratuity transaction data for the specified GratuityID.</response>
        /// <response code="204">If no gratuity transaction is found with the given GratuityID.</response>
        /// <response code="400">If there is an error while fetching the gratuity transaction data.</response>
        [HttpGet("GetGratuity_Transactions")]
        public async Task<IActionResult> GetGratuity_Transactions(string GratuityID)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_Transactions(GratuityID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of gratuity transactions based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the gratuity transaction data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of gratuity transactions.
        /// If no transactions are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of gratuity transactions.</response>
        /// <response code="204">If no gratuity transactions are found.</response>
        /// <response code="400">If there is an error while fetching the gratuity transaction data.</response>
        [HttpGet("GetGratuity_TransactionsFilter")]
        public async Task<IActionResult> GetGratuity_TransactionsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_TransactionsFilter(PagedListCriteria));
        }
        /// <summary>
        /// Saves the details of a full and final (FnF) calculation. 
        /// If the calculation already exists, updates the existing record.
        /// </summary>
        /// <param name="FnF_Calculations">
        /// The FnF calculation details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The FnF calculation was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the FnF calculation.</response>
        [HttpPost("SaveFnF_CalculationsData")]
        public async Task<IActionResult> SaveFnF_CalculationsData([FromBody] FnFCalculationRequestModel FnF_Calculations)
        {
            return ResponseResult(await _IEmployeeServices.SaveFnF_CalculationsData(FnF_Calculations));
        }
        /// <summary>
        /// Deletes a full and final (FnF) calculation based on the provided FnFID.
        /// </summary>
        /// <param name="FnFID">
        /// The unique identifier of the FnF calculation to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The FnF calculation was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the FnF calculation.</response>
        /// <response code="404">If no FnF calculation with the given FnFID is found.</response>
        [HttpPost("RemoveFnF_CalculationsData")]
        public async Task<IActionResult> RemoveFnF_CalculationsData(string FnFID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveFnF_CalculationsData(FnFID));
        }

        /// <summary>
        /// Retrieves the details of a full and final (FnF) calculation based on the provided FnFID.
        /// </summary>
        /// <param name="FnFID">
        /// The unique identifier of the FnF calculation to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the FnF calculation data.
        /// If no calculation is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the FnF calculation data for the specified FnFID.</response>
        /// <response code="204">If no FnF calculation is found with the given FnFID.</response>
        /// <response code="400">If there is an error while fetching the FnF calculation data.</response>
        [HttpGet("GetFnF_Calculations")]
        public async Task<IActionResult> GetFnF_Calculations(string FnFID)
        {
            return ResponseResult(await _IEmployeeServices.GetFnF_Calculations(FnFID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of full and final (FnF) calculations based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the FnF calculation data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of FnF calculations.
        /// If no calculations are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of FnF calculations.</response>
        /// <response code="204">If no FnF calculations are found.</response>
        /// <response code="400">If there is an error while fetching the FnF calculation data.</response>
        [HttpGet("GetFnF_CalculationsFilter")]
        public async Task<IActionResult> GetFnF_CalculationsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetFnF_CalculationsFilter(PagedListCriteria));
        }
    }
}
