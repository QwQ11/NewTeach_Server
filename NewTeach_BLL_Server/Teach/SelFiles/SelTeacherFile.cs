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
    internal class SelTeacherFile
    {
        Model.Teach.SelTeacherFile_mod stf;
        System.Net.Sockets.TcpClient client;

        internal SelTeacherFile(DataPackage data)
        {
            stf = FileSelectionConvert.ConvertToClass_SelTeacher(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            Sender sender = new Sender();

            try
            {
                SQLService sql = new SQLService();
                List<FileInfo_mod> arrFiles = sql.SelTeacherFiles(stf.Student_id, stf.Teacher_id);

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
                        Uid = stf.Uid,
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
                        Uid = stf.Uid,
                        Op_code = Flags.FileFlags.FileOPFailed
                    })
                });
            }
        }
    }
}
