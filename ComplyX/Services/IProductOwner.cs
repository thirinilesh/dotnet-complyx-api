using ComplyX.Helper;
using ComplyX.Models;

namespace ComplyX.Services
{
    public interface IProductOwner
    {
        Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwners ProductOwners);

        Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string AccountOwnereId);
    }

}
