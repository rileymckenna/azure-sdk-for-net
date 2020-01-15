// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DataBoxEdge.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Represent the secrets intended for encryption with asymmetric key pair.
    /// </summary>
    public partial class AsymmetricEncryptedSecret
    {
        /// <summary>
        /// Initializes a new instance of the AsymmetricEncryptedSecret class.
        /// </summary>
        public AsymmetricEncryptedSecret()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AsymmetricEncryptedSecret class.
        /// </summary>
        /// <param name="value">The value of the secret.</param>
        /// <param name="encryptionAlgorithm">The algorithm used to encrypt
        /// "Value". Possible values include: 'None', 'AES256',
        /// 'RSAES_PKCS1_v_1_5'</param>
        /// <param name="encryptionCertThumbprint">Thumbprint certificate used
        /// to encrypt \"Value\". If the value is unencrypted, it will be
        /// null.</param>
        public AsymmetricEncryptedSecret(string value, string encryptionAlgorithm, string encryptionCertThumbprint = default(string))
        {
            Value = value;
            EncryptionCertThumbprint = encryptionCertThumbprint;
            EncryptionAlgorithm = encryptionAlgorithm;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the value of the secret.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets thumbprint certificate used to encrypt \"Value\". If
        /// the value is unencrypted, it will be null.
        /// </summary>
        [JsonProperty(PropertyName = "encryptionCertThumbprint")]
        public string EncryptionCertThumbprint { get; set; }

        /// <summary>
        /// Gets or sets the algorithm used to encrypt "Value". Possible values
        /// include: 'None', 'AES256', 'RSAES_PKCS1_v_1_5'
        /// </summary>
        [JsonProperty(PropertyName = "encryptionAlgorithm")]
        public string EncryptionAlgorithm { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Value == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Value");
            }
            if (EncryptionAlgorithm == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "EncryptionAlgorithm");
            }
        }
    }
}
