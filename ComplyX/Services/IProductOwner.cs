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
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto, int ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SaveSubcontractorData(Subcontractors Subcontractors);
        Task<ManagerBaseResponse<bool>> RemoveSubcontractorData(string SubcontractorsID);
        Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetSubcontractors(int CompanyID);
        Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetProductOwnerSubcontractorsDetails(int ProductOwnerId);

    }

}
