using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Server;
using System.IO;
using File_DAL;
using NewTeach_DAL_Data;
using Flags;
using Model.Sockets;

namespace NewTeach_BLL_Server.File
{
    internal class SendFile
    {
        DataPackage dataPack;
        FileStream fsSend;
        string path;
        bool isAccess = true;
        int uid;

        internal SendFile(DataPackage dataPackTemp)
        {
            dataPack = dataPackTemp;
            //Buffer.BlockCopy(dataPackTemp.Data, 2, bPackBegin, 0, 4);

            Model.FileInfo_mod fr = FileInfoConvert.ConvertToClass_Transport(dataPackTemp.Data);
            path = FileCheck.SelUserFilePath(fr.User_id, fr.FileName);
            uid = fr.Uid;

            SQLService sql = new SQLService();
            if (sql.CheckFileKey(fr.User_id, fr.FileName, fr.FileKey))
                isAccess = false;
        }

        internal bool Send()
        {
            if (isAccess)
            {
                if (path == FileFlags.FileExistsFailedFlag)
                    return false;
                else
                    fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);
                NewTeach_DAL_Server.SendFile sender = new NewTeach_DAL_Server.SendFile(dataPack.Client, fsSend);
                return sender.Send();
            }
            else
            {
                FileRequestResponse_mod frr = new FileRequestResponse_mod
                {
                    Uid = uid,
                    Op_code = Flags.FileFlags.FileExistsTrue
                };

                Sender sender = new Sender();

                sender.SendMessage(new DataPackage
                {
                    Client = dataPack.Client,
                    Data = FileInfoConvert.ConvertToBytes_Response(frr)
                });
                return false;
            }
        }
    }
}
