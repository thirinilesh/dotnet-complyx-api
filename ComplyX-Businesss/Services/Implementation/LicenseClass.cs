using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ComplyX.Shared.Helper;
 
using System.Linq;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using ComplyX_Businesss.Helper;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.LicenseKeyMaster;
using ComplyX.Data.Entities;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.LicenseActivation;
using ComplyX_Businesss.Models.LicenseAuditLog;
using ComplyX_Businesss.Models.MachineBinding;

namespace ComplyX.BusinessLogic
{
    public class LicenseClass : LicenseServices
    {

        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppContext _context;
        private readonly IUnitOfWork _UnitOfWork;

        public LicenseClass(AppContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
           _UnitOfWork = unitOfWork;
        }
        public async Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMasterRequestModel LicenseKeyMaster)
        {
            var response = new ManagerBaseResponse<List<LicenseKeyMaster>>();

            try
            {
                var ProductOwner = _UnitOfWork.ProductOwnerRepositories.GetQueryable().Where(x => x.ProductOwnerId == LicenseKeyMaster.ProductOwnerId).FirstOrDefault();
                if (ProductOwner == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "ProductOwner Data not found."
                    };
                }
                else
                {
                    if (LicenseKeyMaster.LicenseId == 0)
                    {
                        // Insert
                        LicenseKeyMaster _model = new LicenseKeyMaster();
                        _model.ProductOwnerId = ProductOwner.ProductOwnerId;
                        _model.LicenseKey = LicenseKeyMaster.LicenseKey;
                        _model.PlanType = LicenseKeyMaster.PlanType;
                        _model.MaxActivations = LicenseKeyMaster.MaxActivations;
                        _model.StartDate = LicenseKeyMaster.StartDate;
                        _model.EndDate = LicenseKeyMaster.EndDate;
                        _model.IsActive = LicenseKeyMaster.IsActive;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();                     

                       await _UnitOfWork.LegalKeyMasterRepositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.LegalKeyMasterRepositories.GetQueryable()
                            .Where(x => x.LicenseId == LicenseKeyMaster.LicenseId).FirstOrDefault();
                        originalTerm.IsActive = LicenseKeyMaster.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                       
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LicenseKeyMaster Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<IEnumerable<LicenseKeyMaster>>> GetLicenseKeyMasterFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LegalKeyMasterRepositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PlanType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.LicenseId);

                PageListed<LicenseKeyMaster> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LicenseKeyMaster>>
                {
                    Result = result.Data,
                    Message = "LicenseKey Master Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<LicenseKeyMaster>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivationRequestModel LicenseActivation)
        {
            var response = new ManagerBaseResponse<List<LicenseActivation>>();

            try
            {
                var License = _UnitOfWork.LicenseActivationRespositories.GetQueryable().Where(x => x.LicenseId == LicenseActivation.LicenseId).FirstOrDefault();
                if (License == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License Data not found."
                    };
                }
                else
                {
                    if (LicenseActivation.ActivationId == 0)
                    {
                        // Insert
                        LicenseActivation _model = new LicenseActivation();
                        _model.LicenseId = LicenseActivation.LicenseId;
                        _model.MachineHash = LicenseActivation.MachineHash;
                        _model.MachineName = LicenseActivation.MachineName;
                        _model.OsUser = LicenseActivation.OsUser;
                        _model.ActivatedAt = Util.GetCurrentCSTDateAndTime();
                        _model.LastVerifiedAt = Util.GetCurrentCSTDateAndTime();
                        _model.IsRevoked    = LicenseActivation.IsRevoked;
                        _model.GraceExpiryAt = Util.GetCurrentCSTDateAndTime();
                        _model.AppVersion = LicenseActivation.AppVersion;

                       await _UnitOfWork.LicenseActivationRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.LicenseActivationRespositories.GetQueryable()
                            .Where(x => x.ActivationId == LicenseActivation.ActivationId).FirstOrDefault();
                        originalTerm.LastVerifiedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.IsRevoked = LicenseActivation.IsRevoked;
                        originalTerm.GraceExpiryAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.AppVersion = LicenseActivation.AppVersion;


                       
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LicenseKey Activation Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<IEnumerable<LicenseActivation>>> GetLicenseActivationFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LicenseActivationRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.MachineName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ActivationId);

                PageListed<LicenseActivation> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LicenseActivation>>
                {
                    Result = result.Data,
                    Message = "License Activation Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<LicenseActivation>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogRequestModel LicenseAuditLogs)
        {
            var response = new ManagerBaseResponse<List<LicenseAuditLog>>();

            try
            {
                var LicenseActive = _UnitOfWork.LicenseActivationRespositories.GetQueryable().Where(x => x.ActivationId == LicenseAuditLogs.ActivationId && x.LicenseId == LicenseAuditLogs.LicenseId).FirstOrDefault();
               
                if (LicenseActive == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License not active."
                    };
                }
                else
                {
                    if (LicenseAuditLogs.AuditId == 0)
                    {
                        // Insert
                        LicenseAuditLog _model = new LicenseAuditLog();
                        _model.LicenseId = LicenseAuditLogs.LicenseId;
                        _model.ActivationId = LicenseAuditLogs.ActivationId;
                        _model.EventMessage = LicenseAuditLogs.EventMessage;
                        _model.EventType    = LicenseAuditLogs.EventType;
                        _model.LoggedAt = LicenseAuditLogs.LoggedAt;
                        _model.MachineHash = LicenseAuditLogs.MachineHash;
                        _model.Ipaddress = LicenseAuditLogs.Ipaddress;

                       await _UnitOfWork.LicenseAuditLogRespositories.AddAsync(_model);
                    }
                     
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "License Activation log Saved Successfully."
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
        public async Task<ManagerBaseResponse<IEnumerable<LicenseAuditLog>>> GetLicenseAuditLogsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LicenseAuditLogRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.EventType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.AuditId);

                PageListed<LicenseAuditLog> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LicenseAuditLog>>
                {
                    Result = result.Data,
                    Message = "License Activation Logs Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<LicenseAuditLog>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveMachineBindingData(MachineBindingRequestModel MachineBinding)
        {
            var response = new ManagerBaseResponse<List<MachineBinding>>();

            try
            {
                var LicenseActive = _UnitOfWork.LicenseActivationRespositories.GetQueryable().Where(x => x.ActivationId == MachineBinding.LicenseActivationId).FirstOrDefault();
                if (LicenseActive == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License Active Data not found."
                    };
                }
                else
                {
                    if (MachineBinding.MachineBindingId == 0)
                    {
                        // Insert
                        MachineBinding _model = new MachineBinding();
                         _model.MachineHash = MachineBinding.MachineHash;
                        _model.Cpuid = MachineBinding.Cpuid;
                        _model.MotherboardSerial = MachineBinding.MotherboardSerial;
                        _model.Macaddresses = MachineBinding.Macaddresses;
                        _model.WindowsSid = MachineBinding.WindowsSid;
                        _model.FirstSeenAt = MachineBinding.FirstSeenAt;
                        _model.LastSeenAt = MachineBinding.LastSeenAt;
                        _model.LicenseActivationId = MachineBinding.LicenseActivationId;

                       await _UnitOfWork.MachineBindingRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.MachineBindingRespositories.GetQueryable()
                            .Where(x => x.MachineBindingId == MachineBinding.MachineBindingId).FirstOrDefault();
                        originalTerm.MachineHash = MachineBinding.MachineHash;
                        originalTerm.Cpuid = MachineBinding.Cpuid;
                        originalTerm.MotherboardSerial = MachineBinding.MotherboardSerial ;
                        originalTerm.Macaddresses = MachineBinding.Macaddresses ;
                        originalTerm.WindowsSid = MachineBinding.WindowsSid ;
                        originalTerm.FirstSeenAt = MachineBinding.FirstSeenAt ;
                        originalTerm.LastSeenAt = MachineBinding.LastSeenAt ;
                        originalTerm .LicenseActivationId = MachineBinding.LicenseActivationId ;

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "MachineBindings Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<IEnumerable<MachineBinding>>> GetMachineBindingFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.MachineBindingRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.MachineHash.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.MachineBindingId);

                PageListed<MachineBinding> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<MachineBinding>>
                {
                    Result = result.Data,
                    Message = "MachineBinding Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<MachineBinding>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
