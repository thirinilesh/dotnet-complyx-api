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
        /// <param name="tdsDeductor">
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
        /// <param name="pagedListCriteria">
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
        /// <param name="deductorId">
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
        /// <param name="tdsDeductor">
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
        /// <param name="pagedListCriteria">
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
        /// <param name="deductorId">
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
        /// <param name="tdsDeductee">
        /// The TDS deductee data to save or synchronize.
        /// </param>
        /// <param name="companyId">
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
        /// <param name="tdsDeductor">
        /// The TDS deductor data to save or synchronize.
        /// </param>
        /// <param name="companyId">
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
        /// <param name="tdsReturn">
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
        /// <param name="pagedListCriteria">
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
        /// <param name="returnId">
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
    }
}
