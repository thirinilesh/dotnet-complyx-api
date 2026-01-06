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
        /// Saves or updates TDS party data.
        /// </summary>
        /// <param name="TDS_Party">
        /// The TDS party details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the TDS party data is saved successfully.
        /// Returns a bad request response when the request data is invalid.
        /// </returns>
        /// <response code="200">TDS party data saved successfully.</response>
        /// <response code="400">Invalid request data or validation error.</response>
        /// <response code="500">An unexpected error occurred while processing the request.</response>
        [HttpPost("SaveTDS_PartyData")]
        public async Task<IActionResult> SaveTDS_PartyData([FromBody] TDS_Party TDS_Party)
        {
            return ResponseResult(await _ITTDSServices.SaveTDS_PartyData(TDS_Party));
        }

        /// <summary>
        /// Deletes TDS party data based on the specified party identifier.
        /// </summary>
        /// <param name="PartyID">
        /// The unique identifier of the TDS party to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// Returns a success response when the TDS party data is deleted successfully.
        /// Returns a no content response when the specified TDS party data is not found.
        /// Returns a bad request response when an error occurs during the deletion process.
        /// </returns>
        /// <response code="200">TDS party data deleted successfully.</response>
        /// <response code="204">Specified TDS party data was not found.</response>
        /// <response code="400">An error occurred while deleting the TDS party data.</response>
        /// <response code="400">If there is an error while deleting the product owner data.</response>
        [HttpPut("RemoveTDS_PartyData")]
        public async Task<IActionResult> RemoveTDS_PartyData(string PartyID)
        {
            return ResponseResult(await _ITTDSServices.RemoveTDS_PartyData(PartyID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of product owners.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve product owners.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of product owners.
        /// If no product owners are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of product owners.</response>
        /// <response code="204">If no product owners are found.</response>
        /// <response code="400">If there is an error while fetching the product owner data.</response>
        [HttpGet("GetTDS_PartyFilter")]
        public async Task<IActionResult> GetTDS_PartyFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _ITTDSServices.GetTDS_PartyFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of subcontractors associated with the specified company.
        /// </summary>
        /// <param name="PartyID">
        /// The unique identifier of the company whose subcontractors are being retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of subcontractors for the specified company.
        /// If no subcontractors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of subcontractors for the specified company.</response>
        /// <response code="204">If no subcontractors are found for the specified company.</response>
        /// <response code="400">If there is an error while fetching the subcontractor data.</response>
        [HttpGet("GetAllTDS_PartyData")]
        public async Task<IActionResult> GetAllTDS_PartyData(string  PartyID)
        {
            return ResponseResult(await _ITTDSServices.GetAllTDS_PartyData(PartyID));
        }

    }
}
