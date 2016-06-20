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
    internal class Deal
    {
        ClassDealList_mod cdl;
        System.Net.Sockets.TcpClient client;
        int uid;

        internal Deal(DataPackage data)
        {
            cdl = ClassDealConvert.ConvertToClass(data.Data);
            client = data.Client;
            uid = cdl.Uid;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            sender.SendMessage(new DataPackage
            {
                Client = client,
                Data = Re_Convert.ConvertToBytes_Op(sql.DealAndWriteReview(cdl.Class_id, cdl.Student_id, cdl.Review), uid)
            });
        }
    }
}
