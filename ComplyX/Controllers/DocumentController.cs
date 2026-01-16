using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Controllers;
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
    public class DocumentController : BaseController
    {
        private readonly AppDbContext _context;

        private readonly JwtTokenService _tokenService;
        private readonly DocumentService _documentService;

        public DocumentController(JwtTokenService tokenservice, AppDbContext context, DocumentService documentService)
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
        public async Task<IActionResult> SavelegalDocumentData([FromBody] legalDocument legalDocument)
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

    }
}
