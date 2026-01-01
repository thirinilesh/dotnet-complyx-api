using AutoMapper;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Resource;
using Nest;
using Moq;
using ComplyX_Tests.Service;
using System.Linq;

namespace ComplyX_Tests.Repositories.Implementation
{
    public class CompanyRespository : ICompanyRespository
    {

        private readonly Mock<IMapper> _mapper;
        private readonly AppDbContext _appDbContext;

        public CompanyRespository(Mock<IMapper> mapper, AppDbContext appDbContext)
        {

          _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<bool> CompanyExists(int companyId)
        {
            return await _appDbContext.Companiess.AnyAsync(c => c.CompanyId == companyId);
        }

        public async Task<bool> CompanyExistsByNameAsync(string companyName)
        {
            return await  _appDbContext.Companiess.AnyAsync(c => c.Name == companyName);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
  
        public async Task<bool> CompanyExistsByNameAsync( string companyName,bool caseSensitive,int excludedCompanyId = 0)
        {
            return await _appDbContext.Companiess.AnyAsync(c =>
                (caseSensitive
                    ? c.Name.Trim() == companyName.Trim()
                    : c.Name.Trim().ToUpper() == companyName.Trim().ToUpper())
                && c.CompanyId != excludedCompanyId);
        }
       
    }
}