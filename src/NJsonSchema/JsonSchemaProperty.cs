﻿//-----------------------------------------------------------------------
// <copyright file="JsonProperty.cs" company="NJsonSchema">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NJsonSchema/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.ComponentModel;
using Newtonsoft.Json;

namespace NJsonSchema
{
    /// <summary>A description of a JSON property of a JSON schema. </summary>
    public class JsonSchemaProperty : JsonSchema
    {
        private object _parent;

        /// <summary>Gets or sets the name of the property. </summary>
        [JsonIgnore]
        public string Name { get; internal set; }

        /// <summary>Gets the parent schema of this property schema. </summary>
        [JsonIgnore]
        public override object Parent
        {
            get { return _parent; }
            set
            {
                var initialize = _parent == null;
                _parent = value;

                if (initialize && InitialIsRequired)
                {
                    IsRequired = InitialIsRequired;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the property is required. </summary>
        [JsonIgnore]
        public bool IsRequired
        {
            get { return ParentSchema.RequiredProperties.Contains(Name); }
            set
            {
                if (ParentSchema == null)
                {
                    InitialIsRequired = value;
                }
                else
                {
                    if (value)
                    {
                        if (!ParentSchema.RequiredProperties.Contains(Name))
                        {
                            ParentSchema.RequiredProperties.Add(Name);
                        }
                    }
                    else
                    {
                        if (ParentSchema.RequiredProperties.Contains(Name))
                        {
                            ParentSchema.RequiredProperties.Remove(Name);
                        }
                    }
                }
            }
        }

        /// <remarks>Value used to set <see cref="IsRequired"/> property even if parent is not set yet. </remarks>
        [JsonIgnore]
        internal bool InitialIsRequired { get; set; }

        /// <summary>Gets or sets a value indicating whether the property is read only (Swagger and Open API only).</summary>
        [DefaultValue(false)]
        [JsonProperty("x-readOnly", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsReadOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether the property is write only (Open API only).</summary>
        [DefaultValue(false)]
        [JsonProperty("x-writeOnly", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsWriteOnly { get; set; }

        /// <summary>Gets a value indicating whether the property is an inheritance discriminator.</summary>
        [JsonIgnore]
        public bool IsInheritanceDiscriminator => ParentSchema.ActualDiscriminator == Name;

        /// <summary>Determines whether the specified property null handling is nullable.</summary>
        /// <param name="schemaType">The schema type.</param>
        /// <returns>true if the type can be null.</returns>
        public override bool IsNullable(SchemaType schemaType)
        {
            // PATCH: Fix for https://github.com/RicoSuter/NSwag/issues/2887
            if (Type == JsonObjectType.Number
                || Type == JsonObjectType.Integer
                || (Type == JsonObjectType.String && Format == "date-time"))
            {
                return false;
            }

            if (IsNullableRaw == null && (Type == JsonObjectType.Object || Type == JsonObjectType.String || Type == JsonObjectType.Array))
            {
                return !IsRequired;
            }

            if (IsNullableRaw == true)
            {
                return true;
            }

            return base.IsNullable(schemaType);
        }
    }
}