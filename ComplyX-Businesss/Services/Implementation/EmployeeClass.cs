using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
 
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
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX_Businesss.Models.Employee;
using ComplyX.Data.Entities;
using ComplyX.Repositories.UnitOfWork;
using AutoMapper;
using ComplyX_Businesss.Models.GratuityPolicy;
using ComplyX_Businesss.Models.GratuityTransaction;
using ComplyX_Businesss.Models.FnFCalculation;
using ComplyX_Businesss.Models.ComplianceSchedule;
using Microsoft.Graph.Models.Security;
using Microsoft.Graph.Models;
using Nest;
using static NHibernate.Engine.Query.CallableParser;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ComplyX.BusinessLogic
{
    public class EmployeeClass : IEmployeeServices
    {

        private readonly Dictionary<string, string> orderByTranslations = new Dictionary<string, string>
        {
            { "name", "Name" }
        };

        private readonly IMapper _mapper;
        private readonly AppContext _context;
        private readonly IUnitOfWork _UnitOfWork;

        public EmployeeClass(AppContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ManagerBaseResponse<bool>> SaveEmployeeData(EmployeeRequestModel Employee)
        {
            
            
            var response = new ManagerBaseResponse<List<Employee>>();

            try
            {
                if (Employee.EmployeeId == 0)
                {
                    var baemployee = _mapper.Map<Employee>(Employee);
                    if (Employee.Aadhaar.Contains(" "))
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            IsSuccess = false,
                            Message = "Aadhaar number should not contain spaces."
                        };
                    }
                    await _UnitOfWork.EmployeeRespositories.AddAsync(baemployee);
                    await _UnitOfWork.CommitAsync();

                    var responses = _mapper.Map<EmployeeResponseModel>(baemployee);
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        IsSuccess = true,
                        Message = "Employee Data Saved Successfully."
                    };
                }
                else
                {
                    var existingEmployee = await _UnitOfWork.EmployeeRespositories
                         .GetQueryable().Where(x => x.EmployeeId == Employee.EmployeeId).FirstOrDefaultAsync();
                   
                    if (Employee.Aadhaar.Contains(" "))
                    {
                        return new ManagerBaseResponse<bool>
                        {
                            Result = false,
                            IsSuccess = false,
                            Message = "Aadhaar number should not contain spaces."
                        };
                    }
                    _mapper.Map(Employee, existingEmployee);
                    _UnitOfWork.EmployeeRespositories.Update(existingEmployee);
                    await _UnitOfWork.CommitAsync();

                    var responses = _mapper.Map<EmployeeResponseModel>(existingEmployee);
                    return new ManagerBaseResponse<bool>
                    {
                        Result = true,
                        IsSuccess = true,
                        Message = "Employee Data Updated Successfully."
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
        public async Task<ManagerBaseResponse<bool>> RemoveEmployeeData(string EmployeeID)
        {
            try
            {
                // Get all report detail definitions for the given report name
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().Where(x => x.EmployeeId.ToString() == EmployeeID).ToListAsync();

                if (string.IsNullOrEmpty(Employee.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.EmployeeRespositories.RemoveRange(Employee);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompany(string CompanyID)
        {
            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().OrderBy(x => x.CompanyId)
                      .Select(x => new EmployeeResponseModel
                      {
                          EmployeeId = x.EmployeeId,
                          CompanyId = x.CompanyId,
                          SubcontractorId = x.SubcontractorId,
                          EmployeeCode = x.EmployeeCode,
                          FirstName = x.FirstName,
                          LastName = x.LastName,
                          FatherSpouseName = x.FatherSpouseName,
                          Dob = x.Dob,
                          Gender = x.Gender,
                          MaritalStatus = x.MaritalStatus,
                          Nationality = x.Nationality,
                          Pan = x.Pan,
                          Aadhaar = x.Aadhaar,
                          Mobile = x.Mobile,
                          Email = x.Email,
                          PresentAddress = x.PresentAddress,
                          PermanentAddress = x.PermanentAddress,
                          City = x.City,
                          State = x.State,
                          Pincode = x.Pincode,
                          Doj = x.Doj,
                          ConfirmationDate = x.ConfirmationDate,
                          Designation = x.Designation,
                          Department = x.Department,
                          Grade = x.Grade,
                          EmploymentType = x.EmploymentType,
                          WorkLocation = x.WorkLocation,
                          ReportingManager = x.ReportingManager,
                          ExitDate = x.ExitDate,
                          ExitType = x.ExitType,
                          ExitReason = x.ExitReason,
                          Uan = x.Uan,
                          PfaccountNumber = x.PfaccountNumber,
                          EsicIp = x.EsicIp,
                          Ptstate = x.Ptstate,
                          ActiveStatus = x.ActiveStatus,
                          IsDeleted = x.IsDeleted
                      }).ToListAsync();

                if(Employee.Count == 0)
                {
                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Employee Details not Retrieved.",
                    };
                }
                else
                {

                
                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = true,
                        Result = Employee,
                        Message = "Employee Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<EmployeeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

        public async Task<ManagerBaseResponse<IEnumerable<CommonDropdownModel>>> GetEmployeeData(PagedListCriteria PagedListCriteria)
        {
            try
            {
                var query = _UnitOfWork.EmployeeRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmployeeId);
                var responseQuery = query 
               
                      .Select(x => new CommonDropdownModel
                      {
                          id = x.EmployeeId,
                          name = x.FirstName + ' ' + x.LastName
                         
                      });
                PageListed<CommonDropdownModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);


                if (result.Data.Count > 0)
                {
                    return new ManagerBaseResponse<IEnumerable<CommonDropdownModel>>
                {
                    Result = result.Data,
                    IsSuccess = true,
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
                else
                {
                    return new ManagerBaseResponse<IEnumerable<CommonDropdownModel>>
                    {
                        Result = result.Data,
                        IsSuccess = false,
                        Message = "Employee Data not Retrieved.",

                    };
                }

            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<IEnumerable<CommonDropdownModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompanySubcontractor(string CompanyID, string SubcontractorID)
        {
            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().Where(x => x.CompanyId.ToString() == CompanyID && x.SubcontractorId.ToString() == SubcontractorID)
                      .Select(x => new EmployeeResponseModel
                      {
                          EmployeeId = x.EmployeeId,
                          CompanyId = x.CompanyId,
                          SubcontractorId = x.SubcontractorId,
                          EmployeeCode = x.EmployeeCode,
                          FirstName = x.FirstName,
                          LastName = x.LastName,
                          FatherSpouseName = x.FatherSpouseName,
                          Dob = x.Dob,
                          Gender = x.Gender,
                          MaritalStatus = x.MaritalStatus,
                          Nationality = x.Nationality,
                          Pan = x.Pan,
                          Aadhaar = x.Aadhaar,
                          Mobile = x.Mobile,
                          Email = x.Email,
                          PresentAddress = x.PresentAddress,
                          PermanentAddress = x.PermanentAddress,
                          City = x.City,
                          State = x.State,
                          Pincode = x.Pincode,
                          Doj = x.Doj,
                          ConfirmationDate = x.ConfirmationDate,
                          Designation = x.Designation,
                          Department = x.Department,
                          Grade = x.Grade,
                          EmploymentType = x.EmploymentType,
                          WorkLocation = x.WorkLocation,
                          ReportingManager = x.ReportingManager,
                          ExitDate = x.ExitDate,
                          ExitType = x.ExitType,
                          ExitReason = x.ExitReason,
                          Uan = x.Uan,
                          PfaccountNumber = x.PfaccountNumber,
                          EsicIp = x.EsicIp,
                          Ptstate = x.Ptstate,
                          ActiveStatus = x.ActiveStatus,
                          IsDeleted = x.IsDeleted
                      }).ToListAsync();

                if (Employee == null)
                {

                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "CompanyID and SubcontractorID are required.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = true,
                        Result = Employee,
                        Message = "Employee Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<EmployeeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<List<EmployeeResponseModel>>> GetEmployeesByCompanyEmployee(string CompanyID, string EmployeeID)
        {
            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().Where(x => x.CompanyId.ToString() == CompanyID && x.EmployeeId.ToString() == EmployeeID)
                      .Select(x => new EmployeeResponseModel
                      {
                          EmployeeId = x.EmployeeId,
                          CompanyId = x.CompanyId,
                          SubcontractorId = x.SubcontractorId,
                          EmployeeCode = x.EmployeeCode,
                          FirstName = x.FirstName,
                          LastName = x.LastName,
                          FatherSpouseName = x.FatherSpouseName,
                          Dob = x.Dob,
                          Gender = x.Gender,
                          MaritalStatus = x.MaritalStatus,
                          Nationality = x.Nationality,
                          Pan = x.Pan,
                          Aadhaar = x.Aadhaar,
                          Mobile = x.Mobile,
                          Email = x.Email,
                          PresentAddress = x.PresentAddress,
                          PermanentAddress = x.PermanentAddress,
                          City = x.City,
                          State = x.State,
                          Pincode = x.Pincode,
                          Doj = x.Doj,
                          ConfirmationDate = x.ConfirmationDate,
                          Designation = x.Designation,
                          Department = x.Department,
                          Grade = x.Grade,
                          EmploymentType = x.EmploymentType,
                          WorkLocation = x.WorkLocation,
                          ReportingManager = x.ReportingManager,
                          ExitDate = x.ExitDate,
                          ExitType = x.ExitType,
                          ExitReason = x.ExitReason,
                          Uan = x.Uan,
                          PfaccountNumber = x.PfaccountNumber,
                          EsicIp = x.EsicIp,
                          Ptstate = x.Ptstate,
                          ActiveStatus = x.ActiveStatus,
                          IsDeleted = x.IsDeleted
                      }).ToListAsync();

                if (Employee == null)
                {

                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "CompanyID and EmployeeID are required.",
                    };
                }
                else
                {
                    return new ManagerBaseResponse<List<EmployeeResponseModel>>
                    {
                        IsSuccess = true,
                        Result = Employee,
                        Message = "Employee Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<EmployeeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<EmployeeResponseModel>>> GetEmployeeDataFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {
                 
                var query = from employee in  _UnitOfWork.EmployeeRespositories.GetQueryable()
                    join company in _UnitOfWork.CompanyRepository.GetQueryable()
                    on employee.CompanyId equals company.CompanyId
                            join Subcontractor in _UnitOfWork.SubcontractorRepository.GetQueryable()
                            on employee.SubcontractorId equals Subcontractor.SubcontractorId
                            select( new EmployeeResponseModel

                            {
                                EmployeeId = employee.EmployeeId,
                                CompanyId = employee.CompanyId,
                                CompanyName = company.Name,
                                SubcontractorId = employee.SubcontractorId,
                                SubcontractorName = Subcontractor.Name,
                                EmployeeCode = employee.EmployeeCode,
                                FirstName = employee.FirstName,
                                LastName = employee.LastName,
                                FatherSpouseName = employee.FatherSpouseName,
                                Dob = employee.Dob,
                                Gender = employee.Gender,
                                MaritalStatus = employee.MaritalStatus,
                                Nationality = employee.Nationality,
                                Pan = employee.Pan,
                                Aadhaar = employee.Aadhaar,
                                Mobile = employee.Mobile,
                                Email = employee.Email,
                                PresentAddress = employee.PresentAddress,
                                PermanentAddress = employee.PermanentAddress,
                                City = employee.City,
                                State = employee.State,
                                Pincode = employee.Pincode,
                                Doj = employee.Doj,
                                ConfirmationDate = employee.ConfirmationDate,
                                Designation = employee.Designation,
                                Department = employee.Department,
                                Grade = employee.Grade,
                                EmploymentType = employee.EmploymentType,
                                WorkLocation = employee.WorkLocation,
                                ReportingManager = employee.ReportingManager,
                                ExitDate = employee.ExitDate,
                                ExitType = employee.ExitType,
                                ExitReason = employee.ExitReason,
                                Uan = employee.Uan,
                                PfaccountNumber = employee.PfaccountNumber,
                                EsicIp = employee.EsicIp,
                                Ptstate = employee.Ptstate,
                                ActiveStatus = employee.ActiveStatus,
                                IsDeleted = employee.IsDeleted
                            });
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.EmployeeId);
                var responseQuery = query.Select(x => new EmployeeResponseModel
                
                   {
                        EmployeeId = x.EmployeeId,
                        CompanyId = x.CompanyId,
                        CompanyName = x.CompanyName,
                        SubcontractorId = x.SubcontractorId,
                        SubcontractorName = x.SubcontractorName,
                        EmployeeCode = x.EmployeeCode,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        FatherSpouseName = x.FatherSpouseName,
                        Dob = x.Dob,
                        Gender = x.Gender,
                        MaritalStatus = x.MaritalStatus,
                        Nationality = x.Nationality,
                        Pan = x.Pan,
                        Aadhaar = x.Aadhaar,
                        Mobile = x.Mobile,
                        Email = x.Email,
                        PresentAddress = x.PresentAddress,
                        PermanentAddress = x.PermanentAddress,
                        City = x.City,
                        State = x.State,
                        Pincode = x.Pincode,
                        Doj = x.Doj,
                        ConfirmationDate = x.ConfirmationDate,
                        Designation = x.Designation,
                        Department = x.Department,
                        Grade = x.Grade,
                        EmploymentType = x.EmploymentType,
                        WorkLocation = x.WorkLocation,
                        ReportingManager = x.ReportingManager,
                        ExitDate = x.ExitDate,
                        ExitType = x.ExitType,
                        ExitReason = x.ExitReason,
                        Uan = x.Uan,
                        PfaccountNumber = x.PfaccountNumber,
                        EsicIp = x.EsicIp,
                        Ptstate = x.Ptstate,
                        ActiveStatus = x.ActiveStatus,
                        IsDeleted = x.IsDeleted
                });
                PageListed<EmployeeResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<EmployeeResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<EmployeeResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGratuity_PolicyData(GratuityPolicyRequestModel Gratuity_Policy)
        {
            var response = new ManagerBaseResponse<List<GratuityPolicy>>();

            try
            {
                var Company = await _UnitOfWork.CompanyRepository.GetQueryable().FirstOrDefaultAsync(x => x.CompanyId == Gratuity_Policy.CompanyId);

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

                    if (Gratuity_Policy.PolicyId == Guid.Empty)
                    {
                        // Insert
                        GratuityPolicy _model = new GratuityPolicy();
                         
                            _model.PolicyId = Guid.NewGuid();
                       
                        _model.CompanyId = Gratuity_Policy.CompanyId;
                        _model.MinimumServiceYears = Gratuity_Policy.MinimumServiceYears;
                        _model.Formula = Gratuity_Policy.Formula;
                        _model.MaxGratuityAmount = Gratuity_Policy.MaxGratuityAmount;
                        _model.Eligible = Gratuity_Policy.Eligible;
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                        await _UnitOfWork.GratuityPolicyRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.GratuityPolicyRespositories.GetQueryable()
                            .Where(x => x.PolicyId == Gratuity_Policy.PolicyId)
                                .FirstOrDefault();
                        originalTerm.CompanyId = Gratuity_Policy.CompanyId;
                        originalTerm.MinimumServiceYears = Gratuity_Policy.MinimumServiceYears;
                        originalTerm.Formula = Gratuity_Policy.Formula;
                        originalTerm.MaxGratuityAmount = Gratuity_Policy.MaxGratuityAmount;
                        originalTerm.Eligible = Gratuity_Policy.Eligible;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var Gratuity_Policy = await _UnitOfWork.GratuityPolicyRespositories.GetQueryable().Where(x => x.PolicyId.ToString() == PolicyID).ToListAsync();

                if (string.IsNullOrEmpty(Gratuity_Policy.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Gratuity Policy Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.GratuityPolicyRespositories.RemoveRange(Gratuity_Policy);
                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<GratuityPolicyResponseModel>>> GetGratuity_Policy(string PolicyID)
        {
            try
            {
                var Gratuity_Policy = await _UnitOfWork.GratuityPolicyRespositories.GetQueryable().Where(x => x.PolicyId.ToString() == PolicyID).Select(x => new GratuityPolicyResponseModel
                {
                    PolicyId = x.PolicyId,
                    MinimumServiceYears = x.MinimumServiceYears,
                    Formula = x.Formula,
                    MaxGratuityAmount = x.MaxGratuityAmount,
                    Eligible = x.Eligible,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    CompanyId = x.CompanyId
                }).ToListAsync();

                if (Gratuity_Policy.Count == 0)
                {
                    return new ManagerBaseResponse<List<GratuityPolicyResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Gratuity Policy Details Retrieved Successfully.",
                    };
                }
                else
                {


                    return new ManagerBaseResponse<List<GratuityPolicyResponseModel>>
                    {
                        IsSuccess = true,
                        Result = Gratuity_Policy,
                        Message = "Gratuity Policy Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GratuityPolicyResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GratuityPolicyResponseModel>>> GetGratuity_PolicyFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.GratuityPolicyRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.Formula.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.PolicyId);
                var responseQuery = query.Select(x => new GratuityPolicyResponseModel
                {

                    PolicyId = x.PolicyId,
                    MinimumServiceYears = x.MinimumServiceYears,
                    Formula = x.Formula,
                    MaxGratuityAmount = x.MaxGratuityAmount,
                    Eligible = x.Eligible,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    CompanyId = x.CompanyId
                });
                PageListed<GratuityPolicyResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<GratuityPolicyResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<GratuityPolicyResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveGratuity_TransactionsData(GratuityTransactionRequestModel Gratuity_Transactions)
        {
            var response = new ManagerBaseResponse<List<GratuityTransaction>>();

            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.EmployeeId == Gratuity_Transactions.EmployeeId && x.CompanyId == Gratuity_Transactions.CompanyId);

                if (Employee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee and Company are not found.",
                    };
                }
                else
                {
                    if (Gratuity_Transactions.GratuityId == Guid.Empty)
                    {
                        // Insert
                        GratuityTransaction _model = new GratuityTransaction();
                        _model.GratuityId = Guid.NewGuid();
                        _model.EmployeeId = Employee.EmployeeId;
                        _model.CompanyId = Gratuity_Transactions.CompanyId;
                        _model.LastDrawnSalary  =  Gratuity_Transactions.LastDrawnSalary;
                        _model.YearsOfService = Gratuity_Transactions.YearsOfService;
                        _model.GratuityAmount = Gratuity_Transactions.GratuityAmount;
                        _model.PaymentStatus = Gratuity_Transactions.PaymentStatus; 
                        _model.PaidDate = Gratuity_Transactions.PaidDate;
                        _model.ApprovedBy = Guid.NewGuid();
                        _model.CreatedAt = Util.GetCurrentCSTDateAndTime();

                       await  _UnitOfWork.GratuityTransactionRespositories.AddAsync( _model );
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.GratuityTransactionRespositories.GetQueryable()
                            .Where(x => x.GratuityId == Gratuity_Transactions.GratuityId)
                                .FirstOrDefault();
                        originalTerm.EmployeeId = Employee.EmployeeId;
                        originalTerm.CompanyId = Gratuity_Transactions.CompanyId;
                        originalTerm.LastDrawnSalary = Gratuity_Transactions.LastDrawnSalary;
                        originalTerm.YearsOfService = Gratuity_Transactions.YearsOfService;
                        originalTerm.GratuityAmount = Gratuity_Transactions.GratuityAmount;
                        originalTerm.PaymentStatus = Gratuity_Transactions.PaymentStatus;
                        originalTerm.PaidDate = Gratuity_Transactions.PaidDate;
                        originalTerm.ApprovedBy = Gratuity_Transactions.ApprovedBy;
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                       
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var Gratuity_Transactions = await _UnitOfWork.GratuityTransactionRespositories.GetQueryable().Where(x => x.GratuityId.ToString() == GratuityID).ToListAsync();

                if (string.IsNullOrEmpty(Gratuity_Transactions.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Gratuity Transactions Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.GratuityTransactionRespositories.RemoveRange(Gratuity_Transactions);

           await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<GratuityTransactionResponseModel>>> GetGratuity_Transactions(string GratuityID)
        {
            try
            {
                var Gratuity_Transactions = await _UnitOfWork.GratuityTransactionRespositories.GetQueryable().Where(x => x.GratuityId.ToString() == GratuityID)
                     .Select(x => new GratuityTransactionResponseModel
                     {
                         GratuityId = x.GratuityId,
                         LastDrawnSalary = x.LastDrawnSalary,
                         YearsOfService = x.YearsOfService,
                         GratuityAmount = x.GratuityAmount,
                         PaymentStatus = x.PaymentStatus,
                         PaidDate = x.PaidDate,
                         ApprovedBy = x.ApprovedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt,
                         EmployeeId = x.EmployeeId,
                         CompanyId = x.CompanyId
                     }).ToListAsync();

                if (Gratuity_Transactions.Count == 0)
                {
                    return new ManagerBaseResponse<List<GratuityTransactionResponseModel>>
                    {
                        IsSuccess = false,
                        Result = null,
                        Message = "Gratuity Transactions Details not Retrieved.",
                    };
                }
                else
                {


                    return new ManagerBaseResponse<List<GratuityTransactionResponseModel>>
                    {
                        IsSuccess = true,
                        Result = Gratuity_Transactions,
                        Message = "Gratuity Transactions Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<GratuityTransactionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<GratuityTransactionResponseModel>>> GetGratuity_TransactionsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.GratuityTransactionRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PaymentStatus.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.GratuityId);
                var responseQuery = query.Select(x => new GratuityTransactionResponseModel
                {

                    GratuityId = x.GratuityId,
                    LastDrawnSalary = x.LastDrawnSalary,
                    YearsOfService = x.YearsOfService,
                    GratuityAmount = x.GratuityAmount,
                    PaymentStatus = x.PaymentStatus,
                    PaidDate = x.PaidDate,
                    ApprovedBy = x.ApprovedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    EmployeeId = x.EmployeeId,
                    CompanyId = x.CompanyId
                });
                PageListed<GratuityTransactionResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<GratuityTransactionResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<GratuityTransactionResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }
        public async Task<ManagerBaseResponse<bool>> SaveFnF_CalculationsData(FnFCalculationRequestModel FnF_Calculations)
        {
            var response = new ManagerBaseResponse<List<FnFCalculation>>();

            try
            {
                var Employee = await _UnitOfWork.EmployeeRespositories.GetQueryable().FirstOrDefaultAsync(x => x.EmployeeId == FnF_Calculations.EmployeeId && x.CompanyId == FnF_Calculations.CompanyId);

                if (Employee == null)
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "Employee and Company are not found.",
                    };
                }
                else
                {
                    if (FnF_Calculations.FnFid == Guid.Empty)
                    {
                        // Insert
                        FnFCalculation _model = new FnFCalculation();
                        _model.FnFid = Guid.NewGuid();
                        _model.EmployeeId = FnF_Calculations.EmployeeId;
                        _model.CompanyId = FnF_Calculations.CompanyId;
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

                       await _UnitOfWork.FCalculationRespositories.AddAsync(_model);
                    }
                    else
                    {
                        // Update
                        var originalTerm = _UnitOfWork.FCalculationRespositories.GetQueryable()
                            .Where(x => x.FnFid == FnF_Calculations.FnFid)
                                .FirstOrDefault();
                        originalTerm.EmployeeId = FnF_Calculations.EmployeeId;
                        originalTerm.CompanyId = FnF_Calculations.CompanyId;
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
                        originalTerm.UpdatedAt = Util.GetCurrentCSTDateAndTime();

                        
                    }
                }
                await _UnitOfWork.CommitAsync();
                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
                var FnF_Calculations = await _UnitOfWork.FCalculationRespositories.GetQueryable().Where(x => x.FnFid.ToString() == FnFID).ToListAsync();

                if (string.IsNullOrEmpty(FnF_Calculations.ToString()))
                {
                    return new ManagerBaseResponse<bool>
                    {
                        Result = false,
                        Message = "FnF Calculations Id is not Vaild",
                    };
                }

                // Remove all related report details
                _UnitOfWork.FCalculationRespositories.RemoveRange(FnF_Calculations);

                await _UnitOfWork.CommitAsync();

                return new ManagerBaseResponse<bool>
                {
                    Result = true,IsSuccess = true,
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
        public async Task<ManagerBaseResponse<List<FnFCalculationResponseModel >>> GetFnF_Calculations(string FnFID)
        {
            try
            {
                var FnF_Calculations = await _UnitOfWork.FCalculationRespositories.GetQueryable().Where(x => x.FnFid.ToString() == FnFID)
                    .Select(x => new FnFCalculationResponseModel
                    {
                        FnFid = x.FnFid,
                        ResignationDate = x.ResignationDate,
                        LastWorkingDate = x.LastWorkingDate,
                        NoticePeriodServedDays = x.NoticePeriodServedDays,
                        SalaryDue = x.SalaryDue,
                        LeaveEncashmentAmount = x.LeaveEncashmentAmount,
                        GratuityAmount = x.GratuityAmount,
                        Bonus = x.Bonus,
                        Deductions = x.Deductions,
                        NetPayable = x.NetPayable,
                        ProcessedBy = x.ProcessedBy,
                        PaymentStatus = x.PaymentStatus,
                        ProcessedDate = x.ProcessedDate,
                        Remarks = x.Remarks,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        EmployeeId = x.EmployeeId,
                        CompanyId = x.CompanyId
                    }).ToListAsync();

                if(FnF_Calculations.Count == 0)
                {
                    return new ManagerBaseResponse<List<FnFCalculationResponseModel>>
                    {
                        IsSuccess = false ,
                        Result = null,
                        Message = "FnF Calculations Details Retrieved Successfully.",
                    };
                }
                else
                {

               
                    return new ManagerBaseResponse<List<FnFCalculationResponseModel>>
                    {
                        IsSuccess = true,
                        Result = FnF_Calculations,
                        Message = "FnF Calculations Details Retrieved Successfully.",
                    };
                }
            }
            catch (Exception ex)
            {

                return new ManagerBaseResponse<List<FnFCalculationResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
               
            }
        }
        public async Task<ManagerBaseResponse<IEnumerable<FnFCalculationResponseModel>>> GetFnF_CalculationsFilter(PagedListCriteria PagedListCriteria)
        {
            try
            {

                var query = _UnitOfWork.FCalculationRespositories.GetQueryable();
                var searchText = PagedListCriteria.SearchText?.Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    query = query.Where(x => x.PaymentStatus.ToLower().Contains(searchText.ToLower()));
                }

                query = query.OrderBy(a => a.FnFid);
                var responseQuery = query.Select(x => new FnFCalculationResponseModel
                {

                    FnFid = x.FnFid,
                    ResignationDate = x.ResignationDate,
                    LastWorkingDate = x.LastWorkingDate,
                    NoticePeriodServedDays = x.NoticePeriodServedDays,
                    SalaryDue = x.SalaryDue,
                    LeaveEncashmentAmount = x.LeaveEncashmentAmount,
                    GratuityAmount = x.GratuityAmount,
                    Bonus = x.Bonus,
                    Deductions = x.Deductions,
                    NetPayable = x.NetPayable,
                    ProcessedBy = x.ProcessedBy,
                    PaymentStatus = x.PaymentStatus,
                    ProcessedDate = x.ProcessedDate,
                    Remarks = x.Remarks,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    EmployeeId = x.EmployeeId,
                    CompanyId = x.CompanyId
                });
                PageListed<FnFCalculationResponseModel> result = await responseQuery.ToPagedListAsync(PagedListCriteria, orderByTranslations);
                return new ManagerBaseResponse<IEnumerable<FnFCalculationResponseModel>>
                {
                    Result = result.Data,IsSuccess = true,
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

                return new ManagerBaseResponse<IEnumerable<FnFCalculationResponseModel>>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = ex.Message
                };
            }
        }

    }
}