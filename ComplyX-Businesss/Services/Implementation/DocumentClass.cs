using AutoMapper.Configuration.Annotations;
using ComplyX_Businesss.Helper;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Data;
using ComplyX_Businesss.Services.Interface;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using static ComplyX_Businesss.Helper.Commanfield;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.LegalDocument;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.LegalDocumentVersion;
using ComplyX_Businesss.Models.LegalDocumentAcceptance;
using System.Security.Claims;
using ComplyX.Data.DbContexts;
using ComplyX_Businesss.Models.ComplianceSchedule;

namespace ComplyX_Businesss.Services.Implementation
{
    public class DocumentClass : DocumentService
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

        public DocumentClass(AppDbContext context, Nest.Filter filter, UserManager<ApplicationUsers> userManager, Commanfield commanfield,IUnitOfWork unitOfWork)
        {
            _context = context;
            _filter = filter;
            _userManager = userManager;
            _commanfield = commanfield;
             _UnitOfWork = unitOfWork;
        }

        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentData(LegalDocumentRequestModel legalDocument , string UserName)
        {
            var response = new ManagerBaseResponse<List<LegalDocument>>();

            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                // var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        Message = "User is not Found."
                    };
                }
                else
                {

                    if (legalDocument.DocumentId == 0)
                    {
                        // Insert
                        LegalDocument originalTerm = new LegalDocument();
                        originalTerm.DocumentName = legalDocument.DocumentName;
                        originalTerm.DocumentCode = legalDocument.DocumentCode;
                        originalTerm.ApplicableRegion = legalDocument.ApplicableRegion;
                        originalTerm.Status = legalDocument.Status;
                        originalTerm.CreatedBy = user.Id;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.LegalDocumentRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.LegalDocumentRespositories.GetQueryable()
                            .Where(x => x.DocumentId == legalDocument.DocumentId)
                            .FirstOrDefault();
                        originalTerm.DocumentName = legalDocument.DocumentName;
                        originalTerm.DocumentCode = legalDocument.DocumentCode;
                        originalTerm.ApplicableRegion = legalDocument.ApplicableRegion;
                        originalTerm.Status = legalDocument.Status;
                        originalTerm.CreatedBy = user.Id.ToString();
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                      
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemovelegalDocumentData(string document_id)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Party = await _UnitOfWork.LegalDocumentRespositories.GetQueryable().Where(x => x.DocumentId.ToString() == document_id).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document Id is not Vaild",
                    };
                }

                // Remove all related report details
                //_context.LegalDocument.RemoveRange(Party);

                //await _context.SaveChangesAsync();
                _UnitOfWork.LegalDocumentRespositories.RemoveRange(Party);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<LegalDocumentResponseModel>>> GetAlllegalDocumentData(string document_id)
        {
            try
            {
                var plans = _UnitOfWork.LegalDocumentRespositories.GetQueryable().Where(x => x.DocumentId.ToString() == document_id).Select(x => new LegalDocumentResponseModel
                {
                    DocumentId = x.DocumentId,
                    DocumentCode = x.DocumentCode,
                    DocumentName = x.DocumentName,
                    ApplicableRegion = x.ApplicableRegion,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                }).ToList();


                return new ManagerBaseResponse<List<LegalDocumentResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<LegalDocumentResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<LegalDocumentResponseModel>>> GetAlllegalDocumentFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LegalDocumentRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.DocumentName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.DocumentId);
                var responseQuery = query.Select(x => new LegalDocumentResponseModel
                {
                    DocumentId = x.DocumentId,
                    DocumentCode = x.DocumentCode,
                    DocumentName = x.DocumentName,
                    ApplicableRegion = x.ApplicableRegion,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy
                });
                PageListed<LegalDocumentResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<LegalDocumentResponseModel>>
                {
                    Result = result.Data,
                    Message = "Legal DOcument Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<LegalDocumentResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentVersionData(LegalDocumentVersionRequestModel legalDocumentVersion, string UserName)
        {
            var response = new ManagerBaseResponse<List<LegalDocumentVersion>>();

            try
            {

                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        Message = "User is not Found."
                    };
                }
                else
                {
                    var document =  _UnitOfWork.LegalDocumentRespositories.GetQueryable().FirstOrDefault(x => x.DocumentId == legalDocumentVersion.DocumentId);
                    if (document == null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = true,
                            Message = "Legal Document  is not Found."
                        };
                    }
                    else
                    {
                        if (legalDocumentVersion.VersionId == 0)
                        {
                            // Insert
                            LegalDocumentVersion originalTerm = new LegalDocumentVersion();
                            originalTerm.DocumentId = legalDocumentVersion.DocumentId;
                            originalTerm.VersionNumber = legalDocumentVersion.VersionNumber;
                            originalTerm.VersionType = legalDocumentVersion.VersionType;
                            originalTerm.ChangeSummary = legalDocumentVersion.ChangeSummary;
                            originalTerm.ContentHash = legalDocumentVersion.ContentHash;
                            originalTerm.EffectiveFromDate = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.ReleaseDate = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.ExpiryDate = legalDocumentVersion.ExpiryDate;
                            originalTerm.HtmlContent = legalDocumentVersion.HtmlContent;
                            originalTerm.LegalBasis = legalDocumentVersion.LegalBasis;
                            originalTerm.IsPublished = legalDocumentVersion.IsPublished;
                            originalTerm.IsActive = legalDocumentVersion.IsActive;
                            originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.CreatedBy = user.Id;
                            
                            await _UnitOfWork.LegalDocumentVersionRespositories.AddAsync(originalTerm);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.LegalDocumentVersionRespositories.GetQueryable()
                                .Where(x => x.VersionId == legalDocumentVersion.VersionId)
                                .FirstOrDefault();
                            originalTerm.DocumentId = legalDocumentVersion.DocumentId;
                            originalTerm.VersionNumber = legalDocumentVersion.VersionNumber;
                            originalTerm.VersionType = legalDocumentVersion.VersionType;
                            originalTerm.ChangeSummary = legalDocumentVersion.ChangeSummary;
                            originalTerm.ContentHash = legalDocumentVersion.ContentHash;
                            originalTerm.EffectiveFromDate = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.ReleaseDate = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.ExpiryDate = legalDocumentVersion.ExpiryDate;
                            originalTerm.HtmlContent = legalDocumentVersion.HtmlContent;
                            originalTerm.LegalBasis = legalDocumentVersion.LegalBasis;
                            originalTerm.IsPublished = legalDocumentVersion.IsPublished;
                            originalTerm.IsActive = legalDocumentVersion.IsActive;
                            originalTerm.ApprovedAt = Util.GetCurrentCSTDateAndTime();
                            originalTerm.ApprovedBy = user.Id;
                            
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document version Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemovelegalDocumentVersionData(string version_id)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Party = await _UnitOfWork.LegalDocumentVersionRespositories.GetQueryable().Where(x => x.VersionId.ToString() == version_id).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document version Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.LegalDocumentVersionRespositories.RemoveRange(Party);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document version Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<LegalDocumentVersionResponseModel>>> GetAlllegalDocumentVersionData(string version_id)
        {
            try
            {
                var plans = _UnitOfWork.LegalDocumentVersionRespositories.GetQueryable().Where(x => x.VersionId.ToString() == version_id).Select(x => new LegalDocumentVersionResponseModel
                {
                    VersionId = x.VersionId,
                    DocumentId = x.DocumentId,
                    VersionNumber = x.VersionNumber,
                    VersionType = x.VersionType,
                    HtmlContent = x.HtmlContent,
                    ContentHash = x.ContentHash,
                    EffectiveFromDate = x.EffectiveFromDate,
                    ReleaseDate = x.ReleaseDate,
                    ExpiryDate = x.ExpiryDate,
                    ChangeSummary = x.ChangeSummary,
                    LegalBasis = x.LegalBasis,
                    IsPublished = x.IsPublished,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    ApprovedAt = x.ApprovedAt,
                    ApprovedBy = x.ApprovedBy
                }).ToList();


                return new ManagerBaseResponse<List<LegalDocumentVersionResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document version Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<LegalDocumentVersionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<LegalDocumentVersionResponseModel>>> GetAlllegalDocumentVersionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LegalDocumentVersionRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.VersionNumber.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.VersionId);
                var responseQuery = query.Select(x => new LegalDocumentVersionResponseModel
                {
                    VersionId = x.VersionId,
                    DocumentId = x.DocumentId,
                    VersionNumber = x.VersionNumber,
                    VersionType = x.VersionType,
                    HtmlContent = x.HtmlContent,
                    ContentHash = x.ContentHash,
                    EffectiveFromDate = x.EffectiveFromDate,
                    ReleaseDate = x.ReleaseDate,
                    ExpiryDate = x.ExpiryDate,
                    ChangeSummary = x.ChangeSummary,
                    LegalBasis = x.LegalBasis,
                    IsPublished = x.IsPublished,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    ApprovedAt = x.ApprovedAt,
                    ApprovedBy = x.ApprovedBy
                });
                PageListed<LegalDocumentVersionResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<LegalDocumentVersionResponseModel>>
                {
                    Result = result.Data,
                    Message = "Legal Document version Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<LegalDocumentVersionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentAcceptanceData(LegalDocumentAcceptanceRequestModel legalDocumentAcceptance, string UserName)
        {
            var response = new ManagerBaseResponse<List<LegalDocumentAcceptance>>();

            try
            {

                var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == UserName);
                if (user == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        Message = "User is not Found."
                    };
                }
                else 
                {
                    var document = _UnitOfWork.LegalDocumentVersionRespositories.GetQueryable().FirstOrDefault(x => x.DocumentId == legalDocumentAcceptance.DocumentId && x.VersionId == legalDocumentAcceptance.VersionId);
                    if (document == null)
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = true,
                            Message = "Legal Document and Legal Document Version is not Found."
                        };
                    }
                    else
                    {

                        if (legalDocumentAcceptance.AcceptanceId == 0)
                        {
                            // Insert
                            LegalDocumentAcceptance originalTerm = new LegalDocumentAcceptance();
                            originalTerm.VersionId = legalDocumentAcceptance.VersionId;
                            originalTerm.DocumentId = legalDocumentAcceptance.DocumentId;
                            originalTerm.AcceptedAt = legalDocumentAcceptance.AcceptedAt;
                            originalTerm.UserId = user.Id;
                            originalTerm.AcceptedDevice = legalDocumentAcceptance.AcceptedDevice;
                            originalTerm.AcceptanceMethod = legalDocumentAcceptance.AcceptanceMethod;
                            originalTerm.ConsentProofHash = legalDocumentAcceptance.ConsentProofHash;
                            originalTerm.AcceptedIp = legalDocumentAcceptance.AcceptedIp;
                            
                            await _UnitOfWork.LegalDocumentAcceptanceRespositories.AddAsync(originalTerm);
                        }
                        else
                        {
                            // Update
                            var originalTerm = _UnitOfWork.LegalDocumentAcceptanceRespositories.GetQueryable()
                                .Where(x => x.AcceptanceId == legalDocumentAcceptance.AcceptanceId)
                                .FirstOrDefault();
                            originalTerm.VersionId = legalDocumentAcceptance.VersionId;
                            originalTerm.DocumentId = legalDocumentAcceptance.DocumentId;
                            originalTerm.AcceptedAt = legalDocumentAcceptance.AcceptedAt;
                            originalTerm.UserId = user.Id;
                            originalTerm.AcceptedDevice = legalDocumentAcceptance.AcceptedDevice;
                            originalTerm.AcceptanceMethod = legalDocumentAcceptance.AcceptanceMethod;
                            originalTerm.ConsentProofHash = legalDocumentAcceptance.ConsentProofHash;
                            originalTerm.AcceptedIp = legalDocumentAcceptance.AcceptedIp;
                             
                        }
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document Acceptance Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemovelegalDocumentAcceptanceData(string Acceptanceid)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Party = await _UnitOfWork.LegalDocumentAcceptanceRespositories.GetQueryable().Where(x => x.AcceptanceId.ToString() == Acceptanceid).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document Acceptance Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.LegalDocumentAcceptanceRespositories.RemoveRange(Party);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Legal Document Acceptance Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<LegalDocumentAcceptanceResponseModel>>> GetAlllegalDocumentAcceptanceData(string Acceptanceid)
        {
            try
            {
                var plans = _UnitOfWork.LegalDocumentAcceptanceRespositories.GetQueryable().Where(x => x.AcceptanceId.ToString() == Acceptanceid).Select(x => new LegalDocumentAcceptanceResponseModel
                {
                    AcceptanceId = x.AcceptanceId,
                    UserId = x.UserId,
                    DocumentId = x.DocumentId,
                    VersionId = x.VersionId,
                    AcceptedAt = x.AcceptedAt,
                    AcceptedIp = x.AcceptedIp,
                    AcceptedDevice = x.AcceptedDevice,
                    AcceptanceMethod = x.AcceptanceMethod,
                    ConsentProofHash = x.ConsentProofHash
                }).ToList();


                return new ManagerBaseResponse<List<LegalDocumentAcceptanceResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document Acceptance Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<LegalDocumentAcceptanceResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<LegalDocumentAcceptanceResponseModel>>> GetAlllegalDocumentAcceptanceFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LegalDocumentAcceptanceRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.AcceptedDevice.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.AcceptanceId);
                var responseQuery = query.Select(x => new LegalDocumentAcceptanceResponseModel
                {
                    AcceptanceId = x.AcceptanceId,
                    UserId = x.UserId,
                    DocumentId = x.DocumentId,
                    VersionId = x.VersionId,
                    AcceptedAt = x.AcceptedAt,
                    AcceptedIp = x.AcceptedIp,
                    AcceptedDevice = x.AcceptedDevice,
                    AcceptanceMethod = x.AcceptanceMethod,
                    ConsentProofHash = x.ConsentProofHash
                });
                PageListed<LegalDocumentAcceptanceResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<LegalDocumentAcceptanceResponseModel>>
                {
                    Result = result.Data,
                    Message = "Legal Document Acceptance Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<LegalDocumentAcceptanceResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
