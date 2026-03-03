using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX_Businesss.Models.CompanyEPFO;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.EmployeeEPFO;
using ComplyX_Businesss.Models.EPFOECRFile;
using ComplyX_Businesss.Models.EPFOPeriod;
using ComplyX_Businesss.Models.EPFOMonthWage;
using ComplyX_Businesss.Models.CompanyBranches;


namespace ComplyX_Businesss.Services
{
    public interface EPFOServices
    {
        Task<ManagerBaseResponse<bool>> SaveEPFOEstablishmentData(EPFOEstablishmentRequestModel CompanyEPFO);

        Task<ManagerBaseResponse<bool>> RemoveEPFOEstablishmentData(string CompanyEPFOId);
        Task<ManagerBaseResponse<IEnumerable<EPFOEstablishmentResponseModel>>> GetAllEPFOEstablishmentFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEmployeeEPFOData(EmployeeEPFORequestModel EmployeeEPFO);

        Task<ManagerBaseResponse<bool>> RemoveEmployeeEPFOData(string EmployeeEPFOId);
        Task<ManagerBaseResponse<IEnumerable<EmployeeEPFOResponseModel>>> GetAllEmployeeEPFOFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOECRData(EPFOECRFileRequestModel EPFOECRFile);

        Task<ManagerBaseResponse<bool>> RemoveEPFOECRData(string ECRFileId);
        Task<ManagerBaseResponse<IEnumerable<EPFOECRFileResponseModel>>> GetEPFOECRDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveEPFOPeriodData(EPFOPeriodRequestModel EPFOPeriod , string UserID);

        Task<ManagerBaseResponse<bool>> RemoveEPFOPeriodData(string EPFOPeriodId);
        Task<ManagerBaseResponse<IEnumerable<EPFOPeriodResponseModel>>> GetEPFOPeriodDataFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<bool>> SaveEPFOMonthlyWageData(EPFOMonthWageRequestModel EPFOMonthlyWage);

        Task<ManagerBaseResponse<bool>> RemoveEPFOMonthlyWageData(string WageId);
        Task<ManagerBaseResponse<IEnumerable<EPFOMonthWageResponseModel>>> GetAllEPFOMonthlyWageFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<bool>> SaveCompanyBranchesData(CompanyBranchesRequestModel companyBranches);

        Task<ManagerBaseResponse<bool>> RemoveCompanyBranchesData(string BranchId);
        Task<ManagerBaseResponse<IEnumerable<CompanyBranchesResponseModel>>> GetAllCompanyBranchesFilter(PagedListCriteria PagedListCriteria);
    }
}
