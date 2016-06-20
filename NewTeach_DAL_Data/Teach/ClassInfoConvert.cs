using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;

namespace NewTeach_DAL_Data.Teach
{
    public static class ClassInfoConvert
    {
        public static ClassInfo_mod ConvertToClass_Info(byte[] data)
        {
            ClassInfo_mod ci = new ClassInfo_mod();

            ci.Uid = BitConverter.ToInt32(data, 2);
            ci.Teacher_id = BitConverter.ToInt32(data, 6);
            ci.Subject = BitConverter.ToInt16(data, 10);
            long tick = BitConverter.ToInt64(data, 12);
            ci.Start_time = new DateTime(tick);
            tick = BitConverter.ToInt64(data, 20);
            ci.End_time = new DateTime(tick);
            ci.Class_id = BitConverter.ToInt32(data, 28);
            ci.Class_state = BitConverter.ToInt16(data, 32);
            ci.Student_count = BitConverter.ToInt32(data, 34);
            ci.Class_introduction = Encoding.Default.GetString(data, 38, 200);

            return ci;
        }

        public static byte[] ConvertToBytes_Info(ClassInfo_mod data)
        {
            byte[] bResult = new byte[236];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Teacher_id).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.Subject).CopyTo(bResult, 8);
            BitConverter.GetBytes(data.Start_time.Ticks).CopyTo(bResult, 10);
            BitConverter.GetBytes(data.End_time.Ticks).CopyTo(bResult, 18);
            BitConverter.GetBytes(data.Class_id).CopyTo(bResult, 26);
            BitConverter.GetBytes(data.Class_state).CopyTo(bResult, 30);
            BitConverter.GetBytes(data.Student_count).CopyTo(bResult, 32);
            Encoding.Default.GetBytes(data.Class_introduction).CopyTo(bResult, 36);

            return bResult;
        }

        public static ClassStudentInfo_mod ConvertToClass_Student(byte[] data)
        {
            ClassStudentInfo_mod csi = new ClassStudentInfo_mod();

            csi.Uid = BitConverter.ToInt32(data, 2);
            csi.Student_id = BitConverter.ToInt32(data, 6);
            csi.Class_id = BitConverter.ToInt32(data, 10);

            return csi;
        }

        public static byte[] ConvertToBytes_Student(ClassStudentInfo_mod data)
        {
            byte[] bResult = new byte[12];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Student_id).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.Class_id).CopyTo(bResult, 8);

            return bResult; 
        }
    }
}
