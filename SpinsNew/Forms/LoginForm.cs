using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
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
        private void Login()
        {
            try
            {
                //CODE FOR LOGIN FORM

                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Lastname, Firstname, Middlename, Username, Password, UserRole, IsActive FROM tbl_registered_users WHERE Username=@Username AND Password=@Password";
                cmd.Parameters.AddWithValue("@Username", txt_username.EditValue);
                cmd.Parameters.AddWithValue("@Password", txt_password.EditValue);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if(Convert.ToInt32(row["IsActive"]) == 0)
                    {
                        XtraMessageBox.Show("Account not yet activated. Please contact the administrator.", "Request for Activation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Retrieve user details
                    string lastName = row["Lastname"].ToString();
                    string firstName = row["Firstname"].ToString();
                    string username = row["Username"].ToString();
                    string userRole = row["UserRole"].ToString();

                    Dashboard dash = new Dashboard(lastName, firstName, username, userRole);
                    this.Hide();
                    dash.Show();
                    
                  
                }
                else
                {
                    XtraMessageBox.Show("Invalid login details", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Text == "")
            {
                XtraMessageBox.Show("Please fill all the box to login", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Login();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
