using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface ComplianceMgmtService
    {
        Task<ManagerBaseResponse<bool>> SaveComplianceMgmtData(ComplianceDeadlines ComplianceDeadlines);
        Task<ManagerBaseResponse<bool>> RemoveComplianceMgmtData(string DeadlineID);
        Task<ManagerBaseResponse<List<ComplianceDeadlines>>> GetAllComplianceMgmtData(string DeadlineID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceDeadlines>>> GetComplianceMgmtFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveComplianceSchedulesData(ComplianceSchedules ComplianceSchedules);
        Task<ManagerBaseResponse<bool>> RemoveComplianceSchedulesData(string ScheduleID);
        Task<ManagerBaseResponse<List<ComplianceSchedules>>> GetAllComplianceSchedulesData(string ScheduleID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceSchedules>>> GetComplianceSchedulesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveComplianceFilingsData(ComplianceFilings ComplianceFilings);
        Task<ManagerBaseResponse<bool>> RemoveComplianceFilingsData(string FilingID);
        Task<ManagerBaseResponse<List<ComplianceFilings>>> GetAllComplianceFilingsData(string FilingID);
        Task<ManagerBaseResponse<IEnumerable<ComplianceFilings>>> GetComplianceFilingsFilter(PagedListCriteria PagedListCriteria);
    }
}
