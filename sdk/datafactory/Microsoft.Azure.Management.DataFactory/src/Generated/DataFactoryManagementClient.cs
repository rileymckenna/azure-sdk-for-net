// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DataFactory
{
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Microsoft.Rest.Serialization;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// The Azure Data Factory V2 management API provides a RESTful set of web
    /// services that interact with Azure Data Factory V2 services.
    /// </summary>
    public partial class DataFactoryManagementClient : ServiceClient<DataFactoryManagementClient>, IDataFactoryManagementClient, IAzureClient
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        public ServiceClientCredentials Credentials { get; private set; }

        /// <summary>
        /// The subscription identifier.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The API version.
        /// </summary>
        public string ApiVersion { get; private set; }

        /// <summary>
        /// The preferred language for the response.
        /// </summary>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// The retry timeout in seconds for Long Running Operations. Default value is
        /// 30.
        /// </summary>
        public int? LongRunningOperationRetryTimeout { get; set; }

        /// <summary>
        /// Whether a unique x-ms-client-request-id should be generated. When set to
        /// true a unique x-ms-client-request-id value is generated and included in
        /// each request. Default is true.
        /// </summary>
        public bool? GenerateClientRequestId { get; set; }

        /// <summary>
        /// Gets the IOperations.
        /// </summary>
        public virtual IOperations Operations { get; private set; }

        /// <summary>
        /// Gets the IFactoriesOperations.
        /// </summary>
        public virtual IFactoriesOperations Factories { get; private set; }

        /// <summary>
        /// Gets the IExposureControlOperations.
        /// </summary>
        public virtual IExposureControlOperations ExposureControl { get; private set; }

        /// <summary>
        /// Gets the IIntegrationRuntimesOperations.
        /// </summary>
        public virtual IIntegrationRuntimesOperations IntegrationRuntimes { get; private set; }

        /// <summary>
        /// Gets the IIntegrationRuntimeObjectMetadataOperations.
        /// </summary>
        public virtual IIntegrationRuntimeObjectMetadataOperations IntegrationRuntimeObjectMetadata { get; private set; }

        /// <summary>
        /// Gets the IIntegrationRuntimeNodesOperations.
        /// </summary>
        public virtual IIntegrationRuntimeNodesOperations IntegrationRuntimeNodes { get; private set; }

        /// <summary>
        /// Gets the ILinkedServicesOperations.
        /// </summary>
        public virtual ILinkedServicesOperations LinkedServices { get; private set; }

        /// <summary>
        /// Gets the IDatasetsOperations.
        /// </summary>
        public virtual IDatasetsOperations Datasets { get; private set; }

        /// <summary>
        /// Gets the IPipelinesOperations.
        /// </summary>
        public virtual IPipelinesOperations Pipelines { get; private set; }

        /// <summary>
        /// Gets the IPipelineRunsOperations.
        /// </summary>
        public virtual IPipelineRunsOperations PipelineRuns { get; private set; }

        /// <summary>
        /// Gets the IActivityRunsOperations.
        /// </summary>
        public virtual IActivityRunsOperations ActivityRuns { get; private set; }

        /// <summary>
        /// Gets the ITriggersOperations.
        /// </summary>
        public virtual ITriggersOperations Triggers { get; private set; }

        /// <summary>
        /// Gets the ITriggerRunsOperations.
        /// </summary>
        public virtual ITriggerRunsOperations TriggerRuns { get; private set; }

        /// <summary>
        /// Gets the IRerunTriggersOperations.
        /// </summary>
        public virtual IRerunTriggersOperations RerunTriggers { get; private set; }

        /// <summary>
        /// Gets the IDataFlowsOperations.
        /// </summary>
        public virtual IDataFlowsOperations DataFlows { get; private set; }

        /// <summary>
        /// Gets the IDataFlowDebugSessionOperations.
        /// </summary>
        public virtual IDataFlowDebugSessionOperations DataFlowDebugSession { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling DataFactoryManagementClient.Dispose(). False: will not dispose provided httpClient</param>
        protected DataFactoryManagementClient(HttpClient httpClient, bool disposeHttpClient) : base(httpClient, disposeHttpClient)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected DataFactoryManagementClient(params DelegatingHandler[] handlers) : base(handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected DataFactoryManagementClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected DataFactoryManagementClient(System.Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected DataFactoryManagementClient(System.Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public DataFactoryManagementClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='httpClient'>
        /// HttpClient to be used
        /// </param>
        /// <param name='disposeHttpClient'>
        /// True: will dispose the provided httpClient on calling DataFactoryManagementClient.Dispose(). False: will not dispose provided httpClient</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public DataFactoryManagementClient(ServiceClientCredentials credentials, HttpClient httpClient, bool disposeHttpClient) : this(httpClient, disposeHttpClient)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public DataFactoryManagementClient(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public DataFactoryManagementClient(System.Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the DataFactoryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public DataFactoryManagementClient(System.Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        /// </summary>
        partial void CustomInitialize();
        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            Operations = new Operations(this);
            Factories = new FactoriesOperations(this);
            ExposureControl = new ExposureControlOperations(this);
            IntegrationRuntimes = new IntegrationRuntimesOperations(this);
            IntegrationRuntimeObjectMetadata = new IntegrationRuntimeObjectMetadataOperations(this);
            IntegrationRuntimeNodes = new IntegrationRuntimeNodesOperations(this);
            LinkedServices = new LinkedServicesOperations(this);
            Datasets = new DatasetsOperations(this);
            Pipelines = new PipelinesOperations(this);
            PipelineRuns = new PipelineRunsOperations(this);
            ActivityRuns = new ActivityRunsOperations(this);
            Triggers = new TriggersOperations(this);
            TriggerRuns = new TriggerRunsOperations(this);
            RerunTriggers = new RerunTriggersOperations(this);
            DataFlows = new DataFlowsOperations(this);
            DataFlowDebugSession = new DataFlowDebugSessionOperations(this);
            BaseUri = new System.Uri("https://management.azure.com");
            ApiVersion = "2018-06-01";
            AcceptLanguage = "en-US";
            LongRunningOperationRetryTimeout = 30;
            GenerateClientRequestId = true;
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            SerializationSettings.Converters.Add(new TransformationJsonConverter());
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<SecretBase>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<SecretBase>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FactoryRepoConfiguration>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FactoryRepoConfiguration>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<IntegrationRuntime>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<IntegrationRuntime>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<IntegrationRuntimeStatus>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<IntegrationRuntimeStatus>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<LinkedService>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<LinkedService>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<Dataset>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<Dataset>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<Activity>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<Activity>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<Trigger>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<Trigger>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DataFlow>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DataFlow>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DependencyReference>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DependencyReference>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<WebLinkedServiceTypeProperties>("authenticationType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<WebLinkedServiceTypeProperties>("authenticationType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DatasetCompression>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DatasetCompression>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DatasetStorageFormat>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DatasetStorageFormat>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DatasetLocation>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DatasetLocation>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<StoreReadSettings>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<StoreReadSettings>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FormatReadSettings>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FormatReadSettings>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<CopySource>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<CopySource>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<StoreWriteSettings>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<StoreWriteSettings>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FormatWriteSettings>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FormatWriteSettings>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<CopySink>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<CopySink>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<LinkedIntegrationRuntimeType>("authorizationType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<LinkedIntegrationRuntimeType>("authorizationType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<CustomSetupBase>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<CustomSetupBase>("type"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<SsisObjectMetadata>("type"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<SsisObjectMetadata>("type"));
            CustomInitialize();
            DeserializationSettings.Converters.Add(new TransformationJsonConverter());
            DeserializationSettings.Converters.Add(new CloudErrorJsonConverter());
        }
    }
}
