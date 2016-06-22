using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;
using Newtonsoft.Json;
using System.IO;

namespace NewTeach_DAL_Data.Teach
{
    public static class FileSelectionConvert
    {
        static public SelAllFiles_mod ConvertToClass_SelAll(string jData) {
            var r = new SelAllFiles_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "fileselection.uid":
                        r.Uid = reader.ReadAsInt32().Value;
                        break;
                    case "fileselection.studentid":
                        r.Student_id = reader.ReadAsInt32().Value;
                        break;
                }
            }
            sr.Close();
            return r;
        }

        static public SelAllFiles_mod ConvertToClass_SelAll(byte[] data)
        {
            return ConvertToClass_SelAll(JsonBytesConvert.ToJson(data));
            //SelAllFiles_mod saf = new SelAllFiles_mod();
            //saf.Uid = BitConverter.ToInt32(data, 2);
            //saf.Student_id = BitConverter.ToInt32(data, 6);
            //return saf;
        }

        static public SelFileDynamic_mod ConvertToClass_SelDynamic(string jData) {
            var r = new SelFileDynamic_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "fileselection.uid":
                        r.Uid = reader.ReadAsInt32().Value;
                        break;
                    case "fileselection.studentid":
                        r.Student_id = reader.ReadAsInt32().Value;
                        break;
                }
            }
            sr.Close();
            return r;
        }

        static public SelFileDynamic_mod ConvertToClass_SelDynamic(byte[] data)
        {
            return ConvertToClass_SelDynamic(JsonBytesConvert.ToJson(data));
            //SelFileDynamic_mod sfd = new SelFileDynamic_mod();
            //sfd.Uid = BitConverter.ToInt32(data, 2);
            //sfd.Student_id = BitConverter.ToInt32(data, 6);
            //return sfd;
        }

        static public SelTeacherFile_mod ConvertToClass_SelTeacher(string jData) {
            var r = new SelTeacherFile_mod();

            var sr = new StringReader(jData);
            var reader = new JsonTextReader(sr);

            while (reader.Read()) {
                switch (reader.Path) {
                    case "fileselction.uid":
                        r.Uid = reader.ReadAsInt32().Value;
                        break;
                    case "fileselction.studentid":
                        r.Student_id = reader.ReadAsInt32().Value;
                        break;
                    case "fileselction.teacherid":
                        r.Teacher_id = reader.ReadAsInt32().Value;
                        break;
                }
            }
            sr.Close();
            return r;
        }

        static public SelTeacherFile_mod ConvertToClass_SelTeacher(byte[] data)
        {
            return ConvertToClass_SelTeacher(JsonBytesConvert.ToJson(data));
            //SelTeacherFile_mod stf = new SelTeacherFile_mod();
            //stf.Uid = BitConverter.ToInt32(data, 2);
            //stf.Student_id = BitConverter.ToInt32(data, 6);
            //stf.Teacher_id = BitConverter.ToInt32(data, 10);
            //return stf;
        }
    }
}
