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

namespace NewTeach_BLL_Server.Teach.Classes.Teachers
{
    internal class ChangeClassState
    {
        ClassInfo_mod cif;
        System.Net.Sockets.TcpClient client;
        int uid;

        internal ChangeClassState(DataPackage data,  int user_id)
        {
            cif = ClassInfoConvert.ConvertToClass_Info(data.Data);
            cif.Teacher_id = user_id;
            client = data.Client;
            uid = cif.Uid;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            sender.SendMessage(new DataPackage
            {
                Client = client,
                Data = Re_Convert.ConvertToBytes_Op(sql.ChangeClassState(cif.Class_id, cif.Teacher_id, cif.Class_state), uid)
            });
        }
    }
}
