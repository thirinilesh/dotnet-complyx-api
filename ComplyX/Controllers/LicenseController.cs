using Microsoft.AspNetCore.Mvc;
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
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;

namespace ComplyX.Shared.Controllers
{
    /// <summary>
    /// Controller for managing license operations.
    /// Provides endpoints to create, update, retrieve, and manage licenses.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LicenseController : BaseController
    {
        private readonly LicenseServices _LicenseServices;
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="LicenseServices">The service for managing license operations.</param>
        public LicenseController(AppDbContext context, LicenseServices LicenseServices)
        {
            _LicenseServices = LicenseServices;
        }
        /// <summary>
        /// Saves the details of a License Key record. 
        /// If the license key data already exists, updates the existing record.
        /// </summary>
        /// <param name="LicenseKeyMaster">
        /// The License Key details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The License Key data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the License Key data.</response>
        [HttpPut("SaveLicenseKeyMasterData")]
        public async Task<IActionResult> SaveLicenseKeyMasterData([FromBody] LicenseKeyMaster LicenseKeyMaster)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseKeyMasterData(LicenseKeyMaster));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of License Key records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the License Key data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of License Key records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of License Key records.</response>
        /// <response code="204">If no License Key data is found.</response>
        /// <response code="400">If there is an error while fetching the License Key data.</response>
        [HttpGet("GetLicenseKeyMasterFilter")]
        public async Task<IActionResult> GetLicenseKeyMasterFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseKeyMasterFilter(PagedListCriteria));
        }
        /// <summary>
        /// Saves the details of a License Key Activation record. 
        /// If the activation data already exists, updates the existing record.
        /// </summary>
        /// <param name="LicenseActivation">
        /// The License Key Activation details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The License Key Activation data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the License Key Activation data.</response>
        [HttpPut("SaveLicenseKeyActivationData")]
        public async Task<IActionResult> SaveLicenseKeyActivationData([FromBody] LicenseActivation LicenseActivation)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseKeyActivationData(LicenseActivation));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of License Key Activation records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the License Key Activation data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of License Key Activation records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of License Key Activation records.</response>
        /// <response code="204">If no License Key Activation data is found.</response>
        /// <response code="400">If there is an error while fetching the License Key Activation data.</response>
        [HttpGet("GetLicenseActivationFilter")]
        public async Task<IActionResult> GetLicenseActivationFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseActivationFilter(PagedListCriteria));
        }
        /// <summary>
        /// Saves the details of a License Audit Log record. 
        /// If the audit log already exists, updates the existing record.
        /// </summary>
        /// <param name="LicenseAuditLogs">
        /// The License Audit Log details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The License Audit Log data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the License Audit Log data.</response>
        [HttpPut("SaveLicenseAuditLogsData")]
        public async Task<IActionResult> SaveLicenseAuditLogsData([FromBody] LicenseAuditLogs LicenseAuditLogs)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseAuditLogsData(LicenseAuditLogs));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of License Audit Log records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the License Audit Log data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of License Audit Log records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of License Audit Log records.</response>
        /// <response code="204">If no License Audit Log data is found.</response>
        /// <response code="400">If there is an error while fetching the License Audit Log data.</response>
        [HttpGet("GetLicenseAuditLogsFilter")]
        public async Task<IActionResult> GetLicenseAuditLogsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseAuditLogsFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a Machine Binding record. 
        /// If the machine binding data already exists, updates the existing record.
        /// </summary>
        /// <param name="MachineBinding">
        /// The Machine Binding details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The Machine Binding data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the Machine Binding data.</response>
        [HttpPut("SaveMachineBindingData")]
        public async Task<IActionResult> SaveMachineBindingData([FromBody] MachineBinding MachineBinding)
        {
            return ResponseResult(await _LicenseServices.SaveMachineBindingData(MachineBinding));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of Machine Binding records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the Machine Binding data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of Machine Binding records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of Machine Binding records.</response>
        /// <response code="204">If no Machine Binding data is found.</response>
        /// <response code="400">If there is an error while fetching the Machine Binding data.</response>
        [HttpGet("GetMachineBindingFilter")]
        public async Task<IActionResult> GetMachineBindingFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetMachineBindingFilter(PagedListCriteria));
        }
    }
}
