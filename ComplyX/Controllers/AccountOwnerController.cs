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
        public async Task<ActionResult> GetAll()
        {
             
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
        public async Task<IActionResult> RemoveProductOwnerData([FromBody] string ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.RemoveProductOwnerData(ProductOwnerId));
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

        /// <summary>
        /// Get List of Subscription Plans
        /// </summary>
        [HttpGet("GetSubscriptionPlans")]
        public async Task<IActionResult> GetSubscriptionPlans()
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlans());
        }

        /// <summary>
        /// Subscription Plans 
        /// </summary>
        [HttpPost("GetSubscriptionPlanFilter")]
        public async Task<IActionResult> GetSubscriptionPlanFilter([FromBody] SubscriptionPlansFilterRequest request)
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlanFilter(request));
        }

        /// <summary>
        /// Save User Subscription Plan Data
        /// </summary>
        [HttpPut("SaveUserSubscriptionData")]
        public async Task<IActionResult> SaveUserSubscriptionData([FromBody] ProductOwnerSubscriptions ProductOwnerSubscriptions)
        {
            return ResponseResult(await _IProductOwnere.SaveUserSubscriptionData(ProductOwnerSubscriptions));
        }

        /// <summary>
        /// Get List of all user's with Subscription Details
        /// </summary>
        [HttpPost("GetUserSubscriptionPlansDetails")]
        public async Task<IActionResult> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto)
        {
            return ResponseResult(await _IProductOwnere.GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto));
        }

        /// <summary>
        /// Get List of ProductOwner Subscription Details
        /// </summary>
        [HttpPost("GetProductOwnerSubscriptionPlansDetails")]
        public async Task<IActionResult> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto, int ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto, ProductOwnerId));
        }
    }
}
