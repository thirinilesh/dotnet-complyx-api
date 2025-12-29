namespace ComplyX_Tests.Repositories.Interface
{
    public interface IPartnerApiAuthConfigRepository 
    {
        Task<bool> CheckUserCompanyAccess(string userId, int companyId);

        Task<bool> CheckUserBrandAccess(string userId, string brand);

#nullable enable
        Task<string?> GetApiUsersDefaultBrandName(string userId);

      

    }
}
