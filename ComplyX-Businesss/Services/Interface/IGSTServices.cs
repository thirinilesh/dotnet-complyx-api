using ComplyX.Data.Entities;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX_Businesss.Models.GSTHSNMapping;
using ComplyX_Businesss.Models.GSTHSNSAC;
using ComplyX_Businesss.Models.GSTInvoiceSeries;
using ComplyX_Businesss.Models.GSTPurchase;
using ComplyX_Businesss.Models.GSTReturns;
using ComplyX_Businesss.Models.GSTSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Services.Interface
{
    public interface IGSTServices
    {
        Task<ManagerBaseResponse<bool>> SaveGST_HSNSACData(GSTHSNSACRequestModel GST_HSNSAC);
        Task<ManagerBaseResponse<bool>> RemoveGST_HSNSACData(string CodeID);
        Task<ManagerBaseResponse<List<GSTHSNSACResponseModel>>> GetGST_HSNSACData();
        Task<ManagerBaseResponse<IEnumerable<GstHsnsac>>> GetGST_HSNSACFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_HSN_MappingData(GSTHSNMappingRequestModel GST_HSN_Mapping);
        Task<ManagerBaseResponse<bool>> RemoveGST_HSN_MappingData(string MappingID);
        Task<ManagerBaseResponse<List<GSTHSNMappingResponseModel>>> GetGST_HSN_MappingData();
        Task<ManagerBaseResponse<IEnumerable<GstHsnMapping>>> GetGST_HSN_MappingFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_InvoiceSeriesData(GstInvoiceSeriesRequestModel GST_InvoiceSeries);
        Task<ManagerBaseResponse<bool>> RemoveGST_InvoiceSeriesData(string SeriesID);
        Task<ManagerBaseResponse<List<GstInvoiceSeriesResponseModel>>> GetGST_InvoiceSeriesData();
        Task<ManagerBaseResponse<IEnumerable<GstInvoiceSeries>>> GetGST_InvoiceSeriesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_PurchaseData(GstPurchaseRequestModel GST_Purchase);
        Task<ManagerBaseResponse<bool>> RemoveGST_PurchaseData(string PurchaseID);
        Task<ManagerBaseResponse<List<GstPurchaseResponseModel>>> GetGST_PurchaseData();
        Task<ManagerBaseResponse<IEnumerable<GstPurchase>>> GetGST_PurchaseFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_ReturnsData(GstReturnRequestModel GST_Returns);
        Task<ManagerBaseResponse<bool>> RemoveGST_ReturnsData(string ReturnID);
        Task<ManagerBaseResponse<List<GstReturnResponseModel>>> GetGST_ReturnsData();
        Task<ManagerBaseResponse<IEnumerable<GstReturn>>> GetGST_ReturnsFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveGST_SalesData(GstSaleRequestModel GST_Sales);
        Task<ManagerBaseResponse<bool>> RemoveGST_SalesData(string SaleID);
        Task<ManagerBaseResponse<List<GstSaleResponseModel>>> GetGST_SalesData();
        Task<ManagerBaseResponse<IEnumerable<GstSale>>> GetGST_SalesFilter(PagedListCriteria PagedListCriteria);
    }
}