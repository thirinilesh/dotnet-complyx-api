using Castle.Core.Resource;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Repositories.Implementation;
using ComplyX_Tests.Service;

namespace ComplyX_Tests.Repositories.Interface
{
    public interface ICompanyRespository
    {
        Task<bool> CompanyExists(int companyId); 
        Task<bool> CompanyExistsByNameAsync(string companyName);
        Task<int> SaveChangesAsync();
        Task<bool> CompanyExistsByNameAsync(string companyName,bool caseSensitive, int excludedCompanyId = 0);
      
    }
}
