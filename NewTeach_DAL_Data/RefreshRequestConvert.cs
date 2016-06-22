using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace NewTeach_DAL_Data
{
    static public class RefreshRequestConvert {
        static public RefreshRequest_mod ConvertToClass(string jData) {
            RefreshRequest_mod r = new RefreshRequest_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "refresh.uid":
                        r.Uid = reader.ReadAsInt32().Value;
                        break;
                    case "refresh.userid":
                        r.User_id = reader.ReadAsInt32().Value;
                        break;
                }
            }
            sr.Close();
            return r;
        }

        static public RefreshRequest_mod ConvertToClass(byte[] data)
        {
            return ConvertToClass(JsonBytesConvert.ToJson(data));
            //RefreshRequest_mod rr = new RefreshRequest_mod();
            //rr.Uid = BitConverter.ToInt32(data, 2);
            //rr.User_id = BitConverter.ToInt32(data, 6);

            //return rr;
        }

        static public byte[] ConvertToBytes_End(Int32 uid) {
            return JsonBytesConvert.ToBytes(ConvertToJson_End(uid), 2);
            //byte[] bResult = new byte[8];

            //BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            //BitConverter.GetBytes(uid).CopyTo(bResult, 2);

            //return bResult;
        }

        static public string ConvertToJson_End(Int32 uid) {
            // Type: 2
            var sw = new StringWriter();
            var writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("refresh");
            writer.WriteStartObject();
            writer.WritePropertyName("uid");
            writer.WriteValue(uid);
            writer.WriteEndObject();
            writer.WriteEndObject();

            writer.Flush();
            var str = sw.ToString();
            sw.Close();

            return str;
        }
    }
}
