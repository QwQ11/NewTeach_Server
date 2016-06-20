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
    internal class AllowFollow
    {
        FollowTeacherInfo_mod fti;
        System.Net.Sockets.TcpClient client;

        internal AllowFollow(DataPackage data)
        {
            fti = FollowTeacherInfoConvert.ConvertToClass(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            if(sql.TeacherAllowFollow(fti.Teacher_id,fti.Student_id))
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FollowTeacherInfoConvert_Re.ConvertToBytes_Query(new FollowTeacherInfo_Re_mod
                    {
                        Uid = fti.Uid,
                        IsSucceed = true
                    })
                });
            }
            else
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = FollowTeacherInfoConvert_Re.ConvertToBytes_Query(new FollowTeacherInfo_Re_mod
                    {
                        Uid = fti.Uid,
                        IsSucceed = false
                    })
                });
            }
        }
    }
}
