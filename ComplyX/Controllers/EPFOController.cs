using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Helper;
using ComplyX.Shared.Data;
using ComplyX.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
 
namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EPFOController : BaseController
    {
        private readonly EPFOServices _EPFOServices;

        public EPFOController(AppDbContext context, EPFOServices EPFOServices)
        {
            _EPFOServices = EPFOServices;
        }

        /// <summary>
        /// Save Company EPFO Data
        /// </summary>
        [HttpPut("SaveCompanyEPFOData")]
        public async Task<IActionResult> SaveCompanyEPFOData([FromBody] CompanyEPFO CompanyEPFO)
        {
            return ResponseResult(await _EPFOServices.SaveCompanyEPFOData(CompanyEPFO));
        }

        /// <summary>
        /// Delete CompanyEPFO Data
        /// </summary>
        [HttpPut("RemoveCompanyEPFOData")]
        public async Task<IActionResult> RemoveCompanyEPFOData(string CompanyEPFOId)
        {
            return ResponseResult(await _EPFOServices.RemoveCompanyEPFOData(CompanyEPFOId));
        }
        /// <summary>
        /// All Company EPFO Filter
        /// </summary>
        [HttpGet("GetAllCompanyEPFOFilter")]
        public async Task<IActionResult> GetAllCompanyEPFOFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllCompanyEPFOFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save Employee EPFO Data
        /// </summary>
        [HttpPost("SaveEmployeeEPFOData")]
        public async Task<IActionResult> SaveEmployeeEPFOData([FromBody] EmployeeEPFO EmployeeEPFO)
        {
            return ResponseResult(await _EPFOServices.SaveEmployeeEPFOData(EmployeeEPFO));
        }


        /// <summary>
        /// Delete Employee EPFO Data
        /// </summary>
        [HttpPut("RemoveEmployeeEPFOData")]
        public async Task<IActionResult> RemoveEmployeeEPFOData(string EmployeeEPFOId)
        {
            return ResponseResult(await _EPFOServices.RemoveEmployeeEPFOData(EmployeeEPFOId));
        }

        /// <summary>
        /// All Employee EPFO Filter
        /// </summary>
        [HttpGet("GetAllEmployeeEPFOFilter")]
        public async Task<IActionResult> GetAllEmployeeEPFOFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetAllEmployeeEPFOFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save EPFO ECR Data
        /// </summary>
        [HttpPut("SaveEPFOECRData")]
        public async Task<IActionResult> SaveEPFOECRData([FromBody] EPFOECRFile EPFOECRFile)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOECRData(EPFOECRFile));
        }


        /// <summary>
        /// Delete EPFO ECR Data
        /// </summary>
        [HttpPut("RemoveEPFOECRData")]
        public async Task<IActionResult> RemoveEPFOECRData(string ECRFileId)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOECRData(ECRFileId));
        }

        /// <summary>
        /// All EPFO ECR Data Filter
        /// </summary>
        [HttpGet("GetEPFOECRDataFilter")]
        public async Task<IActionResult> GetEPFOECRDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetEPFOECRDataFilter(PagedListCriteria));
        }


        /// <summary>
        /// Save EPFO Period Data
        /// </summary>
        [HttpPut("SaveEPFOPeriodData")]
        public async Task<IActionResult> SaveEPFOPeriodData([FromBody] EPFOPeriod EPFOPeriod)
        {
            return ResponseResult(await _EPFOServices.SaveEPFOPeriodData(EPFOPeriod, User.Claims.GetUserId()));
        }


        /// <summary>
        /// Delete EPFO Period Data
        /// </summary>
        [HttpPut("RemoveEPFOPeriodData")]
        public async Task<IActionResult> RemoveEPFOPeriodData(string EPFOPeriodId)
        {
            return ResponseResult(await _EPFOServices.RemoveEPFOPeriodData(EPFOPeriodId));
        }
        /// <summary>
        /// All EPFO Period Data Filter
        /// </summary>
        [HttpGet("GetEPFOPeriodDataFilter")]
        public async Task<IActionResult> GetEPFOPeriodDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _EPFOServices.GetEPFOPeriodDataFilter(PagedListCriteria));
        }
    }
}
