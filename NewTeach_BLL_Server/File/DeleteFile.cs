using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;

namespace NewTeach_BLL_Server.File
{
    public class DeleteFile
    {
        FileRequest fr;
        System.Net.Sockets.TcpClient client;

        public DeleteFile(DataPackage data, int user_id)
        {
            fr = FileRequestConvert.ConvertToClass_SendDelete(data.Data);
            fr.User_id = user_id;
            client = data.Client;
        }

        public bool Response()
        {
            try
            {
                NewTeach_DAL_Server.File.DeleteFile df = new NewTeach_DAL_Server.File.DeleteFile();
                FileRequestResponse frr = df.Delete(fr);

                Sender sender = new Sender();
                sender.SendMessage(new DataPackage
                {
                    Data = FileRequestConvert.ConvertToBytes_Response(frr),
                    Client = client
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
