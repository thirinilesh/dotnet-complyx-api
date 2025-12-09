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

namespace ComplyX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   
    public class AccountOwnerController : BaseController
    {
        private readonly AppDbContext  _context;

        private readonly JwtTokenService  _tokenService;

        private readonly AccountOwnerLogic _logic;
        private readonly IProductOwner _IProductOwnere;
        public AccountOwnerController(JwtTokenService tokenservice, AppDbContext context, AccountOwnerLogic logic, IProductOwner IProductOwnere)
        {
            _tokenService = tokenservice;
            _context = context;
            _logic = logic;
            _IProductOwnere =  IProductOwnere;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<ProductOwners>>> GetAll(ProductOwners ProductOwners)
        {
            var validator = new ProductOwnersValidater();
            ValidationResult validationResult = validator.Validate(ProductOwners);
            ErrorResponse errorResponse = HandleValidationErrors(validationResult);
            if (errorResponse != null)
            {
                return BadRequest(errorResponse);
            }
            var owners = await _logic.GetAllAsync();
            return Ok(owners);
        }

        /// <summary>
        /// Save Product Data
        /// </summary>
        [HttpPut("SaveProductOwnerData")]
        public async Task<IActionResult> SaveProductOwnerData([FromBody] ProductOwners ProductOwners)
        {
            return ResponseResult(await _IProductOwnere.SaveProductOwnerData(ProductOwners));
        }

        /// <summary>
        /// Delete Product Data
        /// </summary>
        [HttpPut("RemoveProductOwnerData")]
        public async Task<IActionResult> RemoveProductOwnerData([FromBody] string AccountOwnerId)
        {
            return ResponseResult(await _IProductOwnere.RemoveProductOwnerData(AccountOwnerId));
        }

        /// <summary>
        /// Save Company Data
        /// </summary>
        [HttpPut("SaveCompanyData")]
        public async Task<IActionResult> SaveCompanyData([FromBody] Company company)
        {
            return ResponseResult(await _IProductOwnere.SaveCompanyData(company));
        }

        /// <summary>
        /// Delete Company Data
        /// </summary>
        [HttpPut("RemoveCompanyData")]
        public async Task<IActionResult> RemoveCompanyData([FromBody] string CompanyaId)
        {
            return ResponseResult(await _IProductOwnere.RemoveCompanyData(CompanyaId));
        }
    }
}
