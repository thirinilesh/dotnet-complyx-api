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
using ComplyX_Businesss.Services.Interface;
using Microsoft.Graph.Models;

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ITTDSController : BaseController
    {
        private readonly AppDbContext _context;

        private readonly JwtTokenService _tokenService;

        private readonly ITTDSServices _ITTDSServices;
        public ITTDSController(JwtTokenService tokenservice, AppDbContext context, ITTDSServices ITTDSServices)
        {
            _tokenService = tokenservice;
            _context = context;
            _ITTDSServices = ITTDSServices;
        }
        /// <summary>
        /// Saves or updates TDS deductor data.
        /// </summary>
        /// <param name="TDSDeductor">
        /// The TDS deductor details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS deductor data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS deductor data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSDeductorData")]
        public async Task<IActionResult> SaveTDSDeductorData([FromBody] TDSDeductor TDSDeductor)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSDeductorData(TDSDeductor , User.Claims.GetUserId()));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of TDS deductors.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS deductors.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS deductors.
        /// If no TDS deductors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS deductors.</response>
        /// <response code="204">If no TDS deductors are found.</response>
        /// <response code="400">If there is an error while fetching the TDS deductor data.</response>
        [HttpGet("GetTDSDeductorFilter")]
        public async Task<IActionResult> GetTDSDeductorFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSDeductorFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS deductors associated with the specified deductor ID.
        /// </summary>
        /// <param name="DeductorID">
        /// The unique identifier of the TDS deductor.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS deductor data.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of TDS deductor data.</response>
        /// <response code="204">If no TDS deductor data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS deductor data.</response>
        [HttpGet("GetAllTDSDeductorData")]
        public async Task<IActionResult> GetAllTDSDeductorData(string  DeductorID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSDeductorData(DeductorID));
        }

        /// <summary>
        /// Saves or updates TDS deductor data.
        /// </summary>
        /// <param name="TDSDeductee">
        /// The TDS deductor details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS deductor data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS deductor data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSDeducteeData")]
        public async Task<IActionResult> SaveTDSDeducteeData([FromBody] TDSDeductee TDSDeductee)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSDeduteeData(TDSDeductee, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS deductors.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS deductors.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS deductors.
        /// If no TDS deductors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS deductors.</response>
        /// <response code="204">If no TDS deductors are found.</response>
        /// <response code="400">If there is an error while fetching the TDS deductor data.</response>
        [HttpGet("GetTDSDeduteeFilter")]
        public async Task<IActionResult> GetTDSDeduteeFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSDeduteeFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS deductors associated with the specified deductor ID.
        /// </summary>
        /// <param name="DeductorID">
        /// The unique identifier of the TDS deductor.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS deductor data.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of TDS deductor data.</response>
        /// <response code="204">If no TDS deductor data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS deductor data.</response>
        [HttpGet("GetAllTDSDeduteeData")]
        public async Task<IActionResult> GetAllTDSDeduteeData(string DeductorID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSDeduteeData(DeductorID));
        }

        /// <summary>
        /// Saves or synchronizes TDS deductee data for the specified company.
        /// </summary>
        /// <param name="TDSDeductee">
        /// The TDS deductee data to save or synchronize.
        /// </param>
        /// <param name="CompanyID">
        /// The unique identifier of the company.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the save/sync operation.
        /// Returns:
        /// - 200 OK with the result if successful.
        /// - 204 No Content if no data was processed.
        /// - 400 Bad Request if an error occurs.
        /// </returns>
        /// <response code="200">Returns the result of the save/sync operation.</response>
        /// <response code="204">If no TDS deductee data is found to process.</response>
        /// <response code="400">If there is an error during processing.</response>
        [HttpPost("SaveSyncTDSDeducteeData")]
        public async Task<IActionResult> SaveSyncTDSDeducteeData(int CompanyID)
        {
            return ResponseResult(await _ITTDSServices.SaveSyncTDSDeducteeData(CompanyID, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Saves or synchronizes TDS deductor data for the specified company.
        /// </summary>
        /// <param name="TDSDeductor">
        /// The TDS deductor data to save or synchronize.
        /// </param>
        /// <param name="CompanyID">
        /// The unique identifier of the company.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the result of the save/sync operation.
        /// Returns:
        /// - 200 OK with the result if successful.
        /// - 204 No Content if no data was processed.
        /// - 400 Bad Request if an error occurs.
        /// </returns>
        /// <response code="200">Returns the result of the save/sync operation.</response>
        /// <response code="204">If no TDS deductor data is found to process.</response>
        /// <response code="400">If there is an error during processing.</response>
        [HttpPost("SaveSyncTDSDeductorData")]
        public async Task<IActionResult> SaveSyncTDSDeductorData(int CompanyID)
        {
            return ResponseResult(await _ITTDSServices.SaveSyncTDSDeductorData(CompanyID, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Saves or updates TDS return data.
        /// </summary>
        /// <param name="TDSReturn">
        /// The TDS return details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS return data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS return data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSReturnData")]
        public async Task<IActionResult> SaveTDSReturnData([FromBody] TDSReturn TDSReturn)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSReturnData(TDSReturn, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS returns.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS returns.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS returns.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS returns.</response>
        /// <response code="204">If no TDS returns are found.</response>
        /// <response code="400">If there is an error while fetching the TDS return data.</response>
        [HttpGet("GetTDSReturnFilter")]
        public async Task<IActionResult> GetTDSReturnFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSReturnFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS return data associated with the specified return ID.
        /// </summary>
        /// <param name="ReturnID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS return data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS return data.</response>
        /// <response code="204">If no TDS return data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS return data.</response>
        [HttpGet("GetAllTDSReturnData")]
        public async Task<IActionResult> GetAllTDSReturnData(string ReturnID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSReturnData(ReturnID));
        }

        /// Saves or updates TDS return data.
        /// </summary>
        /// <param name="tdsEntry">
        /// The TDS return details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS return data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS return data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSEntryData")]
        public async Task<IActionResult> SaveTDSEntryData([FromBody] TDSEntry TDSEntry)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSEntryData(TDSEntry, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS returns.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS returns.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS returns.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS returns.</response>
        /// <response code="204">If no TDS returns are found.</response>
        /// <response code="400">If there is an error while fetching the TDS return data.</response>
        [HttpGet("GetTDSEntryFilter")]
        public async Task<IActionResult> GetTDSEntryFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSEntryFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS return data associated with the specified return ID.
        /// </summary>
        /// <param name="ReturnID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS return data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS return data.</response>
        /// <response code="204">If no TDS return data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS return data.</response>
        [HttpGet("GetAllTDSEntryData")]
        public async Task<IActionResult> GetAllTDSEntryData(string ReturnID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSEntryData(ReturnID));
        }

        /// <summary>
        /// Saves or updates TDS Challan data.
        /// </summary>
        /// <param name="TDSChallan">
        /// The TDS Challan details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS Challan data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS Challan data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSChallanData")]
        public async Task<IActionResult> SaveTDSChallanData([FromBody] TDSChallan TDSChallan)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSChallanData(TDSChallan, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS Challan data.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS Challan data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS Challan data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS Challan data.</response>
        /// <response code="204">If no TDS Challan data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Challan data.</response>
        [HttpGet("GetTDSChallanFilter")]
        public async Task<IActionResult> GetTDSChallanFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSChallanFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS Challan data associated with the specified return ID.
        /// </summary>
        /// <param name="ChallanID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS Challan data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS Challan data.</response>
        /// <response code="204">If no TDS Challan data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Challan data.</response>
        [HttpGet("GetAllTDSChallanData")]
        public async Task<IActionResult> GetAllTDSChallanData(string ChallanID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSChallanData(ChallanID));
        }

        /// <summary>
        /// Saves or updates TDS Return Challan data.
        /// </summary>
        /// <param name="TDSReturnChallan">
        /// The TDS Return Challan details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS Return Challan data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS Return Challan data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSReturnChallanData")]
        public async Task<IActionResult> SaveTDSReturnChallanData([FromBody] TDSReturnChallan TDSReturnChallan)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSReturnChallanData(TDSReturnChallan, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS Return Challan data.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS Return Challan data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS Return Challan data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS Return Challan data.</response>
        /// <response code="204">If no TDS Return Challan data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Return Challan data.</response>
        [HttpGet("GetTDSReturnChallanFilter")]
        public async Task<IActionResult> GetTDSReturnChallanFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSReturnChallanFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS Challan data associated with the specified return ID.
        /// </summary>
        /// <param name="ReturnChallanID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS Challan data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS Challan data.</response>
        /// <response code="204">If no TDS Challan data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Challan data.</response>
        [HttpGet("GetAllTDSReturnChallanData")]
        public async Task<IActionResult> GetAllTDSReturnChallanData(string ReturnChallanID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSReturnChallanData(ReturnChallanID));
        }

        /// <summary>
        /// Saves or updates TDS Return Entry data.
        /// </summary>
        /// <param name="TDSReturnEntry">
        /// The TDS Return Entry details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS Return Entry data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS Return Entry data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSReturnEntryData")]
        public async Task<IActionResult> SaveTDSReturnEntryData([FromBody] TDSReturnEntry TDSReturnEntry)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSReturnEntryData(TDSReturnEntry, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS Return Entry data.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS Return Entry data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS Return Entry data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS Return Entry data.</response>
        /// <response code="204">If no TDS Return Entry data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Return Entry data.</response>
        [HttpGet("GetTDSReturnEntryFilter")]
        public async Task<IActionResult> GetTDSReturnEntryFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSReturnEntryFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS Return Entry data associated with the specified return ID.
        /// </summary>
        /// <param name="ReturnEntryID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS Return Entry data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS Return Entry data.</response>
        /// <response code="204">If no TDS Return Entry data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Return Entry data.</response>
        [HttpGet("GetAllTDSReturnEntryData")]
        public async Task<IActionResult> GetAllTDSReturnEntryData(string ReturnEntryID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSReturnEntryData(ReturnEntryID));
        }

        /// <summary>
        /// Saves or updates TDS Challan Allocation data.
        /// </summary>
        /// <param name="TDSChallanAllocation">
        /// The TDS Challan Allocation details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS Challan Allocation data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS Challan Allocation data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSChallanAllocationData")]
        public async Task<IActionResult> SaveTDSChallanAllocationData([FromBody] TDSChallanAllocation TDSChallanAllocation)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSChallanAllocationData(TDSChallanAllocation, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS Return Entry data.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS Return Entry data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS Return Entry data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS Return Entry data.</response>
        /// <response code="204">If no TDS Return Entry data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Return Entry data.</response>
        [HttpGet("GetTDSChallanAllocationFilter")]
        public async Task<IActionResult> GetTDSChallanAllocationFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSChallanAllocationFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS Return Entry data associated with the specified return ID.
        /// </summary>
        /// <param name="AllocationID">
        /// The unique identifier of the TDS return.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS Return Entry data.
        /// Returns 204 No Content if no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS Return Entry data.</response>
        /// <response code="204">If no TDS Return Entry data is found.</response>
        /// <response code="400">If there is an error while fetching the TDS Return Entry data.</response>
        [HttpGet("GetAllTDSChallanAllocationData")]
        public async Task<IActionResult> GetAllTDSChallanAllocationData(string AllocationID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSChallanAllocationData(AllocationID));
        }

        /// <summary>
        /// Saves or updates TDS Rates data.
        /// </summary>
        /// <param name="tdsRates">
        /// The TDS Rates details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns 200 OK when the TDS Rates data is saved successfully.
        /// Returns 400 Bad Request if the request data is invalid.
        /// Returns 500 Internal Server Error if an unexpected error occurs.
        /// </returns>
        /// <response code="200">TDS Rates data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDSRatesData")]
        public async Task<IActionResult> SaveTDSRatesData([FromBody] TDSRates TDSRates)
        {
            return ResponseResult(await _ITTDSServices.SaveTDSRatesData(TDSRates, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of TDS Rates data.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve TDS Rates data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of TDS Rates data.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records are found.
        /// Returns 400 Bad Request if an error occurs while fetching data.
        /// </returns>
        /// <response code="200">Returns the filtered list of TDS Rates data.</response>
        /// <response code="204">No TDS Rates data found.</response>
        /// <response code="400">Error occurred while fetching TDS Rates data.</response>
        [HttpGet("GetTDSRatesFilter")]
        public async Task<IActionResult> GetTDSRatesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDSRatesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of TDS Rates data associated with the specified Tax ID.
        /// </summary>
        /// <param name="TaxID">
        /// The unique identifier of the TDS Tax.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of TDS Rates data.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records are found.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of TDS Rates data.</response>
        /// <response code="204">No TDS Rates data found.</response>
        /// <response code="400">Error occurred while fetching TDS Rates data.</response>
        [HttpGet("GetAllTDSRatesData")]
        public async Task<IActionResult> GetAllTDSRatesData(string TaxID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDSRatesData(TaxID));
        }
    }
}
