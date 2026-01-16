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
    }
}
