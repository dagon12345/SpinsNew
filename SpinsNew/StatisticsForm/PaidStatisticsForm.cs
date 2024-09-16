using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.StatisticsForm
{
    public partial class PaidStatisticsForm : DevExpress.XtraEditors.XtraForm
    {
        private ApplicationDbContext _dbContext;
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        private readonly ITablePayroll _tablePayroll; // get from our interfaces.

        //Inject ITablePayroll via contstructor
        public PaidStatisticsForm(ITablePayroll tablePayroll)
        {
            _tablePayroll = tablePayroll;
            InitializeComponent();
            _dbContext = new ApplicationDbContext();
            con = new MySqlConnection(cs.dbcon);


        }
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //await PayrollGrouping();
            await Year();


        }
        //Show the spinner
        private void EnableSpinner()
        {

            panel_spinner.Visible = true;
            groupControl1.Enabled = false;


        }
        //Hide the spinner
        private void DisableSpinner()
        {
            panel_spinner.Visible = false;
            groupControl1.Enabled = true;


        }

        private async void PayrollGrouping()
        {

            try
            {


                var selectedYearItem = (dynamic)cmb_year.SelectedItem;
                int year = selectedYearItem.Year;

                var selectedPeriodItem = (dynamic)cmb_period.SelectedItem;
                int periodId = selectedPeriodItem.PeriodID;


                // We got this logic inside our Services/TablePayroll
                var payrollGroup = await _tablePayroll.GetPayrollAsync(year, periodId);

                //payrollViewModelBindingSource.DataSource = payrollGroup;
                //gridControl1.DataSource = payrollViewModelBindingSource;
                DisableSpinner();



                // Assuming gridView is your GridView instance associated with gridPayroll

            }
            catch (Exception)
            {

            }
        }


        private  void PayrollGroupingADO()
        {

            try
            {


                var selectedYearItem = (dynamic)cmb_year.SelectedItem;
                int year = selectedYearItem.Year;

                var selectedPeriodItem = (dynamic)cmb_period.SelectedItem;
                int periodId = selectedPeriodItem.PeriodID;


               // int currentYear = DateTime.Now.Year;


                con.Open();

                // Query for Target
                MySqlCommand targetCmd = con.CreateCommand();
                targetCmd.CommandType = CommandType.Text;
                targetCmd.CommandText = @"
                            SELECT 
                                lp.ProvinceName,
                                lcm.CityMunName,
                                lcm.District,        
                                COUNT(*) AS TotalTarget
                            FROM 
                                tbl_payroll_socpen t 
                            LEFT JOIN
                                lib_province lp ON t.PSGCProvince = lp.PSGCProvince
                            LEFT JOIN
                                lib_city_municipality lcm ON t.PSGCCityMun = lcm.PSGCCityMun
                            WHERE 
                                t.YEAR = @Year
                                AND t.PeriodID = @PeriodID
                            GROUP BY 
                                t.PSGCProvince,
                                t.PSGCCityMun,
                                lcm.District";
                targetCmd.Parameters.AddWithValue("@Year", year);
                targetCmd.Parameters.AddWithValue("@PeriodID", periodId);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(targetCmd);
                da.Fill(dt);
                gridControl1.DataSource = dt;
                con.Close();
                DisableSpinner();

                //Invoke((MethodInvoker)delegate {
                //    // Bind the merged DataTable to the chart
                //    btnRefreshNew.Enabled = true;
                //    btnRefreshNew.Text = "Refresh";
                //    //btnRefreshNew.Image = null; // Hide the icon by setting the Image property to null
                //    chartControl2.DataSource = dt;

                //    // Configure the series for the chart
                //    Series seriesTarget = new Series("Target", ViewType.Bar);
                //    seriesTarget.ArgumentDataMember = "ProvinceName";
                //    seriesTarget.ValueDataMembers.AddRange(new string[] { "TotalBeneficiaries" });
                //    seriesTarget.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //    seriesTarget.View.Color = System.Drawing.Color.Violet; // Change the color here

                //    Series seriesServed = new Series("Served", ViewType.Bar);
                //    seriesServed.ArgumentDataMember = "ProvinceName";
                //    seriesServed.ValueDataMembers.AddRange(new string[] { "TotalBeneficiariesServed" });
                //    seriesServed.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //    seriesServed.View.Color = System.Drawing.Color.Gray; // Change the color here

                //    chartControl2.Series.Clear();
                //    chartControl2.Series.AddRange(new Series[] { seriesTarget, seriesServed });

                //    // Customize chart appearance
                //    chartControl2.Titles.Clear();
                //    chartControl2.Titles.Add(new ChartTitle { Text = $"Accomplishment Year {currentYear} (Physical)" });
                //    //  chartControl2.BackColor = System.Drawing.Color.Black;
                //    //  chartControl2.ForeColor = System.Drawing.Color.White;

                //    // Display the calculated values in text boxes
                //    textTarget.Text = formattedTotalBeneficiaries;
                //    textActual.Text = formattedTotalBeneficiariesServed;
                //    utilizationTextBox.Text = $"{formattedUtilizationPercentage}%";
                //});


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }


        //Fill the year combobox
        public async Task Year()
        {
            var selectedYears = await _dbContext.lib_year
                .Where(x => x.Active == 1)
                .AsNoTracking()
                .ToListAsync();
            foreach (var selectedYear in selectedYears)
            {

                /*Inside the LibraryYear we use override to get the string of 
                 LibraryYear*/
                cmb_year.Properties.Items.Add(new LibraryYear
                {
                    Id = selectedYear.Id,
                    Year = selectedYear.Year
                });
            }
            // Add the event handler for the SelectedIndexChanged event
            cmb_year.SelectedIndexChanged -= cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
            cmb_year.SelectedIndexChanged += cmb_year_SelectedIndexChanged;
        }
        //When year was clicked then show the periods
        private async Task LoadPeriodsForYear(int year)
        {
            var selectedPeriods = await _dbContext.lib_period
                .Where(x => x.YearsUsed.Contains(year.ToString()))
                .AsNoTracking()
                .ToListAsync();

            cmb_period.Properties.Items.Clear();// Clear the previous year displayed to avoid duplication.
            foreach (var selectedPeriod in selectedPeriods)
            {
                /*Inside the LibraryPeriod we use override to get the string of 
                 LibraryPeriod*/
                cmb_period.Properties.Items.Add(new LibraryPeriod
                {
                    Id = selectedPeriod.Id,
                    PeriodID = selectedPeriod.PeriodID,
                    Period = selectedPeriod.Period,
                    Abbreviation = selectedPeriod.Abbreviation,
                    Months = selectedPeriod.Months,
                });
            }

        }


        private void PaidStatisticsForm_Load(object sender, EventArgs e)
        {

        }

        private async void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmb_year.SelectedItem is LibraryYear selectedYear)
            {
                await LoadPeriodsForYear(selectedYear.Year);
            }
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSpinner();
            PayrollGroupingADO();
        }
    }
}