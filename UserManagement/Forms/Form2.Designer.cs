namespace UserManagement
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Logout = new System.Windows.Forms.Button();
            this.label_Name = new System.Windows.Forms.Label();
            this.label_Group = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.comboBox_Group = new System.Windows.Forms.ComboBox();
            this.button_Search = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_birthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Top = new System.Windows.Forms.Button();
            this.label_Message = new System.Windows.Forms.Label();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Logout
            // 
            this.button_Logout.Location = new System.Drawing.Point(713, 12);
            this.button_Logout.Name = "button_Logout";
            this.button_Logout.Size = new System.Drawing.Size(75, 23);
            this.button_Logout.TabIndex = 10;
            this.button_Logout.Text = "Logout";
            this.button_Logout.UseVisualStyleBackColor = true;
            this.button_Logout.Click += new System.EventHandler(this.button_Logout_Click);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(23, 47);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(35, 13);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Name";
            // 
            // label_Group
            // 
            this.label_Group.AutoSize = true;
            this.label_Group.Location = new System.Drawing.Point(23, 81);
            this.label_Group.Name = "label_Group";
            this.label_Group.Size = new System.Drawing.Size(36, 13);
            this.label_Group.TabIndex = 0;
            this.label_Group.Text = "Group";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(87, 44);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(142, 20);
            this.textBox_Name.TabIndex = 1;
            // 
            // comboBox_Group
            // 
            this.comboBox_Group.FormattingEnabled = true;
            this.comboBox_Group.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "select group"});
            this.comboBox_Group.Location = new System.Drawing.Point(87, 81);
            this.comboBox_Group.Name = "comboBox_Group";
            this.comboBox_Group.Size = new System.Drawing.Size(142, 21);
            this.comboBox_Group.TabIndex = 2;
            // 
            // button_Search
            // 
            this.button_Search.Location = new System.Drawing.Point(283, 44);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(75, 23);
            this.button_Search.TabIndex = 3;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(698, 137);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 4;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_name,
            this.col_group,
            this.col_birthday,
            this.col_level,
            this.col_total});
            this.dataGridView1.Location = new System.Drawing.Point(26, 137);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(649, 167);
            this.dataGridView1.TabIndex = 7;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "id";
            this.col_id.Name = "col_id";
            // 
            // col_name
            // 
            this.col_name.HeaderText = "name";
            this.col_name.Name = "col_name";
            // 
            // col_group
            // 
            this.col_group.HeaderText = "group";
            this.col_group.Name = "col_group";
            // 
            // col_birthday
            // 
            this.col_birthday.HeaderText = "birthday";
            this.col_birthday.Name = "col_birthday";
            // 
            // col_level
            // 
            this.col_level.HeaderText = "level";
            this.col_level.Name = "col_level";
            // 
            // col_total
            // 
            this.col_total.HeaderText = "total";
            this.col_total.Name = "col_total";
            // 
            // button_Top
            // 
            this.button_Top.Location = new System.Drawing.Point(614, 12);
            this.button_Top.Name = "button_Top";
            this.button_Top.Size = new System.Drawing.Size(75, 23);
            this.button_Top.TabIndex = 9;
            this.button_Top.Text = "Top";
            this.button_Top.UseVisualStyleBackColor = true;
            this.button_Top.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Message.ForeColor = System.Drawing.Color.Red;
            this.label_Message.Location = new System.Drawing.Point(280, 87);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(0, 15);
            this.label_Message.TabIndex = 0;
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(698, 261);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 11;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(698, 198);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 23);
            this.button_Update.TabIndex = 12;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.button_Top);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.comboBox_Group);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_Group);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.button_Logout);
            this.Name = "Form2";
            this.Text = "ManageUser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Logout;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_Group;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.ComboBox comboBox_Group;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_group;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_birthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_level;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_total;
        private System.Windows.Forms.Button button_Top;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Update;
    }
}