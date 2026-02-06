using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Controllers;
using ComplyX_Businesss.Services;
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
using ComplyX_Businesss.Models.LegalDocument;
using ComplyX_Businesss.Models.LegalDocumentVersion;
using ComplyX_Businesss.Models.LegalDocumentAcceptance;


namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing documents.
    /// Provides endpoints to create, retrieve, update, and delete documents.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DocumentController : BaseController
    {
        private readonly AppContext _context;

        private readonly JwtTokenService _tokenService;
        private readonly DocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentController"/> class.
        /// </summary>
        /// <param name="tokenservice">Service for managing JWT token operations.</param>
        /// <param name="context">The application database context.</param>
        /// <param name="documentService">The service for managing document operations.</param>
        public DocumentController(JwtTokenService tokenservice, AppContext context, DocumentService documentService)
        {
            _tokenService = tokenservice;
            _context = context;
            _documentService = documentService;
        }

        /// <summary>
        /// Saves or updates legal document data.
        /// </summary>
        /// <param name="legalDocument">
        /// The legal document details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns 200 OK when the legal document data is saved successfully.
        /// Returns 400 Bad Request if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Legal document data saved successfully.</response>
        /// <response code="400">Error occurred while saving legal document data.</response>
        [HttpPut("SavelegalDocumentData")]
        public async Task<IActionResult> SavelegalDocumentData([FromBody] LegalDocumentRequestModel legalDocument)
        {
            return ResponseResult(await _documentService.SavelegalDocumentData(legalDocument, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Deletes legal document data based on the specified legal document identifier.
        /// </summary>
        /// <param name="legalDocumentID">
        /// The unique identifier of the legal document to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// Returns 200 OK when the legal document data is deleted successfully.
        /// Returns 204 No Content when the specified legal document is not found.
        /// Returns 400 Bad Request if an error occurs during the deletion process.
        /// </returns>
        /// <response code="200">Legal document data deleted successfully.</response>
        /// <response code="204">Legal document data not found.</response>
        /// <response code="400">Error occurred while deleting legal document data.</response>
        [HttpPut("RemovelegalDocumentData")]
        public async Task<IActionResult> RemovelegalDocumentData(string legalDocumentID)
        {
            return ResponseResult(await _documentService.RemovelegalDocumentData(legalDocumentID));
        }

        /// <summary>
        /// Retrieves the list of legal documents.
        ///</summary>
        /// <param name="document_id">
        /// Optional identifier used to filter legal documents.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of legal documents.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no legal documents are available.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of legal documents.</response>
        /// <response code="204">No legal documents found.</response>
        /// <response code="400">Error occurred while fetching legal document data.</response>
        [HttpGet("GetAlllegalDocumentData")]
        public async Task<IActionResult> GetAlllegalDocumentData(string document_id)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentData(document_id));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of legal documents.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve legal document data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of legal documents.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records match the criteria.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the filtered list of legal documents.</response>
        /// <response code="204">No legal documents found.</response>
        /// <response code="400">Error occurred while fetching legal document data.</response>
        [HttpGet("GetAlllegalDocumentFilter")]
        public async Task<IActionResult> GetAlllegalDocumentFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentFilter(PagedListCriteria));
        }


        /// <summary>
        /// Saves or updates a legal document version.
        /// </summary>
        /// <param name="legalDocumentVersion">
        /// The legal document version details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// </returns>
        /// <response code="200">Legal document version saved successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("SavelegalDocumentVersionData")]
        public async Task<IActionResult> SavelegalDocumentVersionData([FromBody] LegalDocumentVersionRequestModel legalDocumentVersion)
        {
            return ResponseResult(await _documentService.SavelegalDocumentVersionData(legalDocumentVersion, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Deletes a legal document version based on the specified identifier.
        /// </summary>
        /// <param name="version_id">
        /// The unique identifier of the legal document version to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">Legal document version deleted successfully.</response>
        /// <response code="204">Legal document version not found.</response>
        /// <response code="400">Invalid request or error occurred during deletion.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("RemovelegalDocumentVersionData")]
        public async Task<IActionResult> RemovelegalDocumentVersionData(string version_id)
        {
            return ResponseResult(await _documentService.RemovelegalDocumentVersionData(version_id));
        }

        /// <summary>
        /// Retrieves the list of legal document versions.
        /// </summary>
        /// <param name="version_id">
        /// Optional identifier used to filter legal document versions.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of legal document versions.
        /// </returns>
        /// <response code="200">Returns the list of legal document versions.</response>
        /// <response code="204">No legal document versions found.</response>
        /// <response code="400">Error occurred while fetching legal document version data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAlllegalDocumentVersionData")]
        public async Task<IActionResult> GetAlllegalDocumentVersionData(string version_id)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentVersionData(version_id));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of legal document versions.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// Paging, sorting, and filtering criteria used to retrieve legal document versions.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of legal document versions.
        /// </returns>
        /// <response code="200">Returns the filtered list of legal document versions.</response>
        /// <response code="204">No legal document versions found.</response>
        /// <response code="400">Invalid request or error occurred during retrieval.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAlllegalDocumentVersionFilter")]
        public async Task<IActionResult> GetAlllegalDocumentVersionFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentVersionFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates a legal document acceptance record.
        /// </summary>
        /// <param name="legalDocumentAcceptance">
        /// The legal document acceptance details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// </returns>
        /// <response code="200">Legal document acceptance saved successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("SavelegalDocumentAcceptanceData")]
        public async Task<IActionResult> SavelegalDocumentAcceptanceData([FromBody] LegalDocumentAcceptanceRequestModel legalDocumentAcceptance)
        {
            return ResponseResult(await _documentService.SavelegalDocumentAcceptanceData(legalDocumentAcceptance, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Deletes a legal document acceptance record based on the specified identifier.
        /// </summary>
        /// <param name="acceptance_id">
        /// The unique identifier of the legal document acceptance to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">Legal document acceptance deleted successfully.</response>
        /// <response code="204">Legal document acceptance not found.</response>
        /// <response code="400">Invalid request or error occurred during deletion.</response>
        /// <response code="500">An internal server error occurred.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpPut("RemovelegalDocumentAcceptanceData")]
        public async Task<IActionResult> RemovelegalDocumentAcceptanceData(string acceptance_id)
        {
            return ResponseResult(await _documentService.RemovelegalDocumentAcceptanceData(acceptance_id));
        }

        /// <summary>
        /// Retrieves legal document acceptance records.
        /// </summary>
        /// <param name="acceptance_id">
        /// Optional identifier used to filter legal document acceptance records.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of legal document acceptance records.
        /// </returns>
        /// <response code="200">Returns the list of legal document acceptance records.</response>
        /// <response code="204">No legal document acceptance records found.</response>
        /// <response code="400">Invalid request or error occurred during retrieval.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAlllegalDocumentAcceptanceData")]
        public async Task<IActionResult> GetAlllegalDocumentAcceptanceData(string acceptance_id)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentAcceptanceData(acceptance_id));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of legal document acceptance records.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// Paging, sorting, and filtering criteria used to retrieve legal document acceptance data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of legal document acceptance records.
        /// </returns>
        /// <response code="200">Returns the filtered list of legal document acceptance records.</response>
        /// <response code="204">No legal document acceptance records found.</response>
        /// <response code="400">Invalid request or error occurred during retrieval.</response>
        /// <response code="500">An internal server error occurred.</response>
        [HttpGet("GetAlllegalDocumentAcceptanceFilter")]
        public async Task<IActionResult> GetAlllegalDocumentAcceptanceFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _documentService.GetAlllegalDocumentAcceptanceFilter(PagedListCriteria));
        }
    }
}
