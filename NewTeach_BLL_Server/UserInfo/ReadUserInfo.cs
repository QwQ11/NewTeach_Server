using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Server;
using NewTeach_DAL_Data;
using System.Net;

namespace NewTeach_BLL_Server.UserInfo
{
    internal class ReadUserInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        internal ReadUserInfo(DataPackage data)
        {
            accountInfo = AccountInfoConvet.ConvertToClass(data.Data);
            SQLService sql = new SQLService();
            accountInfo = sql.AccountInfoReader(accountInfo.User_id);
            dataSend.Client = data.Client;
            dataSend.Data = AccountInfoConvet.ConvertToBytes(accountInfo);
        }

        internal AccountInfo Response()
        {
            Sender sender = new Sender();
            sender.SendMessage(dataSend);
            return accountInfo;
        }
    }
}