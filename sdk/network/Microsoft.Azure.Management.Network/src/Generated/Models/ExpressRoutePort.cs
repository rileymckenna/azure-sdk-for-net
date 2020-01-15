// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Network.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ExpressRoute Port
    /// </summary>
    /// <remarks>
    /// ExpressRoutePort resource definition.
    /// </remarks>
    [Rest.Serialization.JsonTransformation]
    public partial class ExpressRoutePort : Resource
    {
        /// <summary>
        /// Initializes a new instance of the ExpressRoutePort class.
        /// </summary>
        public ExpressRoutePort()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ExpressRoutePort class.
        /// </summary>
        /// <param name="id">Resource ID.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="location">Resource location.</param>
        /// <param name="tags">Resource tags.</param>
        /// <param name="peeringLocation">The name of the peering location that
        /// the ExpressRoutePort is mapped to physically.</param>
        /// <param name="bandwidthInGbps">Bandwidth of procured ports in
        /// Gbps.</param>
        /// <param name="provisionedBandwidthInGbps">Aggregate Gbps of
        /// associated circuit bandwidths.</param>
        /// <param name="mtu">Maximum transmission unit of the physical port
        /// pair(s).</param>
        /// <param name="encapsulation">Encapsulation method on physical ports.
        /// Possible values include: 'Dot1Q', 'QinQ'</param>
        /// <param name="etherType">Ether type of the physical port.</param>
        /// <param name="allocationDate">Date of the physical port allocation
        /// to be used in Letter of Authorization.</param>
        /// <param name="links">ExpressRouteLink Sub-Resources</param>
        /// <param name="circuits">Reference the ExpressRoute circuit(s) that
        /// are provisioned on this ExpressRoutePort resource.</param>
        /// <param name="provisioningState">The provisioning state of the
        /// express route port resource. Possible values include: 'Succeeded',
        /// 'Updating', 'Deleting', 'Failed'</param>
        /// <param name="resourceGuid">The resource GUID property of the
        /// express route port resource.</param>
        /// <param name="etag">A unique read-only string that changes whenever
        /// the resource is updated.</param>
        /// <param name="identity">The identity of ExpressRoutePort, if
        /// configured.</param>
        public ExpressRoutePort(string id = default(string), string name = default(string), string type = default(string), string location = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), string peeringLocation = default(string), int? bandwidthInGbps = default(int?), double? provisionedBandwidthInGbps = default(double?), string mtu = default(string), string encapsulation = default(string), string etherType = default(string), string allocationDate = default(string), IList<ExpressRouteLink> links = default(IList<ExpressRouteLink>), IList<SubResource> circuits = default(IList<SubResource>), string provisioningState = default(string), string resourceGuid = default(string), string etag = default(string), ManagedServiceIdentity identity = default(ManagedServiceIdentity))
            : base(id, name, type, location, tags)
        {
            PeeringLocation = peeringLocation;
            BandwidthInGbps = bandwidthInGbps;
            ProvisionedBandwidthInGbps = provisionedBandwidthInGbps;
            Mtu = mtu;
            Encapsulation = encapsulation;
            EtherType = etherType;
            AllocationDate = allocationDate;
            Links = links;
            Circuits = circuits;
            ProvisioningState = provisioningState;
            ResourceGuid = resourceGuid;
            Etag = etag;
            Identity = identity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the name of the peering location that the
        /// ExpressRoutePort is mapped to physically.
        /// </summary>
        [JsonProperty(PropertyName = "properties.peeringLocation")]
        public string PeeringLocation { get; set; }

        /// <summary>
        /// Gets or sets bandwidth of procured ports in Gbps.
        /// </summary>
        [JsonProperty(PropertyName = "properties.bandwidthInGbps")]
        public int? BandwidthInGbps { get; set; }

        /// <summary>
        /// Gets aggregate Gbps of associated circuit bandwidths.
        /// </summary>
        [JsonProperty(PropertyName = "properties.provisionedBandwidthInGbps")]
        public double? ProvisionedBandwidthInGbps { get; private set; }

        /// <summary>
        /// Gets maximum transmission unit of the physical port pair(s).
        /// </summary>
        [JsonProperty(PropertyName = "properties.mtu")]
        public string Mtu { get; private set; }

        /// <summary>
        /// Gets or sets encapsulation method on physical ports. Possible
        /// values include: 'Dot1Q', 'QinQ'
        /// </summary>
        [JsonProperty(PropertyName = "properties.encapsulation")]
        public string Encapsulation { get; set; }

        /// <summary>
        /// Gets ether type of the physical port.
        /// </summary>
        [JsonProperty(PropertyName = "properties.etherType")]
        public string EtherType { get; private set; }

        /// <summary>
        /// Gets date of the physical port allocation to be used in Letter of
        /// Authorization.
        /// </summary>
        [JsonProperty(PropertyName = "properties.allocationDate")]
        public string AllocationDate { get; private set; }

        /// <summary>
        /// Gets or sets expressRouteLink Sub-Resources
        /// </summary>
        /// <remarks>
        /// The set of physical links of the ExpressRoutePort resource.
        /// </remarks>
        [JsonProperty(PropertyName = "properties.links")]
        public IList<ExpressRouteLink> Links { get; set; }

        /// <summary>
        /// Gets reference the ExpressRoute circuit(s) that are provisioned on
        /// this ExpressRoutePort resource.
        /// </summary>
        [JsonProperty(PropertyName = "properties.circuits")]
        public IList<SubResource> Circuits { get; private set; }

        /// <summary>
        /// Gets the provisioning state of the express route port resource.
        /// Possible values include: 'Succeeded', 'Updating', 'Deleting',
        /// 'Failed'
        /// </summary>
        [JsonProperty(PropertyName = "properties.provisioningState")]
        public string ProvisioningState { get; private set; }

        /// <summary>
        /// Gets the resource GUID property of the express route port resource.
        /// </summary>
        [JsonProperty(PropertyName = "properties.resourceGuid")]
        public string ResourceGuid { get; private set; }

        /// <summary>
        /// Gets a unique read-only string that changes whenever the resource
        /// is updated.
        /// </summary>
        [JsonProperty(PropertyName = "etag")]
        public string Etag { get; private set; }

        /// <summary>
        /// Gets or sets the identity of ExpressRoutePort, if configured.
        /// </summary>
        [JsonProperty(PropertyName = "identity")]
        public ManagedServiceIdentity Identity { get; set; }

    }
}
