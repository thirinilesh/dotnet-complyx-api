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

using Microsoft.AspNetCore.Mvc;

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PayrollController : BaseController
    {
        private readonly IPayrollServices _IPayrollServices;

        public PayrollController(AppDbContext context, IPayrollServices IPayrollServices)
        {
            _IPayrollServices = IPayrollServices;
        }

        /// <summary>
        /// Save Payroll Data
        /// </summary>
        [HttpPut("SavePayrollData")]
        public async Task<IActionResult> SavePayrollData([FromBody] PayrollData Payroll)
        {
            return ResponseResult(await _IPayrollServices.SavePayrollData(Payroll));
        }

        /// <summary>
        /// Delete Payroll Data
        /// </summary>
        [HttpPut("RemovePayrollData")]
        public async Task<IActionResult> RemovePayrollData(string PayrollID)
        {
            return ResponseResult(await _IPayrollServices.RemovePayrollData(PayrollID));
        }

        /// <summary>
        /// Delete Payroll Data by EmployeeID and CompanyID
        /// </summary>
        [HttpPut("RemovePayrollDataByCompanyIDEmployeeID")]
        public async Task<IActionResult> RemovePayrollDataByCompanyIDEmployeeID(string CompanyID,string EmployeeID)
        {
            return ResponseResult(await _IPayrollServices.RemovePayrollDataByCompanyIDEmployeeID(CompanyID, EmployeeID));
        }

        /// <summary>
        /// Delete ALL Payroll Data by CompanyID
        /// </summary>
        [HttpPut("RemoveAllPayrollDataByCompanyID")]
        public async Task<IActionResult> RemoveAllPayrollDataByCompanyID(string CompanyID)
        {
            return ResponseResult(await _IPayrollServices.RemoveAllPayrollDataByCompanyID(CompanyID));
        }


        /// <summary>
        /// Edit Payroll Data by EmployeeID and CompanyID
        /// </summary>
        [HttpPut("EditPayrollDataByCompanyIDEmployeeID")]
        public async Task<IActionResult> EditPayrollDataByCompanyIDEmployeeID([FromBody]PayrollData data, string CompanyID, string EmployeeID, string PayrollID)
        {
            return ResponseResult(await _IPayrollServices.EditPayrollDataByCompanyIDEmployeeID(data,CompanyID, EmployeeID,PayrollID));
        }
    }
}
