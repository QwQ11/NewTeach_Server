using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class SelAllFiles_mod
    {
        int uid;
        int student_id;

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

        public int Student_id
        {
            get
            {
                return student_id;
            }

            set
            {
                student_id = value;
            }
        }
    }
}
