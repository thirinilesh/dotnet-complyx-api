using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;

using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.LegalDocument;
using ComplyX_Businesss.Models.LegalDocumentAcceptance;
using ComplyX_Businesss.Models.LegalDocumentVersion;
using System.Globalization;


namespace ComplyX_Businesss.Services.Interface
{
    public  interface DocumentService
    {
        Task<ManagerBaseResponse<bool>> SavelegalDocumentData(LegalDocumentRequestModel legalDocument, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentData(string document_id);
        Task<ManagerBaseResponse<List<LegalDocumentResponseModel>>> GetAlllegalDocumentData(string document_id);
        Task<ManagerBaseResponse<IEnumerable<LegalDocumentResponseModel>>> GetAlllegalDocumentFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SavelegalDocumentVersionData(LegalDocumentVersionRequestModel legalDocumentVersion, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentVersionData(string version_id);
        Task<ManagerBaseResponse<List<LegalDocumentVersionResponseModel>>> GetAlllegalDocumentVersionData(string version_id);
        Task<ManagerBaseResponse<IEnumerable<LegalDocumentVersionResponseModel>>> GetAlllegalDocumentVersionFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SavelegalDocumentAcceptanceData(LegalDocumentAcceptanceRequestModel legalDocumentAcceptance, string UserName);
        Task<ManagerBaseResponse<bool>> RemovelegalDocumentAcceptanceData(string acceptance_id);
        Task<ManagerBaseResponse<List<LegalDocumentAcceptanceResponseModel>>> GetAlllegalDocumentAcceptanceData(string acceptance_id);
        Task<ManagerBaseResponse<IEnumerable<LegalDocumentAcceptanceResponseModel>>> GetAlllegalDocumentAcceptanceFilter(PagedListCriteria PagedListCriteria);
    }
}
