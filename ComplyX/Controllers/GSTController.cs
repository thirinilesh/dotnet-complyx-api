using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using ComplyX.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GSTController : BaseController
    {
        private readonly AppDbContext _context;

        private readonly JwtTokenService _tokenService;

        private readonly AccountOwnerLogic _logic;
        private readonly IGSTServices _IGSTServices;
        public GSTController(JwtTokenService tokenservice, AppDbContext context, IGSTServices IGSTServices)
        {
            _tokenService = tokenservice;
            _context = context;
            _IGSTServices = IGSTServices;
        }
        /// <summary>
        /// Save GST HSNSAC Data
        /// </summary>
        [HttpPut("SaveGST_HSNSACData")]
        public async Task<IActionResult> SaveGST_HSNSACData([FromBody] GST_HSNSAC GST_HSNSAC)
        {
            return ResponseResult(await _IGSTServices.SaveGST_HSNSACData(GST_HSNSAC));
        }

        /// <summary>
        /// Delete GST HSNSAC Data
        /// </summary>
        [HttpPut("RemoveGST_HSNSACData")]
        public async Task<IActionResult> RemoveGST_HSNSACData(string CodeID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_HSNSACData(CodeID));
        }
        /// <summary>
        /// All GST_HSNSAC Data
        /// </summary>
        [HttpGet("GetGST_HSNSACFilterData")]
        public async Task<IActionResult> GetGST_HSNSACData()
        {
            return ResponseResult(await _IGSTServices.GetGST_HSNSACData());
        }

        /// <summary>
        /// All GST_HSNSAC Filter
        /// </summary>
        [HttpGet("GetGST_HSNSACFilter")]
        public async Task<IActionResult> GetGST_HSNSACFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_HSNSACFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save GST HSN_Mapping Data
        /// </summary>
        [HttpPut("SaveGST_HSN_MappingData")]
        public async Task<IActionResult> SaveGST_HSN_MappingData([FromBody] GST_HSN_Mapping GST_HSN_Mapping)
        {
            return ResponseResult(await _IGSTServices.SaveGST_HSN_MappingData(GST_HSN_Mapping));
        }

        /// <summary>
        /// Delete GST HSN_Mapping Data
        /// </summary>
        [HttpPut("RemoveGST_HSN_MappingData")]
        public async Task<IActionResult> RemoveGST_HSN_MappingData(string MappingID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_HSN_MappingData(MappingID));
        }

        /// <summary>
        /// All GST_HSN_Mapping Data
        /// </summary>
        [HttpGet("GetGST_HSN_MappingData")]
        public async Task<IActionResult> GetGST_HSN_MappingData()
        {
            return ResponseResult(await _IGSTServices.GetGST_HSN_MappingData());
        }

        /// <summary>
        /// All GST_HSN_Mapping Filter
        /// </summary>
        [HttpGet("GetGST_HSN_MappingFilter")]
        public async Task<IActionResult> GetGST_HSN_MappingFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_HSN_MappingFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save GST InvoiceSeries Data
        /// </summary>
        [HttpPut("SaveGST_InvoiceSeriesData")]
        public async Task<IActionResult> SaveGST_InvoiceSeriesData([FromBody] GST_InvoiceSeries GST_InvoiceSeries)
        {
            return ResponseResult(await _IGSTServices.SaveGST_InvoiceSeriesData(GST_InvoiceSeries));
        }

        /// <summary>
        /// Delete GST InvoiceSeries Data
        /// </summary>
        [HttpPut("RemoveGST_InvoiceSeriesData")]
        public async Task<IActionResult> RemoveGST_InvoiceSeriesData(string SeriesID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_InvoiceSeriesData(SeriesID));
        }

        /// <summary>
        /// All GST_InvoiceSeries Data
        /// </summary>
        [HttpGet("GetGST_InvoiceSeriesData")]
        public async Task<IActionResult> GetGST_InvoiceSeriesData()
        {
            return ResponseResult(await _IGSTServices.GetGST_InvoiceSeriesData());
        }

        /// <summary>
        /// All GST_InvoiceSeries Filter
        /// </summary>
        [HttpGet("GetGST_InvoiceSeriesFilter")]
        public async Task<IActionResult> GetGST_InvoiceSeriesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_InvoiceSeriesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save GST Purchase Data
        /// </summary>
        [HttpPut("SaveGST_PurchaseData")]
        public async Task<IActionResult> SaveGST_PurchaseData([FromBody] GST_Purchase GST_Purchase)
        {
            return ResponseResult(await _IGSTServices.SaveGST_PurchaseData(GST_Purchase));
        }

        /// <summary>
        /// Delete GST Purchase Data
        /// </summary>
        [HttpPut("RemoveGST_PurchaseData")]
        public async Task<IActionResult> RemoveGST_PurchaseData(string PurchaseID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_PurchaseData(PurchaseID));
        }

        /// <summary>
        /// All GST Purchase Data
        /// </summary>
        [HttpGet("GetGST_PurchaseData")]
        public async Task<IActionResult> GetGST_PurchaseData()
        {
            return ResponseResult(await _IGSTServices.GetGST_PurchaseData());
        }

        /// <summary>
        /// All GST Purchase Filter
        /// </summary>
        [HttpGet("GetGST_PurchaseFilter")]
        public async Task<IActionResult> GetGST_PurchaseFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_PurchaseFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save GST Returns Data
        /// </summary>
        [HttpPut("SaveGST_ReturnsData")]
        public async Task<IActionResult> SaveGST_ReturnsData([FromBody] GST_Returns GST_Returns)
        {
            return ResponseResult(await _IGSTServices.SaveGST_ReturnsData(GST_Returns));
        }

        /// <summary>
        /// Delete GST Returns Data
        /// </summary>
        [HttpPut("RemoveGST_ReturnsData")]
        public async Task<IActionResult> RemoveGST_ReturnsData(string ReturnID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_ReturnsData(ReturnID));
        }

        /// <summary>
        /// All GST Returns Data
        /// </summary>
        [HttpGet("GetGST_ReturnsData")]
        public async Task<IActionResult> GetGST_ReturnsData()
        {
            return ResponseResult(await _IGSTServices.GetGST_ReturnsData());
        }


        /// <summary>
        /// All GST Returns Filter
        /// </summary>
        [HttpGet("GetGST_ReturnsFilter")]
        public async Task<IActionResult> GetGST_ReturnsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_ReturnsFilter(PagedListCriteria));
        }


        /// <summary>
        /// Save GST Sales Data
        /// </summary>
        [HttpPut("SaveGST_SalesData")]
        public async Task<IActionResult> SaveGST_SalesData([FromBody] GST_Sales GST_Sales)
        {
            return ResponseResult(await _IGSTServices.SaveGST_SalesData(GST_Sales));
        }

        /// <summary>
        /// Delete GST Sales Data
        /// </summary>
        [HttpPut("RemoveGST_SalesData")]
        public async Task<IActionResult> RemoveGST_SalesData(string SaleID)
        {
            return ResponseResult(await _IGSTServices.RemoveGST_SalesData(SaleID));
        }

        /// <summary>
        /// All GST Sales Data
        /// </summary>
        [HttpGet("GetGST_SalesData")]
        public async Task<IActionResult> GetGST_SalesData()
        {
            return ResponseResult(await _IGSTServices.GetGST_SalesData());
        }

        /// <summary>
        /// All GST Sales Filter
        /// </summary>
        [HttpGet("GetGST_SalesFilter")]
        public async Task<IActionResult> GetGST_SalesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IGSTServices.GetGST_SalesFilter(PagedListCriteria));
        }

    }
}
