
using ComplyX.Shared.Data;
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

namespace ComplyX.BusinessLogic
{
    public class PayrollClass : IPayrollServices
    {
        private readonly AppDbContext _context;

        public PayrollClass(AppDbContext context)
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

    }
}
