

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

namespace ComplyX.BusinessLogic
{
    public class PayrollClass : IPayrollServices
    {
        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };
        private readonly AppContext _context;

        public PayrollClass(AppContext context)
        {
            _context = context;
        }

        public async Task<ManagerBaseResponse<bool>> SavePayrollData(PayrollData Payrolls)
        {
            var response = new ManagerBaseResponse<List<PayrollData>>();

            try
            {
                var employeeid = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == Payrolls.EmployeeID);
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
                    if (Payrolls.PayrollID == 0)
                    {
                        // Insert
                        PayrollData _model = new PayrollData();
                        _model.EmployeeID = Payrolls.EmployeeID;
                        _model.Month = Payrolls.Month;
                        _model.Basic = Payrolls.Basic;
                        _model.HRA = Payrolls.HRA;
                        _model.SpecialAllowance = Payrolls.SpecialAllowance;
                        _model.VariablePay = Payrolls.VariablePay;
                        _model.GrossSalary = Payrolls.GrossSalary;
                        _model.PF = Payrolls.PF;
                        _model.ESI = Payrolls.ESI;
                        _model.ProfessionalTax = Payrolls.ProfessionalTax;
                        _model.TDS = Payrolls.TDS;
                        _model.NetPay = Payrolls.NetPay;
                        _model.BankAccount = Payrolls.BankAccount;
                        _model.IFSC = Payrolls.IFSC;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.PayrollData
                            .Where(x => x.PayrollID == Payrolls.PayrollID).FirstOrDefault();
                        originalTerm.EmployeeID = Payrolls.EmployeeID;
                        originalTerm.Month = Payrolls.Month;
                        originalTerm.Basic = Payrolls.Basic;
                        originalTerm.HRA = Payrolls.HRA;
                        originalTerm.SpecialAllowance = Payrolls.SpecialAllowance;
                        originalTerm.VariablePay = Payrolls.VariablePay;
                        originalTerm.GrossSalary = Payrolls.GrossSalary;
                        originalTerm.PF = Payrolls.PF;
                        originalTerm.ESI = Payrolls.ESI;
                        originalTerm.ProfessionalTax = Payrolls.ProfessionalTax;
                        originalTerm.TDS = Payrolls.TDS;
                        originalTerm.NetPay = Payrolls.NetPay;
                        originalTerm.BankAccount = Payrolls.BankAccount;
                        originalTerm.IFSC = Payrolls.IFSC;

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                
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
                var PayrollData = await _context.PayrollData.Where(x => x.PayrollID.ToString() == PayrollID).ToListAsync();

                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.PayrollData.RemoveRange(PayrollData);

                await _context.SaveChangesAsync();

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
                var employees = await _context.Employees.FirstOrDefaultAsync(x => x.CompanyID.ToString() == CompanyID && x.EmployeeID.ToString() == EmployeeID);
                if (employees == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee not found.",
                    };
                }

                // Get all report detail definitions for the given report name
                var PayrollData = await _context.PayrollData.Where(x => x.EmployeeID == employees.EmployeeID).ToListAsync();

                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.PayrollData.RemoveRange(PayrollData);

                await _context.SaveChangesAsync();

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

                var PayrollData = await _context.PayrollData.Join( _context.Employees, payroll => payroll.EmployeeID, emp => emp.EmployeeID,
        (payroll, emp) => new { Payroll = payroll, Employee = emp } ).Where(x => x.Employee.CompanyID.ToString() == CompanyID).Select(x => x.Payroll).ToListAsync();


                if (string.IsNullOrEmpty(PayrollData.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Payroll Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.PayrollData.RemoveRange(PayrollData);

                await _context.SaveChangesAsync();

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

        public async Task<ManagerBaseResponse<bool>> EditPayrollDataByCompanyIDEmployeeID(PayrollData data, string CompanyID, string EmployeeID , string PayrollID)
        {
            try
            {
                var employees = await _context.Employees.FirstOrDefaultAsync(x => x.CompanyID.ToString() == CompanyID && x.EmployeeID.ToString() == EmployeeID);
                if (employees == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee not found.",
                    };
                } 
                var PayrollData = await _context.PayrollData.Join(_context.Employees, payroll => payroll.EmployeeID, emp => emp.EmployeeID,
                                        (payroll, emp) => new { Payroll = payroll, Employee = emp }).Where(x => x.Employee.CompanyID.ToString() == CompanyID 
                                        && x.Employee.EmployeeID.ToString() == EmployeeID
                                        && x.Payroll.PayrollID.ToString() == PayrollID)
                                        .Select(x => x.Payroll).ToListAsync();

                foreach (var payroll in PayrollData)
                {
                    payroll.EmployeeID = data.EmployeeID;
                    payroll.Month = data.Month;
                    payroll.Basic = data.Basic;
                    payroll.HRA = data.HRA;
                    payroll.SpecialAllowance = data.SpecialAllowance;
                    payroll.VariablePay = data.VariablePay;
                    payroll.GrossSalary = data.GrossSalary;
                    payroll.PF = data.PF;
                    payroll.ESI = data.ESI;
                    payroll.ProfessionalTax = data.ProfessionalTax;
                    payroll.TDS = data.TDS;
                    payroll.NetPay = data.NetPay;
                    payroll.BankAccount = data.BankAccount;
                    payroll.IFSC = data.IFSC;
                }

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<IEnumerable<PayrollData>>> GetPayrollDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.PayrollData.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Month.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PayrollID);

                PageListed<PayrollData> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<PayrollData>>
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

                return new ManagerBaseResponse<IEnumerable<PayrollData>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_PolicyData(Leave_Encashment_Policy Leave_Encashment_Policy)
        {
            var response = new ManagerBaseResponse<List<Leave_Encashment_Policy>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == Leave_Encashment_Policy.CompanyID);
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
                    if (Leave_Encashment_Policy.PolicyID == Guid.Empty)
                    {
                        // Insert
                        Leave_Encashment_Policy _model = new Leave_Encashment_Policy();
                        _model.PolicyID = Guid.NewGuid();
                        _model.CompanyID = Leave_Encashment_Policy.CompanyID;
                        _model.LeaveType = Leave_Encashment_Policy.LeaveType;
                        _model.EncashmentAllowed = Leave_Encashment_Policy.EncashmentAllowed;
                        _model.MaxEncashableDays = Leave_Encashment_Policy.MaxEncashableDays;
                        _model.EncashmentFrequency = Leave_Encashment_Policy.EncashmentFrequency;
                        _model.EncashmentFormula = Leave_Encashment_Policy.EncashmentFormula;   
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Leave_Encashment_Policy
                            .Where(x => x.PolicyID == Leave_Encashment_Policy.PolicyID).FirstOrDefault();
                        originalTerm.CompanyID = Leave_Encashment_Policy.CompanyID;
                        originalTerm.LeaveType = Leave_Encashment_Policy.LeaveType;
                        originalTerm.EncashmentAllowed = Leave_Encashment_Policy.EncashmentAllowed;
                        originalTerm.MaxEncashableDays = Leave_Encashment_Policy.MaxEncashableDays;
                        originalTerm.EncashmentFrequency = Leave_Encashment_Policy.EncashmentFrequency;
                        originalTerm.EncashmentFormula = Leave_Encashment_Policy.EncashmentFormula;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

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
                var LeaveEncashmentPolicy = await _context.Leave_Encashment_Policy.Where(x => x.PolicyID.ToString() == PolicyID).ToListAsync();

                if (string.IsNullOrEmpty(LeaveEncashmentPolicy.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "LeaveEncashmentPolicy Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Leave_Encashment_Policy.RemoveRange(LeaveEncashmentPolicy);

                await _context.SaveChangesAsync();

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
        public async Task<ManagerBaseResponse<List<Leave_Encashment_Policy>>> GetLeave_Encashment_PolicyByID(string PolicyID)
        {
            try
            {
                var Policy = await _context.Leave_Encashment_Policy.AsQueryable().Where(x => x.PolicyID.ToString() == PolicyID).ToListAsync();

                return new ManagerBaseResponse<List<Leave_Encashment_Policy>>
                {
                    IsSuccess = true,
                    Result = Policy,
                    Message = "Leave Ensachment Policy Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Leave_Encashment_Policy>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Leave_Encashment_Policy>>> GetLeave_Encashment_PolicyFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Leave_Encashment_Policy.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.LeaveType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PolicyID);

                PageListed<Leave_Encashment_Policy> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Leave_Encashment_Policy>>
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

                return new ManagerBaseResponse<IEnumerable<Leave_Encashment_Policy>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<bool>> SaveLeave_Encashment_TransactionData(Leave_Encashment_Transactions Leave_Encashment_Transactions)
        {
            var response = new ManagerBaseResponse<List<Leave_Encashment_Transactions>>();

            try
            {
                var Employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == Leave_Encashment_Transactions.EmployeeID && x.CompanyID == Leave_Encashment_Transactions.CompanyID);
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
                    if (Leave_Encashment_Transactions.EncashmentID == Guid.Empty)
                    {
                        // Insert
                        Leave_Encashment_Transactions _model = new Leave_Encashment_Transactions();
                        _model.EncashmentID = Guid.NewGuid();
                       _model.EmployeeID = Employee.EmployeeID;
                        _model.CompanyID = Employee.CompanyID;
                        _model.LeaveType = Leave_Encashment_Transactions.LeaveType;
                        _model.DaysEncashed = Leave_Encashment_Transactions.DaysEncashed;
                        _model.EncashmentAmount = Leave_Encashment_Transactions.EncashmentAmount;
                        _model.PaymentDate = Util.GetCurrentCSTDateAndTime();
                        _model.Status = Leave_Encashment_Transactions.Status;
                        _model.ApprovedBy = Guid.NewGuid();
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Leave_Encashment_Transactions
                            .Where(x => x.EncashmentID == Leave_Encashment_Transactions.EncashmentID).FirstOrDefault();
                        originalTerm.EmployeeID = Employee.EmployeeID;
                        originalTerm.CompanyID = Employee.CompanyID;
                        originalTerm.LeaveType = Leave_Encashment_Transactions.LeaveType;
                        originalTerm.DaysEncashed = Leave_Encashment_Transactions.DaysEncashed;
                        originalTerm.EncashmentAmount = Leave_Encashment_Transactions.EncashmentAmount;
                        originalTerm.PaymentDate = Leave_Encashment_Transactions.PaymentDate;
                        originalTerm.Status = Leave_Encashment_Transactions.Status;
                        originalTerm.ApprovedBy = Guid.NewGuid();
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }

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
                var LeaveEncashmentTransaction = await _context.Leave_Encashment_Transactions.Where(x => x.EncashmentID.ToString() == EncashmentID).ToListAsync();

                if (string.IsNullOrEmpty(LeaveEncashmentTransaction.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "LeaveEncashmentTransaction Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Leave_Encashment_Transactions.RemoveRange(LeaveEncashmentTransaction);

                await _context.SaveChangesAsync();

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

        public async Task<ManagerBaseResponse<List<Leave_Encashment_Transactions>>> GetLeave_Encashment_TransactionByID(string EncashmentID)
        {
            try
            {
                var Transaction = await _context.Leave_Encashment_Transactions.AsQueryable().Where(x => x.EncashmentID.ToString() == EncashmentID).ToListAsync();

                return new ManagerBaseResponse<List<Leave_Encashment_Transactions>>
                {
                    IsSuccess = true,
                    Result = Transaction,
                    Message = "Leave Ensachment TRansaction Plans Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Leave_Encashment_Transactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<IEnumerable<Leave_Encashment_Transactions>>> GetLeave_Encashment_TransactionFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Leave_Encashment_Transactions.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.LeaveType.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EncashmentID);

                PageListed<Leave_Encashment_Transactions> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);

                return new ManagerBaseResponse<IEnumerable<Leave_Encashment_Transactions>>
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

                return new ManagerBaseResponse<IEnumerable<Leave_Encashment_Transactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}
