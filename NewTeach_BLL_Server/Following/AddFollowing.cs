using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;
using Model;
using System.Net.Sockets;
using Model.Sockets;

namespace NewTeach_BLL_Server.Following
{
    internal class AddFollowing
    {
        FollowingData_mod followingData;
        TcpClient client;

        internal AddFollowing(DataPackage data, int user_id)
        {
            followingData = FollowingConvert.ConvertToClass(data.Data);
            followingData.User_id = user_id;
            client = data.Client;
        }

        internal bool Response()
        {
            try
            {
                SQLService sql = new SQLService();

                Sender sender = new Sender();
                DataPackage dpk = new DataPackage();
                dpk.Client = client;
                dpk.Data = FollowingConvert.ConvertToBytes(followingData.Uid, sql.AddFollowing(followingData));

                return sender.SendMessage(dpk);
                //bool isSucceed = sql.AddFollowing(add);
            }
            catch
            {
                return false;
            }
        }
    }
}
