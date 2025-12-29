using AutoMapper;
using ComplyX_Businesss.Models;
using ComplyX.Common.Data.Context;
using ComplyX.Shared.Data;
using ComplyX_Tests.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Resource;

namespace ComplyX_Tests.Repositories.Implementation
{
    public class CompanyRespository : ICompanyRespository
    {

        private readonly DboContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public CompanyRespository(DboContext dbContext, IMapper mapper, AppDbContext appDbContext)
        {
            _dbContext = dbContext;
            this._mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<bool> CompanyExists(int companyId)
        {
            return await _appDbContext.Companies.AnyAsync(c => c.CompanyID == companyId);
        }

        public async Task<bool> CompanyExistsByNameAsync(string companyName)
        {
            return await _appDbContext.Companies.AnyAsync(c => c.Name == companyName);
        }
        public void AddCompany(Company company) 
        { 
            _appDbContext.Companies.Add(company); 
        }
        public async Task<Company> GetCompanyByIdForUpdateAsync(int companyId)
        {
            return await _appDbContext.Companies
                .Where(c => c.CompanyID == companyId).FirstOrDefaultAsync();
        }
        

        public async Task ActivateCompanyAsync(int companyId, string activatedBy)
        {
            var company = await GetCompanyByIdAsync(companyId);

            if (company != null)
            {
                company.IsActive = true;
                company.CreatedAt = DateTime.UtcNow;
                
                // EF Core tracks changes automatically, but explicit Update is fine
                _appDbContext.Companies.Update(company);
            }
        }

        public async Task<Company> GetCompanyByIdAsync(int companyId)
        {
            return await _appDbContext.Companies
                .Where(c => c.CompanyID == companyId)
                .FirstOrDefaultAsync();
        }

         

    }
}