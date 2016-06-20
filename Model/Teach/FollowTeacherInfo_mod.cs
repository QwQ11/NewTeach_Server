using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class FollowTeacherInfo_mod
    {
        private int uid;
        private short state;
        private int teacher_id;
        private int student_id;

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public int Student_id
        {
            get { return student_id; }
            set { student_id = value; }
        }

        public int Teacher_id
        {
            get { return teacher_id; }
            set { teacher_id = value; }
        }

        public short State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
