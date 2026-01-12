using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;

namespace ComplyX.Services
{
    public interface LicenseServices
    {
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMaster LicenseKeyMaster);
        Task<ManagerBaseResponse<IEnumerable<LicenseKeyMaster>>> GetLicenseKeyMasterFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivation LicenseActivation);
        Task<ManagerBaseResponse<IEnumerable<LicenseActivation>>> GetLicenseActivationFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogs LicenseAuditLogs);
        Task<ManagerBaseResponse<IEnumerable<LicenseAuditLogs>>> GetLicenseAuditLogsFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveMachineBindingData(MachineBinding MachineBinding);
        Task<ManagerBaseResponse<IEnumerable<MachineBinding>>> GetMachineBindingFilter(PagedListCriteria PagedListCriteria);
    }
}
