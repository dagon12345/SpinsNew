using DevExpress.XtraCharts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.StatisticsForm;
using SpinsWinforms.Forms;
using System;
using System.Data;
using System.Linq;
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

        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public Dashboard(string lastName, string firstName, string username, string userRole)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
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
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await DisplayAsyncEF();
        }
        public async Task DisplayAsyncEF()
        {
            try
            {

                // Bind the merged DataTable to the chart
                btnRefreshNew.Enabled = false;
                btnRefreshNew.Text = "Please wait...";

                using (var context = new ApplicationDbContext())
                {
                    // Fetch the data for targets from EF Core
                    var targetData = await context.tbl_payroll_socpen
                        .Include(x => x.LibraryProvince)
                        .Where(x => x.Year == DateTime.Now.Year && x.ClaimTypeID.HasValue && x.PeriodID == 9 && x.PayrollStatusID != 3)
                        .GroupBy(x => x.LibraryProvince.ProvinceName)
                        .Select(g => new
                        {
                            ProvinceName = g.Key,
                            TotalBeneficiaries = g.Count()
                        })
                        .AsNoTracking()
                        .ToListAsync();

                    // Fetch the data for served from EF Core
                    var servedData = await context.tbl_payroll_socpen
                        .Include(x => x.LibraryProvince)
                        .Include(x => x.MasterListModel)
                        .Where(x => x.Year == DateTime.Now.Year && x.PayrollStatusID >= 1 && x.PayrollStatusID <= 3 && (x.PeriodID == 9
                                    || x.MasterListModel.RegTypeId == 2 && x.PeriodID >= 1 && x.PeriodID <= 10 && x.PayrollStatusID == 1))
                        .GroupBy(x => x.LibraryProvince.ProvinceName)
                        .Select(g => new
                        {
                            ProvinceName = g.Key,
                            TotalBeneficiariesServed = g.Count()
                        })
                        .AsNoTracking()
                        .ToListAsync();

                    // Merge the two results into a DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProvinceName", typeof(string));
                    dt.Columns.Add("TotalBeneficiaries", typeof(int));
                    dt.Columns.Add("TotalBeneficiariesServed", typeof(int));

                    // Add target data to the DataTable
                    foreach (var target in targetData)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["ProvinceName"] = target.ProvinceName;
                        newRow["TotalBeneficiaries"] = target.TotalBeneficiaries;
                        newRow["TotalBeneficiariesServed"] = 0; // Default value
                        dt.Rows.Add(newRow);
                    }

                    // Merge served data into the existing rows in the DataTable
                    foreach (var served in servedData)
                    {
                        DataRow existingRow = dt.Select($"ProvinceName = '{served.ProvinceName}'").FirstOrDefault();
                        if (existingRow != null)
                        {
                            existingRow["TotalBeneficiariesServed"] = served.TotalBeneficiariesServed;
                        }
                        else
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["ProvinceName"] = served.ProvinceName;
                            newRow["TotalBeneficiaries"] = 0; // Default value
                            newRow["TotalBeneficiariesServed"] = served.TotalBeneficiariesServed;
                            dt.Rows.Add(newRow);
                        }
                    }
                    // Calculate the total sum and utilization percentage
                    int totalBeneficiaries = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBeneficiaries"));
                    string formattedTotalBeneficiaries = totalBeneficiaries.ToString("#,##0");

                    int totalBeneficiariesServed = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBeneficiariesServed"));
                    string formattedTotalBeneficiariesServed = totalBeneficiariesServed.ToString("#,##0");

                    double utilizationPercentage = totalBeneficiaries > 0 ? (double)totalBeneficiariesServed / totalBeneficiaries * 100 : 0;
                    string formattedUtilizationPercentage = utilizationPercentage.ToString("0.00");

                    // UI Updates (e.g., chart and text boxes)
                    Invoke((MethodInvoker)delegate
                    {
                        // Bind the merged DataTable to the chart
                        btnRefreshNew.Enabled = true;
                        btnRefreshNew.Text = "Refresh";
                        chartControl1.DataSource = dt;
                        // Configure the series for the chart
                        Series seriesTarget = new Series("Target", ViewType.Bar);
                        seriesTarget.ArgumentDataMember = "ProvinceName";
                        seriesTarget.ValueDataMembers.AddRange(new string[] { "TotalBeneficiaries" });
                        seriesTarget.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        seriesTarget.View.Color = System.Drawing.Color.Violet;

                        Series seriesServed = new Series("Served", ViewType.Bar);
                        seriesServed.ArgumentDataMember = "ProvinceName";
                        seriesServed.ValueDataMembers.AddRange(new string[] { "TotalBeneficiariesServed" });
                        seriesServed.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        seriesServed.View.Color = System.Drawing.Color.Gray;

                        chartControl1.Series.Clear();
                        chartControl1.Series.AddRange(new Series[] { seriesTarget, seriesServed });

                        // Customize chart appearance
                        chartControl1.Titles.Clear();
                        chartControl1.Titles.Add(new ChartTitle { Text = $"Accomplishment Year {DateTime.Now.Year} (Physical)" });

                        // Display the calculated values in text boxes
                        textTarget.Text = formattedTotalBeneficiaries;
                        textActual.Text = formattedTotalBeneficiariesServed;
                        utilizationTextBox.Text = $"{formattedUtilizationPercentage}%";
                    });
                }
            }
            catch (Exception)
            {

                //throw;
            }

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

        private async void btnRefreshNew_Click(object sender, EventArgs e)
        {
            await DisplayAsyncEF();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
