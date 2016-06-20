using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class ClassInfo_mod
    {
        int uid;
        int teacher_id;
        short subject;
        DateTime start_time;
        DateTime end_time;
        int class_id;
        short class_state;
        int student_count;
        string class_introduction;

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

        public short Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
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

        public DateTime Start_time
        {
            get
            {
                return start_time;
            }

            set
            {
                start_time = value;
            }
        }

        public DateTime End_time
        {
            get
            {
                return end_time;
            }

            set
            {
                end_time = value;
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

        public short Class_state
        {
            get
            {
                return class_state;
            }

            set
            {
                class_state = value;
            }
        }

        public int Student_count
        {
            get
            {
                return student_count;
            }

            set
            {
                student_count = value;
            }
        }

        public string Class_introduction
        {
            get
            {
                return class_introduction;
            }

            set
            {
                class_introduction = value;
            }
        }
    }
}
