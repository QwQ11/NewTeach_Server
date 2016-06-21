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
    internal class BookClass
    {
        ClassStudentInfo_mod csi;
        System.Net.Sockets.TcpClient client;
        int uid;

        internal BookClass(DataPackage data, int user_id)
        {
            csi = ClassInfoConvert.ConvertToClass_Student(data.Data);
            csi.Student_id = user_id;
            client = data.Client;
            uid = csi.Uid;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();
            
            sender.SendMessage(new DataPackage
            {
                Client = client,
                Data = Re_Convert.ConvertToBytes_Op(
                    sql.BookClass(csi.Student_id, csi.Class_id), uid)
            });
        }
    }
}
