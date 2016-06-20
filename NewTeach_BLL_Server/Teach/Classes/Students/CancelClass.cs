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

namespace NewTeach_BLL_Server.Teach.Classes.Students
{
    internal class CancelClass
    {
        ClassStudentInfo_mod csi;
        System.Net.Sockets.TcpClient client;

        internal CancelClass(DataPackage data)
        {
            csi = ClassInfoConvert.ConvertToClass_Student(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            if (sql.CancelClass(csi.Student_id, csi.Class_id))
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = Re_Convert.ConvertToBytes_Query(new Re_mod
                    {
                        Uid = csi.Uid,
                        IsSucceed = true
                    })
                });
            }
            else
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = Re_Convert.ConvertToBytes_Query(new Re_mod
                    {
                        Uid = csi.Uid,
                        IsSucceed = false
                    })
                });
            }
        }
    }
}
