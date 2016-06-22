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
    internal class SelClassDynamic
    {
        ClassStudentInfo_mod sti;
        List<ClassInfo_mod> arrCif = new List<ClassInfo_mod>();
        System.Net.Sockets.TcpClient client;

        internal  SelClassDynamic(DataPackage data, int user_id)
        {
            sti = ClassInfoConvert.ConvertToClass_Student(data.Data);
            sti.Student_id = user_id;
            client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            Sender sender = new Sender();
            arrCif = sql.SelClassDynamic(sti.Student_id, sti.Uid);

            if(arrCif!=null&&arrCif.Count!=0)
                foreach (ClassInfo_mod cif in arrCif)
                {
                    sender.SendMessage(
                        new DataPackage
                        {
                            Client = client,
                            Data = ClassInfoConvert.ConvertToBytes_Info(cif)
                        });
                }


            sender.SendMessage(
                new DataPackage
                {
                    Client = client,
                    Data = BitConverter.GetBytes(sti.Uid)
                });
        }
    }
}
