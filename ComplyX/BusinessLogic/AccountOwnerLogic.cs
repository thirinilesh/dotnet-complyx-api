using ComplyX.Data;
using ComplyX.Helper;
using ComplyX.Models;
using ComplyX.Services;
using Lakshmi.Common.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ComplyX.BusinessLogic
{
    public class AccountOwnerLogic : IProductOwner
    {

        private readonly AppDbContext _context;

        public AccountOwnerLogic(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductOwners>> GetAllAsync()
        {
            try
            {
                return await _context.ProductOwners
                                     .AsNoTracking() 
                                     .OrderByDescending(x => x.AccountOwnerId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching account owners", ex);
            }
        }

        public Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwners ProductOwners)
        {
            var response = new ManagerBaseResponse<List<ProductOwners>>();

            try
            {
                if (ProductOwners.AccountOwnerId == 0)
                {
                    // Insert
                    ProductOwners _model = new ProductOwners();
                    _model.OwnerName = ProductOwners.OwnerName;
                    _model.Email = ProductOwners.Email;
                    _model.Mobile = ProductOwners.Mobile;
                    //_model.PasswordHash = ProductOwners.PasswordHash;
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
                        .Where(x => x.AccountOwnerId == ProductOwners.AccountOwnerId)
                        .FirstOrDefault();
                    originalTerm.OwnerName = ProductOwners.OwnerName;
                    originalTerm.Email = ProductOwners.Email;
                    originalTerm.Mobile = ProductOwners.Mobile;
                    //originalTerm.PasswordHash = ProductOwners.PasswordHash;
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

        public async Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string AccountOwnerId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.ProductOwners.Where(x => x.AccountOwnerId.ToString() == AccountOwnerId).ToListAsync();

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

        public async Task<ManagerBaseResponse<bool>> SaveCompanyData(Company company)
        {
            var response = new ManagerBaseResponse<List<Company>>();

            try
            {
                var accountid = await _context.ProductOwners.FirstOrDefaultAsync(x => x.AccountOwnerId == company.AccountOwnerId);

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
                        _model.AccountOwnerId = accountid.AccountOwnerId;

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
                        originalTerm.AccountOwnerId = accountid.AccountOwnerId;

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


    }
}
