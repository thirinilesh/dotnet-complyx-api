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
        Task<ManagerBaseResponse<bool>> SaveTDSDeductorData(TDSDeductor TDSDeductor , string UserName);
        Task<ManagerBaseResponse<List<TDSDeductor>>> GetAllTDSDeductorData(string DeductorID);
        Task<ManagerBaseResponse<IEnumerable<TDSDeductor>>> GetTDSDeductorFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSDeduteeData(TDSDeductee TDSDedutee, string UserName);
        Task<ManagerBaseResponse<List<TDSDeductee>>> GetAllTDSDeduteeData(string DeducteeID);
        Task<ManagerBaseResponse<IEnumerable<TDSDeductee>>> GetTDSDeduteeFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSyncTDSDeducteeData(int CompanyID, string UserName);

        Task<ManagerBaseResponse<bool>> SaveSyncTDSDeductorData(int CompanyID, string UserName);

        Task<ManagerBaseResponse<bool>> SaveTDSReturnData(TDSReturn TDSReturn, string UserName);
        Task<ManagerBaseResponse<List<TDSReturn>>> GetAllTDSReturnData(string ReturnID);
        Task<ManagerBaseResponse<IEnumerable<TDSReturn>>> GetTDSReturnFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSEntryData(TDSEntry TDSEntry, string UserName);
        Task<ManagerBaseResponse<List<TDSEntry>>> GetAllTDSEntryData(string ReturnID);
        Task<ManagerBaseResponse<IEnumerable<TDSEntry>>> GetTDSEntryFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSChallanData(TDSChallan TDSChallan, string UserName);
        Task<ManagerBaseResponse<List<TDSChallan>>> GetAllTDSChallanData(string ChallanID);
        Task<ManagerBaseResponse<IEnumerable<TDSChallan>>> GetTDSChallanFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSReturnChallanData(TDSReturnChallan TDSReturnChallan, string UserName);
        Task<ManagerBaseResponse<List<TDSReturnChallan>>> GetAllTDSReturnChallanData(string TDSReturnChallanID);
        Task<ManagerBaseResponse<IEnumerable<TDSReturnChallan>>> GetTDSReturnChallanFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSReturnEntryData(TDSReturnEntry TDSReturnEntry, string UserName);
        Task<ManagerBaseResponse<List<TDSReturnEntry>>> GetAllTDSReturnEntryData(string TDSReturnEntryID);
        Task<ManagerBaseResponse<IEnumerable<TDSReturnEntry>>> GetTDSReturnEntryFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveTDSChallanAllocationData(TDSChallanAllocation TDSChallanAllocation, string UserName);
        Task<ManagerBaseResponse<List<TDSChallanAllocation>>> GetAllTDSChallanAllocationData(string AllocationID);
        Task<ManagerBaseResponse<IEnumerable<TDSChallanAllocation>>> GetTDSChallanAllocationFilter(PagedListCriteria PagedListCriteria);

    }
}
