using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class SelTeacherFile_mod
    {
        int uid;
        int student_id;
        int teacher_id;

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

        public int Teacher_id
        {
            get
            {
                return teacher_id;
            }

            set
            {
                teacher_id = value;
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
