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
        /// Get Gratuity Policy Data  
        /// </summary>
        [HttpGet("GetGratuity_Policy")]
        public async Task<IActionResult> GetGratuity_Policy(string PolicyID)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_Policy(PolicyID));
        }

        /// <summary>
        /// Get List of Gratuity Policy Data Filter
        /// </summary>
        [HttpGet("GetGratuity_PolicyFilter")]
        public async Task<IActionResult> GetGratuity_PolicyFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_PolicyFilter(PagedListCriteria));
        }


        /// <summary>
        /// Save Gratuity Transactions  Data 
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

        /// <summary>
        /// Get Gratuity Transactions Data  
        /// </summary>
        [HttpGet("GetGratuity_Transactions")]
        public async Task<IActionResult> GetGratuity_Transactions(string GratuityID)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_Transactions(GratuityID));
        }

        /// <summary>
        /// Get List of Gratuity Transactions Data Filter
        /// </summary>
        [HttpGet("GetGratuity_TransactionsFilter")]
        public async Task<IActionResult> GetGratuity_TransactionsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetGratuity_TransactionsFilter(PagedListCriteria));
        }
        /// <summary>
        /// Save FnF Calculations  Data 
        /// </summary>
        [HttpPost("SaveFnF_CalculationsData")]
        public async Task<IActionResult> SaveFnF_CalculationsData([FromBody] FnF_Calculations FnF_Calculations)
        {
            return ResponseResult(await _IEmployeeServices.SaveFnF_CalculationsData(FnF_Calculations));
        }
        /// <summary>
        /// Remove FnF Calculations  Data 
        /// </summary>
        [HttpPost("RemoveFnF_CalculationsData")]
        public async Task<IActionResult> RemoveFnF_CalculationsData(string FnFID)
        {
            return ResponseResult(await _IEmployeeServices.RemoveFnF_CalculationsData(FnFID));
        }

        /// <summary>
        /// Get FnF Calculations Data  
        /// </summary>
        [HttpGet("GetFnF_Calculations")]
        public async Task<IActionResult> GetFnF_Calculations(string FnFID)
        {
            return ResponseResult(await _IEmployeeServices.GetFnF_Calculations(FnFID));
        }

        /// <summary>
        /// Get List of FnF Calculations Data Filter
        /// </summary>
        [HttpGet("GetFnF_CalculationsFilter")]
        public async Task<IActionResult> GetFnF_CalculationsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IEmployeeServices.GetFnF_CalculationsFilter(PagedListCriteria));
        }
    }
}
