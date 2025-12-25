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
    }
}
