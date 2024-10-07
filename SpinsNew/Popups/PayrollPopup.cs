using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Libraries;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Popups
{
    public partial class PayrollPopup : Form
    {
        private PayrollModel _payroll;
        public string _username;
        private MasterList masterlistForm;
        public PayrollPopup(MasterList masterListForm, string username) //Instantiate to make the gridcontrol from form masterlist work into the payrollpopup form.
        {
            InitializeComponent();
            this.masterlistForm = masterListForm; //Instantiate to make the gridcontrol from form masterlist work into the payrollpopup form.
            _username = username;
        }

        private async void PayrollPopup_Load(object sender, EventArgs e)
        {
            await YearEf();
            await PayrollTypeMethodEf();
            await PayrollTagEf();
            await PaymentModeEf();
        }

        //Refactored code for year to display.
        public async Task YearEf()
        {

            using (var context = new ApplicationDbContext())
            {
                int yearConvertion = Convert.ToInt32(txt_monthlystipend.Text);
                var year = await context.lib_year
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                cmb_year.Properties.Items.Clear();
                foreach (var years in year)
                {
                    cmb_year.Properties.Items.Add(new LibraryYear
                    {
                        Id = years.Id,
                        Year = years.Year,
                        MonthlyStipend = years.MonthlyStipend
                    });

                }
                // Add the event handler for the SelectedIndexChanged event
                cmb_year.SelectedIndexChanged -= cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
                cmb_year.SelectedIndexChanged += cmb_year_SelectedIndexChanged;
            }
        }

        private async Task PayrollTypeMethodEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var payrollTypes = await context.lib_payroll_type
                    .AsNoTracking()
                    .ToListAsync();

                cmb_type.Properties.Items.Clear();
                foreach (var payrollType in payrollTypes)
                {
                    cmb_type.Properties.Items.Add(new LibraryPayrollType
                    {
                        PayrollTypeID = payrollType.PayrollTypeID,
                        PayrollType = payrollType.PayrollType
                    });
                }
            }
        }
        private async Task PayrollTagEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var payrollTags = await context.lib_payroll_tag
                    .AsNoTracking()
                    .ToListAsync();

                cmb_tag.Properties.Items.Clear();
                foreach (var payrollTag in payrollTags)
                {
                    cmb_tag.Properties.Items.Add(new LibraryPayrollTag
                    {
                        PayrollTagID = payrollTag.PayrollTagID,
                        PayrollTag = payrollTag.PayrollTag
                    });
                }
            }
        }

        //When year was selected then fill the period that contains the year selected.
        private async Task LoadPeriodsForYearEf(int year)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Fetch the data first, and then filter it in memory
                    var periods = await context.lib_period
                        .AsNoTracking()
                        .ToListAsync();

                    // Perform client-side filtering for the specified year
                    var filteredPeriods = periods
                        .Where(p => p.YearsUsed.Replace(" ", "").Split(',').Contains(year.ToString()))
                        .Select(p => new
                        {
                            p.PeriodID,
                            p.Period,
                            p.Abbreviation,
                            p.Months
                        })
                        .ToList();

                    // Clear existing items in the ComboBoxEdit
                    cmb_period.Properties.Items.Clear();

                    // Populate the ComboBoxEdit with the filtered periods
                    foreach (var period in filteredPeriods)
                    {
                        cmb_period.Properties.Items.Add(new LibraryPeriod
                        {
                            PeriodID = period.PeriodID,
                            Period = period.Period,
                            Abbreviation = period.Abbreviation,
                            Months = period.Months
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_year.SelectedItem is LibraryYear selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");

                //When particular year was selected fill the monthly stipend value.
                txt_monthlystipend.Text = selectedYear.MonthlyStipend.ToString();

                await LoadPeriodsForYearEf(selectedYear.Year);
            }
        }

        private async Task PaymentModeEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var paymentModes = await context.lib_payment_mode
                    .AsNoTracking()
                    .ToListAsync();

                cmb_payment.Properties.Items.Clear();
                foreach (var paymentMode in paymentModes)
                {
                    cmb_payment.Properties.Items.Add(new LibraryPaymentMode
                    {
                        PaymentModeID = paymentMode.PaymentModeID,
                        PaymentMode = paymentMode.PaymentMode
                    });
                }
            }
        }

        private async void btn_create_Click(object sender, EventArgs e)
        {
            if (masterlistForm != null)
            {
                if (cmb_year.EditValue == null || cmb_period.EditValue == null || cmb_type.EditValue == null || cmb_tag.EditValue == null || cmb_payment.EditValue == null)
                {
                    XtraMessageBox.Show("Please fill all the dropboxes before proceeding.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (XtraMessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Insertion of payroll code below.

                    try
                    {
                        GridView gridView = masterlistForm.gridControl1.MainView as GridView;
                        var selectedYear = (LibraryYear)cmb_year.SelectedItem;
                        var selectedPeriod = (LibraryPeriod)cmb_period.SelectedItem;
                        var selectedType = (LibraryPayrollType)cmb_type.SelectedItem;
                        var selectedTag = (LibraryPayrollTag)cmb_tag.SelectedItem;
                        var selectedMode = (LibraryPaymentMode)cmb_payment.SelectedItem;
                        double amount = 0;
                        if (!string.IsNullOrEmpty(txt_amount.Text))
                        {
                            amount = Convert.ToDouble(txt_amount.Text);
                        }

                        using (var context = new ApplicationDbContext())
                        {


                            for (int i = 0; i < gridView.RowCount; i++)
                            {
                                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(i);
                                if (row != null)
                                {


                                    int id = row.Id;

                                    int region = row.PSGCRegion;
                                    int province = row.PSGCProvince;
                                    int municipality = row.PSGCCityMun;
                                    int barangay = row.PSGCBrgy;

                                    string address = row.Address;

                                    _payroll = new PayrollModel
                                    {

                                        MasterListID = id,

                                        PSGCRegion = region,
                                        PSGCProvince = province,
                                        PSGCCityMun = municipality,
                                        PSGCBrgy = barangay,

                                        Address = address,
                                        Amount = amount,
                                        Year = selectedYear.Year,
                                        PeriodID = selectedPeriod.PeriodID,
                                        PayrollStatusID = 2,
                                        ClaimTypeID = null,
                                        PayrollTypeID = selectedType.PayrollTypeID,
                                        PayrollTagID = selectedTag.PayrollTagID,
                                        PaymentModeID = selectedMode.PaymentModeID,
                                        DateTimeEntry = DateTime.Now,
                                        EntryBy = _username
                                    };

                                    context.tbl_payroll_socpen.Add(_payroll);


                                }

                            }
                            panel_spinner.Visible = true;
                            btn_create.Enabled = false;
                            await context.SaveChangesAsync();
                        }

                        panel_spinner.Visible = false;
                        XtraMessageBox.Show("Payroll created successfully. Please proceed to the payroll form to generate reports", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();


                    }
                    catch (Exception ex)
                    {

                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("MasterList form is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PeriodEf(LibraryPeriod selectedLibraryPeriod)
        {
            using (var context = new ApplicationDbContext())
            {

                //Retrieve the period details based on the selected period's name
                var period = await context.lib_period
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Period == selectedLibraryPeriod.Period);

                //If the period is found, set the muliplier value to txt_muliplier
                if (period != null)
                {
                    txt_multiplier.Text = period.StipendMultiplier.ToString();
                }
            }
        }
        private async void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmb_period.EditValue == null)
            {
                MessageBox.Show("Please select a period.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmb_period.SelectedItem is LibraryPeriod selectedLibraryPeriod)
            {
                //When particular period was selected fill the monthly stipend mulitiplier value.
                await PeriodEf(selectedLibraryPeriod);
            }

        }
        private void UpdateAmount()
        {
            try
            {
                decimal monthlyStipend = 0;
                decimal multiplier = 0;

                if (!string.IsNullOrEmpty(txt_monthlystipend.Text))
                {
                    decimal.TryParse(txt_monthlystipend.Text, out monthlyStipend);
                }

                if (!string.IsNullOrEmpty(txt_multiplier.Text))
                {
                    decimal.TryParse(txt_multiplier.Text, out multiplier);
                }

                decimal amount = monthlyStipend * multiplier;
                txt_amount.Text = amount.ToString("0"); // Format as needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while calculating the amount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_monthlystipend_EditValueChanged(object sender, EventArgs e)
        {
            UpdateAmount();
        }

        private void txt_multiplier_EditValueChanged(object sender, EventArgs e)
        {
            UpdateAmount();
        }
    }
}
