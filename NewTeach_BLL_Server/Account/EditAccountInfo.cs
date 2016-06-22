using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;
using System.Net;
using NewTeach_DAL_Data;
using Model.Sockets;

namespace NewTeach_BLL_Server.Account
{
    internal class EditAccountInfo
    {
        AccountInfo_mod accountInfo = new AccountInfo_mod();
        DataPackage dataSend = new DataPackage();

        internal EditAccountInfo(DataPackage data, int user_id)
        {
            accountInfo = AccountInfoConvert.ConvertToClass(data.Data);
            accountInfo.User_id = user_id;
            dataSend.Client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            bool isSucceed = sql.AccountInfoEditor(accountInfo);
            dataSend.Data = AccountInfoConvert.ConvertToBytes_Re(isSucceed, accountInfo.Uid);
            Sender sender = new Sender();
            sender.SendMessage(dataSend);
        }
    }
}
