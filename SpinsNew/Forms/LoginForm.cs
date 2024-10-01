﻿using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class LoginForm : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public LoginForm()
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);

        }


        private void LoginForm_Load(object sender, EventArgs e)
        {

        }


        RegistrationForm registrationForm;
        private void hyper_register_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RegistrationForm>().Any())
            {
                registrationForm.Select();
                registrationForm.BringToFront();
            }
            else
            {
                registrationForm = new RegistrationForm();
                registrationForm.Show();
            }
        }
        //Login using ef core
        private async Task LoginEF()
        {
           
            using (var context = new ApplicationDbContext())
            {
                var loginForm = await context.tbl_registered_users.Where(l => l.Username.StartsWith(txt_username.Text.ToLower())
                && l.Password.StartsWith(txt_password.Text))
                    .FirstOrDefaultAsync();

                if (loginForm != null)
                {
                    if (loginForm.IsActive != true)
                    {
                        XtraMessageBox.Show("Account not yet activated. Please contact the administrator.", "Request for activation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string lastName = loginForm.Lastname;
                    string firstName = loginForm.Firstname;
                    string userName = loginForm.Username;
                    string userRole = Convert.ToString(loginForm.UserRole);

                    Dashboard dash = new Dashboard(lastName, firstName, userName, userRole);
                    this.Hide();
                    dash.Show();
                }
                else
                {
                    btn_login.Text = "Login";
                    btn_login.Enabled = true;
                    XtraMessageBox.Show("Username or Password is incorrect please try again.", "Invalid Username/Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }

        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Text == "")
            {
                XtraMessageBox.Show("Please fill all the box to login", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btn_login.Text = "Logging in...";
            btn_login.Enabled = false;
            await LoginEF();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void crystalLink_Click(object sender, EventArgs e)
        {
            string url = "https://www.dropbox.com/scl/fi/pkc8mobyyll9296tup4je/Cyrstal-report-2019.EXE?rlkey=72gy11cz0jey6a4eafgufspeh&st=nt54574s&dl=1";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
