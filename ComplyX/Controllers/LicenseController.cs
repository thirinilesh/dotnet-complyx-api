using Microsoft.AspNetCore.Mvc;
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
using ComplyX.Shared.Data;

namespace ComplyX.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LicenseController : BaseController
    {
        private readonly LicenseServices _LicenseServices;

        public LicenseController(AppDbContext context, LicenseServices LicenseServices)
        {
            _LicenseServices = LicenseServices;
        }
        /// <summary>
        /// Save LicenseKey Data
        /// </summary>
        [HttpPut("SaveLicenseKeyMasterData")]
        public async Task<IActionResult> SaveLicenseKeyMasterData([FromBody] LicenseKeyMaster LicenseKeyMaster)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseKeyMasterData(LicenseKeyMaster));
        }
        /// <summary>
        /// All LicenseKeyMaster Filter
        /// </summary>
        [HttpGet("GetLicenseKeyMasterFilter")]
        public async Task<IActionResult> GetLicenseKeyMasterFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseKeyMasterFilter(PagedListCriteria));
        }
        /// <summary>
        /// Save License Key Activation Data
        /// </summary>
        [HttpPut("SaveLicenseKeyActivationData")]
        public async Task<IActionResult> SaveLicenseKeyActivationData([FromBody] LicenseActivation LicenseActivation)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseKeyActivationData(LicenseActivation));
        }

        /// <summary>
        /// All LicenseActivation Filter
        /// </summary>
        [HttpGet("GetLicenseActivationFilter")]
        public async Task<IActionResult> GetLicenseActivationFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseActivationFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save License Audit Logs Data
        /// </summary>
        [HttpPut("SaveLicenseAuditLogsData")]
        public async Task<IActionResult> SaveLicenseAuditLogsData([FromBody] LicenseAuditLogs LicenseAuditLogs)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseAuditLogsData(LicenseAuditLogs));
        }

        /// <summary>
        /// All License Audit Logs Data Filter
        /// </summary>
        [HttpGet("GetLicenseAuditLogsFilter")]
        public async Task<IActionResult> GetLicenseAuditLogsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetLicenseAuditLogsFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save MachineBinding Data
        /// </summary>
        [HttpPut("SaveMachineBindingData")]
        public async Task<IActionResult> SaveMachineBindingData([FromBody] MachineBinding MachineBinding)
        {
            return ResponseResult(await _LicenseServices.SaveMachineBindingData(MachineBinding));
        }

        /// <summary>
        /// All MachineBinding Filter
        /// </summary>
        [HttpGet("GetMachineBindingFilter")]
        public async Task<IActionResult> GetMachineBindingFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _LicenseServices.GetMachineBindingFilter(PagedListCriteria));
        }
    }
}
