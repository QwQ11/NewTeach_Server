using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTeach_DAL_Data {
    static class JsonBytesConvert {
        static public Byte[] ToBytes(string json) {
            return Encoding.Default.GetBytes(json);
        }
        static public Byte[] ToBytes(string json, int type) {
            var jBytes = Encoding.Default.GetBytes(json);
            var r = new Byte[jBytes.Length + 2];
            BitConverter.GetBytes((short)2).CopyTo(r, 0);
            jBytes.CopyTo(r, 2);

            return r;
        }
        static public string ToJson(Byte[] data) {
            return Encoding.Default.GetString(data);
        }
    }
}
