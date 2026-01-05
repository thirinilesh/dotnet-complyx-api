using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using ComplyX.Shared.Data;
using ComplyX.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;

using System.Linq;
using System.Runtime.InteropServices;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using ComplyX_Businesss.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComplyX.BusinessLogic
{
    public class EmployeeClass : IEmployeeServices
    {

        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

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
        public async Task<ManagerBaseResponse<IEnumerable<Employees>>> GetEmployeeDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Employees.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmployeeID);

                PageListed<Employees> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<Employees>>
                {
                    Result = result.Data,
                    Message = "Employee Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<Employees>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGratuity_PolicyData(Gratuity_Policy Gratuity_Policy)
        {
            var response = new ManagerBaseResponse<List<Gratuity_Policy>>();

            try
            {
                var Company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyID == Gratuity_Policy.CompanyID);

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

                    if (Gratuity_Policy.PolicyID == Guid.Empty)
                    {
                        // Insert
                        Gratuity_Policy _model = new Gratuity_Policy();
                         
                            _model.PolicyID = Guid.NewGuid();
                       
                        _model.CompanyID = Gratuity_Policy.CompanyID;
                        _model.MinimumServiceYears = Gratuity_Policy.MinimumServiceYears;
                        _model.Formula = Gratuity_Policy.Formula;
                        _model.MaxGratuityAmount = Gratuity_Policy.MaxGratuityAmount;
                        _model.Eligible = Gratuity_Policy.Eligible;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Gratuity_Policy
                            .Where(x => x.PolicyID == Gratuity_Policy.PolicyID)
                                .FirstOrDefault();
                        originalTerm.CompanyID = Gratuity_Policy.CompanyID;
                        originalTerm.MinimumServiceYears = Gratuity_Policy.MinimumServiceYears;
                        originalTerm.Formula = Gratuity_Policy.Formula;
                        originalTerm.MaxGratuityAmount = Gratuity_Policy.MaxGratuityAmount;
                        originalTerm.Eligible = Gratuity_Policy.Eligible;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Gratuity Policy Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveGratuity_PolicyData(string PolicyID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Gratuity_Policy = await _context.Gratuity_Policy.Where(x => x.PolicyID.ToString() == PolicyID).ToListAsync();

                if (string.IsNullOrEmpty(Gratuity_Policy.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Gratuity Policy Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Gratuity_Policy.RemoveRange(Gratuity_Policy);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Gratuity Policy Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<Gratuity_Policy>>> GetGratuity_Policy(string PolicyID)
        {
            try
            {
                var Gratuity_Policy = await _context.Gratuity_Policy.AsQueryable().OrderBy(x => x.PolicyID).ToListAsync();

                return new ManagerBaseResponse<List<Gratuity_Policy>>
                {
                    IsSuccess = true,
                    Result = Gratuity_Policy,
                    Message = "Gratuity Policy Details Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Gratuity_Policy>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Gratuity_Policy>>> GetGratuity_PolicyFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Gratuity_Policy.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Formula.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PolicyID);

                PageListed<Gratuity_Policy> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<Gratuity_Policy>>
                {
                    Result = result.Data,
                    Message = "Gratuity Policy Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<Gratuity_Policy>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGratuity_TransactionsData(Gratuity_Transactions Gratuity_Transactions)
        {
            var response = new ManagerBaseResponse<List<Gratuity_Transactions>>();

            try
            {
                var Employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == Gratuity_Transactions.EmployeeID);

                if (Employee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee is not found.",
                    };
                }
                else
                {
                    if (Gratuity_Transactions.GratuityID == Guid.Empty)
                    {
                        // Insert
                        Gratuity_Transactions _model = new Gratuity_Transactions();
                        _model.GratuityID = Guid.NewGuid();
                        _model.EmployeeID = Employee.EmployeeID;
                        _model.LastDrawnSalary  =  Gratuity_Transactions.LastDrawnSalary;
                        _model.YearsOfService = Gratuity_Transactions.YearsOfService;
                        _model.GratuityAmount = Gratuity_Transactions.GratuityAmount;
                        _model.PaymentStatus = Gratuity_Transactions.PaymentStatus; 
                        _model.PaidDate = Gratuity_Transactions.PaidDate;
                        _model.ApprovedBy = Guid.NewGuid();
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.Gratuity_Transactions
                            .Where(x => x.GratuityID == Gratuity_Transactions.GratuityID)
                                .FirstOrDefault();
                        originalTerm.EmployeeID = Employee.EmployeeID;
                        originalTerm.LastDrawnSalary = Gratuity_Transactions.LastDrawnSalary;
                        originalTerm.YearsOfService = Gratuity_Transactions.YearsOfService;
                        originalTerm.GratuityAmount = Gratuity_Transactions.GratuityAmount;
                        originalTerm.PaymentStatus = Gratuity_Transactions.PaymentStatus;
                        originalTerm.PaidDate = Gratuity_Transactions.PaidDate;
                        originalTerm.ApprovedBy = Gratuity_Transactions.ApprovedBy;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Gratuity Transactions Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveGratuity_TransactionsData(string GratuityID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Gratuity_Transactions = await _context.Gratuity_Transactions.Where(x => x.GratuityID.ToString() == GratuityID).ToListAsync();

                if (string.IsNullOrEmpty(Gratuity_Transactions.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Gratuity Transactions Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.Gratuity_Transactions.RemoveRange(Gratuity_Transactions);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "Gratuity Transactions Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<Gratuity_Transactions>>> GetGratuity_Transactions(string GratuityID)
        {
            try
            {
                var Gratuity_Transactions = await _context.Gratuity_Transactions.AsQueryable().OrderBy(x => x.GratuityID).ToListAsync();

                return new ManagerBaseResponse<List<Gratuity_Transactions>>
                {
                    IsSuccess = true,
                    Result = Gratuity_Transactions,
                    Message = "Gratuity Transactions Details Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<Gratuity_Transactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<Gratuity_Transactions>>> GetGratuity_TransactionsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.Gratuity_Transactions.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PaymentStatus.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.GratuityID);

                PageListed<Gratuity_Transactions> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<Gratuity_Transactions>>
                {
                    Result = result.Data,
                    Message = "Gratuity Transactions Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<Gratuity_Transactions>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveFnF_CalculationsData(FnF_Calculations FnF_Calculations)
        {
            var response = new ManagerBaseResponse<List<FnF_Calculations>>();

            try
            {
                var Employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeID == FnF_Calculations.EmployeeID);

                if (Employee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee is not found.",
                    };
                }
                else
                {
                    if (FnF_Calculations.FnFID == Guid.Empty)
                    {
                        // Insert
                        FnF_Calculations _model = new FnF_Calculations();
                        _model.FnFID = Guid.NewGuid();
                        _model.EmployeeID = FnF_Calculations.EmployeeID;
                        _model.ResignationDate = FnF_Calculations.ResignationDate;
                        _model.LastWorkingDate = FnF_Calculations.LastWorkingDate;
                        _model.NoticePeriodServedDays = FnF_Calculations.NoticePeriodServedDays;
                        _model.SalaryDue = FnF_Calculations.SalaryDue;
                        _model.LeaveEncashmentAmount    = FnF_Calculations.LeaveEncashmentAmount;
                        _model.GratuityAmount = FnF_Calculations.GratuityAmount;
                        _model.Bonus = FnF_Calculations.Bonus;
                        _model.Deductions = FnF_Calculations.Deductions;
                        _model.NetPayable = FnF_Calculations.NetPayable;
                        _model.PaymentStatus = FnF_Calculations.PaymentStatus;
                        _model.ProcessedBy = Guid.NewGuid();
                        _model.ProcessedDate    = FnF_Calculations.ProcessedDate;
                        _model.Remarks = FnF_Calculations.Remarks;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Add(_model);
                        _context.SaveChanges();
                    }
                    else
                    {
                        // Update
                        var originalTerm = _context.FnF_Calculations
                            .Where(x => x.FnFID == FnF_Calculations.FnFID)
                                .FirstOrDefault();
                        originalTerm.EmployeeID = FnF_Calculations.EmployeeID;
                        originalTerm.ResignationDate = FnF_Calculations.ResignationDate;
                        originalTerm.LastWorkingDate = FnF_Calculations.LastWorkingDate;
                        originalTerm.NoticePeriodServedDays = FnF_Calculations.NoticePeriodServedDays;
                        originalTerm.SalaryDue = FnF_Calculations.SalaryDue;
                        originalTerm.LeaveEncashmentAmount = FnF_Calculations.LeaveEncashmentAmount;
                        originalTerm.GratuityAmount = FnF_Calculations.GratuityAmount;
                        originalTerm.Bonus = FnF_Calculations.Bonus;
                        originalTerm.Deductions = FnF_Calculations.Deductions;
                        originalTerm.NetPayable = FnF_Calculations.NetPayable;
                        originalTerm.ProcessedBy = FnF_Calculations.ProcessedBy;
                        originalTerm.ProcessedDate = FnF_Calculations.ProcessedDate;
                        originalTerm.PaymentStatus = FnF_Calculations.PaymentStatus;
                        originalTerm.Remarks = FnF_Calculations.Remarks;
                        originalTerm.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        _context.Update(originalTerm);
                        _context.SaveChanges();
                    }
                }
                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "FnF Calculations Data Saved Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveFnF_CalculationsData(string FnFID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var FnF_Calculations = await _context.FnF_Calculations.Where(x => x.FnFID.ToString() == FnFID).ToListAsync();

                if (string.IsNullOrEmpty(FnF_Calculations.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "FnF Calculations Id is not Vaild",
                    };
                }

                // Remove all related report details
                _context.FnF_Calculations.RemoveRange(FnF_Calculations);

                await _context.SaveChangesAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,
                    Message = "FnF Calculations Data Removed Successfully.",
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
        public async Task<ManagerBaseResponse<List<FnF_Calculations >>> GetFnF_Calculations(string FnFID)
        {
            try
            {
                var FnF_Calculations = await _context.FnF_Calculations.AsQueryable().OrderBy(x => x.FnFID).ToListAsync();

                return new ManagerBaseResponse<List<FnF_Calculations>>
                {
                    IsSuccess = true,
                    Result = FnF_Calculations,
                    Message = "FnF Calculations Details Retrieved Successfully.",
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<FnF_Calculations>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<FnF_Calculations>>> GetFnF_CalculationsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _context.FnF_Calculations.AsQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PaymentStatus.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FnFID);

                PageListed<FnF_Calculations> result = await query.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<FnF_Calculations>>
                {
                    Result = result.Data,
                    Message = "FnF Calculations Data Retrieved Successfully.",
                    PageDetail = new PageDetailModel()
                    {
                        Skip = PagedListCriteria.Skip,
                        Take = PagedListCriteria.Take,
                        Count = result.TotalCount,
                        SearchText = PagedListCriteria.SearchText,
                        FilterdCount = PagedListCriteria.Filters
                    }
                };
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<FnF_Calculations>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}