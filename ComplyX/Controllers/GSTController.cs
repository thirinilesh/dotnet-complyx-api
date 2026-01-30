using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.BusinessLogic;
using AppContext = ComplyX_Businesss.Helper.AppContext;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for handling GST-related operations.
    /// Provides endpoints to create, update, retrieve, and manage GST data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GSTController : BaseController
    {
        private readonly AppContext _context;

        private readonly JwtTokenService _tokenService;

        private readonly IGSTServices _IGSTServices;
        /// <summary>
        /// Initializes a new instance of the <see cref="GSTController"/> class.
        /// </summary>
        /// <param name="tokenservice">Service to handle JWT token operations.</param>
        /// <param name="context">Database context for accessing GST data.</param>
        /// <param name="IGSTServices">Service for GST business logic.</param>
        public GSTController(JwtTokenService tokenservice, AppContext context, IGSTServices IGSTServices)
        {
            _tokenService = tokenservice;
            _context = context;
            _IGSTServices = IGSTServices;
        }
        /// <summary>
        /// Saves the details of a GST HSNSAC record. 
        /// If the HSNSAC data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_HSNSAC">
        /// The GST HSNSAC details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST HSNSAC data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST HSNSAC data.</response>
        [HttpPut("SaveGST_HSNSACData")]
        public async Task<IActionResult> SaveGST_HSNSACData([FromBody] GST_HSNSAC GST_HSNSAC)
        {
            return ResponseResult(await _IGSTServices.SaveGST_HSNSACData(GST_HSNSAC));
        }

        /// <summary>
        /// Deletes a GST HSNSAC record based on the provided CodeID.
        /// </summary>
        /// <param name="CodeID">
        /// The unique identifier of the GST HSNSAC record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST HSNSAC data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST HSNSAC data.</response>
        /// <response code="404">If no GST HSNSAC data with the given CodeID is found.</response>
        [HttpPut("RemoveGST_HSNSACData")]
        public async Task<IActionResult> RemoveGST_HSNSACData(string CodeID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_HSNSACData(CodeID));
        }
        /// <summary>
        /// Retrieves the list of all GST HSNSAC records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST HSNSAC data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST HSNSAC records.</response>
        /// <response code="204">If no GST HSNSAC data is found.</response>
        /// <response code="400">If there is an error while fetching the GST HSNSAC data.</response>
        [HttpGet("GetGST_HSNSACFilterData")]
        public async Task<IActionResult> GetGST_HSNSACData()
        {
            return ResponseResult(await _IGSTServices.GetGST_HSNSACData());
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of GST HSNSAC records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST HSNSAC data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST HSNSAC records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST HSNSAC records.</response>
        /// <response code="204">If no GST HSNSAC data is found.</response>
        /// <response code="400">If there is an error while fetching the GST HSNSAC data.</response>
        [HttpGet("GetGST_HSNSACFilter")]
        public async Task<IActionResult> GetGST_HSNSACFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_HSNSACFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a GST HSN Mapping record. 
        /// If the HSN mapping data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_HSN_Mapping">
        /// The GST HSN Mapping details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST HSN Mapping data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST HSN Mapping data.</response>
        [HttpPut("SaveGST_HSN_MappingData")]
        public async Task<IActionResult> SaveGST_HSN_MappingData([FromBody] GST_HSN_Mapping GST_HSN_Mapping)
        {
            return ResponseResult(await _IGSTServices.SaveGST_HSN_MappingData(GST_HSN_Mapping));
        }

        /// <summary>
        /// Deletes a GST HSN Mapping record based on the provided MappingID.
        /// </summary>
        /// <param name="MappingID">
        /// The unique identifier of the GST HSN Mapping record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST HSN Mapping data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST HSN Mapping data.</response>
        /// <response code="404">If no GST HSN Mapping data with the given MappingID is found.</response>
        [HttpPut("RemoveGST_HSN_MappingData")]
        public async Task<IActionResult> RemoveGST_HSN_MappingData(string MappingID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_HSN_MappingData(MappingID));
        }

        /// <summary>
        /// Retrieves the list of all GST HSN Mapping records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST HSN Mapping data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST HSN Mapping records.</response>
        /// <response code="204">If no GST HSN Mapping data is found.</response>
        /// <response code="400">If there is an error while fetching the GST HSN Mapping data.</response>
        [HttpGet("GetGST_HSN_MappingData")]
        public async Task<IActionResult> GetGST_HSN_MappingData()
        {
            return ResponseResult(await _IGSTServices.GetGST_HSN_MappingData());
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of GST HSN Mapping records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST HSN Mapping data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST HSN Mapping records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST HSN Mapping records.</response>
        /// <response code="204">If no GST HSN Mapping data is found.</response>
        /// <response code="400">If there is an error while fetching the GST HSN Mapping data.</response>
        [HttpGet("GetGST_HSN_MappingFilter")]
        public async Task<IActionResult> GetGST_HSN_MappingFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_HSN_MappingFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a GST Invoice Series record. 
        /// If the invoice series data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_InvoiceSeries">
        /// The GST Invoice Series details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST Invoice Series data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST Invoice Series data.</response>
        [HttpPut("SaveGST_InvoiceSeriesData")]
        public async Task<IActionResult> SaveGST_InvoiceSeriesData([FromBody] GST_InvoiceSeries GST_InvoiceSeries)
        {
            return ResponseResult(await _IGSTServices.SaveGST_InvoiceSeriesData(GST_InvoiceSeries));
        }

        /// <summary>
        /// Deletes a GST Invoice Series record based on the provided SeriesID.
        /// </summary>
        /// <param name="SeriesID">
        /// The unique identifier of the GST Invoice Series record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST Invoice Series data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST Invoice Series data.</response>
        /// <response code="404">If no GST Invoice Series data with the given SeriesID is found.</response>
        [HttpPut("RemoveGST_InvoiceSeriesData")]
        public async Task<IActionResult> RemoveGST_InvoiceSeriesData(string SeriesID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_InvoiceSeriesData(SeriesID));
        }
        /// <summary>
        /// Retrieves the list of all GST Invoice Series records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST Invoice Series data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST Invoice Series records.</response>
        /// <response code="204">If no GST Invoice Series data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Invoice Series data.</response>
        [HttpGet("GetGST_InvoiceSeriesData")]
        public async Task<IActionResult> GetGST_InvoiceSeriesData()
        {
            return ResponseResult(await _IGSTServices.GetGST_InvoiceSeriesData());
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of GST Invoice Series records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST Invoice Series data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST Invoice Series records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST Invoice Series records.</response>
        /// <response code="204">If no GST Invoice Series data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Invoice Series data.</response>
        [HttpGet("GetGST_InvoiceSeriesFilter")]
        public async Task<IActionResult> GetGST_InvoiceSeriesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_InvoiceSeriesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a GST Purchase record. 
        /// If the purchase data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_Purchase">
        /// The GST Purchase details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST Purchase data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST Purchase data.</response>
        [HttpPut("SaveGST_PurchaseData")]
        public async Task<IActionResult> SaveGST_PurchaseData([FromBody] GST_Purchase GST_Purchase)
        {
            return ResponseResult(await _IGSTServices.SaveGST_PurchaseData(GST_Purchase));
        }

        /// <summary>
        /// Deletes a GST Purchase record based on the provided PurchaseID.
        /// </summary>
        /// <param name="PurchaseID">
        /// The unique identifier of the GST Purchase record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST Purchase data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST Purchase data.</response>
        /// <response code="404">If no GST Purchase data with the given PurchaseID is found.</response>
        [HttpPut("RemoveGST_PurchaseData")]
        public async Task<IActionResult> RemoveGST_PurchaseData(string PurchaseID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_PurchaseData(PurchaseID));
        }

        /// <summary>
        /// Retrieves the list of all GST Purchase records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST Purchase data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST Purchase records.</response>
        /// <response code="204">If no GST Purchase data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Purchase data.</response>
        [HttpGet("GetGST_PurchaseData")]
        public async Task<IActionResult> GetGST_PurchaseData()
        {
            return ResponseResult(await _IGSTServices.GetGST_PurchaseData());
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of GST Purchase records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST Purchase data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST Purchase records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST Purchase records.</response>
        /// <response code="204">If no GST Purchase data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Purchase data.</response>
        [HttpGet("GetGST_PurchaseFilter")]
        public async Task<IActionResult> GetGST_PurchaseFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_PurchaseFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves the details of a GST Returns record. 
        /// If the returns data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_Returns">
        /// The GST Returns details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST Returns data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST Returns data.</response>
        [HttpPut("SaveGST_ReturnsData")]
        public async Task<IActionResult> SaveGST_ReturnsData([FromBody] GST_Returns GST_Returns)
        {
            return ResponseResult(await _IGSTServices.SaveGST_ReturnsData(GST_Returns));
        }

        /// <summary>
        /// Deletes a GST Returns record based on the provided ReturnID.
        /// </summary>
        /// <param name="ReturnID">
        /// The unique identifier of the GST Returns record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST Returns data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST Returns data.</response>
        /// <response code="404">If no GST Returns data with the given ReturnID is found.</response>
        [HttpPut("RemoveGST_ReturnsData")]
        public async Task<IActionResult> RemoveGST_ReturnsData(string ReturnID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_ReturnsData(ReturnID));
        }

        /// <summary>
        /// Retrieves the list of all GST Returns records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST Returns data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST Returns records.</response>
        /// <response code="204">If no GST Returns data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Returns data.</response>
        [HttpGet("GetGST_ReturnsData")]
        public async Task<IActionResult> GetGST_ReturnsData()
        {
            return ResponseResult(await _IGSTServices.GetGST_ReturnsData());
        }


        /// <summary>
        /// Retrieves a filtered and paginated list of GST Returns records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST Returns data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST Returns records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST Returns records.</response>
        /// <response code="204">If no GST Returns data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Returns data.</response>
        [HttpGet("GetGST_ReturnsFilter")]
        public async Task<IActionResult> GetGST_ReturnsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_ReturnsFilter(PagedListCriteria));
        }


        /// <summary>
        /// Saves the details of a GST Sales record. 
        /// If the sales data already exists, updates the existing record.
        /// </summary>
        /// <param name="GST_Sales">
        /// The GST Sales details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the save or update operation.
        /// </returns>
        /// <response code="200">The GST Sales data was saved or updated successfully.</response>
        /// <response code="400">If there is an error while saving or updating the GST Sales data.</response>
        [HttpPut("SaveGST_SalesData")]
        public async Task<IActionResult> SaveGST_SalesData([FromBody] GST_Sales GST_Sales)
        {
            return ResponseResult(await _IGSTServices.SaveGST_SalesData(GST_Sales));
        }

        /// <summary>
        /// Deletes a GST Sales record based on the provided SaleID.
        /// </summary>
        /// <param name="SaleID">
        /// The unique identifier of the GST Sales record to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
        /// <response code="200">The GST Sales data was successfully deleted.</response>
        /// <response code="400">If there is an error while deleting the GST Sales data.</response>
        /// <response code="404">If no GST Sales data with the given SaleID is found.</response>
        [HttpPut("RemoveGST_SalesData")]
        public async Task<IActionResult> RemoveGST_SalesData(string SaleID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_SalesData(SaleID));
        }

        /// <summary>
        /// Retrieves the list of all GST Sales records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of GST Sales data.
        /// If no data is found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all GST Sales records.</response>
        /// <response code="204">If no GST Sales data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Sales data.</response>
        [HttpGet("GetGST_SalesData")]
        public async Task<IActionResult> GetGST_SalesData()
        {
            return ResponseResult(await _IGSTServices.GetGST_SalesData());
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of GST Sales records based on the provided criteria.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The criteria used to filter, sort, search, and paginate the GST Sales data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of GST Sales records.
        /// If no records are found, it returns a 204 No Content response.
        /// If an error occurs, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of GST Sales records.</response>
        /// <response code="204">If no GST Sales data is found.</response>
        /// <response code="400">If there is an error while fetching the GST Sales data.</response>
        [HttpGet("GetGST_SalesFilter")]
        public async Task<IActionResult> GetGST_SalesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_SalesFilter(PagedListCriteria));
        }

    }
}
