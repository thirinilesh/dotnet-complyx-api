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
        Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_PolicyData(Leave_Encashment_Policy Leave_Encashment_Policy);
        Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_PolicyData(string PolicyID);
        Task<ManagerBaseResponse<List<Leave_Encashment_Policy>>> GetLeave_Encashment_PolicyByID(string PolicyID);
        Task<ManagerBaseResponse<IEnumerable<Leave_Encashment_Policy>>> GetLeave_Encashment_PolicyFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_TransactionData(Leave_Encashment_Transactions Leave_Encashment_Transactions);
        Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_TransactionData(string EncashmentID);
        Task<ManagerBaseResponse<List<Leave_Encashment_Transactions>>> GetLeave_Encashment_TransactionByID(string EncashmentID);

        Task<ManagerBaseResponse<IEnumerable<Leave_Encashment_Transactions>>> GetLeave_Encashment_TransactionFilter(PagedListCriteria PagedListCriteria);

    }
}
