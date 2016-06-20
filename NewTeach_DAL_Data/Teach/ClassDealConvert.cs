using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;

namespace NewTeach_DAL_Data.Teach
{
    static public class ClassDealConvert
    {
        static public ClassDealList_mod ConvertToClass(byte[] data)
        {
            ClassDealList_mod cdl = new ClassDealList_mod();

            cdl.Uid = BitConverter.ToInt32(data, 2);
            cdl.Class_id = BitConverter.ToInt32(data, 6);
            cdl.Student_id = BitConverter.ToInt32(data, 10);
            cdl.Deal_state = BitConverter.ToInt16(data, 14);
            cdl.Deal_price = BitConverter.ToInt32(data, 16);
            cdl.Review = Encoding.Default.GetString(data, 20, 500);

            return cdl;
        }

        static public byte[] ConvertToBytes(ClassDealList_mod data)
        {
            byte[] bResult = new byte[518];

            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Class_id).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.Student_id).CopyTo(bResult, 8);
            BitConverter.GetBytes(data.Deal_state).CopyTo(bResult, 12);
            BitConverter.GetBytes(data.Deal_price).CopyTo(bResult, 14);
            Encoding.Default.GetBytes(data.Review).CopyTo(bResult, 18);

            return bResult;
        }
    }
}
