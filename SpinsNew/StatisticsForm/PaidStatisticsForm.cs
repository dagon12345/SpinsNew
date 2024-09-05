using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsNew.Models;
using SpinsNew.Services;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.StatisticsForm
{
    public partial class PaidStatisticsForm : DevExpress.XtraEditors.XtraForm
    {
        private ApplicationDbContext _dbContext;

        private readonly ITablePayroll _tablePayroll; // get from our interfaces.
        
        //Inject ITablePayroll via contstructor
        public PaidStatisticsForm(ITablePayroll tablePayroll)
        {
            _tablePayroll = tablePayroll;
            InitializeComponent();
            _dbContext = new ApplicationDbContext();
   
           
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

                payrollViewModelBindingSource.DataSource = payrollGroup;
                gridControl1.DataSource = payrollViewModelBindingSource;
                DisableSpinner();

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show($"Error message: {ex.Message}");
            }
            

        }
         //Fill the year combobox
        public async Task Year()
        {
            var selectedYears = await _dbContext.lib_year
                .Where(x => x.Active == 1)
                .AsNoTracking()
                .ToListAsync();
            foreach(var selectedYear in selectedYears)
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

 
            if(cmb_year.SelectedItem is LibraryYear selectedYear)
            {
                 await LoadPeriodsForYear(selectedYear.Year);
            }
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSpinner();
            PayrollGrouping();
        }
    }
}