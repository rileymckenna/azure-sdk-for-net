// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Logic.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The swagger tree command.
    /// </summary>
    public partial class SwaggerCustomDynamicTreeCommand
    {
        /// <summary>
        /// Initializes a new instance of the SwaggerCustomDynamicTreeCommand
        /// class.
        /// </summary>
        public SwaggerCustomDynamicTreeCommand()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SwaggerCustomDynamicTreeCommand
        /// class.
        /// </summary>
        /// <param name="operationId">The path to an item property which
        /// defines the display name of the item.</param>
        /// <param name="itemsPath">The path to an item property which defines
        /// the display name of the item.</param>
        /// <param name="itemValuePath">The path to an item property which
        /// defines the display name of the item.</param>
        /// <param name="itemTitlePath">The path to an item property which
        /// defines the display name of the item.</param>
        /// <param name="itemFullTitlePath">The path to an item property which
        /// defines the display name of the item.</param>
        /// <param name="itemIsParent">The path to an item property which
        /// defines the display name of the item.</param>
        /// <param name="selectableFilter">The path to an item property which
        /// defines the display name of the item.</param>
        public SwaggerCustomDynamicTreeCommand(string operationId = default(string), string itemsPath = default(string), string itemValuePath = default(string), string itemTitlePath = default(string), string itemFullTitlePath = default(string), string itemIsParent = default(string), string selectableFilter = default(string), IDictionary<string, SwaggerCustomDynamicTreeParameter> parameters = default(IDictionary<string, SwaggerCustomDynamicTreeParameter>))
        {
            OperationId = operationId;
            ItemsPath = itemsPath;
            ItemValuePath = itemValuePath;
            ItemTitlePath = itemTitlePath;
            ItemFullTitlePath = itemFullTitlePath;
            ItemIsParent = itemIsParent;
            SelectableFilter = selectableFilter;
            Parameters = parameters;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "operationId")]
        public string OperationId { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "itemsPath")]
        public string ItemsPath { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "itemValuePath")]
        public string ItemValuePath { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "itemTitlePath")]
        public string ItemTitlePath { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "itemFullTitlePath")]
        public string ItemFullTitlePath { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "itemIsParent")]
        public string ItemIsParent { get; set; }

        /// <summary>
        /// Gets or sets the path to an item property which defines the display
        /// name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "selectableFilter")]
        public string SelectableFilter { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "parameters")]
        public IDictionary<string, SwaggerCustomDynamicTreeParameter> Parameters { get; set; }

    }
}
