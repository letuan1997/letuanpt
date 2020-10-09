

namespace UserManagement
{
    /// <summary>
    /// class khai báo đối tượng user
    /// </summary>
    public class UserEntity
    {
        // Khai báo các thuộc tính
        private int id;
        private string loginName;
        private string password;
        private string fullname;
        private int group;
        private string birthday;
        private string level;
        private int total;
        // chia trường hợp update
        private string type;

        // Khởi tạo getter, setter cho các thuộc tính
        public int Id { get => id; set => id = value; }
        public string LoginName { get => loginName; set => loginName = value; }
        public string Password { get => password; set => password = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public int Group { get => group; set => group = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Level { get => level; set => level = value; }
        public int Total { get => total; set => total = value; }
        public string Type { get => type; set => type = value; }
    }
}
