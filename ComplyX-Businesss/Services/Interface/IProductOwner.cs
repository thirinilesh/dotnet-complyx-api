using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models.ProductOwner;
using ComplyX_Businesss.Helper;
using ComplyX_Businesss.Models;
using System.Globalization;
using ComplyX.Data.Entities;
using ComplyX_Businesss.Models.Company;
using ComplyX_Businesss.Models.SubscriptionPlan;
using ComplyX_Businesss.Models.Subcontractor;
using ComplyX_Businesss.Models.Plan;
using ComplyX_Businesss.Models.SubscriptionInvoices;
using ComplyX_Businesss.Models.PaymentTransaction;
using ComplyX_Businesss.Models.CustomerPayments;
using ComplyX_Businesss.Models.PartyMaster;
using ComplyX_Businesss.Models.CompanyPartyRole;

namespace ComplyX_Businesss.Services
{
    public interface IProductOwner
    {
        Task<ManagerBaseResponse<IEnumerable<CommonDropdownModel>>> GetProductOwnerList(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwnerRequestModel ProductOwners);
        Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string ProductOwnerId);
        Task<ManagerBaseResponse<List<ProductOwnerResponseModel>>> GetAllProductOwnerByID(string ProductOwnerId);
        Task<ManagerBaseResponse<IEnumerable<ProductOwnerResponseModel>>> GetAllProductOwnerFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveCompanyData(CompanyRequestModel company);
        Task<ManagerBaseResponse<bool>> RemoveCompanyData(string CompanyId);
        Task<ManagerBaseResponse<IEnumerable<CommonDropdownModel>>> GetCompanyData(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<IEnumerable<CompanyResponseModel>>> GetAllCompanyDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<List<SubscriptionPlanResponseModel>>> GetSubscriptionPlans();
        Task<ManagerBaseResponse<IEnumerable<SubscriptionPlanResponseModel>>> GetSubscriptionPlansByFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<List<SubscriptionPlan>>> GetSubscriptionPlanFilter(SubscriptionPlansFilterRequest request);
        Task<ManagerBaseResponse<bool>> SaveUserSubscriptionData(ProductOwnerSubscription ProductOwnerSubscriptions);
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto);
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto, int ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SaveSubcontractorData(SubcontractorRequestModel Subcontractors);
        Task<ManagerBaseResponse<bool>> RemoveSubcontractorData(string SubcontractorsID);
        Task<ManagerBaseResponse<List<SubcontractorResponseModel>>> GetSubcontractors(int CompanyID);
        Task<ManagerBaseResponse<IEnumerable<CommonDropdownModel>>> GetSubcontractorsData(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<IEnumerable<SubcontractorResponseModel>>> GetSubcontractorsFilter(PagedListCriteria PagedListCriteria);

        Task<ManagerBaseResponse<List<SubcontractorResponseModel>>> GetProductOwnerSubcontractorsDetails(int ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SavePlansData(PlanRequestModel Plans);
        Task<ManagerBaseResponse<bool>> RemovePlansData(string PlanID);
        Task<ManagerBaseResponse<List<PlanResponseModel>>> GetAllPlansData();
        Task<ManagerBaseResponse<List<PlanResponseModel>>> GetAllPlansDataByID(string PlanID);
        Task<ManagerBaseResponse<IEnumerable<PlanResponseModel>>> GetAllPlansDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSubscriptionInvoicesData(SubscriptionInvoicesRequestModel subscriptionInvoices);
        Task<ManagerBaseResponse<bool>> RemoveSubscriptionInvoicesData(string InvoiceID);
        Task<ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesData();
        Task<ManagerBaseResponse<List<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesDataByID(string InvoiceID);
        Task<ManagerBaseResponse<IEnumerable<SubscriptionInvoicesResponseModel>>> GetAllSubscriptionInvoicesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSubscriptionPlansData(SubscriptionPlanRequestModel subscriptionPlans);

        Task<ManagerBaseResponse<bool>> RemoveSubscriptionPlansData(string PlanID);

        Task<ManagerBaseResponse<bool>> SavePaymentTransactionData(PaymentTransactionRequestModel paymentTransactions);
        Task<ManagerBaseResponse<bool>> RemovePaymentTransactionData(string TransactionID);
        Task<ManagerBaseResponse<List<PaymentTransactionRequestModel>>> GetAllPaymentTransactionData();
        Task<ManagerBaseResponse<List<PaymentTransactionRequestModel>>> GetAllPaymentTransactionDataByID(string TransactionID);
        Task<ManagerBaseResponse<IEnumerable<PaymentTransactionRequestModel>>> GetAllPaymentTransactionFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveCustomerPaymentsData(CustomerPaymentsRequestModel CustomerPayments);
        Task<ManagerBaseResponse<bool>> RemoveCustomerPaymentsData(string PaymentID);
        Task<ManagerBaseResponse<List<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentsData();
        Task<ManagerBaseResponse<List<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentDataByID(string PaymentID);
        Task<ManagerBaseResponse<IEnumerable<CustomerPaymentsResponseModel>>> GetAllCustomerPaymentFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SavePartyMasterData(PartyMasterRequestModel PartyMaster, string UserName);
        Task<ManagerBaseResponse<bool>> RemovePartyMasterData(string PartyID);
        Task<ManagerBaseResponse<List<PartyMasterResponseModel>>> GetAllPartyMasterData();
        Task<ManagerBaseResponse<List<PartyMasterResponseModel>>> GetAllPartyMasterDataByID(string PArtyID);
        Task<ManagerBaseResponse<IEnumerable<PartyMasterResponseModel>>> GetAllPartyMasterFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveCompanyPartyRoleData(CompanyPartyRoleRequestModel CompanyPartyRole, string UserName);
        Task<ManagerBaseResponse<bool>> RemoveCompanyPartyRoleData(string CompanyPartyRoleID);
        Task<ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleData();
        Task<ManagerBaseResponse<List<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleDataByID(string CompanyPartyRoleID);
        Task<ManagerBaseResponse<IEnumerable<CompanyPartyRoleResponseModel>>> GetAllCompanyPartyRoleFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<object>> GetDataCompanyCount(int ProductOwnerId);
    }

}
