using ComplyX;
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Logging;
using Nest;
using ComplyX_Businesss.Helper;
using System;
using X.PagedList;
using AppContext = ComplyX_Businesss.Helper.AppContext;


namespace ComplyX_Businesss.Services.Implementation
{
    public class MasterClass  : MasterServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;

        public MasterClass(AppContext context)
        {
            _context = context;
        }

        public Task<ManagerBaseResponse<bool>> SaveEmploymentTypesData(EmploymentTypes EmploymentTypes)
        {
            var response = new ManagerBaseResponse<List<EmploymentTypes>>();

            try
            {
                   if (EmploymentTypes.EmploymentTypeID == 0)
                    {
                    // Insert
                    EmploymentTypes _model = new EmploymentTypes();
                        _model.Name = EmploymentTypes.Name;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.EmploymentTypes
                            .Where(x => x.EmploymentTypeID == EmploymentTypes.EmploymentTypeID).FirstOrDefault();
                       originalTerm.Name = EmploymentTypes.Name;
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }


                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EMployement Types Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEmploymentTypesData(string EmploymentTypeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _context.EmploymentTypes.Where(x => x.EmploymentTypeID.ToString() == EmploymentTypeID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employement Type Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.EmploymentTypes.RemoveRange(Employee);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Employement Type Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<EmploymentTypes>>> GetEmploymentTypesData(string EmploymentTypeID)
        {
            try
            {
                var plans = await _context.EmploymentTypes.Where(x => x.EmploymentTypeID.ToString() == EmploymentTypeID).ToListAsync();

                return new ManagerBaseResponse<List<EmploymentTypes>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Employment Type Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<EmploymentTypes>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<EmploymentTypes>>> GetEmploymentTypesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.EmploymentTypes.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmploymentTypeID);

                PageListed<EmploymentTypes> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EmploymentTypes>>
                {
                    Result = result.Data,
                    Message = "Employment Type Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<EmploymentTypes>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>>SaveExitTypesData(ExitTypes ExitTypes)
        {
            var response = new ManagerBaseResponse<List<ExitTypes>>();

            try
            {
                if (ExitTypes.ExitTypeID == 0)
                {
                    // Insert
                    ExitTypes _model = new ExitTypes();
                    _model.Name = ExitTypes.Name;

                    _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.ExitTypes
                        .Where(x => x.ExitTypeID == ExitTypes.ExitTypeID).FirstOrDefault();
                    originalTerm.Name = ExitTypes.Name;
                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }


                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Exit Types Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveExitTypesData(string ExitTypesID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _context.ExitTypes.Where(x => x.ExitTypeID.ToString() == ExitTypesID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Exit Type Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.ExitTypes.RemoveRange(Employee);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Exit Type Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<ExitTypes>>> GetExitTypesData(string ExitTypesID)
        {
            try
            {
                var plans = await _context.ExitTypes.Where(x => x.ExitTypeID.ToString() == ExitTypesID).ToListAsync();

                return new ManagerBaseResponse<List<ExitTypes>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Exit Type Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ExitTypes>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ExitTypes>>> GetExitTypesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.ExitTypes.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ExitTypeID);

                PageListed<ExitTypes> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ExitTypes>>
                {
                    Result = result.Data,
                    Message = "Exit Type Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<ExitTypes>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public Task<ManagerBaseResponse<bool>> SaveFillingStatusesData(FilingStatuses FilingStatuses)
        {
            var response = new ManagerBaseResponse<List<FilingStatuses>>();

            try
            {
                if (FilingStatuses.FilingStatusID == 0)
                {
                    // Insert
                    FilingStatuses _model = new FilingStatuses();
                    _model.Name = FilingStatuses.Name;

                    _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.FilingStatuses
                        .Where(x => x.FilingStatusID == FilingStatuses.FilingStatusID).FirstOrDefault();
                    originalTerm.Name = FilingStatuses.Name;
                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }


                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Filing Status Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveFillingStatusesData(string FilingStatusID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _context.FilingStatuses.Where(x => x.FilingStatusID.ToString() == FilingStatusID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Filing Statuses Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.FilingStatuses.RemoveRange(Employee);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Filing Statuses Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<FilingStatuses>>> GetFillingStatusesData(string FilingStatusesID)
        {
            try
            {
                var plans = await _context.FilingStatuses.Where(x => x.FilingStatusID.ToString() == FilingStatusesID).ToListAsync();

                return new ManagerBaseResponse<List<FilingStatuses>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Filing Staus Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<FilingStatuses>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<FilingStatuses>>> GetFillingStatusesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.FilingStatuses.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FilingStatusID);

                PageListed<FilingStatuses> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<FilingStatuses>>
                {
                    Result = result.Data,
                    Message = "FilingStatuses Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<FilingStatuses>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
