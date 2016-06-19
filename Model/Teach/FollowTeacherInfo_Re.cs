using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class FollowTeacherInfo_Re
    {
        int uid;
        bool isSucceed;

        public bool IsSucceed
        {
            get { return isSucceed; }
            set { isSucceed = value; }
        }

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }
    }
}
