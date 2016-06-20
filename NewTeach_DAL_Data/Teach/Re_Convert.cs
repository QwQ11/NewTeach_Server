using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;

namespace NewTeach_DAL_Data.Teach
{
    public static class Re_Convert
    {
        static public byte[] ConvertToBytes_Query(Re_mod data)
        {
            byte[] bResult = new byte[6];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.IsSucceed).CopyTo(bResult, 4);

            return bResult;
        }

        static public byte[] ConvertToBytes_SelEnd(int uid)
        {
            byte[] bResult = new byte[8];

            BitConverter.GetBytes((short)2).CopyTo(bResult, 0);
            BitConverter.GetBytes(uid).CopyTo(bResult, 2);

            return bResult;
        }
    }
}
