using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface IGSTServices
    {
        Task<ManagerBaseResponse<bool>> SaveGST_HSNSACData(GST_HSNSAC GST_HSNSAC);
        Task<ManagerBaseResponse<bool>> RemoveGST_HSNSACData(string CodeID);
        Task<ManagerBaseResponse<List<GST_HSNSAC>>> GetGST_HSNSACData();
        Task<ManagerBaseResponse<IEnumerable<GST_HSNSAC>>> GetGST_HSNSACFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_HSN_MappingData(GST_HSN_Mapping GST_HSN_Mapping);
        Task<ManagerBaseResponse<bool>> RemoveGST_HSN_MappingData(string MappingID);
        Task<ManagerBaseResponse<List<GST_HSN_Mapping>>> GetGST_HSN_MappingData();
        Task<ManagerBaseResponse<IEnumerable<GST_HSN_Mapping>>> GetGST_HSN_MappingFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_InvoiceSeriesData(GST_InvoiceSeries GST_InvoiceSeries);
        Task<ManagerBaseResponse<bool>> RemoveGST_InvoiceSeriesData(string SeriesID);
        Task<ManagerBaseResponse<List<GST_InvoiceSeries>>> GetGST_InvoiceSeriesData();
        Task<ManagerBaseResponse<IEnumerable<GST_InvoiceSeries>>> GetGST_InvoiceSeriesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_PurchaseData(GST_Purchase GST_Purchase);
        Task<ManagerBaseResponse<bool>> RemoveGST_PurchaseData(string PurchaseID);
        Task<ManagerBaseResponse<List<GST_Purchase>>> GetGST_PurchaseData();
        Task<ManagerBaseResponse<IEnumerable<GST_Purchase>>> GetGST_PurchaseFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_ReturnsData(GST_Returns GST_Returns);
        Task<ManagerBaseResponse<bool>> RemoveGST_ReturnsData(string ReturnID);
        Task<ManagerBaseResponse<List<GST_Returns>>> GetGST_ReturnsData();
        Task<ManagerBaseResponse<IEnumerable<GST_Returns>>> GetGST_ReturnsFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_SalesData(GST_Sales GST_Sales);
        Task<ManagerBaseResponse<bool>> RemoveGST_SalesData(string SaleID);
        Task<ManagerBaseResponse<List<GST_Sales>>> GetGST_SalesData();
        Task<ManagerBaseResponse<IEnumerable<GST_Sales>>> GetGST_SalesFilter(PagedListCriteria PagedListCriteria);
    }
}
