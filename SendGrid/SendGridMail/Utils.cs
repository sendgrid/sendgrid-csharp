using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SendGridMail
{
    public class Utils
    {
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

        public static string SerializeDictionary(IDictionary<String, String> dic)
        {
            return "{"+String.Join(",",dic.Select(kvp => Serialize(kvp.Key) + ":" + Serialize(kvp.Value)))+"}";
        }
    }
}
