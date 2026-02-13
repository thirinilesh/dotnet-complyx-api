using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyX_Businesss.Services.Interface;
using System.Threading.Tasks;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using AutoMapper.Configuration.Annotations;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.ComplianceDeadline;
using ComplyX.Repositories.UnitOfWork;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.ComplianceSchedule;
using ComplyX_Businesss.Models.ComplianceFiling;
using ComplyX_Businesss.Models.CustomerPayments;

namespace ComplyX_Businesss.Services.Implementation
{
    public  class ComplianceMgmtCLass : ComplianceMgmtService
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;
        private readonly Nest.Filter _filter;
        private readonly IUnitOfWork _UnitOfWork;



        public ComplianceMgmtCLass(AppContext context, Nest.Filter filter, IUnitOfWork unitOfWork)
        {
            _context = context;
            _filter = filter;
            _UnitOfWork = unitOfWork;

        }
        public async Task<ManagerBaseResponse<bool>> SaveComplianceMgmtData(ComplianceDeadlineRequestModel ComplianceDeadlines)
        {
            var response = new ManagerBaseResponse<List<ComplianceDeadline>>();

            try
            {
                var company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == ComplianceDeadlines.CompanyId);

                   
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
                    if (ComplianceDeadlines.DeadlineId == 0)
                    {
                        // Insert
                        ComplianceDeadline _model = new ComplianceDeadline();
                        _model.CompanyId = ComplianceDeadlines.CompanyId;
                        _model.ComplianceType = ComplianceDeadlines.ComplianceType;
                        _model.PeriodStart = ComplianceDeadlines.PeriodStart;
                        _model.PeriodEnd = ComplianceDeadlines.PeriodEnd;
                        _model.DueDate = ComplianceDeadlines.DueDate;
                        _model.Status = ComplianceDeadlines.Status;
                        _model.AckNumber = ComplianceDeadlines.AckNumber;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();


                       await _UnitOfWork.ComplianceDeadlineRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.ComplianceDeadlineRespositories.GetQueryable()
                            .Where(x => x.DeadlineId == ComplianceDeadlines.DeadlineId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = ComplianceDeadlines.CompanyId;
                        originalTerm.ComplianceType = ComplianceDeadlines.ComplianceType;
                        originalTerm.PeriodStart = ComplianceDeadlines.PeriodStart;
                        originalTerm.PeriodEnd = ComplianceDeadlines.PeriodEnd;
                        originalTerm.DueDate = ComplianceDeadlines.DueDate;
                        originalTerm.Status = ComplianceDeadlines.Status;
                        originalTerm.AckNumber = ComplianceDeadlines.AckNumber;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();


                       
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var product = await _UnitOfWork.ComplianceDeadlineRespositories.GetQueryable().Where(x => x.DeadlineId.ToString() == DeadlineID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Deadline Id is not Vaild",
                    };
                }

                // Remove all related report details
                //_context.ComplianceDeadlines.RemoveRange(product);

                //await _context.SaveChangesAsync();
                _UnitOfWork.ComplianceDeadlineRespositories.RemoveRange(product);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<ComplianceDeadlineResponseModel>>> GetAllComplianceMgmtData(string DeadlineID)
        {
            try
            {
                var plans = await _UnitOfWork.ComplianceDeadlineRespositories.GetQueryable().Where(x => x.DeadlineId.ToString() == DeadlineID).Select(x => new ComplianceDeadlineResponseModel
                {
                    DeadlineId = x.DeadlineId,
                    CompanyId = x.CompanyId,
                    ComplianceType = x.ComplianceType,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    DueDate = x.DueDate,
                    Status = x.Status,
                    AckNumber = x.AckNumber,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync();

                if (plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<ComplianceDeadlineResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Compliance Deadlines Data not Retrieved.",
                    };
                }
                else
                {


                    return new ManagerBaseResponse<List<ComplianceDeadlineResponseModel>>
                    {
                        IsSuccess = true,
                        Result = plans,
                        Message = "Compliance Deadlines Data Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceDeadlineResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceDeadlineResponseModel>>> GetComplianceMgmtFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ComplianceDeadlineRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ComplianceType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DeadlineId);
                var responseQuery = query.Select(x => new ComplianceDeadlineResponseModel
                {
                    DeadlineId = x.DeadlineId,
                    CompanyId = x.CompanyId,
                    ComplianceType = x.ComplianceType,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    DueDate = x.DueDate,
                    Status = x.Status,
                    AckNumber = x.AckNumber,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });


                PageListed<ComplianceDeadlineResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceDeadlineResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<ComplianceDeadlineResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveComplianceSchedulesData(ComplianceScheduleRequestModel ComplianceSchedules)
        {
            var response = new ManagerBaseResponse<List<ComplianceSchedule>>();

            try
            {
                var company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == ComplianceSchedules.CompanyId);

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


                    if (ComplianceSchedules.ScheduleId == 0)
                    {
                        // Insert
                        ComplianceSchedule _model = new ComplianceSchedule();
                        _model.CompanyId = ComplianceSchedules.CompanyId;
                        _model.ComplianceType = ComplianceSchedules.ComplianceType;
                        _model.Frequency = ComplianceSchedules.Frequency;
                        _model.StateCode = ComplianceSchedules.StateCode;
                        _model.BaseDay = ComplianceSchedules.BaseDay;
                        _model.QuarterMonth = ComplianceSchedules.QuarterMonth;
                        _model.OffsetDays = ComplianceSchedules.OffsetDays;
                        _model.Active = ComplianceSchedules.Active;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.ComplianceScheduleRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.ComplianceScheduleRespositories.GetQueryable()
                            .Where(x => x.ScheduleId == ComplianceSchedules.ScheduleId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = ComplianceSchedules.CompanyId;
                        originalTerm.ComplianceType = ComplianceSchedules.ComplianceType;
                        originalTerm.Frequency = ComplianceSchedules.Frequency;
                        originalTerm.StateCode = ComplianceSchedules.StateCode;
                        originalTerm.BaseDay = ComplianceSchedules.BaseDay;
                        originalTerm.QuarterMonth = ComplianceSchedules.QuarterMonth;
                        originalTerm.OffsetDays = ComplianceSchedules.OffsetDays;
                        originalTerm.Active = ComplianceSchedules.Active;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                      
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var product = await _UnitOfWork.ComplianceScheduleRespositories.GetQueryable().Where(x => x.ScheduleId.ToString() == ScheduleID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Schedules Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.ComplianceScheduleRespositories.RemoveRange(product);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<ComplianceScheduleResponseModel>>> GetAllComplianceSchedulesData(string ScheduleID)
        {
            try
            {
                var plans = await _UnitOfWork.ComplianceScheduleRespositories.GetQueryable().Where(x => x.ScheduleId.ToString() == ScheduleID).Select(x => new ComplianceScheduleResponseModel
                {
                    ScheduleId = x.ScheduleId,
                    CompanyId = x.CompanyId,
                    ComplianceType = x.ComplianceType,
                    Frequency = x.Frequency,
                    StateCode = x.StateCode,
                    BaseDay = x.BaseDay,
                    QuarterMonth = x.QuarterMonth,
                    OffsetDays = x.OffsetDays,
                    Active = x.Active,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<ComplianceScheduleResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Compliance Schedules Data not Retrieved.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<ComplianceScheduleResponseModel>>
                    {
                        IsSuccess = true,
                        Result = plans,
                        Message = "Compliance Schedules Data Retrieved Successfully.",
                    };
                }
                
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceScheduleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceScheduleResponseModel>>> GetComplianceSchedulesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ComplianceScheduleRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ComplianceType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ScheduleId);
                var responseQuery = query.Select(x => new ComplianceScheduleResponseModel
                {
                    ScheduleId = x.ScheduleId,
                    CompanyId = x.CompanyId,
                    ComplianceType = x.ComplianceType,
                    Frequency = x.Frequency,
                    StateCode = x.StateCode,
                    BaseDay = x.BaseDay,
                    QuarterMonth = x.QuarterMonth,
                    OffsetDays = x.OffsetDays,
                    Active = x.Active,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });
                PageListed<ComplianceScheduleResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceScheduleResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<ComplianceScheduleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveComplianceFilingsData(ComplianceFilingRequestModel ComplianceFilings)
        {
            var response = new ManagerBaseResponse<List<ComplianceFiling>>();

            try
            {

                var employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.EmployeeId == ComplianceFilings.EmployeeId && x.CompanyId == ComplianceFilings.CompanyId);
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

          
                if (ComplianceFilings.FilingId == 0)
                {
                    // Insert
                    ComplianceFiling _model = new ComplianceFiling();
                     _model.CompanyId = ComplianceFilings.CompanyId;
                        _model.EmployeeId = ComplianceFilings.EmployeeId;
                    _model.Type = ComplianceFilings.Type;
                    _model.FilingMonth = ComplianceFilings.FilingMonth;
                    _model.FilePath = ComplianceFilings.FilePath;   
                    _model.Status = ComplianceFilings.Status;
                    _model.Errors = ComplianceFilings.Errors;
                    _model.SubmittedAt = ComplianceFilings.SubmittedAt;
                    _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                      await _UnitOfWork.ComplianceFilingRespositories.AddAsync(_model);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.ComplianceFilingRespositories.GetQueryable()
                        .Where(x => x.FilingId == ComplianceFilings.FilingId)
                        .FirstOrDefault();
                    originalTerm.CompanyId = ComplianceFilings.CompanyId;
                    originalTerm.EmployeeId = ComplianceFilings.EmployeeId;
                    originalTerm.Type = ComplianceFilings.Type;
                    originalTerm.FilingMonth = ComplianceFilings.FilingMonth;
                    originalTerm.FilePath = ComplianceFilings.FilePath;
                    originalTerm.Status = ComplianceFilings.Status;
                    originalTerm.Errors = ComplianceFilings.Errors;
                    originalTerm.SubmittedAt = ComplianceFilings.SubmittedAt;
                    originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();


                     
                }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var product = await _UnitOfWork.ComplianceFilingRespositories.GetQueryable().Where(x => x.FilingId.ToString() == FilingID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Compliance Filings Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.ComplianceFilingRespositories.RemoveRange(product);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<ComplianceFilingResponseModel>>> GetAllComplianceFilingsData(string FilingID)
        {
            try
            {
                var plans = await _UnitOfWork.ComplianceFilingRespositories.GetQueryable().Where(x => x.FilingId.ToString() == FilingID).Select(x => new ComplianceFilingResponseModel
                {
                    FilingId = x.FilingId,
                    CompanyId = x.CompanyId,
                    EmployeeId = x.EmployeeId,
                    Type = x.Type,
                    FilingMonth = x.FilingMonth,
                    FilePath = x.FilePath,
                    Status = x.Status,
                    Errors = x.Errors,
                    SubmittedAt = x.SubmittedAt,
                    CreatedAt = x.CreatedAt
                })
                    .ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<ComplianceFilingResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Compliance Filings Data not Retrieved.",
                    };
                }
                else 
                {

                    return new ManagerBaseResponse<List<ComplianceFilingResponseModel>>
                    {
                        IsSuccess = true,
                        Result = plans,
                        Message = "Compliance Filings Data Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ComplianceFilingResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ComplianceFilingResponseModel>>> GetComplianceFilingsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ComplianceFilingRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Type.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FilingId);
                var responseQuery = query.Select(x => new ComplianceFilingResponseModel
                {
                    FilingId = x.FilingId,
                    CompanyId = x.CompanyId,
                    EmployeeId = x.EmployeeId,
                    Type = x.Type,
                    FilingMonth = x.FilingMonth,
                    FilePath = x.FilePath,
                    Status = x.Status,
                    Errors = x.Errors,
                    SubmittedAt = x.SubmittedAt,
                    CreatedAt = x.CreatedAt
                });
                PageListed<ComplianceFilingResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ComplianceFilingResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<ComplianceFilingResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
