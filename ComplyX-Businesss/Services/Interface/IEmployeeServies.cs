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
    }
}
