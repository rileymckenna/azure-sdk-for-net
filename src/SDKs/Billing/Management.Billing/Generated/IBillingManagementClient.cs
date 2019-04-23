// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Billing
{
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Billing client provides access to billing resources for Azure
    /// subscriptions.
    /// </summary>
    public partial interface IBillingManagementClient : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        ServiceClientCredentials Credentials { get; }

        /// <summary>
        /// Version of the API to be used with the client request. The current
        /// version is 2018-11-01-preview.
        /// </summary>
        string ApiVersion { get; }

        /// <summary>
        /// Azure Subscription ID.
        /// </summary>
        string SubscriptionId { get; set; }

        /// <summary>
        /// The preferred language for the response.
        /// </summary>
        string AcceptLanguage { get; set; }

        /// <summary>
        /// The retry timeout in seconds for Long Running Operations. Default
        /// value is 30.
        /// </summary>
        int? LongRunningOperationRetryTimeout { get; set; }

        /// <summary>
        /// Whether a unique x-ms-client-request-id should be generated. When
        /// set to true a unique x-ms-client-request-id value is generated and
        /// included in each request. Default is true.
        /// </summary>
        bool? GenerateClientRequestId { get; set; }


        /// <summary>
        /// Gets the IBillingAccountsOperations.
        /// </summary>
        IBillingAccountsOperations BillingAccounts { get; }

        /// <summary>
        /// Gets the IAvailableBalanceByBillingProfileOperations.
        /// </summary>
        IAvailableBalanceByBillingProfileOperations AvailableBalanceByBillingProfile { get; }

        /// <summary>
        /// Gets the IPaymentMethodsByBillingProfileOperations.
        /// </summary>
        IPaymentMethodsByBillingProfileOperations PaymentMethodsByBillingProfile { get; }

        /// <summary>
        /// Gets the IBillingProfilesByBillingAccountNameOperations.
        /// </summary>
        IBillingProfilesByBillingAccountNameOperations BillingProfilesByBillingAccountName { get; }

        /// <summary>
        /// Gets the IBillingProfilesOperations.
        /// </summary>
        IBillingProfilesOperations BillingProfiles { get; }

        /// <summary>
        /// Gets the IInvoiceSectionsByBillingAccountNameOperations.
        /// </summary>
        IInvoiceSectionsByBillingAccountNameOperations InvoiceSectionsByBillingAccountName { get; }

        /// <summary>
        /// Gets the IInvoiceSectionsOperations.
        /// </summary>
        IInvoiceSectionsOperations InvoiceSections { get; }

        /// <summary>
        /// Gets the IInvoiceSectionsWithCreateSubscriptionPermissionOperations.
        /// </summary>
        IInvoiceSectionsWithCreateSubscriptionPermissionOperations InvoiceSectionsWithCreateSubscriptionPermission { get; }

        /// <summary>
        /// Gets the IDepartmentsByBillingAccountNameOperations.
        /// </summary>
        IDepartmentsByBillingAccountNameOperations DepartmentsByBillingAccountName { get; }

        /// <summary>
        /// Gets the IDepartmentsOperations.
        /// </summary>
        IDepartmentsOperations Departments { get; }

        /// <summary>
        /// Gets the IEnrollmentAccountsByBillingAccountNameOperations.
        /// </summary>
        IEnrollmentAccountsByBillingAccountNameOperations EnrollmentAccountsByBillingAccountName { get; }

        /// <summary>
        /// Gets the IEnrollmentAccountsOperations.
        /// </summary>
        IEnrollmentAccountsOperations EnrollmentAccounts { get; }

        /// <summary>
        /// Gets the IInvoicesByBillingAccountOperations.
        /// </summary>
        IInvoicesByBillingAccountOperations InvoicesByBillingAccount { get; }

        /// <summary>
        /// Gets the IInvoicePricesheetOperations.
        /// </summary>
        IInvoicePricesheetOperations InvoicePricesheet { get; }

        /// <summary>
        /// Gets the IInvoicesByBillingProfileOperations.
        /// </summary>
        IInvoicesByBillingProfileOperations InvoicesByBillingProfile { get; }

        /// <summary>
        /// Gets the IInvoiceOperations.
        /// </summary>
        IInvoiceOperations Invoice { get; }

        /// <summary>
        /// Gets the IProductsByBillingSubscriptionsOperations.
        /// </summary>
        IProductsByBillingSubscriptionsOperations ProductsByBillingSubscriptions { get; }

        /// <summary>
        /// Gets the IBillingSubscriptionsByBillingProfileOperations.
        /// </summary>
        IBillingSubscriptionsByBillingProfileOperations BillingSubscriptionsByBillingProfile { get; }

        /// <summary>
        /// Gets the IBillingSubscriptionsByInvoiceSectionOperations.
        /// </summary>
        IBillingSubscriptionsByInvoiceSectionOperations BillingSubscriptionsByInvoiceSection { get; }

        /// <summary>
        /// Gets the IBillingSubscriptionOperations.
        /// </summary>
        IBillingSubscriptionOperations BillingSubscription { get; }

        /// <summary>
        /// Gets the IProductsByBillingAccountOperations.
        /// </summary>
        IProductsByBillingAccountOperations ProductsByBillingAccount { get; }

        /// <summary>
        /// Gets the IProductsByInvoiceSectionOperations.
        /// </summary>
        IProductsByInvoiceSectionOperations ProductsByInvoiceSection { get; }

        /// <summary>
        /// Gets the IProductsOperations.
        /// </summary>
        IProductsOperations Products { get; }

        /// <summary>
        /// Gets the ITransactionsByBillingAccountOperations.
        /// </summary>
        ITransactionsByBillingAccountOperations TransactionsByBillingAccount { get; }

        /// <summary>
        /// Gets the ITransactionsByBillingProfileOperations.
        /// </summary>
        ITransactionsByBillingProfileOperations TransactionsByBillingProfile { get; }

        /// <summary>
        /// Gets the ITransactionsByInvoiceSectionOperations.
        /// </summary>
        ITransactionsByInvoiceSectionOperations TransactionsByInvoiceSection { get; }

        /// <summary>
        /// Gets the IPolicyOperations.
        /// </summary>
        IPolicyOperations Policy { get; }

        /// <summary>
        /// Gets the IBillingPropertyOperations.
        /// </summary>
        IBillingPropertyOperations BillingProperty { get; }

        /// <summary>
        /// Gets the ITransfersOperations.
        /// </summary>
        ITransfersOperations Transfers { get; }

        /// <summary>
        /// Gets the IRecipientTransfersOperations.
        /// </summary>
        IRecipientTransfersOperations RecipientTransfers { get; }

        /// <summary>
        /// Gets the IOperations.
        /// </summary>
        IOperations Operations { get; }

        /// <summary>
        /// Gets the IBillingAccountBillingPermissionsOperations.
        /// </summary>
        IBillingAccountBillingPermissionsOperations BillingAccountBillingPermissions { get; }

        /// <summary>
        /// Gets the IInvoiceSectionsBillingPermissionsOperations.
        /// </summary>
        IInvoiceSectionsBillingPermissionsOperations InvoiceSectionsBillingPermissions { get; }

        /// <summary>
        /// Gets the IBillingProfileBillingPermissionsOperations.
        /// </summary>
        IBillingProfileBillingPermissionsOperations BillingProfileBillingPermissions { get; }

        /// <summary>
        /// Gets the IBillingAccountBillingRoleDefinitionOperations.
        /// </summary>
        IBillingAccountBillingRoleDefinitionOperations BillingAccountBillingRoleDefinition { get; }

        /// <summary>
        /// Gets the IInvoiceSectionBillingRoleDefinitionOperations.
        /// </summary>
        IInvoiceSectionBillingRoleDefinitionOperations InvoiceSectionBillingRoleDefinition { get; }

        /// <summary>
        /// Gets the IBillingProfileBillingRoleDefinitionOperations.
        /// </summary>
        IBillingProfileBillingRoleDefinitionOperations BillingProfileBillingRoleDefinition { get; }

        /// <summary>
        /// Gets the IBillingAccountBillingRoleAssignmentOperations.
        /// </summary>
        IBillingAccountBillingRoleAssignmentOperations BillingAccountBillingRoleAssignment { get; }

        /// <summary>
        /// Gets the IInvoiceSectionBillingRoleAssignmentOperations.
        /// </summary>
        IInvoiceSectionBillingRoleAssignmentOperations InvoiceSectionBillingRoleAssignment { get; }

        /// <summary>
        /// Gets the IBillingProfileBillingRoleAssignmentOperations.
        /// </summary>
        IBillingProfileBillingRoleAssignmentOperations BillingProfileBillingRoleAssignment { get; }

        /// <summary>
        /// Gets the IAgreementsOperations.
        /// </summary>
        IAgreementsOperations Agreements { get; }

        /// <summary>
        /// Cancel product by product id
        /// </summary>
        /// <param name='billingAccountName'>
        /// billing Account Id.
        /// </param>
        /// <param name='productName'>
        /// Invoice Id.
        /// </param>
        /// <param name='body'>
        /// Update auto renew request parameters.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<AzureOperationResponse<UpdateAutoRenewOperationSummary>> UpdateAutoRenewForBillingAccountWithHttpMessagesAsync(string billingAccountName, string productName, UpdateAutoRenewRequest body, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Cancel auto renew for product by product id
        /// </summary>
        /// <param name='billingAccountName'>
        /// billing Account Id.
        /// </param>
        /// <param name='invoiceSectionName'>
        /// InvoiceSection Id.
        /// </param>
        /// <param name='productName'>
        /// Invoice Id.
        /// </param>
        /// <param name='body'>
        /// Update auto renew request parameters.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<AzureOperationResponse<UpdateAutoRenewOperationSummary>> UpdateAutoRenewForInvoiceSectionWithHttpMessagesAsync(string billingAccountName, string invoiceSectionName, string productName, UpdateAutoRenewRequest body, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}