using ComplyX.Helper;
using ComplyX.Models;

namespace ComplyX.Services
{
    public interface IProductOwner
    {
        Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwners ProductOwners);
        Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SaveCompanyData(Company company);
        Task<ManagerBaseResponse<bool>> RemoveCompanyData(string CompanyId);
        Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlans();
        Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlanFilter(SubscriptionPlansFilterRequest request);
        Task<ManagerBaseResponse<bool>> SaveUserSubscriptionData(ProductOwnerSubscriptions ProductOwnerSubscriptions);
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto);

    }

}
