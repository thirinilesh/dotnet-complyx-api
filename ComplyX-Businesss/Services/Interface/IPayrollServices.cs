using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;

namespace ComplyX.Services
{
    public interface IPayrollServices
    {
        Task<ManagerBaseResponse<bool>> SavePayrollData(PayrollData Payroll);
        Task<ManagerBaseResponse<bool>> RemovePayrollData(string PayrollID);
        Task<ManagerBaseResponse<bool>> RemovePayrollDataByCompanyIDEmployeeID(string CompanyID, string EmployeeID);
        Task<ManagerBaseResponse<bool>> RemoveAllPayrollDataByCompanyID(string CompanyID);
        Task<ManagerBaseResponse<bool>> EditPayrollDataByCompanyIDEmployeeID(PayrollData data, string CompanyID, string EmployeeID, string PayrollID);
        Task<ManagerBaseResponse<IEnumerable<PayrollData>>> GetPayrollDataFilter(PagedListCriteria PagedListCriteria);
    }
}
