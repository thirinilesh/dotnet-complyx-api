using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.Employee;
using ComplyX_Businesss.Models.FnFCalculation;
using ComplyX_Businesss.Models.GratuityPolicy;
using ComplyX_Businesss.Models.GratuityTransaction;

namespace ComplyX.Services
{
    public interface IEmployeeServices
    {
        Task<ManagerBaseResponse<bool>> SaveEmployeeData(EmployeeRequestModel Employees);
        Task<ManagerBaseResponse<bool>> RemoveEmployeeData(string EmployeeID);
        Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompany(string CompanyID);
        Task<ManagerBaseResponse<List<CommonDropdownModel>>> GetEmployeeData();
        Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID);
        Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID);
        Task<ManagerBaseResponse<IEnumerable<EmployeeResponseModel>>> GetEmployeeDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGratuity_PolicyData(GratuityPolicyRequestModel Gratuity_Policy);
        Task<ManagerBaseResponse<bool>> RemoveGratuity_PolicyData(string PolicyID);
        Task<ManagerBaseResponse<List<GratuityPolicyResponseModel>>> GetGratuity_Policy(string PolicyID);
        Task<ManagerBaseResponse<IEnumerable<GratuityPolicyResponseModel>>> GetGratuity_PolicyFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGratuity_TransactionsData(GratuityTransactionRequestModel Gratuity_Transactions);
        Task<ManagerBaseResponse<bool>> RemoveGratuity_TransactionsData(string GratuityID);
        Task<ManagerBaseResponse<List<GratuityTransactionResponseModel>>> GetGratuity_Transactions(string GratuityID);
        Task<ManagerBaseResponse<IEnumerable<GratuityTransactionResponseModel>>> GetGratuity_TransactionsFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveFnF_CalculationsData(FnFCalculationRequestModel FnF_Calculations);
        Task<ManagerBaseResponse<bool>> RemoveFnF_CalculationsData(string FnFID);
        Task<ManagerBaseResponse<List<FnFCalculationResponseModel>>> GetFnF_Calculations(string FnFID);
        Task<ManagerBaseResponse<IEnumerable<FnFCalculationResponseModel>>> GetFnF_CalculationsFilter(PagedListCriteria PagedListCriteria);


    }
}
