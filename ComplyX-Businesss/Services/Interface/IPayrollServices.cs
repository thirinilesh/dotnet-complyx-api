using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.LeaveEncashmentPolicy;
using ComplyX_Businesss.Models.LeaveEncashmentTransaction;
using ComplyX_Businesss.Models.PayrollData;

namespace ComplyX.Services
{
    public interface IPayrollServices
    {
        Task<ManagerBaseResponse<bool>> SavePayrollData(PayrollDataRequestModel Payroll);
        Task<ManagerBaseResponse<bool>> RemovePayrollData(string PayrollID);
        Task<ManagerBaseResponse<bool>> RemovePayrollDataByCompanyIDEmployeeID(string CompanyID, string EmployeeID);
        Task<ManagerBaseResponse<bool>> RemoveAllPayrollDataByCompanyID(string CompanyID);
        Task<ManagerBaseResponse<bool>> EditPayrollDataByCompanyIDEmployeeID(PayrollDataRequestModel data, string CompanyID, string EmployeeID, string PayrollID);
        Task<ManagerBaseResponse<IEnumerable<PayrollDataResponseModel>>> GetPayrollDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_PolicyData(LeaveEncashmentPolicyRequestModel Leave_Encashment_Policy);
        Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_PolicyData(string PolicyID);
        Task<ManagerBaseResponse<List<LeaveEncashmentPolicyResponseModel>>> GetLeave_Encashment_PolicyByID(string PolicyID);
        Task<ManagerBaseResponse<IEnumerable<LeaveEncashmentPolicyResponseModel>>> GetLeave_Encashment_PolicyFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_TransactionData(LeaveEncashmentTransactionRequestModel Leave_Encashment_Transactions);
        Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_TransactionData(string EncashmentID);
        Task<ManagerBaseResponse<List<LeaveEncashmentTransactionResponseModel>>> GetLeave_Encashment_TransactionByID(string EncashmentID);
        Task<ManagerBaseResponse<IEnumerable<LeaveEncashmentTransactionResponseModel>>> GetLeave_Encashment_TransactionFilter(PagedListCriteria PagedListCriteria);

    }
}
