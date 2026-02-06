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
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.EmploymentTypes;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.LicenseKeyMaster;
using ComplyX_Businesss.Models.ExitTypes;
using ComplyX_Businesss.Models.FilingStatus;


namespace ComplyX_Businesss.Services.Implementation
{
    public class MasterClass  : MasterServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;
        private readonly IUnitOfWork _UnitOfWork;


        public MasterClass(AppContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _UnitOfWork = unitOfWork;
        }

        public async Task<ManagerBaseResponse<bool>> SaveEmploymentTypesData(EmploymentTypeRequestModel EmploymentTypes)
        {
            var response = new ManagerBaseResponse<List<EmploymentType>>();

            try
            {
                   if (EmploymentTypes.EmploymentTypeId == 0)
                    {
                    // Insert
                    EmploymentType _model = new EmploymentType();
                        _model.Name = EmploymentTypes.Name;

                       await _UnitOfWork.EmploymentTypeRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.EmploymentTypeRespositories.GetQueryable()
                            .Where(x => x.EmploymentTypeId == EmploymentTypes.EmploymentTypeId).FirstOrDefault();
                       originalTerm.Name = EmploymentTypes.Name;
                
                }

                   await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "EMployement Types Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEmploymentTypesData(string EmploymentTypeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _UnitOfWork.EmploymentTypeRespositories.GetQueryable().Where(x => x.EmploymentTypeId.ToString() == EmploymentTypeID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employement Type Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.EmploymentTypeRespositories.RemoveRange(Employee);
                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<EmploymentTypeResponseModel>>> GetEmploymentTypesData(string EmploymentTypeID)
        {
            try
            {
                var plans = await _UnitOfWork.EmploymentTypeRespositories.GetQueryable().Where(x => x.EmploymentTypeId.ToString() == EmploymentTypeID).Select(x => new EmploymentTypeResponseModel
                {
                    EmploymentTypeId = x.EmploymentTypeId,
                    Name = x.Name
                }).ToListAsync();

                return new ManagerBaseResponse<List<EmploymentTypeResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Employment Type Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<EmploymentTypeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<EmploymentType>>> GetEmploymentTypesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.EmploymentTypeRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmploymentTypeId);

                PageListed<EmploymentType> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<EmploymentType>>
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

                return new ManagerBaseResponse<IEnumerable<EmploymentType>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>>SaveExitTypesData(ExitTypeRequestModel ExitTypes)
        {
            var response = new ManagerBaseResponse<List<ExitType>>();

            try
            {
                if (ExitTypes.ExitTypeId == 0)
                {
                    // Insert
                    ExitType _model = new ExitType();
                    _model.Name = ExitTypes.Name;

                   await _UnitOfWork.ExitTypesRespositories.AddAsync(_model);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.ExitTypesRespositories.GetQueryable()
                        .Where(x => x.ExitTypeId == ExitTypes.ExitTypeId).FirstOrDefault();
                    originalTerm.Name = ExitTypes.Name;
 
                }

                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Exit Types Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveExitTypesData(string ExitTypesID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _UnitOfWork.ExitTypesRespositories.GetQueryable().Where(x => x.ExitTypeId.ToString() == ExitTypesID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Exit Type Id is not Vaild",
                    };
                }

                // Remove all related report details
               _UnitOfWork.ExitTypesRespositories.RemoveRange(Employee);
                await _UnitOfWork.CommitAsync();
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
        public async Task<ManagerBaseResponse<List<ExitTypeResponseModel>>> GetExitTypesData(string ExitTypesID)
        {
            try
            {
                var plans = await _UnitOfWork.ExitTypesRespositories.GetQueryable().Where(x => x.ExitTypeId.ToString() == ExitTypesID).Select(x => new ExitTypeResponseModel
                {
                    ExitTypeId = x.ExitTypeId,
                    Name = x.Name
                }).ToListAsync();

                return new ManagerBaseResponse<List<ExitTypeResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Exit Type Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<ExitTypeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<ExitType>>> GetExitTypesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ExitTypesRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ExitTypeId);

                PageListed<ExitType> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<ExitType>>
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

                return new ManagerBaseResponse<IEnumerable<ExitType>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveFillingStatusesData(FilingsStatusRequestModel FilingStatuses)
        {
            var response = new ManagerBaseResponse<List<FilingStatus>>();

            try
            {
                if (FilingStatuses.FilingStatusId == 0)
                {
                    // Insert
                    FilingStatus _model = new FilingStatus();
                    _model.Name = FilingStatuses.Name;

           await _UnitOfWork.FilingStatusesRespositories.AddAsync(_model);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.FilingStatusesRespositories.GetQueryable()
                        .Where(x => x.FilingStatusId == FilingStatuses.FilingStatusId).FirstOrDefault();
                    originalTerm.Name = FilingStatuses.Name;
                   
                }
                await _UnitOfWork.CommitAsync();

                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Filing Status Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveFillingStatusesData(string FilingStatusID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _UnitOfWork.FilingStatusesRespositories.GetQueryable().Where(x => x.FilingStatusId.ToString() == FilingStatusID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Filing Statuses Id is not Vaild",
                    };
                }

                // Remove all related report details
              _UnitOfWork.FilingStatusesRespositories.RemoveRange(Employee);

                await _UnitOfWork.CommitAsync();

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
        public async Task<ManagerBaseResponse<List<FilingsStatusResponseModel>>> GetFillingStatusesData(string FilingStatusesID)
        {
            try
            {
                var plans = await _UnitOfWork.FilingStatusesRespositories.GetQueryable().Where(x => x.FilingStatusId.ToString() == FilingStatusesID).Select(x => new FilingsStatusResponseModel
                {
                    FilingStatusId = x.FilingStatusId,
                    Name = x.Name
                }).ToListAsync();

                return new ManagerBaseResponse<List<FilingsStatusResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Filing Staus Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<FilingsStatusResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<FilingStatus>>> GetFillingStatusesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.FilingStatusesRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FilingStatusId);

                PageListed<FilingStatus> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<FilingStatus>>
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

                return new ManagerBaseResponse<IEnumerable<FilingStatus>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
