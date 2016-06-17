using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserImage
    {
        private int uid;
        private int user_id;
        private int file_length;

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

        public int User_id
        {
            get
            {
                return user_id;
            }

            set
            {
                user_id = value;
            }
        }

        public int File_length
        {
            get
            {
                return file_length;
            }

            set
            {
                file_length = value;
            }
        }
    }
}
