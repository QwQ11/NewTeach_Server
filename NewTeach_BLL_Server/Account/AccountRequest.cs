using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;
using Data;
using File_DAL;
using Model.Sockets;

namespace NewTeach_BLL_Server.Account
{
    internal class AccountRequest
    {
        DataPackage dataResponse = new DataPackage();

        internal AccountRequest(DataPackage data)
        {
            SQLService sql = new SQLService();
            dataResponse.Client = data.Client;

            LoginData loginData = AccountRequestConvert.ConvertToClass(data.Data);
            loginData.User_id = sql.AccountRequest(loginData.User_password, loginData.Type);

            dataResponse.Data = AccountRequestConvert.ConvertToBytes(loginData);
            FileCheck.CheckCreateUserDir(loginData.User_id);
        }

        internal void Response()
        {
            Sender sender = new Sender();
            sender.SendMessage(dataResponse);
        }
    }
}
