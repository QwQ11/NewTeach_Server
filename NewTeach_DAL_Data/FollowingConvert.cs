using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace NewTeach_DAL_Data
{
    static public class FollowingConvert
    {
        static public FollowingData_mod ConvertToClass(byte[] data)
        {
            FollowingData_mod followingData = new FollowingData_mod();
            followingData.Uid = BitConverter.ToInt32(data, 2);
            followingData.User_id = BitConverter.ToInt32(data, 6);
            followingData.Following_id = BitConverter.ToInt32(data, 10);

            return followingData;
        }

        static public byte[] ConvertToBytes(int uid, bool isSucceed)
        {
            byte[] bResult = new byte[6];

            BitConverter.GetBytes(uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(isSucceed).CopyTo(bResult, 4);

            return bResult;
        }
    }
}
