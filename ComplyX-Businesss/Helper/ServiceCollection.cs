using ComplyX.Repositories.Repositories.Abstractions;
using ComplyX.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyX.Data.Entities;

namespace ComplyX_Businesss.Helper
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductOwnerRespositories, ProductOwnerRespositories>();
            services.AddScoped<ICompanyRespositories, CompanyRespositories>();
            services.AddScoped<ICompanyEPFORespositories, CompanyEPFORespositories>();
            services.AddScoped<ICompanyPartyRoleRespositories, CompanyPartyRoleRespositories>();
            services.AddScoped<IComplianceDeadlineRespositories, ComplianceDeadlineRespositories>();
            services.AddScoped<IComplianceFilingRespositories, ComplianceFilingRespositories>();
            services.AddScoped<IComplianceScheduleRespositories, ComplianceScheduleRespositories>();
            services.AddScoped<ICustomerPaymentsRespositories, CustomerPaymentsRespositories>();
            services.AddScoped<IPaymentTransactionRespositories, PaymentTransactionRespositories>();
            services.AddScoped<IPartyMasterRespositories, PartyMasterRespositories>();
            services.AddScoped<IPayrollDataRespositories, PayrollDataRespositories>();
            services.AddScoped<IPlanRespositories, PlanRespositories>();
            services.AddScoped<IProductOwnerSubscriptionsRespostories, ProductOwnerSubscriptionsRespostories>();
            services.AddScoped<ITdschallanAllocationRespostories,TdschallanAllocationRespostories>();
            services.AddScoped<ITdschallanRespositories, TdschallanRespositories>();
            services.AddScoped<ITdsdeducteeRespositories, TdsdeducteeRespositories>();
            services.AddScoped<ITdsrateRespostories, TdsrateRespostories>();
            services.AddScoped<ITdsdeductorRespositories,TdsdeductorRespositories>();
            services.AddScoped<ITdsentryRespositories,TdsentryRespositories>();
            services.AddScoped<ITDSReturnRespositories, TDSReturnRespositories>();
            services.AddScoped<ITdsreturnChallanRespositories, TdsreturnChallanRespositories>();
            services.AddScoped<ITdsreturnEntryRespositories, TdsreturnEntryRespositories>();
            services.AddScoped<ISubscriptionInvoicesRespostories, SubscriptionInvoicesRespositories>();
            services.AddScoped<ISubcontractorRespositories, SubcontractorRespositories>();
            services.AddScoped<ISubscriptionPlanRespositories, SubscriptionPlanRespositories>();
            services.AddScoped<ILeaveEncashmentPolicyRespositories,LeaveEncashmentPolicyRespositories>();
            services.AddScoped<ILeaveEncashmentTransactionRespositories,LeaveEncashmentTransactionRespositories>();
            services.AddScoped<ILegalDocumentAcceptanceRespositories,LegalDocumentAcceptanceRespositories>();
            services.AddScoped<ILegalDocumentRespositories,LegalDocumentRespositories>();
            services.AddScoped<ILegalDocumentVersionRespositories,LegalDocumentVersionRespositories>();
            services.AddScoped<ILicenseActivationRespositories, LicenseActivationRespositories>();
            services.AddScoped<ILicenseAuditLogRespositories, LicenseAuditLogRespositories>();
            services.AddScoped<ILicenseKeyMasterRespositories, LicenseKeyMasterRespositories>();
            services.AddScoped<IMachineBindingRespositories, MachineBindingRespositories>();
            services.AddScoped<IGratuityPolicyRespositories, GratuityPolicyRespositories>();
            services.AddScoped<IGratuityTransactionRespositories, GratuityTransactionRespositories>();
            services.AddScoped<IGSTHSNMappingRespositories, GSTHSNMappingRespositories>();
            services.AddScoped<IGSTHSNSACRespositories, GSTHSNSACRespositories>();
            services.AddScoped<IGstInvoiceSeriesRespositories, GstInvoiceSeriesRespositories>();
            services.AddScoped<IGstPurchaseRespositories, GstPurchaseRespositories>();
            services.AddScoped<IGstReturnRespositories, GstReturnRespositories>();
            services.AddScoped<IGstSaleRespositories, GstSaleRespositories>();
            services.AddScoped<IFnFCalculationRespositories, FnFCalculationRespositories>();
            services.AddScoped<IExitTypesRespositories, ExitTypesRespositories>();
            services.AddScoped<IFilingStatusesRespositories, FilingStatusesRespositories>();
            services.AddScoped<IEmployeeEPFORespositories, EmployeeEPFORespositories>();
            services.AddScoped<IEmployeeRespositories, EmployeeRespositories>();
            services.AddScoped<IEmploymentTypeRespositories, EmploymentTypeRespositories>();
            services.AddScoped<IEPFOECRFileRespositories, EPFOECRFileRespositories>();
            services.AddScoped<IEPFOMonthWageRespositories, EPFOMonthWageRespositories>();
            services.AddScoped<IEPFOPeriodRespositories, EPFOPeriodRespositories>();
            services.AddScoped<IUserRespositories, UserRespositories>();
            services.AddScoped<IRegisterRespositories, RegisterRespositories>();
            return services;
        }
        public static IServiceCollection AddAcaBusinessAutoMapper(this IServiceCollection services, ServiceLifetime lifetime) => 
            services.AddAutoMapper(_ => { }, [typeof(MappingProfile)], lifetime);
    }
}
