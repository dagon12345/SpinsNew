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
    public partial class RegistrationForm : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public RegistrationForm()
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
        }
        private void Register()
        {
            try
            {
                if (XtraMessageBox.Show("Are you sure you want to continue?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                
                    // Insert GIS information into database
                    MySqlCommand insertCmd = con.CreateCommand();
                    insertCmd.CommandType = CommandType.Text;
                    insertCmd.CommandText = "INSERT INTO tbl_registered_users (Lastname, Firstname, Middlename, Birthdate, Username, Password, UserRole, DateRegistered, IsActive)" +
                        " VALUES (@Lastname, @Firstname, @Middlename, @Birthdate, @Username, @Password, @UserRole, @DateRegistered, @IsActive)";
                    insertCmd.Parameters.AddWithValue("@Lastname", txt_lastname.EditValue);
                    insertCmd.Parameters.AddWithValue("@Firstname", txt_firstname.EditValue);
                    insertCmd.Parameters.AddWithValue("@Middlename", txt_middlename.Text);
                    insertCmd.Parameters.AddWithValue("@Birthdate", dt_birth.EditValue);
                    insertCmd.Parameters.AddWithValue("@Username", txt_username.EditValue);
                    insertCmd.Parameters.AddWithValue("@Password", txt_password.EditValue);
                    insertCmd.Parameters.AddWithValue("@UserRole", 3);
                    insertCmd.Parameters.AddWithValue("@DateRegistered", DateTime.Now);
                    insertCmd.Parameters.AddWithValue("@IsActive", false);
                    insertCmd.ExecuteNonQuery();
               

                    XtraMessageBox.Show("Your account registered successfully, please wait for the admin to confirm", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         
        }
        private void Validation()
        {
          
            try
            {
                con.Open();
                int i = 0;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Username FROM tbl_registered_users WHERE Username=@Username";
                cmd.Parameters.AddWithValue("@Username", txt_username.EditValue);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)// scan if there is existing username
                {
                    //Register if there is no existing username
                    Register();
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("User already exist please enter another", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_username.Focus();
                }
                con.Close();


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void RegistrationForm_Load(object sender, EventArgs e)
        {
           
        }

        private void btn_register_Click(object sender, EventArgs e)
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
         
           Validation();
           
        }
    }
}
