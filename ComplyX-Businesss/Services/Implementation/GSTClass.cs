
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;
using Elasticsearch.Net;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.GSTHSNSAC;
using ComplyX.Data.Entities;
using AutoMapper;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.GSTHSNMapping;
using ComplyX_Businesss.Models.GSTInvoiceSeries;
using ComplyX_Businesss.Models.GSTPurchase;
using ComplyX_Businesss.Models.GSTReturns;
using ComplyX_Businesss.Models.GSTSales;
using ComplyX_Businesss.Models.CompanyEPFO;

namespace ComplyX_Businesss.Services.Implementation
{
    public class GSTClass : IGSTServices
    {
        private readonly AppContext _context;
        private readonly Nest.Filter _filter;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;


        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        public GSTClass(AppContext context, Nest.Filter filter, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _filter = filter;
            _mapper = mapper;
            _UnitOfWork = unitOfWork;
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_HSNSACData(GSTHSNSACRequestModel GST_HSNSAC)
        {
            var response = new ManagerBaseResponse<List<GstHsnsac>>();

            try
            {

                if (GST_HSNSAC.CodeId == 0)
                {
                    // Insert
                    GstHsnsac originalTerm = new GstHsnsac();
                    originalTerm.Code = GST_HSNSAC.Code;
                    originalTerm.CodeType = GST_HSNSAC.CodeType;
                    originalTerm.Description = GST_HSNSAC.Description;
                    originalTerm.GstRate = GST_HSNSAC.GstRate;
                    await _UnitOfWork.GSTHSNSACRespositories.AddAsync(originalTerm);
                }
                else
                {
                    var originalTerm = _UnitOfWork.GSTHSNSACRespositories.GetQueryable()
                       .Where(x => x.CodeId == GST_HSNSAC.CodeId)
                       .FirstOrDefault();
                    originalTerm.Code = GST_HSNSAC.Code;
                    originalTerm.CodeType = GST_HSNSAC.CodeType;
                    originalTerm.Description = GST_HSNSAC.Description;
                    originalTerm.GstRate = GST_HSNSAC.GstRate;
                }
                await _UnitOfWork.CommitAsync();
                 

                return  new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST HSNSAC Details Saved Successfully."
                } ;
            }
            catch (Exception e)
            {
                return  new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                } ;
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_HSNSACData(string CodeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var GST_HSNSAC = await _UnitOfWork.GSTHSNSACRespositories.GetQueryable().Where(x => x.CodeId.ToString() == CodeID).ToListAsync();

                if (string.IsNullOrEmpty(GST_HSNSAC.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST HSNSAC Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.GSTHSNSACRespositories.RemoveRange(GST_HSNSAC);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST HSNSAC Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GSTHSNSACResponseModel>>> GetGST_HSNSACData()
        {
            try
            {
                var plans = await _UnitOfWork.GSTHSNSACRespositories.GetQueryable().OrderBy(x => x.CodeId)
                     .Select(x => new GSTHSNSACResponseModel
                     {
                         CodeId = x.CodeId,
                         CodeType = x.CodeType,
                         Code = x.Code,
                         Description = x.Description,
                         GstRate = x.GstRate
                     }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GSTHSNSACResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST HSNSACData NOT Retrieved.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<GSTHSNSACResponseModel>>
                    {
                        IsSuccess = true,
                        Result = plans,
                        Message = "GST HSNSACData Retrieved Successfully.",
                    };
                }
               
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GSTHSNSACResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GSTHSNSACResponseModel>>> GetGST_HSNSACFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.GSTHSNSACRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Description.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CodeId);
                var responseQuery = query.Select(x => new GSTHSNSACResponseModel
                {

                    CodeId = x.CodeId,
                    CodeType = x.CodeType,
                    Code = x.Code,
                    Description = x.Description,
                    GstRate = x.GstRate
                });
                PageListed<GSTHSNSACResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GSTHSNSACResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST HSNSAC Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GSTHSNSACResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_HSN_MappingData(GSTHSNMappingRequestModel GST_HSN_Mapping)
        {
            var response = new ManagerBaseResponse<List<GstHsnMapping>>();

            try
            {
                var Company = await _UnitOfWork.GSTHSNMappingRespositories.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == GST_HSN_Mapping.CompanyId);

                if (Company == null)
                {
                    return   new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };

                }
                else
                {
                    var code = await _UnitOfWork.GSTHSNSACRespositories.GetQueryable().AnyAsync(x => x.Code == GST_HSN_Mapping.Saccode);
                    if (GST_HSN_Mapping.Saccode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }

                    if (GST_HSN_Mapping.MappingId == 0)
                {
                    // Insert
                    GstHsnMapping originalTerm = new GstHsnMapping();
                        originalTerm.CompanyId = GST_HSN_Mapping.CompanyId;
                        originalTerm.Hsncode=GST_HSN_Mapping.Hsncode;
                        originalTerm.ItemId = GST_HSN_Mapping.ItemId;
                        if(!code && GST_HSN_Mapping.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }                       
                        originalTerm.Saccode = GST_HSN_Mapping.Saccode;
                        originalTerm.GstRate = GST_HSN_Mapping.GstRate;

                   await _UnitOfWork.GSTHSNMappingRespositories.AddAsync(originalTerm);
                }
                else
                {
                    // Update
                    var originalTerm = _UnitOfWork.GSTHSNMappingRespositories.GetQueryable()
                        .Where(x => x.MappingId == GST_HSN_Mapping.MappingId)
                        .FirstOrDefault();
                        originalTerm.CompanyId = GST_HSN_Mapping.CompanyId;
                        originalTerm.Hsncode = GST_HSN_Mapping.Hsncode;
                        originalTerm.ItemId = GST_HSN_Mapping.ItemId;
                        if (!code && GST_HSN_Mapping.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.Saccode = GST_HSN_Mapping.Saccode;
                        originalTerm.GstRate = GST_HSN_Mapping.GstRate;

                      
                }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST_HSN Mappings Details Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_HSN_MappingData(string MappingID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var GST_HSN_Mappings = await _UnitOfWork.GSTHSNMappingRespositories.GetQueryable().Where(x => x.MappingId.ToString() == MappingID).ToListAsync();

                if (string.IsNullOrEmpty(GST_HSN_Mappings.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST_HSN Mappings Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.GSTHSNMappingRespositories.RemoveRange(GST_HSN_Mappings);
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST_HSN Mappings Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GSTHSNMappingResponseModel>>> GetGST_HSN_MappingData()
        {
            try
            {
                var plans = await _UnitOfWork.GSTHSNMappingRespositories.GetQueryable().OrderBy(x => x.MappingId)
                     .Select(x => new GSTHSNMappingResponseModel
                     {
                         MappingId = x.MappingId,
                         CompanyId = x.CompanyId,
                         ItemId = x.ItemId,
                         Hsncode = x.Hsncode,
                         Saccode = x.Saccode,
                         GstRate = x.GstRate
                     }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GSTHSNMappingResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST HSN Mapping NOT Retrieved.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<GSTHSNMappingResponseModel>>
                    {
                        IsSuccess = true,
                        Result = plans,
                        Message = "GST HSN Mapping Retrieved Successfully.",
                    };
                }
              
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GSTHSNMappingResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GSTHSNMappingResponseModel>>> GetGST_HSN_MappingFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.GSTHSNMappingRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Hsncode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.MappingId);
                var responseQuery = query.Select(x => new GSTHSNMappingResponseModel
                {

                    MappingId = x.MappingId,
                    CompanyId = x.CompanyId,
                    ItemId = x.ItemId,
                    Hsncode = x.Hsncode,
                    Saccode = x.Saccode,
                    GstRate = x.GstRate
                });
                PageListed<GSTHSNMappingResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GSTHSNMappingResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST HSN Mapping Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GSTHSNMappingResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_InvoiceSeriesData(GstInvoiceSeriesRequestModel GST_InvoiceSeries)
        {
            var response = new ManagerBaseResponse<List<GstInvoiceSeries>>();

            try
            {
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == GST_InvoiceSeries.CompanyId);

                if (Company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };

                }
                else
                {
                    if (GST_InvoiceSeries.SeriesId == 0)
                    {
                        // Insert
                        GstInvoiceSeries originalTerm = new GstInvoiceSeries();
                        originalTerm.CompanyId = GST_InvoiceSeries.CompanyId;
                        originalTerm.FinancialYear = GST_InvoiceSeries.FinancialYear;
                        originalTerm.Prefix = GST_InvoiceSeries.Prefix;
                        originalTerm.CurrentNumber = GST_InvoiceSeries.CurrentNumber;
                        originalTerm.Suffix = GST_InvoiceSeries.Suffix;
                        originalTerm.LastUpdated = GST_InvoiceSeries.LastUpdated;

                        await _UnitOfWork.InvoiceSeriesRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.InvoiceSeriesRespositories.GetQueryable()
                            .Where(x => x.SeriesId == GST_InvoiceSeries.SeriesId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = GST_InvoiceSeries.CompanyId;
                        originalTerm.CurrentNumber = GST_InvoiceSeries.CurrentNumber;
                        originalTerm.Prefix = GST_InvoiceSeries.Prefix;
                        originalTerm.CurrentNumber = GST_InvoiceSeries.CurrentNumber;
                        originalTerm.Suffix = GST_InvoiceSeries.Suffix;
                        originalTerm.LastUpdated = GST_InvoiceSeries.LastUpdated;

                       
                    }
                    await _UnitOfWork.CommitAsync();
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true, 
                        IsSuccess = true,
                        Message = "GST InvoiceSeries Details Saved Successfully."
                    };
                }
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_InvoiceSeriesData(string SeriesID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var GST_Invoic = await _UnitOfWork.InvoiceSeriesRespositories.GetQueryable().Where(x => x.SeriesId.ToString() == SeriesID).ToListAsync();

                if (string.IsNullOrEmpty(GST_Invoic.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.InvoiceSeriesRespositories.RemoveRange(GST_Invoic);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST INvoice Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GstInvoiceSeriesResponseModel>>> GetGST_InvoiceSeriesData()
        {
            try
            {
                var plans = await _UnitOfWork.InvoiceSeriesRespositories.GetQueryable().OrderBy(x => x.SeriesId)
                     .Select(x => new GstInvoiceSeriesResponseModel
                     {
                         SeriesId = x.SeriesId,
                         CompanyId = x.CompanyId,
                         FinancialYear = x.FinancialYear,
                         Prefix = x.Prefix,
                         CurrentNumber = x.CurrentNumber,
                         Suffix = x.Suffix,
                         LastUpdated = x.LastUpdated
                     }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GstInvoiceSeriesResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST InvoiceSeries Data NOT Retrieved.",
                    };
                }
                else
                {

               
                return new ManagerBaseResponse<List<GstInvoiceSeriesResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST InvoiceSeries Data Retrieved Successfully.",
                };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GstInvoiceSeriesResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GstInvoiceSeriesResponseModel>>> GetGST_InvoiceSeriesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.InvoiceSeriesRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FinancialYear.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SeriesId);
                var responseQuery = query.Select(x => new GstInvoiceSeriesResponseModel
                {

                    SeriesId = x.SeriesId,
                    CompanyId = x.CompanyId,
                    FinancialYear = x.FinancialYear,
                    Prefix = x.Prefix,
                    CurrentNumber = x.CurrentNumber,
                    Suffix = x.Suffix,
                    LastUpdated = x.LastUpdated
                });
                PageListed<GstInvoiceSeriesResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GstInvoiceSeriesResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST InvoiceSeries Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GstInvoiceSeriesResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_PurchaseData(GstPurchaseRequestModel GST_Purchase)
        {
            var response = new ManagerBaseResponse<List<GstPurchase>>();

            try
            {
                var Company =await _UnitOfWork.PurchaseRespositories.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == GST_Purchase.CompanyId);
                 if (Company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };

                }
                else
                {
                    var code = await _UnitOfWork.GSTHSNSACRespositories.GetQueryable().AnyAsync(x => x.Code == GST_Purchase.Saccode);
                    if (GST_Purchase.Saccode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }
                    if (GST_Purchase.PurchaseId == 0)
                    {
                        // Insert
                        GstPurchase originalTerm = new GstPurchase();
                        originalTerm.CompanyId = GST_Purchase.CompanyId;
                       originalTerm.Hsncode = GST_Purchase.Hsncode;
                        originalTerm.BillNo = GST_Purchase.BillNo;
                        originalTerm.BillDate = GST_Purchase.BillDate;
                        originalTerm.SupplierName = GST_Purchase.SupplierName;
                        originalTerm.SupplierGstin    = GST_Purchase.SupplierGstin;

                        if (!code && GST_Purchase.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.Saccode = GST_Purchase.Saccode;
                        originalTerm.TaxableValue = GST_Purchase.TaxableValue;
                        originalTerm.Cgst = GST_Purchase.Cgst;
                        originalTerm.Sgst = GST_Purchase.Sgst;
                        originalTerm.Igst = GST_Purchase.Igst;
                        originalTerm.PlaceOfSupply = GST_Purchase.PlaceOfSupply;
                        originalTerm.TotalBillValue = GST_Purchase.TotalBillValue;
                        originalTerm.CreatedOn = GST_Purchase.CreatedOn;
                        originalTerm.CreatedBy = GST_Purchase.CreatedBy;

                       await _UnitOfWork.PurchaseRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.PurchaseRespositories.GetQueryable()
                            .Where(x => x.PurchaseId == GST_Purchase.PurchaseId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = GST_Purchase.CompanyId;
                        originalTerm.Hsncode = GST_Purchase.Hsncode;
                        originalTerm.BillNo = GST_Purchase.BillNo;
                        originalTerm.BillDate = GST_Purchase.BillDate;
                        originalTerm.SupplierName = GST_Purchase.SupplierName;
                        originalTerm.SupplierGstin = GST_Purchase.SupplierGstin;
                        if (!code && GST_Purchase.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.Saccode = GST_Purchase.Saccode;
                        originalTerm.TaxableValue = GST_Purchase.TaxableValue;
                        originalTerm.Cgst = GST_Purchase.Cgst;
                        originalTerm.Sgst = GST_Purchase.Sgst;
                        originalTerm.Igst = GST_Purchase.Igst;
                        originalTerm.PlaceOfSupply = GST_Purchase.PlaceOfSupply;
                        originalTerm.TotalBillValue = GST_Purchase.TotalBillValue;
                        originalTerm.CreatedOn = GST_Purchase.CreatedOn;
                        originalTerm.CreatedBy = GST_Purchase.CreatedBy;

                      
                    }
                }
                 await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Purchase Details Saved Successfully."
                } ;
            }
            catch (Exception e)
            {
                return  new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                } ;
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_PurchaseData(string PurchaseID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Purchase = await _UnitOfWork.PurchaseRespositories.GetQueryable().Where(x => x.PurchaseId.ToString() == PurchaseID).ToListAsync();

                if (string.IsNullOrEmpty(PurchaseID.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PurchaseRespositories.RemoveRange(Purchase);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Purchase Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GstPurchaseResponseModel>>> GetGST_PurchaseData()
        {
            try
            {
                var plans = await _UnitOfWork.PurchaseRespositories.GetQueryable().OrderBy(x => x.PurchaseId)
                   .Select(x => new GstPurchaseResponseModel
                   {
                       PurchaseId = x.PurchaseId,
                       CompanyId = x.CompanyId,
                       BillNo = x.BillNo,
                       BillDate = x.BillDate,
                       SupplierName = x.SupplierName,
                       SupplierGstin = x.SupplierGstin,
                       PlaceOfSupply = x.PlaceOfSupply,
                       Hsncode = x.Hsncode,
                       Saccode = x.Saccode,
                       TaxableValue = x.TaxableValue,
                       Cgst = x.Cgst,
                       Sgst = x.Sgst,
                       Igst = x.Igst,
                       TotalBillValue = x.TotalBillValue,
                       CreatedOn = x.CreatedOn,
                       CreatedBy = x.CreatedBy
                   }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GstPurchaseResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST Purchase Data NOT Retrieved.",
                    };
                }
                else
                {

               
                return new ManagerBaseResponse<List<GstPurchaseResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Purchase Data Retrieved Successfully.",
                };
                     }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GstPurchaseResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GstPurchaseResponseModel>>> GetGST_PurchaseFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.PurchaseRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.SupplierName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PurchaseId);
                var responseQuery = query.Select(x => new GstPurchaseResponseModel
                {

                    PurchaseId = x.PurchaseId,
                    CompanyId = x.CompanyId,
                    BillNo = x.BillNo,
                    BillDate = x.BillDate,
                    SupplierName = x.SupplierName,
                    SupplierGstin = x.SupplierGstin,
                    PlaceOfSupply = x.PlaceOfSupply,
                    Hsncode = x.Hsncode,
                    Saccode = x.Saccode,
                    TaxableValue = x.TaxableValue,
                    Cgst = x.Cgst,
                    Sgst = x.Sgst,
                    Igst = x.Igst,
                    TotalBillValue = x.TotalBillValue,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy
                });
                PageListed<GstPurchaseResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GstPurchaseResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST Purchase Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GstPurchaseResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_ReturnsData(GstReturnRequestModel GST_Returns)
        {
            var response = new ManagerBaseResponse<List<GstReturn>>();

            try
            {
                var Company = await _UnitOfWork.ReturnRespositories.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == GST_Returns.CompanyId);
                if (Company == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };

                }
                else
                {
                    if (GST_Returns.ReturnId == 0)
                    {
                        // Insert
                        GstReturn originalTerm = new GstReturn();
                        originalTerm.CompanyId = GST_Returns.CompanyId;
                        originalTerm.ReturnType = GST_Returns.ReturnType;
                        originalTerm.PeriodMonth = GST_Returns.PeriodMonth;
                        originalTerm.PeriodYear = GST_Returns.PeriodYear;
                        originalTerm.TotalPurchases = GST_Returns.TotalPurchases;
                        originalTerm.TotalTaxPayable = GST_Returns.TotalTaxPayable;
                        originalTerm.TotalTaxPaid = GST_Returns.TotalTaxPaid;
                        originalTerm.FilingDate = GST_Returns.FilingDate;
                        originalTerm.Status = GST_Returns.Status;
                        originalTerm.CreatedOn = GST_Returns.CreatedOn;

                       await _UnitOfWork.ReturnRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.ReturnRespositories.GetQueryable()
                            .Where(x => x.ReturnId == GST_Returns.ReturnId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = GST_Returns.CompanyId;
                        originalTerm.ReturnType = GST_Returns.ReturnType;
                        originalTerm.PeriodMonth = GST_Returns.PeriodMonth;
                        originalTerm.PeriodYear = GST_Returns.PeriodYear;
                        originalTerm.TotalPurchases = GST_Returns.TotalPurchases;
                        originalTerm.TotalTaxPayable = GST_Returns.TotalTaxPayable;
                        originalTerm.TotalTaxPaid = GST_Returns.TotalTaxPaid;
                        originalTerm.FilingDate = GST_Returns.FilingDate;
                        originalTerm.Status = GST_Returns.Status;
                        originalTerm.CreatedOn = GST_Returns.CreatedOn;

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Return Details Saved Successfully."

                };
             }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_ReturnsData(string ReturnID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Return = await _UnitOfWork.ReturnRespositories.GetQueryable().Where(x => x.ReturnId.ToString() == ReturnID).ToListAsync();

                if (string.IsNullOrEmpty(Return.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.ReturnRespositories.RemoveRange(Return);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Return Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GstReturnResponseModel>>> GetGST_ReturnsData()
        {
            try
            {
                var plans = await _UnitOfWork.ReturnRespositories.GetQueryable().OrderBy(x => x.ReturnId)
                      .Select(x => new GstReturnResponseModel
                      {
                          ReturnId = x.ReturnId,
                          CompanyId = x.CompanyId,
                          ReturnType = x.ReturnType,
                          PeriodMonth = x.PeriodMonth,
                          PeriodYear = x.PeriodYear,
                          TotalSales = x.TotalSales,
                          TotalPurchases = x.TotalPurchases,
                          TotalTaxPayable = x.TotalTaxPayable,
                          TotalTaxPaid = x.TotalTaxPaid,
                          FilingDate = x.FilingDate,
                          Status = x.Status,
                          CreatedOn = x.CreatedOn
                      }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GstReturnResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST Returns Data not Retrieved.",
                    };
                }
                else
                {

               
                return new ManagerBaseResponse<List<GstReturnResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Returns Data Retrieved Successfully.",
                };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GstReturnResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GstReturnResponseModel>>> GetGST_ReturnsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.ReturnRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ReturnType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ReturnId);
                var responseQuery = query.Select(x => new GstReturnResponseModel
                {

                    ReturnId = x.ReturnId,
                    CompanyId = x.CompanyId,
                    ReturnType = x.ReturnType,
                    PeriodMonth = x.PeriodMonth,
                    PeriodYear = x.PeriodYear,
                    TotalSales = x.TotalSales,
                    TotalPurchases = x.TotalPurchases,
                    TotalTaxPayable = x.TotalTaxPayable,
                    TotalTaxPaid = x.TotalTaxPaid,
                    FilingDate = x.FilingDate,
                    Status = x.Status,
                    CreatedOn = x.CreatedOn
                });
                PageListed<GstReturnResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GstReturnResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST Returns Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GstReturnResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_SalesData(GstSaleRequestModel GST_Sales)
        {
            var response = new ManagerBaseResponse<List<GstSale>>();

            try
            {
                var Company =await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == GST_Sales.CompanyId);
                

                if (Company == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    } ;

                }
                else
                {
                    var code = await _UnitOfWork.GSTHSNSACRespositories.GetQueryable().AnyAsync(x => x.Code == GST_Sales.Saccode);
                    if(GST_Sales.Saccode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }
                    if (GST_Sales.SaleId == 0)
                    {
                        // Insert
                        GstSale originalTerm = new GstSale();
                        originalTerm.CompanyId = GST_Sales.CompanyId;
                        originalTerm.InvoiceNo = GST_Sales.InvoiceNo;
                        originalTerm.InvoiceDate = GST_Sales.InvoiceDate;
                        originalTerm.CustomerName = GST_Sales.CustomerName;
                        originalTerm.CustomerGstin =  GST_Sales.CustomerGstin;
                        originalTerm.PlaceOfSupply = GST_Sales.PlaceOfSupply;
                        originalTerm.Hsncode = GST_Sales.Hsncode;
                        if (!code && GST_Sales.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.Saccode = GST_Sales.Saccode;
                        originalTerm.TaxableValue = GST_Sales.TaxableValue;
                        originalTerm.Cgst = GST_Sales.Cgst;
                        originalTerm.Sgst = GST_Sales.Sgst;
                        originalTerm.Igst = GST_Sales.Igst;
                        originalTerm.TotalInvoiceValue = GST_Sales.TotalInvoiceValue;
                        originalTerm.CreatedOn = GST_Sales.CreatedOn;
                        originalTerm.CreatedBy = GST_Sales.CreatedBy;

                       await _UnitOfWork.GgstSaleRespositories.AddAsync(originalTerm);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.GgstSaleRespositories.GetQueryable()
                            .Where(x => x.SaleId == GST_Sales.SaleId)
                            .FirstOrDefault();
                        originalTerm.CompanyId = GST_Sales.CompanyId;
                        originalTerm.InvoiceNo = GST_Sales.InvoiceNo;
                        originalTerm.InvoiceDate = GST_Sales.InvoiceDate;
                        originalTerm.CustomerName = GST_Sales.CustomerName;
                        originalTerm.CustomerGstin = GST_Sales.CustomerGstin;
                        originalTerm.PlaceOfSupply = GST_Sales.PlaceOfSupply;
                        originalTerm.Hsncode = GST_Sales.Hsncode;
                        if (!code && GST_Sales.Saccode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.Saccode =  GST_Sales.Saccode;
                        originalTerm.TaxableValue = GST_Sales.TaxableValue;
                        originalTerm.Cgst = GST_Sales.Cgst;
                        originalTerm.Sgst = GST_Sales.Sgst;
                        originalTerm.Igst = GST_Sales.Igst;
                        originalTerm.TotalInvoiceValue = GST_Sales.TotalInvoiceValue;
                        originalTerm.CreatedOn = GST_Sales.CreatedOn;
                        originalTerm.CreatedBy = GST_Sales.CreatedBy;

                         
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Sales Details Saved Successfully."
                };
            }
            catch (Exception e)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_SalesData(string SaleID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Sales = await _UnitOfWork.GgstSaleRespositories.GetQueryable().Where(x => x.SaleId.ToString() == SaleID).ToListAsync();

                if (string.IsNullOrEmpty(Sales.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Sales Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.GgstSaleRespositories.RemoveRange(Sales);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true, 
                    IsSuccess = true,
                    Message = "GST Sales Data Removed Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<GstSaleResponseModel>>> GetGST_SalesData()
        {
            try
            {
                var plans = await _UnitOfWork.GgstSaleRespositories.GetQueryable().OrderBy(x => x.SaleId)
                     .Select(x => new GstSaleResponseModel
                     {
                         SaleId = x.SaleId,
                         CompanyId = x.CompanyId,
                         InvoiceNo = x.InvoiceNo,
                         InvoiceDate = x.InvoiceDate,
                         CustomerName = x.CustomerName,
                         CustomerGstin = x.CustomerGstin,
                         PlaceOfSupply = x.PlaceOfSupply,
                         Hsncode = x.Hsncode,
                         Saccode = x.Saccode,
                         TaxableValue = x.TaxableValue,
                         Cgst = x.Cgst,
                         Sgst = x.Sgst,
                         Igst = x.Igst,
                         TotalInvoiceValue = x.TotalInvoiceValue,
                         CreatedOn = x.CreatedOn,
                         CreatedBy = x.CreatedBy
                     }).ToListAsync();

                if(plans.Count == 0)
                {
                    return new ManagerBaseResponse<List<GstSaleResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "GST Sales Data not Retrieved .",
                    };
                }
                else
                {

                
                return new ManagerBaseResponse<List<GstSaleResponseModel>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Sales Data Retrieved Successfully.",
                };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GstSaleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GstSaleResponseModel>>> GetGST_SalesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.GgstSaleRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.CustomerName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SaleId);
                var responseQuery = query.Select(x => new GstSaleResponseModel
                {

                    SaleId = x.SaleId,
                    CompanyId = x.CompanyId,
                    InvoiceNo = x.InvoiceNo,
                    InvoiceDate = x.InvoiceDate,
                    CustomerName = x.CustomerName,
                    CustomerGstin = x.CustomerGstin,
                    PlaceOfSupply = x.PlaceOfSupply,
                    Hsncode = x.Hsncode,
                    Saccode = x.Saccode,
                    TaxableValue = x.TaxableValue,
                    Cgst = x.Cgst,
                    Sgst = x.Sgst,
                    Igst = x.Igst,
                    TotalInvoiceValue = x.TotalInvoiceValue,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy
                });
                PageListed<GstSaleResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GstSaleResponseModel>>
                {
                    Result = result.Data, 
                    IsSuccess = true,
                    Message = "GST Sales Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters,
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<GstSaleResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
