using System;
using System.Windows.Forms;

namespace UserManagement
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Khởi tạo Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// click button login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Login_Click(object sender, EventArgs e)
        {
            // lấy loginName và password từ màn hình
            string loginName = textBox_UserName.Text;
            string password = textBox_Password.Text;

            // kiểm tra login lấy về thông báo lỗi
            string error = Common.checkLogin(loginName, password);
            // trường hợp đúng loginName và password
            if (error == null)
            {
                // ẩn form1
                this.Hide();
                Form2 listForm = new Form2();
                // hiển thị form2
                listForm.Show();
            }
            // nếu có lỗi
            else
            {
                // hiển thị lỗi
                label_Error.Text = error;
            }
        }

        /// <summary>
        /// thay đổi kí tự ô password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Password_TextChanged(object sender, EventArgs e)
        {
            // mã hóa password thành dấu *
            textBox_Password.PasswordChar = '*';
        }

        /// <summary>
        /// click button Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Exit_Click(object sender, EventArgs e)
        {
            // đóng ứng dụng
            Application.Exit();
        }

        /// <summary>
        /// đóng form1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đóng ứng dụng
            Application.Exit();
        }
    }
}
