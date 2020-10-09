using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UserManagement.Utils;

namespace UserManagement
{
    /// <summary>
    /// class thao tác với DB
    /// </summary>
    class UserDao
    {
        // Khai báo các biến
        SqlConnection con;
        SqlCommand command;
        SqlDataReader reader;

        /// <summary>
        /// Kết nối DB
        /// </summary>
        /// <returns>true nếu kết nối thành công và ngược lại</returns>
        public void openConnection()
        {
            //tạo chuỗi kết nối
            string current = System.IO.Directory.GetCurrentDirectory();
            string connetion_string = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + current + "\\UserDB.mdf;Integrated Security=True";

            // tạo kết nối
            con = new SqlConnection(connetion_string);
            try
            {
                // mở kết nối
                con.Open();
            }
            catch (Exception e)
            {
                // thông báo
                Console.WriteLine("Không kết nối được DB!");
                throw e;
            }
        }
        /// <summary>
        /// lấy id của user theo name
        /// </summary>
        /// <param name="name">name lấy từ textbox_Name</param>
        /// <returns>id user</returns>
        public int getUserFromTblUser(string loginName, string password)
        {
            int id = 0;
            try
            {
                // kết nối db
                openConnection();
                // kết nối db thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "SELECT id FROM tbl_user ";
                    sql += "WHERE login_name = @loginName AND password = @password";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // gán giá trị cho tham số trong câu query
                    command.Parameters.Add("loginName", System.Data.SqlDbType.VarChar).Value = loginName;
                    command.Parameters.Add("password", System.Data.SqlDbType.VarChar).Value = password;

                    // thực thi truy vấn
                    reader = command.ExecuteReader();
                    // đọc result set
                    while (reader.Read())
                    {
                        // lấy giá trị id
                        id = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: getUserFromTblUser: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối, đóng reader
                con.Close();
                reader.Close();
            }
            return id;
        }

        /// <summary>
        /// Lấy list user theo name và group 
        /// </summary>
        /// <param name="name">lấy từ textbox_Name</param>
        /// <param name="group">lấy từ comboBox_Group</param>
        /// <returns>list user tương ứng</returns>
        public List<UserEntity> searchUser(string name, int group, int offset, int limit)
        {
            // Khai báo biến
            List<UserEntity> listUser = new List<UserEntity>();
            UserEntity nv = null;
            try
            {
                // kết nối db
                openConnection();
                // kết nối db thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "SELECT u.id, u.full_name, u.group_name, u.birthday, d.level, d.total ";
                    sql += "FROM tbl_user u LEFT JOIN tbl_detail d ON u.id=d.id ";
                    sql += "WHERE u.id != 1 ";
                    // Nếu có nhập textbox_Name
                    if (!Constant.NAME_DEFAULT.Equals(name))
                    {
                        sql += "AND u.full_name LIKE @name ";
                    }
                    // Nếu có chọn group
                    if (group != Constant.GROUP_DEFAULT)
                    {
                        sql += "AND u.group_name = @group ";
                    }
                    // bắt đầu lấy từ vị trí OFFSET và lấy LIMIT bản ghi
                    sql += "ORDER BY u.id OFFSET @offset ROW FETCH NEXT @limit ROWS ONLY";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    if (!Constant.NAME_DEFAULT.Equals(name))
                    {
                        command.Parameters.Add("name", System.Data.SqlDbType.VarChar).Value = "%" + Common.replaceWildcard(name) + "%";
                    }
                    if (group != Constant.GROUP_DEFAULT)
                    {
                        command.Parameters.Add("group", System.Data.SqlDbType.Int).Value = group;
                    }
                    command.Parameters.Add("offset", System.Data.SqlDbType.Int).Value = offset;
                    command.Parameters.Add("limit", System.Data.SqlDbType.Int).Value = limit;

                    // thực thi truy vấn
                    reader = command.ExecuteReader();
                    // đọc result set
                    while (reader.Read())
                    {
                        nv = new UserEntity();
                        // gán giá trị cho các thuộc tính của đối tượng UserEntity
                        nv.Id = reader.GetInt32(0);
                        nv.Fullname = reader.GetString(1);
                        nv.Group = reader.GetInt32(2);
                        nv.Birthday = reader.GetDateTime(3).ToShortDateString();
                        // kiểm tra level có giá trị ko
                        if (reader[4] != DBNull.Value)
                        {
                            nv.Level = reader.GetString(4);
                            nv.Total = reader.GetInt32(5);
                        }
                        // add user vào list
                        listUser.Add(nv);
                    }
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: searchUser: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối, đóng reader
                con.Close();
                reader.Close();
            }
            return listUser;
        }

        /// <summary>
        /// lấy tổng số user
        /// </summary>
        /// <param name="name">lấy từ textbox_Name</param>
        /// <param name="group">lấy từ comboBox_Group</param>
        /// <returns>tổng số user</returns>
        public int getTotalUser(string name, int group)
        {
            int total = 0;
            try
            {
                // kết nối db
                openConnection();
                // kết nối db thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "SELECT COUNT (*) ";
                    sql += "FROM tbl_user ";
                    sql += "WHERE id != 1 ";
                    // Nếu có nhập textbox_Name
                    if (!Constant.NAME_DEFAULT.Equals(name))
                    {
                        sql += "AND full_name LIKE @name ";
                    }
                    // Nếu có chọn group
                    if (group != Constant.GROUP_DEFAULT)
                    {
                        sql += "AND group_name = @group ";
                    }

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    if (!Constant.NAME_DEFAULT.Equals(name))
                    {
                        command.Parameters.Add("name", System.Data.SqlDbType.VarChar).Value = "%" + Common.replaceWildcard(name) + "%";
                    }
                    if (group != Constant.GROUP_DEFAULT)
                    {
                        command.Parameters.Add("group", System.Data.SqlDbType.Int).Value = group;
                    }

                    // thực thi truy vấn
                    reader = command.ExecuteReader();
                    // đọc result set
                    while (reader.Read())
                    {
                        // lấy giá trị id
                        total = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: getTotalUser: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối, đóng reader
                con.Close();
                reader.Close();
            }
            return total;
        }

        /// <summary>
        /// thực hiện insert user
        /// </summary>
        /// <param name="user">đối tượng user</param>
        /// <returns>id user vừa insert</returns>
        public int insertUser(UserEntity user)
        {
            int id = 0;
            try
            {
                // kết nối db
                openConnection();
                // kết nối thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "INSERT INTO tbl_user ";
                    sql += "OUTPUT INSERTED.id ";
                    sql += "VALUES (@loginName, @password, @fullName, @group, @birthday)";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("loginName", System.Data.SqlDbType.VarChar).Value = user.Fullname;
                    command.Parameters.Add("password", System.Data.SqlDbType.VarChar).Value = user.Fullname;
                    command.Parameters.Add("fullName", System.Data.SqlDbType.VarChar).Value = user.Fullname;
                    command.Parameters.Add("group", System.Data.SqlDbType.VarChar).Value = user.Group;
                    command.Parameters.Add("birthday", System.Data.SqlDbType.VarChar).Value = user.Birthday;
                    
                    // thực thi truy vấn trả về giá trị của ô đầu tiên
                    id = (int)command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: insertUser: " + e.Message);
                throw e;
            }
            return id;
        }

        /// <summary>
        /// insert vào bảng tbl_detail
        /// </summary>
        /// <param name="id">id user vừa insert vào tbl_user</param>
        /// <param name="user">đối tượng user</param>
        public void insertLevel(int id, UserEntity user)
        {
            try
            {
                // mở kết nối
                openConnection();
                // kết nối thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "INSERT INTO tbl_detail ";
                    sql += "VALUES (@id, @level, @total)";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("id", System.Data.SqlDbType.VarChar).Value = id;
                    command.Parameters.Add("level", System.Data.SqlDbType.VarChar).Value = user.Level;
                    command.Parameters.Add("total", System.Data.SqlDbType.VarChar).Value = user.Total;
                    
                    // thực thi truy vấn trả về số bản ghi insert thành công
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: insertLevel: " + e.Message);
                throw e;
            }
        }

        /// <summary>
        /// delete ở bảng tbl_user
        /// </summary>
        /// <param name="id">id user muốn xóa</param>
        public void deleteUser(int id)
        {
            try
            {
                openConnection();
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "DELETE FROM tbl_user ";
                    sql += "WHERE id = @id ";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("id", System.Data.SqlDbType.VarChar).Value = id;

                    // thực thi truy vấn trả về số bản ghi thành công
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: deleteUser: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối
                con.Close();
            }
        }
        
        /// <summary>
        /// delete ở bảng tbl_user
        /// </summary>
        /// <param name="id">id user muốn xóa</param>
        public void deleteLevel(int id)
        {
            try
            {
                // mở kết nối
                openConnection();
                // kết nối thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "DELETE FROM tbl_detail ";
                    sql += "WHERE id = @id ";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("id", System.Data.SqlDbType.VarChar).Value = id;

                    // thực thi truy vấn trả về số bản ghi thành công
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: deleteLevel: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối
                con.Close();
            }
        }

        /// <summary>
        /// edit bảng tbl_user
        /// </summary>
        /// <param name="user">đối tượng user</param>
        public void editUser(UserEntity user)
        {
            try
            {
                // mở kết nối
                openConnection();
                // kết nối thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "UPDATE tbl_user ";
                    sql += "SET full_name = @fullName, group_name = @group, birthday = @birthday ";
                    sql += "WHERE id = @id";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("fullName", System.Data.SqlDbType.VarChar).Value = user.Fullname;
                    command.Parameters.Add("group", System.Data.SqlDbType.Int).Value = user.Group;
                    command.Parameters.Add("birthday", System.Data.SqlDbType.VarChar).Value = user.Birthday;
                    command.Parameters.Add("id", System.Data.SqlDbType.Int).Value = user.Id;

                    // thực thi truy vấn trả về số bản ghi update thành công
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: editUser: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối
                con.Close();
            }
        }

        /// <summary>
        /// edit bảng tbl_detail
        /// </summary>
        /// <param name="user">đối tượng user</param>
        public void editLevel(UserEntity user)
        {
            try
            {
                // mở kết nối
                openConnection();
                // kết nối thành công
                if (con != null)
                {
                    // tạo câu truy vấn
                    string sql = "UPDATE tbl_detail ";
                    sql += "SET level = @level, total = @total ";
                    sql += "WHERE id = @id";

                    // khởi tạo đối tượng sqlCommand để truy vấn tới db
                    command = new SqlCommand(sql, con);

                    // add giá trị cho các tham số trong câu truy vấn
                    command.Parameters.Add("level", System.Data.SqlDbType.VarChar).Value = user.Level;
                    command.Parameters.Add("total", System.Data.SqlDbType.Int).Value = user.Total;
                    command.Parameters.Add("id", System.Data.SqlDbType.Int).Value = user.Id;

                    // thực thi truy vấn trả về số bản ghi update thành công
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("UserDao: editLevel: " + e.Message);
                throw e;
            }
            finally
            {
                // đóng kết nối
                con.Close();
            }
        }
    }
}
