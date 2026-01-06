using ComplyX.Shared.Data;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Services.Interface;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Implementation
{
    public class ITTDSCClass : ITTDSServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly AppDbContext _context;
        private readonly Nest.Filter _filter;


        public ITTDSCClass(AppDbContext context, Nest.Filter filter)
        {
            _context = context;
            _filter = filter;

        }

        public Task<ManagerBaseResponse<bool>> SaveTDS_PartyData(TDS_Party TDS_Party)
        {
            var response = new ManagerBaseResponse<List<TDS_Party>>();

            try
            {
                if (TDS_Party.PartyID == 0)
                {
                    // Insert
                    TDS_Party _model = new TDS_Party();
                   _model.PartyType = TDS_Party.PartyType;
                    _model.RefID = TDS_Party.RefID;
                    _model.PAN  =   TDS_Party.PAN;
                    _model.TAN = TDS_Party.TAN;
                    _model.Name = TDS_Party.Name;
                    _model.Address = TDS_Party.Address;
                    _model.CreatedOn = Util.GetCurrentCSTDateAndTime();

                    _context.Add(_model);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.TDS_Party
                        .Where(x => x.PartyID == TDS_Party.PartyID)
                        .FirstOrDefault();
                    originalTerm.PartyType = TDS_Party.PartyType;
                    originalTerm.RefID = TDS_Party.RefID;
                    originalTerm.PAN = TDS_Party.PAN;
                    originalTerm.TAN = TDS_Party.TAN;
                    originalTerm.Name = TDS_Party.Name;
                    originalTerm.Address = TDS_Party.Address;
                    originalTerm.CreatedOn = TDS_Party.CreatedOn;

                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Party Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveTDS_PartyData(string PartyID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var product = await _context.TDS_Party.Where(x => x.PartyID.ToString() == PartyID).ToListAsync();

                if (string.IsNullOrEmpty(product.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "TDS Party Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.TDS_Party.RemoveRange(product);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "TDS Party Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<TDS_Party>>> GetAllTDS_PartyData(string PartyID)
        {
            try
            {
                var plans = await _context.TDS_Party.Where(x => x.PartyID.ToString() == PartyID).ToListAsync();

                return new ManagerBaseResponse<List<TDS_Party>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "TDS Party Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<TDS_Party>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<TDS_Party>>> GetTDS_PartyFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.TDS_Party.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PartyID);

                PageListed<TDS_Party> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<TDS_Party>>
                {
                    Result = result.Data,
                    Message = "TDS Party Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<TDS_Party>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
