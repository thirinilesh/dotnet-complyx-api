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
using Microsoft.AspNetCore.Mvc;
 

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeServices _IEmployeeServices;

        public EmployeesController(AppDbContext context, IEmployeeServices IEmployeeServices)
        {
            _IEmployeeServices = IEmployeeServices;
        }
        /// <summary>
        /// Save Employee Data
        /// </summary>
        [HttpPut("SaveEmployeeData")]
        public async Task<IActionResult> SaveEmployeeData([FromBody] Employees Employees)
        {
            return ResponseResult(await _IEmployeeServices.SaveEmployeeData(Employees));
        }

        /// <summary>
        /// Delete Employee Data
        /// </summary>
        [HttpPut("RemoveEmployeeData")]
        public async Task<IActionResult> RemoveEmployeeData(string EmployeeID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveEmployeeData(EmployeeID));
        }

        /// <summary>
        /// Get List for Employee Data by CompanyId
        /// </summary>
        [HttpPut("GetEmployeesByCompany")]
        public async Task<IActionResult> GetEmployeesByCompany(string CompanyID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompany(CompanyID));
        }

        /// <summary>
        ///  Get List for Employee Data by CompanyId and SubcontractorID
        /// </summary>
        [HttpPut("GetEmployeesByCompanySubcontractor")]
        public async Task<IActionResult> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompanySubcontractor(CompanyID, SubcontractorID));
        }
        /// <summary>
        ///  Get List for Employee Data by CompanyId and EmployeeID
        /// </summary>
        [HttpPut("GetEmployeesByCompanyEmployee")]
        public async Task<IActionResult> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeesByCompanyEmployee(CompanyID, EmployeeID));
        }

        /// <summary>
        /// Get List of Employee Data Filter
        /// </summary>
        [HttpGet("GetEmployeeDataFilter")]
        public async Task<IActionResult> GetEmployeeDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetEmployeeDataFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save Gratuity Policy  Data 
        /// </summary>
        [HttpPost("SaveGratuity_PolicyData")]
        public async Task<IActionResult> SaveGratuity_PolicyData([FromBody] Gratuity_Policy Gratuity_Policy)
        {
            return ResponseResult(await _IEmployeeServices.SaveGratuity_PolicyData(Gratuity_Policy));
        }

        /// <summary>
        /// Remove Gratuity Policy  Data 
        /// </summary>
        [HttpPost("RemoveGratuity_PolicyData")]
        public async Task<IActionResult> RemoveGratuity_PolicyData(string PolicyID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveGratuity_PolicyData(PolicyID));
        }
        /// <summary>
        /// Save Gratuity Policy  Data 
        /// </summary>
        [HttpPost("SaveGratuity_TransactionsData")]
        public async Task<IActionResult> SaveGratuity_TransactionsData([FromBody] Gratuity_Transactions Gratuity_Transactions)
        {
            return ResponseResult(await _IEmployeeServices.SaveGratuity_TransactionsData(Gratuity_Transactions));
        }
        /// <summary>
        /// Remove Gratuity Transactions  Data 
        /// </summary>
        [HttpPost("RemoveGratuity_TransactionsData")]
        public async Task<IActionResult> RemoveGratuity_TransactionsData(string GratuityID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveGratuity_TransactionsData(GratuityID));
        }

    }
}
