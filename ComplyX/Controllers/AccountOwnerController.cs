using Azure.Core;
using ComplyX.BusinessLogic;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.BusinessLogic;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.ProductOwner;
using ComplyX_Businesss.Models.Company;
using ComplyX_Businesss.Models.Subcontractor;
using ComplyX_Businesss.Models.Plan;
using ComplyX_Businesss.Models.SubscriptionInvoices;
using ComplyX_Businesss.Models.SubscriptionPlan;
using ComplyX_Businesss.Models.PaymentTransaction;
using ComplyX_Businesss.Models.CustomerPayments;
using ComplyX_Businesss.Models.CompanyPartyRole;
using ComplyX_Businesss.Models.PartyMaster;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller to manage Account owners.
    /// Provides endpoints to create, update, delete, and retrieve account owner data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountOwnerController : BaseController
    {
        private readonly AppContext  _context;

        private readonly JwtTokenService  _tokenService;

        private readonly AccountOwnerLogic _logic;
        private readonly IProductOwner _IProductOwnere;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountOwnerController"/> class.
        /// </summary>
        /// <param name="tokenservice">Service to handle JWT token operations.</param>
        /// <param name="context">Database context for data access.</param>
        /// <param name="logic">Business logic for account owners.</param>
        /// <param name="IProductOwnere">Service to manage product owners.</param>

        public AccountOwnerController(JwtTokenService tokenservice, AppContext context, AccountOwnerLogic logic, IProductOwner IProductOwnere)
        {
            _tokenService = tokenservice;
            _context = context;
            _logic = logic;
            _IProductOwnere =  IProductOwnere;
        }


        /// <summary>
        /// Retrieves the paginated list of all product owners.
        /// </summary>
        /// <param name="PagedListCriteria">Pagination and filter criteria.</param>
        /// <returns>An IActionResult containing the list of product owners.</returns>

        [HttpGet]

        public async Task<IActionResult> GetProductOwnerList([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetProductOwnerList(PagedListCriteria));
        }


        /// <summary>
        /// Saves or updates product owner data.
        /// </summary>
        /// <param name="ProductOwners">
        /// The product owner details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns a success response when the product owner data is saved successfully.
        /// Returns a 400 Bad Request response if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Product owner data saved successfully.</response>
        /// <response code="400">If there is an error while saving the product owner data.</response>
        [HttpPut("SaveProductOwnerData")]
        [ProducesResponseType(typeof(ProductOwnerRequestModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveProductOwnerData([FromBody] ProductOwnerRequestModel ProductOwners)
        {
            return ResponseResult(await _IProductOwnere.SaveProductOwnerData(ProductOwners));
        }


        /// <summary>
        /// Retrieves a product owner by Product Owner ID.
        /// </summary>
        /// <param name="productOwnerId">The unique Product Owner ID.</param>
        /// <returns>An IActionResult containing the product owner details.</returns>
        [HttpGet("{ProductOwnerId}")]

        public async Task<IActionResult> GetAllProductOwnerByID(string ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.GetAllProductOwnerByID(ProductOwnerId));
        }
        /// <summary>
        /// Deletes product owner data based on the specified product owner identifier.
        /// </summary>
        /// <param name="ProductOwnerId">
        /// The unique identifier of the product owner to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the product owner data is deleted successfully, it returns a 200 OK response.
        /// If the specified product owner is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Product owner data deleted successfully.</response>
        /// <response code="204">If the specified product owner data is not found.</response>
        /// <response code="400">If there is an error while deleting the product owner data.</response>
        [HttpPut("RemoveProductOwnerData")]
        public async Task<IActionResult> RemoveProductOwnerData(string ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.RemoveProductOwnerData(ProductOwnerId));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of product owners.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve product owners.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of product owners.
        /// If no product owners are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of product owners.</response>
        /// <response code="204">If no product owners are found.</response>
        /// <response code="400">If there is an error while fetching the product owner data.</response>
        [HttpGet("GetAllProductOwnerFilter")]
        public async Task<IActionResult> GetAllProductOwnerFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllProductOwnerFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates company data.
        /// </summary>
        /// <param name="company">
        /// The company details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the company data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Company data saved successfully.</response>
        /// <response code="400">If there is an error while saving the company data.</response>
        [HttpPut("SaveCompanyData")]
        public async Task<IActionResult> SaveCompanyData([FromBody] CompanyRequestModel company)
        {
            return ResponseResult(await _IProductOwnere.SaveCompanyData(company));
        }

        /// <summary>
        /// Deletes company data based on the specified company identifier.
        /// </summary>
        /// <param name="CompanyaId">
        /// The unique identifier of the company to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the company data is deleted successfully, it returns a 200 OK response.
        /// If the specified company is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Company data deleted successfully.</response>
        /// <response code="204">If the specified company data is not found.</response>
        /// <response code="400">If there is an error while deleting the company data.</response>
        [HttpPut("RemoveCompanyData")]
        public async Task<IActionResult> RemoveCompanyData(string CompanyaId)
        {
            return ResponseResult(await _IProductOwnere.RemoveCompanyData(CompanyaId));
        }

        /// <summary>
        /// Retrieves the list of companies.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of companies.
        /// If no companies are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of companies.</response>
        /// <response code="204">If no companies are found.</response>
        /// <response code="400">If there is an error while fetching the companies.</response>
        [HttpGet("GetCompanyData")]
        public async Task<IActionResult> GetCompanyData([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetCompanyData(PagedListCriteria));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of companies.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve company data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of companies.
        /// If no companies are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of companies.</response>
        /// <response code="204">If no companies are found.</response>
        /// <response code="400">If there is an error while fetching the company data.</response>
        [HttpGet("GetAllCompanyDataFilter")]
        public async Task<IActionResult> GetAllCompanyDataFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllCompanyDataFilter(PagedListCriteria));
        }
        /// <summary>
        /// Retrieves the list of subscription plans.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of subscription plans.
        /// If no subscription plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of subscription plans.</response>
        /// <response code="204">If no subscription plans are found.</response>
        /// <response code="400">If there is an error while fetching the subscription plans.</response>
        [HttpGet("GetSubscriptionPlans")]
        public async Task<IActionResult> GetSubscriptionPlans()
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlans());
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of subscription plans.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve subscription plans.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of subscription plans.
        /// If no subscription plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of subscription plans.</response>
        /// <response code="204">If no subscription plans are found.</response>
        /// <response code="400">If there is an error while fetching the subscription plans.</response>
        [HttpGet("GetSubscriptionPlansFilter")]
        public async Task<IActionResult> GetSubscriptionPlansByFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlansByFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves subscription plans based on the specified filter criteria.
        /// </summary>
        /// <param name="request">
        /// The filter criteria used to retrieve subscription plans.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of subscription plans.
        /// If no subscription plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of subscription plans.</response>
        /// <response code="204">If no subscription plans are found.</response>
        /// <response code="400">If there is an error while fetching the subscription plans.</response>
        [HttpGet("GetSubscriptionPlanFilter")]
        public async Task<IActionResult> GetSubscriptionPlanFilter([FromBody] SubscriptionPlansFilterRequest request)
        {
            return ResponseResult(await _IProductOwnere.GetSubscriptionPlanFilter(request));
        }

        /// <summary>
        /// Saves or updates user subscription plan data.
        /// </summary>
        /// <param name="ProductOwnerSubscription">
        /// The user subscription plan details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the user subscription data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">User subscription plan data saved successfully.</response>
        /// <response code="400">If there is an error while saving the user subscription plan data.</response>
        [HttpPut("SaveUserSubscriptionData")]
        public async Task<IActionResult> SaveUserSubscriptionData([FromBody] ProductOwnerSubscription ProductOwnerSubscriptions)
        {
            return ResponseResult(await _IProductOwnere.SaveUserSubscriptionData(ProductOwnerSubscriptions));
        }

        /// <summary>
        /// Retrieves a list of all users along with their subscription plan details based on the specified filter criteria.
        /// </summary>
        /// <param name="ProductOwnerSubscriptionDto">
        /// The filter criteria used to retrieve user subscription plan details.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of users and their subscription plans.
        /// If no users or subscription plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of users with their subscription plan details.</response>
        /// <response code="204">If no users or subscription plans are found.</response>
        /// <response code="400">If there is an error while fetching the user subscription plan details.</response>
        [HttpPost("GetUserSubscriptionPlansDetails")]
        public async Task<IActionResult> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto)
        {
            return ResponseResult(await _IProductOwnere.GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto));
        }

        /// <summary>
        /// Retrieves the subscription plan details for a specific product owner based on the provided filter criteria.
        /// </summary>
        /// <param name="ProductOwnerSubscriptionDto">
        /// The filter criteria used to retrieve subscription plan details.
        /// </param>
        /// <param name="ProductOwnerId">
        /// The unique identifier of the product owner whose subscription details are being retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the product owner's subscription plan details.
        /// If no subscription details are found for the specified product owner, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the subscription plan details for the specified product owner.</response>
        /// <response code="204">If no subscription plan details are found for the specified product owner.</response>
        /// <response code="400">If there is an error while fetching the product owner subscription details.</response>

        [HttpPost("GetProductOwnerSubscriptionPlansDetails")]
        public async Task<IActionResult> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto, int ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto, ProductOwnerId));
        }

        /// <summary>
        /// Saves or updates subcontractor data.
        /// </summary>
        /// <param name="Subcontractors">
        /// The subcontractor details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the subcontractor data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subcontractor data saved successfully.</response>
        /// <response code="400">If there is an error while saving the subcontractor data.</response>
        [HttpPut("SaveSubcontractorData")]
        public async Task<IActionResult> SaveSubcontractorData([FromBody] SubcontractorRequestModel Subcontractors)
        {
            return ResponseResult(await _IProductOwnere.SaveSubcontractorData(Subcontractors));
        }

        /// <summary>
        /// Deletes subcontractor data based on the specified subcontractor identifier.
        /// </summary>
        /// <param name="SubcontractorsID">
        /// The unique identifier of the subcontractor to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the subcontractor data is deleted successfully, it returns a 200 OK response.
        /// If the specified subcontractor is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subcontractor data deleted successfully.</response>
        /// <response code="204">If the specified subcontractor data is not found.</response>
        /// <response code="400">If there is an error while deleting the subcontractor data.</response>
        [HttpPut("RemoveSubcontractorData")]
        public async Task<IActionResult> RemoveSubcontractorData(string SubcontractorsID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubcontractorData(SubcontractorsID));
        }
        /// <summary>
        /// Retrieves the list of subcontractors associated with the specified company.
        /// </summary>
        /// <param name="CompanyId">
        /// The unique identifier of the company whose subcontractors are being retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of subcontractors for the specified company.
        /// If no subcontractors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of subcontractors for the specified company.</response>
        /// <response code="204">If no subcontractors are found for the specified company.</response>
        /// <response code="400">If there is an error while fetching the subcontractor data.</response>
        [HttpGet("GetSubcontractors")]
        public async Task<IActionResult> GetSubcontractors(int CompanyId)
        {
            return ResponseResult(await _IProductOwnere.GetSubcontractors(CompanyId));
        }
        /// <summary>
        /// Retrieves the list of subcontractors.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of subcontractors.
        /// If no subcontractors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of subcontractors.</response>
        /// <response code="204">If no subcontractors are found.</response>
        /// <response code="400">If there is an error while fetching the subcontractors.</response>
        [HttpGet("GetSubcontractorsData")]
        public async Task<IActionResult> GetSubcontractorsData([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetSubcontractorsData(PagedListCriteria));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of subcontractors.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve subcontractor data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of subcontractors.
        /// If no subcontractors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of subcontractors.</response>
        /// <response code="204">If no subcontractors are found.</response>
        /// <response code="400">If there is an error while fetching the subcontractor data.</response>
        [HttpGet("GetSubcontractorsFilter")]
        public async Task<IActionResult> GetSubcontractorsFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetSubcontractorsFilter(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of subcontractors associated with a specific product owner.
        /// </summary>
        /// <param name="ProductOwnerId">
        /// The unique identifier of the product owner whose subcontractor details are being retrieved.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of subcontractors for the specified product owner.
        /// If no subcontractors are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of subcontractors for the specified product owner.</response>
        /// <response code="204">If no subcontractors are found for the specified product owner.</response>
        /// <response code="400">If there is an error while fetching the subcontractor data.</response>
        [HttpGet("GetProductOwnerSubcontractorsDetails")]
        public async Task<IActionResult> GetProductOwnerSubcontractorsDetails(int ProductOwnerId)
        {
            return ResponseResult(await _IProductOwnere.GetProductOwnerSubcontractorsDetails(ProductOwnerId));
        }

        /// <summary>
        /// Saves or updates plan data.
        /// </summary>
        /// <param name="Plans">
        /// The plan details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the plan data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Plan data saved successfully.</response>
        /// <response code="400">If there is an error while saving the plan data.</response>
        [HttpPut("SavePlansData")]
        public async Task<IActionResult> SavePlansData([FromBody] PlanRequestModel Plans)
        {
            return ResponseResult(await _IProductOwnere.SavePlansData(Plans));
        }

        /// <summary>
        /// Deletes plan data based on the specified plan identifier.
        /// </summary>
        /// <param name="PlansID">
        /// The unique identifier of the plan to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the plan data is deleted successfully, it returns a 200 OK response.
        /// If the specified plan is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Plan data deleted successfully.</response>
        /// <response code="204">If the specified plan data is not found.</response>
        /// <response code="400">If there is an error while deleting the plan data.</response>
        [HttpPut("RemovePlansData")]
        public async Task<IActionResult> RemovePlansData(string PlansID)
        {
            return ResponseResult(await _IProductOwnere.RemovePlansData(PlansID));
        }

        /// <summary>
        /// Retrieves the list of all plans.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all plans.
        /// If no plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all plans.</response>
        /// <response code="204">If no plans are found.</response>
        /// <response code="400">If there is an error while fetching the plans data.</response>
        [HttpGet("GetAllPlansData")]
        public async Task<IActionResult> GetAllPlansData()
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansData());
        }

        /// <summary>
        /// Retrieves plan data for the specified plan identifier.
        /// </summary>
        /// <param name="PlanID">
        /// The unique identifier of the plan to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the plan details for the specified PlanID.
        /// If no plan is found with the given ID, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the plan details for the specified PlanID.</response>
        /// <response code="204">If no plan is found with the given PlanID.</response>
        /// <response code="400">If there is an error while fetching the plan data.</response>
        [HttpGet("GetAllPlansDataByID")]
        public async Task<IActionResult> GetAllPlansDataByID(string PlanID)
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansDataByID(PlanID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of plans.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve plan data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of plans.
        /// If no plans are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of plans.</response>
        /// <response code="204">If no plans are found.</response>
        /// <response code="400">If there is an error while fetching the plan data.</response>
        [HttpGet("GetAllPlansDataFilter")]
        public async Task<IActionResult> GetAllPlansDataFilter([FromQuery]PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllPlansDataFilter(PagedListCriteria));
        }
        /// <summary>
        /// Saves or updates subscription invoice data.
        /// </summary>
        /// <param name="SubscriptionInvoices">
        /// The subscription invoice details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the subscription invoice data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subscription invoice data saved successfully.</response>
        /// <response code="400">If there is an error while saving the subscription invoice data.</response>
        [HttpPut("SaveSubscriptionInvoicesData")]
        public async Task<IActionResult> SaveSubscriptionInvoicesData([FromBody] SubscriptionInvoicesRequestModel SubscriptionInvoices)
        {
            return ResponseResult(await _IProductOwnere.SaveSubscriptionInvoicesData(SubscriptionInvoices));
        }

        /// <summary>
        /// Deletes subscription invoice data based on the specified invoice identifier.
        /// </summary>
        /// <param name="InvoiceID">
        /// The unique identifier of the subscription invoice to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the subscription invoice is deleted successfully, it returns a 200 OK response.
        /// If the specified invoice is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subscription invoice data deleted successfully.</response>
        /// <response code="204">If the specified subscription invoice is not found.</response>
        /// <response code="400">If there is an error while deleting the subscription invoice data.</response>
        [HttpPut("RemoveSubscriptionInvoicesData")]
        public async Task<IActionResult> RemoveSubscriptionInvoicesData(string InvoiceID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubscriptionInvoicesData(InvoiceID));
        }

        /// <summary>
        /// Retrieves the list of all subscription invoices.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all subscription invoices.
        /// If no subscription invoices are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all subscription invoices.</response>
        /// <response code="204">If no subscription invoices are found.</response>
        /// <response code="400">If there is an error while fetching the subscription invoices data.</response>
        [HttpGet("GetAllSubscriptionInvoicesData")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesData()
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesData());
        }

        /// <summary>
        /// Retrieves subscription invoice data for the specified invoice identifier.
        /// </summary>
        /// <param name="InvoiceID">
        /// The unique identifier of the subscription invoice to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the subscription invoice details for the specified InvoiceID.
        /// If no subscription invoice is found with the given ID, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the subscription invoice details for the specified InvoiceID.</response>
        /// <response code="204">If no subscription invoice is found with the given InvoiceID.</response>
        /// <response code="400">If there is an error while fetching the subscription invoice data.</response>
        [HttpGet("GetAllSubscriptionInvoicesDataByID")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesDataByID(string InvoiceID)
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesDataByID(InvoiceID));
        }
        /// <summary>
        /// Retrieves a filtered and paginated list of subscription invoices.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve subscription invoice data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of subscription invoices.
        /// If no subscription invoices are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of subscription invoices.</response>
        /// <response code="204">If no subscription invoices are found.</response>
        /// <response code="400">If there is an error while fetching the subscription invoice data.</response>
        [HttpGet("GetAllSubscriptionInvoicesFilter")]
        public async Task<IActionResult> GetAllSubscriptionInvoicesFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllSubscriptionInvoicesFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates subscription plan data.
        /// </summary>
        /// <param name="subscriptionPlans">
        /// The subscription plan details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the subscription plan data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subscription plan data saved successfully.</response>
        /// <response code="400">If there is an error while saving the subscription plan data.</response>
        [HttpPut("SaveSubscriptionPlansData")]
        public async Task<IActionResult> SaveSubscriptionPlansData([FromBody] SubscriptionPlanRequestModel subscriptionPlans)
        {
            return ResponseResult(await _IProductOwnere.SaveSubscriptionPlansData(subscriptionPlans));
        }

        /// <summary>
        /// Deletes subscription plan data based on the specified plan identifier.
        /// </summary>
        /// <param name="PlanID">
        /// The unique identifier of the subscription plan to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the subscription plan is deleted successfully, it returns a 200 OK response.
        /// If the specified plan is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Subscription plan data deleted successfully.</response>
        /// <response code="204">If the specified subscription plan is not found.</response>
        /// <response code="400">If there is an error while deleting the subscription plan data.</response>

        [HttpPut("RemoveSubscriptionPlansData")]
        public async Task<IActionResult> RemoveSubscriptionPlansData(string PlanID)
        {
            return ResponseResult(await _IProductOwnere.RemoveSubscriptionPlansData(PlanID));
        }

        /// <summary>
        /// Saves or updates payment transaction data.
        /// </summary>
        /// <param name="PaymentTransactions">
        /// The payment transaction details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the payment transaction data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Payment transaction data saved successfully.</response>
        /// <response code="400">If there is an error while saving the payment transaction data.</response>
        [HttpPut("SavePaymentTransactionData")]
        public async Task<IActionResult> SavePaymentTransactionData([FromBody] PaymentTransactionRequestModel PaymentTransactions)
        {
            return ResponseResult(await _IProductOwnere.SavePaymentTransactionData(PaymentTransactions));
        }

        /// <summary>
        /// Deletes payment transaction data based on the specified transaction identifier.
        /// </summary>
        /// <param name="TransactionID">
        /// The unique identifier of the payment transaction to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the payment transaction is deleted successfully, it returns a 200 OK response.
        /// If the specified transaction is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Payment transaction data deleted successfully.</response>
        /// <response code="204">If the specified payment transaction is not found.</response>
        /// <response code="400">If there is an error while deleting the payment transaction data.</response>

        [HttpPut("RemovePaymentTransactionData")]
        public async Task<IActionResult> RemovePaymentTransactionData(string TransactionID)
        {
            return ResponseResult(await _IProductOwnere.RemovePaymentTransactionData(TransactionID));
        }

        /// <summary>
        /// Retrieves the list of all payment transactions.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all payment transactions.
        /// If no payment transactions are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all payment transactions.</response>
        /// <response code="204">If no payment transactions are found.</response>
        /// <response code="400">If there is an error while fetching the payment transaction data.</response>
        [HttpGet("GetAllPaymentTransactionData")]
        public async Task<IActionResult> GetAllPaymentTransactionData()
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionData());
        }

        /// <summary>
        /// Retrieves payment transaction data for the specified transaction identifier.
        /// </summary>
        /// <param name="TransactionID">
        /// The unique identifier of the payment transaction to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the payment transaction details for the specified TransactionID.
        /// If no payment transaction is found with the given ID, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the payment transaction details for the specified TransactionID.</response>
        /// <response code="204">If no payment transaction is found with the given TransactionID.</response>
        /// <response code="400">If there is an error while fetching the payment transaction data.</response>
        [HttpGet("GetAllPaymentTransactionDataByID")]
        public async Task<IActionResult> GetAllPaymentTransactionDataByID(string TransactionID)
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionDataByID(TransactionID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of payment transactions.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve payment transaction data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of payment transactions.
        /// If no payment transactions are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of payment transactions.</response>
        /// <response code="204">If no payment transactions are found.</response>
        /// <response code="400">If there is an error while fetching the payment transaction data.</response>
        [HttpGet("GetAllPaymentTransactionFilter")]
        public async Task<IActionResult> GetAllPaymentTransactionFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllPaymentTransactionFilter(PagedListCriteria));
        }
        /// <summary>
        /// Saves or updates customer payment data.
        /// </summary>
        /// <param name="CustomerPayments">
        /// The customer payment details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// If the customer payment data is saved successfully, it returns a 200 OK response.
        /// If the request data is invalid or an error occurs during the save process,
        /// it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Customer payment data saved successfully.</response>
        /// <response code="400">If there is an error while saving the customer payment data.</response>
        [HttpPut("SaveCustomerPaymentsData")]
        public async Task<IActionResult> SaveCustomerPaymentsData([FromBody] CustomerPaymentsRequestModel CustomerPayments)
        {
            return ResponseResult(await _IProductOwnere.SaveCustomerPaymentsData(CustomerPayments));
        }


        /// <summary>
        /// Deletes customer payment data based on the specified payment identifier.
        /// </summary>
        /// <param name="PaymentID">
        /// The unique identifier of the customer payment to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the customer payment is deleted successfully, it returns a 200 OK response.
        /// If the specified payment is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Customer payment data deleted successfully.</response>
        /// <response code="204">If the specified customer payment is not found.</response>
        /// <response code="400">If there is an error while deleting the customer payment data.</response>
        [HttpPut("RemoveCustomerPaymentsData")]
        public async Task<IActionResult> RemoveCustomerPaymentsData(string PaymentID)
        {
            return ResponseResult(await _IProductOwnere.RemoveCustomerPaymentsData(PaymentID));
        }

        /// <summary>
        /// Retrieves the list of all payment transactions.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all payment transactions.
        /// If no payment transactions are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of all payment transactions.</response>
        /// <response code="204">If no payment transactions are found.</response>
        /// <response code="400">If there is an error while fetching the payment transaction data.</response>
        [HttpGet("GetAllCustomerPaymentsData")]
        public async Task<IActionResult> GetAllCustomerPaymentsData()
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentsData());
        }

        /// <summary>
        /// Retrieves customer payment data for the specified payment identifier.
        /// </summary>
        /// <param name="PaymentID">
        /// The unique identifier of the customer payment to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the customer payment details for the specified PaymentID.
        /// If no customer payment is found with the given ID, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the customer payment details for the specified PaymentID.</response>
        /// <response code="204">If no customer payment is found with the given PaymentID.</response>
        /// <response code="400">If there is an error while fetching the customer payment data.</response>
        [HttpGet("GetAllCustomerPaymentDataByID")]
        public async Task<IActionResult> GetAllCustomerPaymentDataByID(string PaymentID)
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentDataByID(PaymentID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of customer payments.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve customer payment data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of customer payments.
        /// If no customer payments are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the filtered list of customer payments.</response>
        /// <response code="204">If no customer payments are found.</response>
        /// <response code="400">If there is an error while fetching the customer payment data.</response>
        [HttpGet("GetAllCustomerPaymentFilter")]
        public async Task<IActionResult> GetAllCustomerPaymentFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllCustomerPaymentFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates party master data.
        /// </summary>
        /// <param name="PartyMaster">
        /// The party master details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns 200 OK when the data is saved successfully.
        /// Returns 400 Bad Request if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Party master data saved successfully.</response>
        /// <response code="400">Error occurred while saving party master data.</response>
        [HttpPut("SavePartyMasterData")]
        public async Task<IActionResult> SavePartyMasterData([FromBody] PartyMasterRequestModel PartyMaster)
        {
            return ResponseResult(await _IProductOwnere.SavePartyMasterData(PartyMaster, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Deletes customer payment data based on the specified payment identifier.
        /// </summary>
        /// <param name="PartyID">
        /// The unique identifier of the customer payment to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// If the customer payment is deleted successfully, it returns a 200 OK response.
        /// If the specified payment is not found, it returns a 204 No Content response.
        /// If an error occurs during the deletion process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Customer payment data deleted successfully.</response>
        /// <response code="204">If the specified customer payment is not found.</response>
        /// <response code="400">If there is an error while deleting the customer payment data.</response>
        [HttpPut("RemovePartyMasterData")]
        public async Task<IActionResult> RemovePartyMasterData(string PartyID)
        {
            return ResponseResult(await _IProductOwnere.RemovePartyMasterData(PartyID));
        }

        /// <summary>
        /// Retrieves the list of all party master records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all party master records.
        /// Returns 200 OK with data when records are found.
        /// Returns 204 No Content when no records are available.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of all party master records.</response>
        /// <response code="204">No party master records found.</response>
        /// <response code="400">Error occurred while fetching party master data.</response>
        [HttpGet("GetAllPartyMasterData")]
        public async Task<IActionResult> GetAllPartyMasterData()
        {
            return ResponseResult(await _IProductOwnere.GetAllPartyMasterData());
        }

        /// <summary>
        /// Retrieves party master data for the specified party identifier.
        /// </summary>
        /// <param name="PartyID">
        /// The unique identifier of the party to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the party master details for the specified party ID.
        /// Returns 200 OK when data is found.
        /// Returns 204 No Content when no record exists for the given party ID.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the party master details for the specified party ID.</response>
        /// <response code="204">No party master data found for the given party ID.</response>
        /// <response code="400">Error occurred while fetching party master data.</response>
        [HttpGet("GetAllPartyMastertDataByID")]
        public async Task<IActionResult> GetAllPartyMastertDataByID(string PartyID)
        {
            return ResponseResult(await _IProductOwnere.GetAllPartyMasterDataByID(PartyID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of party master records.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve party master data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of party master records.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records match the criteria.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the filtered list of party master records.</response>
        /// <response code="204">No party master records found.</response>
        /// <response code="400">Error occurred while fetching party master data.</response>
        [HttpGet("GetAllPatryMasterFilter")]
        public async Task<IActionResult> GetAllPatryMasterFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllPartyMasterFilter(PagedListCriteria));
        }

        /// <summary>
        /// Saves or updates company party role data.
        /// </summary>
        /// <param name="CompanyPartyRole">
        /// The company party role details to be saved or updated.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation.
        /// Returns 200 OK when the data is saved successfully.
        /// Returns 400 Bad Request if the request data is invalid or an error occurs.
        /// </returns>
        /// <response code="200">Company party role data saved successfully.</response>
        /// <response code="400">Error occurred while saving company party role data.</response>
        [HttpPut("SaveCompanyPartyRoleData")]
        public async Task<IActionResult> SaveCompanyPartyRoleData([FromBody] CompanyPartyRoleRequestModel CompanyPartyRole)
        {
            return ResponseResult(await _IProductOwnere.SaveCompanyPartyRoleData(CompanyPartyRole, User.Claims.GetUserId()));
        }

        /// <summary>
        /// Deletes party master data for the specified party identifier.
        /// </summary>
        /// <param name="CompanyPartyROleID">
        /// The unique identifier of the party to be deleted.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the delete operation.
        /// Returns 200 OK when the party is deleted successfully.
        /// Returns 204 No Content when the specified party is not found.
        /// Returns 400 Bad Request if an error occurs during deletion.
        /// </returns>
        /// <response code="200">Party master data deleted successfully.</response>
        /// <response code="204">Party master data not found.</response>
        /// <response code="400">Error occurred while deleting party master data.</response>
        [HttpPut("RemoveCompanyPartyRoleData")]
        public async Task<IActionResult> RemoveCompanyPartyRoleData(string CompanyPartyROleID)
        {
            return ResponseResult(await _IProductOwnere.RemoveCompanyPartyRoleData(CompanyPartyROleID));
        }

        /// <summary>
        /// Retrieves the list of all company party role records.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of all company party role records.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records are available.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the list of all company party role records.</response>
        /// <response code="204">No company party role records found.</response>
        /// <response code="400">Error occurred while fetching company party role data.</response>
        [HttpGet("GetAllCompanyPartyRoleData")]
        public async Task<IActionResult> GetAllCompanyPartyRoleData()
        {
            return ResponseResult(await _IProductOwnere.GetAllCompanyPartyRoleData());
        }

        /// <summary>
        /// Retrieves company party role data for the specified party identifier.
        /// </summary>
        /// <param name="CompanyPartyID">
        /// The unique identifier of the company party to retrieve.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the company party role details for the specified ID.
        /// Returns 200 OK when data is found.
        /// Returns 204 No Content when no record exists for the given ID.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the company party role details for the specified ID.</response>
        /// <response code="204">No company party role data found for the given ID.</response>
        /// <response code="400">Error occurred while fetching company party role data.</response>
        [HttpGet("GetAllCompanyPartyRoleDataByID")]
        public async Task<IActionResult> GetAllPartyMastevrtDataByID(string CompanyPartyID)
        {
            return ResponseResult(await _IProductOwnere.GetAllCompanyPartyRoleDataByID(CompanyPartyID));
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of company party role records.
        /// </summary>
        /// <param name="PagedListCriteria">
        /// The paging, sorting, and filtering criteria used to retrieve company party role data.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the filtered list of company party role records.
        /// Returns 200 OK when records are found.
        /// Returns 204 No Content when no records match the criteria.
        /// Returns 400 Bad Request if an error occurs during retrieval.
        /// </returns>
        /// <response code="200">Returns the filtered list of company party role records.</response>
        /// <response code="204">No company party role records found.</response>
        /// <response code="400">Error occurred while fetching company party role data.</response>
        [HttpGet("GetAllCompanyPartyRoleFilter")]
        public async Task<IActionResult> GetAllCompanyPartyRoleFilter([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IProductOwnere.GetAllCompanyPartyRoleFilter(PagedListCriteria));
        }

    }
}
