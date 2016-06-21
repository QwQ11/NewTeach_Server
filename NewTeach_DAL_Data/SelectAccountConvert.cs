using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace NewTeach_DAL_Data
{
    static public class SelectAccountConvert {
        static public SelectAccount_mod ConvertToClass(string jData) {
            SelectAccount_mod r = new SelectAccount_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "account.uid":
                        r.Uid = reader.ReadAsInt32().Value;
                        break;
                    case "account.selinfo":
                        r.Sel_info = reader.ReadAsString();
                        break;
                }
            }
            sr.Close();
            return r;
        }
        static public SelectAccount_mod ConvertToClass(byte[] data)
        {
            return ConvertToClass(JsonBytesConvert.ToJson(data));
            //SelectAccount_mod sel = new SelectAccount_mod();
            //sel.Uid = BitConverter.ToInt32(data, 2);
            //sel.Sel_info = Encoding.Default.GetString(data, 6, 30);

            //return sel;
        }
    }
}
