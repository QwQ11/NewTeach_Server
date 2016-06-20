using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;

namespace NewTeach_DAL_Data.Teach
{
    public static class FileSelectionConvert
    {
        static public SelAllFiles_mod ConvertToClass_SelAll(byte[] data)
        {
            SelAllFiles_mod saf = new SelAllFiles_mod();
            saf.Uid = BitConverter.ToInt32(data, 2);
            saf.Student_id = BitConverter.ToInt32(data, 6);
            return saf;
        }

        static public SelFileDynamic_mod ConvertToClass_SelDynamic(byte[] data)
        {
            SelFileDynamic_mod sfd = new SelFileDynamic_mod();
            sfd.Uid = BitConverter.ToInt32(data, 2);
            sfd.Student_id = BitConverter.ToInt32(data, 6);
            return sfd;
        }

        static public SelTeacherFile_mod ConvertToClass_SelTeacher(byte[] data)
        {
            SelTeacherFile_mod stf = new SelTeacherFile_mod();
            stf.Uid = BitConverter.ToInt32(data, 2);
            stf.Student_id = BitConverter.ToInt32(data, 6);
            stf.Teacher_id = BitConverter.ToInt32(data, 10);
            return stf;
        }
    }
}
