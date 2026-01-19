using ComplyX.Shared.Helper;

using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System.Globalization;


namespace ComplyX_Businesss.Services.Interface
{
    public  interface DocumentService
    {
        Task<ManagerBaseResponse<bool>> SavelegalDocumentData(legalDocument legalDocument, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentData(string document_id);
        Task<ManagerBaseResponse<List<legalDocument>>> GetAlllegalDocumentData(string document_id);
        Task<ManagerBaseResponse<IEnumerable<legalDocument>>> GetAlllegalDocumentFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SavelegalDocumentVersionData(legalDocumentVersion legalDocumentVersion, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentVersionData(string version_id);
        Task<ManagerBaseResponse<List<legalDocumentVersion>>> GetAlllegalDocumentVersionData(string version_id);
        Task<ManagerBaseResponse<IEnumerable<legalDocumentVersion>>> GetAlllegalDocumentVersionFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SavelegalDocumentAcceptanceData(legalDocumentAcceptance legalDocumentAcceptance, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentAcceptanceData(string acceptance_id);
        Task<ManagerBaseResponse<List<legalDocumentAcceptance>>> GetAlllegalDocumentAcceptanceData(string acceptance_id);
        Task<ManagerBaseResponse<IEnumerable<legalDocumentAcceptance>>> GetAlllegalDocumentAcceptanceFilter(PagedListCriteria PagedListCriteria);
    }
}
