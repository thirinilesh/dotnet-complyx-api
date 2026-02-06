using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.EmploymentTypes;
using ComplyX_Businesss.Models.ExitTypes;
using ComplyX_Businesss.Models.FilingStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface MasterServices
    {
        Task<ManagerBaseResponse<bool>> SaveEmploymentTypesData(EmploymentTypeRequestModel EmploymentTypes);
        Task<ManagerBaseResponse<bool>> RemoveEmploymentTypesData(string EmploymentTypesID);
        Task<ManagerBaseResponse<List<EmploymentTypeResponseModel>>> GetEmploymentTypesData(string EmploymentTypesID);
        Task<ManagerBaseResponse<IEnumerable<EmploymentType>>> GetEmploymentTypesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveExitTypesData(ExitTypeRequestModel ExitTypes);
        Task<ManagerBaseResponse<bool>> RemoveExitTypesData(string ExitTypesID);
        Task<ManagerBaseResponse<List<ExitTypeResponseModel>>> GetExitTypesData(string ExitTypeID);
        Task<ManagerBaseResponse<IEnumerable<ExitType>>> GetExitTypesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveFillingStatusesData(FilingsStatusRequestModel FilingStatuses);
        Task<ManagerBaseResponse<bool>> RemoveFillingStatusesData(string FilingStatusesID);
        Task<ManagerBaseResponse<List<FilingsStatusResponseModel>>> GetFillingStatusesData(string FilingStatusesID);
        Task<ManagerBaseResponse<IEnumerable<FilingStatus>>> GetFillingStatusesFilter(PagedListCriteria PagedListCriteria);
    }
}
