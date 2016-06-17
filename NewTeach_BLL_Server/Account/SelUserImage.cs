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

namespace NewTeach_BLL_Server.Account
{
    internal class SelUserImage
    {
        DataPackage dataPack;
        FileStream fsSend;
        string path;
        int uid;

        internal SelUserImage(DataPackage dataPackTemp)
        {
            UserImage userImage = SelUserImageConvert.ConvertToClass(dataPackTemp.Data);
            uid = userImage.Uid;
            //SQLService sql = new SQLService();
            //string strFileName = sql.SelUserImageName(userImage.User_id);
            path = FileCheck.SelUserImage(userImage.User_id, Flags.FileFlags.DefaultUserImageFileName);
        }

        internal bool Send()
        {
            Sender sender = new Sender();
            if (path == FileFlags.FileExistsFailedFlag)
            {
                sender.SendMessage(new DataPackage
                {
                    Data = FileRequestConvert.ConvertToBytes_Response(new FileRequestResponse
                    {
                        Uid = uid,
                        Op_code = Flags.FileFlags.FileExistsFalse
                    })
                });

                return false;
            }
            else
                fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);

            sender.SendMessage(new DataPackage
            {
                Data = SelUserImageConvert.ConvertToBytes(new UserImage
                {
                    Uid = uid,
                    File_length = GetFileInfo.GetLength(path)
                })
            });

            NewTeach_DAL_Server.SendFile sendFile = new NewTeach_DAL_Server.SendFile(dataPack.Client, fsSend);
            return sendFile.Send();
        }
    }
}
