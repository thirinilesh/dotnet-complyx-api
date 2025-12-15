using ComplyX.Helper;
using ComplyX.Models;
using ComplyX.Data;
using ComplyX.Services;
using Lakshmi.Common.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;

using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Providers.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace ComplyX.BusinessLogic
{
    public class EmployeeClass : IEmployeeServices
    {
        private readonly AppDbContext _context;

        public EmployeeClass(AppDbContext context)
        {
            _context = context;
        }
        public Task<ManagerBaseResponse<bool>> SaveEmployeeData(Employees Employee)
        {
            var response = new ManagerBaseResponse<List<Employees>>();

            try
            {
                if (Employee.CompanyID == 0 && Employee.CompanyID.ToString() == null)
                {
                    return Task.FromResult(new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Company ID is required."
                    });
                }
                else if (Employee.SubcontractorID == null)
                {
                    var varsub = _context.Subcontractors.AsQueryable().Where(x => x.CompanyID == Employee.CompanyID).ToList();
                    if (varsub.Count == 0)
                    {
                        var varemployee = _context.Subcontractors.AsQueryable().Where(x => x.SubcontractorID == Employee.SubcontractorID).ToList();
                        if (varemployee != null || varemployee.Count != 0)
                        {
                            if (Employee.EmployeeID == 0)
                            {
                                // Insert
                                Employees _model = new Employees();
                                _model.CompanyID = Employee.CompanyID;
                                _model.SubcontractorID = Employee.SubcontractorID;
                                _model.EmployeeCode = Employee.EmployeeCode;
                                _model.FirstName = Employee.FirstName;
                                _model.LastName = Employee.LastName;
                                _model.FatherSpouseName = Employee.FatherSpouseName;
                                _model.DOB = Employee.DOB;
                                _model.Gender = Employee.Gender;
                                _model.MaritalStatus = Employee.MaritalStatus;
                                _model.Nationality = Employee.Nationality;
                                _model.PAN = Employee.PAN;
                                _model.Aadhaar = Employee.Aadhaar;
                                _model.Email = Employee.Email;
                                _model.Mobile = Employee.Mobile;
                                _model.PresentAddress = Employee.PresentAddress;
                                _model.PermanentAddress = Employee.PermanentAddress;
                                _model.State = Employee.State;
                                _model.City = Employee.City;
                                _model.PinCode = Employee.PinCode;
                                _model.DOJ = Employee.DOJ;
                                _model.ConfirmationDate = Employee.ConfirmationDate;
                                _model.Department = Employee.Department;
                                _model.Designation = Employee.Designation;
                                _model.Grade = Employee.Grade;
                                _model.EmploymentType = Employee.EmploymentType;
                                _model.WorkLocation = Employee.WorkLocation;
                                _model.ReportingManager = Employee.ReportingManager;
                                _model.ExitDate = Employee.ExitDate;
                                _model.ExitReason = Employee.ExitReason;
                                _model.ExitType = Employee.ExitType;
                                _model.UAN = Employee.UAN;
                                _model.PFAccountNumber = Employee.PFAccountNumber;
                                _model.ESIC_IP = Employee.ESIC_IP;
                                _model.PTState = Employee.PTState;
                                _model.ActiveStatus = Employee.ActiveStatus;
                                _model.IsDeleted = Employee.IsDeleted;

                                _context.Add(_model);
                                _context.SaveChanges();
                            }
                            else
                            {
                                // Update
                                var originalTerm = _context.Employees
                                    .Where(x => x.EmployeeID == Employee.EmployeeID)
                                    .FirstOrDefault();
                                originalTerm.CompanyID = Employee.CompanyID;
                                originalTerm.SubcontractorID = Employee.SubcontractorID;
                                originalTerm.EmployeeCode = Employee.EmployeeCode;
                                originalTerm.FirstName = Employee.FirstName;
                                originalTerm.LastName = Employee.LastName;
                                originalTerm.FatherSpouseName = Employee.FatherSpouseName;
                                originalTerm.DOB = Employee.DOB;
                                originalTerm.Gender = Employee.Gender;
                                originalTerm.MaritalStatus = Employee.MaritalStatus;
                                originalTerm.Nationality = Employee.Nationality;
                                originalTerm.PAN = Employee.PAN;
                                originalTerm.Aadhaar = Employee.Aadhaar;
                                originalTerm.Email = Employee.Email;
                                originalTerm.Mobile = Employee.Mobile;
                                originalTerm.PresentAddress = Employee.PresentAddress;
                                originalTerm.PermanentAddress = Employee.PermanentAddress;
                                originalTerm.State = Employee.State;
                                originalTerm.City = Employee.City;
                                originalTerm.PinCode = Employee.PinCode;
                                originalTerm.DOJ = Employee.DOJ;
                                originalTerm.ConfirmationDate = Employee.ConfirmationDate;
                                originalTerm.Department = Employee.Department;
                                originalTerm.Designation = Employee.Designation;
                                originalTerm.Grade = Employee.Grade;
                                originalTerm.EmploymentType = Employee.EmploymentType;
                                originalTerm.WorkLocation = Employee.WorkLocation;
                                originalTerm.ReportingManager = Employee.ReportingManager;
                                originalTerm.ExitDate = Employee.ExitDate;
                                originalTerm.ExitReason = Employee.ExitReason;
                                originalTerm.ExitType = Employee.ExitType;
                                originalTerm.UAN = Employee.UAN;
                                originalTerm.PFAccountNumber = Employee.PFAccountNumber;
                                originalTerm.ESIC_IP = Employee.ESIC_IP;
                                originalTerm.PTState = Employee.PTState;
                                originalTerm.ActiveStatus = Employee.ActiveStatus;
                                originalTerm.IsDeleted = Employee.IsDeleted;


                                _context.Update(originalTerm);
                                _context.SaveChanges();
                            }
                            return Task.FromResult(new ManagerBaseResponse<bool>
                            {
                                Result = true,
                                Message = "Employee Details Saved Successfully."
                            });
                        }
                        else
                        {

                            return Task.FromResult(new ManagerBaseResponse<bool>
                            {
                                Result = false,
                                Message = "Subcontractor ID is null."
                            });
                        }
                    }
                    else
                    {
                        return Task.FromResult(new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Company ID and Subcontractor ID do not match."
                        });
                    }
                }
                else if (Employee.CompanyID != 0 && Employee.SubcontractorID != null)
                {
                    var varemployee = _context.Subcontractors.AsQueryable().Where(x => x.SubcontractorID == Employee.SubcontractorID && x.CompanyID == Employee.CompanyID).ToList();
                    if (varemployee == null || varemployee.Count == 0)
                    {

                        return Task.FromResult(new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            Message = "Company ID and Subcontractor ID do not match."
                        });
                    }
                    else
                    {
                        if (Employee.EmployeeID == 0)
                        {
                            // Insert
                            Employees _model = new Employees();
                            _model.CompanyID = Employee.CompanyID;
                            _model.SubcontractorID = Employee.SubcontractorID;
                            _model.EmployeeCode = Employee.EmployeeCode;
                            _model.FirstName = Employee.FirstName;
                            _model.LastName = Employee.LastName;
                            _model.FatherSpouseName = Employee.FatherSpouseName;
                            _model.DOB = Employee.DOB;
                            _model.Gender = Employee.Gender;
                            _model.MaritalStatus = Employee.MaritalStatus;
                            _model.Nationality = Employee.Nationality;
                            _model.PAN = Employee.PAN;
                            _model.Aadhaar = Employee.Aadhaar;
                            _model.Email = Employee.Email;
                            _model.Mobile = Employee.Mobile;
                            _model.PresentAddress = Employee.PresentAddress;
                            _model.PermanentAddress = Employee.PermanentAddress;
                            _model.State = Employee.State;
                            _model.City = Employee.City;
                            _model.PinCode = Employee.PinCode;
                            _model.DOJ = Employee.DOJ;
                            _model.ConfirmationDate = Employee.ConfirmationDate;
                            _model.Department = Employee.Department;
                            _model.Designation = Employee.Designation;
                            _model.Grade = Employee.Grade;
                            _model.EmploymentType = Employee.EmploymentType;
                            _model.WorkLocation = Employee.WorkLocation;
                            _model.ReportingManager = Employee.ReportingManager;
                            _model.ExitDate = Employee.ExitDate;
                            _model.ExitReason = Employee.ExitReason;
                            _model.ExitType = Employee.ExitType;
                            _model.UAN = Employee.UAN;
                            _model.PFAccountNumber = Employee.PFAccountNumber;
                            _model.ESIC_IP = Employee.ESIC_IP;
                            _model.PTState = Employee.PTState;
                            _model.ActiveStatus = Employee.ActiveStatus;
                            _model.IsDeleted = Employee.IsDeleted;

                            _context.Add(_model);
                            _context.SaveChanges();
                        }
                        else
                        {
                            // Update
                            var originalTerm = _context.Employees
                                .Where(x => x.EmployeeID == Employee.EmployeeID)
                                .FirstOrDefault();
                            originalTerm.CompanyID = Employee.CompanyID;
                            originalTerm.SubcontractorID = Employee.SubcontractorID;
                            originalTerm.EmployeeCode = Employee.EmployeeCode;
                            originalTerm.FirstName = Employee.FirstName;
                            originalTerm.LastName = Employee.LastName;
                            originalTerm.FatherSpouseName = Employee.FatherSpouseName;
                            originalTerm.DOB = Employee.DOB;
                            originalTerm.Gender = Employee.Gender;
                            originalTerm.MaritalStatus = Employee.MaritalStatus;
                            originalTerm.Nationality = Employee.Nationality;
                            originalTerm.PAN = Employee.PAN;
                            originalTerm.Aadhaar = Employee.Aadhaar;
                            originalTerm.Email = Employee.Email;
                            originalTerm.Mobile = Employee.Mobile;
                            originalTerm.PresentAddress = Employee.PresentAddress;
                            originalTerm.PermanentAddress = Employee.PermanentAddress;
                            originalTerm.State = Employee.State;
                            originalTerm.City = Employee.City;
                            originalTerm.PinCode = Employee.PinCode;
                            originalTerm.DOJ = Employee.DOJ;
                            originalTerm.ConfirmationDate = Employee.ConfirmationDate;
                            originalTerm.Department = Employee.Department;
                            originalTerm.Designation = Employee.Designation;
                            originalTerm.Grade = Employee.Grade;
                            originalTerm.EmploymentType = Employee.EmploymentType;
                            originalTerm.WorkLocation = Employee.WorkLocation;
                            originalTerm.ReportingManager = Employee.ReportingManager;
                            originalTerm.ExitDate = Employee.ExitDate;
                            originalTerm.ExitReason = Employee.ExitReason;
                            originalTerm.ExitType = Employee.ExitType;
                            originalTerm.UAN = Employee.UAN;
                            originalTerm.PFAccountNumber = Employee.PFAccountNumber;
                            originalTerm.ESIC_IP = Employee.ESIC_IP;
                            originalTerm.PTState = Employee.PTState;
                            originalTerm.ActiveStatus = Employee.ActiveStatus;
                            originalTerm.IsDeleted = Employee.IsDeleted;


                            _context.Update(originalTerm);
                            _context.SaveChanges();
                        }
                    }
                }
                else
                {
                    if (Employee.EmployeeID == 0)
                    {
                        // Insert
                        Employees _model = new Employees();
                        _model.CompanyID = Employee.CompanyID;
                        _model.SubcontractorID = Employee.SubcontractorID;
                        _model.EmployeeCode = Employee.EmployeeCode;
                        _model.FirstName = Employee.FirstName;
                        _model.LastName = Employee.LastName;
                        _model.FatherSpouseName = Employee.FatherSpouseName;
                        _model.DOB = Employee.DOB;
                        _model.Gender = Employee.Gender;
                        _model.MaritalStatus = Employee.MaritalStatus;
                        _model.Nationality = Employee.Nationality;
                        _model.PAN = Employee.PAN;
                        _model.Aadhaar = Employee.Aadhaar;
                        _model.Email = Employee.Email;
                        _model.Mobile = Employee.Mobile;
                        _model.PresentAddress = Employee.PresentAddress;
                        _model.PermanentAddress = Employee.PermanentAddress;
                        _model.State = Employee.State;
                        _model.City = Employee.City;
                        _model.PinCode = Employee.PinCode;
                        _model.DOJ = Employee.DOJ;
                        _model.ConfirmationDate = Employee.ConfirmationDate;
                        _model.Department = Employee.Department;
                        _model.Designation = Employee.Designation;
                        _model.Grade = Employee.Grade;
                        _model.EmploymentType = Employee.EmploymentType;
                        _model.WorkLocation = Employee.WorkLocation;
                        _model.ReportingManager = Employee.ReportingManager;
                        _model.ExitDate = Employee.ExitDate;
                        _model.ExitReason = Employee.ExitReason;
                        _model.ExitType = Employee.ExitType;
                        _model.UAN = Employee.UAN;
                        _model.PFAccountNumber = Employee.PFAccountNumber;
                        _model.ESIC_IP = Employee.ESIC_IP;
                        _model.PTState = Employee.PTState;
                        _model.ActiveStatus = Employee.ActiveStatus;
                        _model.IsDeleted = Employee.IsDeleted;

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Employees
                            .Where(x => x.EmployeeID == Employee.EmployeeID)
                            .FirstOrDefault();
                        originalTerm.CompanyID = Employee.CompanyID;
                        originalTerm.SubcontractorID = Employee.SubcontractorID;
                        originalTerm.EmployeeCode = Employee.EmployeeCode;
                        originalTerm.FirstName = Employee.FirstName;
                        originalTerm.LastName = Employee.LastName;
                        originalTerm.FatherSpouseName = Employee.FatherSpouseName;
                        originalTerm.DOB = Employee.DOB;
                        originalTerm.Gender = Employee.Gender;
                        originalTerm.MaritalStatus = Employee.MaritalStatus;
                        originalTerm.Nationality = Employee.Nationality;
                        originalTerm.PAN = Employee.PAN;
                        originalTerm.Aadhaar = Employee.Aadhaar;
                        originalTerm.Email = Employee.Email;
                        originalTerm.Mobile = Employee.Mobile;
                        originalTerm.PresentAddress = Employee.PresentAddress;
                        originalTerm.PermanentAddress = Employee.PermanentAddress;
                        originalTerm.State = Employee.State;
                        originalTerm.City = Employee.City;
                        originalTerm.PinCode = Employee.PinCode;
                        originalTerm.DOJ = Employee.DOJ;
                        originalTerm.ConfirmationDate = Employee.ConfirmationDate;
                        originalTerm.Department = Employee.Department;
                        originalTerm.Designation = Employee.Designation;
                        originalTerm.Grade = Employee.Grade;
                        originalTerm.EmploymentType = Employee.EmploymentType;
                        originalTerm.WorkLocation = Employee.WorkLocation;
                        originalTerm.ReportingManager = Employee.ReportingManager;
                        originalTerm.ExitDate = Employee.ExitDate;
                        originalTerm.ExitReason = Employee.ExitReason;
                        originalTerm.ExitType = Employee.ExitType;
                        originalTerm.UAN = Employee.UAN;
                        originalTerm.PFAccountNumber = Employee.PFAccountNumber;
                        originalTerm.ESIC_IP = Employee.ESIC_IP;
                        originalTerm.PTState = Employee.PTState;
                        originalTerm.ActiveStatus = Employee.ActiveStatus;
                        originalTerm.IsDeleted = Employee.IsDeleted;


                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                
                
                return Task.FromResult(new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Employee Details Saved Successfully."
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

        public async Task<ManagerBaseResponse<bool>> RemoveEmployeeData(string EmployeeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _context.Employees.Where(x => x.EmployeeID.ToString() == EmployeeID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Employees.RemoveRange(Employee);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Employee Data Removed Successfully.",
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

        public async Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompany(string CompanyID)
        {
            try
            {
                var Employee = await _context.Employees.AsQueryable().OrderBy(x => x.CompanyID).ToListAsync();

                return new ManagerBaseResponse<List<Employees>>
                {
                    IsSuccess = true,
                    Result = Employee,
                    Message = "Employee Details Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Employees>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID)
        {
            try
            {
                var Employee = await _context.Employees.AsQueryable().Where(x => x.CompanyID.ToString() == CompanyID && x.SubcontractorID.ToString() == SubcontractorID).ToListAsync();

                if (Employee == null)
                {

                    return new ManagerBaseResponse<List<Employees>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "CompanyID and SubcontractorID are required.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<Employees>>
                    {
                        IsSuccess = true,
                        Result = Employee,
                        Message = "Employee Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Employees>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<List<Employees>>> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID)
        {
            try
            {
                var Employee = await _context.Employees.AsQueryable().Where(x => x.CompanyID.ToString() == CompanyID && x.EmployeeID.ToString() == EmployeeID).ToListAsync();

                if (Employee == null)
                {

                    return new ManagerBaseResponse<List<Employees>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "CompanyID and EmployeeID are required.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<Employees>>
                    {
                        IsSuccess = true,
                        Result = Employee,
                        Message = "Employee Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Employees>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
    }
}