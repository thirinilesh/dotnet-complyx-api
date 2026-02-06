using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using ComplyX.Shared.Data;
using ComplyX_Businesss.Helper;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.CompanyEPFO;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.EmployeeEPFO;
using ComplyX_Businesss.Models.EPFOECRFile;
using ComplyX_Businesss.Models.EPFOPeriod;
using ComplyX_Businesss.Models.EPFOMonthWage;

namespace ComplyX_Businesss.BusinessLogic
{
    public class EPFOClass  : EPFOServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };
        private readonly AppContext _context;
        private readonly UserManager<ApplicationUsers> _usermanager;
        private readonly IUnitOfWork _UnitOfWork;

        public EPFOClass(AppContext context, UserManager<ApplicationUsers> usermanager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _usermanager = usermanager;
            _UnitOfWork = unitOfWork;
        }
        public   async Task<ManagerBaseResponse<bool>> SaveCompanyEPFOData(CompanyEPFORequestModel CompanyEPFO)
        {
            var response = new ManagerBaseResponse<List<CompanyEpfo>>();

            try
            {
                var companyid =  _UnitOfWork.CompanyRepository.GetQueryable().Where(x => x.CompanyId == CompanyEPFO.CompanyId).FirstOrDefault();
                if (companyid == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    } ;
                }
                else
                {
                    if (CompanyEPFO.CompanyEpfoid == 0)
                    {
                        // Insert
                        CompanyEpfo _model = new CompanyEpfo();
                        _model.CompanyId = companyid.CompanyId;
                        _model.EstablishmentCode = CompanyEPFO.EstablishmentCode;
                        _model.Extension = CompanyEPFO.Extension;
                        _model.OfficeCode = CompanyEPFO.OfficeCode;
                        _model.CreatedAt= Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.CompanyEPFORespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.CompanyEPFORespositories.GetQueryable()
                            .Where(x => x.CompanyEpfoid == CompanyEPFO.CompanyEpfoid).FirstOrDefault();
                        originalTerm.CompanyId = companyid.CompanyId;
                        originalTerm.EstablishmentCode = CompanyEPFO.EstablishmentCode;
                        originalTerm.Extension = CompanyEPFO.Extension;
                        originalTerm.OfficeCode = CompanyEPFO.OfficeCode;

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CompanyEPFO Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveCompanyEPFOData(string CompanyEPFOId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Company = await _UnitOfWork.CompanyEPFORespositories.GetQueryable().Where(x => x.CompanyEpfoid.ToString() == CompanyEPFOId).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.CompanyEPFORespositories.RemoveRange(Company);
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CompanyEPFO Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<CompanyEpfo>>> GetAllCompanyEPFOFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.CompanyEPFORespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.EstablishmentCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyEpfoid);

                PageListed<CompanyEpfo> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<CompanyEpfo>>
                {
                    Result = result.Data,
                    Message = "CompanyEPFO Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<CompanyEpfo>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFORequestModel EmployeeEPFO)
        {
            var response = new ManagerBaseResponse<List<EmployeeEpfo>>();

            try
            {
                var Employees = _UnitOfWork.EmployeeRespositories.GetQueryable().Where(x => x.EmployeeId == EmployeeEPFO.EmployeeId).FirstOrDefault();
                if (Employees == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Data not found."
                    } ;
                }
                else
                {
                    if (EmployeeEPFO.EmployeeEpfoid == 0)
                    {
                        // Insert
                        EmployeeEpfo _model = new EmployeeEpfo();
                        _model.EmployeeId = Employees.EmployeeId;
                        _model.Uan = EmployeeEPFO.Uan;
                        _model.PfaccountNumber = EmployeeEPFO.PfaccountNumber;
                        _model.DojEpf = EmployeeEPFO.DojEpf;
                        _model.DoeEpf = EmployeeEPFO.DoeEpf;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await _UnitOfWork.EmployeeEPFORespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.EmployeeEPFORespositories.GetQueryable()
                            .Where(x => x.EmployeeEpfoid == EmployeeEPFO.EmployeeEpfoid).FirstOrDefault();
                        originalTerm.DoeEpf = EmployeeEPFO.DoeEpf;

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EmployeeEPFO Data Saved Successfully."
                } ;
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
        public async Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _UnitOfWork.EmployeeEPFORespositories.GetQueryable().Where(x => x.EmployeeEpfoid.ToString() == EmployeeEPFOId).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.EmployeeEPFORespositories.RemoveRange(Employee);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EmployeeEPFO Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<EmployeeEpfo>>> GetAllEmployeeEPFOFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.EmployeeEPFORespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PfaccountNumber.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmployeeEpfoid);

                PageListed<EmployeeEpfo> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EmployeeEpfo>>
                {
                    Result = result.Data,
                    Message = "EmployeeEPFO Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<EmployeeEpfo>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFileRequestModel EPFOECRFile)
        {
            var response = new ManagerBaseResponse<List<Epfoecrfile>>();

            try
            {
                var EPFOECR = _UnitOfWork.CompanyRepository.GetQueryable().Where(x => x.CompanyId == EPFOECRFile.CompanyId).FirstOrDefault();
                if (EPFOECR == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "EPFOECR Company Data not found."
                    } ;
                }
                else
                {
                    if (EPFOECRFile.EcrfileId == 0)
                    {
                        // Insert
                        Epfoecrfile _model = new Epfoecrfile();
                        _model.CompanyId = EPFOECR.CompanyId;
                         _model.SubcontractorId = EPFOECRFile.SubcontractorId;
                        _model.WageMonth = EPFOECRFile.WageMonth;
                        _model.FileName = EPFOECRFile.FileName;
                        _model.TotalContribution = EPFOECRFile.TotalContribution;
                        _model.TotalWages = EPFOECRFile.TotalWages;
                        _model.TotalEmployees = EPFOECRFile.TotalEmployees;
                        _model.Status = EPFOECRFile.Status;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.EPFOECRFileRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.EPFOECRFileRespositories.GetQueryable()
                            .Where(x => x.EcrfileId == EPFOECRFile.EcrfileId).FirstOrDefault();
                        originalTerm.CompanyId = EPFOECR.CompanyId;
                        originalTerm.SubcontractorId = EPFOECRFile.SubcontractorId;
                        originalTerm.WageMonth = EPFOECRFile.WageMonth;
                        originalTerm.FileName = EPFOECRFile.FileName;
                        originalTerm.TotalContribution = EPFOECRFile.TotalContribution;
                        originalTerm.TotalWages = EPFOECRFile.TotalWages;
                        originalTerm.TotalEmployees = EPFOECRFile.TotalEmployees;
                        originalTerm.Status = EPFOECRFile.Status;

                         
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFOECR File Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var EPFOECR = await _UnitOfWork.EPFOECRFileRespositories.GetQueryable().Where(x => x.EcrfileId.ToString() == ECRFileId).ToListAsync();

                if (string.IsNullOrEmpty(EPFOECR.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.EPFOECRFileRespositories.RemoveRange(EPFOECR);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFOECR File  Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<Epfoecrfile>>> GetEPFOECRDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.EPFOECRFileRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FileName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EcrfileId);

                PageListed<Epfoecrfile> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Epfoecrfile>>
                {
                    Result = result.Data,
                    Message = "EPFO ECRFile Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<Epfoecrfile>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriodRequestModel EPFOPeriod , string UserID)
        {
            var response = new ManagerBaseResponse<List<Epfoperiod>>();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == UserID);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User Data not found."
                    };
                }
                var companyid = _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefault(x => x.CompanyId == EPFOPeriod.CompanyId);
                if (companyid == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    };
                }
                if (EPFOPeriod.EpfoperiodId == 0)
                    {
                        // Insert
                        Epfoperiod _model = new Epfoperiod();
                        _model.CompanyId = EPFOPeriod.CompanyId;
                        _model.SubcontractorId = EPFOPeriod.SubcontractorId;
                        _model.PeriodMonth = EPFOPeriod.PeriodMonth;
                        _model.PeriodYear = EPFOPeriod.PeriodYear;
                        _model.Status = EPFOPeriod.Status;
                        _model.EcrfilePath = EPFOPeriod.EcrfilePath;
                        _model.Trrn = EPFOPeriod.Trrn;
                        _model.Trrndate = EPFOPeriod.Trrndate;
                        _model.ChallanFilePath = EPFOPeriod.ChallanFilePath;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CreatedByUserId = user.Id;
                        _model.IsLocked =   EPFOPeriod.IsLocked;      

                      await _UnitOfWork.ePFOPeriodRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.ePFOPeriodRespositories.GetQueryable()
                            .Where(x => x.EpfoperiodId== EPFOPeriod.EpfoperiodId).FirstOrDefault();
 
                        originalTerm.Status = EPFOPeriod.Status;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.IsLocked = EPFOPeriod .IsLocked;
                        originalTerm.LockedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.LockedByUserId = user.Id;

                     
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFO Peroid Data Saved Successfully."
                } ;
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var EPFOPeriod = await _UnitOfWork.ePFOPeriodRespositories.GetQueryable().Where(x => x.EpfoperiodId.ToString() == EPFOPeriodId).ToListAsync();

                if (string.IsNullOrEmpty(EPFOPeriod.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.ePFOPeriodRespositories.RemoveRange(EPFOPeriod);
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFO Period  Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<Epfoperiod>>> GetEPFOPeriodDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ePFOPeriodRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.EcrfilePath.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EpfoperiodId);

                PageListed<Epfoperiod> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Epfoperiod>>
                {
                    Result = result.Data,
                    Message = "EPFO Period Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<Epfoperiod>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveEPFOMonthlyWageData(EPFOMonthWageRequestModel EPFOMonthlyWage)
        {
            var response = new ManagerBaseResponse<List<EpfomonthlyWage>>();

            try
            {
                var companyid = _UnitOfWork.EmployeeRespositories.GetQueryable().Where(x => x.CompanyId == EPFOMonthlyWage.CompanyId && x.EmployeeId == EPFOMonthlyWage.EmployeeId).FirstOrDefault();
                if (companyid == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    } ;
                }
                else
                {
                    if (EPFOMonthlyWage.WageId == 0)
                    {
                        // Insert
                        EpfomonthlyWage _model = new EpfomonthlyWage();
                        _model.CompanyId = (int)companyid.CompanyId;
                        _model.EmployeeId = EPFOMonthlyWage.EmployeeId;
                        _model.SubcontractorId = EPFOMonthlyWage.SubcontractorId;
                        _model.WageMonth = EPFOMonthlyWage.WageMonth;
                        _model.Wages = EPFOMonthlyWage.Wages;
                        _model.Epswages = EPFOMonthlyWage.Epswages;
                        _model.Edliwages = EPFOMonthlyWage.Edliwages;
                        _model.Epfwages = EPFOMonthlyWage.Epfwages;
                        _model.Contribution = EPFOMonthlyWage.Contribution;
                        _model.EmployerShare = EPFOMonthlyWage.EmployerShare;
                        _model.PensionShare = EPFOMonthlyWage.PensionShare;
                        _model.Ncpdays = EPFOMonthlyWage.Ncpdays;
                        _model.RefundAdvance = EPFOMonthlyWage.RefundAdvance;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
await _UnitOfWork.ePFOMonthWageRespositories.AddAsync( _model );
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.ePFOMonthWageRespositories.GetQueryable()
                            .Where(x => x.WageId == EPFOMonthlyWage.WageId).FirstOrDefault();
                        originalTerm.CompanyId = (int)companyid.CompanyId;
                        originalTerm.EmployeeId = EPFOMonthlyWage.EmployeeId;
                        originalTerm.SubcontractorId = EPFOMonthlyWage.SubcontractorId;
                        originalTerm.WageMonth = EPFOMonthlyWage.WageMonth;
                        originalTerm.Wages = EPFOMonthlyWage.Wages;
                        originalTerm.Epswages = EPFOMonthlyWage.Epswages;
                        originalTerm.Edliwages = EPFOMonthlyWage.Edliwages;
                        originalTerm.Epswages = EPFOMonthlyWage.Epswages;
                        originalTerm.Contribution = EPFOMonthlyWage.Contribution;
                        originalTerm.EmployerShare = EPFOMonthlyWage.EmployerShare;
                        originalTerm.PensionShare = EPFOMonthlyWage.PensionShare;
                        originalTerm.Ncpdays = EPFOMonthlyWage.Ncpdays;
                        originalTerm.RefundAdvance = EPFOMonthlyWage.RefundAdvance;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        

                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CompanyEPFO Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOMonthlyWageData(string WageID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Company = await _UnitOfWork.ePFOMonthWageRespositories.GetQueryable().Where(x => x.WageId.ToString() == WageID).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "EPFOMonthlyWage Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.ePFOMonthWageRespositories.RemoveRange(Company);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFOMonthlyWage Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<EpfomonthlyWage>>> GetAllEPFOMonthlyWageFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ePFOMonthWageRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.WageMonth.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.WageId);

                PageListed<EpfomonthlyWage> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EpfomonthlyWage>>
                {
                    Result = result.Data,
                    Message = "EPFOMonthlyWage Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<EpfomonthlyWage>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
