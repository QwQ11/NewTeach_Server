using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace NewTeach_DAL_Data
{
    static public class SelUserImageConvert
    {
        static public UserImage_mod ConvertToClass(byte[] data)
        {
            UserImage_mod userImage = new UserImage_mod();

            userImage.Uid = BitConverter.ToInt32(data, 2);
            userImage.User_id = BitConverter.ToInt32(data, 6);
            userImage.File_length = BitConverter.ToInt32(data, 10);

            return userImage;
        }

        static public byte[] ConvertToBytes(UserImage_mod data)
        {
            byte[] bResult = new byte[12];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.File_length).CopyTo(bResult, 8);

            return bResult;
        }
    }
}
