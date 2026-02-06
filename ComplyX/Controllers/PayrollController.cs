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
using ComplyX_Businesss.Models.PayrollData;
using ComplyX_Businesss.Models.LeaveEncashmentPolicy;
using ComplyX_Businesss.Models.LeaveEncashmentTransaction;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing payroll-related operations.
    /// Provides endpoints for leave encashment, salary, and other payroll transactions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PayrollController : BaseController
    {
        private readonly IPayrollServices _IPayrollServices;

        /// <summary>
        /// Service for handling payroll business logic.
        /// </summary>
        public PayrollController(ComplyX_Businesss.Helper.AppContext context, IPayrollServices IPayrollServices)
        {
            _IPayrollServices = IPayrollServices;
        }

        /// <summary>
        /// Saves the details of a Payroll record. 
        /// If the payroll data already exists, updates the existing record.
        /// </summary>
        /// <param name="Payroll">
        /// The Payroll details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The Payroll data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the Payroll data.</response>
        [HttpPut("SavePayrollData")]
        public async Task<IActionResult> SavePayrollData([FromBody] PayrollDataRequestModel Payroll)
        {
            return ResponseResult(await _IPayrollServices.SavePayrollData(Payroll));
        }

        /// <summary>
        /// Deletes a Payroll record based on the provided Payroll ID.
        /// </summary>
        /// <param name="PayrollID">
        /// The unique identifier of the Payroll record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The Payroll data was deleted successfully.</response>
        /// <response code="400">If there is an error while deleting the Payroll data, such as invalid Payroll ID.</response>
        [HttpPut("RemovePayrollData")]
        public async Task<IActionResult> RemovePayrollData(string PayrollID)
        {
            return ResponseResult(await _IPayrollServices.RemovePayrollData(PayrollID));
        }

        /// <summary>
        /// Deletes Payroll records based on the provided Company ID and Employee ID.
        /// </summary>
        /// <param name="CompanyID">
        /// The unique identifier of the company for which payroll data needs to be deleted.
        /// </param>
        /// <param name="EmployeeID">
        /// The unique identifier of the employee whose payroll data needs to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The Payroll data for the specified employee and company was deleted successfully.</response>
        /// <response code="400">If there is an error while deleting the Payroll data, such as invalid CompanyID or EmployeeID.</response>
        [HttpPut("RemovePayrollDataByCompanyIDEmployeeID")]
        public async Task<IActionResult> RemovePayrollDataByCompanyIDEmployeeID(string CompanyID,string EmployeeID)
        {
            return ResponseResult(await _IPayrollServices.RemovePayrollDataByCompanyIDEmployeeID(CompanyID, EmployeeID));
        }

        /// <summary>
        /// Deletes all Payroll records associated with the specified Company ID.
        /// </summary>
        /// <param name="CompanyID">
        /// The unique identifier of the company whose payroll data should be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">All Payroll data for the specified company was deleted successfully.</response>
        /// <response code="400">If there is an error while deleting the Payroll data, such as an invalid CompanyID.</response>
        [HttpPut("RemoveAllPayrollDataByCompanyID")]
        public async Task<IActionResult> RemoveAllPayrollDataByCompanyID(string CompanyID)
        {
            return ResponseResult(await _IPayrollServices.RemoveAllPayrollDataByCompanyID(CompanyID));
        }


        /// <summary>
        /// Edits an existing Payroll record based on the provided Company ID, Employee ID, and Payroll ID.
        /// </summary>
        /// <param name="data">
        /// The updated Payroll data to be saved.
        /// </param>
        /// <param name="CompanyID">
        /// The unique identifier of the company associated with the payroll record.
        /// </param>
        /// <param name="EmployeeID">
        /// The unique identifier of the employee associated with the payroll record.
        /// </param>
        /// <param name="PayrollID">
        /// The unique identifier of the Payroll record to be edited.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the edit operation.
        /// </returns>
        /// <response code="200">The Payroll data was updated successfully.</response>
        /// <response code="400">If there is an error while editing the Payroll data, such as invalid IDs or validation failures.</response>
        [HttpPut("EditPayrollDataByCompanyIDEmployeeID")]
        public async Task<IActionResult> EditPayrollDataByCompanyIDEmployeeID([FromBody]PayrollDataRequestModel data, string CompanyID, string EmployeeID, string PayrollID)
        {
            return ResponseResult(await _IPayrollServices.EditPayrollDataByCompanyIDEmployeeID(data,CompanyID, EmployeeID,PayrollID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of Payroll records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the Payroll data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of Payroll records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of Payroll records.</response>
        /// <response code="204">If no Payroll data is found.</response>
        /// <response code="400">If there is an error while fetching the Payroll data.</response>
        [HttpGet("GetPayrollDataFilter")]
        public async Task<IActionResult> GetPayrollDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IPayrollServices.GetPayrollDataFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a Leave Encashment Policy. 
        /// If the policy already exists, updates the existing record.
        /// </summary>
        /// <param name="Leave_Encashment_Policy">
        /// The Leave Encashment Policy details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The Leave Encashment Policy was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the policy.</response>
        [HttpPut("SaveLeaveEncashmentPolicyData")]
        public async Task<IActionResult> SaveLeave_Encashment_PolicyData([FromBody] LeaveEncashmentPolicyRequestModel Leave_Encashment_Policy)
        {
            return ResponseResult(await _IPayrollServices.SaveLeave_Encashment_PolicyData(Leave_Encashment_Policy));
        }
        /// <summary>
        /// Deletes a Leave Encashment Policy based on the provided Policy ID.
        /// </summary>
        /// <param name="PolicyID">
        /// The unique identifier of the Leave Encashment Policy to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The Leave Encashment Policy was deleted successfully.</response>
        /// <response code="400">If there is an error while deleting the policy, such as an invalid PolicyID.</response>
        [HttpPut("RemoveLeaveEncashmentPolicyData")]
        public async Task<IActionResult> RemoveLeaveEncashmentPolicyData(string PolicyID)
        {
            return ResponseResult(await _IPayrollServices.RemoveLeave_Encashment_PolicyData(PolicyID));
        }
        /// <summary>
        /// Retrieves a specific Leave Encashment Policy based on the provided Policy ID.
        /// </summary>
        /// <param name="PolicyID">
        /// The unique identifier of the Leave Encashment Policy to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the Leave Encashment Policy data.
        /// If the policy is not found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the requested Leave Encashment Policy.</response>
        /// <response code="204">If the Leave Encashment Policy with the specified ID is not found.</response>
        /// <response code="400">If there is an error while fetching the policy.</response>
        [HttpGet("GetLeaveEncashmentPolicyByID")]
        public async Task<IActionResult> GetLeave_Encashment_PolicyByID(string PolicyID)
        {
            return ResponseResult(await _IPayrollServices.GetLeave_Encashment_PolicyByID(PolicyID));
        }


        /// <summary>
        /// Retrieves a filtered and paginated list of Leave Encashment Policies based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the Leave Encashment Policy data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of Leave Encashment Policies.
        /// If no policies are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of Leave Encashment Policies.</response>
        /// <response code="204">If no Leave Encashment Policies are found.</response>
        /// <response code="400">If there is an error while fetching the policies.</response>
        [HttpGet("GetLeaveEncashmentPolicyFilter")]
        public async Task<IActionResult> GetLeave_Encashment_PolicyFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IPayrollServices.GetLeave_Encashment_PolicyFilter(PagedListCriteria));
        }


        /// <summary>
        /// Saves the details of a Leave Encashment Transaction. 
        /// If the transaction already exists, updates the existing record.
        /// </summary>
        /// <param name="Leave_Encashment_Transactions">
        /// The Leave Encashment Transaction details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The Leave Encashment Transaction was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the transaction.</response>
        [HttpPut("SaveLeaveEncashmentTransactionData")]
        public async Task<IActionResult> SaveLeave_Encashment_TransactionData([FromBody] LeaveEncashmentTransactionRequestModel Leave_Encashment_Transactions)
        {
            return ResponseResult(await _IPayrollServices.SaveLeave_Encashment_TransactionData(Leave_Encashment_Transactions));
        }
        /// <summary>
        /// Deletes a Leave Encashment Transaction based on the provided Encashment ID.
        /// </summary>
        /// <param name="EncashmentID">
        /// The unique identifier of the Leave Encashment Transaction to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The Leave Encashment Transaction was deleted successfully.</response>
        /// <response code="400">If there is an error while deleting the transaction, such as an invalid EncashmentID.</response>
        [HttpPut("RemoveLeaveEncashmentTransactionData")]
        public async Task<IActionResult> RemoveLeave_Encashment_TransactionData(string EncashmentID)
        {
            return ResponseResult(await _IPayrollServices.RemoveLeave_Encashment_TransactionData(EncashmentID));
        }

        /// <summary>
        /// Retrieves the leave encashment transaction details for a specific Encashment ID.
        /// </summary>
        /// <param name="EncashmentID">The unique identifier of the leave encashment transaction.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the transaction details.
        /// Returns 200 OK with the transaction if found, or an appropriate error response if not found.
        /// </returns>
        /// <response code="200">Returns the leave encashment transaction details.</response>
        /// <response code="400">If the EncashmentID is invalid.</response>
        /// <response code="404">If no transaction is found for the given ID.</response>
        [HttpGet("GetLeaveEncashmentTransacionByID")]
        public async Task<IActionResult> GetLeave_Encashment_TransactionByID(string EncashmentID)
        {
            return ResponseResult(await _IPayrollServices.GetLeave_Encashment_TransactionByID(EncashmentID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of Leave Encashment Transactions based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the Leave Encashment Transaction data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of Leave Encashment Transactions.
        /// If no transactions are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of Leave Encashment Transactions.</response>
        /// <response code="204">If no Leave Encashment Transactions are found.</response>
        /// <response code="400">If there is an error while fetching the transactions.</response>
        [HttpGet("GetLeaveEncashmentTransactionFilter")]
        public async Task<IActionResult> GetLeave_Encashment_TransactionFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IPayrollServices.GetLeave_Encashment_TransactionFilter(PagedListCriteria));
        }
    }
}
