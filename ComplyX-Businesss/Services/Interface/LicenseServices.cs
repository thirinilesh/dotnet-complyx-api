using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;

namespace ComplyX.Services
{
    public interface LicenseServices
    {
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMaster LicenseKeyMaster);

        Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivation LicenseActivation);

        Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogs LicenseAuditLogs);

        Task<ManagerBaseResponse<bool>> SaveMachineBindingData(MachineBinding MachineBinding);
    }
}
