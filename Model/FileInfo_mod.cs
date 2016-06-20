using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FileInfo_mod
    {
        private int uid;
        private int user_id;
        private short file_type;
        private int file_length;
        private string fileName = "";
        private string fileKey = "";

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
        
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
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

        public string FileKey
        {
            get
            {
                return fileKey;
            }

            set
            {
                fileKey = value;
            }
        }

        public short File_type
        {
            get
            {
                return file_type;
            }

            set
            {
                file_type = value;
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
