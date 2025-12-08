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


    }
}
