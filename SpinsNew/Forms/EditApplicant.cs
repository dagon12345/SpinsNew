using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew;
using SpinsNew.Connection;
using SpinsNew.Forms;
using System;
using System.Collections.Generic;
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
        public string _username;
        public EditApplicant(MasterList masterlist, Replacements replacements, string username)//Call the MasterList into our Edit Applicant form.
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);

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
        //Load the logs below method.
        public async void LoadLogsAsync()
        {
            try
            {
                await con.OpenAsync();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                        m.ID,
                        lm.Log,
                        lt.Type as Type,
                        lm.User,
                        lm.DateTimeEntry
                    FROM
                        tbl_masterlist m
                    LEFT JOIN
                        log_masterlist lm ON m.ID = lm.MasterlistID
                    LEFT JOIN 
                        lib_log_type lt ON lm.LogType = lt.ID
                    WHERE 
                        m.ID = @MasterlistID
                    ORDER BY
                        lm.DateTimeEntry DESC";

                cmd.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //Await to reduce lag while loading large amount of datas
                await Task.Run(() => da.Fill(dt));
                //We are using DevExpress datagridview
                gv_logs.DataSource = dt;
                // Get the GridView instance
                GridView gridView = gv_logs.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();
                    // Hide the "ID" column
                    gridView.Columns["ID"].Visible = false;
                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                    await con.CloseAsync();
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }
        public void LoadLogs()
        {
            con.Open();
            MySqlCommand cmd0 = con.CreateCommand();
            cmd0.CommandType = CommandType.Text;
            cmd0.CommandText = @"SELECT 
                        m.ID,
                        lm.Log,
                        lt.Type as Type,
                        lm.User,
                        lm.DateTimeEntry
                    FROM
                        tbl_masterlist m
                    LEFT JOIN
                        log_masterlist lm ON m.ID = lm.MasterlistID
                    LEFT JOIN 
                        lib_log_type lt ON lm.LogType = lt.ID
                    WHERE 
                        m.ID = @MasterlistID
                    ORDER BY
                        lm.DateTimeEntry DESC";

            cmd0.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

            DataTable dt0 = new DataTable();
            MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0);
            //Await to reduce lag while loading large amount of datas
            //await Task.Run(() => da0.Fill(dt0));
            da0.Fill(dt0);
            //We are using DevExpress datagridview
            gv_logs.DataSource = dt0;
            // Get the GridView instance
            GridView gridView = gv_logs.MainView as GridView;
            if (gridView != null)
            {
                // Auto-size all columns based on their content
                gridView.BestFitColumns();
                // Hide the "ID" column
                gridView.Columns["ID"].Visible = false;
                gridView.Columns["Type"].Visible = false;
                // Ensure horizontal scrollbar is enabled
                gridView.OptionsView.ColumnAutoWidth = false;
                // Disable editing
                gridView.OptionsBehavior.Editable = false;

                con.Close();
            }

        }
        //Load data from tables and display into textboxes
        public void LoadDataAsync()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @" SELECT 
                m.ID,
                m.LastName,
                m.FirstName,
                m.MiddleName,
                m.ExtName,
                m.BirthDate,
                m.SexID,
                ls.Sex as Sex,
                m.MaritalStatusID,
                lms.MaritalStatus as MaritalStatus,
                m.HealthStatusID,
                lhs.HealthStatus as HealthStatus,
                m.HealthStatusRemarks,
                m.IDNumber,
                m.IDDateIssued,
                m.Pantawid,
                m.Indigenous,
                m.Citizenship,
                m.MothersMaiden,
                m.Religion,
                m.BirthPlace,
                m.EducAttain,
                m.ContactNum,
                m.DataSourceID,
                ld.DataSource as DataSource,
                m.PSGCRegion,
                lr.Region as Region,
                m.PSGCProvince,
                lp.ProvinceName as Province,
                m.PSGCCityMun,
                lcm.CityMunName as Municipality,
                m.PSGCBrgy,
                lb.BrgyName as Barangay,

                tg_max.ReferenceCode,
                tg_max.MasterListID,
                tg_max.HouseholdSize,
                la.Assessment as Assessment,
                tg_max.AssessmentID,
                tg_max.ValidatedByID,
                lv.Validator as Validator,
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
                lib_validator lv ON tg_max.ValidatedByID = lv.ID
            LEFT JOIN
                lib_assessment la ON tg_max.AssessmentID = la.ID
            LEFT JOIN
                lib_sex ls ON m.SexId = ls.Id
            LEFT JOIN
                lib_marital_status lms ON m.MaritalStatusID = lms.Id
            LEFT JOIN
                lib_health_status lhs ON m.HealthStatusID = lhs.Id
            LEFT JOIN
                lib_datasource ld ON m.DataSourceID = ld.Id
            LEFT JOIN
                lib_region lr ON m.PSGCRegion = lr.PSGCRegion 
            LEFT JOIN
                lib_province lp ON m.PSGCProvince = lp.PSGCProvince 
            LEFT JOIN
                lib_city_municipality lcm ON m.PSGCCityMun = lcm.PSGCCityMun
            LEFT JOIN
                lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
            WHERE 
                m.ID = @MasterlistID";
                cmd.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //Await to reduce lag while loading large amount of datas
                da.Fill(dt);
                // Check if data is retrieved
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Populate the textboxes
                    txt_lastname.EditValue = row["LastName"].ToString();
                    txt_firstname.EditValue = row["FirstName"].ToString();
                    txt_middlename.EditValue = row["MiddleName"].ToString();
                    txt_extname.EditValue = row["ExtName"].ToString();
                    dt_birth.EditValue = Convert.ToDateTime(row["BirthDate"]);

                    cmb_sex.EditValue = row["Sex"];
                    lbl_sex.Text = row["SexID"].ToString();

                    cmb_marital.EditValue = row["MaritalStatus"];
                    lbl_marital.Text = row["MaritalStatusID"].ToString();

                    cmb_healthstatus.EditValue = row["HealthStatus"];
                    lbl_healthstatus.Text = row["HealthStatusID"].ToString();

                    txt_remarks.EditValue = row["HealthStatusRemarks"].ToString();
                    txt_idno.EditValue = row["IDNumber"].ToString();
                    //This is to accept date as a null.
                    if (row["IDDateIssued"] != DBNull.Value)
                    {
                        dt_dateissued.EditValue = Convert.ToDateTime(row["IDDateIssued"]);
                    }
                    else
                    {
                        dt_dateissued.Text = string.Empty; // or set to a default value, e.g., "N/A"
                    }
                    //ck_pantawid.Checked = Convert.ToBoolean(row["Pantawid"]);
                    //ck_indigenous.Checked = Convert.ToBoolean(row["Indigenous"]);
                    ck_pantawid.Checked = row["Pantawid"] != DBNull.Value && Convert.ToBoolean(row["Pantawid"]);
                    ck_indigenous.Checked = row["Indigenous"] != DBNull.Value && Convert.ToBoolean(row["Indigenous"]);
                    txt_citizenship.EditValue = row["Citizenship"].ToString();
                    txt_mothers.EditValue = row["MothersMaiden"].ToString();
                    txt_religion.EditValue = row["Religion"].ToString();
                    txt_birthplace.EditValue = row["BirthPlace"].ToString();
                    txt_educ.EditValue = row["EducAttain"].ToString();
                    txt_contact.EditValue = row["ContactNum"].ToString();

                    cmb_datasource.EditValue = row["DataSource"];
                    lbl_datasource.Text = row["DataSourceID"].ToString();

                    cmb_region.EditValue = row["Region"];
                    lbl_region.Text = row["PSGCRegion"].ToString();

                    cmb_province.EditValue = row["Province"];
                    lbl_province.Text = row["PSGCProvince"].ToString();

                    cmb_municipality.EditValue = row["Municipality"];
                    lbl_municipality.Text = row["PSGCCityMun"].ToString();

                    cmb_barangay.EditValue = row["Barangay"];
                    lbl_barangay.Text = row["PSGCBrgy"].ToString();

                    // Retrieve the selected item and get the ID
                    // var selectedItem = (dynamic)cmb_sex.SelectedItem;
                    // int sexId = selectedItem.Id;
                    //cmd.Parameters.AddWithValue("@Sex", sexId);

                    //GIS BElow
                    txt_householdsize.EditValue = row["HouseholdSize"].ToString();

                    cmb_assessment.EditValue = row["Assessment"];
                    lbl_assessment.Text = row["AssessmentID"].ToString();

                    cmb_validator.EditValue = row["Validator"];
                    lbl_validator.Text = row["ValidatedByID"].ToString();

                    //This is to accept date as a null.
                    if (row["ValidationDate"] != DBNull.Value)
                    {
                        dt_accomplished.EditValue = Convert.ToDateTime(row["ValidationDate"]);
                    }
                    else
                    {
                        dt_accomplished.Text = string.Empty; // or set to a default value, e.g., "N/A"
                    }
                    txt_referencecode.EditValue = row["ReferenceCode"];

                    // con.Close();
                }
                else
                {
                    MessageBox.Show("No data found for the provided ID.");
                }



            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
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

        // Other methods and controls
        private void EditApplicant_Load(object sender, EventArgs e)
        {
            //Call the methods below to fill the comboboxes
            LoadDataAsync();
            LoadLogs();
            DataSource();
            Marital();
            PopulateSexComboBox();
            HealthStatus();
            InitializeComboboxes();
            DisplayAge();
            Assessment();
            Validator();

            cmb_municipality.SelectedIndexChanged += cmb_municipality_SelectedIndexChanged;

          


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

        // Custom class to store Id and DataSource
        public class DataSourceItem
        {
            public int Id { get; set; }
            public string DataSource { get; set; }


            public override string ToString()
            {
                return DataSource; // Display DataSource in the ComboBox
            }
        }



        public class ProvinceItem
        {
            public int PSGCProvince { get; set; }
            public string ProvinceName { get; set; }
            public int PSGCRegion { get; set; }
            // public string ProvinceName { get; set; } // Add this property
        }

        public class RegionItem
        {
            public int PSGCRegion { get; set; }
            public string Region { get; set; }
            // public string ProvinceName { get; set; } // Add this property
        }

        // For municipality combobox
        public class MunicipalityItem
        {
            public int PSGCCityMun { get; set; }
            public string CityMunName { get; set; }
            public int PSGCProvince { get; set; }
            public string ProvinceName { get; set; } // Add this property

            public override string ToString()
            {
                return $"{CityMunName} | {ProvinceName} "; // Display both the municipality and province in the ComboBox
            }
        }


        // Custom class for barangay combobox cascaded into municipality
        public class BarangayItem
        {
            public int PSGCBrgy { get; set; }
            public string BrgyName { get; set; }
            public int PSGCCityMun { get; set; }

            public override string ToString()
            {
                return BrgyName; // Display DataSource in the ComboBox
            }
        }


        // Custom class to store Id and Sex
        public class SexItem
        {
            public int Id { get; set; }
            public string Sex { get; set; }

            public override string ToString()
            {
                return Sex; // Display DataSource in the ComboBox
            }
        }

        // Custom class to store Id and Sex
        public class MaritalItem
        {
            public int Id { get; set; }
            public string MaritalStatus { get; set; }

            public override string ToString()
            {
                return MaritalStatus; // Display DataSource in the ComboBox
            }
        }
        public class AssessmentItem
        {
            public int Id { get; set; }
            public string Assessment { get; set; }

            public override string ToString()
            {
                return Assessment; // Display DataSource in the ComboBox
            }
        }

        public class ValidatorItem
        {
            public int Id { get; set; }
            public string Validator { get; set; }

            public override string ToString()
            {
                return Validator; // Display DataSource in the ComboBox
            }
        }


        // Custom class to store Id and Sex
        public class HealthStatusItem
        {
            public int Id { get; set; }
            public string HealthStatus { get; set; }

            public override string ToString()
            {
                return HealthStatus; // Display DataSource in the ComboBox
            }
        }


        //Fill combobox datasource
        public void DataSource()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, DataSource FROM lib_datasource"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_datasource.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_datasource.Properties.Items.Add(new DataSourceItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        DataSource = dr["DataSource"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void LoadSexData()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT id, sex FROM lib_sex";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Clear existing items
                cmb_sex.Properties.Items.Clear();

                // Add items to ComboBoxEdit
                foreach (DataRow row in dt.Rows)
                {
                    cmb_sex.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(row["sex"].ToString(), row["id"]));
                }

                con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }




        // Fill combobox datasource
        public void PopulateSexComboBox()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Sex FROM lib_sex"; // Specify the columns to retrieve
                MySqlDataReader reader = cmd.ExecuteReader();

                List<SexItem> sexItems = new List<SexItem>();

                while (reader.Read())
                {
                    sexItems.Add(new SexItem
                    {
                        Id = reader.GetInt32("Id"),
                        Sex = reader.GetString("Sex")
                    });
                }

                cmb_sex.Properties.Items.Clear();
                cmb_sex.Properties.Items.AddRange(sexItems);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Fill combobox Marital
        public void Marital()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, MaritalStatus FROM lib_marital_status"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_marital.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_marital.Properties.Items.Add(new MaritalItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        MaritalStatus = dr["MaritalStatus"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        //Fill combobox Marital
        public void Assessment()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Assessment FROM lib_assessment"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_assessment.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_assessment.Properties.Items.Add(new AssessmentItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Assessment = dr["Assessment"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        //Fill combobox Validator
        public void Validator()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Validator FROM lib_validator"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_validator.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_validator.Properties.Items.Add(new ValidatorItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Validator = dr["Validator"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        //Fill combobox HealthStatus
        public void HealthStatus()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, HealthStatus FROM lib_health_status"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_healthstatus.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_healthstatus.Properties.Items.Add(new HealthStatusItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        HealthStatus = dr["HealthStatus"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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


        //Fill combobox Municipality
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
                MySqlDataReader reader = cmd.ExecuteReader();

                List<MunicipalityItem> municipalityItems = new List<MunicipalityItem>();

                while (reader.Read())
                {
                    municipalityItems.Add(new MunicipalityItem
                    {
                        PSGCCityMun = reader.GetInt32("PSGCCityMun"),
                        CityMunName = reader.GetString("CityMunName"),
                        PSGCProvince = reader.GetInt32("PSGCProvince"),
                        ProvinceName = reader.GetString("ProvinceName")
                    });
                }

                cmb_municipality.Properties.Items.Clear();
                cmb_municipality.Properties.Items.AddRange(municipalityItems);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Fill combobox barangay
        public void Barangay(int selectedCityMun)
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PSGCBrgy, BrgyName, PSGCCityMun FROM lib_barangay WHERE PSGCCityMun = @PSGCCityMun"; // Specify the columns to retrieve
                cmd.Parameters.AddWithValue("@PSGCCityMun", selectedCityMun);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_barangay.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_barangay.Properties.Items.Add(new BarangayItem
                    {
                        PSGCBrgy = Convert.ToInt32(dr["PSGCBrgy"]),
                        BrgyName = dr["BrgyName"].ToString(),
                        PSGCCityMun = Convert.ToInt32(dr["PSGCCityMun"])
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Ensure connection is closed in the finally block
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        // Add event handler to the municipality combobox
        public void InitializeComboboxes()
        {
            Municipality(); // Load municipalities
            cmb_municipality.SelectedIndexChanged += new EventHandler(cmb_municipality_SelectedIndexChanged);
            // Initialize barangay combobox if necessary, otherwise it will be populated upon municipality selection
        }

        private int psgcRegion; // Field to store PSGCRegion
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
                lbl_age.Text = $"{age}";
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
        private void UpdateMaster()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT 
            m.LastName,
            m.FirstName,
            m.MiddleName,
            m.ExtName,
            m.BirthDate,
            m.SexID,
            m.MaritalStatusID,
            m.HealthStatusID,
            m.HealthStatusRemarks,
            m.IDNumber,
            m.IDDateIssued,
            m.Pantawid,
            m.Indigenous,
            m.Citizenship,
            m.MothersMaiden,
            m.Religion,
            m.BirthPlace,
            m.EducAttain,
            m.ContactNum,
            m.DataSourceID,
            m.PSGCRegion,
            m.PSGCProvince,
            m.PSGCCityMun,
            m.PSGCBrgy
        FROM 
            tbl_masterlist m
        WHERE 
            m.ID = @IDNumber";
                cmd.Parameters.AddWithValue("@IDNumber", txt_id.Text);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];

                    // Fetch current sex name
                    string sexNameBefore = "";
                    string sexNameAfter = "";

                    if (oldRow["SexID"] != DBNull.Value)
                    {
                        int sexIDBefore = Convert.ToInt32(oldRow["SexID"]);
                        MySqlCommand sexCmdBefore = con.CreateCommand();
                        sexCmdBefore.CommandType = CommandType.Text;
                        sexCmdBefore.CommandText = "SELECT Sex FROM lib_sex WHERE Id = @SexID";
                        sexCmdBefore.Parameters.AddWithValue("@SexID", sexIDBefore);
                        sexNameBefore = sexCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int sexIDAfter = Convert.ToInt32(lbl_sex.Text); // Assuming lbl_sex.Text contains the updated SexID

                    MySqlCommand sexCmdAfter = con.CreateCommand();
                    sexCmdAfter.CommandType = CommandType.Text;
                    sexCmdAfter.CommandText = "SELECT Sex FROM lib_sex WHERE Id = @SexID";
                    sexCmdAfter.Parameters.AddWithValue("@SexID", sexIDAfter);
                    sexNameAfter = sexCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Fetch current maritalstatus name
                    string maritalNameBefore = "";
                    string maritalNameAfter = "";

                    if (oldRow["MaritalStatusID"] != DBNull.Value)
                    {
                        int maritalIDBefore = Convert.ToInt32(oldRow["MaritalStatusID"]);
                        MySqlCommand maritalCmdBefore = con.CreateCommand();
                        maritalCmdBefore.CommandType = CommandType.Text;
                        maritalCmdBefore.CommandText = "SELECT MaritalStatus FROM lib_marital_status WHERE Id = @MaritalStatusID";
                        maritalCmdBefore.Parameters.AddWithValue("@MaritalStatusID", maritalIDBefore);
                        maritalNameBefore = maritalCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int maritalIDAfter = Convert.ToInt32(lbl_marital.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

                    MySqlCommand maritalCmdAfter = con.CreateCommand();
                    maritalCmdAfter.CommandType = CommandType.Text;
                    maritalCmdAfter.CommandText = "SELECT MaritalStatus FROM lib_marital_status WHERE Id = @MaritalStatusID";
                    maritalCmdAfter.Parameters.AddWithValue("@MaritalStatusID", maritalIDAfter);
                    maritalNameAfter = maritalCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Fetch current HealthStatus name
                    string healthStatusBefore = "";
                    string healthStatusAfter = "";

                    if (oldRow["HealthStatusID"] != DBNull.Value)
                    {
                        int healthstatusIDBefore = Convert.ToInt32(oldRow["HealthStatusID"]);
                        MySqlCommand healthstatusCmdBefore = con.CreateCommand();
                        healthstatusCmdBefore.CommandType = CommandType.Text;
                        healthstatusCmdBefore.CommandText = "SELECT HealthStatus FROM lib_health_status WHERE Id = @HealthStatusID";
                        healthstatusCmdBefore.Parameters.AddWithValue("@HealthStatusID", healthstatusIDBefore);
                        healthStatusBefore = healthstatusCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int healthstatusIDAfter = Convert.ToInt32(lbl_healthstatus.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

                    MySqlCommand healthstatusCmdAfter = con.CreateCommand();
                    healthstatusCmdAfter.CommandType = CommandType.Text;
                    healthstatusCmdAfter.CommandText = "SELECT HealthStatus FROM lib_health_status WHERE Id = @HealthStatusID";
                    healthstatusCmdAfter.Parameters.AddWithValue("@HealthStatusID", healthstatusIDAfter);
                    healthStatusAfter = healthstatusCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Fetch current Municipality name
                    string municipalityBefore = "";
                    string municipalityAfter = "";

                    if (oldRow["PSGCCityMun"] != DBNull.Value)
                    {
                        int municipalityIDBefore = Convert.ToInt32(oldRow["PSGCCityMun"]);

                        MySqlCommand municipalityCmdBefore = con.CreateCommand();
                        municipalityCmdBefore.CommandType = CommandType.Text;
                        municipalityCmdBefore.CommandText = "SELECT CityMunName FROM lib_city_municipality WHERE PSGCCityMun = @PSGCCityMun";
                        municipalityCmdBefore.Parameters.AddWithValue("@PSGCCityMun", municipalityIDBefore);
                        municipalityBefore = municipalityCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int municipalityIDAfter = Convert.ToInt32(lbl_municipality.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

                    MySqlCommand municipalityCmdAfter = con.CreateCommand();
                    municipalityCmdAfter.CommandType = CommandType.Text;
                    municipalityCmdAfter.CommandText = "SELECT CityMunName FROM lib_city_municipality WHERE PSGCCityMun = @PSGCCityMun";
                    municipalityCmdAfter.Parameters.AddWithValue("@PSGCCityMun", municipalityIDAfter);
                    municipalityAfter = municipalityCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Fetch current Datasource name
                    string datasourceBefore = "";
                    string datasourceAfter = "";

                    if (oldRow["DataSourceID"] != DBNull.Value)
                    {
                        int datasourceIDBefore = Convert.ToInt32(oldRow["DataSourceID"]);

                        MySqlCommand datasourceCmdBefore = con.CreateCommand();
                        datasourceCmdBefore.CommandType = CommandType.Text;
                        datasourceCmdBefore.CommandText = "SELECT DataSource FROM lib_datasource WHERE ID = @DataSourceID";
                        datasourceCmdBefore.Parameters.AddWithValue("@DataSourceID", datasourceIDBefore);
                        datasourceBefore = datasourceCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int datasourceIDAfter = Convert.ToInt32(lbl_datasource.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

                    MySqlCommand datasourceCmdAfter = con.CreateCommand();
                    datasourceCmdAfter.CommandType = CommandType.Text;
                    datasourceCmdAfter.CommandText = "SELECT DataSource FROM lib_datasource WHERE ID = @DataSourceID";
                    datasourceCmdAfter.Parameters.AddWithValue("@DataSourceID", datasourceIDAfter);
                    datasourceAfter = datasourceCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Perform the update
                    cmd.CommandText = @"
            UPDATE 
                tbl_masterlist 
            SET
                LastName = @LastName,
                FirstName = @FirstName,
                MiddleName = @MiddleName,
                ExtName = @ExtName,
                BirthDate = @BirthDate,
                SexID = @SexID,
                MaritalStatusID = @MaritalStatusID,
                HealthStatusID = @HealthStatusID,
                HealthStatusRemarks = @HealthStatusRemarks,
                IDNumber = @IDNumber,
                IDDateIssued = @IDDateIssued,
                Pantawid = @Pantawid,
                Indigenous = @Indigenous,
                Citizenship = @Citizenship,
                MothersMaiden = @MothersMaiden,
                Religion = @Religion,
                Birthplace = @BirthPlace,
                EducAttain = @EducAttain,
                ContactNum = @ContactNum,
                DataSourceID = @DataSourceID,
                PSGCRegion = @PSGCRegion,
                PSGCProvince = @PSGCProvince,
                PSGCCityMun = @PSGCCityMun,
                PSGCBrgy = @PSGCBrgy,
                DateTimeModified = @DateTimeModified,
                ModifiedBy = @ModifiedBy
            WHERE 
                ID = @ID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", txt_id.Text);
                    cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
                    cmd.Parameters.AddWithValue("@MiddleName", txt_middlename.Text);
                    cmd.Parameters.AddWithValue("@ExtName", txt_extname.Text);
                    cmd.Parameters.AddWithValue("@BirthDate", dt_birth.EditValue);
                    cmd.Parameters.AddWithValue("@SexID", lbl_sex.Text);
                    cmd.Parameters.AddWithValue("@MaritalStatusID", lbl_marital.Text);
                    cmd.Parameters.AddWithValue("@HealthStatusID", lbl_healthstatus.Text);
                    cmd.Parameters.AddWithValue("@HealthStatusRemarks", txt_remarks.Text);
                    cmd.Parameters.AddWithValue("@IDNumber", txt_idno.Text);
                    //cmd.Parameters.AddWithValue("@IDDateIssued", dt_dateissued.EditValue);
                    // Handle null value for IDDateIssued
                    if (dt_dateissued.EditValue == null)
                    {
                        cmd.Parameters.AddWithValue("@IDDateIssued", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IDDateIssued", dt_dateissued.EditValue);
                    }
                    cmd.Parameters.AddWithValue("@Pantawid", ck_pantawid.Checked);
                    cmd.Parameters.AddWithValue("@Indigenous", ck_indigenous.Checked);
                    cmd.Parameters.AddWithValue("@Citizenship", txt_citizenship.EditValue);
                    cmd.Parameters.AddWithValue("@MothersMaiden", txt_mothers.EditValue);
                    cmd.Parameters.AddWithValue("@Religion", txt_religion.EditValue);
                    cmd.Parameters.AddWithValue("@BirthPlace", txt_birthplace.EditValue);
                    cmd.Parameters.AddWithValue("@EducAttain", txt_educ.EditValue);
                    cmd.Parameters.AddWithValue("@ContactNum", txt_contact.EditValue);
                    cmd.Parameters.AddWithValue("@DataSourceID", lbl_datasource.Text);
                    cmd.Parameters.AddWithValue("@PSGCRegion", lbl_region.Text);
                    cmd.Parameters.AddWithValue("@PSGCProvince", lbl_province.Text);
                    cmd.Parameters.AddWithValue("@PSGCCityMun", lbl_municipality.Text);
                    cmd.Parameters.AddWithValue("@PSGCBrgy", lbl_barangay.Text);
                    cmd.Parameters.AddWithValue("@DateTimeModified", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _username);

                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    string[] columns = new string[]
                    {
            "LastName", "FirstName", "MiddleName", "ExtName", "BirthDate",
            "SexID", "MaritalStatusID", "HealthStatusID", "HealthStatusRemarks", "IDNumber",
            "IDDateIssued", "Pantawid", "Indigenous", "Citizenship", "MothersMaiden",
            "Religion", "BirthPlace", "EducAttain", "ContactNum", "DataSourceID", "PSGCCityMun",
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
                            logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                            if (column == "SexID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Sex changed from [{sexNameBefore}] to [{sexNameAfter}]");
                            }
                            else if (column == "MaritalStatusID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Marital changed from [{maritalNameBefore}] to [{maritalNameAfter}]");
                            }
                            else if (column == "HealthStatusID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Health Status changed from [{healthStatusBefore}] to [{healthStatusAfter}]");
                            }
                            else if (column == "PSGCCityMun")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Municipality changed from [{municipalityBefore}] to [{municipalityAfter}]");
                            }
                            else if (column == "DataSourceID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Datasource name changed from [{datasourceBefore}] to [{datasourceAfter}]");
                            }
                            else
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}]");
                            }
                            logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                            logCmd.Parameters.AddWithValue("@User", Environment.UserName); // Replace with the actual user
                            logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

                            logCmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();
                    masterlistForm.ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.



                    XtraMessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
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
                insertCmd.Parameters.AddWithValue("@EntryBy", Environment.UserName);
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
                masterlistForm.ReloadMasterlist();//Reload the masterlist when updated except for the select all municiaplities and all statuses.

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
        private void UpdateGIS()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // First query to retrieve the current state
                cmd.CommandText = @"
        SELECT
            g.MasterlistID,
            g.HouseHoldSize,
            g.AssessmentID,
            g.ValidatedByID,
            g.ValidationDate
        FROM 
            tbl_gis g
        WHERE 
            g.MasterlistID = @MasterlistID";
                cmd.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

                DataTable dtOld = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtOld);

                if (dtOld.Rows.Count > 0)
                {
                    DataRow oldRow = dtOld.Rows[0];

                    // Fetch current Assesment name
                    string assessmentNameBefore = "";
                    string assessmentNameAfter = "";

                    if (oldRow["AssessmentID"] != DBNull.Value)
                    {
                        int assessmentIDBefore = Convert.ToInt32(oldRow["AssessmentID"]);
                        MySqlCommand assessmentCmdBefore = con.CreateCommand();
                        assessmentCmdBefore.CommandType = CommandType.Text;
                        assessmentCmdBefore.CommandText = "SELECT Assessment FROM lib_assessment WHERE Id = @AssessmentID";
                        assessmentCmdBefore.Parameters.AddWithValue("@AssessmentID", assessmentIDBefore);
                        assessmentNameBefore = assessmentCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int assessmentIDAfter = Convert.ToInt32(lbl_assessment.Text); // Assuming lbl_sex.Text contains the updated SexID

                    MySqlCommand assessmentCmdAfter = con.CreateCommand();
                    assessmentCmdAfter.CommandType = CommandType.Text;
                    assessmentCmdAfter.CommandText = "SELECT Assessment FROM lib_assessment WHERE Id = @AssessmentID";
                    assessmentCmdAfter.Parameters.AddWithValue("@AssessmentID", assessmentIDAfter);
                    assessmentNameAfter = assessmentCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Fetch current validator name
                    string validatedNameBefore = "";
                    string validatedNameAfter = "";

                    if (oldRow["ValidatedByID"] != DBNull.Value)
                    {
                        int validatedIDBefore = Convert.ToInt32(oldRow["ValidatedByID"]);
                        MySqlCommand validatedCmdBefore = con.CreateCommand();
                        validatedCmdBefore.CommandType = CommandType.Text;
                        validatedCmdBefore.CommandText = "SELECT Validator FROM lib_validator WHERE Id = @ValidatedByID";
                        validatedCmdBefore.Parameters.AddWithValue("@ValidatedByID", validatedIDBefore);
                        validatedNameBefore = validatedCmdBefore.ExecuteScalar()?.ToString() ?? "";
                    }

                    int validatedIDAfter = Convert.ToInt32(lbl_validator.Text); // Assuming lbl_martital.Text contains the updated MaritalStatusID

                    MySqlCommand validatedCmdAfter = con.CreateCommand();
                    validatedCmdAfter.CommandType = CommandType.Text;
                    validatedCmdAfter.CommandText = "SELECT Validator FROM lib_validator WHERE Id = @ValidatedByID";
                    validatedCmdAfter.Parameters.AddWithValue("@ValidatedByID", validatedIDAfter);
                    validatedNameAfter = validatedCmdAfter.ExecuteScalar()?.ToString() ?? "";


                    // Perform the update
                    cmd.CommandText = @"
            UPDATE 
                tbl_gis 
            SET
                HouseHoldSize = @HouseHoldSize,
                AssessmentID = @AssessmentID,
                ValidatedByID = @ValidatedByID,
                ValidationDate = @ValidationDate
            WHERE 
                MasterListID = @MasterListID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                    cmd.Parameters.AddWithValue("@HouseHoldSize", txt_householdsize.Text);
                    cmd.Parameters.AddWithValue("@AssessmentID", lbl_assessment.Text);
                    cmd.Parameters.AddWithValue("@ValidatedByID", lbl_validator.Text);
                    cmd.Parameters.AddWithValue("@ValidationDate", dt_accomplished.EditValue);
                    cmd.ExecuteNonQuery();

                    // Check for changes and log them
                    string[] columns = new string[]
                    {
                "HouseHoldSize", "AssessmentID", "ValidatedByID", "ValidationDate",
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
                            logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                            if (column == "AssessmentID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Assessment changed from [{assessmentNameBefore}] to [{assessmentNameAfter}] [Source:GIS]");
                            }
                            else if (column == "ValidatedByID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Validator changed from [{validatedNameBefore}] to [{validatedNameAfter}] [Source:GIS]");
                            }
                            else
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"{column} changed from [{oldValue}] to [{newValue}] [Source:GIS]");
                            }
                            logCmd.Parameters.AddWithValue("@Logtype", 1); // Assuming 1 is for update
                            logCmd.Parameters.AddWithValue("@User", Environment.UserName); // Replace with the actual user
                            logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

                            logCmd.ExecuteNonQuery();
                        }
                    }

                    con.Close();

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

        private void btn_edit_Click(object sender, EventArgs e)
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
                if(cmb_assessment.Text == "")
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
                SaveGIS();
                return;
            }
            if(txt_referencecode.Text == "")
            {
                UpdateMaster();
                return;
            }
            UpdateGIS();
            UpdateMaster();


        }

        private void txt_id_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmb_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {



                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Sex FROM lib_sex WHERE Sex='" + cmb_sex.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_sex.Text = dr["ID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }

        }

        private void cmb_marital_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {



                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, MaritalStatus FROM lib_marital_status WHERE MaritalStatus='" + cmb_marital.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_marital.Text = dr["ID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }
        }

        private void cmb_healthstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {



                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, HealthStatus FROM lib_health_status WHERE HealthStatus='" + cmb_healthstatus.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_healthstatus.Text = dr["ID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }
        }

        private void cmb_datasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {



                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, DataSource FROM lib_datasource WHERE DataSource='" + cmb_datasource.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_datasource.Text = dr["ID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }
        }
        // Municipality fill when selected indexchanged was click with concatenated province
        private void UpdateMunicipalityLabel(string cityMunName)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PSGCCityMun, CityMunName FROM lib_city_municipality WHERE CityMunName=@CityMunName";
                cmd.Parameters.AddWithValue("@CityMunName", cityMunName);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    lbl_municipality.Text = dr["PSGCCityMun"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillMunicipalityandBarangay()
        {
            //Municipality
            if (cmb_municipality.SelectedItem is MunicipalityItem selectedMunicipality)
            {
                UpdateMunicipalityLabel(selectedMunicipality.CityMunName);

                // Reset other comboboxes and fetch related data
                cmb_barangay.Text = "";
                lbl_barangay.Text = "0";
                cmb_region.Text = "Region";
                cmb_province.Text = "Province";

                // Load barangays based on selected municipality
                Barangay(selectedMunicipality.PSGCCityMun);

                // Fetch and set the province name and PSGCRegion
                var (provinceName, psgcRegion) = GetProvinceNameAndRegion(selectedMunicipality.PSGCProvince);
                if (provinceName != null)
                {
                    cmb_province.Text = provinceName;

                    // Fetch and set the region name based on PSGCRegion
                    string regionName = GetRegionName(psgcRegion);
                    if (regionName != null)
                    {
                        cmb_region.Text = regionName;
                    }

                    // Store the psgcRegion in a field for later use in SaveMasterlistEntry
                    this.psgcRegion = psgcRegion;
                }
            }
        }

        private void cmb_municipality_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillMunicipalityandBarangay();
            //Province
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PSGCProvince, ProvinceName FROM lib_province WHERE ProvinceName='" + cmb_province.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_province.Text = dr["PSGCProvince"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }
            //Region
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PSGCRegion, Region FROM lib_region WHERE Region='" + cmb_region.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_region.Text = dr["PSGCRegion"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }


        }

        private void cmb_region_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void cmb_barangay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When clicked fill the lbl_barangay
            //Barangay
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PSGCBrgy, BrgyName FROM lib_barangay WHERE BrgyName='" + cmb_barangay.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_barangay.Text = dr["PSGCBrgy"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
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
                LoadDataAsync();
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

        private void cmb_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();  // Open the connection

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, Assessment FROM lib_assessment WHERE Assessment=@Assessment";

                // Use parameterized query to avoid SQL injection
                cmd.Parameters.AddWithValue("@Assessment", cmb_assessment.EditValue);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lbl_assessment.Text = dr["ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cmb_validator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, Validator FROM lib_validator WHERE Validator='" + cmb_validator.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_validator.Text = dr["ID"].ToString();
                }
                // con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
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
                if(txt_referencecode.Text == "")
                {
                    XtraMessageBox.Show("There is no Validation form uploaded on this particular beneficiary, please create and upload first to proceed.","Empty Validation",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
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
    }
}
