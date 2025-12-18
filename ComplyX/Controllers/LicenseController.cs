using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Data;
using ComplyX.Helper;
using ComplyX.Models;
using ComplyX.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

using Microsoft.AspNetCore.Mvc;

namespace ComplyX.Controllers
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
        /// Save License Key Activation Data
        /// </summary>
        [HttpPut("SaveLicenseKeyActivationData")]
        public async Task<IActionResult> SaveLicenseKeyActivationData([FromBody] LicenseActivation LicenseActivation)
        {
            return ResponseResult(await _LicenseServices.SaveLicenseKeyActivationData(LicenseActivation));
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
        /// Save MachineBinding Data
        /// </summary>
        [HttpPut("SaveMachineBindingData")]
        public async Task<IActionResult> SaveMachineBindingData([FromBody] MachineBinding MachineBinding)
        {
            return ResponseResult(await _LicenseServices.SaveMachineBindingData(MachineBinding));
        }
    }
}
