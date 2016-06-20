using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace NewTeach_DAL_Data
{
    static public class FileInfoConvert
    {
        static public FileInfo_mod ConvertToClass_Transport(byte[] data)
        {
            FileInfo_mod fileRequest = new FileInfo_mod();
            fileRequest.Uid = BitConverter.ToInt32(data, 2);
            fileRequest.User_id = BitConverter.ToInt32(data, 6);
            fileRequest.File_type = BitConverter.ToInt16(data, 10);
            fileRequest.File_length = BitConverter.ToInt32(data, 12);

            string temp = Encoding.Default.GetString(data, 16, 255);
            foreach (char c in temp)
            {
                if (c == '\0')
                    break;
                fileRequest.FileName += c;
            }

            fileRequest.FileKey = Encoding.Default.GetString(data, 271, 16);
            return fileRequest;
        }

        static public UploadFileRequest_mod ConvertToClass_FileUpLoad(byte[] data)
        {
            UploadFileRequest_mod receiveFile = new UploadFileRequest_mod();
            receiveFile.Uid = BitConverter.ToInt32(data, 2);
            receiveFile.User_id = BitConverter.ToInt32(data, 6);
            receiveFile.File_type = BitConverter.ToInt16(data, 10);       
            receiveFile.File_length = BitConverter.ToInt16(data, 12);
            receiveFile.File_name_length = BitConverter.ToInt16(data, 16);
            receiveFile.File_name = Encoding.Default.GetString(data, 18, receiveFile.File_name_length);
            return receiveFile;
        }

        static public byte[] ConvertToBytes_FileInfo(FileInfo_mod data)
        {
            byte[] bResult = new byte[285];
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 4);
            BitConverter.GetBytes(data.File_type).CopyTo(bResult, 8);
            BitConverter.GetBytes(data.File_length).CopyTo(bResult, 10);
            Encoding.Default.GetBytes(data.FileName).CopyTo(bResult, 14);
            Encoding.Default.GetBytes(data.FileKey).CopyTo(bResult, 269);

            return bResult;
        }

        static public byte[] ConvertToBytes_Response(FileRequestResponse_mod frr)
        {
            byte[] data = new byte[6];

            BitConverter.GetBytes(frr.Uid).CopyTo(data, 0);
            BitConverter.GetBytes(frr.Op_code).CopyTo(data, 4);

            return data;
        }
    }
}
