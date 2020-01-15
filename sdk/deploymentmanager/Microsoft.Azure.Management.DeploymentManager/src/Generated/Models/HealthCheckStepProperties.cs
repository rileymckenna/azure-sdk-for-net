// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DeploymentManager.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Defines the properties of a health check step.
    /// </summary>
    [Newtonsoft.Json.JsonObject("HealthCheck")]
    public partial class HealthCheckStepProperties : StepProperties
    {
        /// <summary>
        /// Initializes a new instance of the HealthCheckStepProperties class.
        /// </summary>
        public HealthCheckStepProperties()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the HealthCheckStepProperties class.
        /// </summary>
        /// <param name="attributes">The health check step attributes</param>
        public HealthCheckStepProperties(HealthCheckStepAttributes attributes)
        {
            Attributes = attributes;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the health check step attributes
        /// </summary>
        [JsonProperty(PropertyName = "attributes")]
        public HealthCheckStepAttributes Attributes { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Attributes == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Attributes");
            }
            if (Attributes != null)
            {
                Attributes.Validate();
            }
        }
    }
}
