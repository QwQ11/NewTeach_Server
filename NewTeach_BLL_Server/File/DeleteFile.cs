using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;
using Model.Sockets;

namespace NewTeach_BLL_Server.File
{
    internal class DeleteFile
    {
        FileInfo_mod fr;
        System.Net.Sockets.TcpClient client;

        internal DeleteFile(DataPackage data, int user_id)
        {
            fr = FileInfoConvert.ConvertToClass_Transport(data.Data);
            fr.User_id = user_id;
            client = data.Client;
        }

        internal bool Response()
        {
            try
            {
                NewTeach_DAL_Server.File.DeleteFile df = new NewTeach_DAL_Server.File.DeleteFile();
                FileRequestResponse_mod frr = df.Delete(fr);

                Sender sender = new Sender();
                sender.SendMessage(new DataPackage
                {
                    Data = FileInfoConvert.ConvertToBytes_Response(frr),
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
