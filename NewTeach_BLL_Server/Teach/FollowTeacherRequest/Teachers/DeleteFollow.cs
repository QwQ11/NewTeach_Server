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

namespace NewTeach_BLL_Server.Teach.FollowTeacherRequest.Teachers
{
    internal class DeleteFollow
    {
        FollowTeacherInfo_mod fti;
        System.Net.Sockets.TcpClient client;

        internal DeleteFollow(DataPackage dataPackage, int user_id)
        {
            fti = FollowTeacherInfoConvert.ConvertToClass(dataPackage.Data);
            client = dataPackage.Client;
            fti.Teacher_id = user_id;
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
                    Data = Re_Convert.ConvertToBytes_Query(new Re_mod
                    {
                        Uid = fti.Uid,
                        IsSucceed = sql.DeleteTeacherFollow(fti.Student_id, fti.Teacher_id)
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
