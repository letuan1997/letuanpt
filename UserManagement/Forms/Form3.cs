using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UserManagement.Forms
{
    public partial class Form3 : Form
    {
        // Khai báo list label
        List<Label> listLabel = new List<Label>();
        public UserEntity userUpdate;

        /// <summary>
        /// Tạo form add/edit
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load form add/edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form3_Load(object sender, EventArgs e)
        {
            if (userUpdate.Type != null && "update".Equals(userUpdate.Type))
            {
                label_AddOrUpdate.Text = "Edit User";
                button_Add.Text = "Edit";

                textBox_Name.Text = userUpdate.Fullname;
                comboBox_Group.SelectedItem = userUpdate.Group.ToString().TrimEnd();

                DateTime date = Convert.ToDateTime(userUpdate.Birthday);
                if (date < dateTimePicker_Birthday.MinDate)
                {
                    date = dateTimePicker_Birthday.MinDate;
                }
                else if (date > dateTimePicker_Birthday.MaxDate)
                {
                    date = dateTimePicker_Birthday.MaxDate;
                }
                dateTimePicker_Birthday.Value = date;
                
                if (userUpdate.Level != null)
                {
                    comboBox_Level.SelectedItem = userUpdate.Level.TrimEnd();
                    textBox_Total.Text = userUpdate.Total.ToString();
                }
                else
                {
                    comboBox_Level.SelectedItem = "select level";
                    textBox_Total.Text = "";
                }
            }
            else
            {
                label_AddOrUpdate.Text = "Add User";
                button_Add.Text = "Add";
                // selected group
                comboBox_Group.SelectedItem = "select group";
                // selected level
                comboBox_Level.SelectedItem = "select level";
            }
            
        }

        /// <summary>
        /// click button add/edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, EventArgs e)
        {
            // khởi tạo và gán các thông tin cho user từ form3
            UserEntity user = new UserEntity();
            user.Id = userUpdate.Id;
            user.Fullname = textBox_Name.Text;
            user.Group = Common.convertStringToInt(comboBox_Group.SelectedItem.ToString());
            user.Birthday = dateTimePicker_Birthday.Value.ToShortDateString();
            string level = comboBox_Level.SelectedItem.ToString();
            // kiểm tra có chọn level ko
            if ("select level".Equals(level))
            {
                user.Level = "";
            }
            else
            {
                user.Level = level;
                user.Total = Common.convertStringToInt(textBox_Total.Text);
            }

            // xóa list error cũ
            if (listLabel.Count > 0)
            {
                for (int i = 0; i < listLabel.Count; i++)
                {
                    Controls.Remove(listLabel[i]);
                }
            }
            // kiểm tra thông tin nhập trả về list error
            List<string> listError = Common.validateUser(user);
            // nếu có lỗi
            if (listError.Count != 0)
            {
                // tạo label hiển thị lỗi
                int y = 70;
                for (int i = 0; i < listError.Count; i++)
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    label.ForeColor = Color.Red;
                    label.Location = new Point(80, y);
                    y += 20;
                    label.Text = listError[i];

                    // add label vào form
                    this.Controls.Add(label);
                    // add label vào listLabel
                    listLabel.Add(label);
                }
            }
            // không có lỗi
            else
            {
                // trường hợp update
                if ("update".Equals(userUpdate.Type))
                {
                    // hiển thị alert lựa chọn
                    DialogResult reusult = MessageBox.Show("Do you want to edit?", "Edit", MessageBoxButtons.YesNo);
                    // nếu chọn Yes
                    if (reusult == DialogResult.Yes)
                    {
                        // thực hiện insert user thành công không
                        if (Common.updateUser(user, userUpdate.Level))
                        {
                            MessageBox.Show("edit thành công");
                        }
                        else
                        {
                            MessageBox.Show("edit thất bại");
                        }
                    }
                }
                // trường hợp add
                else
                {
                    // hiển thị alert lựa chọn
                    DialogResult reusult = MessageBox.Show("Do you want to add?", "Add", MessageBoxButtons.YesNo);
                    // nếu chọn Yes
                    if (reusult == DialogResult.Yes)
                    {
                        // thực hiện insert user thành công không
                        if (Common.insertUser(user))
                        {
                            MessageBox.Show("insert thành công");
                        }
                        else
                        {
                            MessageBox.Show("insert thất bại");
                        }
                    }
                }
            }
                
        }

        /// <summary>
        /// click button back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Back_Click(object sender, EventArgs e)
        {
            // ẩn form3
            this.Hide();
        }

        /// <summary>
        /// đóng form3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ẩn form3
            this.Hide();
        }
    }
}
