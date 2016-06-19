using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewTeach_BLL_Server.File;
using NewTeach_DAL_Server;
using Model;
using NewTeach_DAL_Data;
using System.Net.Sockets;
using File_DAL;
using Model.Sockets;

namespace NewTeach_BLL_Server.Account
{
    internal class UploadUserImage
    {
        UserImage userImage;
        TcpClient client;

        internal UploadUserImage(DataPackage data, int user_id)
        {
            userImage = SelUserImageConvert.ConvertToClass(data.Data);
            userImage.User_id = user_id;
            client = data.Client;
        }

        internal bool Receive()
        {
            try
            {
                //恢复文件名
                string[] strs = FileCheck.CheckCreateUserDir(userImage.User_id);
                string path = strs[0] + Flags.FileFlags.DefaultUserImageFileName;
                
                NewTeach_DAL_Server.ReceiveFile rece = new NewTeach_DAL_Server.ReceiveFile(client, new WriteFile(path), userImage.File_length);
                if (!rece.Receive())
                    return false;

                //SQLService sql = new SQLService();
                //sql.ChangeUser_Image(userImage.User_id, userImage.File_name);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
