using Castle.Core.Resource;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Repositories.Implementation;

namespace ComplyX_Tests.Repositories.Interface
{
    public interface ICompanyRespository
    {
        Task<bool> CompanyExists(int companyId);
 

        Task<bool> CompanyExistsByNameAsync(string companyName);

        Task<Company> GetCompanyByIdAsync(int companyId);

        void AddCompany(Company company);

        Task<Company> GetCompanyByIdForUpdateAsync(int companyId);

        Task ActivateCompanyAsync(int companyId, string activatedBy);


    }
}
