using AutoMapper.Configuration.Annotations;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Nest;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyX_Businesss.Helper.Commanfield;

namespace ComplyX_Businesss.Services.Implementation
{
    public class ITTDSCClass : ITTDSServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppDbContext _context;
        private readonly Nest.Filter _filter;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Commanfield _commanfield;

        public ITTDSCClass(AppDbContext context, Nest.Filter filter, UserManager<ApplicationUser> userManager, Commanfield commanfield)
        {
            _context = context;
            _filter = filter;
            _userManager = userManager;
            _commanfield = commanfield;
        }

        public async Task<ManagerBaseResponse<bool>> SaveTDSDeductorData(TDSDeductor TDSDeductor , string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSDeductor>>();

            try
            {
                var company = _context.Companies.FirstOrDefault(x => x.CompanyID == TDSDeductor.CompanyID);
                if (company == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSDeductor.DeductorID == 0)
                    {
                        // Insert
                        TDSDeductor _model = new TDSDeductor();
                        _model.CompanyID = TDSDeductor.CompanyID;
                        _model.DeductorCategory = TDSDeductor.DeductorCategory;
                        _model.DeductorName = TDSDeductor.DeductorName;
                        _model.TAN = TDSDeductor.TAN;
                        _model.PAN = TDSDeductor.PAN;
                        _model.Address1 = TDSDeductor.Address1;
                        _model.Address2 = TDSDeductor.Address2;
                        _model.City = TDSDeductor.City;
                        _model.State = TDSDeductor.State;
                        _model.PinCode = TDSDeductor.PinCode;
                        _model.Phone = TDSDeductor.Phone;
                        _model.Email = TDSDeductor.Email;
                        _model.AOCode = TDSDeductor.AOCode;
                        _model.CreatedBy = user.Id;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                       await  _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.TDSDeductor
                            .Where(x => x.DeductorID == TDSDeductor.DeductorID)
                            .FirstOrDefault();
                        originalTerm.IsActive = TDSDeductor.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Deductor Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSDeductor>>> GetAllTDSDeductorData(string DeductorID)
        {
            try
            {
                var plans =  _context.TDSDeductor.Where(x => x.DeductorID.ToString() == DeductorID).ToList();
                 

                return new ManagerBaseResponse<List<TDSDeductor>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Deductor Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSDeductor>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSDeductor>>> GetTDSDeductorFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSDeductor.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.DeductorName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeductorID);

                PageListed<TDSDeductor> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSDeductor>>
                {
                    Result = result.Data,
                    Message = "TDS Deductor Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSDeductor>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveTDSDeduteeData(TDSDeductee TDSDedutee, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSDeductee>>();

            try
            {
                var company = _context.Companies.FirstOrDefault(x => x.CompanyID == TDSDedutee.CompanyID);
                if(company == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not Found."
                    });
                }
                else
                {

             
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                if (TDSDedutee.DeducteeID == 0)
                {
                        // Insert
                        TDSDeductee _model = new TDSDeductee();
                    _model.CompanyID = TDSDedutee.CompanyID;
                    _model.DeducteeName = TDSDedutee.DeducteeName;
                        _model.DeducteeType = TDSDedutee.DeducteeType;
                    _model.AadhaarLinked = TDSDedutee.AadhaarLinked;
                    _model.PANStatus = TDSDedutee.PANStatus;
                    _model.PAN = TDSDedutee.PAN;
                    _model.Mobile = TDSDedutee.Mobile;
                    _model.Email = TDSDedutee.Email;
                    _model.ResidentStatus = TDSDedutee.ResidentStatus;
                    _model.IsActive = TDSDedutee.IsActive;
                    _model.CreatedBy = user.Id;
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                    _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.TDSDeductee
                        .Where(x => x.DeducteeID == TDSDedutee.DeducteeID)
                        .FirstOrDefault();
                    originalTerm.IsActive = TDSDedutee.IsActive;
                        originalTerm.PANStatus = TDSDedutee.PANStatus;
                        originalTerm.DeducteeType = TDSDedutee.DeducteeType;
                            originalTerm.DeducteeName = TDSDedutee.DeducteeName;

                        originalTerm.ResidentStatus = TDSDedutee.ResidentStatus;
                    originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                    originalTerm.UpdatedBy = user.Id;
                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }
                }
                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Dedutee Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSDeductee>>> GetAllTDSDeduteeData(string DeduteeID)
        {
            try
            {
                var plans =  _context.TDSDeductee.Where(x => x.DeducteeID.ToString() == DeduteeID).ToList();

                return new ManagerBaseResponse<List<TDSDeductee>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Dedutee Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSDeductee>>
                { 
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSDeductee>>> GetTDSDeduteeFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSDeductee.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.DeducteeName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeducteeID);

                PageListed<TDSDeductee> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSDeductee>>
                {
                    Result = result.Data,
                    Message = "TDS Dedutee Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSDeductee>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSyncTDSDeducteeData(int CompanyID, string UserName)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User Data not Found."
                    };
                }
                var data = (
                                             from e in _context.Employees
                                             join d in _context.Companies
                                                 on e.CompanyID
                                                 equals d.CompanyID
                                             where e.CompanyID == CompanyID
                                             select new { Deductee = d, Employee = e }
                                         ).ToList();
                if (data == null || data.Count == 0)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        IsSuccess = false,
                        Result = false,
                        Message = "Company ID is not Found",
                    };
                }
                else
                {                   
                      foreach (var item in data)
                        {
                            TDSDeductee _model = new TDSDeductee();
                            var deducteename = _context.TDSDeductee.FirstOrDefault(x => x.DeducteeName == item.Employee.FirstName + " " + item.Employee.LastName); 
                     
                            if (deducteename != null)
                            {
                                continue;
                            }
                        
                            else {
                                    _model.CompanyID = item.Employee.CompanyID;
                                    _model.DeducteeType = _commanfield.GetDeducteeType(item.Employee);
                                    _model.DeducteeName = item.Employee.FirstName + " " + item.Employee.LastName;
                                    _model.PAN = item.Employee.PAN;
                                    _model.PANStatus = string.IsNullOrEmpty(item.Employee.PAN)? PANStatus.NOT_AVAILABLE.ToString() : _commanfield.IsValidPAN(item.Employee.PAN) ? PANStatus.VALID.ToString(): PANStatus.INVALID.ToString();
                                    _model.AadhaarLinked = !string.IsNullOrEmpty(item.Employee.Aadhaar) ? true : false;
                                    _model.ResidentStatus = item.Employee.Nationality == "Indian" ? "RESIDENT" : "NON_RESIDENT";
                                    _model.Email = item.Employee.Email;
                                    _model.Mobile = item.Employee.Mobile;
                                    _model.IsActive = item.Employee.ActiveStatus;
                                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                                    _model.CreatedBy = user.Id;
                                    _context.TDSDeductee.Add(_model);
                                    await _context.SaveChangesAsync();

                            }
                       }
                }
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = true,
                    Result = true,
                    Message = "TDS Dedutee Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result =false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSyncTDSDeductorData(int CompanyID, string UserName)
        {
            try
            {               
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    if (user == null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "User Data not Found."
                        };
                    }
                    var data =  (
                                                 from d in _context.Companies
                                                 join e in _context.ProductOwners
                                                     on   d.ProductOwnerId  
                                                     equals  e.ProductOwnerId  
                                                  
                                                 where d.ProductOwnerId == e.ProductOwnerId 
                                               && d.CompanyID == CompanyID
                                                 select new { Company =d , product =e}
                                             ).ToList();
                if (data == null || data.Count == 0)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        IsSuccess = false,
                        Result = false,
                        Message = "Company ID is not Found",
                    };
                }
                else
                {

                    foreach (var item in data)
                    {
                        string address = item.product.Address;
                        string[] parts = address.Split(',');

                        TDSDeductor _model = new TDSDeductor();
                        var deducteename = _context.TDSDeductor.FirstOrDefault(x => x.DeductorName == item.product.OwnerName);

                        if (deducteename != null)
                        {
                            continue;
                        }

                        else
                        {
                            _model.CompanyID = item.Company.CompanyID;
                            _model.DeductorName = item.product.OwnerName;
                            _model.TAN = null;
                            _model.PAN = item.Company.PAN;
                            _model.DeductorCategory = "Company";
                            _model.Address1 = parts[0];
                            _model.Address2 = parts[1];
                            _model.City = parts.Length > 1 ? parts[2] : parts[1];
                            _model.State = item.product.State;
                            _model.PinCode = item.product.Pincode;
                            _model.Email = item.product.Email;
                            _model.Phone = item.product.Mobile;
                            _model.IsActive = item.product.IsActive;
                            _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            _model.CreatedBy = user.Id;
                            _context.TDSDeductor.Add(_model);
                            await _context.SaveChangesAsync();
                        }
                    }

                   
                }
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = true,
                    Result = true,
                    Message = "TDS Dedutee Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnData(TDSReturn TDSReturn, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSReturn>>();

            try
            {
                var Deductor = _context.TDSDeductor.FirstOrDefault(x => x.DeductorID == TDSReturn.DeductorID);
                if (Deductor == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Deductor Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSReturn.ReturnID == 0)
                    {
                        // Insert
                        TDSReturn _model = new TDSReturn();
                        _model.DeductorID = Deductor.DeductorID;
                        _model.FormType = TDSReturn.FormType;
                        _model.FinancialYear = TDSReturn.FinancialYear;
                        _model.Quarter = TDSReturn.Quarter;
                        _model.ReturnType = TDSReturn.ReturnType;
                        _model.OriginalTokenNo = TDSReturn.OriginalTokenNo;
                        _model.Status = TDSReturn.Status;
                        _model.FVUVersion = TDSReturn.FVUVersion;
                        _model.RPUVersion = TDSReturn.RPUVersion;                     
                        _model.CreatedBy = user.Id;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.TDSReturn
                            .Where(x => x.ReturnID == TDSReturn.ReturnID)
                            .FirstOrDefault();
                        originalTerm.DeductorID = Deductor.DeductorID;
                        originalTerm.FormType = TDSReturn.FormType;
                        originalTerm.FinancialYear = TDSReturn.FinancialYear;
                        originalTerm.Quarter = TDSReturn.Quarter;
                        originalTerm.ReturnType = TDSReturn.ReturnType;
                        originalTerm.OriginalTokenNo = TDSReturn.OriginalTokenNo;
                        originalTerm.Status = TDSReturn.Status;
                        originalTerm.FVUVersion = TDSReturn.FVUVersion;
                        originalTerm.RPUVersion = TDSReturn.RPUVersion;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Return Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSReturn>>> GetAllTDSReturnData(string ReturnID)
        {
            try
            {
                var plans = _context.TDSReturn.Where(x => x.ReturnID.ToString() == ReturnID).ToList();


                return new ManagerBaseResponse<List<TDSReturn>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSReturn>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSReturn>>> GetTDSReturnFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSReturn.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ReturnType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ReturnID);

                PageListed<TDSReturn> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSReturn>>
                {
                    Result = result.Data,
                    Message = "TDS Return Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSReturn>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
