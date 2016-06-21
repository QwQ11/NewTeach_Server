using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace NewTeach_DAL_Data
{
    static public class MessageDataConvert {
        // Type: 1
        static public string ConvertToJson(MessageData_mod data) {
            var sw = new StringWriter();
            var writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("message");
            writer.WriteStartObject();
            writer.WritePropertyName("userid");
            writer.WriteValue(data.User_id);
            writer.WritePropertyName("receiverid");
            writer.WriteValue(data.Receiver_id);
            writer.WritePropertyName("time");
            writer.WriteValue(data.Time.Ticks);
            writer.WritePropertyName("message");
            writer.WriteValue(data.Message);
            writer.WriteEndObject();
            writer.WriteEndObject();

            writer.Flush();
            var str = sw.ToString();
            sw.Close();

            return str;
        }
        static public byte[] ConvertToBytes(MessageData_mod data)
        {
            return JsonBytesConvert.ToBytes(ConvertToJson(data));
            //byte[] bResult = new byte[1452];

            //short type = 1;
            //BitConverter.GetBytes(type).CopyTo(bResult, 0);
            //BitConverter.GetBytes(data.User_id).CopyTo(bResult, 2);
            //BitConverter.GetBytes(data.Receiver_id).CopyTo(bResult, 6);
            //BitConverter.GetBytes(data.Time.Ticks).CopyTo(bResult, 10);
            //Encoding.Default.GetBytes(data.Message).CopyTo(bResult, 18);

            //return bResult;
        }

        static public MessageData_mod ConvertToClass(string jData) {
            MessageData_mod r = new MessageData_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "message.userid":
                        r.User_id = reader.ReadAsInt32().Value;
                        break;
                    case "message.receiverid":
                        r.Receiver_id = reader.ReadAsInt32().Value;
                        break;
                    case "message.time":
                        r.Time = new DateTime(reader.ReadAsInt32().Value);
                        break;
                    case "message.message":
                        r.Message = reader.ReadAsString();
                        break;
                }
            }
            sr.Close();
            return r;
        }

        static public MessageData_mod ConvertToClass(byte[] bReceived) {
            return ConvertToClass(JsonBytesConvert.ToJson(bReceived));
            //MessageData_mod dataResult = new MessageData_mod();

            //dataResult.User_id = BitConverter.ToInt32(bReceived, 2);
            //dataResult.Receiver_id = BitConverter.ToInt32(bReceived, 6);
            //long timeTick = BitConverter.ToInt64(bReceived, 10);
            //dataResult.Time = new DateTime(timeTick);
            //string msgTemp = Encoding.Default.GetString(bReceived, 18, 1434);

            //foreach (char c in msgTemp) {
            //    if (c == '\0')
            //        break;
            //    dataResult.Message += c;
            //}

            //return dataResult;
        }
    }
}
