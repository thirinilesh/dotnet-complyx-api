
using ComplyX_Businesss.Services.Interface;
using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using ComplyX_Businesss.Helper;
using Elasticsearch.Net;
using AppContext = ComplyX_Businesss.Helper.AppContext;

namespace ComplyX_Businesss.Services.Implementation
{
    public class GSTClass : IGSTServices
    {
        private readonly AppContext _context;
        private readonly Nest.Filter _filter;


        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        public GSTClass(AppContext context, Nest.Filter filter)
        {
            _context = context;
            _filter = filter;

        }
        public Task<ManagerBaseResponse<bool>> SaveGST_HSNSACData(GST_HSNSAC GST_HSNSAC)
        {
            var response = new ManagerBaseResponse<List<GST_HSNSAC>>();

            try
            {
                if (GST_HSNSAC.CodeID == 0)
                {
                    // Insert
                    GST_HSNSAC originalTerm = new GST_HSNSAC();
                   originalTerm.CodeType = GST_HSNSAC.CodeType;
                    originalTerm.Code = GST_HSNSAC.Code;
                    originalTerm.Description = GST_HSNSAC.Description;
                    originalTerm.GST_Rate =   GST_HSNSAC.GST_Rate;

                    _context.Add(originalTerm);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.GST_HSNSAC
                        .Where(x => x.CodeID == GST_HSNSAC.CodeID)
                        .FirstOrDefault();
                    originalTerm.CodeType = GST_HSNSAC.CodeType;
                    originalTerm.Code = GST_HSNSAC.Code;
                    originalTerm.Description = GST_HSNSAC.Description;
                    originalTerm.GST_Rate = GST_HSNSAC.GST_Rate;

                    _context.Update(originalTerm);
                    _context.SaveChanges();
                }

                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "GST HSNSAC Details Saved Successfully."
                });
            }
            catch (Exception e)
            {
                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                });
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveGST_HSNSACData(string CodeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var GST_HSNSAC = await _context.GST_HSNSAC.Where(x => x.CodeID.ToString() == CodeID).ToListAsync();

                if (string.IsNullOrEmpty(GST_HSNSAC.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST HSNSAC Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_HSNSAC.RemoveRange(GST_HSNSAC);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_HSNSAC>>> GetGST_HSNSACData()
        {
            try
            {
                var plans = await _context.GST_HSNSAC.AsQueryable().OrderBy(x => x.CodeID).ToListAsync();

                return new ManagerBaseResponse<List<GST_HSNSAC>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST HSNSACData Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_HSNSAC>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_HSNSAC>>> GetGST_HSNSACFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_HSNSAC.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Description.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.CodeID);

                PageListed<GST_HSNSAC> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_HSNSAC>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_HSNSAC>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_HSN_MappingData(GST_HSN_Mapping GST_HSN_Mapping)
        {
            var response = new ManagerBaseResponse<List<GST_HSN_Mapping>>();

            try
            {
                var Company = await  _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == GST_HSN_Mapping.CompanyID);

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
                    var code = await _context.GST_HSNSAC.AnyAsync(x => x.Code == GST_HSN_Mapping.SACCode);
                    if (GST_HSN_Mapping.SACCode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }

                    if (GST_HSN_Mapping.MappingID == 0)
                {
                    // Insert
                    GST_HSN_Mapping originalTerm = new GST_HSN_Mapping();
                        originalTerm.CompanyID = GST_HSN_Mapping.CompanyID;
                        originalTerm.HSNCode=GST_HSN_Mapping.HSNCode;
                        originalTerm.ItemID = GST_HSN_Mapping.ItemID;
                        if(!code && GST_HSN_Mapping.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }                       
                        originalTerm.SACCode = GST_HSN_Mapping.SACCode;
                        originalTerm.GST_Rate = GST_HSN_Mapping.GST_Rate;

                    _context.Add(originalTerm);
                    _context.SaveChanges();
                }
                else
                {
                    // Update
                    var originalTerm = _context.GST_HSN_Mapping
                        .Where(x => x.MappingID == GST_HSN_Mapping.MappingID)
                        .FirstOrDefault();
                        originalTerm.CompanyID = GST_HSN_Mapping.CompanyID;
                        originalTerm.HSNCode = GST_HSN_Mapping.HSNCode;
                        originalTerm.ItemID = GST_HSN_Mapping.ItemID;
                        if (!code && GST_HSN_Mapping.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.SACCode = GST_HSN_Mapping.SACCode;
                        originalTerm.GST_Rate = GST_HSN_Mapping.GST_Rate;

                        _context.Update(originalTerm);
                    _context.SaveChanges();
                }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
                var GST_HSN_Mappings = await _context.GST_HSN_Mapping.Where(x => x.MappingID.ToString() == MappingID).ToListAsync();

                if (string.IsNullOrEmpty(GST_HSN_Mappings.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST_HSN Mappings Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_HSN_Mapping.RemoveRange(GST_HSN_Mappings);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_HSN_Mapping>>> GetGST_HSN_MappingData()
        {
            try
            {
                var plans = await _context.GST_HSN_Mapping.AsQueryable().OrderBy(x => x.MappingID).ToListAsync();

                return new ManagerBaseResponse<List<GST_HSN_Mapping>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST HSN Mapping Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_HSN_Mapping>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_HSN_Mapping>>> GetGST_HSN_MappingFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_HSN_Mapping.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.HSNCode.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.MappingID);

                PageListed<GST_HSN_Mapping> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_HSN_Mapping>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_HSN_Mapping>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_InvoiceSeriesData(GST_InvoiceSeries GST_InvoiceSeries)
        {
            var response = new ManagerBaseResponse<List<GST_InvoiceSeries>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == GST_InvoiceSeries.CompanyID);

                if (Company == null)
                {
                    return  new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company is not found.",
                    };

                }
                else
                {
                    if (GST_InvoiceSeries.SeriesID == 0)
                    {
                        // Insert
                        GST_InvoiceSeries originalTerm = new GST_InvoiceSeries();
                        originalTerm.CompanyID = GST_InvoiceSeries.CompanyID;
                        originalTerm.FinancialYear = GST_InvoiceSeries.FinancialYear;
                        originalTerm.Prefix = GST_InvoiceSeries.Prefix;
                        originalTerm.CurrentNumber = GST_InvoiceSeries.CurrentNumber;
                        originalTerm.Suffix = GST_InvoiceSeries.Suffix;
                        originalTerm.LastUpdated = GST_InvoiceSeries.LastUpdated;

                        _context.Add(originalTerm);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.GST_InvoiceSeries
                            .Where(x => x.SeriesID == GST_InvoiceSeries.SeriesID)
                            .FirstOrDefault();
                        originalTerm.CompanyID =  GST_InvoiceSeries .CompanyID;
                        originalTerm.CurrentNumber = GST_InvoiceSeries .CurrentNumber;
                        originalTerm.Prefix = GST_InvoiceSeries .Prefix;
                        originalTerm.CurrentNumber = GST_InvoiceSeries.CurrentNumber;
                        originalTerm.Suffix = GST_InvoiceSeries .Suffix;
                        originalTerm.LastUpdated = GST_InvoiceSeries .LastUpdated;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "GST InvoiceSeries Details Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveGST_InvoiceSeriesData(string SeriesID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var GST_Invoic = await _context.GST_InvoiceSeries.Where(x => x.SeriesID.ToString() == SeriesID).ToListAsync();

                if (string.IsNullOrEmpty(GST_Invoic.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_InvoiceSeries.RemoveRange(GST_Invoic);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_InvoiceSeries>>> GetGST_InvoiceSeriesData()
        {
            try
            {
                var plans = await _context.GST_InvoiceSeries.AsQueryable().OrderBy(x => x.SeriesID).ToListAsync();

                return new ManagerBaseResponse<List<GST_InvoiceSeries>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST InvoiceSeries Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_InvoiceSeries>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_InvoiceSeries>>> GetGST_InvoiceSeriesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_InvoiceSeries.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FinancialYear.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SeriesID);

                PageListed<GST_InvoiceSeries> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_InvoiceSeries>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_InvoiceSeries>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_PurchaseData(GST_Purchase GST_Purchase)
        {
            var response = new ManagerBaseResponse<List<GST_Purchase>>();

            try
            {
                var Company =await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == GST_Purchase.CompanyID);
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
                    var code = await _context.GST_HSNSAC.AnyAsync(x => x.Code == GST_Purchase.SACCode);
                    if (GST_Purchase.SACCode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }
                    if (GST_Purchase.PurchaseID == 0)
                    {
                        // Insert
                        GST_Purchase originalTerm = new GST_Purchase();
                        originalTerm.CompanyID = GST_Purchase.CompanyID;
                       originalTerm.HSNCode = GST_Purchase.HSNCode;
                        originalTerm.BillNo = GST_Purchase.BillNo;
                        originalTerm.BillDate = GST_Purchase.BillDate;
                        originalTerm.SupplierName = GST_Purchase.SupplierName;
                        originalTerm.SupplierGSTIN    = GST_Purchase.SupplierGSTIN;

                        if (!code && GST_Purchase.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.SACCode = GST_Purchase.SACCode;
                        originalTerm.TaxableValue = GST_Purchase.TaxableValue;
                        originalTerm.CGST = GST_Purchase.CGST;
                        originalTerm.SGST = GST_Purchase.SGST;
                        originalTerm.IGST = GST_Purchase.IGST;
                        originalTerm.PLaceOfSupply = GST_Purchase.PLaceOfSupply;
                        originalTerm.TotalBillValue = GST_Purchase.TotalBillValue;
                        originalTerm.CreatedOn = GST_Purchase.CreatedOn;
                        originalTerm.CreatedBy = GST_Purchase.CreatedBy;

                        _context.Add(originalTerm);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.GST_Purchase
                            .Where(x => x.PurchaseID == GST_Purchase.PurchaseID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = GST_Purchase.CompanyID;
                        originalTerm.HSNCode = GST_Purchase.HSNCode;
                        originalTerm.BillNo = GST_Purchase.BillNo;
                        originalTerm.BillDate = GST_Purchase.BillDate;
                        originalTerm.SupplierName = GST_Purchase.SupplierName;
                        originalTerm.SupplierGSTIN = GST_Purchase.SupplierGSTIN;
                        if (!code && GST_Purchase.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.SACCode = GST_Purchase.SACCode;
                        originalTerm.TaxableValue = GST_Purchase.TaxableValue;
                        originalTerm.CGST = GST_Purchase.CGST;
                        originalTerm.SGST = GST_Purchase.SGST;
                        originalTerm.IGST = GST_Purchase.IGST;
                        originalTerm.PLaceOfSupply = GST_Purchase.PLaceOfSupply;
                        originalTerm.TotalBillValue = GST_Purchase.TotalBillValue;
                        originalTerm.CreatedOn = GST_Purchase.CreatedOn;
                        originalTerm.CreatedBy = GST_Purchase.CreatedBy;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
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
                var Purchase = await _context.GST_Purchase.Where(x => x.PurchaseID.ToString() == PurchaseID).ToListAsync();

                if (string.IsNullOrEmpty(PurchaseID.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_Purchase.RemoveRange(Purchase);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_Purchase>>> GetGST_PurchaseData()
        {
            try
            {
                var plans = await _context.GST_Purchase.AsQueryable().OrderBy(x => x.PurchaseID).ToListAsync();

                return new ManagerBaseResponse<List<GST_Purchase>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Purchase Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_Purchase>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_Purchase>>> GetGST_PurchaseFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_Purchase.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.SupplierName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PurchaseID);

                PageListed<GST_Purchase> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_Purchase>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_Purchase>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_ReturnsData(GST_Returns GST_Returns)
        {
            var response = new ManagerBaseResponse<List<GST_Returns>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == GST_Returns.CompanyID);
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
                    if (GST_Returns.ReturnID == 0)
                    {
                        // Insert
                        GST_Returns originalTerm = new GST_Returns();
                        originalTerm.CompanyID = GST_Returns.CompanyID;
                        originalTerm.ReturnType = GST_Returns.ReturnType;
                        originalTerm.PeriodMonth = GST_Returns.PeriodMonth;
                        originalTerm.PeriodYear = GST_Returns.PeriodYear;
                        originalTerm.TotalPurchases = GST_Returns.TotalPurchases;
                        originalTerm.TotalTaxPayable = GST_Returns.TotalTaxPayable;
                        originalTerm.TotalTaxPaid = GST_Returns.TotalTaxPaid;
                        originalTerm.FilingDate = GST_Returns.FilingDate;
                        originalTerm.Status = GST_Returns.Status;
                        originalTerm.CreatedOn = GST_Returns.CreatedOn;

                        _context.Add(originalTerm);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.GST_Returns
                            .Where(x => x.ReturnID == GST_Returns.ReturnID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = GST_Returns.CompanyID;
                        originalTerm.ReturnType = GST_Returns.ReturnType;
                        originalTerm.PeriodMonth = GST_Returns.PeriodMonth;
                        originalTerm.PeriodYear = GST_Returns.PeriodYear;
                        originalTerm.TotalPurchases = GST_Returns.TotalPurchases;
                        originalTerm.TotalTaxPayable = GST_Returns.TotalTaxPayable;
                        originalTerm.TotalTaxPaid = GST_Returns.TotalTaxPaid;
                        originalTerm.FilingDate = GST_Returns.FilingDate;
                        originalTerm.Status = GST_Returns.Status;
                        originalTerm.CreatedOn = GST_Returns.CreatedOn;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
                var Return = await _context.GST_Returns.Where(x => x.ReturnID.ToString() == ReturnID).ToListAsync();

                if (string.IsNullOrEmpty(Return.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Invoice Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_Returns.RemoveRange(Return);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_Returns>>> GetGST_ReturnsData()
        {
            try
            {
                var plans = await _context.GST_Returns.AsQueryable().OrderBy(x => x.ReturnID).ToListAsync();

                return new ManagerBaseResponse<List<GST_Returns>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Returns Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_Returns>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_Returns>>> GetGST_ReturnsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_Returns.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.ReturnType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.ReturnID);

                PageListed<GST_Returns> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_Returns>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_Returns>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGST_SalesData(GST_Sales GST_Sales)
        {
            var response = new ManagerBaseResponse<List<GST_Sales>>();

            try
            {
                var Company =await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == GST_Sales.CompanyID);
                

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
                    var code = await _context.GST_HSNSAC.AnyAsync(x => x.Code == GST_Sales.SACCode);
                    if(GST_Sales.SACCode == "")
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            IsSuccess = false,
                            Result = false,
                            StatusCode = 400,
                            Message = "SAC Code is not allow blank."
                        };
                    }
                    if (GST_Sales.SaleID == 0)
                    {
                        // Insert
                        GST_Sales originalTerm = new GST_Sales();
                        originalTerm.CompanyID = GST_Sales.CompanyID;
                        originalTerm.InvoiceNo = GST_Sales.InvoiceNo;
                        originalTerm.InvoiceDate = GST_Sales.InvoiceDate;
                        originalTerm.CustomerName = GST_Sales.CustomerName;
                        originalTerm.CustomerGSTIN =  GST_Sales.CustomerGSTIN;
                        originalTerm.PLaceOfSupply = GST_Sales.PLaceOfSupply;
                        originalTerm.HSNCode = GST_Sales.HSNCode;
                        if (!code && GST_Sales.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.SACCode = GST_Sales.SACCode;
                        originalTerm.TaxableValue = GST_Sales.TaxableValue;
                        originalTerm.CGST = GST_Sales.CGST;
                        originalTerm.SGST = GST_Sales.SGST;
                        originalTerm.IGST = GST_Sales.IGST;
                        originalTerm.TotalInvoiceValue = GST_Sales.TotalInvoiceValue;
                        originalTerm.CreatedOn = GST_Sales.CreatedOn;
                        originalTerm.CreatedBy = GST_Sales.CreatedBy;

                        _context.Add(originalTerm);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.GST_Sales
                            .Where(x => x.SaleID == GST_Sales.SaleID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = GST_Sales.CompanyID;
                        originalTerm.InvoiceNo = GST_Sales.InvoiceNo;
                        originalTerm.InvoiceDate = GST_Sales.InvoiceDate;
                        originalTerm.CustomerName = GST_Sales.CustomerName;
                        originalTerm.CustomerGSTIN = GST_Sales.CustomerGSTIN;
                        originalTerm.PLaceOfSupply = GST_Sales.PLaceOfSupply;
                        originalTerm.HSNCode = GST_Sales.HSNCode;
                        if (!code && GST_Sales.SACCode != null)
                        {
                            return new ManagerBaseResponse<bool>
                            {
                                IsSuccess = false,
                                StatusCode = 400,
                                Result = false,
                                Message = "SAC Code is not found in Master Table.",
                            };
                        }
                        originalTerm.SACCode =  GST_Sales.SACCode;
                        originalTerm.SACCode = GST_Sales.SACCode;
                        originalTerm.TaxableValue = GST_Sales.TaxableValue;
                        originalTerm.CGST = GST_Sales.CGST;
                        originalTerm.SGST = GST_Sales.SGST;
                        originalTerm.IGST = GST_Sales.IGST;
                        originalTerm.TotalInvoiceValue = GST_Sales.TotalInvoiceValue;
                        originalTerm.CreatedOn = GST_Sales.CreatedOn;
                        originalTerm.CreatedBy = GST_Sales.CreatedBy;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return  new ManagerBaseResponse<bool>
                {
                    Result = true,
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
                var Sales = await _context.GST_Sales.Where(x => x.SaleID.ToString() == SaleID).ToListAsync();

                if (string.IsNullOrEmpty(Sales.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "GST Sales Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.GST_Sales.RemoveRange(Sales);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
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
        public async Task<ManagerBaseResponse<List<GST_Sales>>> GetGST_SalesData()
        {
            try
            {
                var plans = await _context.GST_Sales.AsQueryable().OrderBy(x => x.SaleID).ToListAsync();

                return new ManagerBaseResponse<List<GST_Sales>>
                {
                    IsSuccess = true,
                    Result = plans,
                    Message = "GST Sales Data Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GST_Sales>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GST_Sales>>> GetGST_SalesFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.GST_Sales.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.CustomerName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.SaleID);

                PageListed<GST_Sales> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<GST_Sales>>
                {
                    Result = result.Data,
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

                return new ManagerBaseResponse<IEnumerable<GST_Sales>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
