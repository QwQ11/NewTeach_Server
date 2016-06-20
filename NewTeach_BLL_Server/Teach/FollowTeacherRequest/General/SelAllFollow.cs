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

namespace Newtalking_BLL_Server.Teach.FollowTeacherRequest.General
{
    internal class SelAllFollow
    {
        System.Net.Sockets.TcpClient client;
        int uid;
        int user_id;
        short type;

        internal SelAllFollow(DataPackage data, int user_id, short type)
        {
            uid = BitConverter.ToInt32(data.Data, 0);
            this.user_id = user_id;
            this.type = type;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            List<FollowTeacherInfo_mod> arrFti;
            Sender sender = new Sender();

            if(type==1)
                arrFti = sql.SelAllFollow_Student(user_id, uid);
            else
                arrFti = sql.SelAllFollow_Student(user_id, uid);

            if (arrFti != null && arrFti.Count != 0)
            {
                foreach (FollowTeacherInfo_mod fti in arrFti)
                {
                    sender.SendMessage(new DataPackage
                    {
                        Data = FollowTeacherInfoConvert.ConvertToBytes(fti),
                        Client = client
                    });
                }

                sender.SendMessage(new DataPackage
                {
                    Data = Re_Convert.ConvertToBytes_SelEnd(uid),
                    Client = client
                });
            }
        }
    }
}
