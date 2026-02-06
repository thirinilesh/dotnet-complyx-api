//using ComplyX_Businesss.Models;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models.ProductOwner;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using ComplyX_Businesss.Services;
using ComplyX.Repositories.UnitOfWork;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.Company;
using ComplyX_Businesss.Models.SubscriptionPlan;
using ComplyX_Businesss.Models.Subcontractor;
using ComplyX_Businesss.Models.Plan;
using ComplyX_Businesss.Models.SubscriptionInvoices;
using ComplyX_Businesss.Models.PaymentTransaction;
using ComplyX_Businesss.Models.CustomerPayments;
using ComplyX_Businesss.Models.PartyMaster;
using ComplyX_Businesss.Models.CompanyPartyRole;
using NHibernate.Criterion;
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
        public async Task<List<ProductOwner>> GetAllAsync()
        {
            try
            {
                var result = await _UnitOfWork.ProductOwnerRepositories.GetQueryable()
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
                            OrganizationName = x.OrganizationName,
                            LegalName = x.LegalName,
                            RegistrationId = x.RegistrationId,
                            OrganizationType = x.OrganizationType,
                            Address = x.Address,
                            City = x.City,
                            Pincode = x.Pincode,
                            State = x.State,
                            Country = x.Country,
                            CreatedBy = x.CreatedBy,
                            UpdatedBy = x.UpdatedBy,
                            CreatedAt = x.CreatedAt,
                            UpdatedAt = x.UpdatedAt,
                            IsActive = x.IsActive,
                            SubscriptionPlan = x.SubscriptionPlan,
                            SubscriptionStart = x.SubscriptionStart,
                            SubscriptionExpiry = x.SubscriptionExpiry,
                            MaxCompanies = x.MaxCompanies,
                            MaxUsers = x.MaxUsers,
                            MaxStorageMb = x.MaxStorageMb,
                            AllowCloudBackup = x.AllowCloudBackup,
                            AllowGstmodule = x.AllowGstmodule,
                            AllowTdsmodule = x.AllowTdsmodule,
                            AllowClramodule = x.AllowClramodule,
                            AllowPayrollModule = x.AllowPayrollModule,
                            AllowDscsigning = x.AllowDscsigning
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
        public async Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwnerRequestModel ProductOwners)
        {
            var response = new ManagerBaseResponse<List<ProductOwner>>();

            try
            {
                if (ProductOwners.ProductOwnerId == 0)
                {
                    // Insert
                    ProductOwner _model = new ProductOwner();
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
                    _model.MaxStorageMb = ProductOwners.MaxStorageMb;
                    _model.MaxUsers = ProductOwners.MaxUsers;
                    _model.AllowCloudBackup = ProductOwners.AllowCloudBackup;
                    _model.AllowTdsmodule = ProductOwners.AllowTdsmodule;
                    _model.AllowGstmodule = ProductOwners.AllowGstmodule;
                    _model.AllowClramodule = ProductOwners.AllowClramodule;
                    _model.AllowPayrollModule = ProductOwners.AllowPayrollModule;
                    _model.AllowDscsigning = ProductOwners.AllowDscsigning;

                    //_context.Add(_model);
                    //_context.SaveChanges();

                  await    _UnitOfWork.ProductOwnerRepositories.AddAsync(_model);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.ProductOwnerRepositories.GetQueryable(x => x.ProductOwnerId == ProductOwners.ProductOwnerId)
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
                    originalTerm.MaxStorageMb = ProductOwners.MaxStorageMb;
                    originalTerm.MaxUsers = ProductOwners.MaxUsers;
                    originalTerm.AllowCloudBackup = ProductOwners.AllowCloudBackup;
                    originalTerm.AllowTdsmodule = ProductOwners.AllowTdsmodule;
                    originalTerm.AllowGstmodule = ProductOwners.AllowGstmodule;
                    originalTerm.AllowClramodule = ProductOwners.AllowClramodule;
                    originalTerm.AllowPayrollModule = ProductOwners.AllowPayrollModule;
                    originalTerm.AllowDscsigning = ProductOwners.AllowDscsigning;

                    //_context.Update(originalTerm);
                    //_context.SaveChanges();
                //  await   _UnitOfWork.ProductOwnerRepositories.UpdateRange(originalTerm);
                }
                await  _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool> 
                {
                    Result = true,
                    Message = "Product Details Saved Successfully."
                } ;
            }
            catch (Exception e)
            {
                return  new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                } ;
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string ProductOwnerId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                // var product = await _context.ProductOwners.Where(x => x.ProductOwnerId.ToString() == ProductOwnerId).ToListAsync();
                var product = await _UnitOfWork.ProductOwnerRepositories.GetQueryable(x => x.ProductOwnerId.ToString() == ProductOwnerId).ToListAsync();
                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message =  "Product Id is not Vaild",
                    };
                }

                // Remove all related report details
                //   _context.ProductOwners.RemoveRange(product);
                _UnitOfWork.ProductOwnerRepositories.RemoveRange(product);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>> GetAllProductOwnerFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                //var query = _context.ProductOwners.AsQueryable();
                var query = _UnitOfWork.ProductOwnerRepositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.OwnerName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ProductOwnerId);
                var responseQuery = query.Select(x => new ProductOwnerResponseModel
                {

                    ProductOwnerId = x.ProductOwnerId,
                    OwnerName = x.OwnerName,
                    Email = x.Email,
                    Mobile = x.Mobile,
                    OrganizationName = x.OrganizationName,
                    LegalName = x.LegalName,
                    RegistrationId = x.RegistrationId,
                    OrganizationType = x.OrganizationType,
                    Address = x.Address,
                    City = x.City,
                    Pincode = x.Pincode,
                    State = x.State,
                    Country = x.Country,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    IsActive = x.IsActive,
                    SubscriptionPlan = x.SubscriptionPlan,
                    SubscriptionStart = x.SubscriptionStart,
                    SubscriptionExpiry = x.SubscriptionExpiry,
                    MaxCompanies = x.MaxCompanies,
                    MaxUsers = x.MaxUsers,
                    MaxStorageMb = x.MaxStorageMb,
                    AllowCloudBackup = x.AllowCloudBackup,
                    AllowGstmodule = x.AllowGstmodule,
                    AllowTdsmodule = x.AllowTdsmodule,
                    AllowClramodule = x.AllowClramodule,
                    AllowPayrollModule = x.AllowPayrollModule,
                    AllowDscsigning = x.AllowDscsigning
                });
                PageListed<ProductOwnerResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCompanyData(CompanyRequestModel company)
        {
            var response = new ManagerBaseResponse<List<Company>>();

            try
            {
                var accountid = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.ProductOwnerId == company.ProductOwnerId);

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
                    if (company.CompanyId == 0)
                    {
                        // Insert
                        Company _model = new Company();
                        _model.Name = company.Name;
                        _model.Domain = company.Domain;
                        _model.ContactEmail = company.ContactEmail;
                        _model.ContactPhone = company.ContactPhone;
                        _model.Address = company.Address;
                        _model.State = company.State;
                        _model.Pan = company.Pan;
                        _model.Gstin = company.Gstin;
                        _model.IsActive = company.IsActive;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.ProductOwnerId = accountid.ProductOwnerId;

                        //_context.Add(_model);
                        //_context.SaveChanges();
                      await  _UnitOfWork.CompanyRepository.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.CompanyRepository.GetQueryable(x => x.CompanyId == company.CompanyId).FirstOrDefault();
                         
                        originalTerm.Name = company.Name;
                        originalTerm.Domain = company.Domain;
                        originalTerm.ContactEmail = company.ContactEmail;
                        originalTerm.ContactPhone = company.ContactPhone;
                        originalTerm.Address = company.Address;
                        originalTerm.State = company.State;
                        originalTerm.Pan = company.Pan;
                        originalTerm.Gstin = company.Gstin;
                        originalTerm.IsActive = company.IsActive;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.ProductOwnerId = accountid.ProductOwnerId;

                       
                    }
                }
                await _UnitOfWork.CommitAsync();
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
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable(x => x.CompanyId.ToString() == CompanyId).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Id is not Vaild",
                    };
                }

                // Remove all related report details
                //_context.Companies.RemoveRange(Company);


                 _UnitOfWork.CompanyRepository.RemoveRange(Company);
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<IEnumerable<CompanyResponseModel>>> GetAllCompanyDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.CompanyRepository.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyId);
                var responseQuery = query.Select(x => new CompanyResponseModel
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name,
                    Domain = x.Domain,
                    ContactEmail = x.ContactEmail,
                    ContactPhone = x.ContactPhone,
                    Address = x.Address,
                    State = x.State,
                    Gstin = x.Gstin,
                    Pan = x.Pan,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    MaxEmployees = x.MaxEmployees,
                    ProductOwnerId = x.ProductOwnerId
                });


                PageListed<CompanyResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<CompanyResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<CompanyResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionPlanResponseModel>>> GetSubscriptionPlans()
        {
            try
            {
                var  plans = await _UnitOfWork.SubscriptionPlanRepository.GetQueryable().OrderBy(x => x.PlanId)
                    .Select(x => new SubscriptionPlanResponseModel
                {
                        PlanId = x.PlanId,
                        PlanCode = x.PlanCode,
                        PlanName = x.PlanName,
                        Description = x.Description,
                        PriceMonthly = x.PriceMonthly,
                        PriceYearly = x.PriceYearly,
                        MaxCompanies = x.MaxCompanies,
                        MaxUsers = x.MaxUsers,
                        MaxStorageMb = x.MaxStorageMb,
                        AllowEpfo = x.AllowEpfo,
                        AllowEsic = x.AllowEsic,
                        AllowGst = x.AllowGst,
                        AllowTds = x.AllowTds,
                        AllowClra = x.AllowClra,
                        AllowLwf = x.AllowLwf,
                        AllowPt = x.AllowPt,
                        AllowPayroll = x.AllowPayroll,
                        AllowDscsigning = x.AllowDscsigning,
                        AllowCloudBackup = x.AllowCloudBackup,
                        IsActive = x.IsActive
                    }).ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionPlanResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Subscription Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionPlanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }            
        }
        public async Task<ManagerBaseResponse<IEnumerable<SubscriptionPlanResponseModel>>> GetSubscriptionPlansByFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.SubscriptionPlanRepository.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PlanName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PlanId);
                var responseQuery = query.Select(x => new SubscriptionPlanResponseModel
                {
                    PlanId = x.PlanId,
                    PlanCode = x.PlanCode,
                    PlanName = x.PlanName,
                    Description = x.Description,
                    PriceMonthly = x.PriceMonthly,
                    PriceYearly = x.PriceYearly,
                    MaxCompanies = x.MaxCompanies,
                    MaxUsers = x.MaxUsers,
                    MaxStorageMb = x.MaxStorageMb,
                    AllowEpfo = x.AllowEpfo,
                    AllowEsic = x.AllowEsic,
                    AllowGst = x.AllowGst,
                    AllowTds = x.AllowTds,
                    AllowClra = x.AllowClra,
                    AllowLwf = x.AllowLwf,
                    AllowPt = x.AllowPt,
                    AllowPayroll = x.AllowPayroll,
                    AllowDscsigning = x.AllowDscsigning,
                    AllowCloudBackup = x.AllowCloudBackup,
                    IsActive = x.IsActive
                });


                PageListed<SubscriptionPlanResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<SubscriptionPlanResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<SubscriptionPlanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionPlan>>> GetSubscriptionPlanFilter(SubscriptionPlansFilterRequest request)
        {
            try
            {
                List<SubscriptionPlan> result = _UnitOfWork.SubscriptionPlanRepository.GetQueryable().ToList();
                List <SubscriptionPlan> query = new List<SubscriptionPlan>();

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



                return new ManagerBaseResponse<List<SubscriptionPlan>>
                {
                    IsSuccess = true,
                    Result = query,
                    Message = "Subscription Plans Fetched Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionPlan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveUserSubscriptionData(ProductOwnerSubscription ProductOwnerSubscriptions)
        {
            var response = new ManagerBaseResponse<List<ProductOwnerSubscription>>();

            try
            {
                var plan =_UnitOfWork.ProductOwnerSubscriptions.GetQueryable().Where(x => x.ProductOwnerId == ProductOwnerSubscriptions.ProductOwnerId && x.PlanId == ProductOwnerSubscriptions.PlanId); 
                if(plan == null)               
                {               
                    if (ProductOwnerSubscriptions.SubscriptionId == 0)
                    {
                        // Insert
                        ProductOwnerSubscription _model = new ProductOwnerSubscription();
                        _model.StartDate = ProductOwnerSubscriptions.StartDate;
                        _model.EndDate = ProductOwnerSubscriptions.EndDate;
                        _model.IsTrial = ProductOwnerSubscriptions.IsTrial;
                        _model.PaymentMode = ProductOwnerSubscriptions.PaymentMode;
                        _model.AmountPaid = ProductOwnerSubscriptions.AmountPaid;
                        _model.TransactionId = ProductOwnerSubscriptions.TransactionId;
                        _model.Remarks = ProductOwnerSubscriptions.Remarks;
                        _model.ProductOwnerId = ProductOwnerSubscriptions.ProductOwnerId;
                        _model.PlanId = ProductOwnerSubscriptions.PlanId;

                        await _UnitOfWork.ProductOwnerSubscriptions.AddAsync(_model);

                    }
                    else
                    {
                        // Update
                        var originalTerm =  _UnitOfWork.ProductOwnerSubscriptions.GetQueryable()
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

                        
                      
                    }
                    await _UnitOfWork.CommitAsync();
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
                                        from p in _UnitOfWork.ProductOwnerSubscriptions.GetQueryable()
                                        join s in _UnitOfWork.SubscriptionPlanRepository.GetQueryable()
                                            on p.PlanId equals s.PlanId
                                        join o in _UnitOfWork.ProductOwnerRepositories.GetQueryable()
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
                                            from p in _UnitOfWork.ProductOwnerSubscriptions.GetQueryable()
                                            join s in _UnitOfWork.SubscriptionPlanRepository.GetQueryable()
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
        public async Task<ManagerBaseResponse<bool>> SaveSubcontractorData(SubcontractorRequestModel Subcontractors)
        {
            var response = new ManagerBaseResponse<List<Subcontractor>>();

            try
            {
                var accountid = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == Subcontractors.CompanyID);

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
                        Subcontractor _model = new Subcontractor();
                        _model.Name = Subcontractors.Name;
                        _model.ContactEmail = Subcontractors.ContactEmail;
                        _model.ContactPhone = Subcontractors.ContactPhone;
                        _model.Pan = Subcontractors.PAN;
                        _model.Gstin = Subcontractors.GSTIN;
                        _model.Address = Subcontractors.Address;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CompanyId = accountid.CompanyId;

                       await _UnitOfWork.SubcontractorRepository.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.SubcontractorRepository.GetQueryable()
                            .Where(x => x.SubcontractorId == Subcontractors.SubcontractorID)
                            .FirstOrDefault();
                        originalTerm.Name = Subcontractors.Name;
                        originalTerm.ContactEmail = Subcontractors.ContactEmail;
                        originalTerm.ContactPhone = Subcontractors.ContactPhone;
                        originalTerm.Address = Subcontractors.Address;
                        originalTerm.Pan = Subcontractors.PAN;
                        originalTerm.Gstin = Subcontractors.GSTIN;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.CompanyId = accountid.CompanyId;

                      
                    }
                }
                await _UnitOfWork.CommitAsync();
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
                var Subcontractors = await _UnitOfWork.SubcontractorRepository.GetQueryable().Where(x => x.SubcontractorId.ToString() == SubcontractorsID).ToListAsync();

                if (string.IsNullOrEmpty(Subcontractors.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Subcontractors Id is not Vaild",
                    };
                }

                // Remove all related report details
                //_context.Subcontractors.RemoveRange(Subcontractors);

                //  await _context.SaveChangesAsync();

                _UnitOfWork.SubcontractorRepository.RemoveRange(Subcontractors);
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<SubcontractorResponseModel>>> GetSubcontractors(int CompanyId)
        {
            try
            {
                if (CompanyId != 0)
                {
                    var result = await (
                                            from p in _UnitOfWork.SubcontractorRepository.GetQueryable()
                                            join s in _UnitOfWork.CompanyRepository.GetQueryable()
                                                on p.CompanyId equals s.CompanyId

                                            select new SubcontractorResponseModel
                                            {
                                                SubcontractorID = p.SubcontractorId,
                                                CompanyID = p.CompanyId,
                                                CompanyName = s.Name,
                                                CompanyEmail = s.ContactEmail,
                                                CompanyPhone = s.ContactPhone,
                                                Domain = s.Domain,
                                                Name = p.Name,
                                                ContactEmail = p.ContactEmail,
                                                ContactPhone = p.ContactPhone,
                                                Address = p.Address,
                                                GSTIN = p.Gstin,
                                                PAN = p.Pan,
                                                CreatedAt = p.CreatedAt


                                            }
                                        ).Where(t => t.CompanyID == CompanyId).ToListAsync();

                    return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                    {
                        IsSuccess = true,
                        Result = result,
                        Message = "Subscription Plans Fetched Successfully.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                    {
                        IsSuccess = true,
                        Result = null,
                        Message = "CompanyID is required.",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<SubcontractorResponseModel>>> GetSubcontractorsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.SubcontractorRepository.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SubcontractorId);
                var responseQuery = query.Select(x => new SubcontractorResponseModel
                {
                    SubcontractorID = x.SubcontractorId,
                    CompanyID = x.CompanyId,
                    Name = x.Name,
                    ContactEmail = x.ContactEmail,
                    ContactPhone = x.ContactPhone,
                    Address = x.Address,
                    GSTIN = x.Gstin,
                    PAN = x.Pan,
                    CreatedAt = x.CreatedAt
                });


                PageListed<SubcontractorResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<SubcontractorResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<SubcontractorResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubcontractorResponseModel>>> GetProductOwnerSubcontractorsDetails(int ProductOwnerId)
        {
            try
            {
                if (ProductOwnerId != 0)
                {
                    var result = await (
                                            from p in _UnitOfWork.SubcontractorRepository.GetQueryable()
                                            join s in _UnitOfWork.CompanyRepository.GetQueryable()
                                                on p.CompanyId equals s.CompanyId
                                            join c in _UnitOfWork.ProductOwnerRepositories.GetQueryable() on  s.ProductOwnerId equals c.ProductOwnerId  

                                            select new SubcontractorResponseModel
                                            {
                                                SubcontractorID = p.SubcontractorId,
                                                CompanyID = p.CompanyId,
                                                CompanyName = s.Name,
                                                CompanyEmail = s.ContactEmail,
                                                CompanyPhone = s.ContactPhone,
                                                Domain = s.Domain,
                                                Name = p.Name,
                                                ContactEmail = p.ContactEmail,
                                                ContactPhone = p.ContactPhone,
                                                Address = p.Address,
                                                GSTIN = p.Gstin,
                                                PAN = p.Pan,
                                                CreatedAt = p.CreatedAt,
                                                ProductOwnerId = s.ProductOwnerId,
                                                OwnerName = c.OwnerName,
                                                OwnerPhone = c.Mobile,
                                                OWnerEmail = c.Email,
                                                OrganizationName = c.OrganizationName
                                                
                                            }
                                        ).Where(t => t.ProductOwnerId == ProductOwnerId).ToListAsync();

                    return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                    {
                        IsSuccess = true,
                        Result = result,
                        Message = "Subscription Plans Fetched Successfully.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                    {
                        IsSuccess = true,
                        Result = null,
                        Message = "ProductOwnerId is required.",
                    };
                }

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<List<SubcontractorResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };

            }
        }
        public async Task<ManagerBaseResponse<bool>> SavePlansData(PlanRequestModel Plans)
        {
            var response = new ManagerBaseResponse<List<Plan>>();

            try
            {              
                    if (Plans.PlanId == 0)
                    {
                        // Insert
                        Plan _model = new Plan();
                        _model.Name =Plans.Name;
                        _model.Description =Plans.Description;
                        _model.MaxEmployees = Plans.MaxEmployees;
                        _model.MultiOrg = Plans.MultiOrg;
                        _model.Price = Plans.Price;
                        _model.BillingCycle = Plans.BillingCycle;   
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                   await _UnitOfWork.PlanRespositories.AddAsync(_model);
                }
                    else
                    {
                        // Update
                        var originalTerm =_UnitOfWork.PlanRespositories.GetQueryable()
                            .Where(x => x.PlanId == Plans.PlanId)
                                .FirstOrDefault();
                        originalTerm.Name = Plans.Name;
                        originalTerm.Description = Plans.Description;
                        originalTerm.MaxEmployees = Plans.MaxEmployees;
                        originalTerm.MultiOrg = Plans.MultiOrg;
                        originalTerm.Price = Plans.Price;
                        originalTerm.BillingCycle = Plans.BillingCycle;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                      
                    }
           await _UnitOfWork.CommitAsync();

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
                var Plans = await _UnitOfWork.PlanRespositories.GetQueryable().Where(x => x.PlanId.ToString() == PlanID).ToListAsync();

                if (string.IsNullOrEmpty(Plans.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Plans Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PlanRespositories.RemoveRange(Plans);

                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<PlanResponseModel>>> GetAllPlansData()
        {
            try
            {
                var plans = await _UnitOfWork.PlanRespositories.GetQueryable().OrderBy(x => x.PlanId).Select(x => new PlanResponseModel
                {
                    PlanId = x.PlanId,
                    Name = x.Name,                  
                    Description = x.Description,
                    MaxEmployees = x.MaxEmployees,         
                    MultiOrg = x.MultiOrg,            
                    Price = x.Price,            
                    BillingCycle = "Monthly",         
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<PlanResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Plans Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PlanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PlanResponseModel>>> GetAllPlansDataByID(string PlanID)
        {
            try
            {
                var plans = await _UnitOfWork.PlanRespositories.GetQueryable().Where(x => x.PlanId.ToString() == PlanID).Select(x => new PlanResponseModel
                {
                    PlanId = x.PlanId,
                    Name = x.Name,
                    Description = x.Description,
                    MaxEmployees = x.MaxEmployees,
                    MultiOrg = x.MultiOrg,
                    Price = x.Price,
                    BillingCycle = "Monthly",
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<PlanResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Plans Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PlanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<PlanResponseModel>>> GetAllPlansDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
       
                var query =   _UnitOfWork.PlanRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PlanId); var responseQuery = query.Select(x => new PlanResponseModel
                {
                    PlanId = x.PlanId,
                    Name = x.Name,
                    Description = x.Description,
                    MaxEmployees = x.MaxEmployees,
                    MultiOrg = x.MultiOrg,
                    Price = x.Price,
                    BillingCycle = "Monthly",
                    CreatedAt = x.CreatedAt
                });


                PageListed<PlanResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<PlanResponseModel>>
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

                return new ManagerBaseResponse< IEnumerable <PlanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSubscriptionInvoicesData(SubscriptionInvoicesRequestModel subscriptionInvoices)
        {
            var response = new ManagerBaseResponse<List<SubscriptionInvoice>>();

            try
            {
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == subscriptionInvoices.CompanyId);

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

                    if (subscriptionInvoices.InvoiceId == 0)
                    {
                        // Insert
                        SubscriptionInvoice _model = new SubscriptionInvoice();
                        _model.CompanyId = subscriptionInvoices.CompanyId;
                        _model.PaymentId = subscriptionInvoices.PaymentId;
                        _model.PeriodStart = subscriptionInvoices.PeriodStart;
                        _model.PeriodEnd = subscriptionInvoices.PeriodEnd;
                        _model.Amount = subscriptionInvoices.Amount;
                        _model.Currency = subscriptionInvoices.Currency;
                        _model.PaidOn = subscriptionInvoices.PaidOn;
                        _model.Status = subscriptionInvoices.Status;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await  _UnitOfWork.SubscriptionInvoices.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.SubscriptionInvoices.GetQueryable()
                            .Where(x => x.InvoiceId == subscriptionInvoices.InvoiceId)
                                .FirstOrDefault();
                        originalTerm.CompanyId = subscriptionInvoices.CompanyId;
                        originalTerm.PaymentId = subscriptionInvoices.PaymentId;
                        originalTerm.PeriodStart = subscriptionInvoices.PeriodStart;
                        originalTerm.PeriodEnd = subscriptionInvoices.PeriodEnd;
                        originalTerm.Amount = subscriptionInvoices.Amount;
                        originalTerm.Currency = subscriptionInvoices.Currency;
                        originalTerm.PaidOn = subscriptionInvoices.PaidOn;
                        originalTerm.Status = subscriptionInvoices.Status;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        
                    }
                    await _UnitOfWork.CommitAsync();
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
                var SubscriptionInvoices = await _UnitOfWork.SubscriptionInvoices.GetQueryable().Where(x => x.InvoiceId.ToString() == InvoiceID).ToListAsync();

                if (string.IsNullOrEmpty(SubscriptionInvoices.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "SubscriptionInvoices Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.SubscriptionInvoices.RemoveRange(SubscriptionInvoices);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesData()
        {
            try
            {
                var SubscriptionInvoices = await _UnitOfWork.SubscriptionInvoices.GetQueryable().OrderBy(x => x.InvoiceId).Select(x => new SubscriptionInvoicesResponseModel
                {
                    InvoiceId = x.InvoiceId,
                    CompanyId = x.CompanyId,
                    PaymentId = x.PaymentId,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    PaidOn = x.PaidOn,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>
                {
                    IsSuccess = true,
                    Result = SubscriptionInvoices,
                    Message = "SubscriptionInvoices Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesDataByID(string InvoiceID)
        {
            try
            {
                var SubscriptionInvoices = await _UnitOfWork.SubscriptionInvoices.GetQueryable().Where(x => x.InvoiceId.ToString() == InvoiceID).Select(x => new SubscriptionInvoicesResponseModel
                {
                    InvoiceId = x.InvoiceId,
                    CompanyId = x.CompanyId,
                    PaymentId = x.PaymentId,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    PaidOn = x.PaidOn,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>
                {
                    IsSuccess = true,
                    Result = SubscriptionInvoices,
                    Message = "SubscriptionInvoices Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                var query = _UnitOfWork.SubscriptionInvoices.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Status.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.InvoiceId);
                var responseQuery = query.Select(x => new SubscriptionInvoicesResponseModel
                {
                    InvoiceId = x.InvoiceId,
                    CompanyId = x.CompanyId,
                    PaymentId = x.PaymentId,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    PaidOn = x.PaidOn,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                });

                PageListed<SubscriptionInvoicesResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<SubscriptionInvoicesResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<SubscriptionInvoicesResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSubscriptionPlansData(SubscriptionPlanRequestModel subscriptionPlans)
        {
            var response = new ManagerBaseResponse<List<SubscriptionPlan>>();

            try
            {
                if (subscriptionPlans.PlanId == 0)
                {
                    // Insert
                    SubscriptionPlan _model = new SubscriptionPlan();
                    _model.PlanCode = subscriptionPlans.PlanCode;
                    _model.PlanName = subscriptionPlans.PlanName;
                    _model.Description = subscriptionPlans.Description;
                    _model.PriceMonthly = subscriptionPlans.PriceMonthly;
                    _model.PriceYearly = subscriptionPlans.PriceYearly;
                    _model.MaxCompanies = subscriptionPlans.MaxCompanies;
                    _model.MaxUsers = subscriptionPlans.MaxUsers;
                    _model.MaxStorageMb = subscriptionPlans.MaxStorageMb;
                    _model.AllowEpfo = subscriptionPlans.AllowEpfo;
                    _model.AllowEsic = subscriptionPlans.AllowEsic;
                    _model.AllowGst = subscriptionPlans.AllowGst;
                    _model.AllowTds = subscriptionPlans.AllowTds;
                    _model.AllowClra = subscriptionPlans.AllowClra;
                    _model.AllowLwf = subscriptionPlans.AllowLwf;
                    _model.AllowPt =   subscriptionPlans.AllowPt;
                    _model.AllowCloudBackup = subscriptionPlans.AllowCloudBackup;
                    _model.AllowPayroll = subscriptionPlans.AllowPayroll;
                    _model.AllowDscsigning = subscriptionPlans.AllowDscsigning;
                    _model.IsActive = subscriptionPlans.IsActive;

                  await  _UnitOfWork.SubscriptionPlanRepository.AddAsync(_model);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.SubscriptionPlanRepository.GetQueryable()
                        .Where(x => x.PlanId == subscriptionPlans.PlanId)
                            .FirstOrDefault();
                    originalTerm.PlanCode = subscriptionPlans.PlanCode;
                    originalTerm.PlanName = subscriptionPlans.PlanName;
                    originalTerm.Description = subscriptionPlans.Description;
                    originalTerm.PriceMonthly = subscriptionPlans.PriceMonthly;
                    originalTerm.PriceYearly = subscriptionPlans.PriceYearly;
                    originalTerm.MaxCompanies = subscriptionPlans.MaxCompanies;
                    originalTerm.MaxUsers = subscriptionPlans.MaxUsers;
                    originalTerm.MaxStorageMb = subscriptionPlans.MaxStorageMb;
                    originalTerm.AllowEpfo = subscriptionPlans.AllowEpfo;
                    originalTerm.AllowEsic = subscriptionPlans.AllowEsic;
                    originalTerm.AllowGst = subscriptionPlans.AllowGst;
                    originalTerm.AllowTds = subscriptionPlans.AllowTds;
                    originalTerm.AllowClra = subscriptionPlans.AllowClra;
                    originalTerm.AllowLwf = subscriptionPlans.AllowLwf;
                    originalTerm.AllowPt = subscriptionPlans.AllowPt;
                    originalTerm.AllowCloudBackup = subscriptionPlans.AllowCloudBackup;
                    originalTerm.AllowPayroll = subscriptionPlans.AllowPayroll;
                    originalTerm.AllowDscsigning = subscriptionPlans.AllowDscsigning;
                    originalTerm.IsActive = subscriptionPlans.IsActive;

                   
                }
                await _UnitOfWork.CommitAsync();

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
                var SubscriptionPlans = await _UnitOfWork.SubscriptionPlanRepository.GetQueryable().Where(x => x.PlanId.ToString() == PlanID).ToListAsync();

                if (string.IsNullOrEmpty(SubscriptionPlans.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "SubscriptionPlans Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.SubscriptionPlanRepository.RemoveRange(SubscriptionPlans);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<bool>> SavePaymentTransactionData(PaymentTransactionRequestModel paymentTransactions)
        {
            var response = new ManagerBaseResponse<List<PaymentTransaction>>();

            try
            {
                if (paymentTransactions.TransactionId == 0)
                {
                    // Insert
                    PaymentTransaction _model = new PaymentTransaction();
                    _model.PaymentId = paymentTransactions.PaymentId;
                    _model.Gateway =  paymentTransactions.Gateway;
                    _model.GatewayPaymentId = paymentTransactions.GatewayPaymentId;
                    _model.Amount = paymentTransactions.Amount;
                    _model.Fees =  paymentTransactions.Fees;
                    _model.Status =  paymentTransactions.Status;
                    _model.ResponsePayload =  paymentTransactions.ResponsePayload;
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
 
                   await  _UnitOfWork.PaymentTransactionRespositories.AddAsync(_model);
                    
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.PaymentTransactionRespositories.GetQueryable()
                        .Where(x => x.TransactionId == paymentTransactions.TransactionId)
                            .FirstOrDefault();
                    originalTerm.PaymentId = paymentTransactions.PaymentId;
                    originalTerm.Gateway = paymentTransactions.Gateway;
                    originalTerm.GatewayPaymentId = paymentTransactions.GatewayPaymentId;
                    originalTerm.Amount = paymentTransactions.Amount;
                    originalTerm.Fees = paymentTransactions.Fees;
                    originalTerm.Status = paymentTransactions.Status;
                    originalTerm.ResponsePayload = paymentTransactions.ResponsePayload;
                    originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                   
                }
                await _UnitOfWork.CommitAsync();
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
                var PaymentTransactions = await _UnitOfWork.PaymentTransactionRespositories.GetQueryable().Where(x => x.TransactionId.ToString() == TransactionID).ToListAsync();

                if (string.IsNullOrEmpty(PaymentTransactions.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "PaymentTransactions Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PaymentTransactionRespositories.RemoveRange(PaymentTransactions);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<PaymentTransactionRequestModel>>> GetAllPaymentTransactionData()
        {
            try
            {
                var PaymentTransactions = await _UnitOfWork.PaymentTransactionRespositories.GetQueryable().OrderBy(x => x.TransactionId).Select(x => new PaymentTransactionRequestModel
                {
                    TransactionId = x.TransactionId,
                    PaymentId = x.PaymentId,
                    Gateway = x.Gateway,
                    GatewayPaymentId = x.GatewayPaymentId,
                    Amount = x.Amount,
                    Fees = x.Fees,
                    Status = x.Status,
                    ResponsePayload = x.ResponsePayload,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<PaymentTransactionRequestModel>>
                {
                    IsSuccess = true,
                    Result = PaymentTransactions,
                    Message = "Payment Transaction Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PaymentTransactionRequestModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PaymentTransactionRequestModel>>> GetAllPaymentTransactionDataByID(string TransactionID)
        {
            try
            {
                var PaymentTransactions = await _UnitOfWork.PaymentTransactionRespositories.GetQueryable().Where(x => x.TransactionId.ToString() == TransactionID).Select(x => new PaymentTransactionRequestModel
                {
                    TransactionId = x.TransactionId,
                    PaymentId = x.PaymentId,
                    Gateway = x.Gateway,
                    GatewayPaymentId = x.GatewayPaymentId,
                    Amount = x.Amount,
                    Fees = x.Fees,
                    Status = x.Status,
                    ResponsePayload = x.ResponsePayload,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<PaymentTransactionRequestModel>>
                {
                    IsSuccess = true,
                    Result = PaymentTransactions,
                    Message = "Payment Transaction Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PaymentTransactionRequestModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<PaymentTransactionRequestModel>>> GetAllPaymentTransactionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                var query = _UnitOfWork.PaymentTransactionRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Status.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.TransactionId);
                var responseQuery = query.Select(x => new PaymentTransactionRequestModel
                {
                    TransactionId = x.TransactionId,
                    PaymentId = x.PaymentId,
                    Gateway = x.Gateway,
                    GatewayPaymentId = x.GatewayPaymentId,
                    Amount = x.Amount,
                    Fees = x.Fees,
                    Status = x.Status,
                    ResponsePayload = x.ResponsePayload,
                    CreatedAt = x.CreatedAt
                });


                PageListed<PaymentTransactionRequestModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<PaymentTransactionRequestModel>>
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

                return new ManagerBaseResponse<IEnumerable<PaymentTransactionRequestModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCustomerPaymentsData(CustomerPaymentsRequestModel CustomerPayments)
        {
            var response = new ManagerBaseResponse<List<CustomerPayment>>();

            try
            {
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == CustomerPayments.CompanyId);

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
                    if (CustomerPayments.PaymentId == 0)
                    {
                        // Insert
                        CustomerPayment _model = new CustomerPayment();
                        _model.CompanyId = CustomerPayments.CompanyId;
                        _model.CustomerIdentifier = CustomerPayments.CustomerIdentifier;
                        _model.PlanId = CustomerPayments.PlanId;
                        _model.Amount = CustomerPayments.Amount;
                        _model.Currency = CustomerPayments.Currency;
                        _model.Status = CustomerPayments.Status;                       
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.CustomerPayments.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.CustomerPayments.GetQueryable()
                            .Where(x => x.PaymentId == CustomerPayments.PaymentId)
                                .FirstOrDefault();
                        originalTerm.CompanyId = CustomerPayments.CompanyId;
                        originalTerm.CustomerIdentifier = CustomerPayments.CustomerIdentifier;
                        originalTerm.PlanId = CustomerPayments.PlanId;
                        originalTerm.Amount = CustomerPayments.Amount;
                        originalTerm.Currency = CustomerPayments.Currency;
                        originalTerm.Status = CustomerPayments.Status;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                      

                    }
                }
                await _UnitOfWork.CommitAsync();
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
                var CustomerPayment = await _UnitOfWork.CustomerPayments.GetQueryable().Where(x => x.PaymentId.ToString() == PaymentID).ToListAsync();

                if (string.IsNullOrEmpty(CustomerPayment.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "CustomerPayment Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.CustomerPayments.RemoveRange(CustomerPayment);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentsData()
        {
            try
            {
                var CustomerPayments = await _UnitOfWork.CustomerPayments.GetQueryable().OrderBy(x => x.PaymentId).Select(x => new CustomerPaymentsResponseModel
                {
                    PaymentId = x.PaymentId,
                    CompanyId = x.CompanyId,
                    CustomerIdentifier = x.CustomerIdentifier,
                    PlanId = x.PlanId,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<CustomerPaymentsResponseModel>>
                {
                    IsSuccess = true,
                    Result = CustomerPayments,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CustomerPaymentsResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentDataByID(string PaymentID)
        {
            try
            {
                var customerPayments = await _UnitOfWork.CustomerPayments.GetQueryable().Where(x => x.PaymentId.ToString() == PaymentID).Select(x => new CustomerPaymentsResponseModel
                {
                    PaymentId = x.PaymentId,
                    CompanyId = x.CompanyId,
                    CustomerIdentifier = x.CustomerIdentifier,
                    PlanId = x.PlanId,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync();

                return new ManagerBaseResponse<List<CustomerPaymentsResponseModel>>
                {
                    IsSuccess = true,
                    Result = customerPayments,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CustomerPaymentsResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                
                var query = _UnitOfWork.CustomerPayments.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.CustomerIdentifier.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PaymentId);
                var responseQuery = query.Select(x => new CustomerPaymentsResponseModel
                {
                    PaymentId = x.PaymentId,
                    CompanyId = x.CompanyId,
                    CustomerIdentifier = x.CustomerIdentifier,
                    PlanId = x.PlanId,
                    Amount = x.Amount,
                    Currency = x.Currency,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });


                PageListed<CustomerPaymentsResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<CustomerPaymentsResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<CustomerPaymentsResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavePartyMasterData(PartyMasterRequestModel PartyMaster, string UserName)
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

                    if (PartyMaster.PartyId == 0)
                    {
                        // Insert
                        PartyMaster _model = new PartyMaster();
                        _model.PartyType =  PartyMaster.PartyType;
                        _model.PartyName = PartyMaster.PartyName;
                        _model.Pan = PartyMaster.Pan;
                        _model.Gstin = PartyMaster.Gstin;
                        _model.Address1 = PartyMaster.Address1;
                        _model.Address2 = PartyMaster.Address2;
                        _model.City = PartyMaster.City;
                        _model.StateCode = PartyMaster.StateCode;
                        _model.Pincode = PartyMaster.Pincode;
                        _model.Email = PartyMaster.Email;
                        _model.Phone = PartyMaster.Phone;
                        _model.IsActive = PartyMaster.IsActive; 
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CreatedBy = user.Id;

                       
                        await _UnitOfWork.PartyMasterRepositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.PartyMasterRepositories.GetQueryable()
                            .Where(x => x.PartyId    == PartyMaster.PartyId)
                                .FirstOrDefault();
                        originalTerm.PartyType = PartyMaster.PartyType;
                        originalTerm.PartyName = PartyMaster.PartyName;
                        originalTerm.Pan = PartyMaster.Pan;
                        originalTerm.Gstin = PartyMaster.Gstin;
                        originalTerm.Address1 = PartyMaster.Address1;
                        originalTerm.Address2 = PartyMaster.Address2;
                        originalTerm.City = PartyMaster.City;
                        originalTerm.StateCode = PartyMaster.StateCode;
                        originalTerm.Pincode = PartyMaster.Pincode;
                        originalTerm.Email = PartyMaster.Email;
                        originalTerm.Phone = PartyMaster.Phone;
                        originalTerm.IsActive = PartyMaster.IsActive;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.CreatedBy = user.Id;

                       
                    }
                }
                await _UnitOfWork.CommitAsync();
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
                var Party = await _UnitOfWork.PartyMasterRepositories.GetQueryable().Where(x => x.PartyId.ToString() == PartyID).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Party Id is not Vaild",
                    };
                }

                // Remove all related report details

                _UnitOfWork.PartyMasterRepositories.RemoveRange(Party);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<PartyMasterResponseModel>>> GetAllPartyMasterData()
        {
            try
            {
                var Party = await _UnitOfWork.PartyMasterRepositories.GetQueryable().OrderBy(x => x.PartyId).Select(x => new PartyMasterResponseModel
                {
                    PartyId = x.PartyId,
                    PartyName = x.PartyName,
                    Pan = x.Pan,
                    Gstin = x.Gstin,
                    PartyType = x.PartyType,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateCode = x.StateCode,
                    Pincode = x.Pincode,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToListAsync();

                return new ManagerBaseResponse<List<PartyMasterResponseModel>>
                {
                    IsSuccess = true,
                    Result = Party,
                    Message = "Party Master Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PartyMasterResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<PartyMasterResponseModel>>> GetAllPartyMasterDataByID(string PartyID)
        {
            try
            {
                var PartyMaster = await _UnitOfWork.PartyMasterRepositories.GetQueryable().Where(x => x.PartyId.ToString() == PartyID).Select(x => new PartyMasterResponseModel
                {
                    PartyId = x.PartyId,
                    PartyName = x.PartyName,
                    Pan = x.Pan,
                    Gstin = x.Gstin,
                    PartyType = x.PartyType,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateCode = x.StateCode,
                    Pincode = x.Pincode,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToListAsync();

                return new ManagerBaseResponse<List<PartyMasterResponseModel>>
                {
                    IsSuccess = true,
                    Result = PartyMaster,
                    Message = "Customer Payments Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<PartyMasterResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<PartyMasterResponseModel>>> GetAllPartyMasterFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.PartyMasterRepositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PartyName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PartyId);
                var responseQuery = query.Select(x => new PartyMasterResponseModel
                {
                    PartyId = x.PartyId,
                    PartyName = x.PartyName,
                    Pan = x.Pan,
                    Gstin = x.Gstin,
                    PartyType = x.PartyType,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateCode = x.StateCode,
                    Pincode = x.Pincode,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                });


                PageListed<PartyMasterResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<PartyMasterResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<PartyMasterResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveCompanyPartyRoleData(CompanyPartyRoleRequestModel CompanyPartyRole, string UserName)
        {
            var response = new ManagerBaseResponse<List<CompanyPartyRole>>();

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                var Party = _UnitOfWork.PartyMasterRepositories.GetQueryable().FirstOrDefault(x => x.PartyId == CompanyPartyRole.PartyID);
                var Company = _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefault(x => x.CompanyId == CompanyPartyRole.CompanyID);
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
                        var Data = _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable().FirstOrDefault(x => x.CompanyId == CompanyPartyRole.CompanyID && 
                        x.PartyId == CompanyPartyRole.PartyID);

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
                                _model.PartyId = CompanyPartyRole.PartyID;
                                _model.CompanyId = CompanyPartyRole.CompanyID;
                                _model.RoleType = CompanyPartyRole.RoleType;
                                _model.IsActive = CompanyPartyRole.IsActive;
                                _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                                _model.CreatedBy = user.Id;

                               await _UnitOfWork.CompanyPartyRoleRepositories.AddAsync(_model);
                            }
                            else
                            {
                                // Update
                                var originalTerm = _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable()
                                    .Where(x => x.CompanyPartyRoleId == CompanyPartyRole.CompanyPartyRoleID)
                                        .FirstOrDefault();
                                originalTerm.PartyId = CompanyPartyRole.PartyID;
                                originalTerm.CompanyId= CompanyPartyRole.CompanyID;
                                originalTerm.RoleType = CompanyPartyRole.RoleType;
                                originalTerm.IsActive = CompanyPartyRole.IsActive;
                                originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                                originalTerm.CreatedBy = user.Id;

                               
                            }
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
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
                var Party = await _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable().Where(x => x.CompanyPartyRoleId.ToString() == CompanyPartyRoleID).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Party Role Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.CompanyPartyRoleRepositories.RemoveRange(Party);

                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleData()
        {
            try
            {
                var Party = await _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable().OrderBy(x => x.CompanyPartyRoleId).Select(x => new CompanyPartyRoleResponseModel
                {
                    CompanyPartyRoleID = x.CompanyPartyRoleId,
                    CompanyID = x.CompanyId,
                    PartyID = x.PartyId,
                    RoleType = x.RoleType,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToListAsync();

                return new ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>
                {
                    IsSuccess = true,
                    Result = Party,
                    Message = "Company Party  Role Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleDataByID(string CompanyPartyRoleID)
        {
            try
            {
                var PartyMaster = await _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable().Where(x => x.CompanyPartyRoleId.ToString() == CompanyPartyRoleID).Select(x => new CompanyPartyRoleResponseModel
                {
                    CompanyPartyRoleID = x.CompanyPartyRoleId,
                    CompanyID = x.CompanyId,
                    PartyID = x.PartyId,
                    RoleType = x.RoleType,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToListAsync();

                return new ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>
                {
                    IsSuccess = true,
                    Result = PartyMaster,
                    Message = "Company Party Role Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.CompanyPartyRoleRepositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.RoleType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyPartyRoleId);
                var responseQuery = query.Select(x => new CompanyPartyRoleResponseModel
                {
                    CompanyPartyRoleID = x.CompanyPartyRoleId,
                    CompanyID = x.CompanyId,
                    PartyID = x.PartyId,
                    RoleType = x.RoleType,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                });


                PageListed<CompanyPartyRoleResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<CompanyPartyRoleResponseModel>>
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

                return new ManagerBaseResponse<IEnumerable<CompanyPartyRoleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
