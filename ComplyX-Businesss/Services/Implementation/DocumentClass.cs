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

    }
}
