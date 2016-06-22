using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Teach;
using NewTeach_DAL_Data.Teach;
using NewTeach_DAL_Server;
using NewTeach_BLL_Server;
using Model.Sockets;
using Data;
using NewTeach_DAL_Data;
using Model;

namespace Newtalking_BLL_Server.Teach.SelFiles
{
    internal class SelFileDynamic
    {
        Model.Teach.SelFileDynamic_mod sfd;
        System.Net.Sockets.TcpClient client;

        internal SelFileDynamic(DataPackage data, int user_id)
        {
            sfd = FileSelectionConvert.ConvertToClass_SelDynamic(data.Data);
            sfd.Student_id = user_id;
            client = data.Client;
        }

        internal void Response()
        {
            Sender sender = new Sender();

            try
            {
                SQLService sql = new SQLService();
                List<FileInfo_mod> arrFiles = sql.SelFileDynamic(sfd.Student_id, sfd.Uid);

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
                        Uid = sfd.Uid,
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
                        Uid = sfd.Uid,
                        Op_code = Flags.FileFlags.FileOPFailed
                    })
                });
            }
        }
    }
}
