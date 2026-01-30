using ComplyX;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models.ProductOwner;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using ComplyX.Data.DbContexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Logging;
using Nest;
using EF = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;
using ComplyX_Businesss.Helper;
using System.Collections.Immutable;
using X.PagedList;
using AutoMapper.Configuration.Annotations;
using ComplyX_Businesss.Services;
using AppDbContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Repositories.UnitOfWork;
using ComplyX.Repositories.Repositories.Abstractions;
using System.Collections;
using AppContext = ComplyX_Businesss.Helper.AppContext;
//using NHibernate.Linq;



namespace ComplyX_Businesss.BusinessLogic
{
    public class AccountOwnerLogic : IProductOwner
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;
        private readonly Nest.Filter _filter;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ComplyX.Data.DbContexts.AppDbContext _appDbContext;

        public AccountOwnerLogic(AppContext context , Nest.Filter filter, IUnitOfWork UnitOfWork , ComplyX.Data.DbContexts.AppDbContext appDbContext)
        {
            _context = context;
            _filter = filter;
            _UnitOfWork = UnitOfWork;
            _appDbContext = appDbContext;
        }
        public async Task<List<ProductOwners>> GetAllAsync()
        {
            try
            {
                var result = await _context.ProductOwners.AsQueryable()
                                     .OrderByDescending(x => x.ProductOwnerId)
                                     .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching account owners", ex);
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>> GetAllProductOwnerData()
        {
            try
            {

                var owners = await _UnitOfWork.ProductOwnerRepositories.GetQueryable().AsNoTracking()
                        .Select(x => new ProductOwnerResponseModel
                        {
                            ProductOwnerId = x.ProductOwnerId,
                            OwnerName = x.OwnerName,
                            Email = x.Email,
                            Mobile = x.Mobile,
                            OrganizationName = x.OrganizationName
                        })
                        .ToListAsync();


                return new ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>
                {
                    IsSuccess = true,
                    Result = owners,
                    Message = "Subscription Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwners ProductOwners)
        {
            var response = new ManagerBaseResponse<List<ProductOwners>>();

            try
            {
                if (ProductOwners.ProductOwnerId == 0)
                {
                    // Insert
                    ProductOwners _model = new ProductOwners();
                    _model.OwnerName = ProductOwners.OwnerName;
                    _model.Email = ProductOwners.Email;
                    _model.Mobile = ProductOwners.Mobile;
                    _model.OrganizationName = ProductOwners.OrganizationName;
                    _model.LegalName = ProductOwners.LegalName;
                    _model.RegistrationId = ProductOwners.RegistrationId;
                    _model.OrganizationType = ProductOwners.OrganizationType;
                    _model.Address = ProductOwners.Address;
                    _model.State = ProductOwners.State;
                    _model.City = ProductOwners.City;
                    _model.Pincode = ProductOwners.Pincode;
                    _model.Country = ProductOwners.Country;
                    _model.IsActive = ProductOwners.IsActive;
                    _model.CreatedBy = ProductOwners.CreatedBy;
                    _model.UpdatedBy = ProductOwners.UpdatedBy;
                    _model.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                    _model.SubscriptionPlan = ProductOwners.SubscriptionPlan;
                    _model.SubscriptionStart = ProductOwners.SubscriptionStart;
                    _model.SubscriptionExpiry = ProductOwners.SubscriptionExpiry;
                    _model.MaxCompanies = ProductOwners.MaxCompanies;
                    _model.MaxStorageMB = ProductOwners.MaxStorageMB;
                    _model.MaxUsers = ProductOwners.MaxUsers;
                    _model.AllowCloudBackup = ProductOwners.AllowCloudBackup;
                    _model.AllowTDSModule = ProductOwners.AllowTDSModule;
                    _model.AllowGSTModule = ProductOwners.AllowGSTModule;
                    _model.AllowCLRAModule = ProductOwners.AllowCLRAModule;
                    _model.AllowPayrollModule = ProductOwners.AllowPayrollModule;
                    _model.AllowDSCSigning = ProductOwners.AllowDSCSigning;

                    _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.ProductOwners
                        .Where(x => x.ProductOwnerId == ProductOwners.ProductOwnerId)
                        .FirstOrDefault();
                    originalTerm.OwnerName = ProductOwners.OwnerName;
                    originalTerm.Email = ProductOwners.Email;
                    originalTerm.Mobile = ProductOwners.Mobile;
                    originalTerm.OrganizationName = ProductOwners.OrganizationName;
                    originalTerm.LegalName = ProductOwners.LegalName;
                    originalTerm.RegistrationId = ProductOwners.RegistrationId;
                    originalTerm.OrganizationType = ProductOwners.OrganizationType;
                    originalTerm.Address = ProductOwners.Address;
                    originalTerm.State = ProductOwners.State;
                    originalTerm.City = ProductOwners.City;
                    originalTerm.Pincode = ProductOwners.Pincode;
                    originalTerm.Country = ProductOwners.Country;
                    originalTerm.IsActive = ProductOwners.IsActive;
                    originalTerm.UpdatedBy = ProductOwners.UpdatedBy;
                    originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                    originalTerm.IsActive = ProductOwners.IsActive;
                    originalTerm.SubscriptionPlan = ProductOwners.SubscriptionPlan;
                    originalTerm.SubscriptionStart = ProductOwners.SubscriptionStart;
                    originalTerm.SubscriptionExpiry = ProductOwners.SubscriptionExpiry;
                    originalTerm.MaxCompanies = ProductOwners.MaxCompanies;
                    originalTerm.MaxStorageMB = ProductOwners.MaxStorageMB;
                    originalTerm.MaxUsers = ProductOwners.MaxUsers;
                    originalTerm.AllowCloudBackup = ProductOwners.AllowCloudBackup;
                    originalTerm.AllowTDSModule = ProductOwners.AllowTDSModule;
                    originalTerm.AllowGSTModule = ProductOwners.AllowGSTModule;
                    originalTerm.AllowCLRAModule = ProductOwners.AllowCLRAModule;
                    originalTerm.AllowPayrollModule = ProductOwners.AllowPayrollModule;
                    originalTerm.AllowDSCSigning = ProductOwners.AllowDSCSigning;

                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }

                return Task.FromResult(new ManagerBaseResponse<bool> 
                {
                    Result = true,
                    Message = "Product Details Saved Successfully."
                });
            }
            catch (Exception e)
            {
                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                });
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string ProductOwnerId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.ProductOwners.Where(x => x.ProductOwnerId.ToString() == ProductOwnerId).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message =  "Product Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.ProductOwners.RemoveRange(product);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Product Data Removed Successfully."      , 
                    };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ProductOwners>>> GetAllProductOwnerFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.ProductOwners.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.OwnerName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ProductOwnerId);

                PageListed<ProductOwners> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ProductOwners>>
                {
                    Result = result.Data,
                    Message = "Product Owner Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<ProductOwners>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCompanyData(Company company)
        {
            var response = new ManagerBaseResponse<List<Company>>();

            try
            {
                var accountid = await _context.ProductOwners.FirstOrDefaultAsync(x => x.ProductOwnerId == company.ProductOwnerId);

                if (accountid == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Account Ownere is not found.",
                    };
                }
                else
                {
                    if (company.CompanyID == 0)
                    {
                        // Insert
                        Company _model = new Company();
                        _model.Name = company.Name;
                        _model.Domain = company.Domain;
                        _model.ContactEmail = company.ContactEmail;
                        _model.ContactPhone = company.ContactPhone;
                        _model.Address = company.Address;
                        _model.State = company.State;
                        _model.PAN = company.PAN;
                        _model.GSTIN = company.GSTIN;
                        _model.IsActive = company.IsActive;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.ProductOwnerId = accountid.ProductOwnerId;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Companies
                            .Where(x => x.CompanyID == company.CompanyID)
                            .FirstOrDefault();
                        originalTerm.Name = company.Name;
                        originalTerm.Domain = company.Domain;
                        originalTerm.ContactEmail = company.ContactEmail;
                        originalTerm.ContactPhone = company.ContactPhone;
                        originalTerm.Address = company.Address;
                        originalTerm.State = company.State;
                        originalTerm.PAN = company.PAN;
                        originalTerm.GSTIN = company.GSTIN;
                        originalTerm.IsActive = company.IsActive;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.ProductOwnerId = accountid.ProductOwnerId;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return   new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Company Details Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return  new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveCompanyData(string CompanyId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Company = await _context.Companies.Where(x => x.CompanyID.ToString() == CompanyId).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Product Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Companies.RemoveRange(Company);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Product Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Company>>> GetAllCompanyDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Companies.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyID);

                PageListed<Company> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Company>>
                {
                    Result = result.Data,
                    Message = "Company Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<Company>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlans()
        {
            try
            {
                var  plans = await _context.SubscriptionPlans.AsQueryable().OrderBy(x => x.PlanId) .ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionPlans>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Subscription Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionPlans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }            
        }
        public async Task<ManagerBaseResponse<IEnumerable<SubscriptionPlans>>> GetSubscriptionPlansByFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.SubscriptionPlans.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PlanName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PlanId);

                PageListed<SubscriptionPlans> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<SubscriptionPlans>>
                {
                    Result = result.Data,
                    Message = "SubscriptionPlans Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<SubscriptionPlans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlanFilter(SubscriptionPlansFilterRequest request)
        {
            try
            {
                List<SubscriptionPlans> result = _context.SubscriptionPlans.AsQueryable().ToList();
                List <SubscriptionPlans> query = new List<SubscriptionPlans>();

                if (request.PlanId != null && request.PlanId != 0 )
                {
                   query  = result.Where(x => x.PlanId == request.PlanId.Value).ToList();
                }


                if (((!string.IsNullOrEmpty(request.PlanName) ) || (!string.IsNullOrWhiteSpace(request.PlanName)) ) && (request.PlanName != "string"))
                {
                    query = result.Where(x => x.PlanName.ToLower().Contains(request.PlanName.ToLower())).ToList();
                }


                if (((!string.IsNullOrEmpty(request.PlanCode)) || (!string.IsNullOrWhiteSpace(request.PlanCode))) && (request.PlanCode != "string"))
                {
                    query = result.Where(x => x.PlanCode.ToLower().Contains(request.PlanCode.ToLower())).ToList();
                }



                return new ManagerBaseResponse<List<SubscriptionPlans>>
                {
                    IsSuccess = true,
                    Result = query,
                    Message = "Subscription Plans Fetched Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionPlans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveUserSubscriptionData(ProductOwnerSubscriptions ProductOwnerSubscriptions)
        {
            var response = new ManagerBaseResponse<List<ProductOwnerSubscriptions>>();

            try
            {
                var plan = _context.ProductOwnerSubscriptions.AsQueryable().Where(x => x.ProductOwnerId == ProductOwnerSubscriptions.ProductOwnerId && x.PlanId == ProductOwnerSubscriptions.PlanId); 
                if(plan == null)               
                {               
                    if (ProductOwnerSubscriptions.SubscriptionId == 0)
                    {
                        // Insert
                        ProductOwnerSubscriptions _model = new ProductOwnerSubscriptions();
                        _model.StartDate = ProductOwnerSubscriptions.StartDate;
                        _model.EndDate = ProductOwnerSubscriptions.EndDate;
                        _model.IsTrial = ProductOwnerSubscriptions.IsTrial;
                        _model.PaymentMode = ProductOwnerSubscriptions.PaymentMode;
                        _model.AmountPaid = ProductOwnerSubscriptions.AmountPaid;
                        _model.TransactionId = ProductOwnerSubscriptions.TransactionId;
                        _model.Remarks = ProductOwnerSubscriptions.Remarks;
                        _model.ProductOwnerId = ProductOwnerSubscriptions.ProductOwnerId;
                        _model.PlanId = ProductOwnerSubscriptions.PlanId;

                        _context.Add(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm =  _context.ProductOwnerSubscriptions
                            .Where(x => x.PlanId == ProductOwnerSubscriptions.PlanId && x.ProductOwnerId == ProductOwnerSubscriptions.ProductOwnerId)
                            .FirstOrDefault();
                        originalTerm.StartDate = ProductOwnerSubscriptions.StartDate;
                        originalTerm.EndDate = ProductOwnerSubscriptions.EndDate;
                        originalTerm.IsTrial = ProductOwnerSubscriptions.IsTrial;
                        originalTerm.PaymentMode = ProductOwnerSubscriptions.PaymentMode;
                        originalTerm.AmountPaid = ProductOwnerSubscriptions.AmountPaid;
                        originalTerm.TransactionId = ProductOwnerSubscriptions.TransactionId;
                        originalTerm.Remarks = ProductOwnerSubscriptions.Remarks;
                        originalTerm.ProductOwnerId = ProductOwnerSubscriptions.ProductOwnerId;
                        originalTerm.PlanId = ProductOwnerSubscriptions.PlanId;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }

                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        Message = "User Subscription Plans Saved Successfully."
                    };

                }
                else
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        Message = "User Subscription Plans already Successfully."
                    };
                }
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto)
        {
            try
            {
                 
                var  result = await (
                                        from p in _context.ProductOwnerSubscriptions
                                        join s in _context.SubscriptionPlans
                                            on p.PlanId equals s.PlanId
                                        join o in _context.ProductOwners
                                            on p.ProductOwnerId equals o.ProductOwnerId
                                        select new ProductOwnerSubscriptionDto
                                        {
                                            ProductOwnerId = p.ProductOwnerId,
                                            PlanId = p.PlanId,
                                            StartDate = p.StartDate,
                                            EndDate = p.EndDate,
                                            PaymentMode = p.PaymentMode,
                                            AmountPaid = p.AmountPaid,
                                            TransactionId = p.TransactionId,
                                            Remarks = p.Remarks,
                                            IsTrial = p.IsTrial,
                                            PlanName = s.PlanName,
                                            PlanCode = s.PlanCode,
                                            Description = s.Description,
                                            PriceMonthly = s.PriceMonthly,
                                            PriceYearly = s.PriceYearly,
                                            OwnerName = o.OwnerName,
                                            Email = o.Email
                                        }
                                    ).AsQueryable().ToListAsync();

                   return new ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>
                {
                    IsSuccess = true,
                    Result = result,
                    Message = "Subscription Plans Fetched Successfully.",
                };

            }
            catch (Exception ex)
            {


                return new ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto,int ProductOwnerId)
        {
            try
            {
                if (ProductOwnerId != 0)
                {
                    var result = await (
                                            from p in _context.ProductOwnerSubscriptions
                                            join s in _context.SubscriptionPlans
                                                on p.PlanId equals s.PlanId

                                            select new ProductOwnerSubscriptionDto
                                            {
                                                ProductOwnerId = p.ProductOwnerId,
                                                PlanId = p.PlanId,
                                                StartDate = p.StartDate,
                                                EndDate = p.EndDate,
                                                PaymentMode = p.PaymentMode,
                                                AmountPaid = p.AmountPaid,
                                                TransactionId = p.TransactionId,
                                                Remarks = p.Remarks,
                                                IsTrial = p.IsTrial,
                                                PlanName = s.PlanName,
                                                PlanCode = s.PlanCode,
                                                Description = s.Description,
                                                PriceMonthly = s.PriceMonthly,
                                                PriceYearly = s.PriceYearly,
                                                Email = "",       // Set if you join with ProductOwners
                                                OwnerName = ""

                                            }
                                        ).Where(t => t.ProductOwnerId == ProductOwnerId).ToListAsync();

                    return new ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>
                    {
                        IsSuccess = true,
                        Result = result,
                        Message = "Subscription Plans Fetched Successfully.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>
                    {
                        IsSuccess = true,
                        Result = null,
                        Message = "ProductOwnerId is required.",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSubcontractorData(Subcontractors Subcontractors)
        {
            var response = new ManagerBaseResponse<List<Subcontractors>>();

            try
            {
                var accountid = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == Subcontractors.CompanyID);

                if (accountid == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };
                }
                else
                {
                    if (Subcontractors.SubcontractorID == 0)
                    {
                        // Insert
                        Subcontractors _model = new Subcontractors();
                        _model.Name = Subcontractors.Name;
                        _model.ContactEmail = Subcontractors.ContactEmail;
                        _model.ContactPhone = Subcontractors.ContactPhone;
                        _model.PAN = Subcontractors.PAN;
                        _model.GSTIN = Subcontractors.GSTIN;
                        _model.Address = Subcontractors.Address;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CompanyID = accountid.CompanyID;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Subcontractors
                            .Where(x => x.SubcontractorID == Subcontractors.SubcontractorID)
                            .FirstOrDefault();
                        originalTerm.Name = Subcontractors.Name;
                        originalTerm.ContactEmail = Subcontractors.ContactEmail;
                        originalTerm.ContactPhone = Subcontractors.ContactPhone;
                        originalTerm.Address = Subcontractors.Address;
                        originalTerm.PAN = Subcontractors.PAN;
                        originalTerm.GSTIN = Subcontractors.GSTIN;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.CompanyID = accountid.CompanyID;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Subcontractors Details Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveSubcontractorData(string SubcontractorsID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Subcontractors = await _context.Subcontractors.Where(x => x.SubcontractorID.ToString() == SubcontractorsID).ToListAsync();

                if (string.IsNullOrEmpty(Subcontractors.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Subcontractors Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Subcontractors.RemoveRange(Subcontractors);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Subcontractors Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetSubcontractors(int CompanyId)
        {
            try
            {
                if (CompanyId != 0)
                {
                    var result = await (
                                            from p in _context.Subcontractors
                                            join s in _context.Companies
                                                on p.CompanyID equals s.CompanyID

                                            select new SubcontractorsRequest
                                            {
                                                SubcontractorID = p.SubcontractorID,
                                                CompanyID = p.CompanyID,
                                                CompanyName = s.Name,
                                                CompanyEmail = s.ContactEmail,
                                                CompanyPhone = s.ContactPhone,
                                                Domain = s.Domain,
                                                Name = p.Name,
                                                ContactEmail = p.ContactEmail,
                                                ContactPhone = p.ContactPhone,
                                                Address = p.Address,
                                                GSTIN = p.GSTIN,
                                                PAN = p.PAN,
                                                CreatedAt = p.CreatedAt

                                            }
                                        ).Where(t => t.CompanyID == CompanyId).ToListAsync();

                    return new ManagerBaseResponse<List<SubcontractorsRequest>>
                    {
                        IsSuccess = true,
                        Result = result,
                        Message = "Subscription Plans Fetched Successfully.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<SubcontractorsRequest>>
                    {
                        IsSuccess = true,
                        Result = null,
                        Message = "CompanyID is required.",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<List<SubcontractorsRequest>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Subcontractors>>> GetSubcontractorsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Subcontractors.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SubcontractorID);

                PageListed<Subcontractors> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Subcontractors>>
                {
                    Result = result.Data,
                    Message = "Subcontractors Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<Subcontractors>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetProductOwnerSubcontractorsDetails(int ProductOwnerId)
        {
            try
            {
                if (ProductOwnerId != 0)
                {
                    var result = await (
                                            from p in _context.Subcontractors
                                            join s in _context.Companies
                                                on p.CompanyID equals s.CompanyID
                                            join c in _context.ProductOwners on  s.ProductOwnerId equals c.ProductOwnerId  

                                            select new SubcontractorsRequest
                                            {
                                                SubcontractorID = p.SubcontractorID,
                                                CompanyID = p.CompanyID,
                                                CompanyName = s.Name,
                                                CompanyEmail = s.ContactEmail,
                                                CompanyPhone = s.ContactPhone,
                                                Domain = s.Domain,
                                                Name = p.Name,
                                                ContactEmail = p.ContactEmail,
                                                ContactPhone = p.ContactPhone,
                                                Address = p.Address,
                                                GSTIN = p.GSTIN,
                                                PAN = p.PAN,
                                                CreatedAt = p.CreatedAt,
                                                ProductOwnerId = s.ProductOwnerId,
                                                OwnerName = c.OwnerName,
                                                OwnerPhone = c.Mobile,
                                                OWnerEmail = c.Email,
                                                OrganizationName = c.OrganizationName
                                                
                                            }
                                        ).Where(t => t.ProductOwnerId == ProductOwnerId).ToListAsync();

                    return new ManagerBaseResponse<List<SubcontractorsRequest>>
                    {
                        IsSuccess = true,
                        Result = result,
                        Message = "Subscription Plans Fetched Successfully.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<SubcontractorsRequest>>
                    {
                        IsSuccess = true,
                        Result = null,
                        Message = "ProductOwnerId is required.",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<List<SubcontractorsRequest>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<bool>> SavePlansData(Plans Plans)
        {
            var response = new ManagerBaseResponse<List<Plans>>();

            try
            {              
                    if (Plans.PlanId == 0)
                    {
                        // Insert
                        Plans _model = new Plans();
                        _model.Name =Plans.Name;
                        _model.Description =Plans.Description;
                        _model.MaxEmployees = Plans.MaxEmployees;
                        _model.MultiOrg = Plans.MultiOrg;
                        _model.Price = Plans.Price;
                        _model.BillingCycle = Plans.BillingCycle;   
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                    await _context.SaveChangesAsync();
                }
                    else
                    {
                        // Update
                        var originalTerm = _context.Plans
                            .Where(x => x.PlanId == Plans.PlanId)
                                .FirstOrDefault();
                        originalTerm.Name = Plans.Name;
                        originalTerm.Description = Plans.Description;
                        originalTerm.MaxEmployees = Plans.MaxEmployees;
                        originalTerm.MultiOrg = Plans.MultiOrg;
                        originalTerm.Price = Plans.Price;
                        originalTerm.BillingCycle = Plans.BillingCycle;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
           

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Plans Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemovePlansData(string PlanID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Plans = await _context.Plans.Where(x => x.PlanId.ToString() == PlanID).ToListAsync();

                if (string.IsNullOrEmpty(Plans.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Plans Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Plans.RemoveRange(Plans);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Plans Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<Plans>>> GetAllPlansData()
        {
            try
            {
                var plans = await _context.Plans.AsQueryable().OrderBy(x => x.PlanId).ToListAsync();

                return new ManagerBaseResponse<List<Plans>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Plans Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Plans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<Plans>>> GetAllPlansDataByID(string PlanID)
        {
            try
            {
                var plans = await _context.Plans.Where(x => x.PlanId.ToString() == PlanID).ToListAsync();

                return new ManagerBaseResponse<List<Plans>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Plans Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Plans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Plans>>> GetAllPlansDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
       
                var query =   _context.Plans.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PlanId);

               PageListed<Plans> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Plans>>
                {
                    Result = result.Data,
                    Message = "Plans Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {                      
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };                           
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse< IEnumerable < Plans>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSubscriptionInvoicesData(SubscriptionInvoices subscriptionInvoices)
        {
            var response = new ManagerBaseResponse<List<SubscriptionInvoices>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == subscriptionInvoices.CompanyID);

                if (Company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };
                }
                else
                {

                    if (subscriptionInvoices.InvoiceID == 0)
                    {
                        // Insert
                        SubscriptionInvoices _model = new SubscriptionInvoices();
                        _model.CompanyID = subscriptionInvoices.CompanyID;
                        _model.PaymentID = subscriptionInvoices.PaymentID;
                        _model.PeriodStart = subscriptionInvoices.PeriodStart;
                        _model.PeriodEnd = subscriptionInvoices.PeriodEnd;
                        _model.Amount = subscriptionInvoices.Amount;
                        _model.Currency = subscriptionInvoices.Currency;
                        _model.PaidOn = subscriptionInvoices.PaidOn;
                        _model.Status = subscriptionInvoices.Status;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.SubscriptionInvoices
                            .Where(x => x.InvoiceID == subscriptionInvoices.InvoiceID)
                                .FirstOrDefault();
                        originalTerm.CompanyID = subscriptionInvoices.CompanyID;
                        originalTerm.PaymentID = subscriptionInvoices.PaymentID;
                        originalTerm.PeriodStart = subscriptionInvoices.PeriodStart;
                        originalTerm.PeriodEnd = subscriptionInvoices.PeriodEnd;
                        originalTerm.Amount = subscriptionInvoices.Amount;
                        originalTerm.Currency = subscriptionInvoices.Currency;
                        originalTerm.PaidOn = subscriptionInvoices.PaidOn;
                        originalTerm.Status = subscriptionInvoices.Status;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }

                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "SubscriptionInvoices Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveSubscriptionInvoicesData(string InvoiceID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var SubscriptionInvoices = await _context.SubscriptionInvoices.Where(x => x.InvoiceID.ToString() == InvoiceID).ToListAsync();

                if (string.IsNullOrEmpty(SubscriptionInvoices.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "SubscriptionInvoices Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.SubscriptionInvoices.RemoveRange(SubscriptionInvoices);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "SubscriptionInvoices Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionInvoices>>> GetAllSubscriptionInvoicesData()
        {
            try
            {
                var SubscriptionInvoices = await _context.SubscriptionInvoices.AsQueryable().OrderBy(x => x.InvoiceID).ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionInvoices>>
                {
                    IsSuccess = true,
                    Result = SubscriptionInvoices,
                    Message = "SubscriptionInvoices Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionInvoices>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionInvoices>>> GetAllSubscriptionInvoicesDataByID(string InvoiceID)
        {
            try
            {
                var SubscriptionInvoices = await _context.SubscriptionInvoices.Where(x => x.InvoiceID.ToString() == InvoiceID ).ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionInvoices>>
                {
                    IsSuccess = true,
                    Result = SubscriptionInvoices,
                    Message = "SubscriptionInvoices Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionInvoices>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<SubscriptionInvoices>>> GetAllSubscriptionInvoicesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                var query = _context.SubscriptionInvoices.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Status.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.InvoiceID);

                PageListed<SubscriptionInvoices> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<SubscriptionInvoices>>
                {
                    Result = result.Data,
                    Message = "Subscription Invoice Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<SubscriptionInvoices>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSubscriptionPlansData(SubscriptionPlans subscriptionPlans)
        {
            var response = new ManagerBaseResponse<List<SubscriptionPlans>>();

            try
            {
                if (subscriptionPlans.PlanId == 0)
                {
                    // Insert
                    SubscriptionPlans _model = new SubscriptionPlans();
                    _model.PlanCode = subscriptionPlans.PlanCode;
                    _model.PlanName = subscriptionPlans.PlanName;
                    _model.Description = subscriptionPlans.Description;
                    _model.PriceMonthly = subscriptionPlans.PriceMonthly;
                    _model.PriceYearly = subscriptionPlans.PriceYearly;
                    _model.MaxCompanies = subscriptionPlans.MaxCompanies;
                    _model.MaxUsers = subscriptionPlans.MaxUsers;
                    _model.MaxStorageMB = subscriptionPlans.MaxStorageMB;
                    _model.AllowEPFO = subscriptionPlans.AllowEPFO;
                    _model.AllowESIC = subscriptionPlans.AllowESIC;
                    _model.AllowGST = subscriptionPlans.AllowGST;
                    _model.AllowTDS = subscriptionPlans.AllowTDS;
                    _model.AllowCLRA = subscriptionPlans.AllowCLRA;
                    _model.AllowLWF = subscriptionPlans.AllowLWF;
                    _model.AllowPT  =   subscriptionPlans.AllowPT;
                    _model.AllowCloudBackup = subscriptionPlans.AllowCloudBackup;
                    _model.AllowPayroll = subscriptionPlans.AllowPayroll;
                    _model.AllowDSCSigning = subscriptionPlans.AllowDSCSigning;
                    _model.IsActive = subscriptionPlans.IsActive;

                    _context.Add(_model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Update
                    var originalTerm = _context.SubscriptionPlans
                        .Where(x => x.PlanId == subscriptionPlans.PlanId)
                            .FirstOrDefault();
                    originalTerm.PlanCode = subscriptionPlans.PlanCode;
                    originalTerm.PlanName = subscriptionPlans.PlanName;
                    originalTerm.Description = subscriptionPlans.Description;
                    originalTerm.PriceMonthly = subscriptionPlans.PriceMonthly;
                    originalTerm.PriceYearly = subscriptionPlans.PriceYearly;
                    originalTerm.MaxCompanies = subscriptionPlans.MaxCompanies;
                    originalTerm.MaxUsers = subscriptionPlans.MaxUsers;
                    originalTerm.MaxStorageMB = subscriptionPlans.MaxStorageMB;
                    originalTerm.AllowEPFO = subscriptionPlans.AllowEPFO;
                    originalTerm.AllowESIC = subscriptionPlans.AllowESIC;
                    originalTerm.AllowGST = subscriptionPlans.AllowGST;
                    originalTerm.AllowTDS = subscriptionPlans.AllowTDS;
                    originalTerm.AllowCLRA = subscriptionPlans.AllowCLRA;
                    originalTerm.AllowLWF = subscriptionPlans.AllowLWF;
                    originalTerm.AllowPT = subscriptionPlans.AllowPT;
                    originalTerm.AllowCloudBackup = subscriptionPlans.AllowCloudBackup;
                    originalTerm.AllowPayroll = subscriptionPlans.AllowPayroll;
                    originalTerm.AllowDSCSigning = subscriptionPlans.AllowDSCSigning;
                    originalTerm.IsActive = subscriptionPlans.IsActive;

                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }


                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "SubscriptionPlans Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveSubscriptionPlansData(string PlanID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var SubscriptionPlans = await _context.SubscriptionPlans.Where(x => x.PlanId.ToString() == PlanID).ToListAsync();

                if (string.IsNullOrEmpty(SubscriptionPlans.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "SubscriptionPlans Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.SubscriptionPlans.RemoveRange(SubscriptionPlans);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "SubscriptionPlans Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavePaymentTransactionData(PaymentTransactions paymentTransactions)
        {
            var response = new ManagerBaseResponse<List<PaymentTransactions>>();

            try
            {
                if (paymentTransactions.TransactionID == 0)
                {
                    // Insert
                    PaymentTransactions _model = new PaymentTransactions();
                    _model.PaymentID = paymentTransactions.PaymentID;
                    _model.Gateway =  paymentTransactions.Gateway;
                    _model.GatewayPaymentId = paymentTransactions.GatewayPaymentId;
                    _model.Amount = paymentTransactions.Amount;
                    _model.Fees =  paymentTransactions.Fees;
                    _model.Status =  paymentTransactions.Status;
                    _model.ResponsePayload =  paymentTransactions.ResponsePayload;
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                    _context.Add(_model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Update
                    var originalTerm = _context.PaymentTransactions
                        .Where(x => x.TransactionID == paymentTransactions.TransactionID)
                            .FirstOrDefault();
                    originalTerm.PaymentID = paymentTransactions.PaymentID;
                    originalTerm.Gateway = paymentTransactions.Gateway;
                    originalTerm.GatewayPaymentId = paymentTransactions.GatewayPaymentId;
                    originalTerm.Amount = paymentTransactions.Amount;
                    originalTerm.Fees = paymentTransactions.Fees;
                    originalTerm.Status = paymentTransactions.Status;
                    originalTerm.ResponsePayload = paymentTransactions.ResponsePayload;
                    originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payment Transaction Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemovePaymentTransactionData(string TransactionID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var PaymentTransactions = await _context.PaymentTransactions.Where(x => x.TransactionID.ToString() == TransactionID).ToListAsync();

                if (string.IsNullOrEmpty(PaymentTransactions.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "PaymentTransactions Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.PaymentTransactions.RemoveRange(PaymentTransactions);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "PaymentTransactions Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PaymentTransactions>>> GetAllPaymentTransactionData()
        {
            try
            {
                var PaymentTransactions = await _context.PaymentTransactions.AsQueryable().OrderBy(x => x.TransactionID).ToListAsync();

                return new ManagerBaseResponse<List<PaymentTransactions>>
                {
                    IsSuccess = true,
                    Result = PaymentTransactions,
                    Message = "Payment Transaction Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PaymentTransactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PaymentTransactions>>> GetAllPaymentTransactionDataByID(string TransactionID)
        {
            try
            {
                var PaymentTransactions = await _context.PaymentTransactions.Where(x => x.TransactionID.ToString() == TransactionID).ToListAsync();

                return new ManagerBaseResponse<List<PaymentTransactions>>
                {
                    IsSuccess = true,
                    Result = PaymentTransactions,
                    Message = "Payment Transaction Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PaymentTransactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<PaymentTransactions>>> GetAllPaymentTransactionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                var query = _context.PaymentTransactions.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Status.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.TransactionID);

                PageListed<PaymentTransactions> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<PaymentTransactions>>
                {
                    Result = result.Data,
                    Message = "Payment Transactions Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<PaymentTransactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCustomerPaymentsData(CustomerPayments CustomerPayments)
        {
            var response = new ManagerBaseResponse<List<CustomerPayments>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == CustomerPayments.CompanyID);

                if (Company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };
                }
                else
                {
                    if (CustomerPayments.PaymentID == 0)
                    {
                        // Insert
                        CustomerPayments _model = new CustomerPayments();
                        _model.CompanyID = CustomerPayments.CompanyID;
                        _model.CustomerIdentifier = CustomerPayments.CustomerIdentifier;
                        _model.PlanID = CustomerPayments.PlanID;
                        _model.Amount = CustomerPayments.Amount;
                        _model.Currency = CustomerPayments.Currency;
                        _model.Status = CustomerPayments.Status;                       
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.CustomerPayments
                            .Where(x => x.PaymentID == CustomerPayments.PaymentID)
                                .FirstOrDefault();
                        originalTerm.CompanyID = CustomerPayments.CompanyID;
                        originalTerm.CustomerIdentifier = CustomerPayments.CustomerIdentifier;
                        originalTerm.PlanID = CustomerPayments.PlanID;
                        originalTerm.Amount = CustomerPayments.Amount;
                        originalTerm.Currency = CustomerPayments.Currency;
                        originalTerm.Status = CustomerPayments.Status;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Customer Payment Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveCustomerPaymentsData(string PaymentID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var CustomerPayment = await _context.CustomerPayments.Where(x => x.PaymentID.ToString() == PaymentID).ToListAsync();

                if (string.IsNullOrEmpty(CustomerPayment.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "CustomerPayment Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.CustomerPayments.RemoveRange(CustomerPayment);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CustomerPayment Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CustomerPayments>>> GetAllCustomerPaymentsData()
        {
            try
            {
                var CustomerPayments = await _context.CustomerPayments.AsQueryable().OrderBy(x => x.PaymentID).ToListAsync();

                return new ManagerBaseResponse<List<CustomerPayments>>
                {
                    IsSuccess = true,
                    Result = CustomerPayments,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CustomerPayments>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CustomerPayments>>> GetAllCustomerPaymentDataByID(string PaymentID)
        {
            try
            {
                var customerPayments = await _context.CustomerPayments.Where(x => x.PaymentID.ToString() == PaymentID).ToListAsync();

                return new ManagerBaseResponse<List<CustomerPayments>>
                {
                    IsSuccess = true,
                    Result = customerPayments,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CustomerPayments>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<CustomerPayments>>> GetAllCustomerPaymentFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                
                var query = _context.CustomerPayments.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.CustomerIdentifier.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PaymentID);

                PageListed<CustomerPayments> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<CustomerPayments>>
                {
                    Result = result.Data,
                    Message = "Customer Payments Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<CustomerPayments>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavePartyMasterData(PartyMaster PartyMaster, string UserName)
        {
            var response = new ManagerBaseResponse<List<PartyMaster>>();

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User not Found."
                    };
                }
                else
                {

                    if (PartyMaster.PartyID == 0)
                    {
                        // Insert
                        PartyMaster _model = new PartyMaster();
                        _model.PartyType =  PartyMaster.PartyType;
                        _model.PartyName = PartyMaster.PartyName;
                        _model.PAN = PartyMaster.PAN;
                        _model.GSTIN = PartyMaster.GSTIN;
                        _model.Address1 = PartyMaster.Address1;
                        _model.Address2 = PartyMaster.Address2;
                        _model.City = PartyMaster.City;
                        _model.StateCode = PartyMaster.StateCode;
                        _model.PinCode = PartyMaster.PinCode;
                        _model.Email = PartyMaster.Email;
                        _model.Phone = PartyMaster.Phone;
                        _model.IsActive = PartyMaster.IsActive; 
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CreatedBy = user.Id;

                        _context.Add(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.PartyMaster
                            .Where(x => x.PartyID    == PartyMaster.PartyID)
                                .FirstOrDefault();
                        originalTerm.PartyType = PartyMaster.PartyType;
                        originalTerm.PartyName = PartyMaster.PartyName;
                        originalTerm.PAN = PartyMaster.PAN;
                        originalTerm.GSTIN = PartyMaster.GSTIN;
                        originalTerm.Address1 = PartyMaster.Address1;
                        originalTerm.Address2 = PartyMaster.Address2;
                        originalTerm.City = PartyMaster.City;
                        originalTerm.StateCode = PartyMaster.StateCode;
                        originalTerm.PinCode = PartyMaster.PinCode;
                        originalTerm.Email = PartyMaster.Email;
                        originalTerm.Phone = PartyMaster.Phone;
                        originalTerm.IsActive = PartyMaster.IsActive;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.CreatedBy = user.Id;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Party Master Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemovePartyMasterData(string PartyID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Party = await _context.PartyMaster.Where(x => x.PartyID.ToString() == PartyID).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Party Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.PartyMaster.RemoveRange(Party);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Party Master Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PartyMaster>>> GetAllPartyMasterData()
        {
            try
            {
                var Party = await _context.PartyMaster.AsQueryable().OrderBy(x => x.PartyID).ToListAsync();

                return new ManagerBaseResponse<List<PartyMaster>>
                {
                    IsSuccess = true,
                    Result = Party,
                    Message = "Party Master Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PartyMaster>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PartyMaster>>> GetAllPartyMasterDataByID(string PartyID)
        {
            try
            {
                var PartyMaster = await _context.PartyMaster.Where(x => x.PartyID.ToString() == PartyID).ToListAsync();

                return new ManagerBaseResponse<List<PartyMaster>>
                {
                    IsSuccess = true,
                    Result = PartyMaster,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PartyMaster>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<PartyMaster>>> GetAllPartyMasterFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.PartyMaster.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PartyName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PartyID);

                PageListed<PartyMaster> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<PartyMaster>>
                {
                    Result = result.Data,
                    Message = "Party Master Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<PartyMaster>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCompanyPartyRoleData(CompanyPartyRole CompanyPartyRole, string UserName)
        {
            var response = new ManagerBaseResponse<List<CompanyPartyRole>>();

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                var Party = _context.PartyMaster.FirstOrDefault(x => x.PartyID == CompanyPartyRole.PartyID);
                var Company = _context.Companies.FirstOrDefault(x => x.CompanyID == CompanyPartyRole.CompanyID);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User not Found."
                    };
                }
                else
                {
                    if (Party == null || Company == null)
                    {

                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Party and Company Data not Found."
                        };
                    }
                    else
                    {
                        var Data = _context.CompanyPartyRole.FirstOrDefault(x => x.CompanyID == CompanyPartyRole.CompanyID && x.PartyID == CompanyPartyRole.PartyID);

                        if (Data != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                Result = false,
                                Message = "Party and Company Data are already Exist."
                            };

                        }
                        else
                        {
                            if (CompanyPartyRole.CompanyPartyRoleID == 0)
                            {
                                // Insert
                                CompanyPartyRole _model = new CompanyPartyRole();
                                _model.PartyID = CompanyPartyRole.PartyID;
                                _model.CompanyID = CompanyPartyRole.CompanyID;
                                _model.RoleType = CompanyPartyRole.RoleType;
                                _model.IsActive = CompanyPartyRole.IsActive;
                                _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                                _model.CreatedBy = user.Id;

                                _context.Add(_model);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                // Update
                                var originalTerm = _context.CompanyPartyRole
                                    .Where(x => x.CompanyPartyRoleID == CompanyPartyRole.CompanyPartyRoleID)
                                        .FirstOrDefault();
                                originalTerm.PartyID = CompanyPartyRole.PartyID;
                                originalTerm.CompanyID = CompanyPartyRole.CompanyID;
                                originalTerm.RoleType = CompanyPartyRole.RoleType;
                                originalTerm.IsActive = CompanyPartyRole.IsActive;
                                originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                                originalTerm.CreatedBy = user.Id;

                                _context.Update(originalTerm);
                                _context.SaveChanges();
                            }
                        }
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Company Party Role Data Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveCompanyPartyRoleData(string CompanyPartyRoleID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Party = await _context.CompanyPartyRole.Where(x => x.CompanyPartyRoleID.ToString() == CompanyPartyRoleID).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Party Role Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.CompanyPartyRole.RemoveRange(Party);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Company Party Role Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CompanyPartyRole>>> GetAllCompanyPartyRoleData()
        {
            try
            {
                var Party = await _context.CompanyPartyRole.AsQueryable().OrderBy(x => x.CompanyPartyRoleID).ToListAsync();

                return new ManagerBaseResponse<List<CompanyPartyRole>>
                {
                    IsSuccess = true,
                    Result = Party,
                    Message = "Company Party  Role Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CompanyPartyRole>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CompanyPartyRole>>> GetAllCompanyPartyRoleDataByID(string CompanyPartyRoleID)
        {
            try
            {
                var PartyMaster = await _context.CompanyPartyRole.Where(x => x.CompanyPartyRoleID.ToString() == CompanyPartyRoleID).ToListAsync();

                return new ManagerBaseResponse<List<CompanyPartyRole>>
                {
                    IsSuccess = true,
                    Result = PartyMaster,
                    Message = "Company Party Role Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CompanyPartyRole>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<CompanyPartyRole>>> GetAllCompanyPartyRoleFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.CompanyPartyRole.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.RoleType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyPartyRoleID);

                PageListed<CompanyPartyRole> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<CompanyPartyRole>>
                {
                    Result = result.Data,
                    Message = "Company Party Role Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<CompanyPartyRole>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
