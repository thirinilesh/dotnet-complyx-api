using ComplyX.Helper;
using ComplyX.Models;
using ComplyX.Services;

namespace ComplyX.Services
{
    public interface EPFOServices
    {
        Task<ManagerBaseResponse<bool>> SaveCompanyEPFOData(CompanyEPFO CompanyEPFO);

        Task<ManagerBaseResponse<bool>> RemoveCompanyEPFOData(string CompanyEPFOId);

        Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFO EmployeeEPFO);

        Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId);
        Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFile EPFOECRFile);

        Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId);

        Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriod EPFOPeriod , string UserID);

        Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId);
    }
}
