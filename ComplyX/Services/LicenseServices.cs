using ComplyX.Helper;
using ComplyX.Models;

namespace ComplyX.Services
{
    public interface LicenseServices
    {
        Task<ManagerBaseResponse<bool>> SaveLicenseKeyMasterData(LicenseKeyMaster LicenseKeyMaster);

        Task<ManagerBaseResponse<bool>> SaveLicenseKeyActivationData(LicenseActivation LicenseActivation);

        Task<ManagerBaseResponse<bool>> SaveLicenseAuditLogsData(LicenseAuditLogs LicenseAuditLogs);
    }
}
