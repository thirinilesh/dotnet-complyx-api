using AutoMapper.Configuration.Annotations;
using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using static ComplyX_Businesss.Helper.Commanfield;

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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Commanfield _commanfield;

        public DocumentClass(AppDbContext context, Nest.Filter filter, UserManager<ApplicationUser> userManager, Commanfield commanfield)
        {
            _context = context;
            _filter = filter;
            _userManager = userManager;
            _commanfield = commanfield;
        }

        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentData(legalDocument legalDocument , string UserName)
        {
            var response = new ManagerBaseResponse<List<legalDocument>>();

            try
            {
                
                    var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
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

                    if (legalDocument.document_id == 0)
                    {
                        // Insert
                        legalDocument originalTerm = new legalDocument();
                        originalTerm.document_name = legalDocument.document_name;
                        originalTerm.document_code = legalDocument.document_code;
                        originalTerm.applicable_region = legalDocument.applicable_region;
                        originalTerm.status = legalDocument.status;
                        originalTerm.created_by = user.Id;
                        originalTerm.created_at = Util.GetCurrentCSTDateAndTime();

                        _context.Add(originalTerm);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.LegalDocument
                            .Where(x => x.document_id == legalDocument.document_id)
                            .FirstOrDefault();
                        originalTerm.document_name = legalDocument.document_name;
                        originalTerm.document_code = legalDocument.document_code;
                        originalTerm.applicable_region = legalDocument.applicable_region;
                        originalTerm.status = legalDocument.status;
                        originalTerm.created_by = user.Id;
                        originalTerm.created_at = Util.GetCurrentCSTDateAndTime();
                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
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
                var Party = await _context.LegalDocument.Where(x => x.document_id.ToString() == document_id).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.LegalDocument.RemoveRange(Party);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<List<legalDocument>>> GetAlllegalDocumentData(string document_id)
        {
            try
            {
                var plans = _context.LegalDocument.Where(x => x.document_id.ToString() == document_id).ToList();


                return new ManagerBaseResponse<List<legalDocument>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<legalDocument>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<legalDocument>>> GetAlllegalDocumentFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LegalDocument.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.document_name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.document_id);

                PageListed<legalDocument> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<legalDocument>>
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

                return new ManagerBaseResponse<IEnumerable<legalDocument>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentVersionData(legalDocumentVersion legalDocumentVersion, string UserName)
        {
            var response = new ManagerBaseResponse<List<legalDocumentVersion>>();

            try
            {

                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
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
                    var document =  _context.LegalDocument.FirstOrDefault(x => x.document_id == legalDocumentVersion.document_id);
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
                        if (legalDocumentVersion.version_id == 0)
                        {
                            // Insert
                            legalDocumentVersion originalTerm = new legalDocumentVersion();
                            originalTerm.document_id = legalDocumentVersion.document_id;
                            originalTerm.version_number = legalDocumentVersion.version_number;
                            originalTerm.version_type = legalDocumentVersion.version_type;
                            originalTerm.change_summary = legalDocumentVersion.change_summary;
                            originalTerm.content_hash = legalDocumentVersion.content_hash;
                            originalTerm.effective_from_date = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.release_date = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.expiry_date = legalDocumentVersion.expiry_date;
                            originalTerm.html_content = legalDocumentVersion.html_content;
                            originalTerm.legal_basis = legalDocumentVersion.legal_basis;
                            originalTerm.is_published = legalDocumentVersion.is_published;
                            originalTerm.is_active = legalDocumentVersion.is_active;
                            originalTerm.created_at = Util.GetCurrentCSTDateAndTime();
                            originalTerm.created_by = user.Id;
                            _context.Add(originalTerm);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.LegalDocumentVersion
                                .Where(x => x.version_id == legalDocumentVersion.version_id)
                                .FirstOrDefault();
                            originalTerm.version_number = legalDocumentVersion.version_number;
                            originalTerm.document_id = legalDocumentVersion.document_id;
                            originalTerm.version_type = legalDocumentVersion.version_type;
                            originalTerm.change_summary = legalDocumentVersion.change_summary;
                            originalTerm.content_hash = legalDocumentVersion.content_hash;
                            originalTerm.effective_from_date = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.release_date = DateOnly.FromDateTime(DateTime.Now);
                            originalTerm.expiry_date = legalDocumentVersion.expiry_date;
                            originalTerm.html_content = legalDocumentVersion.html_content;
                            originalTerm.legal_basis = legalDocumentVersion.legal_basis;
                            originalTerm.is_published = legalDocumentVersion.is_published;
                            originalTerm.is_active = legalDocumentVersion.is_active;
                            originalTerm.approved_at = Util.GetCurrentCSTDateAndTime();
                            originalTerm.approved_by = user.Id;
                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
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
                var Party = await _context.LegalDocumentVersion.Where(x => x.version_id.ToString() == version_id).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document version Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.LegalDocumentVersion.RemoveRange(Party);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<List<legalDocumentVersion>>> GetAlllegalDocumentVersionData(string version_id)
        {
            try
            {
                var plans = _context.LegalDocumentVersion.Where(x => x.version_id.ToString() == version_id).ToList();


                return new ManagerBaseResponse<List<legalDocumentVersion>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document version Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<legalDocumentVersion>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<legalDocumentVersion>>> GetAlllegalDocumentVersionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LegalDocumentVersion.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.version_number.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.version_id);

                PageListed<legalDocumentVersion> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<legalDocumentVersion>>
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

                return new ManagerBaseResponse<IEnumerable<legalDocumentVersion>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SavelegalDocumentAcceptanceData(legalDocumentAcceptance legalDocumentAcceptance, string UserName)
        {
            var response = new ManagerBaseResponse<List<legalDocumentAcceptance>>();

            try
            {

                var user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
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
                    var document = _context.LegalDocumentVersion.FirstOrDefault(x => x.document_id == legalDocumentAcceptance.document_id && x.version_id == legalDocumentAcceptance.version_id);
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

                        if (legalDocumentAcceptance.acceptance_id == 0)
                        {
                            // Insert
                            legalDocumentAcceptance originalTerm = new legalDocumentAcceptance();
                            originalTerm.version_id = legalDocumentAcceptance.version_id;
                            originalTerm.document_id = legalDocumentAcceptance.document_id;
                            originalTerm.accepted_at = legalDocumentAcceptance.accepted_at;
                            originalTerm.user_id = user.Id;
                            originalTerm.accepted_device = legalDocumentAcceptance.accepted_device;
                            originalTerm.acceptance_method = legalDocumentAcceptance.acceptance_method;
                            originalTerm.consent_proof_hash = legalDocumentAcceptance.consent_proof_hash;
                            originalTerm.accepted_ip = legalDocumentAcceptance.accepted_ip;
                            _context.Add(originalTerm);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.LegalDocumentAcceptance
                                .Where(x => x.acceptance_id == legalDocumentAcceptance.acceptance_id)
                                .FirstOrDefault();
                            originalTerm.version_id = legalDocumentAcceptance.version_id;
                            originalTerm.document_id = legalDocumentAcceptance.document_id;
                            originalTerm.accepted_at = legalDocumentAcceptance.accepted_at;
                            originalTerm.user_id = user.Id;
                            originalTerm.accepted_device = legalDocumentAcceptance.accepted_device;
                            originalTerm.acceptance_method = legalDocumentAcceptance.acceptance_method;
                            originalTerm.consent_proof_hash = legalDocumentAcceptance.consent_proof_hash;
                            originalTerm.accepted_ip = legalDocumentAcceptance.accepted_ip;
                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
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
                var Party = await _context.LegalDocumentAcceptance.Where(x => x.acceptance_id.ToString() == Acceptanceid).ToListAsync();

                if (string.IsNullOrEmpty(Party.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Legal Document Acceptance Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.LegalDocumentAcceptance.RemoveRange(Party);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<List<legalDocumentAcceptance>>> GetAlllegalDocumentAcceptanceData(string Acceptanceid)
        {
            try
            {
                var plans =  _context.LegalDocumentAcceptance.Where(x => x.acceptance_id.ToString() == Acceptanceid).ToList();


                return new ManagerBaseResponse<List<legalDocumentAcceptance>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "Legal Document Acceptance Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<legalDocumentAcceptance>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<legalDocumentAcceptance>>> GetAlllegalDocumentAcceptanceFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.LegalDocumentAcceptance.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.accepted_device.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.acceptance_id);

                PageListed<legalDocumentAcceptance> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<legalDocumentAcceptance>>
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

                return new ManagerBaseResponse<IEnumerable<legalDocumentAcceptance>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}
