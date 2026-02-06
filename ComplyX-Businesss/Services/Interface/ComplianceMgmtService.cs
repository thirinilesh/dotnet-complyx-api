using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.ComplianceDeadline;
using ComplyX_Businesss.Models.ComplianceFiling;
using ComplyX_Businesss.Models.ComplianceSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface ComplianceMgmtService
    {
        Task<ManagerBaseResponse<bool>> SaveComplianceMgmtData(ComplianceDeadlineRequestModel ComplianceDeadlines);
        Task<ManagerBaseResponse<bool>> RemoveComplianceMgmtData(string DeadlineID);
        Task<ManagerBaseResponse<List<ComplianceDeadlineResponseModel>>> GetAllComplianceMgmtData(string DeadlineID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceDeadlineResponseModel>>> GetComplianceMgmtFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveComplianceSchedulesData(ComplianceScheduleRequestModel ComplianceSchedules);
        Task<ManagerBaseResponse<bool>> RemoveComplianceSchedulesData(string ScheduleID);
        Task<ManagerBaseResponse<List<ComplianceScheduleResponseModel>>> GetAllComplianceSchedulesData(string ScheduleID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceScheduleResponseModel>>> GetComplianceSchedulesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveComplianceFilingsData(ComplianceFilingRequestModel ComplianceFilings);
        Task<ManagerBaseResponse<bool>> RemoveComplianceFilingsData(string FilingID);
        Task<ManagerBaseResponse<List<ComplianceFilingResponseModel>>> GetAllComplianceFilingsData(string FilingID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceFilingResponseModel>>> GetComplianceFilingsFilter(PagedListCriteria PagedListCriteria);
    }
}
