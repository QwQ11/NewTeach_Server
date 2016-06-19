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

namespace NewTeach_BLL_Server.Teach.FollowTeacherRequest.Students
{
    internal class RequestFollow
    {
        FollowTeacherInfo fti;
        System.Net.Sockets.TcpClient client;

        internal RequestFollow(DataPackage dataPackage)
        {
            fti = FollowTeacherInfoConvert.ConvertToClass(dataPackage.Data);
            client = dataPackage.Client;
        }

        internal bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                Sender sender = new Sender();

                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FollowTeacherInfoConvert_Re.ConvertToBytes_Query(new FollowTeacherInfo_Re
                    {
                        Uid = fti.Uid,
                        IsSucceed = sql.AddTeacherFollowRequest(fti.Student_id, fti.Teacher_id)
                    })
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
