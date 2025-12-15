using ComplyX.Helper;
using ComplyX.Models;

namespace ComplyX.Services
{
    public interface IEmployeeServices
    {
        Task<ManagerBaseResponse<bool>> SaveEmployeeData(Employees Employees);
        Task<ManagerBaseResponse<bool>> RemoveEmployeeData(string EmployeeID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompany(string CompanyID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID);
        Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID);
    }
}
