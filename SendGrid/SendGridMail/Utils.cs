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

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16*1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

    }
}
