using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.LicenseActivation;
using ComplyX_Businesss.Models.LicenseAuditLog;
using ComplyX_Businesss.Models.LicenseKeyMaster;
using ComplyX_Businesss.Models.MachineBinding;

namespace ComplyX.Services
{
    public interface LicenseServices
    {
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMasterRequestModel LicenseKeyMaster);
        Task<ManagerBaseResponse<IEnumerable<LicenseKeyMasterResponseModel>>> GetLicenseKeyMasterFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivationRequestModel LicenseActivation);
        Task<ManagerBaseResponse<IEnumerable<LicenseActivationResponseModel>>> GetLicenseActivationFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogRequestModel LicenseAuditLogs);
        Task<ManagerBaseResponse<IEnumerable<LicenseAuditLogResponseModel>>> GetLicenseAuditLogsFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveMachineBindingData(MachineBindingRequestModel MachineBinding);
        Task<ManagerBaseResponse<IEnumerable<MachineBindingResponseModel>>> GetMachineBindingFilter(PagedListCriteria PagedListCriteria);
    }
}
