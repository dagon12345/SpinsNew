﻿using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsWinforms.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class Replacements : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        EditApplicant EditApplicantForm;
        private Replacements replacementsForm;
        private MasterList masterlistForm;
        private ITableLog _tableLog;
        private ILibraryMunicipality _libraryMunicipality;
        public Replacements(string username, ITableLog tableLog, ILibraryMunicipality libraryMunicipality)
        {
            InitializeComponent();
            _tableLog = tableLog;
            _libraryMunicipality = libraryMunicipality;
            con = new MySqlConnection(cs.dbcon);
            _username = username;
            // gridView1.FocusedRowChanged += gridView_FocusedRowChanged;
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        private async void Replacements_Load(object sender, EventArgs e)
        {
            await MunicipalityEF();
            await YearEF();

            // Cast the MainView to GridView
            GridView gridView = gridDelisted.MainView as GridView;
            //Integrate search control into our grid control.
            searchControl1.Client = gridDelisted;
            searchControl2.Client = gridWaitlisted;
        }

        //This populates our municipality combobox inside our Delisted form.
        public async Task MunicipalityEF()
        {
            var municipalities = await _libraryMunicipality.GetMunicipalitiesAsync();
            cmb_municipality.Properties.Items.Clear();
            foreach (var municpality in municipalities)
            {
                cmb_municipality.Properties.Items.Add(new LibraryMunicipality
                {
                    CityMunName = municpality.CityMunName + " " + municpality.LibraryProvince.ProvinceName,
                    PSGCCityMun = municpality.PSGCCityMun

                });

            }

        }

        private async Task YearEF()
        {
            using(var context = new ApplicationDbContext())
            {
                var years = await context.lib_year.Where(x => x.Active == 1)
                    .AsNoTracking()
                    .ToListAsync();

                cmb_year.Properties.Items.Clear();
                foreach(var year in years)
                {
                    cmb_year.Properties.Items.Add(new LibraryYear { 
                        Id = year.Id,
                        Year = year.Year
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

        private void GridView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            // Assuming gridControl1 is your DevExpress GridControl instance
            GridView gridView = gridDelisted.MainView as GridView;

            if (e.Column == null) // Assuming you want to show this in the first footer cell
            {
                // Calculate the row count
                int rowCount = gridView.RowCount;

                // Format the display text
                e.Info.DisplayText = $"Total Rows: {rowCount}";
            }
        }


        // Method to update the row count display
        public void UpdateRowCount(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Assign formatted row count to txt_total (or any other control)
            groupControl2.Text = $"Delisted List: {formattedRowCount}";
        }

        // Method to update the row count display
        public void UpdateRowCountGroupControl(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Assign formatted row count to txt_total (or any other control)
            groupControl3.Text = $"Replacements Available: {formattedRowCount}";
        }
        //Show the spinner
        private void EnableSpinner()
        {
            // btn_search.Enabled = false;
            // btn_refresh.Enabled = false;
            panel_spinner.Visible = true;
            lbl_fromreplace.Text = "-----";
            lbl_namereplace.Text = "-----";
            lbl_fromtobe.Text = "-----";
            lbl_nametobe.Text = "-----";

            groupControl1.Enabled = false;

        }
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
                                               // btn_search.Enabled = true; //Enable textbox once gridview was loaded successfully
                                               // btn_refresh.Enabled = true;
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
            groupControl1.Enabled = true;
        }
        public async Task Delisted() //The query is all about delisted with payroll unclaimed filtered by year and the latest ID inputed into tbl_payroll_socpen
        {
            try
            {
                // Assuming gridView is your GridView instance associated with gridControl1
                GridView gridView = gridDelisted.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"WITH LatestPayroll AS (
                SELECT 
                    MasterListID,
                    MAX(ID) AS MaxID
                FROM 
                    tbl_payroll_socpen
                WHERE
                    Year = @Year
                GROUP BY 
                    MasterListID
            ),
             ReplacementPayroll AS (
                SELECT 
                    MasterListID,
                    MAX(ID) AS MaxID
                FROM 
                    tbl_payroll_socpen
                WHERE
                    Year = @Year
                    AND PeriodID = @PeriodID
                GROUP BY 
                    MasterListID
            )
            
            SELECT 
                CASE 
                    WHEN tps.PayrollStatusID = 1 THEN NULL
                    ELSE libps.PayrollStatus
                END AS PayrollStatus,
                CASE 
                    WHEN tps.PayrollStatusID = 1 THEN NULL
                    ELSE tps.Amount
                END AS Amount,
                CASE
                    WHEN tps.PayrollStatusID = 1 THEN NULL
                    ELSE libper.Abbreviation
                END AS Abbreviation,

                tps.ID AS PayrollID,
                /*tpsReplace.ID AS ReplacementPayrollID,*/
                tps2.ID AS ReplacementsPayrollID,
                d.ID,
                d.MasterListID,
                d.PSGCRegion,
                d.PSGCProvince,
                d.PSGCCityMun,
                d.PSGCBrgy,

                d.MasterListID_Replacement,
                d.PSGCRegion_Replacement,
                d.PSGCProvince_Replacement,
                d.PSGCCityMun_Replacement,
                d.PSGCBrgy_Replacement,

                tps2.ReplacementForID,
                tps2.PSGCRegion_ReplacementFor,
                tps2.PSGCProvince_ReplacementFor,
                tps2.PSGCCityMun_ReplacementFor,
                tps2.PSGCBrgy_ReplacementFor,

                
                tm.LastName,
                tm.FirstName,
                tm.MiddleName,
                tm.ExtName,
                lb.BrgyName as Barangay,
                tm.BirthDate,
                ls.Status as Reason,
                tm.DateDeceased,
                d.StatusRemarks,
                tm2.ID as MasterListIDReplacement,
                tm2.LastName as LastName2,
                tm2.FirstName as FirstName2,
                tm2.MiddleName as MiddleName2,
                tm2.ExtName as ExtName2,
                lb2.BrgyName as FromBarangay,
                tm2.Birthdate as ReplacementBirthdate,
                lpr.Period as Period,
                d.Year,
                d.PeriodID,
                lps.ReportSource as ReportSource,
                d.DelistedBy,
                d.DateTimeDelisted,
                d.ReplacedBy,
                d.DateTimeReplaced,
                tg_max.ReferenceCode as GIS,
                ts_max.ReferenceCode as SPBUF
            FROM 
                tbl_delisted d
            LEFT JOIN
                tbl_masterlist tm ON d.MasterListID = tm.ID
            LEFT JOIN 
                    (SELECT tg1.*
                     FROM tbl_gis tg1
                     INNER JOIN (
                         SELECT MasterlistID, MAX(ID) as MaxGISID
                         FROM tbl_gis
                         GROUP BY MasterlistID
                     ) tg2 ON tg1.MasterlistID = tg2.MasterlistID AND tg1.ID = tg2.MaxGISID
                    ) tg_max ON tm.ID = tg_max.MasterlistID
            LEFT JOIN 
                         (SELECT ts1.*
                          FROM tbl_spbuf ts1
                          INNER JOIN (
                              SELECT MasterlistID, MAX(ID) as MaxSPBUFID
                              FROM tbl_spbuf
                              GROUP BY MasterlistID
                          ) ts2 ON ts1.MasterlistID = ts2.MasterlistID AND ts1.ID = ts2.MaxSPBUFID
                         ) ts_max ON tm.ID = ts_max.MasterlistID
            LEFT JOIN
                tbl_masterlist tm2 ON d.MasterListID_Replacement = tm2.ID
            LEFT JOIN 
                lib_region lr ON d.PSGCRegion = lr.PSGCRegion
            LEFT JOIN 
                lib_province lp ON d.PSGCProvince = lp.PSGCProvince
            LEFT JOIN 
                lib_city_municipality lc ON d.PSGCCityMun = lc.PSGCCityMun
            LEFT JOIN 
                lib_barangay lb ON d.PSGCBrgy = lb.PSGCBrgy
            LEFT JOIN 
                lib_region lr2 ON d.PSGCRegion_Replacement = lr2.PSGCRegion
            LEFT JOIN 
                lib_province lp2 ON d.PSGCProvince_Replacement = lp2.PSGCProvince
            LEFT JOIN 
                lib_city_municipality lc2 ON d.PSGCCityMun_Replacement = lc2.PSGCCityMun
            LEFT JOIN 
                lib_barangay lb2 ON d.PSGCBrgy_Replacement = lb2.PSGCBrgy
            LEFT JOIN
                lib_status ls ON d.StatusID = ls.ID
            LEFT JOIN
                lib_period lpr ON d.PeriodID = lpr.PeriodID
            LEFT JOIN
                lib_report_source lps ON d.ReportSourceID = lps.ID



            LEFT JOIN
                tbl_payroll_socpen tps ON d.MasterListID = tps.MasterListID
                AND tps.ID = (SELECT MaxID FROM LatestPayroll WHERE MasterListID = tps.MasterListID)

            LEFT JOIN
                tbl_payroll_socpen tps2 ON d.MasterListID_Replacement = tps2.MasterListID
                AND tps2.ID = (SELECT MaxID FROM ReplacementPayroll WHERE MasterListID = tps2.MasterListID)



            LEFT JOIN
                lib_payroll_status libps ON tps.PayrollStatusID = libps.PayrollStatusID
            LEFT JOIN
                lib_period libper ON tps.PeriodID = libper.PeriodID
            WHERE 
                d.PSGCCityMun = @PSGCCityMun
                AND d.Year = @Year
                AND d.PeriodID = @PeriodID
            ORDER BY 
                tm.LastName, tm.FirstName, tm.MiddleName, tm.ExtName";
                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                cmd.Parameters.AddWithValue("@PSGCCityMun", psgccitymun);
                cmd.Parameters.AddWithValue("@Year", cmb_year.EditValue);
                cmd.Parameters.AddWithValue("@PeriodID", periodID);
                // cmd.Parameters.AddWithValue("@Status", cmb_status.Text);


                //cmd.Parameters.AddWithValue("@PSGCCityMun", cmb_municipality.Text); // Use selected municipality

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                //Await to reduce lag while loading large amount of datas
                //await Task.Run(() => da.Fill(dt));
                // Configure the progress bar
                progressBarControl1.Properties.Maximum = 100;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.EditValue = 0;

                //Await to reduce lag while loading large amount of datas
                //await Task.Run(() => da.Fill(dt));
                // Create a task to fill the DataTable and update the progress bar
                await Task.Run(async () =>
                {
                    await Task.Run(() => da.Fill(dt));
                    for (int i = 0; i <= 100; i += 10)
                    {
                        // Simulate progress (this is just for demonstration; adjust as necessary)
                        System.Threading.Thread.Sleep(50); // Simulate a delay
                        this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                    }
                });

                // Add a new column for concatenated Status and DateDeceased
                dt.Columns.Add("FullName", typeof(string));
                dt.Columns.Add("Name of Replacement", typeof(string));
                dt.Columns.Add("Unclaimed Payrolls", typeof(string));

                // Populate the new column with concatenated values
                foreach (DataRow row in dt.Rows)
                {
                    string lastName2 = row["LastName2"].ToString();
                    string firstName2 = row["FirstName2"].ToString();
                    string middleName2 = row["MiddleName2"].ToString();
                    string extName2 = row["ExtName2"].ToString();

                    string lastName = row["LastName"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string middleName = row["MiddleName"].ToString();
                    string extName = row["ExtName"].ToString();

                    string payrollStatus = row["PayrollStatus"].ToString();
                    string amount = row["Amount"].ToString();
                    string abbreviation = row["Abbreviation"].ToString();

                    row["FullName"] = $"{lastName}, {firstName} {middleName} {extName}";

                    string nameofReplacements = row["Name of Replacement"]?.ToString();

                    // Build the replacement name string conditionally to avoid trailing commas
                    List<string> nameParts = new List<string>();
                    if (!string.IsNullOrEmpty(lastName2)) nameParts.Add(lastName2);
                    if (!string.IsNullOrEmpty(firstName2)) nameParts.Add(firstName2);
                    if (!string.IsNullOrEmpty(middleName2)) nameParts.Add(middleName2);
                    if (!string.IsNullOrEmpty(extName2)) nameParts.Add(extName2);
                    row["Name of Replacement"] = string.Join(", ", nameParts);

                    // Build the Unclaimed Payrolls string conditionally
                    string unclaimedPayrolls = string.Empty;

                    if (!string.IsNullOrEmpty(abbreviation))
                    {
                        unclaimedPayrolls = abbreviation;

                        if (!string.IsNullOrEmpty(amount) || !string.IsNullOrEmpty(payrollStatus))
                        {
                            unclaimedPayrolls += " (";

                            if (!string.IsNullOrEmpty(amount))
                                unclaimedPayrolls += amount;

                            if (!string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(payrollStatus))
                                unclaimedPayrolls += "-";

                            if (!string.IsNullOrEmpty(payrollStatus))
                                unclaimedPayrolls += payrollStatus;

                            unclaimedPayrolls += ")";
                        }
                    }

                    // Assign the constructed string to the Unclaimed Payrolls column
                    row["Unclaimed Payrolls"] = unclaimedPayrolls;

                }
                // Move the new column to the 6th position
                dt.Columns["Unclaimed Payrolls"].SetOrdinal(0);
                dt.Columns["FullName"].SetOrdinal(1);
                dt.Columns["BirthDate"].SetOrdinal(2);
                dt.Columns["Barangay"].SetOrdinal(3);
                dt.Columns["Reason"].SetOrdinal(4);
                dt.Columns["DateDeceased"].SetOrdinal(5);
                dt.Columns["StatusRemarks"].SetOrdinal(6);
                dt.Columns["Name of Replacement"].SetOrdinal(7);
                dt.Columns["FromBarangay"].SetOrdinal(8);
                dt.Columns["DateTimeReplaced"].SetOrdinal(9);



                //We are using DevExpress datagridview
                gridDelisted.DataSource = dt;

                // Get the GridView instance
                // GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Hide the "ID" column
                    /*Hide latest table that we've added*/
                    gridView.Columns["PayrollID"].Visible = false;
                    gridView.Columns["ReplacementsPayrollID"].Visible = false;
                    gridView.Columns["ID"].Visible = false;
                    gridView.Columns["MasterListID"].Visible = false;
                    gridView.Columns["MasterListID_Replacement"].Visible = false;
                    gridView.Columns["PSGCRegion_Replacement"].Visible = false;
                    gridView.Columns["PSGCProvince_Replacement"].Visible = false;
                    gridView.Columns["PSGCCityMun_Replacement"].Visible = false;
                    gridView.Columns["PSGCBrgy_Replacement"].Visible = false;
                    gridView.Columns["ReplacementForID"].Visible = false;
                    gridView.Columns["PSGCRegion_ReplacementFor"].Visible = false;
                    gridView.Columns["PSGCProvince_ReplacementFor"].Visible = false;
                    gridView.Columns["PSGCCityMun_ReplacementFor"].Visible = false;
                    gridView.Columns["PSGCBrgy_ReplacementFor"].Visible = false;
                    gridView.Columns["MasterListIDReplacement"].Visible = false;
                    gridView.Columns["PeriodID"].Visible = false;







                    gridView.Columns["PSGCRegion"].Visible = false;
                    gridView.Columns["PSGCProvince"].Visible = false;
                    gridView.Columns["PSGCCityMun"].Visible = false;
                    gridView.Columns["PSGCBrgy"].Visible = false;

                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;
                    gridView.Columns["LastName2"].Visible = false;
                    gridView.Columns["FirstName2"].Visible = false;
                    gridView.Columns["MiddleName2"].Visible = false;
                    gridView.Columns["ExtName2"].Visible = false;
                    gridView.Columns["PayrollStatus"].Visible = false;
                    gridView.Columns["Amount"].Visible = false;
                    gridView.Columns["Abbreviation"].Visible = false;
                    //gridView.Columns["PayrollID"].Visible = false;
                    // Freeze the columns
                    gridView.Columns["Unclaimed Payrolls"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;

                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                }
                // Update row count display
                UpdateRowCount(gridView);
                //groupControl1.Enabled = true;
                //DisableSpinner();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public async Task Waitlisted()
        {
            try
            {
                // Assuming gridView is your GridView instance associated with gridControl1
                GridView gridView = gridWaitlisted.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                        m.ID,
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName,
                        m.PSGCRegion,
                        m.PSGCProvince,
                        m.PSGCCityMun,
                        m.PSGCBrgy,
                        lb.BrgyName as Barangay,
                        m.BirthDate as BirthDate,
                        TIMESTAMPDIFF(YEAR, m.BirthDate, CURDATE()) AS Age,
                        tg_max.SPISBatch as SpisBatch,
                        tg_max.SPISDateReturned as DateWaitlisted,
                        tg_max.ReferenceCode as GIS,
                        ts_max.ReferenceCode as SPBUF
                    FROM 
                        tbl_masterlist m
                  LEFT JOIN 
                         (SELECT tg1.*
                          FROM tbl_gis tg1
                          INNER JOIN (
                              SELECT MasterlistID, MAX(ID) as MaxGISID
                              FROM tbl_gis
                              GROUP BY MasterlistID
                          ) tg2 ON tg1.MasterlistID = tg2.MasterlistID AND tg1.ID = tg2.MaxGISID
                         ) tg_max ON m.ID = tg_max.MasterlistID
                   LEFT JOIN 
                         (SELECT ts1.*
                          FROM tbl_spbuf ts1
                          INNER JOIN (
                              SELECT MasterlistID, MAX(ID) as MaxSPBUFID
                              FROM tbl_spbuf
                              GROUP BY MasterlistID
                          ) ts2 ON ts1.MasterlistID = ts2.MasterlistID AND ts1.ID = ts2.MaxSPBUFID
                         ) ts_max ON m.ID = ts_max.MasterlistID
                    LEFT JOIN 
                        lib_region lr ON m.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN 
                        lib_province lp ON m.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN 
                        lib_city_municipality lc ON m.PSGCCityMun = lc.PSGCCityMun
                    LEFT JOIN 
                        lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                    LEFT JOIN 
                        lib_assessment la ON tg_max.AssessmentID = la.ID
                    WHERE 
                        m.PSGCCityMun = @PSGCCityMun
                        AND tg_max.ReferenceCode IS NOT NULL
                        AND tg_max.SPISBatch IS NOT NULL
                        AND m.StatusID = 99
                        AND m.DateTimeDeleted IS NULL
                        AND la.ID = 1
                    GROUP BY
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName
                    ORDER BY
                        tg_max.SPISBATCH
                        ";
                // Retrieve the selected item and get the PSGCCityMun Code.
                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;
                cmd.Parameters.AddWithValue("@PSGCCityMun", psgccitymun);
                // cmd.Parameters.AddWithValue("@Status", cmb_status.Text);


                //cmd.Parameters.AddWithValue("@PSGCCityMun", cmb_municipality.Text); // Use selected municipality

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                //Await to reduce lag while loading large amount of datas
                //await Task.Run(() => da.Fill(dt));
                // Configure the progress bar
                progressBarControl1.Properties.Maximum = 100;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.EditValue = 0;

                //Await to reduce lag while loading large amount of datas
                //await Task.Run(() => da.Fill(dt));
                // Create a task to fill the DataTable and update the progress bar
                await Task.Run(async () =>
                {
                    await Task.Run(() => da.Fill(dt));
                    for (int i = 0; i <= 100; i += 10)
                    {
                        // Simulate progress (this is just for demonstration; adjust as necessary)
                        System.Threading.Thread.Sleep(50); // Simulate a delay
                        this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                    }
                });

                // Add a new column for concatenated Status and DateDeceased
                dt.Columns.Add("FullName", typeof(string));
                //dt.Columns.Add("Name of Replacement", typeof(string));

                // Populate the new column with concatenated values
                foreach (DataRow row in dt.Rows)
                {
                    //string lastName2 = row["LastName2"].ToString();
                    //string firstName2 = row["FirstName2"].ToString();
                    //string middleName2 = row["MiddleName2"].ToString();
                    //string extName2 = row["ExtName2"].ToString();

                    string lastName = row["LastName"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string middleName = row["MiddleName"].ToString();
                    string extName = row["ExtName"].ToString();

                    row["FullName"] = $"{lastName}, {firstName} {middleName} {extName}";

                    //string nameofReplacements = row["Name of Replacement"]?.ToString();

                    //// Build the replacement name string conditionally to avoid trailing commas
                    //List<string> nameParts = new List<string>();
                    //if (!string.IsNullOrEmpty(lastName2)) nameParts.Add(lastName2);
                    //if (!string.IsNullOrEmpty(firstName2)) nameParts.Add(firstName2);
                    //if (!string.IsNullOrEmpty(middleName2)) nameParts.Add(middleName2);
                    //if (!string.IsNullOrEmpty(extName2)) nameParts.Add(extName2);
                    //row["Name of Replacement"] = string.Join(", ", nameParts);

                }
                // Move the new column to the 6th position
                dt.Columns["FullName"].SetOrdinal(1);
                //dt.Columns["Name of Replacement"].SetOrdinal(13);


                //We are using DevExpress datagridview
                gridWaitlisted.DataSource = dt;

                // Get the GridView instance
                // GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Hide the "ID" column
                    gridView.Columns["ID"].Visible = false;
                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;
                    gridView.Columns["PSGCRegion"].Visible = false;
                    gridView.Columns["PSGCProvince"].Visible = false;
                    gridView.Columns["PSGCCityMun"].Visible = false;
                    gridView.Columns["PSGCBrgy"].Visible = false;
                    // Freeze the columns
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    //gridView.Columns["FirstName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    //gridView.Columns["MiddleName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    //gridView.Columns["ExtName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;

                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                    //// Enable searching
                    //gridView.OptionsFind.AlwaysVisible = true; // Show find panel

                    //// Enable search in specific columns
                    //gridView.Columns["LastName"].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    //gridView.Columns["FirstName"].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    //gridView.Columns["MiddleName"].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                }
                // Update row count display
                UpdateRowCountGroupControl(gridView);
                DisableSpinner();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private async void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is LibraryYear selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                await LoadPeriodsForYearEf(selectedYear.Year);
            }
         

        }
        private async void Search()
        {
            if (cmb_municipality.Text == "Select City/Municipality" || cmb_year.Text == "Select Year" || cmb_period.Text == "Select Period")
            {
                // XtraMessageBox.Show("Please fill all the search dropdown before searching", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EnableSpinner();//Enable the spinner
            await Delisted(); // Display delisted list tbl_delisted.
            await Waitlisted();
        }
        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void searchControl1_QueryIsSearchColumn(object sender, QueryIsSearchColumnEventArgs args)
        {
            //Search only specific columns example FullName.
            if (args.FieldName != "FullName") //for FullName
                args.IsSearchColumn = false;
        }

        public void ClickedDelisted()
        {

            try
            {
                con.Open();

                // Assuming gridControl1 is your GridControl and it is bound to the data source
                GridView gridView = gridDelisted.MainView as GridView;
                if (gridView == null) return;

                // Get the selected row's ID
                if (gridView.SelectedRowsCount > 0)
                {
                    int selectedRowHandle = gridView.GetSelectedRows()[0];
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRowHandle, "ID"));

                    // Prepare SQL command with parameter
                    using (MySqlCommand cmd0 = new MySqlCommand())
                    {
                        cmd0.Connection = con;
                        cmd0.CommandType = CommandType.Text;
                        cmd0.CommandText = @"SELECT 
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName,
                        m.PSGCRegion,
                        m.PSGCProvince,
                        m.PSGCCityMun,
                        m.PSGCBrgy,
                        m.StatusID,
                        lr.Region as Region,
                        lp.ProvinceName as ProvinceName,
                        lcm.CityMunName as Municipality,
                        lb.BrgyName as Barangay
                    FROM
                        tbl_delisted d
                    LEFT JOIN
                        tbl_masterlist m ON d.MasterlistID = m.ID
                    LEFT JOIN
                        lib_region lr ON d.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN
                        lib_province lp ON d.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN
                        lib_city_municipality lcm ON d.PSGCCityMun = lcm.PSGCCityMun
                    LEFT JOIN
                        lib_barangay lb ON d.PSGCBrgy = lb.PSGCBrgy
                    WHERE
                        d.ID = @ID";

                        // Add parameters
                        cmd0.Parameters.AddWithValue("@ID", id);

                        DataTable dt0 = new DataTable();
                        using (MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0))
                        {
                            // Fill the DataTable
                            da0.Fill(dt0);
                        }

                        // Check if data is retrieved
                        if (dt0.Rows.Count > 0)
                        {
                            DataRow row = dt0.Rows[0];
                            // Concatenate and populate the fullname label
                            lbl_nametobe.Text = $"{row["LastName"]}, {row["FirstName"]} {row["MiddleName"]} {row["ExtName"]}";
                            // Concatenate and populate the address label
                            lbl_fromtobe.Text = $"{row["Region"]}, {row["ProvinceName"]}, {row["Municipality"]}, {row["Barangay"]}";
                        }
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }


        public void ClickedMasterlist()
        {

            try
            {
                con.Open();

                // Assuming gridControl1 is your GridControl and it is bound to the data source
                GridView gridView = gridWaitlisted.MainView as GridView;
                if (gridView == null) return;

                // Get the selected row's ID
                if (gridView.SelectedRowsCount > 0)
                {
                    int selectedRowHandle = gridView.GetSelectedRows()[0];
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRowHandle, "ID"));

                    // Prepare SQL command with parameter
                    using (MySqlCommand cmd0 = new MySqlCommand())
                    {
                        cmd0.Connection = con;
                        cmd0.CommandType = CommandType.Text;
                        cmd0.CommandText = @"SELECT 
                        d.LastName,
                        d.FirstName,
                        d.MiddleName,
                        d.ExtName,
                        d.PSGCRegion,
                        d.PSGCProvince,
                        d.PSGCCityMun,
                        d.PSGCBrgy,
                        d.StatusID,
                        lr.Region as Region,
                        lp.ProvinceName as ProvinceName,
                        lcm.CityMunName as Municipality,
                        lb.BrgyName as Barangay
                    FROM
                        tbl_masterlist d
                    LEFT JOIN
                        lib_region lr ON d.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN
                        lib_province lp ON d.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN
                        lib_city_municipality lcm ON d.PSGCCityMun = lcm.PSGCCityMun
                    LEFT JOIN
                        lib_barangay lb ON d.PSGCBrgy = lb.PSGCBrgy
                    WHERE
                        d.ID = @ID";

                        // Add parameters
                        cmd0.Parameters.AddWithValue("@ID", id);

                        DataTable dt0 = new DataTable();
                        using (MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0))
                        {
                            // Fill the DataTable
                            da0.Fill(dt0);
                        }

                        // Check if data is retrieved
                        if (dt0.Rows.Count > 0)
                        {
                            DataRow row = dt0.Rows[0];
                            // Concatenate and populate the fullname label
                            lbl_namereplace.Text = $"{row["LastName"]}, {row["FirstName"]} {row["MiddleName"]} {row["ExtName"]}";
                            // Concatenate and populate the address label
                            lbl_fromreplace.Text = $"{row["Region"]}, {row["ProvinceName"]}, {row["Municipality"]}, {row["Barangay"]}";
                        }
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            ClickedDelisted();
        }

        private void splitContainerControl1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            ClickedMasterlist();
        }

        private void searchControl2_QueryIsSearchColumn(object sender, QueryIsSearchColumnEventArgs args)
        {
            //Search only specific columns example FullName.
            if (args.FieldName != "FullName") //for FullName
                args.IsSearchColumn = false;
        }
        private void ReplaceUpdate()
        {
            try
            {
                //Below code is updating of masterlist status from waitlisted to active.
                con.Open();
                GridView gridView = gridWaitlisted.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView rowMasterlist = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int idFromMasterlistTable = Convert.ToInt32(rowMasterlist["ID"]); // ID is the masterlistid in tbl_masterlist
                                                                                  //int masterListIdFromMasterListTable = Convert.ToInt32(rowMasterlist["MasterlistID"]);

                //int masterlistID = Convert.ToInt32(row["MasterListID"]);
                int psgcRegionFromMasterlistTable = Convert.ToInt32(rowMasterlist["PSGCRegion"]);
                int psgcProvinceFromMasterlistTable = Convert.ToInt32(rowMasterlist["PSGCProvince"]);
                int psgcCityMunFromMasterlistTable = Convert.ToInt32(rowMasterlist["PSGCCityMun"]);
                int psgcBrgyFromMasterlistTable = Convert.ToInt32(rowMasterlist["PSGCBrgy"]);

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                // Perform the update
                cmd.CommandText = @"
            UPDATE 
                tbl_masterlist 
            SET
                StatusID = @StatusID,
                InclusionDate = @InclusionDate,
                RegTypeID = @RegTypeID
            WHERE 
                ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", idFromMasterlistTable);
                cmd.Parameters.AddWithValue("@StatusID", 1);
                cmd.Parameters.AddWithValue("@RegTypeID", 2);// CHange the regtype into replaced
                cmd.Parameters.AddWithValue("@InclusionDate", DateTime.Now);
                cmd.ExecuteNonQuery();


                //Below code is updating of tbl_delisted as replacement from waitlisted from tbl_masterlist.
                GridView gridViewDelisted = gridDelisted.MainView as GridView;
                DataRowView delistedRow = (DataRowView)gridViewDelisted.GetRow(gridViewDelisted.FocusedRowHandle);

                int idforReplacement = Convert.ToInt32(delistedRow["ID"]);
                int masterlistidforReplacement = Convert.ToInt32(delistedRow["PayrollID"]);//From tbl_delisted Joined from tbl_payroll_socpen
                int delistedMasterListID = Convert.ToInt32(delistedRow["MasterListID"]);
                int delistedPSGCRegion = Convert.ToInt32(delistedRow["PSGCRegion"]);
                int deslitedPSGCProvince = Convert.ToInt32(delistedRow["PSGCProvince"]);
                int delistedPSGCCityMun = Convert.ToInt32(delistedRow["PSGCCityMun"]);
                int deslistedPSGCBrgy = Convert.ToInt32(delistedRow["PSGCBrgy"]);


                MySqlCommand replaceCmd = con.CreateCommand();
                replaceCmd.CommandType = CommandType.Text;
                // Perform the update
                replaceCmd.CommandText = @"
            UPDATE 
                tbl_delisted 
            SET
                MasterlistID_Replacement = @MasterlistID_Replacement,
                PSGCRegion_Replacement = @PSGCRegion_Replacement,
                PSGCProvince_Replacement = @PSGCProvince_Replacement,
                PSGCCityMun_Replacement = @PSGCCityMun_Replacement,
                PSGCBrgy_Replacement = @PSGCBrgy_Replacement,
                ReplacedBy = @ReplacedBy,
                DateTimeReplaced = @DateTimeReplaced
            WHERE 
                ID = @ID";
                replaceCmd.Parameters.Clear();
                replaceCmd.Parameters.AddWithValue("@ID", idforReplacement);// this variable is based on the delisted

                replaceCmd.Parameters.AddWithValue("@MasterlistID_Replacement", idFromMasterlistTable); // This variables are based on the masterlist
                replaceCmd.Parameters.AddWithValue("@PSGCRegion_Replacement", psgcRegionFromMasterlistTable);
                replaceCmd.Parameters.AddWithValue("@PSGCProvince_Replacement", psgcProvinceFromMasterlistTable);
                replaceCmd.Parameters.AddWithValue("@PSGCCityMun_Replacement", psgcCityMunFromMasterlistTable);
                replaceCmd.Parameters.AddWithValue("@PSGCBrgy_Replacement", psgcBrgyFromMasterlistTable);

                replaceCmd.Parameters.AddWithValue("@ReplacedBy", _username);
                replaceCmd.Parameters.AddWithValue("@DateTimeReplaced", DateTime.Now);
                replaceCmd.ExecuteNonQuery();



                //Below code is updating of tbl_payroll_socpen from tbl_masterlist.

                MySqlCommand payrollCmd = con.CreateCommand();
                payrollCmd.CommandType = CommandType.Text;
                // Perform the update
                payrollCmd.CommandText = @"
            UPDATE 
                tbl_payroll_socpen 
            SET
                /*Masterlist here*/

                MasterListID = @fromWaitlistedReplacementForID,
                PSGCRegion = @fromWaitlistedPSGCRegion,
                PSGCProvince = @fromWaitlistedPSGCProvince,
                PSGCCityMun = @fromWaitlistedPSGCCityMun,
                PSGCBrgy = @fromWaitlistedPSGCBrgy,
                

                /*tbl_payroll_socpen table properties*/

                ReplacementForID = @fromDelistedReplacementForID,
                PSGCRegion_ReplacementFor = @fromDelistedPSGCRegion,
                PSGCProvince_ReplacementFor = @fromDelistedPSGCProvince,
                PSGCCityMun_ReplacementFor = @fromDelistedPSGCCityMun,
                PSGCBrgy_ReplacementFor = @fromDelistedPSGCBrgy,


                ReplacedBy = @ReplacedBy,
                DateTimeReplaced = @DateTimeReplaced
            WHERE 
                ID = @ID";
                payrollCmd.Parameters.Clear();
                payrollCmd.Parameters.AddWithValue("@ID", masterlistidforReplacement);// Nag base sa joined id from tbl_payroll_socpen. Currently the table is based from tbl_delisted but we might want to use this joined payrollID from tbl_payroll_socpen

                payrollCmd.Parameters.AddWithValue("@fromWaitlistedReplacementForID", idFromMasterlistTable); // This variables are based on the masterlist
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCRegion", psgcRegionFromMasterlistTable);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCProvince", psgcProvinceFromMasterlistTable);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCCityMun", psgcCityMunFromMasterlistTable);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCBrgy", psgcBrgyFromMasterlistTable);

                /*tbl_payroll_socpen table properties*/
                payrollCmd.Parameters.AddWithValue("@fromDelistedReplacementForID", delistedMasterListID); // This variables are based on the masterlist
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCRegion", delistedPSGCRegion);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCProvince", deslitedPSGCProvince);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCCityMun", delistedPSGCCityMun);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCBrgy", deslistedPSGCBrgy);

                payrollCmd.Parameters.AddWithValue("@ReplacedBy", _username);
                payrollCmd.Parameters.AddWithValue("@DateTimeReplaced", DateTime.Now);
                payrollCmd.ExecuteNonQuery();

                con.Close();
                XtraMessageBox.Show("Replaced Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async void btn_replacement_Click(object sender, EventArgs e)
        {
            GridView gridViewDelisted = gridDelisted.MainView as GridView;
            GridView gridViewWaitlisted = gridWaitlisted.MainView as GridView;
            if (gridViewDelisted.SelectedRowsCount == 0 || gridViewWaitlisted.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm

            }
            if (lbl_nametobe.Text == "-----" || lbl_namereplace.Text == "-----")
            {
                MessageBox.Show("Please select data you want to replace.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm

            }
            if (XtraMessageBox.Show($"Are you sure you want to proceed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ReplaceUpdate();
                EnableSpinner();//Enable the spinner
                await Delisted(); // Display delisted list tbl_delisted.
                await Waitlisted();
            }
        }

        private void ShowGISorSpbufWaitlisted()
        {
            if (Application.OpenForms.OfType<EditApplicant>().Any())
            {
                EditApplicantForm.Select();
                EditApplicantForm.BringToFront();
            }
            else
            {
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs
                var tableMasterList = Program.ServiceProvider.GetRequiredService<ITableMasterlist>(); //We called the DI lifecycle inside our Program.cs
                EditApplicantForm = new EditApplicant(masterlistForm, replacementsForm, _username, tableLog, tableMasterList);
                GridView gridView = gridWaitlisted.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to Edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                //EditApplicantForm.DisplayID(id);
                //EditApplicantForm.Show();
                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to view", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing GISForm
                }

                // Pass the ID value to the GISForm
                //DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                string gis = row["GIS"].ToString();
                string spbuf = row["SPBUF"].ToString();

                if (string.IsNullOrWhiteSpace(gis))
                {
                    if (string.IsNullOrWhiteSpace(spbuf))
                    {
                        //MessageBox.Show("Both GIS and SPBUF are missing.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // return; // Exit the method without showing GISForm
                    }
                    else
                    {
                        int spbufId = Convert.ToInt32(spbuf);
                        EditApplicantForm.DisplaySPBUF(spbufId);
                    }
                }
                else
                {
                    int gisId = Convert.ToInt32(gis);
                    EditApplicantForm.DisplayGIS(gisId);
                }
                EditApplicantForm.DisplayID(id);
                EditApplicantForm.ShowDialog();

            }
        }

        private void ShowGISorSpbuf()
        {
            if (Application.OpenForms.OfType<EditApplicant>().Any())
            {
                EditApplicantForm.Select();
                EditApplicantForm.BringToFront();
            }
            else
            {
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs
                var tableMasterList = Program.ServiceProvider.GetRequiredService<ITableMasterlist>(); //We called the DI lifecycle inside our Program.cs
                EditApplicantForm = new EditApplicant(masterlistForm, replacementsForm, _username, tableLog, tableMasterList);
                GridView gridView = gridDelisted.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to Edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["MasterListID"]);

                //EditApplicantForm.DisplayID(id);
                //EditApplicantForm.Show();

                //Below is to get the reference code under masterlist
                // Create a new instance of GISForm
                // GISviewingForm = new GISForm(this);
                // GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to view", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing GISForm
                }

                // Pass the ID value to the GISForm
                //DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                string gis = row["GIS"].ToString();
                string spbuf = row["SPBUF"].ToString();

                if (string.IsNullOrWhiteSpace(gis))
                {
                    if (string.IsNullOrWhiteSpace(spbuf))
                    {
                        //MessageBox.Show("Both GIS and SPBUF are missing.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // return; // Exit the method without showing GISForm
                    }
                    else
                    {
                        int spbufId = Convert.ToInt32(spbuf);
                        EditApplicantForm.DisplaySPBUF(spbufId);
                    }
                }
                else
                {
                    int gisId = Convert.ToInt32(gis);
                    EditApplicantForm.DisplayGIS(gisId);
                }

                EditApplicantForm.DisplayID(id);
                EditApplicantForm.ShowDialog();

            }
        }

        private void gridDelisted_DoubleClick(object sender, EventArgs e)
        {
            ShowGISorSpbuf();
        }

        private void gridWaitlisted_DoubleClick(object sender, EventArgs e)
        {
            ShowGISorSpbufWaitlisted();
        }

        private void UndoReplacementUpdate()
        {
            try
            {
                //Below code is updating of masterlist status from waitlisted to active.
                con.Open();
                GridView gridViewDelisted = gridDelisted.MainView as GridView;
                DataRowView delistedRow = (DataRowView)gridViewDelisted.GetRow(gridViewDelisted.FocusedRowHandle);
                // int idforUndo = Convert.ToInt32(delistedRow["ReplacementForID"]);
                int MasterlistIdDelistedRow = Convert.ToInt32(delistedRow["MasterListID_Replacement"]);
                //int delistedMasterListID = Convert.ToInt32(delistedRow["MasterListID"]);
                //int delistedPSGCRegion = Convert.ToInt32(delistedRow["PSGCRegion_ReplacementFor"]);
                //int deslitedPSGCProvince = Convert.ToInt32(delistedRow["PSGCProvince_ReplacementFor"]);
                //int delistedPSGCCityMun = Convert.ToInt32(delistedRow["PSGCCityMun_ReplacementFor"]);
                //int deslistedPSGCBrgy = Convert.ToInt32(delistedRow["PSGCBrgy_ReplacementFor"]);

                int idforReplacement = Convert.ToInt32(delistedRow["ReplacementsPayrollID"]);//Masterlist ID from tbl_delisted because tbl_payroll_socpen is empty.
                                                                                             // int yearFilterForReplacement = Convert.ToInt32(delistedRow["PayrollYear"]);//Year from tbl_delisted because tbl_payroll_socpen is empty.
                                                                                             //int periodIDFilterForReplacement = Convert.ToInt32(delistedRow["PayrollPeriodID"]);//PeriodID from tbl_delisted because tbl_payroll_socpen is empty.

                int idforDelisted = Convert.ToInt32(delistedRow["ID"]);


                GridView gridView = gridDelisted.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView rowDelisted = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(rowDelisted["MasterListID"]); // ID is the masterlistid in tbl_masterlist
                                                                       //int masterlistID = Convert.ToInt32(row["MasterlistID"]);

                //int masterlistID = Convert.ToInt32(row["MasterListID"]);
                int psgcRegion = Convert.ToInt32(rowDelisted["PSGCRegion"]);
                int psgcProvince = Convert.ToInt32(rowDelisted["PSGCProvince"]);
                int psgcCityMun = Convert.ToInt32(rowDelisted["PSGCCityMun"]);
                int psgcBrgy = Convert.ToInt32(rowDelisted["PSGCBrgy"]);

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                // Perform the update
                cmd.CommandText = @"
            UPDATE 
                tbl_masterlist 
            SET
                StatusID = @StatusID,
                InclusionDate = @InclusionDate,
                RegTypeID = @RegTypeID
            WHERE 
                ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", MasterlistIdDelistedRow);//MasterlistID from tbl_delisted is based on our ID into tbl_masterlist
                cmd.Parameters.AddWithValue("@StatusID", 99);
                cmd.Parameters.AddWithValue("@RegTypeID", 1);// CHange the regtype back to addtional / new
                cmd.Parameters.AddWithValue("@InclusionDate", null);
                cmd.ExecuteNonQuery();

                MySqlCommand replaceCmd = con.CreateCommand();
                replaceCmd.CommandType = CommandType.Text;
                // Perform the update
                replaceCmd.CommandText = @"
            UPDATE 
                tbl_delisted 
            SET
                MasterlistID_Replacement = @MasterlistID_Replacement,
                PSGCRegion_Replacement = @PSGCRegion_Replacement,
                PSGCProvince_Replacement = @PSGCProvince_Replacement,
                PSGCCityMun_Replacement = @PSGCCityMun_Replacement,
                PSGCBrgy_Replacement = @PSGCBrgy_Replacement,
                ReplacedBy = @ReplacedBy,
                DateTimeReplaced = @DateTimeReplaced
            WHERE 
                ID = @ID";
                replaceCmd.Parameters.Clear();
                replaceCmd.Parameters.AddWithValue("@ID", idforDelisted);// this variable is based on the delisted
                replaceCmd.Parameters.AddWithValue("@MasterlistID_Replacement", null); // This variables are based on the masterlist
                replaceCmd.Parameters.AddWithValue("@PSGCRegion_Replacement", null);
                replaceCmd.Parameters.AddWithValue("@PSGCProvince_Replacement", null);
                replaceCmd.Parameters.AddWithValue("@PSGCCityMun_Replacement", null);
                replaceCmd.Parameters.AddWithValue("@PSGCBrgy_Replacement", null);
                replaceCmd.Parameters.AddWithValue("@ReplacedBy", null);
                replaceCmd.Parameters.AddWithValue("@DateTimeReplaced", null);
                replaceCmd.ExecuteNonQuery();

                //Below code is updating of tbl_payroll_socpen from tbl_masterlist.
                MySqlCommand payrollCmd = con.CreateCommand();
                payrollCmd.CommandType = CommandType.Text;
                // Perform the update
                payrollCmd.CommandText = @"
            UPDATE 
                tbl_payroll_socpen 
            SET
                /*tbl_payroll_socpen primary MasterListID here*/

                MasterListID = @fromDelistedReplacementForID,
                PSGCRegion = @fromDelistedPSGCRegion,
                PSGCProvince = @fromDelistedPSGCProvince,
                PSGCCityMun = @fromDelistedPSGCCityMun,
                PSGCBrgy = @fromDelistedPSGCBrgy,
                

                /*tbl_payroll_socpen table properties*/

                ReplacementForID = @fromWaitlistedReplacementForID,
                PSGCRegion_ReplacementFor = @fromWaitlistedPSGCRegion,
                PSGCProvince_ReplacementFor =  @fromWaitlistedPSGCProvince,
                PSGCCityMun_ReplacementFor = @fromWaitlistedPSGCCityMun,
                PSGCBrgy_ReplacementFor = @fromWaitlistedPSGCBrgy,


                ReplacedBy = @ReplacedBy,
                DateTimeReplaced = @DateTimeReplaced
            WHERE 
                ID = @MasterListIDFromDelisted";
                payrollCmd.Parameters.Clear();

                payrollCmd.Parameters.AddWithValue("@MasterListIDFromDelisted", idforReplacement);// This is based on our table payroll socpen there are 2 joins in tbl_delisted.

                payrollCmd.Parameters.AddWithValue("@fromWaitlistedReplacementForID", null); // Return null if undo replaced
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCRegion", null);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCProvince", null);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCCityMun", null);
                payrollCmd.Parameters.AddWithValue("@fromWaitlistedPSGCBrgy", null);

                /*tbl_delisted table properties*/
                payrollCmd.Parameters.AddWithValue("@fromDelistedReplacementForID", id); // This variables are based on the masterlist
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCRegion", psgcRegion);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCProvince", psgcProvince);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCCityMun", psgcCityMun);
                payrollCmd.Parameters.AddWithValue("@fromDelistedPSGCBrgy", psgcBrgy);

                payrollCmd.Parameters.AddWithValue("@ReplacedBy", null);
                payrollCmd.Parameters.AddWithValue("@DateTimeReplaced", null);
                payrollCmd.ExecuteNonQuery();

                con.Close();
                XtraMessageBox.Show("Data undo was Successful", "Undo Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btn_Undo_Click(object sender, EventArgs e)
        {
            GridView gridViewDelisted = gridDelisted.MainView as GridView;
            if (gridViewDelisted.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm

            }
            if (lbl_nametobe.Text == "-----")
            {
                MessageBox.Show("Please select data you want to undo.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm

            }
            if (XtraMessageBox.Show($"Are you sure you want to proceed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UndoReplacementUpdate();
                EnableSpinner();//Enable the spinner
                await Delisted(); // Display delisted list tbl_delisted.
                await Waitlisted();
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cmb_municipality.Text == "Select City/Municipality" || cmb_year.Text == "Select Year" || cmb_period.Text == "Select Period")
            {
                XtraMessageBox.Show("Please fill all the fields before searching.","Fill",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            Search();
        }
    }
}
