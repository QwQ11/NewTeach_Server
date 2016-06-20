using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Sockets;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;

namespace Newtalking_BLL_Server.File
{
    public class ChangeFileType
    {
        System.Net.Sockets.TcpClient client;
        FileInfo_mod fi;
        int user_id;

        public ChangeFileType(DataPackage data, int user_id)
        {
            fi = FileInfoConvert.ConvertToClass_Transport(data.Data);
            client = data.Client;
            this.user_id = user_id;
        }

        public void Response()
        {
            Sender sender = new Sender();

            try
            {
                SQLService sql = new SQLService();

                if (sql.ChangeFileType(user_id, fi.FileName, fi.File_type))
                {
                    sender.SendMessage(new DataPackage
                    {
                        Client = client,
                        Data = FileInfoConvert.ConvertToBytes_Response(new FileRequestResponse_mod
                        {
                            Uid = fi.Uid,
                            Op_code = Flags.FileFlags.FileOPSucceed
                        })
                    });
                }
                else
                    throw new Exception();
            }
            catch
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FileInfoConvert.ConvertToBytes_Response(new FileRequestResponse_mod
                    {
                        Uid = fi.Uid,
                        Op_code = Flags.FileFlags.FileOPFailed
                    })
                });
            }
        }
    }
}
