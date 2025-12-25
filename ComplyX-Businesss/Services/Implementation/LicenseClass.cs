using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using ComplyX.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ComplyX.Shared.Helper;
 
using System.Linq;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using ComplyX_Businesss.Helper;

namespace ComplyX.BusinessLogic
{
    public class LicenseClass : LicenseServices
    {

        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppDbContext _context;

        public LicenseClass(AppDbContext context)
        {
            _context = context;
           
        }
        public Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMaster LicenseKeyMaster)
        {
            var response = new ManagerBaseResponse<List<LicenseKeyMaster>>();

            try
            {
                var ProductOwner = _context.ProductOwners.Where(x => x.ProductOwnerId == LicenseKeyMaster.ProductOwnerId).FirstOrDefault();
                if (ProductOwner == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "ProductOwner Data not found."
                    });
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

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.LicenseKeyMaster
                            .Where(x => x.LicenseId == LicenseKeyMaster.LicenseId).FirstOrDefault();
                        originalTerm.IsActive = LicenseKeyMaster.IsActive;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LicenseKeyMaster Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<IEnumerable<LicenseKeyMaster>>> GetLicenseKeyMasterFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LicenseKeyMaster.AsQueryable();
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


        public Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivation LicenseActivation)
        {
            var response = new ManagerBaseResponse<List<LicenseActivation>>();

            try
            {
                var License = _context.LicenseKeyMaster.Where(x => x.LicenseId == LicenseActivation.LicenseId).FirstOrDefault();
                if (License == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License Data not found."
                    });
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

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.LicenseActivation
                            .Where(x => x.ActivationId == LicenseActivation.ActivationId).FirstOrDefault();
                        originalTerm.LastVerifiedAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.IsRevoked = LicenseActivation.IsRevoked;
                        originalTerm.GraceExpiryAt = Util.GetCurrentCSTDateAndTime();
                        originalTerm.AppVersion = LicenseActivation.AppVersion;


                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LicenseKey Activation Data Saved Successfully."
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

        public async Task<ManagerBaseResponse<IEnumerable<LicenseActivation>>> GetLicenseActivationFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LicenseActivation.AsQueryable();
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


        public Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogs LicenseAuditLogs)
        {
            var response = new ManagerBaseResponse<List<LicenseAuditLogs>>();

            try
            {
                var LicenseActive = _context.LicenseActivation.Where(x => x.ActivationId == LicenseAuditLogs.ActivationId && x.LicenseId == LicenseAuditLogs.LicenseId).FirstOrDefault();
               
                if (LicenseActive == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License not active."
                    });
                }
                else
                {
                    if (LicenseAuditLogs.AuditId == 0)
                    {
                        // Insert
                        LicenseAuditLogs _model = new LicenseAuditLogs();
                        _model.LicenseId = LicenseAuditLogs.LicenseId;
                        _model.ActivationId = LicenseAuditLogs.ActivationId;
                        _model.EventMessage = LicenseAuditLogs.EventMessage;
                        _model.EventType    = LicenseAuditLogs.EventType;
                        _model.LoggedAt = LicenseAuditLogs.LoggedAt;
                        _model.MachineHash = LicenseAuditLogs.MachineHash;
                        _model.IPAddress = LicenseAuditLogs.IPAddress;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                     
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "License Activation log Saved Successfully."
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

        public async Task<ManagerBaseResponse<IEnumerable<LicenseAuditLogs>>> GetLicenseAuditLogsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LicenseAuditLogs.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.EventType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.AuditId);

                PageListed<LicenseAuditLogs> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LicenseAuditLogs>>
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

                return new ManagerBaseResponse<IEnumerable<LicenseAuditLogs>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }


        public Task<ManagerBaseResponse<bool>> SaveMachineBindingData(MachineBinding MachineBinding)
        {
            var response = new ManagerBaseResponse<List<MachineBinding>>();

            try
            {
                var LicenseActive = _context.LicenseActivation.Where(x => x.ActivationId == MachineBinding.LicenseActivationId).FirstOrDefault();
                if (LicenseActive == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "License Active Data not found."
                    });
                }
                else
                {
                    if (MachineBinding.MachineBindingId == 0)
                    {
                        // Insert
                        MachineBinding _model = new MachineBinding();
                         _model.MachineHash = MachineBinding.MachineHash;
                        _model.CPUID = MachineBinding.CPUID;
                        _model.MotherboardSerial = MachineBinding.MotherboardSerial;
                        _model.MACAddresses = MachineBinding.MACAddresses;
                        _model.WindowsSID = MachineBinding.WindowsSID;
                        _model.FirstSeenAt = MachineBinding.FirstSeenAt;
                        _model.LastSeenAt = MachineBinding.LastSeenAt;
                        _model.LicenseActivationId = MachineBinding.LicenseActivationId;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.MachineBinding
                            .Where(x => x.MachineBindingId == MachineBinding.MachineBindingId).FirstOrDefault();
                        originalTerm.MachineHash = MachineBinding.MachineHash;
                        originalTerm.CPUID = MachineBinding.CPUID;
                        originalTerm.MotherboardSerial = MachineBinding.MotherboardSerial ;
                        originalTerm.MACAddresses = MachineBinding.MACAddresses ;
                        originalTerm.WindowsSID = MachineBinding.WindowsSID ;
                        originalTerm.FirstSeenAt = MachineBinding.FirstSeenAt ;
                        originalTerm.LastSeenAt = MachineBinding.LastSeenAt ;
                        originalTerm .LicenseActivationId = MachineBinding.LicenseActivationId ;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "MachineBindings Data Saved Successfully."
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

        public async Task<ManagerBaseResponse<IEnumerable<MachineBinding>>> GetMachineBindingFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.MachineBinding.AsQueryable();
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
