using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface MasterServices
    {
        Task<ManagerBaseResponse<bool>> SaveEmploymentTypesData(EmploymentTypes EmploymentTypes);
        Task<ManagerBaseResponse<bool>> RemoveEmploymentTypesData(string EmploymentTypesID);
        Task<ManagerBaseResponse<List<EmploymentTypes>>> GetEmploymentTypesData(string EmploymentTypesID);
        Task<ManagerBaseResponse<IEnumerable<EmploymentTypes>>> GetEmploymentTypesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveExitTypesData(ExitTypes ExitTypes);
        Task<ManagerBaseResponse<bool>> RemoveExitTypesData(string ExitTypesID);
        Task<ManagerBaseResponse<List<ExitTypes>>> GetExitTypesData(string ExitTypeID);
        Task<ManagerBaseResponse<IEnumerable<ExitTypes>>> GetExitTypesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveFillingStatusesData(FilingStatuses FilingStatuses);
        Task<ManagerBaseResponse<bool>> RemoveFillingStatusesData(string FilingStatusesID);
        Task<ManagerBaseResponse<List<FilingStatuses>>> GetFillingStatusesData(string FilingStatusesID);
        Task<ManagerBaseResponse<IEnumerable<FilingStatuses>>> GetFillingStatusesFilter(PagedListCriteria PagedListCriteria);
    }
}
