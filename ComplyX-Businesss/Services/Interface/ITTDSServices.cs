using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.Tdschallan;
using ComplyX_Businesss.Models.TdschallanAllocation;
using ComplyX_Businesss.Models.Tdsdeductee;
using ComplyX_Businesss.Models.Tdsdeductor;
using ComplyX_Businesss.Models.Tdsentry;
using ComplyX_Businesss.Models.Tdsrate;
using ComplyX_Businesss.Models.Tdsreturn;
using ComplyX_Businesss.Models.TdsreturnChallan;
using ComplyX_Businesss.Models.TdsreturnEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface ITTDSServices
    {
        Task<ManagerBaseResponse<bool>> SaveTDSDeductorData(TdsdeductorRequestModel TDSDeductor , string UserName);
        Task<ManagerBaseResponse<List<TdsdeductorResponseModel>>> GetAllTDSDeductorData(string DeductorID);
        Task<ManagerBaseResponse<IEnumerable<TdsdeductorResponseModel>>> GetTDSDeductorFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSDeduteeData(TdsdeducteeRequestModel TDSDedutee, string UserName);
        Task<ManagerBaseResponse<List<TdsdeducteeResponseModel>>> GetAllTDSDeduteeData(string DeducteeID);
        Task<ManagerBaseResponse<IEnumerable<TdsdeducteeResponseModel>>> GetTDSDeduteeFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSyncTDSDeducteeData(int CompanyID, string UserName);

        Task<ManagerBaseResponse<bool>> SaveSyncTDSDeductorData(int CompanyID, string UserName);

        Task<ManagerBaseResponse<bool>> SaveTDSReturnData(TdsreturnRequestModel TDSReturn, string UserName);
        Task<ManagerBaseResponse<List<TdsreturnResponseModel>>> GetAllTDSReturnData(string ReturnID);
        Task<ManagerBaseResponse<IEnumerable<TdsreturnResponseModel>>> GetTDSReturnFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSEntryData(TdsentryRequestModel TDSEntry, string UserName);
        Task<ManagerBaseResponse<List<TdsentryResponseModel>>> GetAllTDSEntryData(string ReturnID);
        Task<ManagerBaseResponse<IEnumerable<TdsentryResponseModel>>> GetTDSEntryFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSChallanData(TdschallanRequestModel TDSChallan, string UserName);
        Task<ManagerBaseResponse<List<TdschallanResponseModel>>> GetAllTDSChallanData(string ChallanID);
        Task<ManagerBaseResponse<IEnumerable<TdschallanResponseModel>>> GetTDSChallanFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSReturnChallanData(TdsreturnChallanRequsetModel TDSReturnChallan, string UserName);
        Task<ManagerBaseResponse<List<TdsreturnChallanResponseModel>>> GetAllTDSReturnChallanData(string TDSReturnChallanID);
        Task<ManagerBaseResponse<IEnumerable<TdsreturnChallanResponseModel>>> GetTDSReturnChallanFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSReturnEntryData(TdsreturnEntryRequestModel TDSReturnEntry, string UserName);
        Task<ManagerBaseResponse<List<TdsreturnEntryResponseModel>>> GetAllTDSReturnEntryData(string TDSReturnEntryID);
        Task<ManagerBaseResponse<IEnumerable<TdsreturnEntryResponseModel>>> GetTDSReturnEntryFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSChallanAllocationData(TdschallanAllocationRequestModel TDSChallanAllocation, string UserName);
        Task<ManagerBaseResponse<List<TdschallanAllocationResponseModel>>> GetAllTDSChallanAllocationData(string AllocationID);
        Task<ManagerBaseResponse<IEnumerable<TdschallanAllocationResponseModel>>> GetTDSChallanAllocationFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSRatesData(TdsrateRequestModel TDSRate, string UserName);
        Task<ManagerBaseResponse<List<TdsrateResponseModel>>> GetAllTDSRatesData(string TaxID);
        Task<ManagerBaseResponse<IEnumerable<TdsrateResponseModel>>> GetTDSRatesFilter(PagedListCriteria PagedListCriteria);

    }
}
