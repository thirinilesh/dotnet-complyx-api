using ComplyX_Businesss.Models;
using ComplyX_Tests.Service;

namespace ComplyX_Tests.Repositories.Interface
{
    public interface IEmployeeRepository 
    {
        Task<PaginatedResponse<Employees>> GetEmployeesAsync(Employees request);

    }
}
