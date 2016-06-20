using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class ClassStudentInfo_mod
    {
        int uid;
        int student_id;
        int class_id;

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

        public int Class_id
        {
            get
            {
                return class_id;
            }

            set
            {
                class_id = value;
            }
        }
    }
}
