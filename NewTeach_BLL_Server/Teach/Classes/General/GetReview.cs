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

namespace NewTeach_BLL_Server.Teach.Classes.General
{
    internal class GetReview
    {
        int uid;
        int class_id;
        List<ClassDealList_mod> arrDealList;
        System.Net.Sockets.TcpClient client;

        internal GetReview(DataPackage data)
        {
            ClassInfo_mod class_info = ClassInfoConvert.ConvertToClass_Info(data.Data);
            uid = class_info.Uid;
            class_id = class_info.Class_id;
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            arrDealList = sql.GetReviews(class_id);
            if (arrDealList != null && arrDealList.Count != 0)
            {
                foreach(ClassDealList_mod cdl in arrDealList)
                {
                    sender.SendMessage(new DataPackage
                    {
                        Client = client,
                        Data = ClassDealConvert.ConvertToBytes(cdl)
                    });
                }
            }
        }
    }
}
