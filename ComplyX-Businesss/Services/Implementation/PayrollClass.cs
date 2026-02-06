

using ComplyX_Businesss.Models;
using ComplyX.Services;
using ComplyX.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
 
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using ComplyX_Businesss.Helper;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.PayrollData;
using ComplyX.Data.Entities;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.LeaveEncashmentPolicy;
using ComplyX_Businesss.Models.LeaveEncashmentTransaction;
using ComplyX_Businesss.Models.CompanyEPFO;

namespace ComplyX.BusinessLogic
{
    public class PayrollClass : IPayrollServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };
        private readonly AppContext _context;
        private readonly IUnitOfWork _UnitOfWork;

        public PayrollClass(AppContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _UnitOfWork = unitOfWork;
        }

        public async Task<ManagerBaseResponse<bool>> SavePayrollData(PayrollDataRequestModel Payrolls)
        {
            var response = new ManagerBaseResponse<List<PayrollDatum>>();

            try
            {
                var employeeid = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.EmployeeId == Payrolls.EmployeeId);
                if (employeeid == null)
                {
                    return (new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Data not found."
                    });
                }
                else
                {
                    if (Payrolls.PayrollId == 0)
                    {
                        // Insert
                        PayrollDatum _model = new PayrollDatum();
                        _model.EmployeeId = Payrolls.EmployeeId;
                        _model.Month = Payrolls.Month;
                        _model.Basic = Payrolls.Basic;
                        _model.Hra = Payrolls.Hra;
                        _model.SpecialAllowance = Payrolls.SpecialAllowance;
                        _model.VariablePay = Payrolls.VariablePay;
                        _model.GrossSalary = Payrolls.GrossSalary;
                        _model.Pf = Payrolls.Pf;
                        _model.Esi = Payrolls.Esi;
                        _model.ProfessionalTax = Payrolls.ProfessionalTax;
                        _model.Tds = Payrolls.Tds;
                        _model.NetPay = Payrolls.NetPay;
                        _model.BankAccount = Payrolls.BankAccount;
                        _model.Ifsc = Payrolls.Ifsc;

                        await _UnitOfWork.PayrollDataRespositories.AddAsync( _model );
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.PayrollDataRespositories.GetQueryable()
                            .Where(x => x.PayrollId == Payrolls.PayrollId).FirstOrDefault();
                        originalTerm.EmployeeId = Payrolls.EmployeeId;
                        originalTerm.Month = Payrolls.Month;
                        originalTerm.Basic = Payrolls.Basic;
                        originalTerm.Hra = Payrolls.Hra;
                        originalTerm.SpecialAllowance = Payrolls.SpecialAllowance;
                        originalTerm.VariablePay = Payrolls.VariablePay;
                        originalTerm.GrossSalary = Payrolls.GrossSalary;
                        originalTerm.Pf = Payrolls.Pf;
                        originalTerm.Esi = Payrolls.Esi;
                        originalTerm.ProfessionalTax = Payrolls.ProfessionalTax;
                        originalTerm.Tds = Payrolls.Tds;
                        originalTerm.NetPay = Payrolls.NetPay;
                        originalTerm.BankAccount = Payrolls.BankAccount;
                        originalTerm.Ifsc = Payrolls.Ifsc;
 
                    }
                }
                await _UnitOfWork.CommitAsync();
                return  (new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payroll Data Saved Successfully."
                });
            }
            catch (Exception e)
            {
                return  (new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                });
            }
        }

        public async Task<ManagerBaseResponse<bool>> RemovePayrollData(string PayrollID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var PayrollData = await _UnitOfWork.PayrollDataRespositories.GetQueryable().Where(x => x.PayrollId.ToString() == PayrollID).ToListAsync();

                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PayrollDataRespositories.RemoveRange(PayrollData);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payroll Data Removed Successfully.",
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

        public async Task<ManagerBaseResponse<bool>> RemovePayrollDataByCompanyIDEmployeeID(string CompanyID ,string EmployeeID)
        {
            try
            {
                var employees = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId.ToString() == CompanyID && x.EmployeeId.ToString() == EmployeeID);
                if (employees == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee not found.",
                    };
                }

                // Get all report detail definitions for the given report name
                var PayrollData = await _UnitOfWork.PayrollDataRespositories.GetQueryable().Where(x => x.EmployeeId == employees.EmployeeId).ToListAsync();

                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PayrollDataRespositories.RemoveRange(PayrollData);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payroll Data Removed Successfully.",
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

        public async Task<ManagerBaseResponse<bool>> RemoveAllPayrollDataByCompanyID(string CompanyID)
        {
            try
            {

                var PayrollData = await _UnitOfWork.PayrollDataRespositories.GetQueryable().Join( _UnitOfWork.EmployeeRespositories.GetQueryable(), payroll => payroll.EmployeeId, emp => emp.EmployeeId,
        (payroll, emp) => new { Payroll = payroll, Employee = emp } ).Where(x => x.Employee.CompanyId.ToString() == CompanyID).Select(x => x.Payroll).ToListAsync();


                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.PayrollDataRespositories.RemoveRange(PayrollData);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payroll Data Removed Successfully.",
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

        public async Task<ManagerBaseResponse<bool>> EditPayrollDataByCompanyIDEmployeeID(PayrollDataRequestModel data, string CompanyID, string EmployeeID , string PayrollID)
        {
            try
            {
                var employees = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId.ToString() == CompanyID && x.EmployeeId.ToString() == EmployeeID);
                if (employees == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee not found.",
                    };
                } 
                var PayrollData = await _UnitOfWork.PayrollDataRespositories.GetQueryable().Join(_UnitOfWork.EmployeeRespositories.GetQueryable(), payroll => payroll.EmployeeId, emp => emp.EmployeeId,
                                        (payroll, emp) => new { Payroll = payroll, Employee = emp }).Where(x => x.Employee.CompanyId.ToString() == CompanyID 
                                        && x.Employee.EmployeeId.ToString() == EmployeeID
                                        && x.Payroll.PayrollId.ToString() == PayrollID)
                                        .Select(x => x.Payroll).ToListAsync();

                foreach (var payroll in PayrollData)
                {
                    payroll.EmployeeId = data.EmployeeId;
                    payroll.Month = data.Month;
                    payroll.Basic = data.Basic;
                    payroll.Hra = data.Hra;
                    payroll.SpecialAllowance = data.SpecialAllowance;
                    payroll.VariablePay = data.VariablePay;
                    payroll.GrossSalary = data.GrossSalary;
                    payroll.Pf = data.Pf;
                    payroll.Esi = data.Esi;
                    payroll.ProfessionalTax = data.ProfessionalTax;
                    payroll.Tds = data.Tds;
                    payroll.NetPay = data.NetPay;
                    payroll.BankAccount = data.BankAccount;
                    payroll.Ifsc = data.Ifsc;
                }

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Payroll Data Updated Successfully.",
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
        public async Task<ManagerBaseResponse<IEnumerable<PayrollDataResponseModel>>> GetPayrollDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.PayrollDataRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Month.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PayrollId);
                var responseQuery = query.Select(x => new PayrollDataResponseModel
                {

                    PayrollId = x.PayrollId,
                    EmployeeId = x.EmployeeId,
                    Month = x.Month,
                    Basic = x.Basic,
                    Hra = x.Hra,
                    SpecialAllowance = x.SpecialAllowance,
                    VariablePay = x.VariablePay,
                    GrossSalary = x.GrossSalary,
                    Pf = x.Pf,
                    Esi = x.Esi,
                    ProfessionalTax = x.ProfessionalTax,
                    Tds = x.Tds,
                    NetPay = x.NetPay,
                    BankAccount = x.BankAccount,
                    Ifsc = x.Ifsc
                });
                PageListed<PayrollDataResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<PayrollDataResponseModel>>
                {
                    Result = result.Data,
                    Message = "Payroll Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<PayrollDataResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_PolicyData(LeaveEncashmentPolicyRequestModel Leave_Encashment_Policy)
        {
            var response = new ManagerBaseResponse<List<LeaveEncashmentPolicy>>();

            try
            {
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == Leave_Encashment_Policy.CompanyId);
                if (Company == null)
                {
                    return (new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company Data not found."
                    });
                }
                else
                {
                    if (Leave_Encashment_Policy.PolicyId == Guid.Empty)
                    {
                        // Insert
                        LeaveEncashmentPolicy _model = new LeaveEncashmentPolicy();
                        _model.PolicyId = Guid.NewGuid();
                        _model.CompanyId = Leave_Encashment_Policy.CompanyId;
                        _model.LeaveType = Leave_Encashment_Policy.LeaveType;
                        _model.EncashmentAllowed = Leave_Encashment_Policy.EncashmentAllowed;
                        _model.MaxEncashableDays = Leave_Encashment_Policy.MaxEncashableDays;
                        _model.EncashmentFrequency = Leave_Encashment_Policy.EncashmentFrequency;
                        _model.EncashmentFormula = Leave_Encashment_Policy.EncashmentFormula;   
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await _UnitOfWork.LeaveEncashmentPolicyRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.LeaveEncashmentPolicyRespositories.GetQueryable()
                            .Where(x => x.PolicyId == Leave_Encashment_Policy.PolicyId).FirstOrDefault();
                        originalTerm.CompanyId = Leave_Encashment_Policy.CompanyId;
                        originalTerm.LeaveType = Leave_Encashment_Policy.LeaveType;
                        originalTerm.EncashmentAllowed = Leave_Encashment_Policy.EncashmentAllowed;
                        originalTerm.MaxEncashableDays = Leave_Encashment_Policy.MaxEncashableDays;
                        originalTerm.EncashmentFrequency = Leave_Encashment_Policy.EncashmentFrequency;
                        originalTerm.EncashmentFormula = Leave_Encashment_Policy.EncashmentFormula;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                      
                    }
                }

                await _UnitOfWork.CommitAsync();
                return (new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Leave Encashment Policy Saved Successfully."
                });
            }
            catch (Exception e)
            {
                return (new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                });
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_PolicyData(string PolicyID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var LeaveEncashmentPolicy = await _UnitOfWork.LeaveEncashmentPolicyRespositories.GetQueryable().Where(x => x.PolicyId.ToString() == PolicyID).ToListAsync();

                if (string.IsNullOrEmpty(LeaveEncashmentPolicy.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "LeaveEncashmentPolicy Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.LeaveEncashmentPolicyRespositories.RemoveRange(LeaveEncashmentPolicy);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LeaveEncashmentPolicy Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<LeaveEncashmentPolicyResponseModel>>> GetLeave_Encashment_PolicyByID(string PolicyID)
        {
            try
            {
                var Policy = await _UnitOfWork.LeaveEncashmentPolicyRespositories.GetQueryable().Where(x => x.PolicyId.ToString() == PolicyID)
                      .Select(x => new LeaveEncashmentPolicyResponseModel
                      {
                          PolicyId = x.PolicyId,
                          LeaveType = x.LeaveType,
                          EncashmentAllowed = x.EncashmentAllowed,
                          MaxEncashableDays = x.MaxEncashableDays,
                          EncashmentFrequency = x.EncashmentFrequency,
                          EncashmentFormula = x.EncashmentFormula,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          CompanyId = x.CompanyId
                      }).ToListAsync();

                return new ManagerBaseResponse<List<LeaveEncashmentPolicyResponseModel>>
                {
                    IsSuccess = true,
                    Result = Policy,
                    Message = "Leave Ensachment Policy Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<LeaveEncashmentPolicyResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<LeaveEncashmentPolicyResponseModel>>> GetLeave_Encashment_PolicyFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LeaveEncashmentPolicyRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.LeaveType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PolicyId);
                var responseQuery = query.Select(x => new LeaveEncashmentPolicyResponseModel
                {

                    PolicyId = x.PolicyId,
                    LeaveType = x.LeaveType,
                    EncashmentAllowed = x.EncashmentAllowed,
                    MaxEncashableDays = x.MaxEncashableDays,
                    EncashmentFrequency = x.EncashmentFrequency,
                    EncashmentFormula = x.EncashmentFormula,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    CompanyId = x.CompanyId
                });
                PageListed<LeaveEncashmentPolicyResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LeaveEncashmentPolicyResponseModel>>
                {
                    Result = result.Data,
                    Message = "LeaveEncashmentPolicy Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<LeaveEncashmentPolicyResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_TransactionData(LeaveEncashmentTransactionRequestModel Leave_Encashment_Transactions)
        {
            var response = new ManagerBaseResponse<List<LeaveEncashmentTransaction>>();

            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.EmployeeId == Leave_Encashment_Transactions.Employeeid && x.CompanyId == Leave_Encashment_Transactions.CompanyId);
                if (Employee == null)
                {
                    return (new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee and Company Data not found."
                    });
                }
                else
                {
                    if (Leave_Encashment_Transactions.EncashmentId == Guid.Empty)
                    {
                        // Insert
                        LeaveEncashmentTransaction _model = new LeaveEncashmentTransaction();
                        _model.EncashmentId = Guid.NewGuid();
                       _model.Employeeid = Employee.EmployeeId;
                        _model.CompanyId = Leave_Encashment_Transactions.CompanyId;
                        _model.LeaveType = Leave_Encashment_Transactions.LeaveType;
                        _model.DaysEncashed = Leave_Encashment_Transactions.DaysEncashed;
                        _model.EncashmentAmount = Leave_Encashment_Transactions.EncashmentAmount;
                        _model.PaymentDate = Util.GetCurrentCSTDateAndTime();
                        _model.Status = Leave_Encashment_Transactions.Status;
                        _model.ApprovedBy = Guid.NewGuid();
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await _UnitOfWork.LeaveEncashmentTransactionRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.LeaveEncashmentTransactionRespositories.GetQueryable()
                            .Where(x => x.EncashmentId == Leave_Encashment_Transactions.EncashmentId).FirstOrDefault();
                        originalTerm.Employeeid = Employee.EmployeeId;
                        originalTerm.CompanyId = Leave_Encashment_Transactions.CompanyId;
                        originalTerm.LeaveType = Leave_Encashment_Transactions.LeaveType;
                        originalTerm.DaysEncashed = Leave_Encashment_Transactions.DaysEncashed;
                        originalTerm.EncashmentAmount = Leave_Encashment_Transactions.EncashmentAmount;
                        originalTerm.PaymentDate = Leave_Encashment_Transactions.PaymentDate;
                        originalTerm.Status = Leave_Encashment_Transactions.Status;
                        originalTerm.ApprovedBy = Guid.NewGuid();
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return (new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Leave Encashment Transaction Saved Successfully."
                });
            }
            catch (Exception e)
            {
                return (new ManagerBaseResponse<bool>
                {
                    Result = false,
                    Message = e.Message
                });
            }
        }
        public async Task<ManagerBaseResponse<bool>> RemoveLeave_Encashment_TransactionData(string EncashmentID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var LeaveEncashmentTransaction = await _UnitOfWork.LeaveEncashmentTransactionRespositories.GetQueryable().Where(x => x.EncashmentId.ToString() == EncashmentID).ToListAsync();

                if (string.IsNullOrEmpty(LeaveEncashmentTransaction.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "LeaveEncashmentTransaction Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.LeaveEncashmentTransactionRespositories.RemoveRange(LeaveEncashmentTransaction);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "LeaveEncashmentPolicy Data Removed Successfully.",
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

        public async Task<ManagerBaseResponse<List<LeaveEncashmentTransactionResponseModel>>> GetLeave_Encashment_TransactionByID(string EncashmentID)
        {
            try
            {
                var Transaction = await _UnitOfWork.LeaveEncashmentTransactionRespositories.GetQueryable().Where(x => x.EncashmentId.ToString() == EncashmentID)
                     .Select(x => new LeaveEncashmentTransactionResponseModel
                     {
                         EncashmentId = x.EncashmentId,
                         CompanyId = x.CompanyId,
                         Employeeid = x.Employeeid,
                         LeaveType = x.LeaveType,
                         DaysEncashed = x.DaysEncashed,
                         EncashmentAmount = x.EncashmentAmount,
                         PaymentDate = x.PaymentDate,
                         Status = x.Status,
                         ApprovedBy = x.ApprovedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt
                     }).ToListAsync();

                return new ManagerBaseResponse<List<LeaveEncashmentTransactionResponseModel>>
                {
                    IsSuccess = true,
                    Result = Transaction,
                    Message = "Leave Ensachment TRansaction Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<LeaveEncashmentTransactionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<IEnumerable<LeaveEncashmentTransactionResponseModel>>> GetLeave_Encashment_TransactionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.LeaveEncashmentTransactionRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.LeaveType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EncashmentId);
                var responseQuery = query.Select(x => new LeaveEncashmentTransactionResponseModel
                {

                    EncashmentId = x.EncashmentId,
                    CompanyId = x.CompanyId,
                    Employeeid = x.Employeeid,
                    LeaveType = x.LeaveType,
                    DaysEncashed = x.DaysEncashed,
                    EncashmentAmount = x.EncashmentAmount,
                    PaymentDate = x.PaymentDate,
                    Status = x.Status,
                    ApprovedBy = x.ApprovedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                });
                PageListed<LeaveEncashmentTransactionResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<LeaveEncashmentTransactionResponseModel>>
                {
                    Result = result.Data,
                    Message = "LeaveEncashmentTransaction Data Retrieved Successfully.",
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

                return new ManagerBaseResponse<IEnumerable<LeaveEncashmentTransactionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
