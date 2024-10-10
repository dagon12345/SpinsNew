using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class RegistrationForm : Form
    {
        private ITableRegisterUser _tableRegisterUser;
        public RegistrationForm(ITableRegisterUser tableRegisterUser)
        {
            InitializeComponent();
            _tableRegisterUser = tableRegisterUser;

        }
        private async Task RegisterEF()
        {
            try
            {
                var selectedRole = (LibraryRole)cmbUserRole.SelectedItem;
                using (var context = new ApplicationDbContext())
                {
                    var registration = new RegisterModel
                    {
                        Lastname = txt_lastname.Text.ToUpper(),
                        Firstname = txt_firstname.Text.ToUpper(),
                        Middlename = txt_middlename.Text.ToUpper(),
                        Birthdate = Convert.ToDateTime(dt_birth.EditValue),
                        Username = txt_username.Text.ToLower(),
                        Password = txt_password.Text,
                        UserRole = selectedRole.UserRoleId,
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
                        .Where(x => x.Username.StartsWith(username.ToLower()))
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
        private async Task UserRole()
        {
            using (var context = new ApplicationDbContext())
            {
                var userRole = await Task.Run(() => context.LibraryRoles
                    .AsNoTracking()
                    .ToListAsync());
                   

                cmbUserRole.Properties.Items.Clear();
                foreach (var userRoles in userRole)
                {
                    cmbUserRole.Properties.Items.Add(new LibraryRole
                    {
                        UserRoleId = userRoles.UserRoleId,
                        Role = userRoles.Role
                    });
                }
            }
        }

        private async void btn_register_Click(object sender, EventArgs e)
        {
            if (txt_lastname.Text == "" || txt_firstname.Text == "" || txt_middlename.Text == "" || dt_birth.Text == "" || txt_username.Text == "" || txt_password.Text == "" || txt_confirmpass.Text == "")
            {
                XtraMessageBox.Show("Please fill all the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_password.Text != txt_confirmpass.Text)
            {
                XtraMessageBox.Show("Password does not match, please enter again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // validation with register method inside.
            await ValidationEF();

        }

        private async void RegistrationForm_Load(object sender, EventArgs e)
        {
            await UserRole();
        }
    }
}
