using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Forms;
using SpinsNew.Interfaces;
using SpinsNew.Popups;
using SpinsNew.ViewModel;
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
        private EditApplicant editApplicantForm;
        private ApplicationDbContext _dbContext;
        private readonly ILibraryMunicipality _libraryMunicipality;
        private readonly ITableMasterlist _tableMasterlist;
        private readonly ITableLog _tableLog;


        public MasterList(string username, string userRole, EditApplicant editapplicant,
            ILibraryMunicipality libraryMunicipality, ITableMasterlist tableMasterlist, ITableLog tableLog)
        {
            InitializeComponent();
            _libraryMunicipality = libraryMunicipality;
            _tableMasterlist = tableMasterlist;
            _tableLog = tableLog;
            _dbContext = new ApplicationDbContext();
            con = new MySqlConnection(cs.dbcon);

            this.KeyPreview = true;
            this.KeyDown += btnViewAttach_KeyDown;
            this.KeyDown += btnViewPayroll_KeyDown;
            this.KeyDown += btnDelistBene_KeyDown;

            GridView gridView = (GridView)gridControl1.MainView;
            gridView.ColumnFilterChanged += gridView1_ColumnFilterChanged;

            _username = username; // Retrieve the username
            _userRole = userRole;
            editApplicantForm = editapplicant;


            if (userRole == "3")// Number 3 is the encoders
            {
                gbActions.Visible = false;
                gbPayroll.Visible = false;
                gbVerification.Visible = false;
                gbForms.Visible = false;
            }

            LoadStatusItems();
        }

        private void LoadStatusItems()
        {
            // Clear any previous items in the CheckedComboBox
            cmb_status.Properties.Items.Clear();

            // Add "Active" and "Applicant" as individual statuses
            cmb_status.Properties.Items.Add(new CheckedListBoxItem(1, "Active"));
            cmb_status.Properties.Items.Add(new CheckedListBoxItem(99, "Applicant"));

            // Add "Delisted" as a single item representing statuses from 2 to 15
            cmb_status.Properties.Items.Add(new CheckedListBoxItem("Delisted", "Delisted"));
        }


        private void btnDelistBene_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete))
            {
                btnDelistBene.PerformClick();

            }
        }

        private void btnViewPayroll_KeyDown(object sender, KeyEventArgs e)
        {
            // throw new NotImplementedException();
            // Check if Ctrl+B is pressed
            if ((e.Control && e.KeyCode == Keys.P))
            {
                //Focus the txtBarcode
                //txtBarcode.Focus();
                btnViewPayroll.PerformClick();

            }
        }

        private void btnViewAttach_KeyDown(object sender, KeyEventArgs e)
        {
            // throw new NotImplementedException();
            // Check if Ctrl+B is pressed
            if ((e.Control && e.KeyCode == Keys.S))
            {
                //Focus the txtBarcode
                //txtBarcode.Focus();
                btnViewAttach.PerformClick();

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


        //Load the methods.
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await MunicipalityEf();


            groupControl1.Text = "Count of showed data: [0]";
            // Cast the MainView to GridView
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView != null)
            {
                gridView.RowStyle += gridView_RowStyle;
                gridView.CustomDrawFooterCell += GridView_CustomDrawFooterCell;
            }

            //Integrate search control into our grid control.
            searchControl1.Client = gridControl1;

        }

        //Refactored code of municipality dropdown.
        private async Task MunicipalityEf()
        {
            var municipalityLists = await _libraryMunicipality.GetMunicipalitiesAsync();
            foreach (var municipalityList in municipalityLists)
            {
                cmb_municipality.Properties.Items.Add(new CheckedListBoxItem
                (
                    value: municipalityList.PSGCCityMun, //Reference the ID
                    description: municipalityList.CityMunName + " " + municipalityList.LibraryProvince.ProvinceName, // Display the text plus the province name.
                    checkState: CheckState.Unchecked // Initially Unchecked.

                ));
            }

        }

        //combine enum and delisted values intoo a list

        private async Task LoadMasterList()
        {
            EnableSpinner();
            GridView gridView = gridControl1.MainView as GridView;
            // Construct a filter for selected municipalities
            var checkedItems = cmb_municipality.Properties.GetCheckedItems();
            // Convert the checked items to a list of integers
            var municipalitiesArray = checkedItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(m => int.Parse(m.Trim()))
                                                          .ToList();
            // Construct a filter for selected statuses
            var checkedStatusItems = cmb_status.Properties.GetCheckedItems();
            var statusArray = new List<int>();

            // Parse the selected statuses
            foreach (var item in checkedStatusItems.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Trim() == "Delisted")
                {
                    // Add all statuses from 2 to 15 for "Delisted"
                    statusArray.AddRange(Enumerable.Range(2, 15)); // Range from 2 to 15 (14 total statuses)
                }
                else
                {
                    // Add the other statuses like Active (1) or Applicant (99)
                    statusArray.Add(int.Parse(item.Trim()));
                }
            }

            var masterLists = await Task.Run(() => _tableMasterlist.GetMasterListModelsAsync(municipalitiesArray, statusArray));

            masterListViewModelBindingSource.DataSource = masterLists;
            gridControl1.DataSource = masterListViewModelBindingSource;


            // Auto-size all columns based on their content
            gridView.BestFitColumns();

            // Ensure horizontal scrollbar is enabled
            gridView.OptionsView.ColumnAutoWidth = false;

            // Disable editing
            gridView.OptionsBehavior.Editable = false;

            //  gridView.Columns["Verification"].VisibleIndex = 0;

            // Freeze the columns if they exist
            if (gridView.Columns.ColumnByFieldName("Verification") != null)
                gridView.Columns["Verification"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            if (gridView.Columns.ColumnByFieldName("LastName") != null)
                gridView.Columns["LastName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            if (gridView.Columns.ColumnByFieldName("FirstName") != null)
                gridView.Columns["FirstName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            if (gridView.Columns.ColumnByFieldName("MiddleName") != null)
                gridView.Columns["MiddleName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            if (gridView.Columns.ColumnByFieldName("ExtName") != null)
                gridView.Columns["ExtName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            // Set specific width for the AttachmentNames column
            //gridView.Columns["AttachmentNames"].Width = 150; // Set your desired width here
            //gridView.Columns["StatusCurrent"].Width = 150; // Set your desired width here
            // Update row count display
            UpdateRowCount(gridView);
            //progressBarControl1.EditValue = 100; // Set progress bar to 100% on completion
            DisableSpinner();

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
        EditApplicant EditApplicantForm;
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditApplicantForm = new EditApplicant(this, replacementsForm, _username);
            if (Application.OpenForms.OfType<EditApplicant>().Any())
            {
                EditApplicantForm.Select();
                EditApplicantForm.BringToFront();
            }
            else
            {

                GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to Edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Pass the ID value to the EditApplicant form
                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row.Id);

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to view", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing GISForm
                }

                // Pass the ID value to the GISForm
                int gis = Convert.ToInt32(row.ReferenceCode);
                int spbuf = Convert.ToInt32(row.Spbuf);

                if (gis != 0)
                {
                    int gisId = Convert.ToInt32(gis);
                    EditApplicantForm.DisplayGIS(gisId);

                }
                else
                {
                    int spbufId = Convert.ToInt32(spbuf);
                    EditApplicantForm.DisplaySPBUF(spbufId);

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

        PayrollHistory payrollHistoryForm;
        Delisted delistedForm;

        PayrollPopup payrollpopupForm;

        Attachments attachmentsForm;

        private Payroll payrollForm;

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
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
                            //await AllMunicipalities(); // Do not repeat yourself code implemented filters.

            return;
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

        private void btnNew_Click(object sender, EventArgs e)
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

        private void btnViewAttach_Click(object sender, EventArgs e)
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

                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                // Pass the ID value to the EditApplicant form
                int id = Convert.ToInt32(row.Id);
                //int id = Convert.ToInt32(txt_id.Text);
                attachmentsForm = new Attachments(this, payrollForm, _username);

                attachmentsForm.DisplayID(id);
                attachmentsForm.ShowDialog();
            }
        }

        private void btnDelist_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Replacements>().Any())
            {
                replacementsForm.Select();
                replacementsForm.BringToFront();
            }
            else
            {
                replacementsForm = new Replacements(_username);
                replacementsForm.Show();
            }
        }

        private void btnViewPayroll_Click(object sender, EventArgs e)
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
                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row.Id);

                payrollHistoryForm.DisplayID(id);
                payrollHistoryForm.ShowDialog();


            }
        }

        private void btnCreatePayroll_Click(object sender, EventArgs e)
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

        private void btnPayroll_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms.OfType<Payroll>().Any())
            {
                payrollForm.Select();
                payrollForm.BringToFront();
            }
            else
            {
                payrollForm = new Payroll(_username, _userRole);
                payrollForm.Show();
            }
        }

        private void btnDelistBene_Click(object sender, EventArgs e)
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
                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                string status = row.Status;

                // Check if StatusID is 1 (Active) or 99 (Applicant)
                if (status != "Active" && status != "Applicant" && status != "Waitlisted")
                {
                    MessageBox.Show("Particular beneficiary was already Delisted please select an Applicant, Active, or Waitlisted beneficiary to continue.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                delistedForm = new Delisted(this, _username);

                // Pass the ID value to the EditApplicant form
                int id = Convert.ToInt32(row.Id);
                delistedForm.DisplayID(id);
                delistedForm.ShowDialog();
            }
        }
        public async Task UpdateStatusMethod()
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm
            }

            // Get the selected row
            MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
            string status = row.Status;

            // Check if StatusID is 1 (Active) or 99 (Applicant)
            if (status == "Applicant")
            {
                MessageBox.Show("Particular beneficiary is already an Applicant please select an Active or Delisted beneficiary to continue.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing the form
            }

            if (XtraMessageBox.Show($"Are you sure you want to set lastname {row.LastName} to Applicant?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // UpdateMastertoApplicant(); // Set the beneficiary to applicant.
                //return;
                int id = Convert.ToInt32(row.Id); // used by log_masterlist and tbl_masterlist
                string currentStatus = $"[Status] changed from '{row.Status}' to 'Applicant'.";//used by log_masterlist

                int statusId = 99;//used by masterlist
                string dateDeceased = null;//used by masterlist
                string remarks = null;//used by masterlist
                string exclusionBatch = null;//used by masterlist
                DateTime? exclusionDate = null;//used by masterlist
                DateTime? inclusionDate = null;//used by masterlist

                //Update our masterlist property StatusID to 99.
                await _tableMasterlist.UpdateAsync(id, statusId, dateDeceased, remarks, exclusionBatch,
                    exclusionDate, inclusionDate);

                //Insert into our logs once updated.
                await _tableLog.InsertLogs(id, currentStatus, _username);

                XtraMessageBox.Show("Successfully set to Applicant.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnSetApplicant_Click(object sender, EventArgs e)
        {
            await UpdateStatusMethod();

        }
        public async Task ActivateMethod()
        {

            using (var context = new ApplicationDbContext())
            {

                GridView gridView = gridControl1.MainView as GridView;
                // Get the selected row

                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                string status = row.Status;
                string assessment = row.Assessment;
                int? spisBatch = Convert.ToInt32(row.SpisBatch);

                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }


                // IF the status is Applicant and Assessment is not Eligible or Null and SpinsBatch is Null and GIS or SPBUF is null then data is not eligible for Activation.
                if (status == "Active")
                {
                    MessageBox.Show("Benficiary is already Active", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                // IF the status is Applicant and Assessment is not Eligible or Null and SpinsBatch is Null and GIS or SPBUF is null then data is not eligible for Activation.
                if (assessment != "Eligible")
                {
                    MessageBox.Show("Benficiary is not eligible to activate because he/she is Not Eligible.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                // IF the status is Applicant and Assessment is Eligible and SpinsBatch is Null is not eligible for Activation.
                if (spisBatch == null)
                {
                    MessageBox.Show("Benficiary is not eligible to activate because he/she have no Spins Batching.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                if (cmb_status.Text == "Delisted")
                {
                    MessageBox.Show("You can't Activate an delisted beneficiary. Set it as an Applicant instead.", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing the form
                }

                if (XtraMessageBox.Show($"Are you sure you want to Activate {row.LastName} Data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int id = Convert.ToInt32(row.Id); // used by log_masterlist and tbl_masterlist
                    int statusId = 1;//used by masterlist
                    string currentStatus = $"[Status] changed from '{row.Status}' to 'Active'.";//used by log_masterlist
                    string dateDeceased = null;//used by masterlist
                    string remarks = null;//used by masterlist
                    string exclusionBatch = null;//used by masterlist
                    DateTime? exclusionDate = null;//used by masterlist
                    DateTime? inclusionDate = DateTime.Now;//used by masterlist


                    //Update our tblMasterlist StatusID and other properties
                    await _tableMasterlist.UpdateAsync(id, statusId, dateDeceased, remarks, exclusionBatch,
                        exclusionDate, inclusionDate);
                    //Insert into our logs
                    await _tableLog.InsertLogs(id, currentStatus, _username);

                    XtraMessageBox.Show("Successfully activated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }

        }


        private async void btnActivate_Click(object sender, EventArgs e)
        {
            await ActivateMethod();

        }
        private async Task SoftDelete()
        {
            // Check if any row is selected
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to Delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm
            }
            GridView gridView = gridControl1.MainView as GridView;
            MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
            int Id = Convert.ToInt32(row.Id);//Search the particular ID we want to delete.
            DateTime? dateDeleted = DateTime.Now;// set the datetime now
            //The _username below is from our login.
            if (XtraMessageBox.Show($"Are you sure you want to delete {row.LastName} record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Below update the masterlist not actuall deleting the object.
                await _tableMasterlist.SoftDeleteAsync(Id, dateDeleted, _username);
                XtraMessageBox.Show($"{row.LastName} data successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await SoftDelete();
        }
        private async Task Verify()
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a record first.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected row
            MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
            if (row == null)
            {
                MessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool verification = Convert.ToBoolean(row.IsVerified);

            // Check if already verified
            if (verification)
            {
                MessageBox.Show("This beneficiary is already verified.", "Already Verified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = XtraMessageBox.Show($"Are you sure you want to verify {row.LastName}?", "Confirm Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(row.Id); // used by log_masterlist and tbl_masterlist
                string currentStatus = $"[Verification] changed from '{row.IsVerified}' to 'True'.";//used by log_masterlist
                bool verify = true;
                //Update the IsVerified from MasterList into true.
                await _tableMasterlist.VerificationUpdateAsync(id, verify);
                //Save changes into our logs.
                await _tableLog.InsertLogs(id, currentStatus, _username);

                XtraMessageBox.Show("Verified successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async Task UndoVerification()
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a record first.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected row
            MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
            if (row == null)
            {
                MessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool verification = Convert.ToBoolean(row.IsVerified);

            // Check if already verified
            if (!verification)
            {
                MessageBox.Show("This beneficiary is already not verified", "Not Verified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = XtraMessageBox.Show($"Are you sure you want to undo verifcation of {row.LastName}?", "Undo Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(row.Id); // used by log_masterlist and tbl_masterlist
                string currentStatus = $"[Verification] changed from '{row.IsVerified}' to 'False'.";//used by log_masterlist
                bool verify = false;
                //Update the IsVerified from MasterList into true.
                await _tableMasterlist.VerificationUpdateAsync(id, verify);
                //Save changes into our logs.
                await _tableLog.InsertLogs(id, currentStatus, _username);

                XtraMessageBox.Show("Verification Undo'ed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnVerify_Click(object sender, EventArgs e)
        {

            await Verify();
        }

        private async void btnUndoVerified_Click(object sender, EventArgs e)
        {
            await UndoVerification();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadMasterList();
        }
    }
}
