using ComplyX.Repositories.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using ComplyX.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyX.Repositories.Repositories;
using ComplyX.Data.DbContexts;

namespace ComplyX.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext   dbcontext;
        private readonly AppDbContext appDbContext;
        public IProductOwnerRespositories ProductOwnerRepositories { get; }
        public ICompanyRespositories CompanyRepository { get; }
        public ISubscriptionPlanRespositories SubscriptionPlanRepository { get; }
        public IProductOwnerSubscriptionsRespostories ProductOwnerSubscriptions { get; }
        public ISubcontractorRespositories SubcontractorRepository { get; }
        public IPlanRespositories PlanRespositories { get; }
        public ISubscriptionInvoicesRespostories SubscriptionInvoices { get; }
        public IPaymentTransactionRespositories PaymentTransactionRespositories { get; }
        public ICustomerPaymentsRespositories CustomerPayments { get; }
        public IPartyMasterRespositories PartyMasterRepositories { get; }
        public ICompanyPartyRoleRespositories CompanyPartyRoleRepositories { get; }
        public IComplianceDeadlineRespositories ComplianceDeadlineRespositories { get; }
        public IComplianceScheduleRespositories ComplianceScheduleRespositories { get; }

        public IComplianceFilingRespositories ComplianceFilingRespositories { get; }
        public ILegalDocumentVersionRespositories LegalDocumentVersionRespositories { get; }
        public ILegalDocumentRespositories LegalDocumentRespositories { get; }
        public ILegalDocumentAcceptanceRespositories LegalDocumentAcceptanceRespositories { get; }
        public IMachineBindingRespositories MachineBindingRespositories { get; }
        public ILicenseActivationRespositories LicenseActivationRespositories { get; }
        public ILicenseKeyMasterRespositories LegalKeyMasterRepositories { get; }
        public ILicenseAuditLogRespositories LicenseAuditLogRespositories { get; }
        public IFilingStatusesRespositories FilingStatusesRespositories { get; }
        public IEmploymentTypeRespositories EmploymentTypeRespositories { get; }
        public IExitTypesRespositories ExitTypesRespositories { get; }
        public IPayrollDataRespositories PayrollDataRespositories { get; }
        public ILeaveEncashmentPolicyRespositories LeaveEncashmentPolicyRespositories { get; }
        public ILeaveEncashmentTransactionRespositories LeaveEncashmentTransactionRespositories { get; }
        public ICompanyEPFORespositories CompanyEPFORespositories { get; }
        public IEmployeeEPFORespositories EmployeeEPFORespositories { get; }
        public IEPFOECRFileRespositories EPFOECRFileRespositories { get; }
        public IEPFOPeriodRespositories ePFOPeriodRespositories { get; }
        public IEPFOMonthWageRespositories ePFOMonthWageRespositories { get; }
        public IEmployeeRespositories EmployeeRespositories { get; }
        public IGratuityPolicyRespositories GratuityPolicyRespositories { get; }
        public IGratuityTransactionRespositories GratuityTransactionRespositories { get; }
        public IFnFCalculationRespositories FCalculationRespositories { get; }
        public IGstSaleRespositories GgstSaleRespositories { get; }
        public IGstReturnRespositories ReturnRespositories { get; }
        public IGstPurchaseRespositories PurchaseRespositories { get; }
        public IGstInvoiceSeriesRespositories InvoiceSeriesRespositories { get; }
        public IGSTHSNSACRespositories GSTHSNSACRespositories { get; }
        public IGSTHSNMappingRespositories GSTHSNMappingRespositories { get; }
        public ITdsdeductorRespositories TdsdeductorRespositories { get; }
        public ITdsdeducteeRespositories TdsdeducteeRespositories { get; }
        public ITDSReturnRespositories TDSReturnRespositories { get; }
        public ITdsentryRespositories TdsentryRespositories { get; }
        public ITdschallanRespositories TdschallanRespositories { get; }
        public  ITdsreturnChallanRespositories TdsreturnChallanRespositories { get; }
        public ITdsreturnEntryRespositories TdsreturnEntryRespositories { get; }
        public ITdschallanAllocationRespostories TdschallanAllocationRespostories { get; }
        public ITdsrateRespostories TdsrateRespostories { get; }
        public IRegisterRespositories RegisterRespositories { get; }
        public IUserRespositories UserRespositories { get; }
        public UnitOfWork (DbContext dbcontext, IProductOwnerRespositories productOwnerRespositories,ICompanyRespositories companyRepositories,
            ISubscriptionPlanRespositories subscriptionPlanRespositories,IProductOwnerSubscriptionsRespostories productOwnerSubscriptionsRespostories, 
            ISubcontractorRespositories subcontractorRespositories, ISubscriptionInvoicesRespostories subscriptionInvoicesRespostories,
            IPaymentTransactionRespositories paymentTransactionRespositories, ICustomerPaymentsRespositories customerPaymentsRespositories,
            IPartyMasterRespositories partyMasterRespositories,ICompanyPartyRoleRespositories companyPartyRoleRespositories,
            IComplianceDeadlineRespositories complianceDeadlineRespositories, IComplianceScheduleRespositories complianceScheduleRespositories,
            IComplianceFilingRespositories complianceFilingRespositories, ILegalDocumentAcceptanceRespositories legalDocumentAcceptanceRespositories,
            ILegalDocumentRespositories legalDocumentRespositories,ILegalDocumentVersionRespositories legalDocumentVersionRespositories,
            ILicenseAuditLogRespositories licenseAuditLogRespositories , ILicenseActivationRespositories licenseActivationRespositories ,
            ILicenseKeyMasterRespositories  licenseKeyMasterRespositories, IMachineBindingRespositories machineBindingRespositories ,
            IExitTypesRespositories exitTypesRespositories ,IEmploymentTypeRespositories employmentTypeRespositories ,IFilingStatusesRespositories filingStatusesRespositories,
            IPayrollDataRespositories payrollDataRespositories,ILeaveEncashmentTransactionRespositories leaveEncashmentTransactionRespositories,
            ILeaveEncashmentPolicyRespositories leaveEncashmentPolicyRespositories,ICompanyEPFORespositories companyEPFORespositories,
            IEmployeeEPFORespositories employeeEPFORespositories,IEPFOECRFileRespositories ePFOECRFileRespositories,IEPFOMonthWageRespositories EPFOMonthWageRespositories,
            IEPFOPeriodRespositories EPFOPeriodRespositories,IEmployeeRespositories employeeRespositories,
            IGratuityPolicyRespositories gratuityPolicyRespositories,IGratuityTransactionRespositories gratuityTransactionRespositories,
           IFnFCalculationRespositories fnFCalculationRespositories,IGstSaleRespositories gstSaleRespositories,
           IGstReturnRespositories gstReturnRespositories,IGstPurchaseRespositories gstPurchaseRespositories,
           IGstInvoiceSeriesRespositories gstInvoiceSeriesRespositories,IGSTHSNMappingRespositories gSTHSNMappingRespositories,
           IGSTHSNSACRespositories gSTHSNSACRespositories,ITdsdeductorRespositories tdsdeductorRespositories,
           ITdsdeducteeRespositories tdsdeducteeRespositories,ITDSReturnRespositories tDSReturnRespositories,
           ITdsentryRespositories dsentryRespositories,ITdschallanRespositories tdschallanRespositories,
           ITdsreturnEntryRespositories tdsreturnEntryRespositories,ITdsreturnChallanRespositories tdsreturnChallanRespositories, 
           ITdschallanAllocationRespostories tdschallanAllocationRespostories,ITdsrateRespostories tdsrateRespostories,
           IRegisterRespositories registerRespositories,IUserRespositories userRespositories,
           IPlanRespositories planRespositories, AppDbContext appDbContext)
        {
            this.dbcontext = dbcontext;
            ProductOwnerRepositories = productOwnerRespositories;
            this.appDbContext = appDbContext;
            CompanyRepository = companyRepositories;
            SubscriptionPlanRepository = subscriptionPlanRespositories;
            ProductOwnerSubscriptions = productOwnerSubscriptionsRespostories;
            SubcontractorRepository = subcontractorRespositories;
            PlanRespositories = planRespositories;
            SubscriptionInvoices = subscriptionInvoicesRespostories;
            PaymentTransactionRespositories = paymentTransactionRespositories;
            CustomerPayments =  customerPaymentsRespositories;
            PartyMasterRepositories = partyMasterRespositories;
            CompanyPartyRoleRepositories = companyPartyRoleRespositories;
            ComplianceDeadlineRespositories = complianceDeadlineRespositories;
            ComplianceScheduleRespositories = complianceScheduleRespositories;
            ComplianceFilingRespositories = complianceFilingRespositories;
            LegalDocumentAcceptanceRespositories = legalDocumentAcceptanceRespositories;
            LegalDocumentRespositories = legalDocumentRespositories;
            LegalDocumentVersionRespositories = legalDocumentVersionRespositories;
            LicenseActivationRespositories = licenseActivationRespositories;
            LicenseAuditLogRespositories = licenseAuditLogRespositories;
            LegalKeyMasterRepositories = licenseKeyMasterRespositories;
            MachineBindingRespositories = machineBindingRespositories;
            FilingStatusesRespositories = filingStatusesRespositories;
            EmploymentTypeRespositories = employmentTypeRespositories;
            ExitTypesRespositories = exitTypesRespositories;
            LeaveEncashmentPolicyRespositories = leaveEncashmentPolicyRespositories;
            LeaveEncashmentTransactionRespositories = leaveEncashmentTransactionRespositories;
            PayrollDataRespositories = payrollDataRespositories;
            CompanyEPFORespositories = companyEPFORespositories;
            ePFOMonthWageRespositories = EPFOMonthWageRespositories;
            ePFOPeriodRespositories = EPFOPeriodRespositories;
            EmployeeEPFORespositories = employeeEPFORespositories;
            EPFOECRFileRespositories = ePFOECRFileRespositories;
            EmployeeRespositories = employeeRespositories;
            GratuityPolicyRespositories = gratuityPolicyRespositories;
            GratuityTransactionRespositories= gratuityTransactionRespositories;
            FCalculationRespositories = fnFCalculationRespositories;
            GgstSaleRespositories = gstSaleRespositories;
            ReturnRespositories = gstReturnRespositories;
            PurchaseRespositories = gstPurchaseRespositories;
            InvoiceSeriesRespositories = gstInvoiceSeriesRespositories;
            GSTHSNMappingRespositories = gSTHSNMappingRespositories;
            GSTHSNSACRespositories = gSTHSNSACRespositories;
            TdsdeductorRespositories = tdsdeductorRespositories;
            TdsdeducteeRespositories = tdsdeducteeRespositories;
            TDSReturnRespositories = tDSReturnRespositories;
            TdsentryRespositories = dsentryRespositories;
            TdschallanRespositories = tdschallanRespositories;
            TdsreturnChallanRespositories = tdsreturnChallanRespositories;
            TdsreturnEntryRespositories = tdsreturnEntryRespositories;
            TdschallanAllocationRespostories = tdschallanAllocationRespostories;
            TdsrateRespostories = tdsrateRespostories;
            RegisterRespositories = registerRespositories;
            UserRespositories = userRespositories;
        }
        public async Task CommitAsync() 
        {
            try 
            { 
                await appDbContext.SaveChangesAsync(); 
            } catch (Exception ex) 
            {
                throw; 
            }
        }
        void IDisposable.Dispose() 
        { 
            if (appDbContext != null) 
            {
                appDbContext.Dispose(); 
            } 
        }
     
    }
}
