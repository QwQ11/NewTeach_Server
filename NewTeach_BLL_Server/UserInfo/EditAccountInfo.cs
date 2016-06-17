using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using NewTeach_DAL_Data;
using NewTeach_DAL_Server;
using System.Net;
using NewTeach_DAL_Data;

namespace NewTeach_BLL_Server.UserInfo
{
    internal class EditAccountInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        internal EditAccountInfo(DataPackage data, int user_id)
        {
            accountInfo = AccountInfoConvet.ConvertToClass(data.Data);
            accountInfo.User_id = user_id;
            dataSend.Client = data.Client;
        }

        internal void Response()
        {
            SQLService sql = new SQLService();
            bool isSucceed = sql.AccountInfoEditor(accountInfo);
            dataSend.Data = AccountInfoConvet.ConvertToBytes_Re(isSucceed, accountInfo.Uid);
            Sender sender = new Sender();
            sender.SendMessage(dataSend);
        }
    }
}
