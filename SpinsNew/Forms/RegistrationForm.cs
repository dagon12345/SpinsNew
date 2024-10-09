using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();

        }
        private async Task RegisterEF()
        {
            try
            {
                using(var context = new ApplicationDbContext())
                {
                    var registration = new RegisterModel
                    {
                        Lastname = txt_lastname.Text,
                        Firstname = txt_firstname.Text,
                        Middlename = txt_middlename.Text,
                        Birthdate = Convert.ToDateTime(dt_birth.EditValue),
                        Username = txt_username.Text,
                        Password = txt_password.Text,
                        UserRole = 3,
                        DateRegistered = DateTime.Now,
                        IsActive = false
                    };
                    context.tbl_registered_users.Add(registration);
                    await context.SaveChangesAsync();
                   
                }
                this.Close();
                XtraMessageBox.Show("Your account registered successfully, please wait for the admin to confirm", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task ValidationEF()
        {
            try
            {
                string username = txt_username.Text;
                btn_register.Enabled = false;
                btn_register.Text = "Please wait...";
                using (var context = new ApplicationDbContext())
                {
                    //Search the existing username type inside textbox if it existed.
                    var validation = await context.tbl_registered_users
                        .Where(x => x.Username.StartsWith(username))
                        .FirstOrDefaultAsync();

                    //If the username is detected then tell user already exist else save.
                    if (validation != null)
                    {
                        XtraMessageBox.Show("User already exist please enter another", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_username.Focus();
                    }
                    else
                    {
                        //save once the condition returns false.
                        await RegisterEF();
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_register.Enabled = true;
                btn_register.Text = "Register";
            }
          
        }


        private async void btn_register_Click(object sender, EventArgs e)
        {
            if(txt_lastname.Text == "" || txt_firstname.Text == "" || txt_middlename.Text == "" || dt_birth.Text == "" || txt_username.Text == "" || txt_password.Text == "" || txt_confirmpass.Text == "")
            {
                XtraMessageBox.Show("Please fill all the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(txt_password.Text != txt_confirmpass.Text)
            {
                XtraMessageBox.Show("Password does not match, please enter again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        

            // validation with register method inside.
           await ValidationEF();

    

        }
    }
}
