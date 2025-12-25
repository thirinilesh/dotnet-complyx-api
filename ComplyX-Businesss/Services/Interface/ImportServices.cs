using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;

namespace ComplyX.Services
{
    public interface ImportServices
    {
        Task<ManagerBaseResponse<ImportModel>> UploadEmployeeImportFile(string userId, ImportModel request);
    }
}
