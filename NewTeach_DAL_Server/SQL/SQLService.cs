using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using MySQLDriverCS;
using System.Data.Common;
using System.Data;
using System.Collections;
using Flags;

namespace NewTeach_DAL_Server
{
    //防注入修改：参数化查询
    //using (SqlCommand Cmd = Conn.CreateCommand())
    //            {
    //                Cmd.CommandText = "select * from TB_Users where passport=@UN and password=@PWD";
    //                Cmd.Parameters.Add(new SqlParameter("UN", Passport));
    //                Cmd.Parameters.Add(new SqlParameter("PWD", Password));


    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    //                                 警告！防注入修改缺失
    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    public class SQLService
    {
        string sql = "";
        MySQLConnection con = new MySQLConnection(new MySQLConnectionString("localhost", "NewTeach", "root", Server_Properties.Property.SqlKey).AsString);

        public bool Login(LoginData_mod data)
        {
            try
            {
                con.Open();
                sql = "SELECT user_password from users where user_id = " + data.User_id + "";
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    if (reader["user_password"].ToString() == data.User_password)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public int AccountRequest(string pwd, short type)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO users(user_password, type) VALUES(" + pwd + "," + type + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                sql = "SELECT * FROM users WHERE user_id = LAST_INSERT_ID()";
                com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                int user_id = 0;
                if (reader.Read())
                {
                    user_id = (int)reader["user_id"];
                }
                sql = "INSERT INTO users_information VALUES(null,null,null,null)";
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return user_id;
            }
            catch { return 0; }
            finally
            {
                con.Close();
            }
        }

        public AccountInfo_mod AccountInfoReader(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT* FROM users_information WHERE user_id = " + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    AccountInfo_mod accountInfo = new AccountInfo_mod();
                    accountInfo.Sex = (short)reader["user_sex"];
                    accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                    accountInfo.Phone = reader["uesr_phome"].ToString();
                    return accountInfo;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AccountInfoEditor(AccountInfo_mod accountInfo)
        {
            try
            {
                con.Open();
                sql = "UPDATE users_information SET user_sex = " + accountInfo.Sex + ", user_birthday = '" + accountInfo.Birthday.ToString("yyyy-MM-dd") + "', user_phone = " + accountInfo.Phone + " WHERE user_id = " + accountInfo.User_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }

        }

        public bool CheckOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "INSERT INTO user_online(user_id) VALUES(" + user_id + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DelOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "DELETE user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertIntoOverMessages(MessageData_mod msg)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO over_messages(sender_id,receiver_id,time,message) VALUES(" + msg.User_id + "," + msg.Receiver_id + ",'" + msg.Time.ToString("yy-MM-dd hh:mm:ss") + "'," + msg.Message.Trim() + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<MessageData_mod> SelOverMessages(int user_id)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                sql = "SELECT * FROM over_messages WHERE receiver_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataAdapter adp = new MySQLDataAdapter(com);
                adp.Fill(ds);

                List<MessageData_mod> messages = new List<MessageData_mod>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MessageData_mod msg = new MessageData_mod();
                    msg.User_id = (int)dr["sender_id"];
                    msg.Receiver_id = (int)dr["receiver_id"];
                    msg.Time = DateTime.Parse(dr["time"].ToString());
                    msg.Message = dr["message"].ToString();
                    messages.Add(msg);
                }

                sql = "DELETE FROM over_messages WHERE receiver_id=" + user_id;
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public ArrayList SearchAccount(SelectAccount_mod selAccount)
        {
            try
            {
                //AccountInfo accountInfo = new AccountInfo();
                //accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                //accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                //accountInfo.Phone = reader["uesr_phome"].ToString();
                //return accountInfo;
                con.Open();
                sql = "INNER JOIN users_information ON users_information.user_name LIKE '%" + selAccount.Sel_info + "%'";
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataAdapter adp = new MySQLDataAdapter(com);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                ArrayList arr = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AccountInfo_mod accountInfo = new AccountInfo_mod();
                    accountInfo.User_id = Int32.Parse(dr["user_id"].ToString());
                    accountInfo.User_name = dr["user_name"].ToString();
                    accountInfo.Sex = short.Parse(dr["user_sex"].ToString());
                    accountInfo.Birthday = DateTime.Parse(dr["user_birthday"].ToString());
                    accountInfo.Phone = dr["uesr_phome"].ToString();
                    arr.Add(accountInfo);
                }

                try
                {
                    sql = "SELECT * FROM users INNER JOIN users_information ON users.user_id = " + Int32.Parse(selAccount.Sel_info);
                    com = new MySQLCommand(sql, con);
                    DbDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        AccountInfo_mod accountInfo = new AccountInfo_mod();
                        accountInfo.User_id = Int32.Parse(reader["user_id"].ToString());
                        accountInfo.User_name = reader["user_name"].ToString();
                        accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                        accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                        accountInfo.Phone = reader["uesr_phome"].ToString();
                        arr.Add(accountInfo);
                    }
                }
                catch
                {

                }

                return arr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddFollowing(FollowingData_mod data)
        {
            try
            {
                con.Open();

                sql = "INSERT INTO following_list(user_id,follow_id) VALUES(" + data.User_id + "," + data.Following_id + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                sql = "SELECT * FROM black_list WHERE user_id=" + data.User_id + " and black_id=" + data.Following_id;
                com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                {
                    sql = "DELETE FROM black_list WHERE user_id=" + data.User_id + " and black_id=" + data.Following_id;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool RemoveFollowing(FollowingData_mod data)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM following_list where user_id=" + data.User_id + " and follow_id=" + data.Following_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                {
                    sql = "DELETE FROM following_list where user_id=" + data.User_id + " and follow_id=" + data.Following_id;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public string SelUserImageName(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM user_images WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    return reader["file_name"].ToString();
                else
                    return FileFlags.FileExistsFailedFlag;
            }
            catch
            {
                return FileFlags.FileExistsFailedFlag;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertUserImage(int user_id, string strFileName)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO user_images(user_id, file_name) VALUES(" + user_id + ", " + strFileName + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool ChangeUser_Image(int user_id, string strFileName)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM user_images WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    sql = "DELETE FROM uesr_images WHERE file_name=" + strFileName;
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                }

                sql = "INSERT INTO user_images(user_id, file_name) VALUES(" + user_id + ", " + strFileName + ")";
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeleteFile(int user_id, string file_name, string file_key)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM files WHERE file_name=" + file_name + " AND user_id=" + user_id + " AND file_key=" + file_key;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (!reader.Read())
                    return false;

                sql = "DELETE FROM files WHERE file_name=" + file_name + " AND user_id=" + user_id + " AND file_key=" + file_key;
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                //从文件动态表中删除

                sql = "DELETE FROM file_dynamic WHERE file_name=" + file_name + " AND teacher_id=" + user_id;
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {

            }
        }

        public bool UpLoadFile(int user_id, string strFileName, string strFileKey, int file_length, int file_type)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM files WHERE file_name=" + strFileName + " AND user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    return false;
                else
                {
                    sql = "INSERT INTO files(user_id, file_name, file_key, file_length, file_type) VALUES(" + user_id + ", " + strFileName + ", " + strFileKey + "" + file_length + "," + file_type + ")";
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                    //添加到文件动态表

                    sql = "SELECT * FROM students WHERE teacher_id=" + user_id;
                    com = new MySQLCommand(sql, con);
                    reader = com.ExecuteReaderEx();

                    List<int> arrStudents = new List<int>();
                    while (reader.Read())
                        arrStudents.Add((int)reader["student_id"]);

                    foreach (int student_id in arrStudents)
                    {
                        sql = "INSERT INTO file_dynamic(teacher_id, student_id, file_name) VALUES(" + user_id + "," + student_id + "," + strFileName + ")";
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool CheckFileKey(int user_id, string strFileName, string strFileKey)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM files WHERE file_name=" + strFileName + " AND user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    if (reader["file_key"].ToString() == strFileKey)
                        return true;
                    else
                        return false;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool ChangeFileType(int user_id, string file_name, short file_type)
        {
            try
            {
                //UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson' 
                //改变文件可见程度
                con.Open();
                sql = "UPDATE files SET file_type=" + file_type + " WHERE file_name=" + file_name + " AND user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.FileInfo_mod> SelTeacherFiles(int student_id, int teacher_id, int uid)
        {
            try
            {
                //查看一个老师所有文件（若没有跟随则查看共享文件）

                con.Open();
                sql = "SELECT * FROM students WHERE student_id=" + student_id + " AND teacher_id=" + teacher_id + " AND state=1";
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                {
                    sql = "SELECT * FROM files WHERE user_id=" + teacher_id;
                }
                else
                    sql = "SELECT * FROM files WHERE user_id=" + teacher_id + " AND file_type=2";

                reader = com.ExecuteReaderEx();
                List<Model.FileInfo_mod> arr = new List<FileInfo_mod>();
                while (reader.Read())
                {
                    arr.Add(new FileInfo_mod
                    {
                        Uid = uid,
                        User_id = (int)reader["user_id"],
                        File_type = (short)reader["file_type"],
                        FileName = reader["file_name"].ToString(),
                        FileKey = reader["file_key"].ToString(),
                        File_length = (int)reader["file_length"]
                    });
                }

                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.FileInfo_mod> SelFileDynamic(int student_id, int uid)
        {
            try
            {
                con.Open();
                //查看文件动态
                sql = "SELECT * FROM deal_list INNER JOIN file_dynamic ON deal_list.file_name = file_dynamic.file_name WHERE file_dynamic.student_id =  " + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReaderEx();
                List<Model.FileInfo_mod> arr = new List<FileInfo_mod>();
                while (reader.Read())
                {
                    arr.Add(new FileInfo_mod
                    {
                        Uid=uid,
                        User_id = (int)reader["user_id"],
                        File_type = (short)reader["file_type"],
                        FileName = reader["file_name"].ToString(),
                        FileKey = reader["file_key"].ToString(),
                        File_length = (int)reader["file_length"]
                    });
                }

                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DelFileDynamic(int student_id)
        {
            try
            {
                con.Open();
                //清除文件动态
                sql = "DELETE FROM file_dynamic WHERE student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.FileInfo_mod> SelAllFiles(int student_id, int uid)
        {
            //查询所有文件
            try
            {
                con.Open();
                sql = "SELECT * FROM students INNER JOIN files ON students.student_id = files.student_id WHERE students.student_id = " + student_id;

                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReaderEx();
                List<Model.FileInfo_mod> arr = new List<FileInfo_mod>();
                while (reader.Read())
                {
                    arr.Add(new FileInfo_mod
                    {
                        Uid = uid,
                        User_id = (int)reader["user_id"],
                        File_type = (short)reader["file_type"],
                        FileName = reader["file_name"].ToString(),
                        FileKey = reader["file_key"].ToString(),
                        File_length = (int)reader["file_length"]
                    });
                }

                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool TeacherAllowFollow(int teacher_id, int student_id)
        {
            //老师通过跟随
            try
            {
                con.Open();
                sql = "UPDATE students SET state=1 Where teacher_id=" + teacher_id + " AND student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool TeacherTurnDownFollow(int teacher_id, int student_id)
        {
            //老师拒绝跟随
            try
            {
                con.Open();
                sql = "UPDATE students SET state=3 WHERE teacher_id=" + teacher_id + " AND student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public Model.Teach.ClassInfo_mod GetClassInfo(int class_id, int uid)
        {
            //获取课时
            try
            {
                con.Open();
                sql = "SELECT * FROM classes WHERE class_id=" + class_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReaderEx();

                if (reader.Read())
                {
                    return new Model.Teach.ClassInfo_mod
                    {
                        Uid = uid,
                        Teacher_id = (int)reader["teacher_id"],
                        Subject = (short)reader["subject"],
                        Start_time = new DateTime((long)reader["start_time"]),
                        End_time = new DateTime((long)reader["end_time"]),
                        Class_id = (int)reader["class_id"],
                        Class_state = (short)reader["class_state"],
                        Student_count = (int)reader["student_count"],
                        Class_introduction = reader["class_introduction"].ToString()
                    };
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public short BookClass(int student_id, int class_id)
        {
            //订阅课程
            try
            {
                con.Open();
                sql = "SELECT * FROM classes WHERE class_id=" + class_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReaderEx();

                if (reader.Read())
                {
                    int teacher_id = (int)reader["teacher_id"];

                    sql = "SELECT * FROM students WHERE teacher_id=" + teacher_id + " AND student_id=" + student_id;
                    com = new MySQLCommand(sql, con);
                    reader = com.ExecuteReaderEx();
                    if (reader.Read())
                    {

                        sql = "SELECT * FROM deal_list WHERE class_id=" + class_id + " AND student_id=" + student_id;
                        com = new MySQLCommand(sql, con);
                        reader = com.ExecuteReaderEx();
                        if (reader.Read())
                        {
                            return Flags.DealFlags.NotDealt;
                        }
                        else
                        {
                            sql = "INSERT INTO deal_list(class_id,student_id,deal_state) VALUES(" + class_id + "," + student_id + ",1)";
                            com = new MySQLCommand(sql, con);
                            com.ExecuteNonQuery();
                            return Flags.DealFlags.Dealt;
                        }
                    }
                    else
                    {
                        return Flags.DealFlags.NotDealt;
                    }
                }
                else
                {
                    return Flags.DealFlags.NotDealt;
                }
            }
            catch
            {
                return Flags.DealFlags.NotDealt;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DelClassDynamic(int student_id)
        {
            //删除课程动态
            return true;
        }

        public short CancelClass(int student_id, int class_id)
        {
            //取消课程

            return 0;
        }

        public short DealAndWriteReview(int class_id, int student_id, string review)
        {
            //交易并写评论
            return 0;
        }

        public List<Model.Teach.ClassDealList_mod> GetReviews(int class_id)
        {
            //查看评论
            return new List<Model.Teach.ClassDealList_mod>();
        }

        public short ChangeClassState(int class_id, int teacher_id, short state)
        {
            //更改课时状态
            return 0;
        }

        public short CreateClass(Model.Teach.ClassInfo_mod cif)
        {
            //创建课程
            //加入动态
            return 0;
        }

        public short DeleteClass(int class_id, int teacher_id)
        {
            //删除课程
            return 0;
        }

        public short EditClassInfo(Model.Teach.ClassInfo_mod cif)
        {
            //修改课程信息
            return 0;
        }

        public short RemoveStudents(int class_id, int student_id, int teacher_id)
        {
            //删出学生
            return 0;
        }

        public List<Model.Teach.ClassInfo_mod> SelClassDynamic(int student_id, int uid)
        {
            //查询课时动态
            //SELECT * FROM deal_list INNER JOIN file_dynamic ON deal_list.file_name = file_dynamic.file_name WHERE file_dynamic.student_id = ?
            return new List<Model.Teach.ClassInfo_mod>();
        }

        public bool AddTeacherFollowRequest(int student_id, int teacher_id)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM students WHERE teacher_id=" + teacher_id + " AND student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    if (Int16.Parse(reader["state"].ToString()) == 1)
                        return false;
                    else
                        return true;
                }
                else
                {
                    sql = "INSERT INTO students(state,teacher_id,student_id) VALUES(2," + teacher_id + "," + student_id + ")";
                    com = new MySQLCommand(sql, con);
                    com.ExecuteNonQuery();
                    return true;
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.Teach.ClassInfo_mod> SelAllBookClass(int student_id, int uid)
        {
            try
            {
                con.Open();
                sql = "";
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                List<Model.Teach.ClassInfo_mod> arrCif = new List<Model.Teach.ClassInfo_mod>();

                while(reader.Read())
                {
                    arrCif.Add(new Model.Teach.ClassInfo_mod
                    {
                        Uid = uid,
                        Teacher_id = (int)reader["teacher_id"],
                        Subject = (short)reader["subject"],
                        Start_time = new DateTime((long)reader["start_time"]),
                        End_time = new DateTime((long)reader["end_time"]),
                        Class_id = (int)reader["class_id"],
                        Class_state = (short)reader["class_state"],
                        Student_count = (int)reader["student_count"],
                        Class_introduction = reader["class_introduction"].ToString()
                    });
                }

                return arrCif;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeleteTeacherFollow(int student_id, int teacher_id)
        {
            try
            {
                con.Open();

                sql = "SELECT * FROM students WHERE teacher_id=" + teacher_id + " AND student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);

                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    sql = "DELETE FROM students WHERE teacher_id=" + teacher_id + " AND student_id=" + student_id;
                    return true;
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.Teach.FollowTeacherInfo_mod> SelAllFollow_Student(int student_id, int uid)
        {
            try
            {
                con.Open();

                List<Model.Teach.FollowTeacherInfo_mod> arr = new List<Model.Teach.FollowTeacherInfo_mod>();

                sql = "SELECT * FROM students WHERE student_id=" + student_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    arr.Add(new Model.Teach.FollowTeacherInfo_mod
                    {
                        State = (short)reader["state"],
                        Student_id = (int)reader["student_id"],
                        Teacher_id = (int)reader["teacher_id"],
                        Uid = uid
                    });
                }

                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Model.Teach.FollowTeacherInfo_mod> SelAllFollow_Teacher(int teacher_id, int uid)
        {
            try
            {
                con.Open();

                List<Model.Teach.FollowTeacherInfo_mod> arr = new List<Model.Teach.FollowTeacherInfo_mod>();

                sql = "SELECT * FROM students WHERE student_id=" + teacher_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    arr.Add(new Model.Teach.FollowTeacherInfo_mod
                    {
                        State = (short)reader["state"],
                        Student_id = (int)reader["student_id"],
                        Teacher_id = (int)reader["teacher_id"],
                        Uid = uid
                    });
                }

                return arr;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }




        //public bool DelOverMessages(int bool)

        //         --插入用户
        //INSERT INTO users(user_name, user_password) VALUES(?, ?);

        //--搜索最大值
        //SELECT LAST_INSERT_ID();

        //--插入用户资料
        //INSERT INTO users_information VALUES(?, ?, ?, ?);

        //--修改全部信息
        //UPDATE user_sex = ?, user_birthday = ?, user_phone = ? FROM users_information WHERE user_id = ?;

        //--修改密码
        //UPDATE user_password FROM users WHERE user_id = ?;

        //--查找密码
        //SELECT user_password FROM users WHERE user_id = ?;

        //--查找全部信息
        //SELECT* FROM users_information WHERE user_id = ?;

        //--查找用户名
        //SELECT user_name FROM users WHERE user_id = ?;
    }
}
