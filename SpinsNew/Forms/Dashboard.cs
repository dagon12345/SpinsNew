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
    public partial class Dashboard : Form
    {
        public string _lastName;
        public string _firstName;
        public string _username;
        public string _userRole;

        public Dashboard(string lastName, string firstName, string username, string userRole)
        {
            InitializeComponent();

            _lastName = lastName;
            _firstName = firstName;
            _username = username;
            _userRole = userRole;


            // Display the user data on the Dashboard form
            lblName.Text = $"Welcome, {_firstName} {_lastName}";
            lblUsername.Text = $"Username: {_username}";
            lblUserrole.Text = $"Role: {_userRole}";
            // Other details can also be displayed as needed


        }
        //public void ToggleControls(object o, EventArgs e)
        //{
        //    foreach(Control c in this.Controls)
        //    {
        //        c.Enabled = !c.Enabled;
        //    }
        //}

        MasterList masterlistForm;
        private void MasterlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<MasterList>().Any())
            {
                masterlistForm.Select();
                masterlistForm.BringToFront();
            }
            else
            {
                masterlistForm = new MasterList(_username, _userRole);
                masterlistForm.ShowDialog();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();


            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    if (MessageBox.Show("Are you sure you want to exit?",
            //                        "Confirm",
            //                        MessageBoxButtons.YesNo,
            //                        MessageBoxIcon.Information) == DialogResult.Yes)
            //    {
            //        // Close the current form
            //        this.Hide(); // or this.Close(); depending on your use case

            //        // Check if there are any other open forms
            //        if (Application.OpenForms.Count == 1) // Assuming the last form is the LoginForm
            //        {
            //            LoginForm login = new LoginForm();
            //            login.Show();
            //        }
            //    }
            //    else
            //    {
            //        e.Cancel = true; // Prevent closing if the user changes their mind
            //    }
            //}
        }
    }
}
