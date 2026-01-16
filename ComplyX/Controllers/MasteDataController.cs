using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using FluentValidation.Results;
using ComplyX.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lakshmi.Aca.Api.Controllers;

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MasteDataController : BaseController
    {
        private readonly AppDbContext _context;

        private readonly JwtTokenService _tokenService;

     
        private readonly MasterServices _MasterServices;
        public MasteDataController(JwtTokenService tokenservice, AppDbContext context, MasterServices MasterServices)
        {
            _tokenService = tokenservice;
            _context = context;
       
            _MasterServices = MasterServices;
        }

        /// <summary>
        /// Saves or updates employment type data.
        /// </summary>
        /// <param name="EmploymentTypes">
        /// The employment type details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the employment type data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Employment type data saved successfully.</response>
        /// <response code="400">If there is an error while saving the employment type data.</response>
        [HttpPost("SaveEmploymentTypesData")]
        public async Task<IActionResult> SaveEmploymentTypesData([FromBody] EmploymentTypes EmploymentTypes)
        {
            return ResponseResult(await _MasterServices.SaveEmploymentTypesData(EmploymentTypes));
        }

        /// <summary>
        /// Removes Employment data based on the specified subcontractor identifier.
        /// </summary>
        /// <param name="EmploymentID">
        /// The unique identifier of the subcontractor to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the subcontractor data is removed successfully, it returns a 200 OK response.
        /// If the specified subcontractor is not found, it returns a 204 No Content response.
        /// If an error occurs during the removal process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subcontractor data removed successfully.</response>
        /// <response code="204">If the specified subcontractor data is not found.</response>
        /// <response code="400">If there is an error while removing the subcontractor data.</response>
        [HttpPut("RemoveEmploymentTpesData")]
        public async Task<IActionResult> RemoveEmploymentTypesData(string EmploymentID)
        {
            return ResponseResult(await _MasterServices.RemoveEmploymentTypesData(EmploymentID));
        }
        /// <summary>
        /// Retrieves employment type data based on the specified employment type identifier.
        /// </summary>
        /// <param name="EmploymentTypeID">
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
        [HttpPost("GetEmploymentTypesData")]
        public async Task<IActionResult> GetEmploymentTypesData(string EmploymentTypeID)
        {
            return ResponseResult(await _MasterServices.GetEmploymentTypesData(EmploymentTypeID));
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
        [HttpPost("GetEmploymentTypesFilter")]
        public async Task<IActionResult> GetEmploymentTypesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _MasterServices.GetEmploymentTypesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates exit type data.
        /// </summary>
        /// <param name="ExitTypes">
        /// The exit type details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the exit type data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Exit type data saved successfully.</response>
        /// <response code="400">If there is an error while saving the exit type data.</response>
        [HttpPost("SaveExitTypesData")]
        public async Task<IActionResult> SaveExitTypesData([FromBody] ExitTypes ExitTypes)
        {
            return ResponseResult(await _MasterServices.SaveExitTypesData(ExitTypes));
        }

        /// <summary>
        /// Removes exit type data based on the specified exit type identifier.
        /// </summary>
        /// <param name="ExitTypeID">
        /// The unique identifier of the exit type to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the exit type data is removed successfully, it returns a 200 OK response.
        /// If the specified exit type is not found, it returns a 204 No Content response.
        /// If an error occurs during the removal process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Exit type data removed successfully.</response>
        /// <response code="204">If the specified exit type data is not found.</response>
        /// <response code="400">If there is an error while removing the exit type data.</response>
        [HttpPut("RemoveExitTypesData")]
        public async Task<IActionResult> RemoveExitTypesData(string ExitTypeID)
        {
            return ResponseResult(await _MasterServices.RemoveExitTypesData(ExitTypeID));
        }

        /// <summary>
        /// Retrieves subcontractor data based on the specified exit type identifier.
        /// </summary>
        /// <param name="ExitTypeID">
        /// The unique identifier of the exit type associated with the subcontractors to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the subcontractor data.
        /// If no subcontractors are found for the specified exit type, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subcontractor data retrieved successfully.</response>
        /// <response code="204">No subcontractors found for the specified exit type.</response>
        /// <response code="400">If there is an error while retrieving the subcontractor data.</response>
        [HttpPost("GetExitTypesData")]
        public async Task<IActionResult> GetExitTypesData(string ExitTypeID)
        {
            return ResponseResult(await _MasterServices.GetExitTypesData(ExitTypeID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of exit types based on the specified criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria for filtering, paging, and sorting the exit types.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of exit types.
        /// If no data matches the criteria, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filtered exit type data retrieved successfully.</response>
        /// <response code="204">No exit types found matching the filter criteria.</response>
        /// <response code="400">If there is an error while retrieving the exit type data.</response>
        [HttpPost("GetExitTypesFilter")]
        public async Task<IActionResult> GetExitTypesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _MasterServices.GetExitTypesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates filing status data.
        /// </summary>
        /// <param name="FilingStatuses">
        /// The filing status details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the filing status data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filing status data saved successfully.</response>
        /// <response code="400">If there is an error while saving the filing status data.</response>
        [HttpPost("SaveFillingStatusesData")]
        public async Task<IActionResult> SaveFillingStatusesData([FromBody] FilingStatuses FilingStatuses)
        {
            return ResponseResult(await _MasterServices.SaveFillingStatusesData(FilingStatuses));
        }

        /// <summary>
        /// Removes filing status data based on the specified filing status identifier.
        /// </summary>
        /// <param name="FilingStatusesID">
        /// The unique identifier of the filing status to be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the filing status data is removed successfully, it returns a 200 OK response.
        /// If the specified filing status is not found, it returns a 204 No Content response.
        /// If an error occurs during the removal process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filing status data removed successfully.</response>
        /// <response code="204">If the specified filing status data is not found.</response>
        /// <response code="400">If there is an error while removing the filing status data.</response>
        [HttpPut("RemoveFillingStatusesData")]
        public async Task<IActionResult> RemoveFillingStatusesData(string FilingStatusesID)
        {
            return ResponseResult(await _MasterServices.RemoveFillingStatusesData(FilingStatusesID));
        }

        /// <summary>
        /// Retrieves filing status data based on the specified filing status identifier.
        /// </summary>
        /// <param name="FilingStatusesID">
        /// The unique identifier of the filing status to be retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filing status data.
        /// If the specified filing status is not found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filing status data retrieved successfully.</response>
        /// <response code="204">If the specified filing status data is not found.</response>
        /// <response code="400">If there is an error while retrieving the filing status data.</response>
        [HttpPost("GetFillingStatusesData")]
        public async Task<IActionResult> GetFillingStatusesData(string FilingStatusesID)
        {
            return ResponseResult(await _MasterServices.GetFillingStatusesData(FilingStatusesID));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of filing statuses based on the specified criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria for filtering, paging, and sorting the filing statuses.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of filing statuses.
        /// If no data matches the criteria, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Filtered filing status data retrieved successfully.</response>
        /// <response code="204">No filing statuses found matching the filter criteria.</response>
        /// <response code="400">If there is an error while retrieving the filing status data.</response>
        [HttpPost("GetFillingStatusesFilter")]
        public async Task<IActionResult> GetFillingStatusesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _MasterServices.GetFillingStatusesFilter(PagedListCriteria));
        }
    }
}
