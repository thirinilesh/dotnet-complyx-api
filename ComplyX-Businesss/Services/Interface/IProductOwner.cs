using ComplyX.Shared.Helper;
 
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;
using System.Globalization;

namespace ComplyX.Services
{
    public interface IProductOwner
    {
        Task<ManagerBaseResponse<bool>> SaveProductOwnerData(ProductOwners ProductOwners);
        Task<ManagerBaseResponse<bool>> RemoveProductOwnerData(string ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SaveCompanyData(Company company);
        Task<ManagerBaseResponse<bool>> RemoveCompanyData(string CompanyId);
        Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlans();
        Task<ManagerBaseResponse<List<SubscriptionPlans>>> GetSubscriptionPlanFilter(SubscriptionPlansFilterRequest request);
        Task<ManagerBaseResponse<bool>> SaveUserSubscriptionData(ProductOwnerSubscriptions ProductOwnerSubscriptions);
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetUserSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto);
        Task<ManagerBaseResponse<List<ProductOwnerSubscriptionDto>>> GetProductOwnerSubscriptionPlansDetails(ProductOwnerSubscriptionDto ProductOwnerSubscriptionDto, int ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SaveSubcontractorData(Subcontractors Subcontractors);
        Task<ManagerBaseResponse<bool>> RemoveSubcontractorData(string SubcontractorsID);
        Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetSubcontractors(int CompanyID);
        Task<ManagerBaseResponse<List<SubcontractorsRequest>>> GetProductOwnerSubcontractorsDetails(int ProductOwnerId);
        Task<ManagerBaseResponse<bool>> SavePlansData(Plans Plans);
        Task<ManagerBaseResponse<bool>> RemovePlansData(string PlanID);
        Task<ManagerBaseResponse<List<Plans>>> GetAllPlansData();
        Task<ManagerBaseResponse<List<Plans>>> GetAllPlansDataByID(string PlanID);
        Task<ManagerBaseResponse<IEnumerable<Plans>>> GetAllPlansDataFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSubscriptionInvoicesData(SubscriptionInvoices subscriptionInvoices);
        Task<ManagerBaseResponse<bool>> RemoveSubscriptionInvoicesData(string InvoiceID);
        Task<ManagerBaseResponse<List<SubscriptionInvoices>>> GetAllSubscriptionInvoicesData();
        Task<ManagerBaseResponse<List<SubscriptionInvoices>>> GetAllSubscriptionInvoicesDataByID(string InvoiceID);
        Task<ManagerBaseResponse<IEnumerable<SubscriptionInvoices>>> GetAllSubscriptionInvoicesFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveSubscriptionPlansData(SubscriptionPlans subscriptionPlans);

        Task<ManagerBaseResponse<bool>> RemoveSubscriptionPlansData(string PlanID);

        Task<ManagerBaseResponse<bool>> SavePaymentTransactionData(PaymentTransactions paymentTransactions);
        Task<ManagerBaseResponse<bool>> RemovePaymentTransactionData(string TransactionID);
        Task<ManagerBaseResponse<List<PaymentTransactions>>> GetAllPaymentTransactionData();
        Task<ManagerBaseResponse<List<PaymentTransactions>>> GetAllPaymentTransactionDataByID(string TransactionID);
        Task<ManagerBaseResponse<IEnumerable<PaymentTransactions>>> GetAllPaymentTransactionFilter(PagedListCriteria PagedListCriteria);
        Task<ManagerBaseResponse<bool>> SaveCustomerPaymentsData(CustomerPayments CustomerPayments);
        Task<ManagerBaseResponse<bool>> RemoveCustomerPaymentsData(string PaymentID);
        Task<ManagerBaseResponse<List<CustomerPayments>>> GetAllCustomerPaymentsData();
        Task<ManagerBaseResponse<List<CustomerPayments>>> GetAllCustomerPaymentDataByID(string PaymentID);
        Task<ManagerBaseResponse<IEnumerable<CustomerPayments>>> GetAllCustomerPaymentFilter(PagedListCriteria PagedListCriteria);

    }

}
