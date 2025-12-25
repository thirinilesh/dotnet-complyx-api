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

namespace ComplyX.BusinessLogic
{
    public class EPFOClass  : EPFOServices
    {
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

        Task<ManagerBaseResponse<bool>> EPFOServices.SaveCompanyEPFOData(CompanyEPFO CompanyEPFO)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.RemoveCompanyEPFOData(string CompanyEPFOId)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.SaveEmployeeEPFOData(EmployeeEPFO EmployeeEPFO)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.RemoveEmployeeEPFOData(string EmployeeEPFOId)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.SaveEPFOECRData(EPFOECRFile EPFOECRFile)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.RemoveEPFOECRData(string ECRFileId)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.SaveEPFOPeriodData(EPFOPeriod EPFOPeriod, string UserID)
        {
            throw new NotImplementedException();
        }

        Task<ManagerBaseResponse<bool>> EPFOServices.RemoveEPFOPeriodData(string EPFOPeriodId)
        {
            throw new NotImplementedException();
        }
    }
}
