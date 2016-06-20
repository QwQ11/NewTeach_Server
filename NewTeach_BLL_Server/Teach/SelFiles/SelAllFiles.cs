using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Teach;
using NewTeach_DAL_Data.Teach;
using NewTeach_DAL_Server;
using NewTeach_BLL_Server;
using Model.Sockets;
using Data;
using NewTeach_DAL_Data;

namespace Newtalking_BLL_Server.Teach.SelFiles
{
    internal class SelAllFiles
    {
        Model.Teach.SelAllFiles_mod saf;
        System.Net.Sockets.TcpClient client;

        internal SelAllFiles(DataPackage data)
        {
            saf = FileSelectionConvert.ConvertToClass_SelAll(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            Sender sender = new Sender();

            try
            {
                SQLService sql = new SQLService();
                List<FileInfo_mod> arrFiles = sql.SelAllFiles(saf.Student_id);

                foreach (FileInfo_mod fi in arrFiles)
                {
                    sender.SendMessage(new DataPackage
                    {
                        Client = client,
                        Data = FileInfoConvert.ConvertToBytes_FileInfo(fi)
                    });
                }

                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FileInfoConvert.ConvertToBytes_Response(new FileRequestResponse_mod
                    {
                        Uid = saf.Uid,
                        Op_code = Flags.FileFlags.FileOPSucceed
                    })
                });
            }
            catch
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FileInfoConvert.ConvertToBytes_Response(new FileRequestResponse_mod
                    {
                        Uid = saf.Uid,
                        Op_code = Flags.FileFlags.FileOPFailed
                    })
                });
            }
        }
    }
}
