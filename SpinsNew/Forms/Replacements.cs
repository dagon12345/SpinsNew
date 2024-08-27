using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
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
        public Replacements(string username)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            _username = username;
            // gridView1.FocusedRowChanged += gridView_FocusedRowChanged;
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }


        private void Replacements_Load(object sender, EventArgs e)
        {
            Municipality();
            Year();


            // Cast the MainView to GridView
            GridView gridView = gridDelisted.MainView as GridView;

            //if (gridView != null)
            //{
            //    gridView.RowStyle += gridView_RowStyle;
            //    // Subscribe to the CustomDrawFooterCell event
            //    gridView.CustomDrawFooterCell += GridView_CustomDrawFooterCell;
            //}

            //Integrate search control into our grid control.
            searchControl1.Client = gridDelisted;
            searchControl2.Client = gridWaitlisted;
        }
        // Custom class to store Id and DataSource
        public class YearItem
        {
            public int Id { get; set; }
            public int Year { get; set; }
            public double MonthlyStipened { get; set; }


            public override string ToString()
            {
                return Year.ToString();
            }

        }
        //Fill combobox reportsource
        public void Year()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Year FROM lib_year WHERE Active = 1"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_year.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {

                    // Add DataSourceItem to the ComboBox
                    cmb_year.Properties.Items.Add(new YearItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Year = Convert.ToInt32(dr["Year"])
                    });
                }
                con.Close();

                // Add the event handler for the SelectedIndexChanged event
                cmb_year.SelectedIndexChanged -= cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
                cmb_year.SelectedIndexChanged += cmb_year_SelectedIndexChanged;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public class PeriodItem
        {
            public int PeriodID { get; set; }
            public string Period { get; set; }
            public string Abbreviation { get; set; }
            public string Months { get; set; }

            public override string ToString()
            {
                return $"{Period} ({Abbreviation}) {Months}"; // Display Period and Abbreviation in the ComboBox
            }
        }
        // Load periods for the selected year
        private void LoadPeriodsForYear(int year)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PeriodID, Period, Abbreviation, Months FROM lib_period WHERE FIND_IN_SET(@Year, REPLACE(YearsUsed, ' ', ''))"; // Use parameterized query and remove spaces
                cmd.Parameters.AddWithValue("@Year", year.ToString());
                //MessageBox.Show($"Executing query with year: {year}");
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_period.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add PeriodItem to the ComboBox
                    cmb_period.Properties.Items.Add(new PeriodItem
                    {
                        PeriodID = Convert.ToInt32(dr["PeriodID"]),
                        Period = dr["Period"].ToString(),
                        Abbreviation = dr["Abbreviation"].ToString(),
                        Months = dr["Months"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        //{
        //    //Color the row red if the age is 59 and under.
        //    GridView view = sender as GridView;
        //    if (e.RowHandle >= 0)
        //    {
        //        // Get the value of the "Age" column
        //        object ageValue = view.GetRowCellValue(e.RowHandle, view.Columns["Age"]);

        //        // Check for DBNull and convert to int if not null
        //        if (ageValue != DBNull.Value)
        //        {
        //            int age = Convert.ToInt32(ageValue);

        //            if (age < 60)
        //            {
        //                // Use ColorTranslator to convert hex color code to Color object
        //                e.Appearance.BackColor = ColorTranslator.FromHtml("#FA7070");
        //            }
        //        }
        //    }
        //}

        // Event handler for CustomDrawFooterCell
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
        //Fill the combobox
        public void Municipality()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
            SELECT 
                m.PSGCCityMun, 
                m.CityMunName, 
                m.PSGCProvince, 
                p.ProvinceName 
            FROM 
                lib_city_municipality m
                INNER JOIN lib_province p ON m.PSGCProvince = p.PSGCProvince
                ORDER BY ProvinceName"; // Join with lib_province to get ProvinceName
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_municipality.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_municipality.Properties.Items.Add(new MunicipalityItem
                    {
                        PSGCCityMun = Convert.ToInt32(dr["PSGCCityMun"]),
                        CityMunName = dr["CityMunName"].ToString(),
                        PSGCProvince = Convert.ToInt32(dr["PSGCProvince"]),
                        ProvinceName = dr["ProvinceName"].ToString() // Populate the new property
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        }
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
           // btn_search.Enabled = true; //Enable textbox once gridview was loaded successfully
                                       // btn_refresh.Enabled = true;
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
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
                d.ID,
                d.MasterListID,

                tm.LastName,
                tm.FirstName,
                tm.MiddleName,
                tm.ExtName,
                lb.BrgyName as Barangay,
                tm.BirthDate,
                ls.Status as Reason,
                d.StatusRemarks,
                tm2.LastName as LastName2,
                tm2.FirstName as FirstName2,
                tm2.MiddleName as MiddleName2,
                tm2.ExtName as ExtName2,
                lb2.BrgyName as FromBarangay,
                tm2.Birthdate as ReplacementBirthdate,
                lpr.Period as Period,
                d.Year,
                lps.ReportSource as ReportSource,
                d.DelistedBy,
                d.DateTimeDelisted,
                d.ReplacedBy,
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
                dt.Columns["Name of Replacement"].SetOrdinal(13);



                //We are using DevExpress datagridview
                gridDelisted.DataSource = dt;

                // Get the GridView instance
                // GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Hide the "ID" column
                    gridView.Columns["ID"].Visible = false;
                    gridView.Columns["MasterListID"].Visible = false;
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
                    gridView.Columns["PayrollID"].Visible = false;
                    // Freeze the columns
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["Unclaimed Payrolls"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;

                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                }
                // Update row count display
                UpdateRowCount(gridView);
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

        private void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is YearItem selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                LoadPeriodsForYear(selectedYear.Year);
            }
            Search();

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
        private async void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private async void btn_search_Click(object sender, EventArgs e)
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
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]); // ID is the masterlistid in tbl_masterlist
                //int masterlistID = Convert.ToInt32(row["MasterlistID"]);

                //int masterlistID = Convert.ToInt32(row["MasterListID"]);
                int psgcRegion = Convert.ToInt32(row["PSGCRegion"]);
                int psgcProvince = Convert.ToInt32(row["PSGCProvince"]);
                int psgcCityMun = Convert.ToInt32(row["PSGCCityMun"]);
                int psgcBrgy = Convert.ToInt32(row["PSGCBrgy"]);

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
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@StatusID", 1);
                    cmd.Parameters.AddWithValue("@RegTypeID", 2);// CHange the regtype into replaced
                    cmd.Parameters.AddWithValue("@InclusionDate", DateTime.Now);
                    cmd.ExecuteNonQuery();


                //Below code is updating of tbl_delisted as replacement from waitlisted from tbl_masterlist.
                GridView gridViewDelisted = gridDelisted.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView delistedRow = (DataRowView)gridViewDelisted.GetRow(gridViewDelisted.FocusedRowHandle);
                int idforReplacement = Convert.ToInt32(delistedRow["ID"]);

                int masterlistidforReplacement = Convert.ToInt32(delistedRow["PayrollID"]);

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

                replaceCmd.Parameters.AddWithValue("@MasterlistID_Replacement", id); // This variables are based on the masterlist
                replaceCmd.Parameters.AddWithValue("@PSGCRegion_Replacement", psgcRegion);
                replaceCmd.Parameters.AddWithValue("@PSGCProvince_Replacement", psgcProvince);
                replaceCmd.Parameters.AddWithValue("@PSGCCityMun_Replacement", psgcCityMun);
                replaceCmd.Parameters.AddWithValue("@PSGCBrgy_Replacement", psgcBrgy);

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
                ReplacementForID = @ReplacementForID,
                PSGCRegion_ReplacementFor = @PSGCRegion_ReplacementFor,
                PSGCProvince_ReplacementFor = @PSGCProvince_ReplacementFor,
                PSGCCityMun_ReplacementFor = @PSGCCityMun_ReplacementFor,
                PSGCBrgy_ReplacementFor = @PSGCBrgy_ReplacementFor,
                ReplacedBy = @ReplacedBy,
                DateTimeReplaced = @DateTimeReplaced
            WHERE 
                ID = @PayrollID";
                payrollCmd.Parameters.Clear();
                payrollCmd.Parameters.AddWithValue("@PayrollID", masterlistidforReplacement);// this variable is based on the tbl_masterlist

                payrollCmd.Parameters.AddWithValue("@ReplacementForID", id); // This variables are based on the masterlist
                payrollCmd.Parameters.AddWithValue("@PSGCRegion_ReplacementFor", psgcRegion);
                payrollCmd.Parameters.AddWithValue("@PSGCProvince_ReplacementFor", psgcProvince);
                payrollCmd.Parameters.AddWithValue("@PSGCCityMun_ReplacementFor", psgcCityMun);
                payrollCmd.Parameters.AddWithValue("@PSGCBrgy_ReplacementFor", psgcBrgy);

                payrollCmd.Parameters.AddWithValue("@ReplacedBy", Environment.UserName);
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
                EditApplicantForm = new EditApplicant(masterlistForm, replacementsForm);
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
                EditApplicantForm = new EditApplicant(masterlistForm, replacementsForm);
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
    }
}
