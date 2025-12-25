using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;


namespace ComplyX.Services
{
    public interface EPFOServices
    {
        Task<ManagerBaseResponse<bool>> SaveCompanyEPFOData(CompanyEPFO CompanyEPFO);

        Task<ManagerBaseResponse<bool>> RemoveCompanyEPFOData(string CompanyEPFOId);
        Task<ManagerBaseResponse<IEnumerable<CompanyEPFO>>> GetAllCompanyEPFOFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFO EmployeeEPFO);

        Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId);
        Task<ManagerBaseResponse<IEnumerable<EmployeeEPFO>>> GetAllEmployeeEPFOFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFile EPFOECRFile);

        Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId);
        Task<ManagerBaseResponse<IEnumerable<EPFOECRFile>>> GetEPFOECRDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriod EPFOPeriod , string UserID);

        Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId);
        Task<ManagerBaseResponse<IEnumerable<EPFOPeriod>>> GetEPFOPeriodDataFilter(PagedListCriteria PagedListCriteria);
    }
}
