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
    public class TurnDownFollow
    {
        FollowTeacherInfo_mod fti;
        System.Net.Sockets.TcpClient client;

        internal TurnDownFollow(DataPackage data)
        {
            fti = FollowTeacherInfoConvert.ConvertToClass(data.Data);
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();

            if (sql.TeacherTurnDownFollow(fti.Teacher_id, fti.Student_id))
            {
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = Re_Convert.ConvertToBytes_Query(new BoolRe_mod
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
                    Data = Re_Convert.ConvertToBytes_Query(new BoolRe_mod
                    {
                        Uid = fti.Uid,
                        IsSucceed = false
                    })
                });
            }
        }
    }
}
