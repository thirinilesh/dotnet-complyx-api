using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComplyX.Common.Data.Context;
using ComplyX.Shared.Data;
using ComplyX_Businesss.Models;
using ComplyX_Tests.Repositories.Interface;
using ComplyX_Tests.Service;
using Microsoft.EntityFrameworkCore;
using static ComplyX_Tests.Repositories.Implementation.EmployeeRespository;

namespace ComplyX_Tests.Repositories.Implementation
{
 
        public class EmployeeRespository : IEmployeeRepository
    {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

            public EmployeeRespository(
                AppDbContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedResponse<Employees>> GetEmployeesAsync(
                Employees request)
            {
                var query = _context.Employees
                    .AsNoTracking()
                    .Where(e => e.EmployeeID == request.EmployeeID);
             

                int totalCount = await query.CountAsync();

            query =    query
                .OrderBy(e => e.EmployeeID)
                .ThenBy(e => e.FirstName);
                    
                    var Employee = await query  
                    .ProjectTo<Employees>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new PaginatedResponse<Employees>
                {
                    Items = Employee,
                    TotalCount = totalCount
                     
                };
            }
        }

   
}
