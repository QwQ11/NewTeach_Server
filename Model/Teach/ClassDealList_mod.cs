using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Teach
{
    public class ClassDealList_mod
    {
        //deal_list(class_id INTEGER, student_id INTEGER, deal_state TINYINT, deal_price INTEGER, review VARCHER(500), FOREIGN KEY(class_id) REFERENCES classes(class_id));
        int uid;
        int class_id;
        int student_id;
        short deal_state;
        int deal_price;
        string review;

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

        public short Deal_state
        {
            get
            {
                return deal_state;
            }

            set
            {
                deal_state = value;
            }
        }

        public int Deal_price
        {
            get
            {
                return deal_price;
            }

            set
            {
                deal_price = value;
            }
        }

        public string Review
        {
            get
            {
                return review;
            }

            set
            {
                review = value;
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
