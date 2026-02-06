using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX_Businesss.Models.CompanyEPFO;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.EmployeeEPFO;
using ComplyX_Businesss.Models.EPFOECRFile;
using ComplyX_Businesss.Models.EPFOPeriod;
using ComplyX_Businesss.Models.EPFOMonthWage;


namespace ComplyX_Businesss.Services
{
    public interface EPFOServices
    {
        Task<ManagerBaseResponse<bool>> SaveCompanyEPFOData(CompanyEPFORequestModel CompanyEPFO);

        Task<ManagerBaseResponse<bool>> RemoveCompanyEPFOData(string CompanyEPFOId);
        Task<ManagerBaseResponse<IEnumerable<CompanyEpfo>>> GetAllCompanyEPFOFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFORequestModel EmployeeEPFO);

        Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId);
        Task<ManagerBaseResponse<IEnumerable<EmployeeEpfo>>> GetAllEmployeeEPFOFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFileRequestModel EPFOECRFile);

        Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId);
        Task<ManagerBaseResponse<IEnumerable<Epfoecrfile>>> GetEPFOECRDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriodRequestModel EPFOPeriod , string UserID);

        Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId);
        Task<ManagerBaseResponse<IEnumerable<Epfoperiod>>> GetEPFOPeriodDataFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<bool>> SaveEPFOMonthlyWageData(EPFOMonthWageRequestModel EPFOMonthlyWage);

        Task<ManagerBaseResponse<bool>> RemoveEPFOMonthlyWageData(string WageId);
        Task<ManagerBaseResponse<IEnumerable<EpfomonthlyWage>>> GetAllEPFOMonthlyWageFilter(PagedListCriteria PagedListCriteria);
    }
}
