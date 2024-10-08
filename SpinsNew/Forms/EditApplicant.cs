using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Forms;
using SpinsNew.Interfaces;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsWinforms.Forms
{
    public partial class EditApplicant : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        private MasterList masterlistForm;// Call MasterList form
        private Replacements replacementsForm;// Call MasterList form
        private ITableLog _tableLog;
        private ITableMasterlist _tableMasterlist;
        public string _username;
        public EditApplicant(MasterList masterlist, Replacements replacements, string username,
            ITableLog tablelog, ITableMasterlist tableMasterlist)//Call the MasterList into our Edit Applicant form.
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            // _applicantData = applicantData;
            _tableLog = tablelog;
            _tableMasterlist = tableMasterlist;
            replacementsForm = replacements;// Execute the MasterListform.

            // Subscribe to the ValueChanged event of the DateTimePicker
            dt_birth.EditValueChanged += new EventHandler(Dt_birth_ValueChanged);

            // Initialize label text
            lbl_age.Text = "Age: ";

            // Set minimum date to 60 years ahead
            dt_birth.Properties.MaxValue = DateTime.Today.AddYears(-60);

            _username = username;
            masterlistForm = masterlist;// Execute the MasterListform.

        }


        public void DisplayID(int id)
        {
            // Display the ID in a label or textbox on your form
            txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
        }

        private async Task tableLog()
        {
            GridView gridView = gv_logs.MainView as GridView;


            int masterlistId = Convert.ToInt32(txt_id.Text);

            var logsList = await Task.Run(() => _tableLog.GetLogsAsync(masterlistId));
            gv_logs.DataSource = logsList;


            // Auto-size all columns based on their content
            gridView.BestFitColumns();

            // Ensure horizontal scrollbar is enabled
            gridView.OptionsView.ColumnAutoWidth = false;

            // Disable editing
            gridView.OptionsBehavior.Editable = false;

        }

        //Updating
        private async Task UpdateMaster()
        {

            int Id = Convert.ToInt32(txt_id.Text);

            using (var context = new ApplicationDbContext())
            {
                var updateMaster = await context.tbl_masterlist
                    .Include(s => s.LibrarySex)
                    .Include(m => m.LibraryMaritalStatus)
                    .Include(h => h.LibraryHealthStatus)
                    .Include(d => d.LibraryDataSource)
                    .Include(r => r.LibraryRegion)
                    .Include(p => p.LibraryProvince)
                    .Include(m => m.LibraryMunicipality)
                    .Include(b => b.LibraryBarangay)
                    .FirstOrDefaultAsync(i => i.Id == Id);
                //Default values from masterlist here.
                string defaultLastName = updateMaster.LastName;
                string defaultFirstName = updateMaster.FirstName;
                string defaultMiddleName = updateMaster.MiddleName;
                string defaultExtName = updateMaster.ExtName;
                DateTime? defaultBirthDate = updateMaster.BirthDate;
                string defaultSex = updateMaster.LibrarySex.Sex;
                string defaultMarital = updateMaster.LibraryMaritalStatus.MaritalStatus;
                string defaultHealthStatus = updateMaster.LibraryHealthStatus.HealthStatus;
                string defaultRemarks = updateMaster.HealthStatusRemarks;
                string defaultIdNumber = updateMaster.IDNumber;
                DateTime? defaultIdDateissued = updateMaster.IDDateIssued;
                string defaultCitizenship = updateMaster.Citizenship;
                string defaultMothersMaiden = updateMaster.MothersMaiden;
                string defaultReligion = updateMaster.Religion;
                string defaultBirthPlace = updateMaster.BirthPlace;
                string defaultEducAttain = updateMaster.EducAttain;
                string defaultContactNo = updateMaster.ContactNum;
                string defaultDataSource = updateMaster.LibraryDataSource.DataSource;

                string defaultRegion = updateMaster.LibraryRegion.Region;
                string defaultProvince = updateMaster.LibraryProvince.ProvinceName;
                string defaultMunicipality = updateMaster.LibraryMunicipality.CityMunName;
                string defaultBarangay = updateMaster.LibraryBarangay.BrgyName;
                string defaultAddress = updateMaster.Address;

                //Boolean property below.
                bool defaultPantawid = updateMaster.Pantawid ?? false;
                bool defaultIndigenous = updateMaster.Indigenous ?? false;

                //Updating masterlist with textboxes/comboboxes
                updateMaster.LastName = txt_lastname.Text;//Done for logging.
                updateMaster.FirstName = txt_firstname.Text;//Done for logging.
                updateMaster.MiddleName = txt_middlename.Text;//Done for logging.
                updateMaster.ExtName = txt_extname.Text;//Done for logging.
                updateMaster.BirthDate = Convert.ToDateTime(dt_birth.EditValue);//Done for logging.
                updateMaster.SexID = Convert.ToInt32(lblSex.Text);//Done for logging.
                updateMaster.MaritalStatusID = Convert.ToInt32(lblMaritalStatus.Text);//Done for logging.
                updateMaster.HealthStatusID = Convert.ToInt32(lblHealthStatus.Text);//Done for logging.
                updateMaster.HealthStatusRemarks = txt_remarks.Text;//Done for logging.
                updateMaster.IDNumber = txt_idno.Text;//Done for logging.
                updateMaster.IDDateIssued = Convert.ToDateTime(dt_dateissued.EditValue);//Done for logging.
                updateMaster.Pantawid = ck_pantawid.Checked;//Done
                updateMaster.Indigenous = ck_indigenous.Checked;//Done
                updateMaster.Citizenship = txt_citizenship.Text;//Done
                updateMaster.MothersMaiden = txt_mothers.Text;//Done
                updateMaster.Religion = txt_religion.Text;//Done
                updateMaster.BirthPlace = txt_birthplace.Text;//Done
                updateMaster.EducAttain = txt_educ.Text;//Done
                updateMaster.ContactNum = txt_contact.Text;//Done
                updateMaster.DataSourceId = Convert.ToInt32(lblDatasource.Text);//Done
                updateMaster.PSGCRegion = Convert.ToInt32(lbl_region.Text);//Done
                updateMaster.PSGCProvince = Convert.ToInt32(lbl_province.Text);//Done
                updateMaster.PSGCCityMun = Convert.ToInt32(lbl_municipality.Text);//Done
                updateMaster.PSGCBrgy = Convert.ToInt32(lbl_barangay.Text);//Done
                updateMaster.Address = txt_address.Text;//Done

                await context.SaveChangesAsync();

                DateTime birthDate = Convert.ToDateTime(dt_birth.EditValue);
                DateTime idDateIssued = Convert.ToDateTime(dt_dateissued.EditValue);
                //Activity logs below if fields are changed.. Saving using EF core.
                //If boolean was changed.
                if(defaultPantawid != ck_pantawid.Checked)
                {
                    string changedPantawid = $"[Pantawid] changed from '{defaultPantawid}' to '{ck_pantawid.Checked}'";
                    await _tableLog.InsertLogs(Id, changedPantawid, _username);

                }
                if (defaultIndigenous != ck_indigenous.Checked)
                {
                    string changeIndigeous = $"[Indigenous] changed from '{defaultIndigenous}' to '{ck_indigenous.Checked}'";
                    await _tableLog.InsertLogs(Id, changeIndigeous, _username);
                }

                /*Below is the Region, Province, Municipality and Barangay*/
                if (defaultRegion != cmb_region.Text)
                {
                    string changedRegion = $"[Region] transfered from '{defaultRegion}' to '{cmb_region.Text}' ";
                    await _tableLog.InsertLogs(Id, changedRegion, _username);
                }
                if (defaultProvince != cmb_province.Text)
                {
                    string changedProvince = $"[Province] transfered from '{defaultProvince}' to '{cmb_province.Text}'";
                    await _tableLog.InsertLogs(Id, changedProvince, _username);
                }
                if (defaultMunicipality != cmb_municipality.Text)
                {
                    string changedMunicipality = $"[Municipality] transfered from '{defaultMunicipality}' to '{cmb_municipality.Text}'";
                    await _tableLog.InsertLogs(Id, changedMunicipality, _username);
                }
                if (defaultBarangay != cmb_barangay.Text)
                {
                    string changedBarangay = $"[Barangay] transfered from '{defaultBarangay}' to '{cmb_barangay.Text}'";
                    await _tableLog.InsertLogs(Id, changedBarangay, _username);
                }
                //Address
                if (defaultAddress != txt_address.Text)
                {
                    string changedAddress = $"[Address] changed from '{defaultAddress}' to '{txt_address.Text}'";
                    await _tableLog.InsertLogs(Id, changedAddress, _username);
                }
                //Field continue below.
                if (defaultLastName != txt_lastname.Text)
                {
                    string changedLastname = $"[Last Name] changed from '{defaultLastName} to '{txt_lastname.Text}'";
                    //From our TableLogService.
                    await _tableLog.InsertLogs(Id, changedLastname, _username);
                }
                if (defaultFirstName != txt_firstname.Text)
                {
                    string changedFirstname = $"[First Name] changed from '{defaultFirstName}' to '{txt_firstname.Text}'";

                    await _tableLog.InsertLogs(Id, changedFirstname, _username);
                }
                if (defaultMiddleName != txt_middlename.Text)
                {
                    string changeMiddlename = $"[Middle Name] changed from '{defaultMiddleName}' to '{txt_middlename.Text}'";

                    await _tableLog.InsertLogs(Id, changeMiddlename, _username);
                }
                if (defaultExtName != txt_extname.Text)
                {
                    string changedExtname = $"[Extension Name] changed from '{defaultExtName}' to '{txt_extname.Text}'";
                    await _tableLog.InsertLogs(Id, changedExtname, _username);
                }
                if (defaultBirthDate != birthDate.Date)
                {
                    string changedDate = $"[Birth Date] changed from '{defaultBirthDate: yyyy-MM-dd}' to '{birthDate: yyyy-MM-dd}'";
                    await _tableLog.InsertLogs(Id, changedDate, _username);
                }
                if (defaultSex != cmb_sex.Text)
                {
                    string changedSex = $"[Sex] changed from '{defaultSex}' to '{cmb_sex.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedSex, _username);
                }
                if (defaultMarital != cmb_marital.Text)
                {
                    string changedMarital = $"[Marital Status] changed from '{defaultMarital}' to '{cmb_marital.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedMarital, _username);
                }
                //healthStatus id
                if (defaultHealthStatus != cmb_healthstatus.Text)
                {
                    string changedHealthStatus = $"[Health Status] changed from '{defaultHealthStatus}' to '{cmb_healthstatus.EditValue}";
                    await _tableLog.InsertLogs(Id, changedHealthStatus, _username);
                }
                //health status remarks
                if (defaultRemarks != txt_remarks.Text)
                {
                    string changedHealthStatus = $"[Health Remarks] changed from '{defaultRemarks}' to '{txt_remarks.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedHealthStatus, _username);
                }
                //Id number
                if (defaultIdNumber != txt_idno.Text)
                {
                    string changedIdNumber = $"[ID Number] changed from '{defaultIdNumber}' to '{txt_idno.Text}'";
                    await _tableLog.InsertLogs(Id, changedIdNumber, _username);
                }
                // Id Date issued
                if (defaultIdDateissued != idDateIssued.Date)
                {
                    string changedIdDateissued = $"[ID Date Issued] changed from '{defaultIdDateissued: yyyy-MM-dd}' to '{idDateIssued: yyyy-MM-dd}'";
                    await _tableLog.InsertLogs(Id, changedIdDateissued, _username);
                }
                //Citizenship
                if (defaultCitizenship != txt_citizenship.Text)
                {
                    string changedCitizenship = $"[Citizenship] changed from '{defaultCitizenship}' to '{txt_citizenship.Text}'";
                    await _tableLog.InsertLogs(Id, changedCitizenship, _username);
                }
                //Mothers maiden
                if (defaultMothersMaiden != txt_mothers.Text)
                {
                    string changedMothers = $"[Mothers Maiden] changed from '{defaultMothersMaiden}' to '{txt_mothers.Text}'";
                    await _tableLog.InsertLogs(Id, changedMothers, _username);
                }
                //Religion
                if (defaultReligion != txt_religion.Text)
                {
                    string changedReligion = $"[Religion] changed from '{defaultReligion}' to '{txt_religion.Text}'";
                    await _tableLog.InsertLogs(Id, changedReligion, _username);
                }
                //Birth Place
                if (defaultBirthPlace != txt_birthplace.Text)
                {
                    string changedBirthPlace = $"[Birth place] changed from '{defaultBirthPlace}' to '{txt_birthplace.Text}'";
                    await _tableLog.InsertLogs(Id, changedBirthPlace, _username);
                }
                //Educ attain
                if (defaultEducAttain != txt_educ.Text)
                {
                    string changedEducAttain = $"[Educational Attainment] changed from '{defaultEducAttain}' to '{txt_educ.Text}'";
                    await _tableLog.InsertLogs(Id, changedEducAttain, _username);
                }
                //Contact number
                if (defaultContactNo != txt_contact.Text)
                {
                    string changedContact = $"[Contact Number] changed from '{defaultContactNo}' to '{txt_contact.Text}'";
                    await _tableLog.InsertLogs(Id, changedContact, _username);
                }
                //Datasource
                if (defaultDataSource != cmb_datasource.Text)
                {
                    string changedDataSource = $"[Datasource] changed from '{defaultDataSource}' to '{cmb_datasource.Text}'";
                    await _tableLog.InsertLogs(Id, changedDataSource, _username);
                }

                XtraMessageBox.Show("Updated succesfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        //Retrieve the masterlist and fill the toolbox like text and comboboxes.
        private async Task masterListFill()
        {
            int id = Convert.ToInt32(txt_id.Text);
            MasterListModel masterListData = await Task.Run(() => _tableMasterlist.getById(id));
            if (masterListData != null)
            {
                txt_lastname.Text = masterListData.LastName;
                txt_firstname.Text = masterListData.FirstName;
                txt_middlename.Text = masterListData.MiddleName;
                txt_extname.Text = masterListData.ExtName;
                dt_birth.EditValue = masterListData.BirthDate;
                //Fill the sex combobox and label
                cmb_sex.EditValue = masterListData.LibrarySex.Sex;
                lblSex.Text = masterListData.SexID.ToString();
                // Fill the marital combobox and label
                cmb_marital.EditValue = masterListData.LibraryMaritalStatus.MaritalStatus;
                lblMaritalStatus.Text = masterListData.MaritalStatusID.ToString();
                //Fill the health status combobox and label
                cmb_healthstatus.EditValue = masterListData.LibraryHealthStatus.HealthStatus;
                lblHealthStatus.Text = masterListData.HealthStatusID.ToString();
                txt_remarks.Text = masterListData.HealthStatusRemarks;
                txt_idno.EditValue = masterListData.IDNumber;
                dt_dateissued.EditValue = masterListData.IDDateIssued;
                ck_pantawid.Checked = masterListData.Pantawid ?? false;
                ck_indigenous.Checked = masterListData.Indigenous ?? false;
                txt_citizenship.EditValue = masterListData.Citizenship;
                txt_mothers.EditValue = masterListData.MothersMaiden;
                txt_religion.EditValue = masterListData.Religion;
                txt_birthplace.EditValue = masterListData.BirthPlace;
                txt_educ.EditValue = masterListData.EducAttain;
                txt_contact.EditValue = masterListData.ContactNum;
                cmb_region.EditValue = masterListData.LibraryRegion.Region;
                lbl_region.Text = masterListData.PSGCRegion.ToString();
                cmb_province.EditValue = masterListData.LibraryProvince.ProvinceName;
                lbl_province.Text = masterListData.PSGCProvince.ToString();
                cmb_municipality.EditValue = masterListData.LibraryMunicipality.CityMunName;
                lbl_municipality.Text = masterListData.PSGCCityMun.ToString();
                cmb_barangay.EditValue = masterListData.LibraryBarangay.BrgyName;
                lbl_barangay.Text = masterListData.PSGCBrgy.ToString();
                txt_address.EditValue = masterListData.Address;
                cmb_datasource.EditValue = masterListData.LibraryDataSource.DataSource;
                lblDatasource.Text = masterListData.DataSourceId.ToString();
                txt_householdsize.EditValue = masterListData.GisModels.Select(x => x.HouseholdSize)
                    .FirstOrDefault();
                //Assessment retrieval
                cmb_assessment.EditValue = masterListData.GisModels.Select(s => s.LibraryAssessment.Assessment)
                    .FirstOrDefault();
                lbl_assessment.Text = masterListData.GisModels.Select(g => g.AssessmentID.ToString())
                    .FirstOrDefault();
                //Validator retrieval
                cmb_validator.EditValue = masterListData.GisModels.Select(v => v.LibraryValidator.Validator)
                    .FirstOrDefault();
                lbl_validator.Text = masterListData.GisModels.Select(v => v.ValidatedByID.ToString())
                    .FirstOrDefault();
                dt_accomplished.EditValue = masterListData.GisModels.Select(d => d.ValidationDate)
                    .FirstOrDefault();
            }

        }
        private void Loading()
        {
            xtraScrollableControl1.Enabled = false;
        }
        private void DoneLoading()
        {
            xtraScrollableControl1.Enabled = true;
        }
        //Below is the alternative way to load methods inside our winforms
        //protected override async void OnLoad(EventArgs e)
        //{
            
        //}
        private async Task AllMethods()
        {
            await DataSourceEF();
            await MaritalEf();
            await SexEf();
            await HealthStatusEf();
            await AssessmentEf();
            await ValidatorEf();
            await LoadRegions();
            await Provinces();
            await Municipalities();
            await Barangays();
        }
        public void DisplaySPBUF(int spbuf)
        {
            // Display the SPBUF value in a label or textbox on your form
            txt_referencecode.Text = spbuf.ToString(); // Assuming txt_referencecode is a TextBox on your form
            LoadAndDisplayPdfAsync(spbuf.ToString(), @"\\172.26.153.181\spbuf");
        }

        public void DisplayGIS(int gis)
        {
            // Display the GIS ID in a label or textbox on your form
            txt_referencecode.Text = gis.ToString(); // Assuming txt_referencecode is a TextBox on your form
            LoadAndDisplayPdfAsync(gis.ToString(), @"\\172.26.153.181\gis");
        }

        private void LoadAndDisplayPdfAsync(string referenceCode, string folderPath)
        {
            try
            {
                string pdfPath = Path.Combine(folderPath, referenceCode + ".PDF");

                if (File.Exists(pdfPath))
                {
                    pdfViewer1.LoadDocument(pdfPath);
                }
                //else
                //{
                //    XtraMessageBox.Show("PDF file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task DataSourceEF()
        {
            using (var context = new ApplicationDbContext())
            {
                var libDatasources = await context.lib_datasource
                     .AsNoTracking()
                     .ToListAsync();
                cmb_datasource.Properties.Items.Clear();
                foreach (var libDataSource in libDatasources)
                {
                    cmb_datasource.Properties.Items.Add(new LibraryDataSource
                    {
                        Id = libDataSource.Id,
                        DataSource = libDataSource.DataSource
                    });

                }

            }
        }

        public async Task SexEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var librarySexs = await context.lib_sex
                    .AsNoTracking()
                    .ToListAsync();

                cmb_sex.Properties.Items.Clear();
                foreach (var librarySex in librarySexs)
                {
                    cmb_sex.Properties.Items.Add(new LibrarySex
                    {
                        Id = librarySex.Id,
                        Sex = librarySex.Sex
                    });
                }
            }
        }
        public async Task MaritalEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var libraryMaritals = await context.lib_marital_status
                    .AsNoTracking()
                    .ToListAsync();

                cmb_marital.Properties.Items.Clear();
                foreach (var libraryMarital in libraryMaritals)
                {
                    cmb_marital.Properties.Items.Add(new LibraryMaritalStatus
                    {
                        Id = libraryMarital.Id,
                        MaritalStatus = libraryMarital.MaritalStatus
                    });
                }
            }
        }
        public async Task AssessmentEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var libraryAssessments = await context.lib_assessment
                    .AsNoTracking()
                    .ToListAsync();

                cmb_assessment.Properties.Items.Clear();
                foreach (var libraryAssessment in libraryAssessments)
                {
                    cmb_assessment.Properties.Items.Add(new LibraryAssessment
                    {
                        Id = libraryAssessment.Id,
                        Assessment = libraryAssessment.Assessment
                    });
                }
            }
        }

        public async Task ValidatorEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var libraryValidators = await context.lib_validator
                    .AsNoTracking()
                    .ToListAsync();

                cmb_validator.Properties.Items.Clear();
                foreach (var libraryValidator in libraryValidators)
                {
                    cmb_validator.Properties.Items.Add(new LibraryValidator
                    {
                        Id = libraryValidator.Id,
                        Validator = libraryValidator.Validator
                    });
                }
            }
        }


        public async Task HealthStatusEf()
        {
            using (var context = new ApplicationDbContext())
            {
                var libraryHealthStatuses = await context.lib_health_status
                    .AsNoTracking()
                    .ToListAsync();

                cmb_healthstatus.Properties.Items.Clear();
                foreach (var libraryHealthStatus in libraryHealthStatuses)
                {
                    cmb_healthstatus.Properties.Items.Add(new LibraryHealthStatus
                    {
                        Id = libraryHealthStatus.Id,
                        HealthStatus = libraryHealthStatus.HealthStatus
                    });
                }
            }
        }
        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Event handler for the DateTimePicker ValueChanged event
        private void Dt_birth_ValueChanged(object sender, EventArgs e)
        {
            // Calculate and display the age
            DisplayAge();
        }
        // Method to calculate and display the age
        private void DisplayAge()
        {
            if (dt_birth.EditValue != null && DateTime.TryParse(dt_birth.EditValue.ToString(), out DateTime birthDate))
            {
                DateTime today = DateTime.Today;

                // Calculate age
                int age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }

                // Display age in label
                lbl_age.Text = $"[{age}]";
            }
            else
            {
                lbl_age.Text = "0";
            }
        }

        public void Clear()
        {
            txt_lastname.Text = "";
            txt_firstname.Text = "";
            txt_middlename.Text = "";
            txt_extname.Text = "";
            txt_address.Text = "";
            dt_birth.Text = "";
            txt_id.Text = "";
            dt_dateissued.Text = "";
            cmb_sex.Text = "";
            ck_pantawid.Checked = false;
            ck_indigenous.Checked = false;
            txt_lastname.Focus();

        }


        private async Task UpdateGISEF()
        {
            int masterlistID = Convert.ToInt32(txt_id.Text);

            using (var context = new ApplicationDbContext())
            {
                var updateGIS = await context.tbl_gis.FirstOrDefaultAsync(x => x.MasterListID == masterlistID);
                if (updateGIS != null)
                {
                    updateGIS.HouseholdSize = Convert.ToInt32(txt_householdsize.EditValue);
                    updateGIS.AssessmentID = Convert.ToInt32(lbl_assessment.Text);
                    updateGIS.ValidatedByID = Convert.ToInt32(lbl_validator.Text);
                    updateGIS.ValidationDate = Convert.ToDateTime(dt_accomplished.EditValue);
                    await context.SaveChangesAsync();
                }
                else
                {
                    //  MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
  
        private async Task SaveGISEF()
        {
            if (selectedPdfPath == null)
            {
                XtraMessageBox.Show("Please import a PDF file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using(var context = new ApplicationDbContext())
            {
                var gis = new GisModel
                {
                    MasterListID = Convert.ToInt32(txt_id.Text),
                    HouseholdSize = Convert.ToInt32(txt_householdsize.Text),
                    AssessmentID = Convert.ToInt32(lbl_assessment.Text),
                    ValidatedByID = Convert.ToInt32(lbl_validator.Text),
                    ValidationDate = Convert.ToDateTime(dt_accomplished.EditValue),
                    EntryBy = _username,
                    EntryDateTime = DateTime.Now
                };
                context.tbl_gis.Add(gis);
                await context.SaveChangesAsync();
                
            }

            // Save the PDF file to network path with custom filename based on referenceCode
            string fileName = Path.GetFileName(selectedPdfPath);
            //string destinationPath = @"\\172.26.153.181\gis\" + referenceCode + ".pdf";
            string destinationPath = @"\\172.26.153.181\gis\" + lbl_reference.Text + ".pdf";
            //string destinationPath = @"E:\SPInS Documents\" + lbl_reference.Text + ".pdf";
            File.Copy(selectedPdfPath, destinationPath, true);


            this.Close();
            //masterlistForm.ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.

            XtraMessageBox.Show("GIS and PDF saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void btn_edit_Click(object sender, EventArgs e)
        {
            //For editing.
            if (cmb_barangay.Text == "")
            {
                MessageBox.Show("Please select Barangay before updating", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmb_healthstatus.Text == "")
            {
                MessageBox.Show("Please enter health status before updating", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ck_new.Checked == true)
            {
                if (cmb_assessment.Text == "")
                {
                    MessageBox.Show("Please select Assessment before saving", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmb_validator.Text == "")
                {
                    MessageBox.Show("Please select Validator before saving", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dt_accomplished.Text == "")
                {
                    MessageBox.Show("Please select Date Accomplished before saving", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await SaveGISEF();
                return;
            }
            if (txt_referencecode.Text == "")
            {
                await UpdateMaster();
                return;
            }
            await UpdateGISEF();
            await UpdateMaster();


        }

        private async void txt_id_EditValueChanged(object sender, EventArgs e)
        {
            await masterListFill(); // Retrieve the details for every text/combo field.
        }

        private async void cmb_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSex = cmb_sex.Text;
            using (var context = new ApplicationDbContext())
            {
                var sex = await context.lib_sex.FirstOrDefaultAsync(s => s.Sex == selectedSex);
                lblSex.Text = sex.Id.ToString();
            }

        }

        private async void cmb_marital_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMarital = cmb_marital.Text;
            using (var context = new ApplicationDbContext())
            {
                var maritalStatus = await context.lib_marital_status.FirstOrDefaultAsync(s => s.MaritalStatus == selectedMarital);
                lblMaritalStatus.Text = maritalStatus.Id.ToString();
            }
        }

        private async void cmb_healthstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHealthStatus = cmb_healthstatus.Text;
            using (var context = new ApplicationDbContext())
            {
                var healthStatus = await context.lib_health_status
                    .FirstOrDefaultAsync(h => h.HealthStatus == selectedHealthStatus);
                lblHealthStatus.Text = healthStatus.Id.ToString();
            }
        }

        private async void cmb_datasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDataSource = cmb_datasource.Text;
            using (var context = new ApplicationDbContext())
            {
                var dataSource = await context.lib_datasource
                    .FirstOrDefaultAsync(d => d.DataSource == selectedDataSource);
                lblDatasource.Text = dataSource.Id.ToString();
            }
        }

        private async void cmb_municipality_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmb_municipality.SelectedItem != null)
            {

                var selectedMunicipality = cmb_municipality.SelectedItem as LibraryMunicipality;
                if (selectedMunicipality != null)
                {
                    int selectedMunicipalityId = selectedMunicipality.PSGCCityMun; // get the psgcregion
                    await LoadBarangays(selectedMunicipalityId);
                }

                //When combobox region was clicked then fill the label.
                string fillMunicipality = cmb_municipality.Text;
                using (var context = new ApplicationDbContext())
                {
                    var municipality = await context.lib_city_municipality.FirstOrDefaultAsync(p => p.CityMunName == fillMunicipality);
                    lbl_municipality.Text = municipality.PSGCCityMun.ToString();

                }

                cmb_barangay.EditValue = "";

            }
        }

        public async Task LoadRegions()
        {
            using (var context = new ApplicationDbContext())
            {
                var regions = await context.lib_region_fortesting
                    //.Where(r => r.PSGCRegion == 160000000)
                    .AsNoTracking()
                    .ToListAsync();
                cmb_region.Properties.Items.Clear();
                foreach (var region in regions)
                {
                    cmb_region.Properties.Items.Add(new LibraryRegion
                    {
                        Id = region.Id,
                        PSGCRegion = region.PSGCRegion,
                        Region = region.Region

                    });
                }


            }
        }
        public async Task Provinces()
        {
            using (var context = new ApplicationDbContext())
            {
                var provinces = await context.lib_province
                    .AsNoTracking()
                    .ToListAsync();

                cmb_province.Properties.Items.Clear();
                foreach (var province in provinces)
                {
                    cmb_province.Properties.Items.Add(new LibraryProvince
                    {
                        PSGCProvince = province.PSGCProvince,
                        ProvinceName = province.ProvinceName,
                        PSGCRegion = province.PSGCRegion
                    });
                }
            }
        }
        public async Task LoadProvinces(int regionId)
        {
            using (var context = new ApplicationDbContext())
            {
                var provinces = await context.lib_province
                    .Where(p => p.PSGCRegion == regionId)
                    .ToListAsync();

                cmb_province.Properties.Items.Clear();
                foreach (var province in provinces)
                {
                    cmb_province.Properties.Items.Add(new LibraryProvince
                    {
                        PSGCProvince = province.PSGCProvince,
                        ProvinceName = province.ProvinceName,
                        PSGCRegion = province.PSGCRegion
                    });
                }
            }
        }
        public async Task Municipalities()
        {
            using (var context = new ApplicationDbContext())
            {
                var municipalities = await context.lib_city_municipality
                    .AsNoTracking()
                    .ToListAsync();
                cmb_municipality.Properties.Items.Clear();
                foreach (var municipality in municipalities)
                {
                    cmb_municipality.Properties.Items.Add(new LibraryMunicipality
                    {
                        PSGCCityMun = municipality.PSGCCityMun,
                        CityMunName = municipality.CityMunName,
                        PSGCProvince = municipality.PSGCProvince,
                        District = municipality.District
                    });
                }
            }
        }

        public async Task LoadMunicipalities(int provinceId)
        {
            using (var context = new ApplicationDbContext())
            {
                var municipalities = await context.lib_city_municipality
                    .Where(m => m.PSGCProvince == provinceId)
                    .ToListAsync();
                cmb_municipality.Properties.Items.Clear();
                foreach (var municipality in municipalities)
                {
                    cmb_municipality.Properties.Items.Add(new LibraryMunicipality
                    {
                        PSGCCityMun = municipality.PSGCCityMun,
                        CityMunName = municipality.CityMunName,
                        PSGCProvince = municipality.PSGCProvince,
                        District = municipality.District
                    });
                }
            }
        }
        public async Task Barangays()
        {
            using (var context = new ApplicationDbContext())
            {
                var barangays = await context.lib_barangay
                    .AsNoTracking()
                    .ToListAsync();

                cmb_barangay.Properties.Items.Clear();
                foreach (var barangay in barangays)
                {
                    cmb_barangay.Properties.Items.Add(new LibraryBarangay
                    {
                        PSGCBrgy = barangay.PSGCBrgy,
                        BrgyName = barangay.BrgyName,
                        PSGCCityMun = barangay.PSGCCityMun
                    });
                }
            }

        }


        public async Task LoadBarangays(int municipalityId)
        {
            using (var context = new ApplicationDbContext())
            {
                var barangays = await context.lib_barangay
                    .Where(p => p.PSGCCityMun == municipalityId)
                    .ToListAsync();

                cmb_barangay.Properties.Items.Clear();
                foreach (var barangay in barangays)
                {
                    cmb_barangay.Properties.Items.Add(new LibraryBarangay
                    {
                        PSGCBrgy = barangay.PSGCBrgy,
                        BrgyName = barangay.BrgyName,
                        PSGCCityMun = barangay.PSGCCityMun
                    });
                }
            }

        }
        private async void cmb_region_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_region.SelectedItem != null)
            {
                var selectedRegion = cmb_region.SelectedItem as LibraryRegion;
                int selectedRegionId = selectedRegion.PSGCRegion; // get the psgcregion
                await LoadProvinces(selectedRegionId);

                //When combobox region was clicked then fill the label.
                string fillRegion = cmb_region.Text;
                using (var context = new ApplicationDbContext())
                {
                    var region = await context.lib_region_fortesting.FirstOrDefaultAsync(r => r.Region == fillRegion);
                    lbl_region.Text = region.PSGCRegion.ToString();
                }

                cmb_province.EditValue = "";
                cmb_municipality.EditValue = "";
                cmb_barangay.EditValue = "";
            }

        }

        private void cmb_province_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_province_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cmb_province_TextChanged(object sender, EventArgs e)
        {

        }

        private async void cmb_barangay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When combobox region was clicked then fill the label.
            string fillBarangay = cmb_barangay.Text;
            using (var context = new ApplicationDbContext())
            {
                var barangay = await context.lib_barangay.FirstOrDefaultAsync(p => p.BrgyName == fillBarangay);
                lbl_barangay.Text = barangay.PSGCBrgy.ToString();
            }
        }
        private void GenerateReferenceCode()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT MAX(g.ReferenceCode) AS LastReferenceCode
                            FROM tbl_gis g";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0 && dt.Rows[0]["LastReferenceCode"] != DBNull.Value)
                {
                    // Retrieve the last reference code from the DataTable
                    int lastReferenceCode = Convert.ToInt32(dt.Rows[0]["LastReferenceCode"]);

                    // Increment the last reference code by one
                    int newReferenceCode = lastReferenceCode + 1;

                    // Assign the new reference code to your label
                    lbl_reference.Text = newReferenceCode.ToString();
                }
                else
                {
                    // Handle the case where no valid last reference code exists
                    lbl_reference.Text = "1"; // Assuming starting from 1 if no records exist
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void ClearPdfViewer()
        {
            pdfViewer1.LoadDocument(@"\\172.26.153.181\gis\FILEDOESNOTEXIST.pdf");
        }
        private void ck_new_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_new.Checked == true)
            {
                if (XtraMessageBox.Show("You are about to upload a new GIS. Proceed?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grp_gis.Enabled = true;
                    //Clear fields once new gis was created.
                    GenerateReferenceCode(); // This is to generate random reference code into our txt_referencecode.
                                             // Replace 'empty.pdf' with the path to your empty PDF file
                                             //pdfViewer1.LoadDocument("empty.pdf"); // Clears the PDF document
                    ClearPdfViewer();
                    txt_householdsize.Text = "0";
                    cmb_assessment.Text = "";
                    lbl_assessment.Text = "0";
                    cmb_validator.Text = "";
                    lbl_validator.Text = "0";
                    dt_accomplished.Text = "";
                    btn_import.Enabled = true;
                    return;

                }

                ck_new.Checked = false;
            }
            else
            {
                //con.Close();
                //LoadDataAsync();
                //DisplayGIS(int.Parse(txt_referencecode.Text)); 
                //DisplaySPBUF(int.Parse(txt_referencecode.Text));

                if (!string.IsNullOrEmpty(txt_referencecode.Text))
                {
                    int referenceCode;
                    if (int.TryParse(txt_referencecode.Text, out referenceCode))
                    {
                        // Try to display GIS first
                        DisplayGIS(referenceCode);
                    }
                    else
                    {
                        // Fall back to displaying SPBUF
                        DisplaySPBUF(int.Parse(txt_referencecode.Text));
                    }
                }
                lbl_reference.Text = "----------";
                btn_import.Enabled = false;
                //grp_gis.Enabled = false;

            }
        }

        private async void cmb_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAssessment = cmb_assessment.Text;
            using (var context = new ApplicationDbContext())
            {
                var assessment = await context.lib_assessment
                    .FirstOrDefaultAsync(a => a.Assessment == selectedAssessment);
                lbl_assessment.Text = assessment.Id.ToString();
            }

        }

        private async void cmb_validator_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedValidator = cmb_validator.Text;
            using (var context = new ApplicationDbContext())
            {
                var validator = await context.lib_validator
                    .FirstOrDefaultAsync(v => v.Validator == selectedValidator);
                lbl_validator.Text = validator.Id.ToString();
            }

        }

        private string selectedPdfPath; // Store selected PDF path for later use
        private void btn_import_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files|*.pdf";
            openFileDialog.Title = "Select a PDF File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Display selected PDF in PdfViewer or store path for later use
                pdfViewer1.LoadDocument(openFileDialog.FileName);
                selectedPdfPath = openFileDialog.FileName;
            }
        }

        private void xtraScrollableControl1_Click(object sender, EventArgs e)
        {

        }
        AuthorizeRepresentative AuthorizeRepresentativeForm;
        private void btn_authrep_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AuthorizeRepresentative>().Any())
            {
                AuthorizeRepresentativeForm.Select();
                AuthorizeRepresentativeForm.BringToFront();
            }
            else
            {
                if (txt_referencecode.Text == "")
                {
                    XtraMessageBox.Show("There is no Validation form uploaded on this particular beneficiary, please create and upload first to proceed.", "Empty Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                AuthorizeRepresentativeForm = new AuthorizeRepresentative(this);

                // Pass the ID value to the EditApplicant form
                int reference = Convert.ToInt32(txt_referencecode.Text);
                int id = Convert.ToInt32(txt_id.Text);

                AuthorizeRepresentativeForm.DisplayReference(reference);
                AuthorizeRepresentativeForm.DisplayID(id);
                AuthorizeRepresentativeForm.Show();


            }
        }


        private void EditApplicant_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close the AuthorizeRepresentativeForm only if it has been opened
            if (AuthorizeRepresentativeForm != null)
            {
                AuthorizeRepresentativeForm.Close();
                AuthorizeRepresentativeForm = null; // Clear the reference
            }
        }


        private async void cmb_province_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if (cmb_province.SelectedItem != null)
            {
                var selectedProvince = cmb_province.SelectedItem as LibraryProvince;
                int selectedProvinceId = selectedProvince.PSGCProvince; // get the psgcregion
                await LoadMunicipalities(selectedProvinceId);

                //When combobox region was clicked then fill the label.
                string fillProvince = cmb_province.Text;
                using (var context = new ApplicationDbContext())
                {
                    var province = await context.lib_province.FirstOrDefaultAsync(p => p.ProvinceName == fillProvince);
                    lbl_province.Text = province.PSGCProvince.ToString();
                }
                cmb_municipality.EditValue = "";
                cmb_barangay.EditValue = "";
            }
        }

        private async void EditApplicant_Load(object sender, EventArgs e)
        {
            Loading();
            //base.OnLoad(e);
            await tableLog();// Retrieve the logs
            await AllMethods();
            DisplayAge();// to display age in a label
            DoneLoading();
        }
    }
}
