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
    internal class RemoveFollowing
    {
        TcpClient client;
        FollowingData_mod followingData;

        internal RemoveFollowing(DataPackage data, int user_id)
        {
            client = data.Client;
            followingData = FollowingConvert.ConvertToClass(data.Data);
            followingData.User_id = user_id;
        }

        internal bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                bool isSucceed = sql.RemoveFollowing(followingData);

                Sender sender = new Sender();
                DataPackage dpk = new DataPackage();
                dpk.Client = client;
                dpk.Data = FollowingConvert.ConvertToBytes(followingData, isSucceed);
                sender.SendMessage(dpk);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
