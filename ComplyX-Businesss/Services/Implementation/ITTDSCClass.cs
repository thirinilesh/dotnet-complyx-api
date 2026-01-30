
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using ComplyX.Shared.Data;
using static ComplyX_Businesss.Helper.Commanfield;
using AppContext = ComplyX_Businesss.Helper.AppContext;

namespace ComplyX_Businesss.Services.Implementation
{
    public class ITTDSCClass : ITTDSServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;
        private readonly Nest.Filter _filter;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Commanfield _commanfield;

        public ITTDSCClass(AppContext context, Nest.Filter filter, UserManager<ApplicationUser> userManager, Commanfield commanfield)
        {
            _context = context;
            _filter = filter;
            _userManager = userManager;
            _commanfield = commanfield;
        }

        public async Task<ManagerBaseResponse<bool>> SaveTDSDeductorData(TDSDeductor TDSDeductor, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSDeductor>>();

            try
            {
                var company = _context.Companies.FirstOrDefault(x => x.CompanyID == TDSDeductor.CompanyID);
                if (company == null)
                {
                    return new ManagerBaseResponse<bool>
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
                        TDSDeductor originalTerm = new TDSDeductor();
                        originalTerm.CompanyID = TDSDeductor.CompanyID;
                        originalTerm.DeductorCategory = TDSDeductor.DeductorCategory;
                        originalTerm.DeductorName = TDSDeductor.DeductorName;
                        originalTerm.TAN = TDSDeductor.TAN;
                        originalTerm.PAN = TDSDeductor.PAN;
                        originalTerm.Address1 = TDSDeductor.Address1;
                        originalTerm.Address2 = TDSDeductor.Address2;
                        originalTerm.City = TDSDeductor.City;
                        originalTerm.State = TDSDeductor.State;
                        originalTerm.PinCode = TDSDeductor.PinCode;
                        originalTerm.Phone = TDSDeductor.Phone;
                        originalTerm.Email = TDSDeductor.Email;
                        originalTerm.AOCode = TDSDeductor.AOCode;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(originalTerm);
                        await _context.SaveChangesAsync();
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
                var plans = _context.TDSDeductor.Where(x => x.DeductorID.ToString() == DeductorID).ToList();


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
                if (company == null)
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
                        TDSDeductee originalTerm = new TDSDeductee();
                        originalTerm.CompanyID = TDSDedutee.CompanyID;
                        originalTerm.DeducteeName = TDSDedutee.DeducteeName;
                        originalTerm.DeducteeType = TDSDedutee.DeducteeType;
                        originalTerm.AadhaarLinked = TDSDedutee.AadhaarLinked;
                        originalTerm.PANStatus = TDSDedutee.PANStatus;
                        originalTerm.PAN = TDSDedutee.PAN;
                        originalTerm.Mobile = TDSDedutee.Mobile;
                        originalTerm.Email = TDSDedutee.Email;
                        originalTerm.ResidentStatus = TDSDedutee.ResidentStatus;
                        originalTerm.IsActive = TDSDedutee.IsActive;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(originalTerm);
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
                var plans = _context.TDSDeductee.Where(x => x.DeducteeID.ToString() == DeduteeID).ToList();

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
                        TDSDeductee originalTerm = new TDSDeductee();
                        var deducteename = _context.TDSDeductee.FirstOrDefault(x => x.DeducteeName == item.Employee.FirstName + " " + item.Employee.LastName);

                        if (deducteename != null)
                        {
                            continue;
                        }

                        else
                        {
                            originalTerm.CompanyID = item.Employee.CompanyID;
                            originalTerm.DeducteeType = _commanfield.GetDeducteeType(deducteename.DeducteeType);
                            originalTerm.DeducteeName = item.Employee.FirstName + " " + item.Employee.LastName;
                            originalTerm.PAN = item.Employee.PAN;
                            originalTerm.PANStatus = string.IsNullOrEmpty(item.Employee.PAN) ? PANStatus.NOT_AVAILABLE.ToString() : _commanfield.IsValidPAN(item.Employee.PAN) ? PANStatus.VALID.ToString() : PANStatus.INVALID.ToString();
                            originalTerm.AadhaarLinked = !string.IsNullOrEmpty(item.Employee.Aadhaar) ? true : false;
                            originalTerm.ResidentStatus = item.Employee.Nationality == "Indian" ? "RESIDENT" : "NON_RESIDENT";
                            originalTerm.Email = item.Employee.Email;
                            originalTerm.Mobile = item.Employee.Mobile;
                            originalTerm.IsActive = item.Employee.ActiveStatus;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            _context.TDSDeductee.Add(originalTerm);
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
                var data = (
                                             from d in _context.Companies
                                             join e in _context.ProductOwners
                                                 on d.ProductOwnerId
                                                 equals e.ProductOwnerId

                                             where d.ProductOwnerId == e.ProductOwnerId
                                           && d.CompanyID == CompanyID
                                             select new { Company = d, product = e }
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

                        TDSDeductor originalTerm = new TDSDeductor();
                        var deducteename = _context.TDSDeductor.FirstOrDefault(x => x.DeductorName == item.product.OwnerName);

                        if (deducteename != null)
                        {
                            continue;
                        }

                        else
                        {
                            originalTerm.CompanyID = item.Company.CompanyID;
                            originalTerm.DeductorName = item.product.OwnerName;
                            originalTerm.TAN = null;
                            originalTerm.PAN = item.Company.PAN;
                            originalTerm.DeductorCategory = "Company";
                            originalTerm.Address1 = parts[0];
                            originalTerm.Address2 = parts[1];
                            originalTerm.City = parts.Length > 1 ? parts[2] : parts[1];
                            originalTerm.State = item.product.State;
                            originalTerm.PinCode = item.product.Pincode;
                            originalTerm.Email = item.product.Email;
                            originalTerm.Phone = item.product.Mobile;
                            originalTerm.IsActive = item.product.IsActive;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            _context.TDSDeductor.Add(originalTerm);
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
                        TDSReturn originalTerm = new TDSReturn();
                        originalTerm.DeductorID = Deductor.DeductorID;
                        originalTerm.FormType = _commanfield.FormType(TDSReturn.FormType);
                        originalTerm.FinancialYear = _commanfield.FinancialYear(TDSReturn.FinancialYear);
                        originalTerm.Quarter = TDSReturn.Quarter;
                        originalTerm.ReturnType = TDSReturn.ReturnType;
                        originalTerm.OriginalTokenNo = TDSReturn.ReturnType == "CORRECTION" ? TDSReturn.OriginalTokenNo : null;
                        originalTerm.Status = TDSReturn.Status;
                        originalTerm.FVUVersion = TDSReturn.FVUVersion;
                        originalTerm.RPUVersion = TDSReturn.RPUVersion;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(originalTerm);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.TDSReturn
                            .Where(x => x.ReturnID == TDSReturn.ReturnID)
                            .FirstOrDefault();
                        originalTerm.DeductorID = Deductor.DeductorID;
                        originalTerm.FormType = _commanfield.FormType(TDSReturn.FormType);
                        originalTerm.FinancialYear = _commanfield.FinancialYear(TDSReturn.FinancialYear);
                        originalTerm.Quarter = TDSReturn.Quarter;
                        originalTerm.ReturnType = TDSReturn.ReturnType;
                        originalTerm.OriginalTokenNo = TDSReturn.ReturnType == "CORRECTION" ? TDSReturn.OriginalTokenNo : null;
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
        public async Task<ManagerBaseResponse<bool>> SaveTDSEntryData(TDSEntry TDSEntry, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSEntry>>();

            try
            {
                var Deductor = _context.TDSDeductor.FirstOrDefault(x => x.DeductorID == TDSEntry.DeductorID);
                var Deductee = _context.TDSDeductee.FirstOrDefault(x => x.DeducteeID == TDSEntry.DeducteeID);
                if (Deductor == null || Deductee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Deductor and Deductee Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSEntry.EntryID == 0)
                    {
                        // Insert
                        TDSEntry _model = new TDSEntry();
                        _model.DeductorID = TDSEntry.DeductorID;
                        _model.DeducteeID = TDSEntry.DeducteeID;
                        _model.SectionCode = TDSEntry.SectionCode;
                        _model.PaymentDate = TDSEntry.PaymentDate;
                        _model.AmountPaid = TDSEntry.AmountPaid;
                        _model.TaxableAmount = TDSEntry.TaxableAmount;
                        _model.TDSRate = TDSEntry.TDSRate;
                        _model.TDSAmount = TDSEntry.TDSAmount;
                        _model.Surcharge = TDSEntry.Surcharge;
                        _model.Cess = TDSEntry.Cess;
                        _model.TotalTDS = TDSEntry.TDSAmount + TDSEntry.Surcharge + TDSEntry.Cess;
                        _model.HigherRateApplied = TDSEntry.HigherRateApplied;
                        _model.HigherRateReason = TDSEntry.HigherRateReason;
                        _model.IsMappedToReturn = TDSEntry.IsMappedToReturn;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.TDSEntry
                            .Where(x => x.EntryID == TDSEntry.EntryID)
                            .FirstOrDefault();
                        originalTerm.DeductorID = TDSEntry.DeductorID;
                        originalTerm.DeducteeID = TDSEntry.DeducteeID;
                        originalTerm.SectionCode = TDSEntry.SectionCode;
                        originalTerm.PaymentDate = TDSEntry.PaymentDate;
                        originalTerm.AmountPaid = TDSEntry.AmountPaid;
                        originalTerm.TaxableAmount = TDSEntry.TaxableAmount;
                        originalTerm.TDSRate = TDSEntry.TDSRate;
                        originalTerm.TDSAmount = TDSEntry.TDSAmount;
                        originalTerm.Surcharge = TDSEntry.Surcharge;
                        originalTerm.Cess = TDSEntry.Cess;
                        originalTerm.TotalTDS = TDSEntry.TDSAmount + TDSEntry.Surcharge + TDSEntry.Cess;
                        originalTerm.HigherRateApplied = TDSEntry.HigherRateApplied;
                        originalTerm.HigherRateReason = TDSEntry.HigherRateReason;
                        originalTerm.IsMappedToReturn = TDSEntry.IsMappedToReturn;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Entry Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSEntry>>> GetAllTDSEntryData(string EntryID)
        {
            try
            {
                var plans = _context.TDSEntry.Where(x => x.EntryID.ToString() == EntryID).ToList();


                return new ManagerBaseResponse<List<TDSEntry>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSEntry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSEntry>>> GetTDSEntryFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSEntry.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.SectionCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EntryID);

                PageListed<TDSEntry> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSEntry>>
                {
                    Result = result.Data,
                    Message = "TDS Entry Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSEntry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSChallanData(TDSChallan TDSChallan, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSChallan>>();

            try
            {
                var Deductor = _context.TDSDeductor.FirstOrDefault(x => x.DeductorID == TDSChallan.DeductorID);
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
                    if (TDSChallan.ChallanID == 0)
                    {
                        // Insert
                        TDSChallan _model = new TDSChallan();
                        _model.DeductorID = Deductor.DeductorID;
                        _model.BSRCode = TDSChallan.BSRCode;
                        _model.ChallanDate = TDSChallan.ChallanDate;
                        _model.ChallanSerialNo = TDSChallan.ChallanSerialNo;
                        _model.SectionCode = TDSChallan.SectionCode;
                        _model.TaxAmount = TDSChallan.TaxAmount;
                        _model.InterestAmount = TDSChallan.InterestAmount;
                        _model.LateFeeAmount = TDSChallan.LateFeeAmount;
                        _model.OtherAmount = TDSChallan.OtherAmount;
                        _model.TotalAmount = TDSChallan.TaxAmount + TDSChallan.InterestAmount + TDSChallan.LateFeeAmount + TDSChallan.OtherAmount;
                        _model.MatchedWithOLTAS = TDSChallan.MatchedWithOLTAS;
                        _model.IsMappedToReturn = TDSChallan.IsMappedToReturn;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.TDSChallan
                            .Where(x => x.ChallanID == TDSChallan.ChallanID)
                            .FirstOrDefault();
                        originalTerm.DeductorID = Deductor.DeductorID;
                        originalTerm.BSRCode = TDSChallan.BSRCode;
                        originalTerm.ChallanDate = TDSChallan.ChallanDate;
                        originalTerm.ChallanSerialNo = TDSChallan.ChallanSerialNo;
                        originalTerm.SectionCode = TDSChallan.SectionCode;
                        originalTerm.TaxAmount = TDSChallan.TaxAmount;
                        originalTerm.InterestAmount = TDSChallan.InterestAmount;
                        originalTerm.LateFeeAmount = TDSChallan.LateFeeAmount;
                        originalTerm.OtherAmount = TDSChallan.OtherAmount;
                        originalTerm.TotalAmount = TDSChallan.TaxAmount + TDSChallan.InterestAmount + TDSChallan.LateFeeAmount + TDSChallan.OtherAmount;
                        originalTerm.MatchedWithOLTAS = TDSChallan.MatchedWithOLTAS;
                        originalTerm.IsMappedToReturn = TDSChallan.IsMappedToReturn;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Challan Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSChallan>>> GetAllTDSChallanData(string ChallanID)
        {
            try
            {
                var plans = _context.TDSChallan.Where(x => x.ChallanID.ToString() == ChallanID).ToList();


                return new ManagerBaseResponse<List<TDSChallan>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Challan Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSChallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSChallan>>> GetTDSChallanFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSChallan.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.BSRCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ChallanID);

                PageListed<TDSChallan> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSChallan>>
                {
                    Result = result.Data,
                    Message = "TDS Challan Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSChallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnChallanData(TDSReturnChallan TDSReturnChallan, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSReturnChallan>>();

            try
            {
                var Return = _context.TDSReturn.FirstOrDefault(x => x.ReturnID == TDSReturnChallan.ReturnID);
                var Challan = _context.TDSChallan.FirstOrDefault(x => x.ChallanID == TDSReturnChallan.ChallanID);

                if (Return == null || Challan == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Return and Challan Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    var data = _context.TDsReturnChallan.FirstOrDefault(x => x.ChallanID == TDSReturnChallan.ChallanID && x.ReturnID == TDSReturnChallan.ReturnID);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Return ID and Challan ID are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSReturnChallan.ReturnChallanID == 0)
                        {
                            // Insert
                            TDSReturnChallan _model = new TDSReturnChallan();
                            _model.ReturnID = TDSReturnChallan.ReturnID;
                            _model.ChallanID = TDSReturnChallan.ChallanID;

                            _context.Add(_model);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.TDsReturnChallan
                                .Where(x => x.ReturnChallanID == TDSReturnChallan.ReturnChallanID)
                                .FirstOrDefault();
                            originalTerm.ReturnID = TDSReturnChallan.ReturnID;
                            originalTerm.ChallanID = TDSReturnChallan.ChallanID;
                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Return and Challan Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSReturnChallan>>> GetAllTDSReturnChallanData(string TDSReturnChallanID)
        {
            try
            {
                var plans = _context.TDsReturnChallan.Where(x => x.ReturnChallanID.ToString() == TDSReturnChallanID).ToList();


                return new ManagerBaseResponse<List<TDSReturnChallan>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return and Challan Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSReturnChallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSReturnChallan>>> GetTDSReturnChallanFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDsReturnChallan.AsQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.ReturnChallanID);

                PageListed<TDSReturnChallan> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSReturnChallan>>
                {
                    Result = result.Data,
                    Message = "TDS Return and Challan Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSReturnChallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnEntryData(TDSReturnEntry TDSReturnEntry, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSReturnEntry>>();

            try
            {
                var Return = _context.TDSReturn.FirstOrDefault(x => x.ReturnID == TDSReturnEntry.ReturnID);
                var Entry = _context.TDSEntry.FirstOrDefault(x => x.EntryID == TDSReturnEntry.EntryID);

                if (Return == null || Entry == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Return and Entry Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    var data = _context.TDSReturnEntry.FirstOrDefault(x => x.ReturnID == TDSReturnEntry.ReturnID && x.EntryID == TDSReturnEntry.EntryID);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Return ID and Entry ID are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSReturnEntry.ReturnEntryId == 0)
                        {
                            // Insert
                            TDSReturnEntry _model = new TDSReturnEntry();
                            _model.ReturnID = TDSReturnEntry.ReturnID;
                            _model.EntryID = TDSReturnEntry.EntryID;

                            _context.Add(_model);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.TDSReturnEntry
                                .Where(x => x.ReturnEntryId == TDSReturnEntry.ReturnEntryId)
                                .FirstOrDefault();
                            originalTerm.ReturnID = TDSReturnEntry.ReturnID;
                            originalTerm.EntryID = TDSReturnEntry.EntryID;
                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Return and Entry Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSReturnEntry>>> GetAllTDSReturnEntryData(string TDSReturnEntryID)
        {
            try
            {
                var plans = _context.TDSReturnEntry.Where(x => x.ReturnEntryId.ToString() == TDSReturnEntryID).ToList();


                return new ManagerBaseResponse<List<TDSReturnEntry>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return and Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSReturnEntry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSReturnEntry>>> GetTDSReturnEntryFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSReturnEntry.AsQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.ReturnEntryId);

                PageListed<TDSReturnEntry> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSReturnEntry>>
                {
                    Result = result.Data,
                    Message = "TDS Return and Entry Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSReturnEntry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSChallanAllocationData(TDSChallanAllocation TDSChallanAllocation, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSChallanAllocation>>();

            try
            {
                var Challan = _context.TDSChallan.FirstOrDefault(x => x.ChallanID == TDSChallanAllocation.ChallanID);
                var Entry = _context.TDSEntry.FirstOrDefault(x => x.EntryID == TDSChallanAllocation.EntryID);

                if (Challan == null || Entry == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Challan and Entry Data not Found."
                    };
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                    var data = _context.TDSChallanAllocation.FirstOrDefault(x => x.EntryID == TDSChallanAllocation.EntryID);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Entry ID are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSChallanAllocation.AllocationID == 0)
                        {
                            // Insert
                            TDSChallanAllocation _model = new TDSChallanAllocation();
                            _model.ChallanID = TDSChallanAllocation.ChallanID;
                            _model.EntryID = TDSChallanAllocation.EntryID;
                            _model.AllocatedTDSAmount = TDSChallanAllocation.AllocatedTDSAmount;
                            _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            _model.CreatedBy = user.Id;

                            _context.Add(_model);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.TDSChallanAllocation
                                .Where(x => x.AllocationID == TDSChallanAllocation.AllocationID)
                                .FirstOrDefault();
                            originalTerm.ChallanID = TDSChallanAllocation.ChallanID;
                            originalTerm.EntryID = TDSChallanAllocation.EntryID;
                            originalTerm.AllocatedTDSAmount = TDSChallanAllocation.AllocatedTDSAmount;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Challan and Entry Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSChallanAllocation>>> GetAllTDSChallanAllocationData(string TDSChallanAllocatinoID)
        {
            try
            {
                var plans = _context.TDSChallanAllocation.Where(x => x.AllocationID.ToString() == TDSChallanAllocatinoID).ToList();


                return new ManagerBaseResponse<List<TDSChallanAllocation>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Challan and Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSChallanAllocation>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSChallanAllocation>>> GetTDSChallanAllocationFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSChallanAllocation.AsQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.AllocationID);

                PageListed<TDSChallanAllocation> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSChallanAllocation>>
                {
                    Result = result.Data,
                    Message = "TDS Challan and Entry Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSChallanAllocation>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSRatesData(TDSRates TDSRates, string UserName)
        {
            var response = new ManagerBaseResponse<List<TDSRates>>();

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


                        if (TDSRates.TaxID == 0)
                        {
                            // Insert
                            TDSRates _model = new TDSRates();
                            _model.TaxName = TDSRates.TaxName;
                        _model.Rate = TDSRates.Rate;
                        _model.TaxType = TDSRates.TaxType;
                        _model.IsActive = TDSRates.IsActive;
                            _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            _model.CreatedBy = user.Id;

                            _context.Add(_model);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.TDSRates
                                .Where(x => x.TaxID == TDSRates.TaxID)
                                .FirstOrDefault();
                        originalTerm.TaxName = TDSRates.TaxName;
                        originalTerm.Rate = TDSRates.Rate;
                        originalTerm.TaxType = TDSRates.TaxType;
                        originalTerm.IsActive = TDSRates.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                        _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Rates Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TDSRates>>> GetAllTDSRatesData(string TaxID)
        {
            try
            {
                var plans = _context.TDSRates.Where(x => x.TaxID.ToString() == TaxID).ToList();


                return new ManagerBaseResponse<List<TDSRates>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Rates Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDSRates>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDSRates>>> GetTDSRatesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDSRates.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.TaxName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.TaxID);

                PageListed<TDSRates> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDSRates>>
                {
                    Result = result.Data,
                    Message = "TDS Rates Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDSRates>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
