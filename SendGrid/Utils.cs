using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SendGrid
{
    public static class Utils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                var jsonData = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
                return jsonData;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string SerializeDictionary(IDictionary<string, string> dic)
        {
            return "{"+string.Join(",",dic.Select(kvp => Serialize(kvp.Key) + ":" + Serialize(kvp.Value)))+"}";
        }

    }
}
