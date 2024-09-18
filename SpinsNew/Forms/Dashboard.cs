﻿using Microsoft.Extensions.DependencyInjection;
using SpinsNew.Interfaces;
using SpinsNew.StatisticsForm;
using SpinsWinforms.Forms;
using System;
using System.Linq;
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

        EditApplicant editapplicantForm;
        private void MasterlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<MasterList>().Any())
            {
                masterlistForm.Select();
                masterlistForm.BringToFront();
            }
            else
            {
                // Resolve the ITablePayroll service from the Program.ServiceProvider
                var libraryMunicipality = Program.ServiceProvider.GetRequiredService<ILibraryMunicipality>(); //We called the DI lifecycle inside our Program.cs
                var tableMasterlist = Program.ServiceProvider.GetRequiredService<ITableMasterlist>(); //We called the DI lifecycle inside our Program.cs
                var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs

                masterlistForm = new MasterList(_username, _userRole, editapplicantForm, libraryMunicipality, tableMasterlist, tableLog);
                masterlistForm.Show();
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

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private PaidStatisticsForm paidstatisticsForm;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<PaidStatisticsForm>().Any())
            {
                paidstatisticsForm.Select();
                paidstatisticsForm.BringToFront();
            }
            else
            {
                // Resolve the ITablePayroll service from the Program.ServiceProvider
                var tablePayroll = Program.ServiceProvider.GetRequiredService<ITablePayroll>(); //We called the DI lifecycle inside our Program.cs

                // Ensure the service is resolved correctly
                if (tablePayroll != null)
                {
                    paidstatisticsForm = new PaidStatisticsForm(tablePayroll);
                    paidstatisticsForm.Show(); // Or ShowDialog() for modal display
                }
                else
                {
                    MessageBox.Show("Failed to resolve ITablePayroll service.");
                }
            }
        }

    }
}
