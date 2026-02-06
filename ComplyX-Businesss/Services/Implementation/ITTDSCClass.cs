
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using ComplyX.Shared.Data;
using static ComplyX_Businesss.Helper.Commanfield;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.Tdsdeductor;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.Tdsdeductee;
using ComplyX_Businesss.Models.Tdsreturn;
using ComplyX_Businesss.Models.Tdsentry;
using ComplyX_Businesss.Models.Tdschallan;
using ComplyX_Businesss.Models.TdsreturnChallan;
using ComplyX_Businesss.Models.TdsreturnEntry;
using ComplyX_Businesss.Models.TdschallanAllocation;
using ComplyX_Businesss.Models.Tdsrate;
using ComplyX.Data.DbContexts;

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
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly Commanfield _commanfield;
        private readonly IUnitOfWork _UnitOfWork;

        public ITTDSCClass(AppDbContext context, Nest.Filter filter, UserManager<ApplicationUsers> userManager, Commanfield commanfield, IUnitOfWork unitOfWork)
        {
            _context = context;
            _filter = filter;
            _userManager = userManager;
            _commanfield = commanfield;
            _UnitOfWork = unitOfWork;
        }

        public async Task<ManagerBaseResponse<bool>> SaveTDSDeductorData(TdsdeductorRequestModel TDSDeductor, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdsdeductor>>();

            try
            {
                var company = _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefault(x => x.CompanyId == TDSDeductor.CompanyId);
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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSDeductor.DeductorId == 0)
                    {
                        // Insert
                        Tdsdeductor originalTerm = new Tdsdeductor();
                        originalTerm.CompanyId = TDSDeductor.CompanyId;
                        originalTerm.DeductorCategory = TDSDeductor.DeductorCategory;
                        originalTerm.DeductorName = TDSDeductor.DeductorName;
                        originalTerm.Tan = TDSDeductor.Tan;
                        originalTerm.Pan = TDSDeductor.Pan;
                        originalTerm.Address1 = TDSDeductor.Address1;
                        originalTerm.Address2 = TDSDeductor.Address2;
                        originalTerm.City = TDSDeductor.City;
                        originalTerm.State = TDSDeductor.State;
                        originalTerm.Pincode = TDSDeductor.Pincode;
                        originalTerm.Phone = TDSDeductor.Phone;
                        originalTerm.Email = TDSDeductor.Email;
                        originalTerm.Aocode = TDSDeductor.Aocode;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.TdsdeductorRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.TdsdeductorRespositories.GetQueryable()
                            .Where(x => x.DeductorId == TDSDeductor.DeductorId)
                            .FirstOrDefault();
                        originalTerm.IsActive = TDSDeductor.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                      
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsdeductorResponseModel>>> GetAllTDSDeductorData(string DeductorId)
        {
            try
            {
                var plans = _UnitOfWork.TdsdeductorRespositories.GetQueryable().
                    Where(x => x.DeductorId.ToString() == DeductorId)
                      .Select(x => new TdsdeductorResponseModel
                      {
                          DeductorId = x.DeductorId,
                          CompanyId = x.CompanyId,
                          DeductorName = x.DeductorName,
                          Tan = x.Tan,
                          Pan = x.Pan,
                          DeductorCategory = x.DeductorCategory,
                          Address1 = x.Address1,
                          Address2 = x.Address2,
                          City = x.City,
                          State = x.State,
                          Pincode = x.Pincode,
                          Phone = x.Phone,
                          Email = x.Email,
                          Aocode = x.Aocode,
                          IsActive = x.IsActive,
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedBy = x.UpdatedBy,
                          UpdatedAt = x.UpdatedAt
                      }).ToList();


                return new ManagerBaseResponse<List<TdsdeductorResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Deductor Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsdeductorResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdsdeductor>>> GetTDSDeductorFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsdeductorRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.DeductorName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeductorId);

                PageListed<Tdsdeductor> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdsdeductor>>
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

                return new ManagerBaseResponse<IEnumerable<Tdsdeductor>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSDeduteeData(TdsdeducteeRequestModel TDSDedutee, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdsdeductee>>();

            try
            {
                var company = _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefault(x => x.CompanyId == TDSDedutee.CompanyId);
                if (company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not Found."
                    } ;
                }
                else
                {


                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSDedutee.DeducteeId == 0)
                    {
                        // Insert
                        Tdsdeductee originalTerm = new Tdsdeductee();
                        originalTerm.CompanyId = TDSDedutee.CompanyId;
                        originalTerm.DeducteeName = TDSDedutee.DeducteeName;
                        originalTerm.DeducteeType = TDSDedutee.DeducteeType;
                        originalTerm.AadhaarLinked = TDSDedutee.AadhaarLinked;
                        originalTerm.Panstatus = TDSDedutee.Panstatus;
                        originalTerm.Pan = TDSDedutee.Pan;
                        originalTerm.Mobile = TDSDedutee.Mobile;
                        originalTerm.Email = TDSDedutee.Email;
                        originalTerm.ResidentStatus = TDSDedutee.ResidentStatus;
                        originalTerm.IsActive = TDSDedutee.IsActive;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                     await  _UnitOfWork.TdsdeducteeRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.TdsdeducteeRespositories.GetQueryable()
                            .Where(x => x.DeducteeId == TDSDedutee.DeducteeId)
                            .FirstOrDefault();
                        originalTerm.IsActive = TDSDedutee.IsActive;
                        originalTerm.Panstatus = TDSDedutee.Panstatus;
                        originalTerm.DeducteeType = TDSDedutee.DeducteeType;
                        originalTerm.DeducteeName = TDSDedutee.DeducteeName;
                        originalTerm.ResidentStatus = TDSDedutee.ResidentStatus;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                       
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Dedutee Saved Successfully."
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
        public async Task<ManagerBaseResponse<List<TdsdeducteeResponseModel>>> GetAllTDSDeduteeData(string DeduteeId)
        {
            try
            {
                var plans = _UnitOfWork.TdsdeducteeRespositories.GetQueryable()
                    .Where(x => x.DeducteeId.ToString() == DeduteeId)
                        .Select(x => new TdsdeducteeResponseModel
                        {
                            DeducteeId = x.DeducteeId,
                            CompanyId = x.CompanyId,
                            DeducteeType = x.DeducteeType,
                            DeducteeName = x.DeducteeName,
                            Pan = x.Pan,
                            Panstatus = x.Panstatus,
                            AadhaarLinked = x.AadhaarLinked,
                            ResidentStatus = x.ResidentStatus,
                            Email = x.Email,
                            Mobile = x.Mobile,
                            IsActive = x.IsActive,
                            CreatedBy = x.CreatedBy,
                            CreatedAt = x.CreatedAt,
                            UpdatedBy = x.UpdatedBy,
                            UpdatedAt = x.UpdatedAt
                        }).ToList();

                return new ManagerBaseResponse<List<TdsdeducteeResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Dedutee Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsdeducteeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdsdeductee>>> GetTDSDeduteeFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsdeducteeRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.DeducteeName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeducteeId);

                PageListed<Tdsdeductee> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdsdeductee>>
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

                return new ManagerBaseResponse<IEnumerable<Tdsdeductee>>
                { 
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveSyncTDSDeducteeData(int CompanyId, string UserName)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User Data not Found."
                    };
                }
                var data = (
                                             from e in _UnitOfWork.EmployeeRespositories.GetQueryable()
                                             join d in _UnitOfWork.CompanyRepository.GetQueryable()
                                                 on e.CompanyId
                                                 equals d.CompanyId
                                             where e.CompanyId == CompanyId
                                             select new { Deductee = d, Employee = e }
                                         ).ToList();
                if (data == null || data.Count == 0)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        IsSuccess = false,
                        Result = false,
                        Message = "Company Id is not Found",
                    };
                }
                else
                {
                    foreach (var item in data)
                    {
                        Tdsdeductee originalTerm = new Tdsdeductee();
                        var deducteename = _UnitOfWork.TdsdeducteeRespositories.GetQueryable().FirstOrDefault(x => x.DeducteeName == item.Employee.FirstName + " " + item.Employee.LastName);

                        if (deducteename != null)
                        {
                            continue;
                        }

                        else
                        {
                            originalTerm.CompanyId = (int)item.Employee.CompanyId;
                            originalTerm.DeducteeType = _commanfield.GetDeducteeType(deducteename.DeducteeType);
                            originalTerm.DeducteeName = item.Employee.FirstName + " " + item.Employee.LastName;
                            originalTerm.Pan = item.Employee.Pan;
                            originalTerm.Panstatus = string.IsNullOrEmpty(item.Employee.Pan) ? PANStatus.NOT_AVAILABLE.ToString() : _commanfield.IsValidPAN(item.Employee.Pan) ? PANStatus.VALID.ToString() : PANStatus.INVALID.ToString();
                            originalTerm.AadhaarLinked = !string.IsNullOrEmpty(item.Employee.Aadhaar) ? true : false;
                            originalTerm.ResidentStatus = item.Employee.Nationality == "Indian" ? "RESIdENT" : "NON_RESIdENT";
                            originalTerm.Email = item.Employee.Email;
                            originalTerm.Mobile = item.Employee.Mobile;
                            originalTerm.IsActive = (bool)item.Employee.ActiveStatus;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            
                            await _UnitOfWork.TdsdeducteeRespositories.AddAsync(originalTerm);

                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<bool>> SaveSyncTDSDeductorData(int CompanyId, string UserName)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User Data not Found."
                    };
                }
                var data = (
                                             from d in _UnitOfWork.CompanyRepository.GetQueryable()
                                             join e in _UnitOfWork.ProductOwnerRepositories.GetQueryable()
                                                 on d.ProductOwnerId
                                                 equals e.ProductOwnerId

                                             where d.ProductOwnerId == e.ProductOwnerId
                                           && d.CompanyId == CompanyId
                                             select new { Company = d, product = e }
                                         ).ToList();
                if (data == null || data.Count == 0)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        IsSuccess = false,
                        Result = false,
                        Message = "Company Id is not Found",
                    };
                }
                else
                {

                    foreach (var item in data)
                    {
                        string address = item.product.Address;
                        string[] parts = address.Split(',');

                        Tdsdeductor originalTerm = new Tdsdeductor();
                        var deducteename = _UnitOfWork.TdsdeductorRespositories.GetQueryable().FirstOrDefault(x => x.DeductorName == item.product.OwnerName);

                        if (deducteename != null)
                        {
                            continue;
                        }

                        else
                        {
                            originalTerm.CompanyId = item.Company.CompanyId;
                            originalTerm.DeductorName = item.product.OwnerName;
                            originalTerm.Tan = null;
                            originalTerm.Pan = item.Company.Pan;
                            originalTerm.DeductorCategory = "Company";
                            originalTerm.Address1 = parts[0];
                            originalTerm.Address2 = parts[1];
                            originalTerm.City = parts.Length > 1 ? parts[2] : parts[1];
                            originalTerm.State = item.product.State;
                            originalTerm.Pincode = item.product.Pincode;
                            originalTerm.Email = item.product.Email;
                            originalTerm.Phone = item.product.Mobile;
                            originalTerm.IsActive = (bool)item.product.IsActive;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                           await _UnitOfWork.TdsdeductorRespositories.AddAsync(originalTerm);
                        }
                    }


                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnData(TdsreturnRequestModel TDSReturn, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdsreturn>>();

            try
            {
                var Deductor = _UnitOfWork.TdsdeductorRespositories.GetQueryable().FirstOrDefault(x => x.DeductorId == TDSReturn.DeductorId);
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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSReturn.ReturnId == 0)
                    {
                        // Insert
                        Tdsreturn originalTerm = new Tdsreturn();
                        originalTerm.DeductorId = Deductor.DeductorId;
                        originalTerm.FormType = _commanfield.FormType(TDSReturn.FormType);
                        originalTerm.FinancialYear = _commanfield.FinancialYear(TDSReturn.FinancialYear);
                        originalTerm.Quarter = TDSReturn.Quarter;
                        originalTerm.ReturnType = TDSReturn.ReturnType;
                        originalTerm.OriginalTokenNo = TDSReturn.ReturnType == "CORRECTION" ? TDSReturn.OriginalTokenNo : null;
                        originalTerm.Status = TDSReturn.Status;
                        originalTerm.Fvuversion = TDSReturn.Fvuversion;
                        originalTerm.Rpuversion = TDSReturn.Rpuversion;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await _UnitOfWork.TDSReturnRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.TDSReturnRespositories.GetQueryable()
                            .Where(x => x.ReturnId == TDSReturn.ReturnId)
                            .FirstOrDefault();
                        originalTerm.DeductorId = Deductor.DeductorId;
                        originalTerm.FormType = _commanfield.FormType(TDSReturn.FormType);
                        originalTerm.FinancialYear = _commanfield.FinancialYear(TDSReturn.FinancialYear);
                        originalTerm.Quarter = TDSReturn.Quarter;
                        originalTerm.ReturnType = TDSReturn.ReturnType;
                        originalTerm.OriginalTokenNo = TDSReturn.ReturnType == "CORRECTION" ? TDSReturn.OriginalTokenNo : null;
                        originalTerm.Status = TDSReturn.Status;
                        originalTerm.Fvuversion = TDSReturn.Fvuversion;
                        originalTerm.Rpuversion = TDSReturn.Rpuversion;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                         
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsreturnResponseModel>>> GetAllTDSReturnData(string ReturnId)
        {
            try
            {
                var plans = _UnitOfWork.TDSReturnRespositories.GetQueryable().Where(x => x.ReturnId.ToString() == ReturnId)
                    .Select(x => new TdsreturnResponseModel
                    {
                        ReturnId = x.ReturnId,
                        DeductorId = x.DeductorId,
                        FormType = x.FormType,
                        FinancialYear = x.FinancialYear,
                        Quarter = x.Quarter,
                        ReturnType = x.ReturnType,
                        OriginalTokenNo = x.OriginalTokenNo,
                        Status = x.Status,
                        Fvuversion = x.Fvuversion,
                        Rpuversion = x.Rpuversion,
                        CreatedBy = x.CreatedBy,
                        CreatedAt = x.CreatedAt,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();


                return new ManagerBaseResponse<List<TdsreturnResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsreturnResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdsreturn>>> GetTDSReturnFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TDSReturnRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ReturnType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ReturnId);

                PageListed<Tdsreturn> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdsreturn>>
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

                return new ManagerBaseResponse<IEnumerable<Tdsreturn>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSEntryData(TdsentryRequestModel TDSEntry, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdsentry>>();

            try
            {
                var Deductor = _UnitOfWork.TdsdeductorRespositories.GetQueryable().FirstOrDefault(x => x.DeductorId == TDSEntry.DeductorId);
                var Deductee = _UnitOfWork.TdsdeducteeRespositories.GetQueryable().FirstOrDefault(x => x.DeducteeId == TDSEntry.DeducteeId);
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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSEntry.EntryId == 0)
                    {
                        // Insert
                        Tdsentry _model = new Tdsentry();
                        _model.DeductorId = TDSEntry.DeductorId;
                        _model.DeducteeId = TDSEntry.DeducteeId;
                        _model.SectionCode = TDSEntry.SectionCode;
                        _model.PaymentDate = TDSEntry.PaymentDate;
                        _model.AmountPaid = TDSEntry.AmountPaid;
                        _model.TaxableAmount = TDSEntry.TaxableAmount;
                        _model.Tdsrate = TDSEntry.Tdsrate;
                        _model.Tdsamount = TDSEntry.Tdsamount;
                        _model.Surcharge = TDSEntry.Surcharge;
                        _model.Cess = TDSEntry.Cess;
                        _model.TotalTds = TDSEntry.Tdsamount + TDSEntry.Surcharge + TDSEntry.Cess;
                        _model.HigherRateApplied = TDSEntry.HigherRateApplied;
                        _model.HigherRateReason = TDSEntry.HigherRateReason;
                        _model.IsMappedToReturn = TDSEntry.IsMappedToReturn;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                      await _UnitOfWork.TdsentryRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.TdsentryRespositories.GetQueryable()
                            .Where(x => x.EntryId == TDSEntry.EntryId)
                            .FirstOrDefault();
                        originalTerm.DeductorId = TDSEntry.DeductorId;
                        originalTerm.DeducteeId = TDSEntry.DeducteeId;
                        originalTerm.SectionCode = TDSEntry.SectionCode;
                        originalTerm.PaymentDate = TDSEntry.PaymentDate;
                        originalTerm.AmountPaid = TDSEntry.AmountPaid;
                        originalTerm.TaxableAmount = TDSEntry.TaxableAmount;
                        originalTerm.Tdsrate = TDSEntry.Tdsrate;
                        originalTerm.Tdsamount = TDSEntry.Tdsamount;
                        originalTerm.Surcharge = TDSEntry.Surcharge;
                        originalTerm.Cess = TDSEntry.Cess;
                        originalTerm.TotalTds = TDSEntry.Tdsamount + TDSEntry.Surcharge + TDSEntry.Cess;
                        originalTerm.HigherRateApplied = TDSEntry.HigherRateApplied;
                        originalTerm.HigherRateReason = TDSEntry.HigherRateReason;
                        originalTerm.IsMappedToReturn = TDSEntry.IsMappedToReturn;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsentryResponseModel>>> GetAllTDSEntryData(string EntryId)
        {
            try
            {
                var plans = _UnitOfWork.TdsentryRespositories.GetQueryable().Where(x => x.EntryId.ToString() == EntryId)
                     .Select(x => new TdsentryResponseModel
                     {
                         EntryId = x.EntryId,
                         DeductorId = x.DeductorId,
                         DeducteeId = x.DeducteeId,
                         SectionCode = x.SectionCode,
                         PaymentDate = x.PaymentDate,
                         AmountPaid = x.AmountPaid,
                         TaxableAmount = x.TaxableAmount,
                         Tdsrate = x.Tdsrate,
                         Tdsamount = x.Tdsamount,
                         Surcharge = x.Surcharge,
                         Cess = x.Cess,
                         TotalTds = x.TotalTds,
                         HigherRateApplied = x.HigherRateApplied,
                         HigherRateReason = x.HigherRateReason,
                         IsMappedToReturn = x.IsMappedToReturn,
                         CreatedAt = x.CreatedAt
                     }).ToList();


                return new ManagerBaseResponse<List<TdsentryResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsentryResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdsentry>>> GetTDSEntryFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsentryRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.SectionCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EntryId);

                PageListed<Tdsentry> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdsentry>>
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

                return new ManagerBaseResponse<IEnumerable<Tdsentry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSChallanData(TdschallanRequestModel TDSChallan, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdschallan>>();

            try
            {
                var Deductor = _UnitOfWork.TdsdeductorRespositories.GetQueryable().FirstOrDefault(x => x.DeductorId == TDSChallan.DeductorId);
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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    if (TDSChallan.ChallanId == 0)
                    {
                        // Insert
                        Tdschallan _model = new Tdschallan();
                        _model.DeductorId = Deductor.DeductorId;
                        _model.Bsrcode = TDSChallan.Bsrcode;
                        _model.ChallanDate = TDSChallan.ChallanDate;
                        _model.ChallanSerialNo = TDSChallan.ChallanSerialNo;
                        _model.SectionCode = TDSChallan.SectionCode;
                        _model.TaxAmount = TDSChallan.TaxAmount;
                        _model.InterestAmount = TDSChallan.InterestAmount;
                        _model.LateFeeAmount = TDSChallan.LateFeeAmount;
                        _model.OtherAmount = TDSChallan.OtherAmount;
                        _model.TotalAmount = TDSChallan.TaxAmount + TDSChallan.InterestAmount + TDSChallan.LateFeeAmount + TDSChallan.OtherAmount;
                        _model.MatchedWithOltas = TDSChallan.MatchedWithOltas;
                        _model.IsMappedToReturn = TDSChallan.IsMappedToReturn;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                      
                        await  _UnitOfWork.TdschallanRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.TdschallanRespositories.GetQueryable()
                            .Where(x => x.ChallanId == TDSChallan.ChallanId)
                            .FirstOrDefault();
                        originalTerm.DeductorId = Deductor.DeductorId;
                        originalTerm.Bsrcode = TDSChallan.Bsrcode;
                        originalTerm.ChallanDate = TDSChallan.ChallanDate;
                        originalTerm.ChallanSerialNo = TDSChallan.ChallanSerialNo;
                        originalTerm.SectionCode = TDSChallan.SectionCode;
                        originalTerm.TaxAmount = TDSChallan.TaxAmount;
                        originalTerm.InterestAmount = TDSChallan.InterestAmount;
                        originalTerm.LateFeeAmount = TDSChallan.LateFeeAmount;
                        originalTerm.OtherAmount = TDSChallan.OtherAmount;
                        originalTerm.TotalAmount = TDSChallan.TaxAmount + TDSChallan.InterestAmount + TDSChallan.LateFeeAmount + TDSChallan.OtherAmount;
                        originalTerm.MatchedWithOltas = TDSChallan.MatchedWithOltas;
                        originalTerm.IsMappedToReturn = TDSChallan.IsMappedToReturn;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                         
                      
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdschallanResponseModel>>> GetAllTDSChallanData(string ChallanId)
        {
            try
            {
                var plans =_UnitOfWork.TdschallanRespositories.GetQueryable().Where(x => x.ChallanId.ToString() == ChallanId)
                    .Select(x => new TdschallanResponseModel
                    {
                        ChallanId = x.ChallanId,
                        DeductorId = x.DeductorId,
                        Bsrcode = x.Bsrcode,
                        ChallanDate = x.ChallanDate,
                        ChallanSerialNo = x.ChallanSerialNo,
                        SectionCode = x.SectionCode,
                        TaxAmount = x.TaxAmount,
                        InterestAmount = x.InterestAmount,
                        LateFeeAmount = x.LateFeeAmount,
                        OtherAmount = x.OtherAmount,
                        TotalAmount = x.TotalAmount,
                        MatchedWithOltas = x.MatchedWithOltas,
                        IsMappedToReturn = x.IsMappedToReturn,
                        CreatedAt = x.CreatedAt
                    }).ToList();


                return new ManagerBaseResponse<List<TdschallanResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Challan Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdschallanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdschallan>>> GetTDSChallanFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdschallanRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Bsrcode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ChallanId);

                PageListed<Tdschallan> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdschallan>>
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

                return new ManagerBaseResponse<IEnumerable<Tdschallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnChallanData(TdsreturnChallanRequsetModel TDSReturnChallan, string UserName)
        {
            var response = new ManagerBaseResponse<List<TdsreturnChallan>>();

            try
            {
                var Return = _UnitOfWork.TDSReturnRespositories.GetQueryable().FirstOrDefault(x => x.ReturnId == TDSReturnChallan.ReturnId);
                var Challan = _UnitOfWork.TdschallanRespositories.GetQueryable().FirstOrDefault(x => x.ChallanId == TDSReturnChallan.ChallanId);

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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    var data = _UnitOfWork.TdsreturnChallanRespositories.GetQueryable().FirstOrDefault(x => x.ChallanId == TDSReturnChallan.ChallanId && x.ReturnId == TDSReturnChallan.ReturnId);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Return Id and Challan Id are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSReturnChallan.ReturnChallanId == 0)
                        {
                            // Insert
                            TdsreturnChallan _model = new TdsreturnChallan();
                            _model.ReturnId = TDSReturnChallan.ReturnId;
                            _model.ChallanId = TDSReturnChallan.ChallanId;

                            
                            await _UnitOfWork.TdsreturnChallanRespositories.AddAsync(_model);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.TdsreturnChallanRespositories.GetQueryable()
                                .Where(x => x.ReturnChallanId == TDSReturnChallan.ReturnChallanId)
                                .FirstOrDefault();
                            originalTerm.ReturnId = TDSReturnChallan.ReturnId;
                            originalTerm.ChallanId = TDSReturnChallan.ChallanId;
                             
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsreturnChallanResponseModel>>> GetAllTDSReturnChallanData(string TDSReturnChallanId)
        {
            try
            {
                var plans = _UnitOfWork.TdsreturnChallanRespositories.GetQueryable().Where(x => x.ReturnChallanId.ToString() == TDSReturnChallanId)
                      .Select(x => new TdsreturnChallanResponseModel
                      {
                          ReturnChallanId = x.ReturnChallanId,
                          ReturnId = x.ReturnId,
                          ChallanId = x.ChallanId
                      }).ToList();


                return new ManagerBaseResponse<List<TdsreturnChallanResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return and Challan Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsreturnChallanResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TdsreturnChallan>>> GetTDSReturnChallanFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsreturnChallanRespositories.GetQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.ReturnChallanId);

                PageListed<TdsreturnChallan> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TdsreturnChallan>>
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

                return new ManagerBaseResponse<IEnumerable<TdsreturnChallan>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSReturnEntryData(TdsreturnEntryRequestModel TDSReturnEntry, string UserName)
        {
            var response = new ManagerBaseResponse<List<TdsreturnEntry>>();

            try
            {
                var Return = _UnitOfWork.TDSReturnRespositories.GetQueryable().FirstOrDefault(x => x.ReturnId == TDSReturnEntry.ReturnId);
                var Entry = _UnitOfWork.TdsentryRespositories.GetQueryable().FirstOrDefault(x => x.EntryId == TDSReturnEntry.EntryId);

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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    var data = _UnitOfWork.TdsreturnEntryRespositories.GetQueryable().FirstOrDefault(x => x.ReturnId == TDSReturnEntry.ReturnId && x.EntryId == TDSReturnEntry.EntryId);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Return Id and Entry Id are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSReturnEntry.ReturnEntryId == 0)
                        {
                            // Insert
                            TdsreturnEntry _model = new TdsreturnEntry();
                            _model.ReturnId = TDSReturnEntry.ReturnId;
                            _model.EntryId = TDSReturnEntry.EntryId;

                            await _UnitOfWork.TdsreturnEntryRespositories.AddAsync(_model);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.TdsreturnEntryRespositories.GetQueryable()
                                .Where(x => x.ReturnEntryId == TDSReturnEntry.ReturnEntryId)
                                .FirstOrDefault();
                            originalTerm.ReturnId = TDSReturnEntry.ReturnId;
                            originalTerm.EntryId = TDSReturnEntry.EntryId;
                            
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsreturnEntryResponseModel>>> GetAllTDSReturnEntryData(string TDSReturnEntryId)
        {
            try
            {
                var plans =_UnitOfWork.TdsreturnEntryRespositories.GetQueryable().Where(x => x.ReturnEntryId.ToString() == TDSReturnEntryId)
                      .Select(x => new TdsreturnEntryResponseModel
                      {
                          ReturnEntryId = x.ReturnEntryId,
                          ReturnId = x.ReturnId,
                          EntryId = x.EntryId
                      }).ToList();


                return new ManagerBaseResponse<List<TdsreturnEntryResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Return and Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsreturnEntryResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TdsreturnEntry>>> GetTDSReturnEntryFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsreturnEntryRespositories.GetQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.ReturnEntryId);

                PageListed<TdsreturnEntry> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TdsreturnEntry>>
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

                return new ManagerBaseResponse<IEnumerable<TdsreturnEntry>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSChallanAllocationData(TdschallanAllocationRequestModel TDSChallanAllocation, string UserName)
        {
            var response = new ManagerBaseResponse<List<TdschallanAllocation>>();

            try
            {
                var Challan =_UnitOfWork.TdschallanRespositories.GetQueryable().FirstOrDefault(x => x.ChallanId == TDSChallanAllocation.ChallanId);
                var Entry = _UnitOfWork.TdsentryRespositories.GetQueryable().FirstOrDefault(x => x.EntryId == TDSChallanAllocation.EntryId);

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
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    var data = _UnitOfWork.TdschallanAllocationRespostories.GetQueryable().FirstOrDefault(x => x.EntryId == TDSChallanAllocation.EntryId);
                    if (data != null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Entry Id are already Exits."
                        };
                    }
                    else
                    {


                        if (TDSChallanAllocation.AllocationId == 0)
                        {
                            // Insert
                            TdschallanAllocation _model = new TdschallanAllocation();
                            _model.ChallanId = TDSChallanAllocation.ChallanId;
                            _model.EntryId = TDSChallanAllocation.EntryId;
                            _model.AllocatedTdsamount = TDSChallanAllocation.AllocatedTdsamount;
                            _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            _model.CreatedBy = user.Id;


                            await _UnitOfWork.TdschallanAllocationRespostories.AddAsync(_model);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.TdschallanAllocationRespostories.GetQueryable()
                                .Where(x => x.AllocationId == TDSChallanAllocation.AllocationId)
                                .FirstOrDefault();
                            originalTerm.ChallanId = TDSChallanAllocation.ChallanId;
                            originalTerm.EntryId = TDSChallanAllocation.EntryId;
                            originalTerm.AllocatedTdsamount = TDSChallanAllocation.AllocatedTdsamount;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdschallanAllocationResponseModel>>> GetAllTDSChallanAllocationData(string TDSChallanAllocatinoId)
        {
            try
            {
                var plans = _UnitOfWork.TdschallanAllocationRespostories.GetQueryable().Where(x => x.AllocationId.ToString() == TDSChallanAllocatinoId)
                      .Select(x => new TdschallanAllocationResponseModel
                      {
                          AllocationId = x.AllocationId,
                          ChallanId = x.ChallanId,
                          EntryId = x.EntryId,
                          AllocatedTdsamount = x.AllocatedTdsamount,
                          CreatedAt = x.CreatedAt,
                          CreatedBy = x.CreatedBy
                      }).ToList();


                return new ManagerBaseResponse<List<TdschallanAllocationResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Challan and Entry Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdschallanAllocationResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TdschallanAllocation>>> GetTDSChallanAllocationFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdschallanAllocationRespostories.GetQueryable();
                //var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    query = query.Where(x => x..ToLower().Contains(searchText.ToLower()));
                //}

                query = query.OrderBy(a => a.AllocationId);

                PageListed<TdschallanAllocation> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TdschallanAllocation>>
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

                return new ManagerBaseResponse<IEnumerable<TdschallanAllocation>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveTDSRatesData(TdsrateRequestModel TDSRates, string UserName)
        {
            var response = new ManagerBaseResponse<List<Tdsrate>>();

            try
            {
                
                    var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                    
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


                        if (TDSRates.TaxId == 0)
                        {
                            // Insert
                            Tdsrate _model = new Tdsrate();
                            _model.TaxName = TDSRates.TaxName;
                        _model.Rate = TDSRates.Rate;
                        _model.TaxType = TDSRates.TaxType;
                        _model.IsActive = TDSRates.IsActive;
                            _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            _model.CreatedBy = user.Id;

                            
                            await _UnitOfWork.TdsrateRespostories.AddAsync(_model);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.TdsrateRespostories.GetQueryable()
                                .Where(x => x.TaxId == TDSRates.TaxId)
                                .FirstOrDefault();
                        originalTerm.TaxName = TDSRates.TaxName;
                        originalTerm.Rate = TDSRates.Rate;
                        originalTerm.TaxType = TDSRates.TaxType;
                        originalTerm.IsActive = TDSRates.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.UpdatedBy = user.Id;
                        
                    }
                    }
                    await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<TdsrateResponseModel>>> GetAllTDSRatesData(string TaxId)
        {
            try
            {
                var plans = _UnitOfWork.TdsrateRespostories.GetQueryable().Where(x => x.TaxId.ToString() == TaxId).Select(x => new TdsrateResponseModel
                {
                    TaxId = x.TaxId,
                    TaxName = x.TaxName,
                    Rate = x.Rate,
                    TaxType = x.TaxType,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                }).ToList();


                return new ManagerBaseResponse<List<TdsrateResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Rates Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TdsrateResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Tdsrate>>> GetTDSRatesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.TdsrateRespostories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.TaxName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.TaxId);

                PageListed<Tdsrate> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Tdsrate>>
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

                return new ManagerBaseResponse<IEnumerable<Tdsrate>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
