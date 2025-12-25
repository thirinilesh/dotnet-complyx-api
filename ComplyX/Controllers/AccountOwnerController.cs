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
        public async Task<IActionResult> RemoveProductOwnerData(string ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.RemoveProductOwnerData(ProductOwnerId));
        }
        /// <summary>
        /// All Product Owner Filter
        /// </summary>
        [HttpGet("GetAllProductOwnerFilter")]
        public async Task<IActionResult> GetAllProductOwnerFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllProductOwnerFilter(PagedListCriteria));
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
        public async Task<IActionResult> RemoveCompanyData(string CompanyaId)
        {
            return ResponseResult(await _IProductOwnere.RemoveCompanyData(CompanyaId));
        }
        /// <summary>
        /// All Company Data Filter
        /// </summary>
        [HttpGet("GetAllCompanyDataFilter")]
        public async Task<IActionResult> GetAllCompanyDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllCompanyDataFilter(PagedListCriteria));
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
        /// Get List of Subscription Plans Filter
        /// </summary>
        [HttpGet("GetSubscriptionPlansFilter")]
        public async Task<IActionResult> GetSubscriptionPlansByFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlansByFilter(PagedListCriteria));
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

        /// <summary>
        /// Save Subcontractor Data
        /// </summary>
        [HttpPut("SaveSubcontractorData")]
        public async Task<IActionResult> SaveSubcontractorData([FromBody] Subcontractors Subcontractors)
        {
            return ResponseResult(await _IProductOwnere.SaveSubcontractorData(Subcontractors));
        }

        /// <summary>
        /// Delete Subcontractor Data
        /// </summary>
        [HttpPut("RemoveSubcontractorData")]
        public async Task<IActionResult> RemoveSubcontractorData(string SubcontractorsID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubcontractorData(SubcontractorsID));
        }
        /// <summary>
        /// Get List of Subcontractors for CompanyId
        /// </summary>
        [HttpPost("GetSubcontractors")]
        public async Task<IActionResult> GetSubcontractors(int CompanyId)
        {
            return ResponseResult(await _IProductOwnere.GetSubcontractors(CompanyId));
        }

        /// <summary>
        /// Get List of Subcontractors Data Filter
        /// </summary>
        [HttpGet("GetSubcontractorsFilter")]
        public async Task<IActionResult> GetSubcontractorsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetSubcontractorsFilter(PagedListCriteria));
        }

        /// <summary>
        /// Get List of ProductOwner Subcontractors Details
        /// </summary>
        [HttpPost("GetProductOwnerSubcontractorsDetails")]
        public async Task<IActionResult> GetProductOwnerSubcontractorsDetails(int ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.GetProductOwnerSubcontractorsDetails(ProductOwnerId));
        }

        /// <summary>
        /// Save Plans Data
        /// </summary>
        [HttpPut("SavePlansData")]
        public async Task<IActionResult> SavePlansData([FromBody] Plans Plans)
        {
            return ResponseResult(await _IProductOwnere.SavePlansData(Plans));
        }

        /// <summary>
        /// Remove Plans Data
        /// </summary>
        [HttpPut("RemovePlansData")]
        public async Task<IActionResult> RemovePlansData(string PlansID)
        {
            return ResponseResult(await _IProductOwnere.RemovePlansData(PlansID));
        }

        /// <summary>
        /// All Plans Data
        /// </summary>
        [HttpPost("GetAllPlansData")]
        public async Task<IActionResult> GetAllPlansData()
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansData());
        }

        /// <summary>
        /// All Plans Data By ID
        /// </summary>
        [HttpPost("GetAllPlansDataByID")]
        public async Task<IActionResult> GetAllPlansDataByID(string PlanID)
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansDataByID(PlanID));
        }

        /// <summary>
        /// All Plans Data Filter
        /// </summary>
        [HttpGet("GetAllPlansDataFilter")]
        public async Task<IActionResult> GetAllPlansDataFilter([FromQuery]PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansDataFilter(PagedListCriteria));
        }
        /// <summary>
        /// Save SubscriptionInvoices Data
        /// </summary>
        [HttpPut("SaveSubscriptionInvoicesData")]
        public async Task<IActionResult> SaveSubscriptionInvoicesData([FromBody] SubscriptionInvoices SubscriptionInvoices)
        {
            return ResponseResult(await _IProductOwnere.SaveSubscriptionInvoicesData(SubscriptionInvoices));
        }

        /// <summary>
        /// Remove SubscriptionInvoices Data
        /// </summary>
        [HttpPut("RemoveSubscriptionInvoicesData")]
        public async Task<IActionResult> RemoveSubscriptionInvoicesData(string InvoiceID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubscriptionInvoicesData(InvoiceID));
        }

        /// <summary>
        /// All SubscriptionInvoices Data
        /// </summary>
        [HttpPost("GetAllSubscriptionInvoicesData")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesData()
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesData());
        }

        /// <summary>
        /// All SubscriptionInvoices Data By ID
        /// </summary>
        [HttpPost("GetAllSubscriptionInvoicesDataByID")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesDataByID(string InvoiceID)
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesDataByID(InvoiceID));
        }
        /// <summary>
        /// All SubscriptionInvoices Data Filter
        /// </summary>
        [HttpGet("GetAllSubscriptionInvoicesFilter")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Save SubscriptionPlans Data
        /// </summary>
        [HttpPut("SaveSubscriptionPlansData")]
        public async Task<IActionResult> SaveSubscriptionPlansData([FromBody] SubscriptionPlans subscriptionPlans)
        {
            return ResponseResult(await _IProductOwnere.SaveSubscriptionPlansData(subscriptionPlans));
        }

        /// <summary>
        /// Remove SubscriptionPlans Data
        /// </summary>
        [HttpPut("RemoveSubscriptionPlansData")]
        public async Task<IActionResult> RemoveSubscriptionPlansData(string PlanID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubscriptionPlansData(PlanID));
        }

        /// <summary>
        /// Save Payment Transaction Data
        /// </summary>
        [HttpPut("SavePaymentTransactionData")]
        public async Task<IActionResult> SavePaymentTransactionData([FromBody] PaymentTransactions PaymentTransactions)
        {
            return ResponseResult(await _IProductOwnere.SavePaymentTransactionData(PaymentTransactions));
        }

        /// <summary>
        /// Remove Payment Transaction Data
        /// </summary>
        [HttpPut("RemovePaymentTransactionData")]
        public async Task<IActionResult> RemovePaymentTransactionData(string TransactionID)
        {
            return ResponseResult(await _IProductOwnere.RemovePaymentTransactionData(TransactionID));
        }

        /// <summary>
        /// All PaymentTransaction Data
        /// </summary>
        [HttpPost("GetAllPaymentTransactionData")]
        public async Task<IActionResult> GetAllPaymentTransactionData()
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionData());
        }
        /// <summary>
        /// All PaymentTransaction Data By ID
        /// </summary>
        [HttpPost("GetAllPaymentTransactionDataByID")]
        public async Task<IActionResult> GetAllPaymentTransactionDataByID(string TransactionID)
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionDataByID(TransactionID));
        }

        /// <summary>
        /// All PaymentTransaction Data Filter
        /// </summary>
        [HttpGet("GetAllPaymentTransactionFilter")]
        public async Task<IActionResult> GetAllPaymentTransactionFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionFilter(PagedListCriteria));
        }
        /// <summary>
        /// Save Customer Payment Data
        /// </summary>
        [HttpPut("SaveCustomerPaymentsData")]
        public async Task<IActionResult> SaveCustomerPaymentsData([FromBody] CustomerPayments CustomerPayments)
        {
            return ResponseResult(await _IProductOwnere.SaveCustomerPaymentsData(CustomerPayments));
        }


        /// <summary>
        /// Remove Cutomer Payment Data
        /// </summary>
        [HttpPut("RemoveCustomerPaymentsData")]
        public async Task<IActionResult> RemoveCustomerPaymentsData(string PaymentID)
        {
            return ResponseResult(await _IProductOwnere.RemoveCustomerPaymentsData(PaymentID));
        }

        /// <summary>
        /// All Customer Payment Data
        /// </summary>
        [HttpPost("GetAllCustomerPaymentsData")]
        public async Task<IActionResult> GetAllCustomerPaymentsData()
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentsData());
        }

        /// <summary>
        /// All PaymentTransaction Data By ID
        /// </summary>
        [HttpPost("GetAllCustomerPaymentDataByID")]
        public async Task<IActionResult> GetAllCustomerPaymentDataByID(string PaymentID)
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentDataByID(PaymentID));
        }

        /// <summary>
        /// All Customer Payment Data Filter
        /// </summary>
        [HttpGet("GetAllCustomerPaymentFilter")]
        public async Task<IActionResult> GetAllCustomerPaymentFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentFilter(PagedListCriteria));
        }
    }
}
