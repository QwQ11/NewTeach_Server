using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;

namespace NewTeach_DAL_Data.Teach
{
    static public class FollowTeacherInfoConvert
    {
        static public FollowTeacherInfo ConvertToClass(byte[] data)
        {
            FollowTeacherInfo fti = new FollowTeacherInfo();

            fti.Uid = BitConverter.ToInt32(data, 2);
            fti.State = BitConverter.ToInt16(data, 6);
            fti.Teacher_id = BitConverter.ToInt32(data, 8);
            fti.Student_id = BitConverter.ToInt32(data, 12);

            return fti;
        }

        static public byte[] ConvertToBytes(FollowTeacherInfo data)
        {
            byte[] bResult = new byte[14];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.State).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.Teacher_id).CopyTo(bResult, 6);
            BitConverter.GetBytes(data.Student_id).CopyTo(bResult, 10);

            return bResult;
        }
    }
}
