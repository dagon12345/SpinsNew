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
                updateMaster.Citizenship = txt_citizenship.Text;//Done
                updateMaster.MothersMaiden = txt_mothers.Text;
                updateMaster.Religion = txt_religion.Text;
                updateMaster.BirthPlace = txt_birthplace.Text;
                updateMaster.EducAttain = txt_educ.Text;
                updateMaster.ContactNum = txt_contact.Text;
                updateMaster.DataSourceId = Convert.ToInt32(lblDatasource.Text);

                updateMaster.PSGCRegion = Convert.ToInt32(lbl_region.Text);
                updateMaster.PSGCProvince = Convert.ToInt32(lbl_province.Text);
                updateMaster.PSGCCityMun = Convert.ToInt32(lbl_municipality.Text);
                updateMaster.PSGCBrgy = Convert.ToInt32(lbl_barangay.Text);

                await context.SaveChangesAsync();

                DateTime birthDate = Convert.ToDateTime(dt_birth.EditValue);
                DateTime idDateIssued = Convert.ToDateTime(dt_dateissued.EditValue);
                //Activity logs below if fields are changed.. Saving using EF core.
                if(defaultLastName != txt_lastname.Text)
                {
                    string changedLastname = $"[Last Name] changed from '{defaultLastName} to '{txt_lastname.Text}'";
                    //From our TableLogService.
                    await _tableLog.InsertLogs(Id, changedLastname, _username);
                }
                if(defaultFirstName != txt_firstname.Text)
                {
                    string changedFirstname = $"[First Name] changed from '{defaultFirstName}' to '{txt_firstname.Text}'";

                    await _tableLog.InsertLogs(Id, changedFirstname, _username);
                }
                if(defaultMiddleName != txt_middlename.Text)
                {
                    string changeMiddlename = $"[Middle Name] changed from '{defaultMiddleName}' to '{txt_middlename.Text}'";

                    await _tableLog.InsertLogs(Id, changeMiddlename, _username);
                }
                if(defaultExtName != txt_extname.Text)
                {
                    string changedExtname = $"[Extension Name] changed from '{defaultExtName}' to '{txt_extname.Text}'";
                    await _tableLog.InsertLogs(Id, changedExtname, _username);
                }
                if(defaultBirthDate != birthDate.Date)
                {
                    string changedDate = $"[Birth Date] changed from '{defaultBirthDate: yyyy-MM-dd}' to '{birthDate: yyyy-MM-dd}'";
                    await _tableLog.InsertLogs(Id, changedDate, _username);
                }
                if(defaultSex != cmb_sex.Text)
                {
                    string changedSex = $"[Sex] changed from '{defaultSex}' to '{cmb_sex.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedSex, _username);
                }
                if(defaultMarital != cmb_marital.Text)
                {
                    string changedMarital = $"[Marital Status] changed from '{defaultMarital}' to '{cmb_marital.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedMarital, _username);
                }
                //healthStatus id
                if(defaultHealthStatus != cmb_healthstatus.Text)
                {
                    string changedHealthStatus = $"[Health Status] changed from '{defaultHealthStatus}' to '{cmb_healthstatus.EditValue}";
                    await _tableLog.InsertLogs(Id, changedHealthStatus, _username);
                }
                //health status remarks
                if(defaultRemarks != txt_remarks.Text)
                {
                    string changedHealthStatus = $"[Health Remarks] changed from '{defaultRemarks}' to '{txt_remarks.EditValue}'";
                    await _tableLog.InsertLogs(Id, changedHealthStatus, _username);
                }
                //Id number
                if(defaultIdNumber != txt_idno.Text)
                {
                    string changedIdNumber = $"[ID Number] changed from '{defaultIdNumber}' to '{txt_idno.Text}'";
                    await _tableLog.InsertLogs(Id, changedIdNumber, _username);
                }
                // Id Date issued
                if(defaultIdDateissued != idDateIssued.Date)
                {
                    string changedIdDateissued = $"[ID Date Issued] changed from '{defaultIdDateissued: yyyy-MM-dd}' to '{idDateIssued: yyyy-MM-dd}'";
                    await _tableLog.InsertLogs(Id, changedIdDateissued, _username);
                }
                 //Citizenship
                if(defaultCitizenship != txt_citizenship.Text)
                {
                    string changedCitizenship = $"[Citizenship] changed from '{defaultCitizenship}' to '{txt_citizenship.Text}'";
                    await _tableLog.InsertLogs(Id, changedCitizenship, _username);
                }
                
                XtraMessageBox.Show("Updated succesfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }


        }

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
        protected override async void OnLoad(EventArgs e)
        {
            Loading();
            base.OnLoad(e);

            await tableLog();// Retrieve the logs


            await AllMethods();


            DisplayAge();// to display age in a label

            DoneLoading();
        }
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
        // Other methods and controls
        private void EditApplicant_Load(object sender, EventArgs e)
        {
            //Call the methods below to fill the comboboxes

            // InitializeComboboxes();
            //cmb_municipality.SelectedIndexChanged += cmb_municipality_SelectedIndexChanged;
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

        //Get RegionName
        public string GetRegionName(int psgcRegion)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Region FROM lib_region WHERE PSGCRegion = @PSGCRegion";
                cmd.Parameters.AddWithValue("@PSGCRegion", psgcRegion);
                string regionName = cmd.ExecuteScalar()?.ToString();
                con.Close();
                return regionName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //get province cascading
        public (string provinceName, int psgcRegion) GetProvinceNameAndRegion(int psgcProvince)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ProvinceName, PSGCRegion FROM lib_province WHERE PSGCProvince = @PSGCProvince";
                cmd.Parameters.AddWithValue("@PSGCProvince", psgcProvince);
                MySqlDataReader reader = cmd.ExecuteReader();
                string provinceName = null;
                int psgcRegion = 0;

                if (reader.Read())
                {
                    provinceName = reader["ProvinceName"].ToString();
                    psgcRegion = Convert.ToInt32(reader["PSGCRegion"]);
                }

                reader.Close();
                con.Close();
                return (provinceName, psgcRegion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (null, 0);
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
            //cmb_marital.Text = "";
            //txt_remarks.Text = "";
            ck_pantawid.Checked = false;
            ck_indigenous.Checked = false;
            // gr_similar.DataSource = null;
            txt_lastname.Focus();

        }

        //Updating the masterlist on given property.
        //private void UpdateMaster()
        //{
        //    try
        //    {
        //        con.Open();
        //        MySqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;

        //        // First query to retrieve the current state
        //        cmd.CommandText = @"
        //SELECT 
        //    m.LastName,
        //    m.FirstName,
        //    m.MiddleName,
        //    m.ExtName,
        //    m.BirthDate,
        //    m.SexID,
        //    m.MaritalStatusID,
        //    m.HealthStatusID,
        //    m.HealthStatusRemarks,
        //    m.IDNumber,
        //    m.IDDateIssued,
        //    m.Pantawid,
        //    m.Indigenous,
        //    m.Citizenship,
        //    m.MothersMaiden,
        //    m.Religion,
        //    m.BirthPlace,
        //    m.EducAttain,
        //    m.ContactNum,
        //    m.DataSourceID,
        //    m.PSGCRegion,
        //    m.PSGCProvince,
        //    m.PSGCCityMun,
        //    m.PSGCBrgy
        //FROM 
        //    tbl_masterlist m
        //WHERE 
        //    m.ID = @IDNumber";
        //        cmd.Parameters.AddWithValue("@IDNumber", txt_id.Text);

        //        DataTable dtOld = new DataTable();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(dtOld);

        //        if (dtOld.Rows.Count > 0)
        //        {
        //            DataRow oldRow = dtOld.Rows[0];

        //            // Fetch current sex name
        //            string sexNameBefore = "";
        //            string sexNameAfter = "";

        //            if (oldRow["SexID"] != DBNull.Value)
        //            {
        //                int sexIDBefore = Convert.ToInt32(oldRow["SexID"]);
        //                MySqlCommand sexCmdBefore = con.CreateCommand();
        //                sexCmdBefore.CommandType = CommandType.Text;
        //                sexCmdBefore.CommandText = "SELECT Sex FROM lib_sex WHERE Id = @SexID";
        //                sexCmdBefore.Parameters.AddWithValue("@SexID", sexIDBefore);
        //                sexNameBefore = sexCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int sexIDAfter = Convert.ToInt32(lbl_sex.Text); // Assuming lbl_sex.Text contains the updated SexID

        //            MySqlCommand sexCmdAfter = con.CreateCommand();
        //            sexCmdAfter.CommandType = CommandType.Text;
        //            sexCmdAfter.CommandText = "SELECT Sex FROM lib_sex WHERE Id = @SexID";
        //            sexCmdAfter.Parameters.AddWithValue("@SexID", sexIDAfter);
        //            sexNameAfter = sexCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Fetch current maritalstatus name
        //            string maritalNameBefore = "";
        //            string maritalNameAfter = "";

        //            if (oldRow["MaritalStatusID"] != DBNull.Value)
        //            {
        //                int maritalIDBefore = Convert.ToInt32(oldRow["MaritalStatusID"]);
        //                MySqlCommand maritalCmdBefore = con.CreateCommand();
        //                maritalCmdBefore.CommandType = CommandType.Text;
        //                maritalCmdBefore.CommandText = "SELECT MaritalStatus FROM lib_marital_status WHERE Id = @MaritalStatusID";
        //                maritalCmdBefore.Parameters.AddWithValue("@MaritalStatusID", maritalIDBefore);
        //                maritalNameBefore = maritalCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int maritalIDAfter = Convert.ToInt32(lbl_marital.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

        //            MySqlCommand maritalCmdAfter = con.CreateCommand();
        //            maritalCmdAfter.CommandType = CommandType.Text;
        //            maritalCmdAfter.CommandText = "SELECT MaritalStatus FROM lib_marital_status WHERE Id = @MaritalStatusID";
        //            maritalCmdAfter.Parameters.AddWithValue("@MaritalStatusID", maritalIDAfter);
        //            maritalNameAfter = maritalCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Fetch current HealthStatus name
        //            string healthStatusBefore = "";
        //            string healthStatusAfter = "";

        //            if (oldRow["HealthStatusID"] != DBNull.Value)
        //            {
        //                int healthstatusIDBefore = Convert.ToInt32(oldRow["HealthStatusID"]);
        //                MySqlCommand healthstatusCmdBefore = con.CreateCommand();
        //                healthstatusCmdBefore.CommandType = CommandType.Text;
        //                healthstatusCmdBefore.CommandText = "SELECT HealthStatus FROM lib_health_status WHERE Id = @HealthStatusID";
        //                healthstatusCmdBefore.Parameters.AddWithValue("@HealthStatusID", healthstatusIDBefore);
        //                healthStatusBefore = healthstatusCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int healthstatusIDAfter = Convert.ToInt32(lbl_healthstatus.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

        //            MySqlCommand healthstatusCmdAfter = con.CreateCommand();
        //            healthstatusCmdAfter.CommandType = CommandType.Text;
        //            healthstatusCmdAfter.CommandText = "SELECT HealthStatus FROM lib_health_status WHERE Id = @HealthStatusID";
        //            healthstatusCmdAfter.Parameters.AddWithValue("@HealthStatusID", healthstatusIDAfter);
        //            healthStatusAfter = healthstatusCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Fetch current Municipality name
        //            string municipalityBefore = "";
        //            string municipalityAfter = "";

        //            if (oldRow["PSGCCityMun"] != DBNull.Value)
        //            {
        //                int municipalityIDBefore = Convert.ToInt32(oldRow["PSGCCityMun"]);

        //                MySqlCommand municipalityCmdBefore = con.CreateCommand();
        //                municipalityCmdBefore.CommandType = CommandType.Text;
        //                municipalityCmdBefore.CommandText = "SELECT CityMunName FROM lib_city_municipality WHERE PSGCCityMun = @PSGCCityMun";
        //                municipalityCmdBefore.Parameters.AddWithValue("@PSGCCityMun", municipalityIDBefore);
        //                municipalityBefore = municipalityCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int municipalityIDAfter = Convert.ToInt32(lbl_municipality.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

        //            MySqlCommand municipalityCmdAfter = con.CreateCommand();
        //            municipalityCmdAfter.CommandType = CommandType.Text;
        //            municipalityCmdAfter.CommandText = "SELECT CityMunName FROM lib_city_municipality WHERE PSGCCityMun = @PSGCCityMun";
        //            municipalityCmdAfter.Parameters.AddWithValue("@PSGCCityMun", municipalityIDAfter);
        //            municipalityAfter = municipalityCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Fetch current Datasource name
        //            string datasourceBefore = "";
        //            string datasourceAfter = "";

        //            if (oldRow["DataSourceID"] != DBNull.Value)
        //            {
        //                int datasourceIDBefore = Convert.ToInt32(oldRow["DataSourceID"]);

        //                MySqlCommand datasourceCmdBefore = con.CreateCommand();
        //                datasourceCmdBefore.CommandType = CommandType.Text;
        //                datasourceCmdBefore.CommandText = "SELECT DataSource FROM lib_datasource WHERE ID = @DataSourceID";
        //                datasourceCmdBefore.Parameters.AddWithValue("@DataSourceID", datasourceIDBefore);
        //                datasourceBefore = datasourceCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int datasourceIDAfter = Convert.ToInt32(lbl_datasource.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

        //            MySqlCommand datasourceCmdAfter = con.CreateCommand();
        //            datasourceCmdAfter.CommandType = CommandType.Text;
        //            datasourceCmdAfter.CommandText = "SELECT DataSource FROM lib_datasource WHERE ID = @DataSourceID";
        //            datasourceCmdAfter.Parameters.AddWithValue("@DataSourceID", datasourceIDAfter);
        //            datasourceAfter = datasourceCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Perform the update
        //            cmd.CommandText = @"
        //    UPDATE 
        //        tbl_masterlist 
        //    SET
        //        LastName = @LastName,
        //        FirstName = @FirstName,
        //        MiddleName = @MiddleName,
        //        ExtName = @ExtName,
        //        BirthDate = @BirthDate,
        //        SexID = @SexID,
        //        MaritalStatusID = @MaritalStatusID,
        //        HealthStatusID = @HealthStatusID,
        //        HealthStatusRemarks = @HealthStatusRemarks,
        //        IDNumber = @IDNumber,
        //        IDDateIssued = @IDDateIssued,
        //        Pantawid = @Pantawid,
        //        Indigenous = @Indigenous,
        //        Citizenship = @Citizenship,
        //        MothersMaiden = @MothersMaiden,
        //        Religion = @Religion,
        //        Birthplace = @BirthPlace,
        //        EducAttain = @EducAttain,
        //        ContactNum = @ContactNum,
        //        DataSourceID = @DataSourceID,
        //        PSGCRegion = @PSGCRegion,
        //        PSGCProvince = @PSGCProvince,
        //        PSGCCityMun = @PSGCCityMun,
        //        PSGCBrgy = @PSGCBrgy,
        //        DateTimeModified = @DateTimeModified,
        //        ModifiedBy = @ModifiedBy
        //    WHERE 
        //        ID = @ID";

        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("@ID", txt_id.Text);
        //            cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
        //            cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
        //            cmd.Parameters.AddWithValue("@MiddleName", txt_middlename.Text);
        //            cmd.Parameters.AddWithValue("@ExtName", txt_extname.Text);
        //            cmd.Parameters.AddWithValue("@BirthDate", dt_birth.EditValue);
        //            cmd.Parameters.AddWithValue("@SexID", lbl_sex.Text);
        //            cmd.Parameters.AddWithValue("@MaritalStatusID", lbl_marital.Text);
        //            cmd.Parameters.AddWithValue("@HealthStatusID", lbl_healthstatus.Text);
        //            cmd.Parameters.AddWithValue("@HealthStatusRemarks", txt_remarks.Text);
        //            cmd.Parameters.AddWithValue("@IDNumber", txt_idno.Text);
        //            //cmd.Parameters.AddWithValue("@IDDateIssued", dt_dateissued.EditValue);
        //            // Handle null value for IDDateIssued
        //            if (dt_dateissued.EditValue == null || dt_dateissued.Text == "")
        //            {
        //                cmd.Parameters.AddWithValue("@IDDateIssued", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@IDDateIssued", dt_dateissued.Text ?? null);
        //            }
        //            cmd.Parameters.AddWithValue("@Pantawid", ck_pantawid.Checked);
        //            cmd.Parameters.AddWithValue("@Indigenous", ck_indigenous.Checked);
        //            cmd.Parameters.AddWithValue("@Citizenship", txt_citizenship.EditValue);
        //            cmd.Parameters.AddWithValue("@MothersMaiden", txt_mothers.EditValue);
        //            cmd.Parameters.AddWithValue("@Religion", txt_religion.EditValue);
        //            cmd.Parameters.AddWithValue("@BirthPlace", txt_birthplace.EditValue);
        //            cmd.Parameters.AddWithValue("@EducAttain", txt_educ.EditValue);
        //            cmd.Parameters.AddWithValue("@ContactNum", txt_contact.EditValue);
        //            cmd.Parameters.AddWithValue("@DataSourceID", lbl_datasource.Text);
        //            cmd.Parameters.AddWithValue("@PSGCRegion", lbl_region.Text);
        //            cmd.Parameters.AddWithValue("@PSGCProvince", lbl_province.Text);
        //            cmd.Parameters.AddWithValue("@PSGCCityMun", lbl_municipality.Text);
        //            cmd.Parameters.AddWithValue("@PSGCBrgy", lbl_barangay.Text);
        //            cmd.Parameters.AddWithValue("@DateTimeModified", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@ModifiedBy", _username);

        //            cmd.ExecuteNonQuery();

        //            // Check for changes and log them
        //            string[] columns = new string[]
        //            {
        //    "LastName", "FirstName", "MiddleName", "ExtName", "BirthDate",
        //    "SexID", "MaritalStatusID", "HealthStatusID", "HealthStatusRemarks", "IDNumber",
        //    "IDDateIssued", "Pantawid", "Indigenous", "Citizenship", "MothersMaiden",
        //    "Religion", "BirthPlace", "EducAttain", "ContactNum", "DataSourceID", "PSGCCityMun",
        //            };

        //            foreach (string column in columns)
        //            {
        //                string oldValue = oldRow[column].ToString();
        //                string newValue = cmd.Parameters["@" + column].Value.ToString();

        //                if (oldValue != newValue)
        //                {
        //                    MySqlCommand logCmd = con.CreateCommand();
        //                    logCmd.CommandType = CommandType.Text;
        //                    logCmd.CommandText = @"
        //            INSERT INTO log_masterlist 
        //            (MasterListID, Log, Logtype, User, DateTimeEntry) 
        //            VALUES 
        //            (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
        //                    logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
        //                    if (column == "SexID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Sex changed from [{sexNameBefore}] to [{sexNameAfter}]");
        //                    }
        //                    else if (column == "MaritalStatusID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Marital changed from [{maritalNameBefore}] to [{maritalNameAfter}]");
        //                    }
        //                    else if (column == "HealthStatusID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Health Status changed from [{healthStatusBefore}] to [{healthStatusAfter}]");
        //                    }
        //                    else if (column == "PSGCCityMun")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Municipality changed from [{municipalityBefore}] to [{municipalityAfter}]");
        //                    }
        //                    else if (column == "DataSourceID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Datasource name changed from [{datasourceBefore}] to [{datasourceAfter}]");
        //                    }
        //                    else
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}]");
        //                    }
        //                    logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
        //                    logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
        //                    logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

        //                    logCmd.ExecuteNonQuery();
        //                }
        //            }

        //            con.Close();
        //            // masterlistForm.ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



        //            XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            this.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }




        //}
        private void SaveGIS()
        {
            if (selectedPdfPath == null)
            {
                XtraMessageBox.Show("Please import a PDF file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                con.Open();

                // Insert GIS information into database
                MySqlCommand insertCmd = con.CreateCommand();
                insertCmd.CommandType = CommandType.Text;
                insertCmd.CommandText = "INSERT INTO tbl_gis (MasterListID, HouseholdSize, AssessmentID, ValidatedByID, ValidationDate, EntryBy, EntryDateTime)" +
                    " VALUES (@MasterlistID, @HouseholdSize, @AssessmentID, @ValidatedByID, @ValidationDate, @EntryBy, @EntryDateTime)";

                insertCmd.Parameters.AddWithValue("@MasterlistID", txt_id.EditValue);
                insertCmd.Parameters.AddWithValue("@HouseholdSize", txt_householdsize.EditValue);
                insertCmd.Parameters.AddWithValue("@AssessmentID", lbl_assessment.Text);
                insertCmd.Parameters.AddWithValue("@ValidatedByID", lbl_validator.Text);
                insertCmd.Parameters.AddWithValue("@ValidationDate", dt_accomplished.EditValue);
                insertCmd.Parameters.AddWithValue("@EntryBy", _username);
                insertCmd.Parameters.AddWithValue("@EntryDateTime", DateTime.Now);
                insertCmd.ExecuteNonQuery();
                //int referenceCode = Convert.ToInt32(insertCmd.ExecuteScalar()); // Get auto-incremented ID

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
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
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
        //private void UpdateGIS()
        //{
        //    try
        //    {
        //        con.Open();
        //        MySqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;

        //        // First query to retrieve the current state
        //        cmd.CommandText = @"
        //SELECT
        //    g.MasterlistID,
        //    g.HouseHoldSize,
        //    g.AssessmentID,
        //    g.ValidatedByID,
        //    g.ValidationDate
        //FROM 
        //    tbl_gis g
        //WHERE 
        //    g.MasterlistID = @MasterlistID";
        //        cmd.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

        //        DataTable dtOld = new DataTable();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(dtOld);

        //        if (dtOld.Rows.Count > 0)
        //        {
        //            DataRow oldRow = dtOld.Rows[0];

        //            // Fetch current Assesment name
        //            string assessmentNameBefore = "";
        //            string assessmentNameAfter = "";

        //            if (oldRow["AssessmentID"] != DBNull.Value)
        //            {
        //                int assessmentIDBefore = Convert.ToInt32(oldRow["AssessmentID"]);
        //                MySqlCommand assessmentCmdBefore = con.CreateCommand();
        //                assessmentCmdBefore.CommandType = CommandType.Text;
        //                assessmentCmdBefore.CommandText = "SELECT Assessment FROM lib_assessment WHERE Id = @AssessmentID";
        //                assessmentCmdBefore.Parameters.AddWithValue("@AssessmentID", assessmentIDBefore);
        //                assessmentNameBefore = assessmentCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int assessmentIDAfter = Convert.ToInt32(lbl_assessment.Text); // Assuming lbl_sex.Text contains the updated SexID

        //            MySqlCommand assessmentCmdAfter = con.CreateCommand();
        //            assessmentCmdAfter.CommandType = CommandType.Text;
        //            assessmentCmdAfter.CommandText = "SELECT Assessment FROM lib_assessment WHERE Id = @AssessmentID";
        //            assessmentCmdAfter.Parameters.AddWithValue("@AssessmentID", assessmentIDAfter);
        //            assessmentNameAfter = assessmentCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Fetch current validator name
        //            string validatedNameBefore = "";
        //            string validatedNameAfter = "";

        //            if (oldRow["ValidatedByID"] != DBNull.Value)
        //            {
        //                int validatedIDBefore = Convert.ToInt32(oldRow["ValidatedByID"]);
        //                MySqlCommand validatedCmdBefore = con.CreateCommand();
        //                validatedCmdBefore.CommandType = CommandType.Text;
        //                validatedCmdBefore.CommandText = "SELECT Validator FROM lib_validator WHERE Id = @ValidatedByID";
        //                validatedCmdBefore.Parameters.AddWithValue("@ValidatedByID", validatedIDBefore);
        //                validatedNameBefore = validatedCmdBefore.ExecuteScalar()?.ToString() ?? "";
        //            }

        //            int validatedIDAfter = Convert.ToInt32(lbl_validator.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

        //            MySqlCommand validatedCmdAfter = con.CreateCommand();
        //            validatedCmdAfter.CommandType = CommandType.Text;
        //            validatedCmdAfter.CommandText = "SELECT Validator FROM lib_validator WHERE Id = @ValidatedByID";
        //            validatedCmdAfter.Parameters.AddWithValue("@ValidatedByID", validatedIDAfter);
        //            validatedNameAfter = validatedCmdAfter.ExecuteScalar()?.ToString() ?? "";


        //            // Perform the update
        //            cmd.CommandText = @"
        //    UPDATE 
        //        tbl_gis 
        //    SET
        //        HouseHoldSize = @HouseHoldSize,
        //        AssessmentID = @AssessmentID,
        //        ValidatedByID = @ValidatedByID,
        //        ValidationDate = @ValidationDate
        //    WHERE 
        //        MasterListID = @MasterListID";

        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
        //            cmd.Parameters.AddWithValue("@HouseHoldSize", txt_householdsize.Text);
        //            cmd.Parameters.AddWithValue("@AssessmentID", lbl_assessment.Text);
        //            cmd.Parameters.AddWithValue("@ValidatedByID", lbl_validator.Text);
        //            cmd.Parameters.AddWithValue("@ValidationDate", dt_accomplished.EditValue);
        //            cmd.ExecuteNonQuery();

        //            // Check for changes and log them
        //            string[] columns = new string[]
        //            {
        //        "HouseHoldSize", "AssessmentID", "ValidatedByID", "ValidationDate",
        //            };

        //            foreach (string column in columns)
        //            {
        //                string oldValue = oldRow[column].ToString();
        //                string newValue = cmd.Parameters["@" + column].Value.ToString();

        //                if (oldValue != newValue)
        //                {
        //                    MySqlCommand logCmd = con.CreateCommand();
        //                    logCmd.CommandType = CommandType.Text;
        //                    logCmd.CommandText = @"
        //            INSERT INTO log_masterlist 
        //            (MasterListID, Log, Logtype, User, DateTimeEntry) 
        //            VALUES 
        //            (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
        //                    logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
        //                    if (column == "AssessmentID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Assessment changed from [{assessmentNameBefore}] to [{assessmentNameAfter}] [Source:GIS]");
        //                    }
        //                    else if (column == "ValidatedByID")
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"Validator changed from [{validatedNameBefore}] to [{validatedNameAfter}] [Source:GIS]");
        //                    }
        //                    else
        //                    {
        //                        logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}] [Source:GIS]");
        //                    }
        //                    logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
        //                    logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
        //                    logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

        //                    logCmd.ExecuteNonQuery();
        //                }
        //            }

        //            con.Close();

        //        }
        //        else
        //        {
        //            MessageBox.Show("No data found for the provided ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
                //SaveGIS();
                return;
            }
            if (txt_referencecode.Text == "")
            {
                //UpdateMaster(); 
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
        private MasterList masterlist;

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
    }
}
