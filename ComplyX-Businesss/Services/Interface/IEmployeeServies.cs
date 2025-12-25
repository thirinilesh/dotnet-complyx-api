using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;

namespace ComplyX.Services
{
    public interface IEmployeeServices
    {
        Task<ManagerBaseResponse<bool>> SaveEmployeeData(Employees Employees);
        Task<ManagerBaseResponse<bool>> RemoveEmployeeData(string EmployeeID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompany(string CompanyID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID);
        Task<ManagerBaseResponse<IEnumerable<Employees>>> GetEmployeeDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGratuity_PolicyData(Gratuity_Policy Gratuity_Policy);
        Task<ManagerBaseResponse<bool>> RemoveGratuity_PolicyData(string PolicyID);
        Task<ManagerBaseResponse<bool>> SaveGratuity_TransactionsData(Gratuity_Transactions Gratuity_Transactions);
        Task<ManagerBaseResponse<bool>> RemoveGratuity_TransactionsData(string GratuityID);
    }
}
