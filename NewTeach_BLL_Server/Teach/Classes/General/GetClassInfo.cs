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
    internal class GetClassInfo
    {
        ClassInfo_mod ci = new ClassInfo_mod();
        System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();

        internal GetClassInfo(DataPackage data)
        {
            ci = ClassInfoConvert.ConvertToClass_Info(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            ClassInfo_mod ciRece = sql.GetClassInfo(ci.Class_id);

            if (ciRece != null)
            {
                ciRece.Uid = ci.Uid;
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = ClassInfoConvert.ConvertToBytes_Info(ciRece)
                });
            }
        }
    }
}
