using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Extensions.DependencyInjection;
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

        public string _username;
        public string _userRole;
        private Replacements replacementsForm;
        private EditApplicant editApplicantForm;
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
            }

            //Integrate search control into our grid control.
            searchControl1.Client = gridControl1;

        }

        //Refactored code of municipality dropdown.
        private async Task MunicipalityEf()
        {
            EnableSpinner();
            var municipalityLists = await Task.Run(() => _libraryMunicipality.GetMunicipalitiesAsync());
            foreach (var municipalityList in municipalityLists)
            {
                cmb_municipality.Properties.Items.Add(new CheckedListBoxItem
                (
                    value: municipalityList.PSGCCityMun, //Reference the ID
                    description: municipalityList.CityMunName + " " + municipalityList.LibraryProvince.ProvinceName, // Display the text plus the province name.
                    checkState: CheckState.Unchecked // Initially Unchecked.

                ));
            }
            DisableSpinner();

        }

        // Method to update progress bar value
        private void UpdateProgressBar(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgressBar(value)));
            }
            else
            {
                progressBarControl1.EditValue = value;
            }
        }

        private async Task LoadMasterList()
        {
            EnableSpinner();
            //Initialize progress bar.
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = 100;
            progressBarControl1.Properties.Step = 10;
            progressBarControl1.EditValue = 0;
            progressBarControl1.Visible = true;

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
                    statusArray.AddRange(Enumerable.Range(2, 14)); // Range from 2 to 15 (14 total statuses)
                }
                else
                {
                    // Add the other statuses like Active (1) or Applicant (99)
                    statusArray.Add(int.Parse(item.Trim()));
                }
            }

            // progressBarControl1.PerformStep();
            await Task.Run(async () =>
            {
                try
                {
                    //Update the progress bar to idiciate loading ahs started
                    for (int i = 0; i <= 20; i++)
                    {
                        // Invoke is required to update the UI from a non-UI thread
                        progressBarControl1.Invoke((MethodInvoker)(() =>
                        {
                            progressBarControl1.EditValue = i;

                        }));
                        await Task.Delay(20);//To make the progress bar steps slowly
                    }
                    //Perform data loading.
                    var masterLists = await _tableMasterlist.GetMasterListModelsAsync(municipalitiesArray, statusArray);

                    //Update the progress bar to idiciate loading ahs started
                    for (int i = 20; i <= 50; i ++)
                    {
                        // Invoke is required to update the UI from a non-UI thread
                        progressBarControl1.Invoke((MethodInvoker)(() =>
                        {
                            progressBarControl1.EditValue = i;
                           
                        }));
                        await Task.Delay(30);//To make the progress bar steps slowly
                    }
                    

                    //Update the progress bar to indicate data has been loaded
                    UpdateProgressBar(50);

                    //Simulate pgoress update( if you have multiple stages, updated here.)
                    // progressBarControl1.Invoke(new Action(() => progressBarControl1.EditValue = 50));
                    gridControl1.Invoke(new Action(() =>
                    {
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

                        //Update row count display.
                        UpdateRowCount(gridView);

                        //Update progress bar to indidicate completion.
                        UpdateProgressBar(100);
                    }));

                }
                catch (Exception)
                {

                    throw;
                }

            });

            //progressBarControl1.Visible = false; // Hide the progress bar and finalize.
            DisableSpinner();
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
        //Show the spinner
        private void EnableSpinner()
        {
            // btn_search.Enabled = false;
            // btn_refresh.Enabled = false;
            groupControl2.Enabled = false;
            panel_spinner.Visible = true;
        }
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
                                               //btn_search.Enabled = true; //Enable textbox once gridview was loaded successfully
                                               // btn_refresh.Enabled = true;
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
            groupControl2.Enabled = true;
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
            var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs
            var tableMasterlist = Program.ServiceProvider.GetRequiredService<ITableMasterlist>(); //We called the DI lifecycle inside our Program.cs
            EditApplicantForm = new EditApplicant(this, replacementsForm, _username, tableLog, tableMasterlist);
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
                EditApplicantForm.Show();

            }
        }

        PayrollHistory payrollHistoryForm;
        Delisted delistedForm;

        PayrollPopup payrollpopupForm;
        Attachments attachmentsForm;
        private Payroll payrollForm;


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
                var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs
                replacementsForm = new Replacements(_username, tableLog);
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

                // Resolve the ITablePayroll service from the Program.ServiceProvider
                //var libraryMunicipality = Program.ServiceProvider.GetRequiredService<ILibraryMunicipality>(); //We called the DI lifecycle inside our Program.cs
                var tableMasterlist = Program.ServiceProvider.GetRequiredService<ITableMasterlist>(); //We called the DI lifecycle inside our Program.cs
                var tableLog = Program.ServiceProvider.GetRequiredService<ITableLog>(); //We called the DI lifecycle inside our Program.cs

                delistedForm = new Delisted(this, _username, tableMasterlist, tableLog);

                // Pass the ID value to the EditApplicant form
                int id = Convert.ToInt32(row.Id);
                delistedForm.DisplayID(id);
                delistedForm.ShowDialog();
            }
        }
        public void updateDatagrid(string status, string dateDeceased, string remarks, string exclusionBatch,
   DateTime? exclusionDate, DateTime? inclusionDate)
        {
            //Update the binding source. to prevent loading of the table directly from database.
            var selectedRow = masterListViewModelBindingSource.Current as MasterListViewModel;
            selectedRow.Status = status;
            selectedRow.DateDeceased = dateDeceased;
            selectedRow.Remarks = remarks;
            selectedRow.ExclusionBatch = exclusionBatch;
            selectedRow.ExclusionDate = exclusionDate;
            selectedRow.InclusionDate = inclusionDate;
            masterListViewModelBindingSource.ResetCurrentItem();
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
                string statusGrid = "Applicant";
                int statusId = 99;//used by masterlist
                string dateDeceased = null;//used by masterlist
                string remarks = null;//used by masterlist
                string exclusionBatch = null;//used by masterlist
                DateTime? exclusionDate = null;//used by masterlist
                DateTime? inclusionDate = null;//used by masterlist

                /*Implementation of DRY functionality below.*/
                //Update our masterlist property StatusID to 99.
                await _tableMasterlist.UpdateAsync(id, statusId, dateDeceased, remarks, exclusionBatch,
                    exclusionDate, inclusionDate);
                //Insert into our logs once updated.
                await _tableLog.InsertLogs(id, currentStatus, _username);

                //Update gridcontrol display instead of directly going into our database.
                updateDatagrid(statusGrid, dateDeceased, remarks, exclusionBatch, exclusionDate, inclusionDate);


                XtraMessageBox.Show("Successfully set to Applicant.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnSetApplicant_Click(object sender, EventArgs e)
        {
            await UpdateStatusMethod();

        }
        public async Task ActivateMethod()
        {
            GridView gridView = gridControl1.MainView as GridView;

            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method without showing EditApplicantForm
            }

            using (var context = new ApplicationDbContext())
            {

              
                // Get the selected row

                MasterListViewModel row = (MasterListViewModel)gridView.GetRow(gridView.FocusedRowHandle);
                string status = row.Status;
                string assessment = row.Assessment;
                int? spisBatch = Convert.ToInt32(row.SpisBatch);

            


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
                    string statusGrid = "Active";// use for updating datagrid status graphics.
                    string currentStatus = $"[Status] changed from '{row.Status}' to 'Active'.";//used by log_masterlist
                    string dateDeceased = null;//used by masterlist
                    string remarks = null;//used by masterlist
                    string exclusionBatch = null;//used by masterlist
                    DateTime? exclusionDate = null;//used by masterlist
                    DateTime? inclusionDate = DateTime.Now;//used by masterlist

                    /*Below is the correct implementation of DRY functionality*/
                    //Update our tblMasterlist StatusID and other properties
                    await _tableMasterlist.UpdateAsync(id, statusId, dateDeceased, remarks, exclusionBatch,
                        exclusionDate, inclusionDate);
                    //Insert into our logs
                    await _tableLog.InsertLogs(id, currentStatus, _username);
                    //Update gridcontrol display instead of directly going into our database.
                    updateDatagrid(statusGrid, dateDeceased, remarks, exclusionBatch, exclusionDate, inclusionDate);


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
                //Below remove from data from gridControl1 graphics
                var selectedRow = masterListViewModelBindingSource.Current as MasterListViewModel;
                masterListViewModelBindingSource.Remove(selectedRow);

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
                //Update verify to check
                verificationGrid(verify);

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
                //Update verify to uncheck
                verificationGrid(verify);
                XtraMessageBox.Show("Verification Undoed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Method to update verification from datagridview drawing.
        private void verificationGrid(bool verify)
        {
            //Update the binding source. to prevent loading of the table directly from database.
            var selectedRow = masterListViewModelBindingSource.Current as MasterListViewModel;
            selectedRow.IsVerified = verify;
            masterListViewModelBindingSource.ResetCurrentItem();
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
