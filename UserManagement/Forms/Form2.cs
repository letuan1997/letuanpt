using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UserManagement.Forms;
using UserManagement.Utils;

namespace UserManagement
{
    public partial class Form2 : Form
    {
        // khai báo các biến
        int limitPage = Constant.LIMIT_PAGE;
        int currentPage = 1;
        // Khai báo list label
        List<Label> listLabel = new List<Label>();

        /// <summary>
        /// tạo form 2
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// load vào Form2 (listUser)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            // selected combobox
            comboBox_Group.SelectedItem = "select group";
            // hiển thị list user
            listUser("default", 1);

        }

        /// <summary>
        /// click button search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Search_Click(object sender, EventArgs e)
        {
            // thực hiện search
            listUser("search", 1);
        }

        /// <summary>
        /// click button previous
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Prev_Click(object sender, EventArgs e)
        {
            // tính prevPage
            int prevPage = (currentPage - 1) / limitPage * limitPage - limitPage + 1;
            // gán current page
            currentPage = prevPage;
            // thực hiện paging
            listUser("prev", currentPage);
        }

        /// <summary>
        /// click button next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Next_Click(object sender, EventArgs e)
        {
            // tính nextPage
            int nextPage = (currentPage - 1) / limitPage * limitPage + limitPage + 1;
            // gán current page
            currentPage = nextPage;
            // thực hiện paging
            listUser("next", currentPage);
        }

        /// <summary>
        /// click các page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Page_Click(object sender, EventArgs e)
        {
            // lấy text của label ép sang kiểu int
            currentPage = Common.convertStringToInt((sender as Label).Text);
            // hiển thị user theo page
            listUser("page", currentPage);
        }

        /// <summary>
        /// click button Top
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // selected combobox
            comboBox_Group.SelectedItem = "select group";
            // hiển thị list user
            listUser("default", 1);
        }

        /// <summary>
        /// click button logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Logout_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            // hiển thị form1 (login)
            loginForm.Show();
            // ẩn form2 (listUser)
            this.Hide();
        }

        /// <summary>
        /// hiển thị list user theo từng trường hợp default, search, paging
        /// </summary>
        /// <param name="type">chức năng search, sort, paging</param>
        /// <param name="currentPage">page hiện tại</param>
        private void listUser(string type, int currentPage)
        {
            // Khai báo list
            List<UserEntity> listUser = new List<UserEntity>();
            List<int> listPaging = new List<int>();
            // Khai báo đối tượng Dao
            UserDao userDao = new UserDao();
            // Khai báo các biến default khi load form2
            string name = Constant.NAME_DEFAULT;
            int group = Constant.GROUP_DEFAULT;
            int limit = Constant.LIMIT;
            int offset = Constant.OFFSET_DEFAULT;

            // Ẩn thông báo "Ko có user"
            label_Message.Text = "";
            try
            {
                // trường hợp search
                if ("search".Equals(type))
                {
                    name = textBox_Name.Text;
                    group = Common.convertStringToInt(comboBox_Group.SelectedItem.ToString());
                }
                // trường hợp next
                else if ("next".Equals(type))
                {
                    name = textBox_Name.Text;
                    group = Common.convertStringToInt(comboBox_Group.SelectedItem.ToString());
                    // offset
                    offset = Common.getOffset(currentPage);
                }
                // trường hợp back
                else if ("prev".Equals(type))
                {
                    name = textBox_Name.Text;
                    group = Common.convertStringToInt(comboBox_Group.SelectedItem.ToString());
                    // offset
                    offset = Common.getOffset(currentPage);
                }
                // trường hợp click page
                else if ("page".Equals(type))
                {
                    name = textBox_Name.Text;
                    group = Common.convertStringToInt(comboBox_Group.SelectedItem.ToString());
                    // offset
                    offset = Common.getOffset(currentPage);
                }

                // xóa các dữ liệu cũ của bảng
                dataGridView1.Rows.Clear();

                // xóa paging cũ
                if (listLabel.Count > 0)
                {
                    for (int i = 0; i < listLabel.Count; i++)
                    {
                        Controls.Remove(listLabel[i]);
                    }
                }

                // lấy totalUser để kiểm tra có user ko
                int totalUser = userDao.getTotalUser(name, group);

                // nếu có user
                if (totalUser != 0)
                {
                    // lấy list user
                    listUser = userDao.searchUser(name, group, offset, limit);

                    // thêm data từ list user vào bảng
                    for (int i = 0; i < listUser.Count; i++)
                    {
                        // tạo hàng mới
                        DataGridViewRow newRow = new DataGridViewRow();
                        // biến chỉ số của ô trong hàng
                        int index = 0;
                        // tạo ô
                        newRow.CreateCells(dataGridView1);
                        // gán các giá trị cho từng ô
                        newRow.Cells[index++].Value = listUser[i].Id;
                        newRow.Cells[index++].Value = listUser[i].Fullname;
                        newRow.Cells[index++].Value = listUser[i].Group;
                        newRow.Cells[index++].Value = listUser[i].Birthday;
                        // kiểm tra user có level hay ko
                        if (listUser[i].Total == 0)
                        {
                            newRow.Cells[index++].Value = "";
                            newRow.Cells[index++].Value = "";
                        }
                        else
                        {
                            newRow.Cells[index++].Value = listUser[i].Level;
                            newRow.Cells[index++].Value = listUser[i].Total;
                        }
                        // thêm hàng mới vào bảng
                        dataGridView1.Rows.Add(newRow);
                    }
                    // lấy tổng số page để tạo paging
                    int totalPage = Common.getTotalPage(totalUser);
                    // nếu có > 1 page thì mới tạo
                    if (totalPage > 1)
                    {
                        // lấy listPaging
                        listPaging = Common.getListPaging(currentPage, totalUser, totalPage);

                        // tạo paging
                        createPaging(listPaging, totalPage);

                        // đánh dấu currentPage màu đỏ
                        for (int i = 0; i < listLabel.Count; i++)
                        {
                            if (listLabel[i].Text == currentPage.ToString())
                            {
                                listLabel[i].BackColor = Color.Red;
                            }
                        }
                    }
                }
                // nếu ko có user
                else
                {
                    // thông báo
                    label_Message.Text = "Không tìm thấy user";
                }
            }
            catch (Exception ex)
            {
                // thông báo lỗi
                MessageBox.Show("Form2: listUser: " + ex.Message);
            }
        }

        /// <summary>
        /// tạo label vùng paging
        /// </summary>
        /// <param name="listPaging">list paging</param>
        /// <param name="totalPage">tổng số page</param>
        /// <returns>list các label</returns>
        private List<Label> createPaging(List<int> listPaging, int totalPage)
        {
            // vị trí x của label
            int x = 30;
            // tạo label Previous
            if (listPaging[0] > limitPage)
            {
                // Khởi tạo và gán các thuộc tính cho label
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                label.Location = new Point(x, 320);
                x += 20;
                label.Name = "label_Prev";
                label.Text = "<<";
                label.Click += new EventHandler(label_Prev_Click);

                // add label vào form
                this.Controls.Add(label);
                // add label vào listLabel
                listLabel.Add(label);
            }
            // tạo label số trang
            for (int i = listPaging[0]; i <= listPaging[listPaging.Count - 1]; i++)
            {
                // Khởi tạo và gán các thuộc tính cho label
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                label.Location = new Point(x, 320);
                x += 20;
                label.Name = "label_Page";
                label.Text = "" + i + "";
                label.Click += new EventHandler(label_Page_Click);

                // add label vào form
                this.Controls.Add(label);
                // add label vào listLabel
                listLabel.Add(label);
            }
            // tạo label Next
            if (listPaging[listPaging.Count - 1] < totalPage)
            {
                // Khởi tạo và gán các thuộc tính cho label
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                label.Location = new Point(x, 320);
                label.Name = "label_Next";
                label.Text = ">>";
                label.Click += new EventHandler(label_Next_Click);

                // add label vào form
                this.Controls.Add(label);
                // add label vào listLabel
                listLabel.Add(label);
            }
            return listLabel;
        }

        /// <summary>
        /// đóng form2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đóng ứng dụng
            Application.Exit();
        }

        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, EventArgs e)
        {
            Form3 addForm = new Form3();
            UserEntity user = new UserEntity();
            addForm.userUpdate = user;
            // hiển thị form3
            addForm.Show();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            Form3 addForm = new Form3();
            UserEntity user = new UserEntity();
            // kiểm tra có chọn hàng nào không
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int index = 0;
                user.Id = Common.convertStringToInt(this.dataGridView1.CurrentRow.Cells[index++].Value.ToString());
                user.Fullname = this.dataGridView1.CurrentRow.Cells[index++].Value.ToString();
                user.Group = Common.convertStringToInt(this.dataGridView1.CurrentRow.Cells[index++].Value.ToString());
                user.Birthday = this.dataGridView1.CurrentRow.Cells[index++].Value.ToString();
                string level = this.dataGridView1.CurrentRow.Cells[index++].Value.ToString();
                if (!"".Equals(level))
                {
                    user.Level = level;
                    user.Total = Common.convertStringToInt(this.dataGridView1.CurrentRow.Cells[index++].Value.ToString());
                }
                user.Type = "update";

                addForm.userUpdate = user;
                // hiển thị form3
                addForm.Show();
            }
            else
            {
                MessageBox.Show("Hãy chọn user muốn edit");
            }

        }

        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Delete_Click(object sender, EventArgs e)
        {
            // kiểm tra có chọn hàng nào không
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                // hiển thị alert lựa chọn
                DialogResult reusult = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo);
                // nếu chọn Yes
                if (reusult == DialogResult.Yes)
                {
                    // lấy id của user được chọn
                    int id = Common.convertStringToInt(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    // lấy level user được chọn
                    string level = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();

                    // kiểm tra xóa thành công không
                    if (Common.deleteUser(id, level))
                    {
                        MessageBox.Show("Xóa thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn user muốn xóa");
            }
        }

    }
}
