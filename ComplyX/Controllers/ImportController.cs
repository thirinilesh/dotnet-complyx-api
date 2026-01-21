using ComplyX.Services;
using Microsoft.AspNetCore.Mvc;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ComplyX.Controllers;
using System.Linq;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ComplyX_Businesss.Helper;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing import operations.
    /// Provides endpoints to import data and manage related processes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImportController : BaseController
    {

        private readonly ImportServices _ImportServices;
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="ImportServices">The service for managing import operations.</param>
        public ImportController(AppDbContext context, ImportServices ImportServices)
        {
            _ImportServices = ImportServices;
        }
        /// <summary>
        /// uploads Employee import file
        /// </summary>
        /// <returns></returns>
        [HttpPost("importemployee")]
        public async Task<IActionResult> UploadEmployeeImportFile([FromForm] ImportModel request)
        {
          return  ResponseResult(await _ImportServices.UploadEmployeeImportFile(User.Claims.GetUserId(), request));
        }

    }
}
