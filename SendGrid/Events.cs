using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SendGrid
{

    /// <summary>
    /// A helper class for interacting with the SendGrid Event API.
    /// </summary>
    public static class Events
    {

        /// <summary>
        /// Gets a single event from an Stream, such as an MVC Controller's Request.InputStream.
        /// </summary>
        /// <param name="inputStream">The Stream to pull the JSON from.</param>
        /// <returns></returns>
        public static EventData GetEvent(Stream inputStream)
        {
            var json = new StreamReader(inputStream).ReadToEnd();
            return GetEvent(json);
        }

        /// <summary>
        /// Gets a single event from an Stream, such as an MVC Controller's Request.InputStream.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EventData to use to deserialize the data. Useful when including custom Identifiers.</typeparam>
        /// <param name="inputStream">The Stream to pull the JSON from.</param>
        /// <returns></returns>
        public static T GetEvent<T>(Stream inputStream) where T : EventData
        {
            var json = new StreamReader(inputStream).ReadToEnd();
            return GetEvent<T>(json);
        }

        /// <summary>
        /// Gets a single Event from a JSON string.
        /// </summary>
        /// <param name="json">A string containing the JSON event data.</param>
        /// <returns></returns>
        public static EventData GetEvent(string json)
        {
            return JsonConvert.DeserializeObject<EventData>(string.Format("[{0}]", json));
        }

        /// <summary>
        /// Gets a single Event from a JSON string.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EventData to use to deserialize the data. Useful when including custom Identifiers.</typeparam>
        /// <param name="json">A string containing the JSON event data.</param>
        /// <returns></returns>
        public static T GetEvent<T>(string json) where T : EventData
        {
            return JsonConvert.DeserializeObject<T>(string.Format("[{0}]", json));
        }

        /// <summary>
        /// Gets a list of Batched Events from an Stream, such as an MVC Controller's Request.InputStream.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static List<EventData> GetEvents(Stream inputStream)
        {
            var json = new StreamReader(inputStream).ReadToEnd();
            return GetEvents(json);
        }

        /// <summary>
        /// Gets a list of Batched Events from an Stream, such as an MVC Controller's Request.InputStream.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EventData to use to deserialize the data. Useful when including custom Identifiers.</typeparam>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static List<T> GetEvents<T>(Stream inputStream) where T : EventData
        {
            var json = new StreamReader(inputStream).ReadToEnd();
            return GetEvents<T>(json);
        }

        /// <summary>
        /// Gets a list of Batched Events from a JSON string.
        /// </summary>
        /// <param name="json">A string containing the JSON batched event data.</param>
        /// <returns></returns>
        public static List<EventData> GetEvents(string json)
        {
            json = string.Format("[{0}]", json.Replace("}" + Environment.NewLine + "{", "},{"));
            return JsonConvert.DeserializeObject<List<EventData>>(json);
        }

        /// <summary>
        /// Gets a list of Batched Events from a JSON string.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EventData to use to deserialize the data. Useful when including custom Identifiers.</typeparam>
        /// <param name="json">A string containing the JSON batched event data.</param>
        /// <returns></returns>
        public static List<T> GetEvents<T>(string json) where T : EventData
        {
            json = string.Format("[{0}]", json.Replace("}" + Environment.NewLine + "{", "},{"));
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

    }
}
