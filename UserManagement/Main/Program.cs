﻿using System;
using System.Windows.Forms;

namespace UserManagement
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // chạy form1
            Application.Run(new Form1());
        }
    }
}
