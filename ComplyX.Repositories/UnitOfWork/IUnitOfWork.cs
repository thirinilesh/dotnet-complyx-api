using System;
using ComplyX.Repositories.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyX.Repositories.Repositories.Abstractions;

namespace ComplyX.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IProductOwnerRespositories ProductOwnerRepositories { get; }
        ICompanyRespositories CompanyRepository { get; }
        ISubscriptionPlanRespositories SubscriptionPlanRepository { get; }
        IProductOwnerSubscriptionsRespostories ProductOwnerSubscriptions { get; }
        ISubcontractorRespositories SubcontractorRepository { get; }
        IPlanRespositories PlanRespositories { get; }
        ISubscriptionInvoicesRespostories SubscriptionInvoices { get; }
        IPaymentTransactionRespositories PaymentTransactionRespositories { get; }
        ICustomerPaymentsRespositories CustomerPayments { get; }    
        IPartyMasterRespositories PartyMasterRepositories { get; }
        ICompanyPartyRoleRespositories CompanyPartyRoleRepositories { get; }
        IComplianceDeadlineRespositories ComplianceDeadlineRespositories { get; }
        IComplianceScheduleRespositories ComplianceScheduleRespositories { get; }
        IComplianceFilingRespositories ComplianceFilingRespositories { get; }
        ILegalDocumentAcceptanceRespositories LegalDocumentAcceptanceRespositories { get; }
        ILegalDocumentRespositories LegalDocumentRespositories { get; }
        ILegalDocumentVersionRespositories LegalDocumentVersionRespositories { get; }
        ILicenseAuditLogRespositories LicenseAuditLogRespositories { get; }
        ILicenseKeyMasterRespositories LegalKeyMasterRepositories { get; }
        ILicenseActivationRespositories LicenseActivationRespositories { get; }
        IMachineBindingRespositories MachineBindingRespositories { get; }
        IFilingStatusesRespositories FilingStatusesRespositories { get; }
        IEmploymentTypeRespositories EmploymentTypeRespositories { get; }
        IExitTypesRespositories ExitTypesRespositories { get; }
        IPayrollDataRespositories PayrollDataRespositories { get; }
        ILeaveEncashmentPolicyRespositories LeaveEncashmentPolicyRespositories { get; }
        ILeaveEncashmentTransactionRespositories LeaveEncashmentTransactionRespositories { get; }
        ICompanyEPFORespositories CompanyEPFORespositories { get; }
        IEmployeeEPFORespositories EmployeeEPFORespositories { get; }
        IEPFOECRFileRespositories EPFOECRFileRespositories { get; }
        IEPFOMonthWageRespositories ePFOMonthWageRespositories { get; }

        IEPFOPeriodRespositories ePFOPeriodRespositories { get; }
        IEmployeeRespositories EmployeeRespositories { get; }
        IGratuityPolicyRespositories GratuityPolicyRespositories { get; }
        IGratuityTransactionRespositories GratuityTransactionRespositories { get; }
        IFnFCalculationRespositories FCalculationRespositories { get; }
        IGstSaleRespositories GgstSaleRespositories { get; }
        IGstReturnRespositories ReturnRespositories { get; }
        IGstPurchaseRespositories PurchaseRespositories { get; }
        IGstInvoiceSeriesRespositories InvoiceSeriesRespositories { get; }
        IGSTHSNMappingRespositories GSTHSNMappingRespositories { get; }
        IGSTHSNSACRespositories GSTHSNSACRespositories { get; }
        ITdsdeductorRespositories TdsdeductorRespositories { get; }
        ITdsdeducteeRespositories TdsdeducteeRespositories { get; }
        ITDSReturnRespositories TDSReturnRespositories { get; }
        ITdsentryRespositories TdsentryRespositories { get; }
        ITdschallanRespositories TdschallanRespositories { get; }
        ITdsreturnChallanRespositories TdsreturnChallanRespositories { get; }
        ITdsreturnEntryRespositories TdsreturnEntryRespositories { get; }
        ITdschallanAllocationRespostories TdschallanAllocationRespostories { get; }
        ITdsrateRespostories TdsrateRespostories { get; }
        IRegisterRespositories RegisterRespositories { get; }
        IUserRespositories UserRespositories { get; }
    }
}
