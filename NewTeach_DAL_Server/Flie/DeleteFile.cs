using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flags;
using File_DAL;
using Model;

namespace NewTeach_DAL_Server.File
{
    public class DeleteFile
    {
        public FileRequestResponse Delete(FileRequest fr)
        {
            string path = FileCheck.SelUserFilePath(fr.User_id, fr.FileName);
            if (path != FileFlags.FileExistsFailedFlag)
            {
                SQLService sql = new SQLService();
                if (sql.DeleteFile(fr.User_id, fr.FileName, fr.FileKey))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                        return new FileRequestResponse { Uid = fr.Uid, Op_code = FileFlags.FileOPSucceed };
                    }
                    catch
                    {
                        return new FileRequestResponse { Uid = fr.Uid, Op_code = FileFlags.FileOPFailed };
                    }
                }
                else
                {
                    return new FileRequestResponse { Uid = fr.Uid, Op_code = FileFlags.FileExistsFalse };
                }
            }
            else
                return new FileRequestResponse { Uid = fr.Uid, Op_code = FileFlags.FileExistsFalse };
        }


    }
}
