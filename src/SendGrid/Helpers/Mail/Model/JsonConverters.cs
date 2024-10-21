// <copyright file="JsonConverters.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json;
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
#if NET40
using SendGrid.Helpers.Net40;
#else
using System.Reflection;
#endif

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Removes duplicates when writing the JSON representation of the object.
    /// </summary>
    public class RemoveDuplicatesConverter<T> :
#if NETSTANDARD2_0
        JsonConverter<IEnumerable<T>>
#else
        JsonConverter
#endif
    {

#if NETSTANDARD2_0
        private static readonly JsonConverter<T> s_defaultConverter =
            (JsonConverter<T>)JsonSerializerOptions.Default.GetConverter(typeof(T));
#endif

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            var rc = typeof(IEnumerable<T>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
            return rc;
        }

#if NETSTANDARD2_0
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options) {
            writer.WriteStartArray();
            foreach (T item in value.Distinct()) {
                s_defaultConverter.Write(writer, item, options);
            }
            writer.WriteEndArray();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        public override IEnumerable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            throw new NotImplementedException();
        }
#else
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (T item in ((IEnumerable<T>)value).Distinct())
            {
                serializer.Serialize(writer, item);
            }
            writer.WriteEndArray();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
