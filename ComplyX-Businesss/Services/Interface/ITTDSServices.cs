using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface ITTDSServices
    {
        Task<ManagerBaseResponse<bool>> SaveTDS_PartyData(TDS_Party TDS_Party);
        Task<ManagerBaseResponse<bool>> RemoveTDS_PartyData(string PatryID);
        Task<ManagerBaseResponse<List<TDS_Party>>> GetAllTDS_PartyData(string PartyID);
        Task<ManagerBaseResponse<IEnumerable<TDS_Party>>> GetTDS_PartyFilter(PagedListCriteria PagedListCriteria);
    }
}
