using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using ComplyX.Shared.Helper;
using ComplyX.Shared.Data;
using ComplyX_Businesss.Helper;

namespace ComplyX.BusinessLogic
{
    public class EPFOClass  : EPFOServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public EPFOClass(AppDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }
        public   Task<ManagerBaseResponse<bool>> SaveCompanyEPFOData(CompanyEPFO CompanyEPFO)
        {
            var response = new ManagerBaseResponse<List<CompanyEPFO>>();

            try
            {
                var companyid =  _context.Companies.Where(x => x.CompanyID == CompanyEPFO.CompanyId).FirstOrDefault();
                if (companyid == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    });
                }
                else
                {
                    if (CompanyEPFO.CompanyEPFOId == 0)
                    {
                        // Insert
                        CompanyEPFO _model = new CompanyEPFO();
                        _model.CompanyId = companyid.CompanyID;
                        _model.EstablishmentCode = CompanyEPFO.EstablishmentCode;
                        _model.Extension = CompanyEPFO.Extension;
                        _model.OfficeCode = CompanyEPFO.OfficeCode;
                        _model.CreatedAt= Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.CompanyEPFO
                            .Where(x => x.CompanyEPFOId == CompanyEPFO.CompanyEPFOId).FirstOrDefault();
                        originalTerm.CompanyId = companyid.CompanyID;
                        originalTerm.EstablishmentCode = CompanyEPFO.EstablishmentCode;
                        originalTerm.Extension = CompanyEPFO.Extension;
                        originalTerm.OfficeCode = CompanyEPFO.OfficeCode;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CompanyEPFO Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveCompanyEPFOData(string CompanyEPFOId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Company = await _context.CompanyEPFO.Where(x => x.CompanyEPFOId.ToString() == CompanyEPFOId).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.CompanyEPFO.RemoveRange(Company);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<CompanyEPFO>>> GetAllCompanyEPFOFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.CompanyEPFO.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.EstablishmentCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CompanyEPFOId);

                PageListed<CompanyEPFO> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<CompanyEPFO>>
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

                return new ManagerBaseResponse<IEnumerable<CompanyEPFO>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFO EmployeeEPFO)
        {
            var response = new ManagerBaseResponse<List<CompanyEPFO>>();

            try
            {
                var Employees = _context.Employees.Where(x => x.EmployeeID == EmployeeEPFO.EmployeeID).FirstOrDefault();
                if (Employees == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Data not found."
                    });
                }
                else
                {
                    if (EmployeeEPFO.EmployeeEPFOId == 0)
                    {
                        // Insert
                        EmployeeEPFO _model = new EmployeeEPFO();
                        _model.EmployeeID = Employees.EmployeeID;
                        _model.UAN = EmployeeEPFO.UAN;
                        _model.PFAccountNumber = EmployeeEPFO.PFAccountNumber;
                        _model.DOJ_EPF = EmployeeEPFO.DOJ_EPF;
                        _model.DOE_EPF = EmployeeEPFO.DOE_EPF;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.EmployeeEPFO
                            .Where(x => x.EmployeeEPFOId == EmployeeEPFO.EmployeeEPFOId).FirstOrDefault();
                        originalTerm.DOE_EPF = EmployeeEPFO.DOE_EPF;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EmployeeEPFO Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _context.EmployeeEPFO.Where(x => x.EmployeeEPFOId.ToString() == EmployeeEPFOId).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.EmployeeEPFO.RemoveRange(Employee);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<EmployeeEPFO>>> GetAllEmployeeEPFOFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.EmployeeEPFO.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PFAccountNumber.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmployeeEPFOId);

                PageListed<EmployeeEPFO> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EmployeeEPFO>>
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

                return new ManagerBaseResponse<IEnumerable<EmployeeEPFO>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFile EPFOECRFile)
        {
            var response = new ManagerBaseResponse<List<EPFOECRFile>>();

            try
            {
                var EPFOECR = _context.Companies.Where(x => x.CompanyID == EPFOECRFile.CompanyId).FirstOrDefault();
                if (EPFOECR == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "EPFOECR Company Data not found."
                    });
                }
                else
                {
                    if (EPFOECRFile.ECRFileId == 0)
                    {
                        // Insert
                        EPFOECRFile _model = new EPFOECRFile();
                        _model.CompanyId = EPFOECR.CompanyID;
                         _model.SubcontractorId = EPFOECRFile.SubcontractorId;
                        _model.WageMonth = EPFOECRFile.WageMonth;
                        _model.FileName = EPFOECRFile.FileName;
                        _model.TotalContribution = EPFOECRFile.TotalContribution;
                        _model.TotalWages = EPFOECRFile.TotalWages;
                        _model.TotalEmployees = EPFOECRFile.TotalEmployees;
                        _model.Status = EPFOECRFile.Status;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.EPFOECRFile
                            .Where(x => x.ECRFileId == EPFOECRFile.ECRFileId).FirstOrDefault();
                        originalTerm.CompanyId = EPFOECR.CompanyID;
                        originalTerm.SubcontractorId = EPFOECRFile.SubcontractorId;
                        originalTerm.WageMonth = EPFOECRFile.WageMonth;
                        originalTerm.FileName = EPFOECRFile.FileName;
                        originalTerm.TotalContribution = EPFOECRFile.TotalContribution;
                        originalTerm.TotalWages = EPFOECRFile.TotalWages;
                        originalTerm.TotalEmployees = EPFOECRFile.TotalEmployees;
                        originalTerm.Status = EPFOECRFile.Status;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFOECR File Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var EPFOECR = await _context.EPFOECRFile.Where(x => x.ECRFileId.ToString() == ECRFileId).ToListAsync();

                if (string.IsNullOrEmpty(EPFOECR.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.EPFOECRFile.RemoveRange(EPFOECR);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<EPFOECRFile>>> GetEPFOECRDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.EPFOECRFile.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FileName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ECRFileId);

                PageListed<EPFOECRFile> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EPFOECRFile>>
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

                return new ManagerBaseResponse<IEnumerable<EPFOECRFile>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriod EPFOPeriod , string UserID)
        {
            var response = new ManagerBaseResponse<List<EPFOPeriod>>();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == UserID);
                if (user == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "User Data not found."
                    });
                }
                var companyid = _context.Companies.FirstOrDefault(x => x.CompanyID == EPFOPeriod.CompanyID);
                if (companyid == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    });
                }
                if (EPFOPeriod.EPFOPeriodId == 0)
                    {
                        // Insert
                        EPFOPeriod _model = new EPFOPeriod();
                        _model.CompanyID = EPFOPeriod.CompanyID;
                        _model.SubcontractorId = EPFOPeriod.SubcontractorId;
                        _model.PeriodMonth = EPFOPeriod.PeriodMonth;
                        _model.PeriodYear = EPFOPeriod.PeriodYear;
                        _model.Status = EPFOPeriod.Status;
                        _model.ECRFilePath = EPFOPeriod.ECRFilePath;
                        _model.TRRN = EPFOPeriod.TRRN;
                        _model.TRRNDate = EPFOPeriod.TRRNDate;
                        _model.ChallanFilePath = EPFOPeriod.ChallanFilePath;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.CreatedByUserId = user.Id;
                        _model.IsLocked =   EPFOPeriod.IsLocked;      

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.EPFOPeriod
                            .Where(x => x.EPFOPeriodId== EPFOPeriod.EPFOPeriodId).FirstOrDefault();
 
                        originalTerm.Status = EPFOPeriod.Status;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.IsLocked = EPFOPeriod .IsLocked;
                        originalTerm.LockedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.LockedByUserId = user.Id;

                    _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                
                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EPFO Peroid Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var EPFOPeriod = await _context.EPFOPeriod.Where(x => x.EPFOPeriodId.ToString() == EPFOPeriodId).ToListAsync();

                if (string.IsNullOrEmpty(EPFOPeriod.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.EPFOPeriod.RemoveRange(EPFOPeriod);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<EPFOPeriod>>> GetEPFOPeriodDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.EPFOPeriod.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ECRFilePath.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EPFOPeriodId);

                PageListed<EPFOPeriod> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EPFOPeriod>>
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

                return new ManagerBaseResponse<IEnumerable<EPFOPeriod>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public Task<ManagerBaseResponse<bool>> SaveEPFOMonthlyWageData(EPFOMonthlyWage EPFOMonthlyWage)
        {
            var response = new ManagerBaseResponse<List<EPFOMonthlyWage>>();

            try
            {
                var companyid = _context.Employees.Where(x => x.CompanyID == EPFOMonthlyWage.CompanyId && x.EmployeeID == EPFOMonthlyWage.EmployeeId).FirstOrDefault();
                if (companyid == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    });
                }
                else
                {
                    if (EPFOMonthlyWage.WageId == 0)
                    {
                        // Insert
                        EPFOMonthlyWage _model = new EPFOMonthlyWage();
                        _model.CompanyId = companyid.CompanyID;
                        _model.EmployeeId = EPFOMonthlyWage.EmployeeId;
                        _model.SubcontractorId = EPFOMonthlyWage.SubcontractorId;
                        _model.WageMonth = EPFOMonthlyWage.WageMonth;
                        _model.Wages = EPFOMonthlyWage.Wages;
                        _model.EPSWages = EPFOMonthlyWage.EPSWages;
                        _model.EDLIWages = EPFOMonthlyWage.EDLIWages;
                        _model.EPFWages = EPFOMonthlyWage.EPFWages;
                        _model.Contribution = EPFOMonthlyWage.Contribution;
                        _model.EmployerShare = EPFOMonthlyWage.EmployerShare;
                        _model.PensionShare = EPFOMonthlyWage.PensionShare;
                        _model.NCPDays = EPFOMonthlyWage.NCPDays;
                        _model.RefundAdvance = EPFOMonthlyWage.RefundAdvance;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.EPFOMonthlyWage
                            .Where(x => x.WageId == EPFOMonthlyWage.WageId).FirstOrDefault();
                        originalTerm.CompanyId = companyid.CompanyID;
                        originalTerm.EmployeeId = EPFOMonthlyWage.EmployeeId;
                        originalTerm.SubcontractorId = EPFOMonthlyWage.SubcontractorId;
                        originalTerm.WageMonth = EPFOMonthlyWage.WageMonth;
                        originalTerm.Wages = EPFOMonthlyWage.Wages;
                        originalTerm.EPSWages = EPFOMonthlyWage.EPSWages;
                        originalTerm.EDLIWages = EPFOMonthlyWage.EDLIWages;
                        originalTerm.EPFWages = EPFOMonthlyWage.EPFWages;
                        originalTerm.Contribution = EPFOMonthlyWage.Contribution;
                        originalTerm.EmployerShare = EPFOMonthlyWage.EmployerShare;
                        originalTerm.PensionShare = EPFOMonthlyWage.PensionShare;
                        originalTerm.NCPDays = EPFOMonthlyWage.NCPDays;
                        originalTerm.RefundAdvance = EPFOMonthlyWage.RefundAdvance;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "CompanyEPFO Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEPFOMonthlyWageData(string WageID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Company = await _context.EPFOMonthlyWage.Where(x => x.WageId.ToString() == WageID).ToListAsync();

                if (string.IsNullOrEmpty(Company.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "EPFOMonthlyWage Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.EPFOMonthlyWage.RemoveRange(Company);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<EPFOMonthlyWage>>> GetAllEPFOMonthlyWageFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.EPFOMonthlyWage.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.WageMonth.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.WageId);

                PageListed<EPFOMonthlyWage> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EPFOMonthlyWage>>
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

                return new ManagerBaseResponse<IEnumerable<EPFOMonthlyWage>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
