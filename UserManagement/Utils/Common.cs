using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UserManagement.Utils;

namespace UserManagement
{
    /// <summary>
    /// class chứa các hàm dùng chung
    /// </summary>
    class Common
    {
        /// <summary>
        /// replace các kí tự wildcard
        /// </summary>
        /// <param name="name">lấy từ textbox_Name</param>
        /// <returns>name sau khi replace</returns>
        public static string replaceWildcard(string name)
        {
            return name.Replace("%", "[%]").Replace("_", "[_]");
        }

        /// <summary>
        /// lấy list paging
        /// </summary>
        /// <param name="currentPage">page hiện tại</param>
        /// <param name="totalUser">tổng user</param>
        /// <returns>list paging</returns>
        public static List<int> getListPaging(int currentPage, int totalUser, int totalPage)
        {
            // Khai báo các biến
            List<int> listPaging = new List<int>();
            int limitPage = Constant.LIMIT_PAGE;

            // tính startPage, endPage
            int startPage = (currentPage - 1) / limitPage * limitPage + 1;
            int endPage = startPage + limitPage - 1;
            // nếu endPage > totalPage thì gán endPage = totalPage
            if (endPage > totalPage)
            {
                endPage = totalPage;
            }
            // add page vào list
            for (int i = startPage; i <= endPage; i++)
            {
                listPaging.Add(i);
            }
            return listPaging;
        }

        /// <summary>
        /// tính offset: vị trí bản ghi bắt đầu lấy
        /// </summary>
        /// <param name="limit">số bản ghi / 1 page</param>
        /// <param name="currentPage">số page hiện tại</param>
        /// <returns>offset</returns>
        public static int getOffset(int currentPage)
        {
            int limit = Constant.LIMIT;
            int offset = currentPage * limit - limit;
            return offset;
        }

        /// <summary>
        /// tính tổng số page
        /// </summary>
        /// <param name="totalUser">tổng số user</param>
        /// <param name="limit">số bản ghi/ 1 page</param>
        /// <returns>tổng số page</returns>
        public static int getTotalPage(int totalUser)
        {
            int limit = Constant.LIMIT;
            // tổng số page = tổng user / số user 1 page
            int totalPage = totalUser / limit;
            // nếu chia dư thì thêm 1 page nữa
            if (totalUser % limit != 0)
            {
                totalPage++;
            }
            return totalPage;
        }

        /// <summary>
        /// kiểm tra login
        /// </summary>
        /// <param name="loginName">lấy từ textbox login_name</param>
        /// <param name="password">lấy từ textbox password</param>
        /// <returns>câu thông báo lỗi</returns>
        public static string checkLogin(string loginName, string password)
        {
            // Khai báo biến
            UserDao nvDao = new UserDao();
            string error = null;
            try
            {
                // lấy id theo loginName và password từ màn hình
                int id = nvDao.getUserFromTblUser(loginName, password);
                // Nếu chưa nhập login name
                if (Constant.EMPTY_STRING.Equals(loginName))
                {
                    error = "Nhập login name";
                }
                // chưa nhập password
                else if (Constant.EMPTY_STRING.Equals(password))
                {
                    error = "Nhập password";
                }
                // trường hợp sai loginName hoặc password
                else if (id != 1)
                {
                    error = "Sai login name hoặc password";
                }
            }
            catch (Exception e)
            {
                // thông báo lỗi
                Console.WriteLine("Common: checkLogin: " + e.Message);
                error = "Lỗi kết nối DB";
            }
            return error;
        }

        /// <summary>
        /// Chuyển string sang int
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>số int</returns>
        public static int convertStringToInt(string str)
        {
            int num;
            try
            {
                // ép kiểu sang int
                num = Convert.ToInt32(str);
            }
            catch (Exception)
            {
                num = 0;
            }
            return num;
        }

        /// <summary>
        /// validate thông tin user nhập vào
        /// </summary>
        /// <param name="user">đối tượng user</param>
        /// <returns>list lỗi</returns>
        public static List<string> validateUser(UserEntity user)
        {
            // Khai báo list lỗi
            List<string> listError = new List<string>();

            // chưa nhập full name
            if (Constant.NAME_DEFAULT.Equals(user.Fullname))
            {
                listError.Add("Nhập full name");
            }
            // nhập full name sai format
            else if (!Regex.IsMatch(user.Fullname, @"^[a-zA-Z0-9 ]*$"))
            {
                listError.Add("Full name sai format");
            }
            // chưa chọn group
            if (user.Group == Constant.GROUP_DEFAULT)
            {
                listError.Add("Chọn group");
            }

            // nếu có chọn level
            if (!"".Equals(user.Level))
            {
                // chưa nhập total
                if (user.Total == 0)
                {
                    listError.Add("Nhập lại total");
                }
            }
            return listError;
        }

        /// <summary>
        /// insert user mới
        /// </summary>
        /// <param name="user">đối tượng user</param>
        /// <returns>true nếu insert thành công và ngược lại</returns>
        public static bool insertUser(UserEntity user)
        {
            // khai báo biến 
            bool check = false;
            UserDao userDao = new UserDao();
            try
            {
                // insert data bảng tbl_user và lấy id user vừa insert
                int id = userDao.insertUser(user);
                // nếu insert thành công tbl_user và có data level
                if (id != 0 && !"".Equals(user.Level))
                {
                    // insert data bảng tbl_detail
                    userDao.insertLevel(id, user);
                }
                check = true;
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("Common: insertUser: " + e.Message);
            }
            return check;
        }

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="user">đối tượng user</param>
        /// <param name="oldLevel">level user trước khi edit</param>
        /// <returns></returns>
        public static bool updateUser(UserEntity user, string oldLevel)
        {
            // khai báo biến 
            bool check = false;
            UserDao userDao = new UserDao();
            try
            {
                // update data bảng tbl_user và lấy id user vừa insert
                userDao.editUser(user);
                // user ko có level -> thêm level
                if (oldLevel == null && !"".Equals(user.Level))
                {
                    // insert data bảng tbl_detail
                    userDao.insertLevel(user.Id, user);
                }
                // user có level -> xóa level
                else if (oldLevel != null && "".Equals(user.Level))
                {
                    // xóa data bảng tbl_detail
                    userDao.deleteLevel(user.Id);
                }
                // update level
                else
                {
                    // update data bảng tbl_detail
                    userDao.editLevel(user);
                }
                check = true;
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("Common: updateUser: " + e.Message);
            }
            return check;
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id">id của user</param>
        /// <param name="level">level của user</param>
        /// <returns>true nếu xáo thành công</returns>
        public static bool deleteUser(int id, string level)
        {
            // khai báo biến
            bool check = false;
            UserDao userDao = new UserDao();
            try
            {
                // xóa data bảng tbl_user
                userDao.deleteUser(id);
                // nếu có level thì xóa data ở tbl_detail
                if (!"".Equals(level))
                {
                    userDao.deleteLevel(id);
                }
                check = true;
            }
            catch (Exception e)
            {
                // báo lỗi
                Console.WriteLine("Common: deleteUser: " + e.Message);
            }
            return check;
        }

    }
}
