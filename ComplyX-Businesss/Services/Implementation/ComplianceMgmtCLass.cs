using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyX_Businesss.Services.Interface;
using System.Threading.Tasks;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Data;
using AutoMapper.Configuration.Annotations;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ComplyX_Businesss.Services.Implementation
{
    public  class ComplianceMgmtCLass : ComplianceMgmtService
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppDbContext _context;
        private readonly Nest.Filter _filter;


        public ComplianceMgmtCLass(AppDbContext context, Nest.Filter filter)
        {
            _context = context;
            _filter = filter;

        }
        public async Task<ManagerBaseResponse<bool>> SaveComplianceMgmtData(ComplianceDeadlines ComplianceDeadlines)
        {
            var response = new ManagerBaseResponse<List<ComplianceDeadlines>>();

            try
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == ComplianceDeadlines.CompanyID);

                   
                if (company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data is not found.",
                    };
                }
                else
                {
                    if (ComplianceDeadlines.DeadlineID == 0)
                    {
                        // Insert
                        ComplianceDeadlines _model = new ComplianceDeadlines();
                        _model.CompanyID = ComplianceDeadlines.CompanyID;
                        _model.ComplianceType = ComplianceDeadlines.ComplianceType;
                        _model.PeriodStart = ComplianceDeadlines.PeriodStart;
                        _model.PeriodEnd = ComplianceDeadlines.PeriodEnd;
                        _model.DueDate = ComplianceDeadlines.DueDate;
                        _model.Status = ComplianceDeadlines.Status;
                        _model.AckNumber = ComplianceDeadlines.AckNumber;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();


                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.ComplianceDeadlines
                            .Where(x => x.DeadlineID == ComplianceDeadlines.DeadlineID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = ComplianceDeadlines.CompanyID;
                        originalTerm.ComplianceType = ComplianceDeadlines.ComplianceType;
                        originalTerm.PeriodStart = ComplianceDeadlines.PeriodStart;
                        originalTerm.PeriodEnd = ComplianceDeadlines.PeriodEnd;
                        originalTerm.DueDate = ComplianceDeadlines.DueDate;
                        originalTerm.Status = ComplianceDeadlines.Status;
                        originalTerm.AckNumber = ComplianceDeadlines.AckNumber;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();


                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Compliance Deadlines Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveComplianceMgmtData(string DeadlineID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.ComplianceDeadlines.Where(x => x.DeadlineID.ToString() == DeadlineID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Deadline Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.ComplianceDeadlines.RemoveRange(product);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Compliance Deadlines Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<ComplianceDeadlines>>> GetAllComplianceMgmtData(string DeadlineID)
        {
            try
            {
                var plans = await _context.ComplianceDeadlines.Where(x => x.DeadlineID.ToString() == DeadlineID).ToListAsync();

                return new ManagerBaseResponse<List<ComplianceDeadlines>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Compliance Deadlines Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceDeadlines>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceDeadlines>>> GetComplianceMgmtFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context. ComplianceDeadlines.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ComplianceType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeadlineID);

                PageListed<ComplianceDeadlines> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceDeadlines>>
                {
                    Result = result.Data,
                    Message = "Compliance Deadlines Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<ComplianceDeadlines>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveComplianceSchedulesData(ComplianceSchedules ComplianceSchedules)
        {
            var response = new ManagerBaseResponse<List<ComplianceSchedules>>();

            try
            {
                var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == ComplianceSchedules.CompanyID);

                if (company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data is not found.",
                    };
                }
                else
                {


                    if (ComplianceSchedules.ScheduleID == 0)
                    {
                        // Insert
                        ComplianceSchedules _model = new ComplianceSchedules();
                        _model.CompanyID = ComplianceSchedules.CompanyID;
                        _model.ComplianceType = ComplianceSchedules.ComplianceType;
                        _model.Frequency = ComplianceSchedules.Frequency;
                        _model.StateCode = ComplianceSchedules.StateCode;
                        _model.BaseDay = ComplianceSchedules.BaseDay;
                        _model.QuarterMonth = ComplianceSchedules.QuarterMonth;
                        _model.OffsetDays = ComplianceSchedules.OffsetDays;
                        _model.Active = ComplianceSchedules.Active;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.ComplianceSchedules
                            .Where(x => x.ScheduleID == ComplianceSchedules.ScheduleID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = ComplianceSchedules.CompanyID;
                        originalTerm.ComplianceType = ComplianceSchedules.ComplianceType;
                        originalTerm.Frequency = ComplianceSchedules.Frequency;
                        originalTerm.StateCode = ComplianceSchedules.StateCode;
                        originalTerm.BaseDay = ComplianceSchedules.BaseDay;
                        originalTerm.QuarterMonth = ComplianceSchedules.QuarterMonth;
                        originalTerm.OffsetDays = ComplianceSchedules.OffsetDays;
                        originalTerm.Active = ComplianceSchedules.Active;
                        originalTerm.updatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Compliance Schedules Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveComplianceSchedulesData(string ScheduleID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.ComplianceSchedules.Where(x => x.ScheduleID.ToString() == ScheduleID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Schedules Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.ComplianceSchedules.RemoveRange(product);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Compliance Schedules Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<ComplianceSchedules>>> GetAllComplianceSchedulesData(string ScheduleID)
        {
            try
            {
                var plans = await _context.ComplianceSchedules.Where(x => x.ScheduleID.ToString() == ScheduleID).ToListAsync();

                return new ManagerBaseResponse<List<ComplianceSchedules>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Compliance Schedules Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceSchedules>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceSchedules>>> GetComplianceSchedulesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.ComplianceSchedules.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ComplianceType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ScheduleID);

                PageListed<ComplianceSchedules> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceSchedules>>
                {
                    Result = result.Data,
                    Message = "Compliance Schedules Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<ComplianceSchedules>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveComplianceFilingsData(ComplianceFilings ComplianceFilings)
        {
            var response = new ManagerBaseResponse<List<ComplianceFilings>>();

            try
            {

                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == ComplianceFilings.EmployeeID && x.CompanyID == ComplianceFilings.CompanyID);
                if (employee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee and Company Data is not found.",
                    };
                }
                else
                {

          
                if (ComplianceFilings.FilingID == 0)
                {
                    // Insert
                    ComplianceFilings _model = new ComplianceFilings();
                     _model.CompanyID = ComplianceFilings.CompanyID;
                    _model.EmployeeID = ComplianceFilings.EmployeeID;
                    _model.Type = ComplianceFilings.Type;
                    _model.FilingMonth = ComplianceFilings.FilingMonth;
                    _model.FilePath = ComplianceFilings.FilePath;   
                    _model.Status = ComplianceFilings.Status;
                    _model.Errors = ComplianceFilings.Errors;
                    _model.SubmittedAt = ComplianceFilings.SubmittedAt;
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.ComplianceFilings
                        .Where(x => x.FilingID == ComplianceFilings.FilingID)
                        .FirstOrDefault();
                    originalTerm.CompanyID = ComplianceFilings.CompanyID;
                    originalTerm.EmployeeID = ComplianceFilings.EmployeeID;
                    originalTerm.Type = ComplianceFilings.Type;
                    originalTerm.FilingMonth = ComplianceFilings.FilingMonth;
                    originalTerm.FilePath = ComplianceFilings.FilePath;
                    originalTerm.Status = ComplianceFilings.Status;
                    originalTerm.Errors = ComplianceFilings.Errors;
                    originalTerm.SubmittedAt = ComplianceFilings.SubmittedAt;
                    originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();


                        _context.Update(originalTerm);
                    _context.SaveChanges();
                }
                }

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Compliance Filings Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveComplianceFilingsData(string FilingID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.ComplianceFilings.Where(x => x.FilingID.ToString() == FilingID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Filings Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.ComplianceFilings.RemoveRange(product);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Complaine Filings Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<ComplianceFilings>>> GetAllComplianceFilingsData(string FilingID)
        {
            try
            {
                var plans = await _context.ComplianceFilings.Where(x => x.FilingID.ToString() == FilingID).ToListAsync();

                return new ManagerBaseResponse<List<ComplianceFilings>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Compliance Filings Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceFilings>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceFilings>>> GetComplianceFilingsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.ComplianceFilings.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Type.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FilingID);

                PageListed<ComplianceFilings> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceFilings>>
                {
                    Result = result.Data,
                    Message = "Compliance Filings Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<ComplianceFilings>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
