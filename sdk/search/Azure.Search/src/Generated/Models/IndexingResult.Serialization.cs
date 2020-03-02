// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.Search.Models
{
    public partial class IndexingResult : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Key != null)
            {
                writer.WritePropertyName("key");
                writer.WriteStringValue(Key);
            }
            if (ErrorMessage != null)
            {
                writer.WritePropertyName("errorMessage");
                writer.WriteStringValue(ErrorMessage);
            }
            if (Succeeded != null)
            {
                writer.WritePropertyName("status");
                writer.WriteBooleanValue(Succeeded.Value);
            }
            if (StatusCode != null)
            {
                writer.WritePropertyName("statusCode");
                writer.WriteNumberValue(StatusCode.Value);
            }
            writer.WriteEndObject();
        }
        internal static IndexingResult DeserializeIndexingResult(JsonElement element)
        {
            IndexingResult result = new IndexingResult();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("key"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Key = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("errorMessage"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.ErrorMessage = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("status"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Succeeded = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("statusCode"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.StatusCode = property.Value.GetInt32();
                    continue;
                }
            }
            return result;
        }
    }
}
