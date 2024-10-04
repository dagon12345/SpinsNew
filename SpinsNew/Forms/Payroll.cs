using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsNew.PrintPreviews;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class Payroll : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        public string _userRole;
        private ApplicationDbContext _dbContext;
        private readonly ILibraryMunicipality _libraryMunicipality;
        public Payroll(string username, string userRole, ILibraryMunicipality libraryMunicipality)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            // Attach the CustomDrawCell event to the GridView

            newApplicantToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            viewAttachmentsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;

            _libraryMunicipality = libraryMunicipality;
            _username = username;
            _userRole = userRole;

            if (userRole == "1") // 1 means administration have an access to all the functions.
            {
                ts_delete.Visible = true;
            }
            else // all users except 1
            {
                ts_delete.Visible = false;
            }


        }


        private async void Payroll_Load(object sender, EventArgs e)
        {
            _dbContext = new ApplicationDbContext(); // our dbcontext
                                                     // MunicipalityNew();
            await MunicipalityEf();
            await ClaimTypeEf();
            await YearEf();

            Signatories();
            GridView gridView = (GridView)gridControl1.MainView;
            gridView.ColumnFilterChanged += gridView1_ColumnFilterChanged;
            groupControlPayroll.Text = "Count of showed data: [0]";

            //Search using devexpress searchcontrol.
            searchControl1.Client = gridControl1;

        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;

            // Get the count of rows that match the current filter
            int rowCount = gridView.DataRowCount;

            // Update your control with the row count
            UpdateRowCount(gridView);
        }



        //Refactored code for year to display.
        public async Task YearEf()
        {
            using(var context = new ApplicationDbContext())
            {
                var year = await context.lib_year
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                cmb_year.Properties.Items.Clear();
                foreach(var years in year)
                {
                    cmb_year.Properties.Items.Add(new LibraryYear
                    {
                        Id = years.Id,
                        Year = years.Year
                    });

                }
                // Add the event handler for the SelectedIndexChanged event
                cmb_year.SelectedIndexChanged -= cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
                cmb_year.SelectedIndexChanged += cmb_year_SelectedIndexChanged;

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


        // For municipality combobox properties
        public class MunicipalityItem
        {
            public int PSGCCityMun { get; set; }
            public string CityMunName { get; set; }
            public int PSGCProvince { get; set; }
            public string ProvinceName { get; set; } // Add this property

            public override string ToString()
            {
                return $"{CityMunName} - {ProvinceName}"; // Display both the municipality and province in the ComboBox
            }
        }

        private async void FillBarangay()
        {
            //Municipality
            if (cmb_municipality.SelectedItem is LibraryMunicipality selectedMunicipality)
            {
                cmb_barangay.Text = "";
                await BarangayEf(selectedMunicipality.PSGCCityMun);
                //Search();
            }
        }
        //Refactored claim type
        public async Task ClaimTypeEf()
        {
            using (var context = new ApplicationDbContext())
            {
                // Fetch claim types from the database
                var claimTypes = await context.lib_claim_type
                    .AsNoTracking()
                    .ToListAsync();
                // Bind data to the ComboBoxEdit
                cmb_claimtype.Properties.Items.Clear();
                foreach (var claimType in claimTypes)
                {
                    cmb_claimtype.Properties.Items.Add(new LibraryClaimType
                    {
                        ClaimTypeID = claimType.ClaimTypeID,
                        ClaimType = claimType.ClaimType
                    });
                }
            }

        }
        //Refactored barangay cascading.
        public async Task BarangayEf(int selectedCityMun)
        {
            using (var context = new ApplicationDbContext())
            {
                var barangays = await context.lib_barangay.Where(x => x.PSGCCityMun == selectedCityMun)
                    .AsNoTracking()
                    .ToListAsync();

                foreach(var barangay in barangays)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_barangay.Properties.Items.Add(new CheckedListBoxItem(
                        value: barangay.PSGCBrgy, // The value to store (usually the ID)
                        description: barangay.BrgyName, // The display text
                        checkState: CheckState.Checked // Initially checked all
                    ));
                }
            }
        }

        //Refactored code of municipality dropdown.
        private async Task MunicipalityEf()
        {
            EnableSpinner();
            //this business logic is what we used inside our services LibraryMunicipalityService.
            var municipalityLists = await Task.Run(() => _libraryMunicipality.GetMunicipalitiesAsync());
           
            cmb_municipality.Properties.Items.Clear();
            foreach (var municipalityList in municipalityLists)
            {
                cmb_municipality.Properties.Items.Add(new LibraryMunicipality
                {
                    PSGCCityMun = municipalityList.PSGCCityMun,
                    CityMunName = municipalityList.CityMunName + " - " + municipalityList.LibraryProvince.ProvinceName
                });
                    
                    //value: municipalityList.PSGCCityMun, //Reference the ID
                    //description: municipalityList.CityMunName + " " + municipalityList.LibraryProvince.ProvinceName // Display the text plus the province name.
                   // checkState: CheckState.Unchecked // Initially Unchecked.
            }
            DisableSpinner();


        }
        //Show the spinner
        private void EnableSpinner()
        {
            panel_spinner.Visible = true;
            gb_details.Enabled = false;
            gb_Update.Enabled = false;
            btn_refresh.Enabled = false;
            btnSearch.Enabled = false;

        }
        private async void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is LibraryYear selectedYear)
            {
                // Clear period once the user changed the year selected.
                cmb_period.Text = "Select Period";
                await LoadPeriodsForYearEf(selectedYear.Year);
            }

        }

        // Method to update the row count display and calculate total amount
        public void UpdateRowCount(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Check if the GridView contains the "Amounts" column
            GridColumn amountsColumn = gridView.Columns["Amounts"];
            if (amountsColumn != null)
            {
                // Set up the summary item for the "Amounts" column in the footer
                amountsColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                // Optionally, force an update of the summaries to ensure they are up-to-date
                gridView.UpdateSummary();

                // Retrieve the total sum of the "Amounts" column
                object totalAmount = gridView.Columns["Amounts"].SummaryItem.SummaryValue;

                // Format the total amount with two decimal places
                string formattedTotalAmount = totalAmount != null ? Convert.ToDecimal(totalAmount).ToString("N2") : "0.00";

                // Assign formatted row count and total amount to txt_total (or any other control)
                groupControlPayroll.Text = $"Payroll List: {formattedRowCount} | Total Amount: {formattedTotalAmount}";
            }
        }

        //Raw sql query because it is very complex if we used the EF core ORM.
        public async Task Payrolls() //The query is all about delisted with payroll unclaimed filtered by year and the latest ID inputed into tbl_payroll_socpen
        {

            try
            {
                bool includePayrollStatusID = rbUnclaimed.Checked; // Example of how you might check radio button state
                // Assuming gridView is your GridView instance associated with gridPayroll
                GridView gridView = gridControl1.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string query = @"WITH LatestPayroll AS(
                        SELECT
                            tps.ID,
                            tps.MasterListID,
                            tps.Amount AS LatestAmount,
                            lp.Abbreviation AS LatestAbbreviation,
                            tps.PayrollStatusID,
                            ROW_NUMBER() OVER(PARTITION BY tps.MasterListID ORDER BY tps.ID) AS rn -- ORDER by tbl_socpen_payroll ID Ascending
                           
                       FROM
                           tbl_payroll_socpen tps
                       LEFT JOIN
                           lib_period lp ON tps.PeriodID = lp.PeriodID
                       WHERE

                           /*Filter by Year and PeriodID which is not equal, meaning opposite
                            ex: Search year 2024 and Period of 1st semester, since 1st semester was selected
                           therefore 3q unclaimed is the one that shows instead!*/
                           
                           tps.Year = @Year
                           AND tps.PeriodID != @PeriodID
                       
                        )                   
                       SELECT
                           tps.ID,
                           m.IsVerified as Verified,
                           tps.MasterListID,
                           m.LastName,
                           m.FirstName,
                           m.MiddleName,
                           m.ExtName,
                                    CONCAT(
                                           IFNULL(m.LastName, ''), ', ',
                                           IFNULL(m.FirstName, ''), ' ',
                                           IFNULL(m.MiddleName, ''), ' ',
                                           IFNULL(m.ExtName, ''), ' ',
                                            IFNULL(
                                                   CASE
                                                       WHEN LatestPayroll.PayrollStatusID = 2 THEN
                                                           CONCAT('(', LatestPayroll.LatestAbbreviation, ' - ', LatestPayroll.LatestAmount, ')')
                                                       ELSE
                                                           ''
                                                   END,
                                                   ''
                                               )
                                       ) AS FullName,
                           lb.BrgyName AS Barangay,
                           IF(tps.Address = '' OR tps.Address IS NULL, '', REPLACE(tps.Address, '[^a-zA-Z0-9 ]', '')) AS Address,
                           CONCAT(lb.BrgyName,
                                  IF(tps.Address = '' OR tps.Address IS NULL, '', CONCAT(', ', '[', REPLACE(tps.Address, '[^a-zA-Z0-9 ]', ''), ']'))) AS FullAddress,
                           m.BirthDate,
                           ls.Sex AS Sex,
                           lhs.HealthStatus,
                           m.HealthStatusRemarks,
                           m.IDNumber,
                           tps.Amount AS Amounts,
                           tps.Year,
                           lp.Period,
                           lps.PayrollStatus AS StatusPayroll,
                           lct.ClaimType AS ClaimType,
                           tps.DateClaimedFrom AS DateClaimed,
                           lpt.PayrollType AS PayrollType,
                           lptg.PayrollTag AS PayrollTag,
                           lstat.Status AS Status,
                           m.Remarks,
                           m.DateDeceased,
                           lpm.PaymentMode,

                           tps.Remarks AS PayrollRemarks,

                           tm2.LastName AS LastName2,
                           tm2.FirstName AS FirstName2,
                           tm2.MiddleName AS MiddleName2,
                           tm2.ExtName AS ExtName2,
                           tm2.Remarks as Remarks2,
                           tm2.DateDeceased as DateDeceased2,
                           lstat2.Status AS Status2,
                           lb2.BrgyName AS Barangay2,
                           lit2.Type as Type2,
                           tm2.IDNumber AS IDNumber2,
                           tm2.BirthDate AS BirthDate2,

                           tps.DateTimeModified,
                           tps.ModifiedBy,
                           tps.DateTimeReplaced,
                           tps.ReplacedBy,
                           tps.DateTimeEntry,
                           tps.EntryBy,
                           tps.PRBPDateSent,
                           CONCAT(lcm.CityMunName, ', ', lprov.ProvinceName) AS ProvinceMunicipality,
                           CONCAT(lp.Months, ', ', '(', lp.Period, ' ', tps.Year, ')') AS PeriodMonth,
                          CONCAT(lp.Months, ' ', tps.Year) AS HeaderPeriodYear,
                          lit.Type,
                           CASE
                                   WHEN LatestPayroll.PayrollStatusID = 2 THEN
                                       CONCAT('(', LatestPayroll.LatestAbbreviation, ' - ', LatestPayroll.LatestAmount, ')')
                                   ELSE
                                       ''
                               END AS UnclaimedPayroll
                       FROM
                           tbl_payroll_socpen tps
                       LEFT JOIN
                           tbl_masterlist m ON tps.MasterListID = m.ID
                       LEFT JOIN
                           tbl_masterlist tm2 ON tps.ReplacementForID = tm2.ID
                       LEFT JOIN
                           lib_barangay lb ON tps.PSGCBrgy = lb.PSGCBrgy
                       LEFT JOIN
                           lib_barangay lb2 ON tps.PSGCBrgy_ReplacementFor = lb2.PSGCBrgy
                       LEFT JOIN
                           lib_sex ls ON m.SexID = ls.Id
                       LEFT JOIN
                           lib_health_status lhs ON m.HealthStatusID = lhs.ID
                       LEFT JOIN
                           lib_period lp ON tps.PeriodID = lp.PeriodID
                       LEFT JOIN
                           lib_payroll_status lps ON tps.PayrollStatusID = lps.PayrollStatusID
                       LEFT JOIN
                           lib_claim_type lct ON tps.ClaimTypeID = lct.ClaimTypeID
                       LEFT JOIN
                           lib_payroll_type lpt ON tps.PayrollTypeID = lpt.PayrollTypeID
                       LEFT JOIN
                           lib_payroll_tag lptg ON tps.PayrollTagID = lptg.PayrollTagID
                       LEFT JOIN
                           lib_status lstat ON m.StatusID = lstat.ID
                       LEFT JOIN
                           lib_status lstat2 ON tm2.StatusID = lstat2.ID
                       LEFT JOIN
                           lib_payment_mode lpm ON tps.PaymentModeID = lpm.PaymentModeID
                       LEFT JOIN
                           lib_province lprov ON tps.PSGCProvince = lprov.PSGCProvince
                       LEFT JOIN
                           lib_city_municipality lcm ON tps.PSGCCityMun = lcm.PSGCCityMun
                       LEFT JOIN
                           lib_id_type lit ON m.IDTypeID = lit.ID
                      LEFT JOIN
                           lib_id_type lit2 ON tm2.IDTypeID = lit2.ID
                       LEFT JOIN
                       /* Since Ascending order was implemented
                               the LatestPayroll.rn will show the top row with the value of number 1
                               .The filter above is opposite instead of filter 1st semester period is selected
                               then the 3q is the one that is seleceted depending on the filter applied.*/

                           LatestPayroll ON LatestPayroll.MasterListID = tps.MasterListID 
                           AND LatestPayroll.rn = 1 
                           AND Year = @Year 

                       WHERE
                           tps.PSGCCityMun = @PSGCCityMun
                           AND tps.Year = @Year
                           AND tps.PeriodID = @PeriodID";
                // Add PayrollStatusID condition if not all statuses are included
                if (includePayrollStatusID)
                {
                    query += " AND tps.PayrollStatusID = @PayrollStatusID";
                }

                // Construct a filter for selected barangays
                // Using GetCheckedValues to get the selected items' values.
                var checkedItems = cmb_barangay.Properties.GetCheckedItems();

                // Convert the checked items to a string array
                var barangaysArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                // Check if there are any selected municipalities
                if (barangaysArray.Length > 0)
                {
                    // Join the selected municipality values into a single comma-separated string
                    string barangaysList = string.Join(",", barangaysArray.Select(m => $"'{m.Trim()}'"));

                    // Append the condition to the query
                    query += $" AND m.PSGCBrgy IN ({barangaysList})";

                }




                cmd.CommandText = query;

                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                cmd.Parameters.AddWithValue("@PSGCCityMun", psgccitymun);
                cmd.Parameters.AddWithValue("@Year", cmb_year.EditValue);
                cmd.Parameters.AddWithValue("@PeriodID", periodID);

                // Add PayrollStatusID parameter if not including all statuses
                if (includePayrollStatusID)
                {
                    cmd.Parameters.AddWithValue("@PayrollStatusID", 2);
                }

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                progressBarControl1.Properties.Maximum = 100;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.EditValue = 0;

                // Execute query and fill data
                await Task.Run(() =>
                {
                    da.Fill(dt);

                });


                for (int i = 0; i <= 100; i += 10)
                {
                    await Task.Delay(50); // Simulate a delay
                    this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                }

                // dt.Columns.Add("FullName", typeof(string));
                dt.Columns.Add("Status Payroll", typeof(string));
                dt.Columns.Add("CurrentStatus", typeof(string));
                dt.Columns.Add("Replacement Of", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string statusPayroll = row["StatusPayroll"].ToString(); //Payroll Status
                    string claimType = row["ClaimType"].ToString();


                    string status = row["Status"].ToString();
                    string dateDeceased = row["DateDeceased"]?.ToString();
                    string remarks = row["Remarks"]?.ToString();

                    string lastName2 = row["LastName2"].ToString();
                    string firstName2 = row["FirstName2"].ToString();
                    string middleName2 = row["MiddleName2"].ToString();
                    string extName2 = row["ExtName2"].ToString();

                    List<string> nameParts = new List<string>();
                    if (!string.IsNullOrEmpty(lastName2)) nameParts.Add(lastName2);
                    if (!string.IsNullOrEmpty(firstName2)) nameParts.Add(firstName2);
                    if (!string.IsNullOrEmpty(middleName2)) nameParts.Add(middleName2);
                    if (!string.IsNullOrEmpty(extName2)) nameParts.Add(extName2);

                    string lastName = row["LastName"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string middleName = row["MiddleName"].ToString();
                    string extName = row["ExtName"].ToString();

                    if (!string.IsNullOrEmpty(dateDeceased))
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["CurrentStatus"] = $"{status} ({remarks}) [{dateDeceased}]";
                        }
                        else
                        {
                            row["CurrentStatus"] = $"{status} [{dateDeceased}]";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["CurrentStatus"] = $"{status} ({remarks})";
                        }
                        else
                        {
                            row["CurrentStatus"] = status;
                        }
                    }

                    row["Status Payroll"] = $"{statusPayroll} - {claimType}";
                    row["Replacement Of"] = string.Join(", ", nameParts);
                }

                dt.Columns["Verified"].SetOrdinal(0);
                dt.Columns["FullName"].SetOrdinal(1);
                dt.Columns["Status Payroll"].SetOrdinal(16);
                dt.Columns["CurrentStatus"].SetOrdinal(21);
                dt.Columns["Replacement Of"].SetOrdinal(27);


                gridControl1.DataSource = dt;

                if (gridView != null)
                {
                    gridView.BestFitColumns();

                    // Move the "Verified" column to the first position
                    gridView.Columns["Verified"].VisibleIndex = 0;

                    //gridView.Columns["ID"].Visible = false;

                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;

                    gridView.Columns["Status"].Visible = false;
                    gridView.Columns["Remarks"].Visible = false;
                    gridView.Columns["DateDeceased"].Visible = false;

                    gridView.Columns["StatusPayroll"].Visible = false;
                    gridView.Columns["ClaimType"].Visible = false;
                    gridView.Columns["MasterListID"].Visible = false;

                    gridView.Columns["LastName2"].Visible = false;
                    gridView.Columns["FirstName2"].Visible = false;
                    gridView.Columns["MiddleName2"].Visible = false;
                    gridView.Columns["ExtName2"].Visible = false;
                    gridView.Columns["FullAddress"].Visible = false;
                    gridView.Columns["ProvinceMunicipality"].Visible = false;
                    gridView.Columns["PeriodMonth"].Visible = false;


                    // Freeze the columns
                    //gridView.Columns["UnclaimedAmounts"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["Verified"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                }

                gridView.OptionsCustomization.AllowFilter = true;
                // Update row count display
                UpdateRowCount(gridView);
                this.Invoke(new Action(() => progressBarControl1.EditValue = 100));
                DisableSpinner();
                //QueryPayroll();

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
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
            gb_details.Enabled = true;
            gb_Update.Enabled = true;
            btn_refresh.Enabled = true;
            btnSearch.Enabled = true;
        }
        private async void Search()
        {

            EnableSpinner();
            //await PayrollsEntity();
            await Payrolls();
        }

        public DataTable Signatories()
        {
            DataTable signatoriesData = new DataTable();

            try
            {
                con.Open();  // Open the connection

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, Position, Name FROM lib_signatories WHERE ID IN (1, 2, 3, 4, 5)";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(signatoriesData);

                return signatoriesData; // Return the DataTable after filling it with data
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Return null in case of an error
            }
            finally
            {
                // Ensure the connection is always closed, even if an exception occurs
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private DataTable GetSignatoriesData()
        {
            return Signatories();
        }

        private void PrintReport(DataTable payrollData, DataTable signatoriesData)
        {
            PayrollPrintPreview payrollprintPreview = new PayrollPrintPreview(this);
            payrollprintPreview.SetPayrollData(payrollData);
            payrollprintPreview.SetSignatoriesData(signatoriesData); // Pass signatories data
            payrollprintPreview.Show();
        }
        private void PrintCOERegular(DataTable payrollData, DataTable signatoriesData)
        {
            CoeRegularPrintPreview coepayrollPreview = new CoeRegularPrintPreview(this);
            coepayrollPreview.SetPayrollData(payrollData);
           coepayrollPreview.SetSignatoriesData(signatoriesData); // Pass signatories data
            coepayrollPreview.Show();
        }
        private void PrintCOEUnpaid(DataTable payrollData, DataTable signatoriesData)
        {
            CoeUnpaidPrintPreview coepayrollUnpaidPreview = new CoeUnpaidPrintPreview(this);
            coepayrollUnpaidPreview.SetPayrollData(payrollData);
            coepayrollUnpaidPreview.SetSignatoriesData(signatoriesData); // Pass signatories data
            coepayrollUnpaidPreview.Show();
        }

        private void newApplicantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;

            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("There Is Nothing To Be Printed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable payrollData = (DataTable)gridControl1.DataSource; // Ensure this is the correct DataTable
            DataTable signatoriesData = GetSignatoriesData(); // Retrieve the signatories data
            if (payrollData == null || payrollData.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PrintReport(payrollData, signatoriesData);

        }

        private void rbAllStatus_CheckedChangedAsync(object sender, EventArgs e)
        {
        }

        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBarangay();
        }

        private void rbClaimed_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbUnclaimed_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        Attachments attachmentsForm;
        private MasterList masterlistForm;
        private Payroll payrollForm;

        private void viewAttachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Attachments>().Any())
            {
                gridControl1.Select();
                gridControl1.BringToFront();
            }
            else
            {
                GridView gridView = gridControl1.MainView as GridView;

                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["MasterlistID"]);

                attachmentsForm = new Attachments(masterlistForm, payrollForm, _username); // Ensure this matches the constructor of Attachments
                attachmentsForm.DisplayID(id);
                attachmentsForm.Show();
            }
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_period_Click(object sender, EventArgs e)
        {
        }

        private void cmb_period_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void dt_from_EditValueChanged(object sender, EventArgs e)
        {
            // Ensure dt_to is not null and is enabled before setting its value
            if (dt_to != null && dt_from != null)
            {
                dt_to.EditValue = dt_from.EditValue;
            }

        }

        private void AlternativeUpdatingSingle()
        {

            GridView gridView = gridControl1.MainView as GridView;
            // Update the value of a particular column in the focused row
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "Status Payroll", $"Claimed - {cmb_claimtype.Text}"); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateClaimed", dt_from.Text); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateTimeModified", DateTime.Now); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "ModifiedBy", _username); // Replace 'ColumnName' and 'NewValue'


        }
        private void ArchivingUpdatingSingle()
        {

            GridView gridView = gridControl1.MainView as GridView;
            // Update the value of a particular column in the focused row
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "Status Payroll", $"Archive"); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateClaimed", null); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateTimeModified", DateTime.Now); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "ModifiedBy", _username); // Replace 'ColumnName' and 'NewValue'


        }

        private void AlternativeUpdatingAllRows()
        {
            GridView gridView = gridControl1.MainView as GridView;

            // Loop through all rows in the GridView
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                // Update the value of the particular columns in the current row
                gridView.SetRowCellValue(rowHandle, "Status Payroll", $"Claimed - {cmb_claimtype.Text}");
                gridView.SetRowCellValue(rowHandle, "DateClaimed", dt_from.Text);
                gridView.SetRowCellValue(rowHandle, "DateTimeModified", DateTime.Now);
                gridView.SetRowCellValue(rowHandle, "ModifiedBy", _username);
            }
        }

        private void AlternativeUnclaimingAllRows()
        {
            GridView gridView = gridControl1.MainView as GridView;

            // Loop through all rows in the GridView
            for (int rowHandle = 0; rowHandle < gridView.RowCount; rowHandle++)
            {
                // Update the value of the particular columns in the current row
                gridView.SetRowCellValue(rowHandle, "Status Payroll", $"Unclaimed - ");
                gridView.SetRowCellValue(rowHandle, "DateClaimed", null);
                gridView.SetRowCellValue(rowHandle, "DateTimeModified", DateTime.Now);
                gridView.SetRowCellValue(rowHandle, "ModifiedBy", _username);
            }
        }



        private void unclaimedUpdatingSingle()
        {

            GridView gridView = gridControl1.MainView as GridView;
            // Update the value of a particular column in the focused row
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "Status Payroll", $"Unclaimed -"); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateClaimed", null); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "DateTimeModified", DateTime.Now); // Replace 'ColumnName' and 'NewValue'
            gridView.SetRowCellValue(gridView.FocusedRowHandle, "ModifiedBy", _username); // Replace 'ColumnName' and 'NewValue'


        }

        private async void btn_claimed_Click(object sender, EventArgs e)
        {

            GridView gridView = gridControl1.MainView as GridView;
            if (dt_from.Text == "" || dt_to.Text == "")
            {
                XtraMessageBox.Show("Please select a date before proceeding", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmb_claimtype.Text == "")
            {
                XtraMessageBox.Show("Please select Claim Type before proceeding", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gridView.RowCount == 0)
            {
                XtraMessageBox.Show("Search a payroll first before updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                var selectedYear = (dynamic)cmb_year.SelectedItem;
                int yearID = selectedYear.Year;



                var selectedClaimType = (dynamic)cmb_claimtype.SelectedItem;
                int claimTypeID = selectedClaimType.ClaimTypeID;



                if (gridView != null)
                {
                    // Get the row data
                    DataRowView row = gridView.GetRow(gridView.FocusedRowHandle) as DataRowView;

                    if (row != null && row["ID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(row["ID"]);

                        // Check if 'ck_all' is checked
                        if (ck_all.Checked)
                        {
                            if (MessageBox.Show("Are you sure you want to select all?", "Update All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                // Iterate over all visible rows in the GridControl
                                for (int i = 0; i < gridView1.RowCount; i++)
                                {
                                    // Retrieve the visible row handle
                                    int rowHandle = gridView1.GetVisibleRowHandle(i);

                                    // Get the ID of the current row (adjust to match your column name)
                                    int idGrid = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));
                                    var payroll = _dbContext.tbl_payroll_socpen.FirstOrDefault(x => x.ID == idGrid);

                                    if (payroll != null)
                                    {
                                        // Update payroll details
                                        payroll.DateClaimedFrom = Convert.ToDateTime(dt_from.EditValue);
                                        payroll.DateClaimedTo = Convert.ToDateTime(dt_to.EditValue);
                                        payroll.ClaimTypeID = claimTypeID;
                                        payroll.Remarks = txt_remarks.Text;
                                        payroll.PayrollStatusID = 1;
                                        payroll.DateTimeModified = DateTime.Now;
                                        payroll.ModifiedBy = _username;

                                        // Update the payroll record in the database context
                                        _dbContext.tbl_payroll_socpen.Update(payroll);
                                        AlternativeUpdatingAllRows();
                                        // AlternativeUpdating();
                                    }



                                }

                                // Save all changes to the database after updating all records
                                await _dbContext.SaveChangesAsync();

                                XtraMessageBox.Show("All visible data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                // Refresh the data in the GridControl
                                //gridPayroll.Refresh();
                                //Search();
                            }
                        }
                        else
                        {
                            // Handle the case where 'ck_all' is not checked (updating a single record)
                            var payroll = _dbContext.tbl_payroll_socpen.FirstOrDefault(x => x.ID == id);

                            if (payroll != null)
                            {
                                // Update payroll details for the single record
                                payroll.DateClaimedFrom = Convert.ToDateTime(dt_from.EditValue);
                                payroll.DateClaimedTo = Convert.ToDateTime(dt_to.EditValue);
                                payroll.ClaimTypeID = claimTypeID;
                                payroll.Remarks = txt_remarks.Text;
                                payroll.PayrollStatusID = 1;
                                payroll.DateTimeModified = DateTime.Now;
                                payroll.ModifiedBy = _username;

                                // Save changes to the database
                                _dbContext.tbl_payroll_socpen.Update(payroll);
                                await _dbContext.SaveChangesAsync();

                                XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AlternativeUpdatingSingle();

                                // Refresh the data in the GridControl
                                //Search();
                                // gridPayroll.Refresh();
                            }
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid or missing ID value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("GridView or row handle is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btn_unclaimed_Click(object sender, EventArgs e)
        {


            GridView gridView = gridControl1.MainView as GridView;

            if (gridView.RowCount == 0)
            {
                XtraMessageBox.Show("Search a payroll first before updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {


                if (gridView != null)
                {
                    // Get the row data
                    DataRowView row = gridView.GetRow(gridView.FocusedRowHandle) as DataRowView;

                    if (row != null && row["ID"] != DBNull.Value)
                    {
                        int id = Convert.ToInt32(row["ID"]);

                        // Check if 'ck_all' is checked
                        if (ck_all.Checked)//Update all 
                        {
                            if (MessageBox.Show("Are you sure you want to select all?", "Update All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                // Iterate over all visible rows in the GridControl
                                for (int i = 0; i < gridView1.RowCount; i++)
                                {
                                    // Retrieve the visible row handle
                                    int rowHandle = gridView1.GetVisibleRowHandle(i);
                                    int idGrid = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));

                                    var payroll = _dbContext.tbl_payroll_socpen.FirstOrDefault(x => x.ID == idGrid);
                                    if (payroll != null)
                                    {
                                        // Update payroll details
                                        payroll.DateClaimedFrom = null;
                                        payroll.DateClaimedTo = null;
                                        payroll.ClaimTypeID = null;
                                        payroll.Remarks = txt_remarks.Text;
                                        payroll.PayrollStatusID = 2; // Unclaimed
                                        payroll.DateTimeModified = DateTime.Now;
                                        payroll.ModifiedBy = _username;

                                        // Update the payroll record in the database context
                                        _dbContext.tbl_payroll_socpen.Update(payroll);
                                        AlternativeUnclaimingAllRows();
                                        // AlternativeUpdating();
                                        //await _dbContext.SaveChangesAsync();
                                    }
                                }
                                // Save all changes to the database after updating all records
                                await _dbContext.SaveChangesAsync();

                                XtraMessageBox.Show("All visible data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the data in the GridControl
                                // Search();

                            }
                        }
                        else //Update Individually 
                        {
                            // Retrieve the payroll record
                            var payroll = _dbContext.tbl_payroll_socpen.FirstOrDefault(x => x.ID == id);

                            if (payroll != null)
                            {
                                // Update payroll details
                                payroll.DateClaimedFrom = null;
                                payroll.DateClaimedTo = null;
                                payroll.ClaimTypeID = null;
                                payroll.Remarks = txt_remarks.Text;
                                payroll.PayrollStatusID = 2; // Unclaimed
                                payroll.DateTimeModified = DateTime.Now;
                                payroll.ModifiedBy = _username;

                                // Save changes to the database
                                _dbContext.tbl_payroll_socpen.Update(payroll);
                                await _dbContext.SaveChangesAsync();

                                XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                unclaimedUpdatingSingle();
                                //Search();
                            }
                            else
                            {
                                XtraMessageBox.Show("Payroll record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid or missing ID value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("GridView or row handle is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"Error message {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void gridPayroll_Click(object sender, EventArgs e)
        {

        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void ts_delete_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource == null)

            {
                MessageBox.Show("Search data you want to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (XtraMessageBox.Show("This will delete all the data displayed. Continue?", "Delete all", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                GridView gridView = gridControl1.MainView as GridView;
                // Get the row data
                DataRowView row = gridView.GetRow(gridView.FocusedRowHandle) as DataRowView;
                // Retrieve the visible row handle

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    int rowHandle = gridView1.GetVisibleRowHandle(i);
                    int idGrid = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));

                    //searchControl
                    _dbContext.Remove(_dbContext.tbl_payroll_socpen.Single(x => x.ID == idGrid));
                }
                EnableSpinner();
                await _dbContext.SaveChangesAsync();
                Search();
                MessageBox.Show("Data displayed all deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private PayrollFiles payrollFilesForm;
        private PayrollHistory payrollHistoryForm;
        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<PayrollFiles>().Any())
            {
                payrollFilesForm.Select();
                payrollFilesForm.BringToFront();
            }
            else
            {

                payrollFilesForm = new PayrollFiles(payrollHistoryForm, payrollForm);

                if (cmb_municipality.Text == "Select City/Municipality")
                {
                    MessageBox.Show("Please Select City/Municipality", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmb_year.Text == "Select Year")
                {
                    MessageBox.Show("Please Select Year", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmb_period.Text == "Select Period")
                {
                    MessageBox.Show("Please Select Period", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var selectedMunicipality = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedMunicipality.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                var selectedYear = (dynamic)cmb_year.SelectedItem;
                int yearID = selectedYear.Year;

                // int id = Convert.ToInt32(row["MasterListID"]);
                int municipality = psgccitymun;
                int period = periodID;
                int year = yearID;


                payrollFilesForm.DisplayID(municipality, year, period);
                payrollFilesForm.ShowDialog();

            }
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource == null)
            {
                MessageBox.Show("Empty table no need to refresh", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Search();
        }

        private async void btnArchive_Click(object sender, EventArgs e)
        {

            GridView gridView = gridControl1.MainView as GridView;
            // Get the row data
            DataRowView row = gridView.GetRow(gridView.FocusedRowHandle) as DataRowView;
            if (gridView.RowCount == 0)
            {
                XtraMessageBox.Show("Search a payroll first before archiving", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Are you sure you want to archive this data?", "Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Handle the case where 'ck_all' is not checked (updating a single record)
                int id = Convert.ToInt32(row["ID"]);

                var payroll = _dbContext.tbl_payroll_socpen.FirstOrDefault(x => x.ID == id);

                if (payroll != null)
                {

                    payroll.PayrollStatusID = 3;
                    payroll.DateTimeModified = DateTime.Now;
                    payroll.ModifiedBy = _username;

                    // Save changes to the database
                    _dbContext.tbl_payroll_socpen.Update(payroll);
                    await _dbContext.SaveChangesAsync();

                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ArchivingUpdatingSingle();
                }
            }
        }

        private void cmb_barangay_EditValueChanged(object sender, EventArgs e)
        {
            //Search();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmb_municipality.Text == "Select City/Municipality" || cmb_year.Text == "Select Year" || cmb_period.Text == "Select Period" || cmb_barangay.Text == "Select Barangay")
            {
                MessageBox.Show("Fill all the fields before searching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Search();
        }

        private void claimedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;

            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("There Is Nothing To Be Printed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable payrollData = (DataTable)gridControl1.DataSource; // Ensure this is the correct DataTable
            DataTable signatoriesData = GetSignatoriesData(); // Retrieve the signatories data
            if (payrollData == null || payrollData.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PrintCOERegular(payrollData, signatoriesData);
            //PrintCOERegular(payrollData);
        }

        private void unclaimedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;

            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("There Is Nothing To Be Printed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable payrollData = (DataTable)gridControl1.DataSource; // Ensure this is the correct DataTable
            DataTable signatoriesData = GetSignatoriesData(); // Retrieve the signatories data
            if (payrollData == null || payrollData.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             PrintCOEUnpaid(payrollData, signatoriesData);
            //PrintCOEUnpaid(payrollData);
        }

        private void ExportToExcel(string filePath)
        {
            try
            {

                var exportOptions = new XlsxExportOptions
                {
                    ExportMode = XlsxExportMode.SingleFile,
                    // IncludeSummary = true, // This may not exist; verify in documentation
                    // Set other options as needed
                };

                // Perform the export from your grid control
                gridControl1.ExportToXlsx(filePath, exportOptions);

                // Notify the user that the export was successful
                XtraMessageBox.Show("Data exported successfully to " + filePath, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                XtraMessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = $"Payroll exporting.xlsx";

            // Show the dialog and check if the user clicked Save
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Call the export function
                ExportToExcel(saveFileDialog.FileName);
            }
        }
    }
}
