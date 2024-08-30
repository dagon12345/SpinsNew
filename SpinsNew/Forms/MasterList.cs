using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Forms;
using SpinsNew.Popups;
using SpinsWinforms.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew
{
    public partial class MasterList : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        public string _userRole;
        private Replacements replacementsForm;
        private MasterList masterlistForm;
        //private ApplicationDbContext _dbContext;
        //private MasterList masterlistForm;// Call MasterList form
        public MasterList(string username, string userRole)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            gridView1.FocusedRowChanged += gridView_FocusedRowChanged;

            // masterlistForm = masterlist;// Execute the MasterListform.
            // Set the shortcut key for viewToolStripMenuItem
            viewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            // Set the shortcut key for viewToolStripMenuItem
            delistToolStripMenuItem.ShortcutKeys = Keys.Delete;
            attachmentsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            // Optionally, handle the click event if not already handled
            viewToolStripMenuItem.Click += viewToolStripMenuItem_Click;

            // Subscribe to the FilterExpressionChanged event
            GridView gridView = (GridView)gridControl1.MainView;
            gridView.ColumnFilterChanged += gridView1_ColumnFilterChanged;

            _username = username; // Retrieve the username
            _userRole = userRole;

            if(userRole == "3")// Number 3 is the encoders
            {
                payrollToolStripMenuItem.Visible = false;
                verificationToolStripMenuItem.Visible = false;
                actionsToolStripMenuItem.Visible = false;
            }


        }

        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;

            // Get the count of rows that match the current filter
            int rowCount = gridView.DataRowCount;

            // Update your control with the row count
            UpdateRowCount(gridView);
        }



        private void MasterList_Load(object sender, EventArgs e)
        {
            //ComboboxMunicipality();
            Municipality();
            groupControl1.Text = "Count of showed data: [0]";
            // Cast the MainView to GridView
            GridView gridView = gridControl1.MainView as GridView;

            if (gridView != null)
            {
                gridView.RowStyle += gridView_RowStyle;
                // Subscribe to the CustomDrawFooterCell event
                gridView.CustomDrawFooterCell += GridView_CustomDrawFooterCell;
            }

            gridView1.CustomDrawCell += gridView1_CustomDrawCell;

            //Integrate search control into our grid control.
            searchControl1.Client = gridControl1;



        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //// Set the background color to black for all cells
            //e.Appearance.BackColor = Color.Black;

            //// Optionally, change the text color to white for visibility
            //e.Appearance.ForeColor = Color.White;
        }


        public void Municipality()
        {
            try
            {
                // Fetch data from the database and bind to CheckedComboBoxEdit
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
                INNER JOIN
                    lib_province p ON m.PSGCProvince = p.PSGCProvince
            ORDER BY 
                ProvinceName,
                m.CityMunName
        "; // Join with lib_province to get ProvinceName
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the CheckedComboBoxEdit
                cmb_municipality.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add each municipality to the CheckedComboBoxEdit with a checkbox
                    cmb_municipality.Properties.Items.Add(new CheckedListBoxItem(
                        value: Convert.ToInt32(dr["PSGCCityMun"]), // The value to store (usually the ID)
                        description: dr["CityMunName"].ToString() + " " + dr["ProvinceName"].ToString(), // The display text
                        checkState: CheckState.Unchecked // Initially unchecked
                    ));
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }


        // Event handler for CustomDrawFooterCell
        private void GridView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            // Assuming gridControl1 is your DevExpress GridControl instance
            GridView gridView = gridControl1.MainView as GridView;

            if (e.Column == null) // Assuming you want to show this in the first footer cell
            {
                // Calculate the row count
                int rowCount = gridView.RowCount;

                // Format the display text
                e.Info.DisplayText = $"Total Rows: {rowCount}";
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            //Color the row red if the age is 59 and under.
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                // Get the value of the "Age" column
                object ageValue = view.GetRowCellValue(e.RowHandle, view.Columns["Age"]);

                // Check for DBNull and convert to int if not null
                if (ageValue != DBNull.Value)
                {
                    int age = Convert.ToInt32(ageValue);

                    if (age < 60)
                    {
                        // Use ColorTranslator to convert hex color code to Color object
                        e.Appearance.BackColor = ColorTranslator.FromHtml("#FA7070");
                    }
                }
            }
        }


        //Load masterlist below
        private async void LoadDataAsync()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                SELECT 
                m.ID,
                m.LastName,
                m.FirstName,
                m.MiddleName,
                m.ExtName,
                tg.ReferenceCode as LatestValidation,
                tg.SPISBatch as SpisBatch,
                ls.Status as Status,
                m.Citizenship,
                m.MothersMaiden,
                lr.Region as Region,
                lp.ProvinceName as Province,
                lc.CityMunName as Municipality,
                lb.BrgyName as Barangay,
                m.Address,
                m.BirthDate,
                TIMESTAMPDIFF(YEAR, m.BirthDate, CURDATE()) AS Age,
                s.Sex as Sex,
                ms.MaritalStatus as MaritalStatus,
                m.Religion,
                m.BirthPlace,
                m.EducAttain,
                it.Type as IDType,
                m.IDNumber,
                m.IDDateIssued,
                m.Pantawid,
                m.Indigenous,
                m.SocialPensionID,
                m.HouseholdID,
                m.IndigenousID,
                m.ContactNum,
                lh.HealthStatus as HealthStatus,
                m.HealthStatusRemarks,
                m.DateTimeEntry,
                m.EntryBy,
                ld.DataSource as DataSource,
                m.Remarks,
                lrt.RegType as RegistrationType,
                m.InclusionBatch,
                m.InclusionDate,
                m.ExclusionBatch,
                m.ExclusionDate,
                m.DateDeceased,
                m.DateTimeModified,
                m.ModifiedBy,
                m.DateTimeDeleted,
                m.DeletedBy,
                m.WaitlistedReportID,
                m.WithPhoto
            FROM 
                tbl_masterlist m
            LEFT JOIN 
                tbl_gis tg ON m.ID = tg.MasterlistID
            LEFT JOIN 
                lib_region lr ON m.PSGCRegion = lr.PSGCRegion
            LEFT JOIN 
                lib_province lp ON m.PSGCProvince = lp.PSGCProvince
            LEFT JOIN 
                lib_city_municipality lc ON m.PSGCCityMun = lc.PSGCCityMun
            LEFT JOIN 
                lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
            LEFT JOIN 
                lib_sex s ON m.SexID = s.ID
            LEFT JOIN 
                lib_marital_status ms ON m.MaritalStatusID = ms.ID
            LEFT JOIN 
                lib_id_type it ON m.IDtypeID = it.ID
            LEFT JOIN 
                lib_health_status lh ON m.HealthStatusID = lh.ID
            LEFT JOIN 
                lib_datasource ld ON m.DataSourceID = ld.ID
            LEFT JOIN 
                lib_status ls ON m.StatusID = ls.ID
            LEFT JOIN 
                lib_registration_type lrt ON m.RegtypeID = lrt.ID";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //Await to reduce lag while loading large amount of datas
                await Task.Run(() => da.Fill(dt));
                //We are using DevExpress datagridview
                gridControl1.DataSource = dt;

                // Get the GridView instance
                GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Hide the "ID" column
                    gridView.Columns["ID"].Visible = false;

                    // Freeze the columns
                    gridView.Columns["LastName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["FirstName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["MiddleName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["ExtName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;

                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;
                }


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Fill combobox with municipality
        public void ComboboxMunicipality()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
            SELECT
                lcm.PSGCCityMun,
                lcm.CityMunName,
                lr.ProvinceName as Province
            FROM 
                lib_city_municipality lcm
            LEFT JOIN 
                lib_province lr ON lcm.PSGCProvince = lr.PSGCProvince";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_municipality.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add both ID and Name to the ComboBox
                    cmb_municipality.Properties.Items.Add(new { PSGCCityMun = dr["PSGCCityMun"], Province = dr["Province"], Name = dr["CityMunName"] });
                }

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

        //Status if combobox is needed
        public void ComboboxStatus()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM lib_status";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    cmb_status.Properties.Items.Add(dr["Status"].ToString());

                }
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

        public async Task AllMunicipalities()
        {
            try
            {
                string statusFilter = cmb_status.Text;
                //bool allMunicipalityChecked = ck_all.Checked; // if checked was true
                string municipalitiesFilter = cmb_municipality.Text; // if checked was true

                GridView gridView = gridControl1.MainView as GridView; // Assuming gridView is your GridView instance associated with gridControl1
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string query = @"SELECT 
                    m.IsVerified as Verified,
                    m.ID,
                    IFNULL(tat.AttachmentNames, 'None') AS AttachmentNames,
                    m.LastName,
                    m.FirstName,
                    m.MiddleName,
                    m.ExtName,
                
                    /*tg_max.ID as GISID,*/
                    m.StatusID as StatusID,
                
                    la.Assessment as Assessment,
                
                    tg_max.ReferenceCode as GIS,
                    ts_max.ReferenceCode as SPBUF,
                    tg_max.SPISBatch as SpisBatch,
                    tg_max.AssessmentID as AssessmentID,
                    ls.Status as Status,
                    m.Citizenship as Citizenship,
                    m.MothersMaiden as MothersMaiden,

                    m.PSGCRegion,
                    m.PSGCProvince,
                    m.PSGCCityMun,
                    m.PSGCBrgy,

                    lr.Region as Region,
                    lp.ProvinceName as Province,
                    lc.CityMunName as Municipality,
                    lb.BrgyName as Barangay,
                    m.Address as Address,
                    m.BirthDate as BirthDate,
                    TIMESTAMPDIFF(YEAR, m.BirthDate, CURDATE()) AS Age,
                    s.Sex as Sex,
                    ms.MaritalStatus as MaritalStatus,
                    m.Religion as Religion,
                    m.BirthPlace as BirthPlace,
                    m.EducAttain as EducAttain,
                    it.Type as IDType,
                    m.IDNumber as IDNumber,
                    m.IDDateIssued as IDDateIssued,
                    m.Pantawid as Pantawid,
                    m.Indigenous as Indigenous,
                    m.SocialPensionID as SocialPensionID,
                    m.HouseholdID as HouseholdID,
                    m.IndigenousID as IndigenousID,
                    m.ContactNum as ContactNum,
                    lh.HealthStatus as HealthStatus,
                    m.HealthStatusRemarks as HealthStatusRemarks,
                    m.DateTimeEntry as DateTimeEntry,
                    m.EntryBy as EntryBy,
                    ld.DataSource as DataSource,
                    m.Remarks as Remarks,
                    lrt.RegType as RegistrationType,
                    m.InclusionBatch as InclusionBatch,
                    m.InclusionDate as InclusionDate,
                    m.ExclusionBatch as ExclusionBatch,
                    m.ExclusionDate as ExclusionDate,
                    m.DateDeceased as DateDeceased,
                    m.DateTimeModified as DateTimeModified,
                    m.ModifiedBy as ModifiedBy,
                    m.DateTimeDeleted as DateTimeDeleted,
                    tg_max.ValidationDate

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
                    lib_sex s ON m.SexID = s.ID
                LEFT JOIN 
                    lib_marital_status ms ON m.MaritalStatusID = ms.ID
                LEFT JOIN 
                    lib_id_type it ON m.IDtypeID = it.ID
                LEFT JOIN 
                    lib_health_status lh ON m.HealthStatusID = lh.ID
                LEFT JOIN 
                    lib_datasource ld ON m.DataSourceID = ld.ID
                LEFT JOIN 
                    lib_status ls ON m.StatusID = ls.ID
                LEFT JOIN 
                    lib_registration_type lrt ON m.RegtypeID = lrt.ID
                LEFT JOIN 
                    lib_assessment la ON tg_max.AssessmentID = la.ID
                LEFT JOIN 
                    (SELECT MasterListID, GROUP_CONCAT(AttachmentName ORDER BY AttachmentName SEPARATOR ', ') AS AttachmentNames 
                     FROM tbl_attachments 
                     GROUP BY MasterListID) tat ON m.ID = tat.MasterListID
                WHERE 
                    m.DateTimeDeleted IS NULL";

                /*Filter All Municipalities*/


                    if (statusFilter == "All Statuses")
                    {
                        // Construct a filter for selected municipalities
                        // Using GetCheckedValues to get the selected items' values.
                        var checkedItems = cmb_municipality.Properties.GetCheckedItems();

                        // Convert the checked items to a string array
                        var municipalitiesArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if there are any selected municipalities
                        if (municipalitiesArray.Length > 0)
                        {
                            // Join the selected municipality values into a single comma-separated string
                            string municipalitiesList = string.Join(",", municipalitiesArray.Select(m => $"'{m.Trim()}'"));

                            // Append the condition to the query
                            query += $" AND m.PSGCCityMun IN ({municipalitiesList})";
                        }

                    }
                    if (statusFilter == "Active" || statusFilter == "Applicant")
                    {
                        // Construct a filter for selected municipalities
                        // Using GetCheckedValues to get the selected items' values.
                        var checkedItems = cmb_municipality.Properties.GetCheckedItems();

                        // Convert the checked items to a string array
                        var municipalitiesArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if there are any selected municipalities
                        if (municipalitiesArray.Length > 0)
                        {
                            // Join the selected municipality values into a single comma-separated string
                            string municipalitiesList = string.Join(",", municipalitiesArray.Select(m => $"'{m.Trim()}'"));

                            // Append the condition to the query
                            query += $" AND m.PSGCCityMun IN ({municipalitiesList})";
                        }
                        cmd.Parameters.AddWithValue("@Status", statusFilter);
                        if (statusFilter != "All Statuses")
                        {
                            query += "  AND ls.Status = @Status";
                        }
                 
                    }
                    if (statusFilter == "Waitlisted")
                    {
                        // Construct a filter for selected municipalities
                        // Using GetCheckedValues to get the selected items' values.
                        var checkedItems = cmb_municipality.Properties.GetCheckedItems();

                        // Convert the checked items to a string array
                        var municipalitiesArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if there are any selected municipalities
                        if (municipalitiesArray.Length > 0)
                        {
                            // Join the selected municipality values into a single comma-separated string
                            string municipalitiesList = string.Join(",", municipalitiesArray.Select(m => $"'{m.Trim()}'"));

                            // Append the condition to the query
                            query += $" AND m.PSGCCityMun IN ({municipalitiesList})";
                        }

                        if (statusFilter != "All Statuses")
                        {
                            query += @" AND tg_max.ReferenceCode IS NOT NULL
                                        AND tg_max.SPISBatch IS NOT NULL
                                        AND m.StatusID = 99
                                        AND la.ID = 1";
                        }

                    }
                    if (statusFilter == "Delisted")
                    {
                        // Construct a filter for selected municipalities
                        // Using GetCheckedValues to get the selected items' values.
                        var checkedItems = cmb_municipality.Properties.GetCheckedItems();

                        // Convert the checked items to a string array
                        var municipalitiesArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if there are any selected municipalities
                        if (municipalitiesArray.Length > 0)
                        {
                            // Join the selected municipality values into a single comma-separated string
                            string municipalitiesList = string.Join(",", municipalitiesArray.Select(m => $"'{m.Trim()}'"));

                            // Append the condition to the query
                            query += $" AND m.PSGCCityMun IN ({municipalitiesList})";
                        }

                        if (statusFilter != "All Statuses")
                        {
                            query += " AND m.StatusID BETWEEN 2 AND 15";
                        }

                    }
                query += @"
                       GROUP BY
                           m.ID, 
                           m.LastName,
                           m.FirstName,
                           m.MiddleName,
                           m.ExtName,
                           tg_max.ID
                       ORDER BY
                            lc.CityMunName";

                cmd.CommandText = query;


                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Configure the progress bar
                progressBarControl1.Properties.Maximum = 100;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.EditValue = 0;
                //Create a task to fill the DataTable and update the progress bar
                await Task.Run(async () =>
                {
                    da.Fill(dt);
                    for (int i = 0; i <= 100; i += 10)
                    {
                        // Simulate progress (this is just for demonstration; adjust as necessary)
                        System.Threading.Thread.Sleep(50); // Simulate a delay
                        this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                    }
                });
                // Add a new column for concatenated Status and DateDeceased
                dt.Columns.Add("StatusCurrent", typeof(string));

                // Populate the new column with concatenated values
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Status"].ToString();
                    string dateDeceased = row["DateDeceased"]?.ToString();
                    string remarks = row["Remarks"]?.ToString();
                    int assessment = row["AssessmentID"] != DBNull.Value ? Convert.ToInt32(row["AssessmentID"]) : 0;
                    int spisbatch = row["SpisBatch"] != DBNull.Value ? Convert.ToInt32(row["SpisBatch"]) : 0;
                    int statusID = Convert.ToInt32(row["StatusID"]); // Get the StatusID
                    string gis = row["GIS"]?.ToString();
                    string spbuf = row["SPBUF"]?.ToString();
                    //row["StatusCurrent"] = !string.IsNullOrEmpty(dateDeceased) ? $"{status} ({remarks}) [{dateDeceased}]" : status;

                    // Check if Assessment is 1 (Eligible) and either GIS or SPBUF is not null
                    if (assessment == 1 && statusID == 99 && spisbatch != 0 && (!string.IsNullOrEmpty(gis) || !string.IsNullOrEmpty(spbuf)))
                    {
                        status = "Waitlisted";
                    }

                    if (!string.IsNullOrEmpty(dateDeceased))
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["StatusCurrent"] = $"{status} ({remarks}) [{dateDeceased}]";
                        }
                        else
                        {
                            row["StatusCurrent"] = $"{status} [{dateDeceased}]";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["StatusCurrent"] = $"{status} ({remarks})";
                        }
                        else
                        {
                            row["StatusCurrent"] = status;
                        }
                    }
                }
                // Move the new column to the 6th position
                dt.Columns["StatusCurrent"].SetOrdinal(9);
                                
                        //Sample integrations
                //We are using DevExpress datagridview
                gridControl1.DataSource = dt;

                // Get the GridView instance
                // GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Move the "Verified" column to the first position
                    gridView.Columns["Verified"].VisibleIndex = 0;
                    // Hide the columns if they exist
                    HideColumn(gridView, "ID");
                    HideColumn(gridView, "DateTimeDeleted");
                    HideColumn(gridView, "Status");
                    HideColumn(gridView, "DateDeceased");
                    HideColumn(gridView, "Remarks");
                    HideColumn(gridView, "StatusID");
                    HideColumn(gridView, "AssessmentID");
                    //Hide Region, Province, Municipality, and Barangay
                    HideColumn(gridView, "PSGCRegion");
                    HideColumn(gridView, "PSGCProvince");
                    HideColumn(gridView, "PSGCCityMun");
                    HideColumn(gridView, "PSGCBrgy");
                    //HideColumn(gridView, "PSGCRegion");
                    //HideColumn(gridView, "PSGCProvince");
                    //HideColumn(gridView, "PSGCCityMun");
                    //HideColumn(gridView, "PSGCBrgy");

                    // Freeze the columns if they exist
                    if (gridView.Columns.ColumnByFieldName("Verified") != null)
                        gridView.Columns["Verified"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    if (gridView.Columns.ColumnByFieldName("LastName") != null)
                        gridView.Columns["LastName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    if (gridView.Columns.ColumnByFieldName("FirstName") != null)
                        gridView.Columns["FirstName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    if (gridView.Columns.ColumnByFieldName("MiddleName") != null)
                        gridView.Columns["MiddleName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    if (gridView.Columns.ColumnByFieldName("ExtName") != null)
                        gridView.Columns["ExtName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                  

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;

                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                    // Set specific width for the AttachmentNames column
                    gridView.Columns["AttachmentNames"].Width = 150; // Set your desired width here
                    gridView.Columns["StatusCurrent"].Width = 150; // Set your desired width here

                    // Auto-size all columns based on their content, except AttachmentNames
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
                    {
                        if (column.FieldName != "AttachmentNames" && column.FieldName != "StatusCurrent")
                        {
                            column.BestFit();
                        }
                    }

                }
                // Update row count display
                UpdateRowCount(gridView);
                progressBarControl1.EditValue = 100; // Set progress bar to 100% on completion
                DisableSpinner();
                con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                con.Close();
            }


        }

        // Method to hide columns in the GridView
        private void HideColumn(GridView gridView, string columnName)
        {
            if (gridView.Columns[columnName] != null)
            {
                gridView.Columns[columnName].Visible = false;
            }
        }
        //Show the spinner
        private void EnableSpinner()
        {
           // btn_search.Enabled = false;
           // btn_refresh.Enabled = false;
            panel_spinner.Visible = true;
        }
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
            //btn_search.Enabled = true; //Enable textbox once gridview was loaded successfully
           // btn_refresh.Enabled = true;
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
        }
        private string previousMunicipality = string.Empty;
        private string previousStatus = string.Empty;

        // Method to update the row count display
        public void UpdateRowCount(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Assign formatted row count to txt_total (or any other control)
            groupControl1.Text = $"Count of showed data: [{formattedRowCount}]";
        }


        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ck_all.Checked = false;



        }

        private void gridControl1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void searchControl1_QueryIsSearchColumn_1(object sender, QueryIsSearchColumnEventArgs args)
        {
            //Search only specific columns example lastname.
            if (args.FieldName != "LastName") //for lastname
                args.IsSearchColumn = false;
            if (args.FieldName != "MiddleName") // for middlename
                args.IsSearchColumn = false;
            if (args.FieldName != "FirstName") // for firstname
                args.IsSearchColumn = false;
        }
        //Prevent winforms in opening existing form.
        NewApplicant NewApplicantForm;
        private void ts_newapplicant_Click(object sender, EventArgs e)
        {

        }
        EditApplicant EditApplicantForm;
        AuthorizeRepresentative AuthorizeRepresentativeForm;
        private void simpleButton1_Click(object sender, EventArgs e)
        {


        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Check if any row is selected
            //    if (gridView1.SelectedRowsCount == 0)
            //    {
            //        MessageBox.Show("Please select a data to Delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return; // Exit the method without showing EditApplicantForm
            //    }

            //    // Assuming gridView is your GridView instance associated with gridControl1
            //    GridView gridView = gridControl1.MainView as GridView;
            //    int rowHandle = gridView.FocusedRowHandle; // Get the handle of the focused row
            //    if (rowHandle >= 0) // Ensure there is a selected row
            //    {
            //        object idValue = gridView.GetRowCellValue(rowHandle, "ID"); // Replace "ID" with the name of your ID column
            //        object nameValue = gridView.GetRowCellValue(rowHandle, "LastName"); // Replace "LastName" with the column name of the person's name
            //        if (idValue != null && nameValue != null)
            //        {
            //            int id = Convert.ToInt32(idValue);
            //            string name = nameValue.ToString();

            //            if (XtraMessageBox.Show($"Are you sure you want to delete [{name}'s] data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                con.Open();
            //                MySqlCommand cmd = con.CreateCommand();
            //                cmd.CommandType = CommandType.Text;
            //                // Perform the update
            //                cmd.CommandText = @"
            //        UPDATE 
            //            tbl_masterlist 
            //        SET
            //            DateTimeDeleted = @DateTimeDeleted,
            //            DeletedBy = @DeletedBy
            //        WHERE 
            //            ID = @ID";

            //                cmd.Parameters.Clear();
            //                cmd.Parameters.AddWithValue("@ID", id);
            //                cmd.Parameters.AddWithValue("@DateTimeDeleted", DateTime.Now);
            //                cmd.Parameters.AddWithValue("@DeletedBy", Environment.UserName);
            //                cmd.ExecuteNonQuery();
            //                con.Close();


            //                //Filter by active and applicants only. 
            //                if (cmb_status.Text == "Active" || cmb_status.Text == "Applicant")
            //                {
            //                    btn_search.Enabled = false;
            //                    ActiveandApplicantList();
            //                }
            //                //Filter by Reference code and SpisBatch as not null only. 
            //                else if (cmb_status.Text == "Waitlisted")
            //                {
            //                    btn_search.Enabled = false;
            //                    Waitlisted();
            //                }
            //                //Filter by status between 2 and 15 as as delisted list. 
            //                else if (cmb_status.Text == "Delisted")
            //                {
            //                    btn_search.Enabled = false;
            //                    Delisted();
            //                }
            //                //Filter by Municipality no other.
            //                else if (cmb_status.Text == "All Statuses")
            //                {
            //                    btn_search.Enabled = false;
            //                    AllStatusList();
            //                }
            //                // Update row count display
            //                //UpdateRowCount(gridView);

            //                XtraMessageBox.Show("Data successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show("No valid record selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    // Handle exceptions
            //    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    throw;
            //}
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //GridView gridView = sender as GridView;
            //DataRowView row = (DataRowView)gridView.GetRow(e.FocusedRowHandle);

            //if (row != null)
            //{
            //    string gis = row["GIS"].ToString();
            //    string spbuf = row["SPBUF"].ToString();

            //    ts_view.Enabled = !(string.IsNullOrWhiteSpace(gis) && string.IsNullOrWhiteSpace(spbuf));
            //}
            //else
            //{
            //    ts_view.Enabled = false;
            //}
        }



        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
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
                GridView gridView = gridControl1.MainView as GridView;

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
                //EditApplicantForm.ShowDialog();




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

        public void ReloadMasterlist()
        {


               // btn_search.Enabled = false;
                Task task = AllMunicipalities();
                return;



        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void btn_refresh_Click(object sender, EventArgs e)
        {


            //// Your existing logic to handle search
            //if (cmb_municipality.Text == "Select City/Municipality" && cmb_status.Text == "Select Status")
            //{
            //    MessageBox.Show("Please enter City/Municipality and Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (cmb_municipality.Text == "Select City/Municipality")
            //{
            //    MessageBox.Show("Please enter City/Municipality before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else if (cmb_status.Text == "Select Status")
            //{
            //    MessageBox.Show("Please enter Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            //EnableSpinner();//Enable the spinner
            //await AllMunicipalities(); // Do not repeat yourself code implemented filters.
            //return;

        }
        PayrollHistory payrollHistoryForm;

        private void viewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<PayrollHistory>().Any())
            {
                payrollHistoryForm.Select();
                payrollHistoryForm.BringToFront();
            }
            else
            {


                GridView gridView = gridControl1.MainView as GridView;
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }
                payrollHistoryForm = new PayrollHistory(this);

                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                payrollHistoryForm.DisplayID(id);
                payrollHistoryForm.ShowDialog();


            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void newApplicantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<NewApplicant>().Any())
            {
                NewApplicantForm.Select();
                NewApplicantForm.BringToFront();
            }
            else
            {
                NewApplicantForm = new NewApplicant(_username);
                NewApplicantForm.ShowDialog();
            }
        }
        Delisted delistedForm;
        private void delistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Delisted>().Any())
            {
                delistedForm.Select();
                delistedForm.BringToFront();
            }
            else
            {
                GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data row first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Get the selected row
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                string status = row["StatusCurrent"].ToString();

                // Check if StatusID is 1 (Active) or 99 (Applicant)
                if (status != "Active" && status != "Applicant" && status != "Waitlisted")
                {
                    MessageBox.Show("Particular beneficiary was already Delisted please select an Applicant, Active, or Waitlisted beneficiary to continue.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                delistedForm = new Delisted(this, _username);

                // Pass the ID value to the EditApplicant form
                int id = Convert.ToInt32(row["ID"]);
                delistedForm.DisplayID(id);
                delistedForm.ShowDialog();
            }
        }
        private void UndoVerification()
        {
            try
            {
                con.Open();

                GridView gridView = gridControl1.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                //var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT 
            m.IsVerified
        FROM 
            tbl_masterlist m
        WHERE 
            m.ID = @IDNumber";
                cmd.Parameters.AddWithValue("@IDNumber", id);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];
                    bool verificationBefore = oldRow["IsVerified"] != DBNull.Value && Convert.ToBoolean(oldRow["IsVerified"]);
                    bool verificationAfter = false; // Set the new value to true

                    // Perform the update
                    cmd.CommandText = @"
                UPDATE 
                    tbl_masterlist 
                SET
                    IsVerified = @IsVerified
                WHERE 
                    ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@IsVerified", verificationAfter);
                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    if (verificationBefore != verificationAfter)
                    {
                        MySqlCommand logCmd = con.CreateCommand();
                        logCmd.CommandType = CommandType.Text;
                        logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                        logCmd.Parameters.AddWithValue("@MasterListID", id);
                        logCmd.Parameters.AddWithValue("@Log", $"Verification changed from [{verificationBefore}] to [{verificationAfter}]");
                        logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                        logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                        logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                        logCmd.ExecuteNonQuery();
                    }

                    con.Close();
                    ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Verify()
        {
            try
            {
                con.Open();

                GridView gridView = gridControl1.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                //var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT 
            m.IsVerified
        FROM 
            tbl_masterlist m
        WHERE 
            m.ID = @IDNumber";
                cmd.Parameters.AddWithValue("@IDNumber", id);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];
                    bool verificationBefore = oldRow["IsVerified"] != DBNull.Value && Convert.ToBoolean(oldRow["IsVerified"]);
                    bool verificationAfter = true; // Set the new value to true

                    // Perform the update
                    cmd.CommandText = @"
                UPDATE 
                    tbl_masterlist 
                SET
                    IsVerified = @IsVerified
                WHERE 
                    ID = @ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@IsVerified", verificationAfter);
                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    if (verificationBefore != verificationAfter)
                    {
                        MySqlCommand logCmd = con.CreateCommand();
                        logCmd.CommandType = CommandType.Text;
                        logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                        logCmd.Parameters.AddWithValue("@MasterListID", id);
                        logCmd.Parameters.AddWithValue("@Log", $"Verification changed from [{verificationBefore}] to [{verificationAfter}]");
                        logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                        logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                        logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                        logCmd.ExecuteNonQuery();
                    }

                    con.Close();
                    ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UpdateMastertoApplicant()
        {
            try
            {
                con.Open();

                GridView gridView = gridControl1.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                //var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT 
            m.StatusID
        FROM 
            tbl_masterlist m
        WHERE 
            m.ID = @IDNumber";
                cmd.Parameters.AddWithValue("@IDNumber", id);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];

                    // Fetch current status name
                    string statusNameBefore = "";
                    string statusNameAfter = "";

                    if (oldRow["StatusID"] != DBNull.Value)
                    {
                        int statusIDBefore = Convert.ToInt32(oldRow["StatusID"]);
                        MySqlCommand statusCmdBefore = con.CreateCommand();
                        statusCmdBefore.CommandType = CommandType.Text;
                        statusCmdBefore.CommandText = "SELECT Status FROM lib_status WHERE Id = @StatusID";
                        statusCmdBefore.Parameters.AddWithValue("@StatusID", statusIDBefore);
                        statusNameBefore = statusCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    //int statusIDAfter = Convert.ToInt32(lbl_status.Text); // Assuming lbl_sex.Text contains the updated SexID
                    int statusIDAfter = 99; // Assuming lbl_sex.Text contains the updated SexID

                    MySqlCommand statusCmdAfter = con.CreateCommand();
                    statusCmdAfter.CommandType = CommandType.Text;
                    statusCmdAfter.CommandText = "SELECT Status FROM lib_status WHERE Id = @StatusID";
                    statusCmdAfter.Parameters.AddWithValue("@StatusID", statusIDAfter);
                    statusNameAfter = statusCmdAfter.ExecuteScalar()?.ToString() ?? "";

                    // Perform the update
                    cmd.CommandText = @"
            UPDATE 
                tbl_masterlist 
            SET
                StatusID = @StatusID,
                DateDeceased = @DateDeceased,
                Remarks = @Remarks,
                ExclusionBatch = @ExclusionBatch,
                ExclusionDate = @ExclusionDate,
                InclusionDate = @InclusionDate
            WHERE 
                ID = @ID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@StatusID", 99);
                    cmd.Parameters.AddWithValue("@DateDeceased", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Remarks", null);
                    cmd.Parameters.AddWithValue("@ExclusionBatch", null); // Plus the abbreviation from period
                    cmd.Parameters.AddWithValue("@ExclusionDate", null);
                    cmd.Parameters.AddWithValue("@InclusionDate", null);
                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    string[] columns = new string[]
                    {
            "StatusID"
                    };

                    foreach (string column in columns)
                    {
                        string oldValue = oldRow[column].ToString();
                        string newValue = cmd.Parameters["@" + column].Value.ToString();

                        if (oldValue != newValue)
                        {
                            MySqlCommand logCmd = con.CreateCommand();
                            logCmd.CommandType = CommandType.Text;
                            logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                            logCmd.Parameters.AddWithValue("@MasterListID", id);
                            if (column == "StatusID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Status changed from [{statusNameBefore}] to [{statusNameAfter}]");
                            }
                            //else
                            //{
                            //    logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}]");
                            //}
                            logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                            logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                            logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                            logCmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void setAsApplicantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm
            }

            // Get the selected row
            DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
            string status = row["StatusCurrent"].ToString();

            // Check if StatusID is 1 (Active) or 99 (Applicant)
            if (status == "Applicant")
            {
                MessageBox.Show("Particular beneficiary is already an Applicant please select an Active or Delisted beneficiary to continue.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }

            int rowHandle = gridView.FocusedRowHandle; // Get the handle of the focused row
            if (rowHandle >= 0) // Ensure there is a selected row
            {
                object idValue = gridView.GetRowCellValue(rowHandle, "ID"); // Replace "ID" with the name of your ID column
                object nameValue = gridView.GetRowCellValue(rowHandle, "LastName"); // Replace "LastName" with the column name of the person's name
                object firstnameValue = gridView.GetRowCellValue(rowHandle, "FirstName");
                if (idValue != null && nameValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    string name = $"{nameValue.ToString()}, {firstnameValue.ToString()}";
                    if (XtraMessageBox.Show($"Are you sure you want to set this {name} to Applicant?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        UpdateMastertoApplicant(); // Set the beneficiary to applicant.
                        return;
                    }
                }
            }

        }

        private void UpdateMastertoActive()
        {
            try
            {
                con.Open();

                GridView gridView = gridControl1.MainView as GridView;
                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["ID"]);

                //var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT 
            m.StatusID
        FROM 
            tbl_masterlist m
        WHERE 
            m.ID = @IDNumber";
                cmd.Parameters.AddWithValue("@IDNumber", id);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];

                    // Fetch current status name
                    string statusNameBefore = "";
                    string statusNameAfter = "";

                    if (oldRow["StatusID"] != DBNull.Value)
                    {
                        int statusIDBefore = Convert.ToInt32(oldRow["StatusID"]);
                        MySqlCommand statusCmdBefore = con.CreateCommand();
                        statusCmdBefore.CommandType = CommandType.Text;
                        statusCmdBefore.CommandText = "SELECT Status FROM lib_status WHERE Id = @StatusID";
                        statusCmdBefore.Parameters.AddWithValue("@StatusID", statusIDBefore);
                        statusNameBefore = statusCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    //int statusIDAfter = Convert.ToInt32(lbl_status.Text); // Assuming lbl_sex.Text contains the updated SexID
                    int statusIDAfter = 1; // Assuming lbl_sex.Text contains the updated SexID

                    MySqlCommand statusCmdAfter = con.CreateCommand();
                    statusCmdAfter.CommandType = CommandType.Text;
                    statusCmdAfter.CommandText = "SELECT Status FROM lib_status WHERE Id = @StatusID";
                    statusCmdAfter.Parameters.AddWithValue("@StatusID", statusIDAfter);
                    statusNameAfter = statusCmdAfter.ExecuteScalar()?.ToString() ?? "";

                    // Perform the update
                    cmd.CommandText = @"
            UPDATE 
                tbl_masterlist 
            SET
                StatusID = @StatusID,
                DateDeceased = @DateDeceased,
                Remarks = @Remarks,
                ExclusionBatch = @ExclusionBatch,
                ExclusionDate = @ExclusionDate,
                InclusionDate = @InclusionDate
            WHERE 
                ID = @ID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@StatusID", 1);
                    cmd.Parameters.AddWithValue("@DateDeceased", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Remarks", null);
                    cmd.Parameters.AddWithValue("@ExclusionBatch", null); // Plus the abbreviation from period
                    cmd.Parameters.AddWithValue("@ExclusionDate", null);
                    cmd.Parameters.AddWithValue("@InclusionDate", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    string[] columns = new string[]
                    {
            "StatusID"
                    };

                    foreach (string column in columns)
                    {
                        string oldValue = oldRow[column].ToString();
                        string newValue = cmd.Parameters["@" + column].Value.ToString();

                        if (oldValue != newValue)
                        {
                            MySqlCommand logCmd = con.CreateCommand();
                            logCmd.CommandType = CommandType.Text;
                            logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                            logCmd.Parameters.AddWithValue("@MasterListID", id);
                            if (column == "StatusID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Status changed from [{statusNameBefore}] to [{statusNameAfter}]");
                            }
                            //else
                            //{
                            //    logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}]");
                            //}
                            logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                            logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                            logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                            logCmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm
            }


            // Get the selected row
            DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
            string status = row["StatusCurrent"].ToString();
            string gis = row["GIS"].ToString();
            string spbuf = row["SPBUF"].ToString();
            string assessment = row["Assessment"].ToString();
            string spisBatch = row["SpisBatch"].ToString();

            // IF the status is Applicant and Assessment is not Eligible or Null and SpinsBatch is Null and GIS or SPBUF is null then data is not eligible for Activation.
            if (status == "Active")
            {
                MessageBox.Show("Benficiary is already Active", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }

            // IF the status is Applicant and Assessment is not Eligible or Null and SpinsBatch is Null and GIS or SPBUF is null then data is not eligible for Activation.
            else if (assessment != "Eligible")
            {
                MessageBox.Show("Benficiary is not eligible to activate because he/she is Not Eligible.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }

            // IF the status is Applicant and Assessment is Eligible and SpinsBatch is Null is not eligible for Activation.
            else if (spisBatch == "")
            {
                MessageBox.Show("Benficiary is not eligible to activate because he/she have no Spins Batching.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }
            else if (cmb_status.Text == "Delisted")
            {
                MessageBox.Show("You can't Activate an delisted beneficiary. Set it as an Applicant instead.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }



            int rowHandle = gridView.FocusedRowHandle; // Get the handle of the focused row
            if (rowHandle >= 0) // Ensure there is a selected row
            {
                object idValue = gridView.GetRowCellValue(rowHandle, "ID"); // Replace "ID" with the name of your ID column
                object nameValue = gridView.GetRowCellValue(rowHandle, "LastName"); // Replace "LastName" with the column name of the person's name
                if (idValue != null && nameValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    string name = nameValue.ToString();
                    if (XtraMessageBox.Show($"Are you sure you want to Activate {name} Data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        UpdateMastertoActive();//Update beneficiary to Active.
                        return;
                    }
                }
            }
        }
        private void delistedAndReplacementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Replacements>().Any())
            {
                replacementsForm.Select();
                replacementsForm.BringToFront();
            }
            else
            {
                replacementsForm = new Replacements(_username);
                replacementsForm.ShowDialog();
            }
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        PayrollPopup payrollpopupForm;
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            // Check if any row is selected
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select Municipality and set status to Active before creating payroll", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method without showing EditApplicantForm
            }

            //if (ck_all.Checked == true)
            //{
            //    XtraMessageBox.Show("Select only One[1] Municipality when creating a payroll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (cmb_status.Text != "Active")
            {
                XtraMessageBox.Show("Sorry you can only create a payroll for active applicants", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmb_municipality.Text == "")
            {
                XtraMessageBox.Show("Select municipality before creating payroll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //InsertIntoPayroll();

            if (Application.OpenForms.OfType<PayrollPopup>().Any())
            {
                payrollpopupForm.Select();
                payrollpopupForm.BringToFront();
            }
            else
            {
                payrollpopupForm = new PayrollPopup(this, _username); //Instantiate to make the gridcontrol from form masterlist work into the payrollpopup form.
                payrollpopupForm.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if any row is selected
                if (gridView1.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to Delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Assuming gridView is your GridView instance associated with gridControl1
                GridView gridView = gridControl1.MainView as GridView;
                int rowHandle = gridView.FocusedRowHandle; // Get the handle of the focused row
                if (rowHandle >= 0) // Ensure there is a selected row
                {
                    object idValue = gridView.GetRowCellValue(rowHandle, "ID"); // Replace "ID" with the name of your ID column
                    object nameValue = gridView.GetRowCellValue(rowHandle, "LastName"); // Replace "LastName" with the column name of the person's name
                    if (idValue != null && nameValue != null)
                    {
                        int id = Convert.ToInt32(idValue);
                        string name = nameValue.ToString();

                        if (XtraMessageBox.Show($"Are you sure you want to delete [{name}'s] data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            MySqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            // Perform the update
                            cmd.CommandText = @"
                    UPDATE 
                        tbl_masterlist 
                    SET
                        DateTimeDeleted = @DateTimeDeleted,
                        DeletedBy = @DeletedBy
                    WHERE 
                        ID = @ID";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@DateTimeDeleted", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DeletedBy", _username);
                            cmd.ExecuteNonQuery();
                            con.Close();


                            //Reload the masterlist
                            ReloadMasterlist();

                            XtraMessageBox.Show("Data successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("No valid record selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        Attachments attachmentsForm;
        private void attachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Attachments>().Any())
            {
                attachmentsForm.Select();
                attachmentsForm.BringToFront();
            }
            else
            {

                GridView gridView = gridControl1.MainView as GridView;
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                // Pass the ID value to the EditApplicant form
                int id = Convert.ToInt32(row["ID"]);
                //int id = Convert.ToInt32(txt_id.Text);
                attachmentsForm = new Attachments(masterlistForm, payrollForm, _username);

                attachmentsForm.DisplayID(id);
                attachmentsForm.ShowDialog();
            }


        }
        private Payroll payrollForm;

        private void payrollToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Payroll>().Any())
            {
                payrollForm.Select();
                payrollForm.BringToFront();
            }
            else
            {
                payrollForm = new Payroll(_username, _userRole);
                payrollForm.ShowDialog();
            }
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {

            // Get the current search criteria
            string currentMunicipality = cmb_municipality.Text;
            string currentStatus = cmb_status.Text;

            // Check if the search criteria have changed
            if (currentMunicipality == previousMunicipality && currentStatus == previousStatus)
            {
                // Criteria haven't changed, do not trigger the search method
                //MessageBox.Show("Search criteria have not changed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update the previous search criteria
            previousMunicipality = currentMunicipality;
            previousStatus = currentStatus;

            // Your existing logic to handle search
            if (cmb_municipality.Text == "" && cmb_status.Text == "")
            {
                //MessageBox.Show("Please enter City/Municipality and Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_municipality.Text == "")
            {
                //MessageBox.Show("Please enter City/Municipality before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_status.Text == "")
            {
                //MessageBox.Show("Please enter Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnableSpinner();//Enable the spinner
            await AllMunicipalities(); // Do not repeat yourself code implemented filters.
            return;
        }

        private void verifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a record first.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected row
            DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
            if (row == null)
            {
                MessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool verification = Convert.ToBoolean(row["Verified"]);

            // Check if already verified
            if (verification)
            {
                MessageBox.Show("This beneficiary is already verified.", "Already Verified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowHandle = gridView.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                object idValue = gridView.GetRowCellValue(rowHandle, "ID");
                object nameValue = gridView.GetRowCellValue(rowHandle, "LastName");
                object firstnameValue = gridView.GetRowCellValue(rowHandle, "FirstName");

                if (idValue != null && nameValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    string name = $"{nameValue.ToString()}, {firstnameValue.ToString()}";
                    DialogResult result = XtraMessageBox.Show($"Are you sure you want to verify {name}?", "Confirm Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Verify(); // Call the verification method
                    }
                }
                else
                {
                    MessageBox.Show("Could not retrieve the selected record's details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a record first.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected row
            DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
            if (row == null)
            {
                MessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool verification = Convert.ToBoolean(row["Verified"]);

            // Check if already verified
            if (!verification)
            {
                MessageBox.Show("This beneficiary is already not verified", "Not Verified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowHandle = gridView.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                object idValue = gridView.GetRowCellValue(rowHandle, "ID");
                object nameValue = gridView.GetRowCellValue(rowHandle, "LastName");
                object firstnameValue = gridView.GetRowCellValue(rowHandle, "FirstName");

                if (idValue != null && nameValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    string name = $"{nameValue.ToString()}, {firstnameValue.ToString()}";
                    DialogResult result = XtraMessageBox.Show($"Are you sure you want to undo verifcation of {name}?", "Undo Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        UndoVerification(); // Call the verification method
                    }
                }
                else
                {
                    MessageBox.Show("Could not retrieve the selected record's details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MasterList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    if (MessageBox.Show("Are you sure want to exit?",
            //                   "My First Application",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Information) == DialogResult.Yes)
            //        Environment.Exit(1);
            //    else
            //        e.Cancel = true; // to don't close form is user change his mind
            //}
         
        }

        private async void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Get the current search criteria
            string currentMunicipality = cmb_municipality.Text;
            string currentStatus = cmb_status.Text;

            // Check if the search criteria have changed
            if (currentMunicipality == previousMunicipality && currentStatus == previousStatus)
            {
                // Criteria haven't changed, do not trigger the search method
               // MessageBox.Show("Search criteria have not changed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Update the previous search criteria
            previousMunicipality = currentMunicipality;
            previousStatus = currentStatus;

            // Your existing logic to handle search
            if (cmb_municipality.Text == "" && cmb_status.Text == "")
            {
                //MessageBox.Show("Please enter City/Municipality and Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_municipality.Text == "")
            {
                //MessageBox.Show("Please enter City/Municipality before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmb_status.Text == "")
            {
                //MessageBox.Show("Please enter Status before searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnableSpinner();//Enable the spinner
            await AllMunicipalities(); // Do not repeat yourself code implemented filters.
            return;
        }
    }
}
