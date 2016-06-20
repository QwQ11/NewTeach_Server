using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileRequestResponse_mod
    {
        int uid;
        short op_code;

        //1-允许操作 2-文件已存在 3-文件不存在 4-操作失败 5-操作成功

        public short Op_code
        {
            get
            {
                return op_code;
            }

            set
            {
                op_code = value;
            }
        }

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }
    }
}
