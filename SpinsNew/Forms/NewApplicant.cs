using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class NewApplicant : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        public NewApplicant(string username)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);

            // Subscribe to the ValueChanged event of the DateTimePicker
            dt_birth.EditValueChanged += new EventHandler(Dt_birth_ValueChanged);

            // Initialize label text
            lbl_age.Text = "Age: ";

            // Set minimum date to 60 years ahead
            dt_birth.Properties.MaxValue = DateTime.Today.AddYears(-60);

            // Subscribe to the Leave event
            txt_lastname.Leave += new EventHandler(Txt_lastname_Leave);
            txt_firstname.Leave += new EventHandler(Txt_firstname_Leave);
            txt_middlename.Leave += new EventHandler(Txt_middlename_Leave);
            txt_extname.Leave += new EventHandler(Txt_extname_Leave);

            _username = username;
        }

    
        private async void Txt_lastname_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txt_lastname.Text))
            {
                return;
            }
            // Call the method to load data with the search filter
            await LoadDataAsync();
        }
        private async void Txt_firstname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_firstname.Text))
            {
                return;
            }
            // Call the method to load data with the search filter
            await LoadDataAsync();
        }
        private async void Txt_middlename_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_middlename.Text))
            {
                return;
            }
            // Call the method to load data with the search filter
            await LoadDataAsync();
        }
        private async void Txt_extname_Leave(object sender, EventArgs e)
        {
      
            // Call the method to load data with the search filter
            await LoadDataAsync();
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
                lbl_age.Text = $"Age: {age}";
            }
            else
            {
                lbl_age.Text = "0";
            }
        }

        private void NewApplicant_Load(object sender, EventArgs e)
        {
            //Call the methods below to fill the comboboxes
            DataSource();
            Sex();
            Marital();
            HealthStatus();
            InitializeComboboxes();
            DisplayAge();
        
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
        // For municipality combobox
        //public class MunicipalityItem
        //{
        //    public int PSGCCityMun { get; set; }
        //    public string CityMunName { get; set; }
        //    public int PSGCProvince { get; set; } // Add this property

        //    public override string ToString()
        //    {
        //        return CityMunName; // Display DataSource in the ComboBox
        //    }
        //}


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




        //Fill combobox datasource
        public void Sex()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Sex FROM lib_sex"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_sex.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_sex.Properties.Items.Add(new SexItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Sex = dr["Sex"].ToString()
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
        //public void Municipality()
        //{
        //    try
        //    {
        //        // Fetch data from the database and bind to ComboBox
        //        con.Open();
        //        MySqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT PSGCCityMun, CityMunName, PSGCProvince FROM lib_city_municipality"; // Specify the columns to retrieve
        //        cmd.ExecuteNonQuery();
        //        DataTable dt = new DataTable();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(dt);

        //        // Clear existing items in the ComboBoxEdit
        //        cmb_municipality.Properties.Items.Clear();

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            // Add DataSourceItem to the ComboBox
        //            cmb_municipality.Properties.Items.Add(new MunicipalityItem
        //            {
        //                PSGCCityMun = Convert.ToInt32(dr["PSGCCityMun"]),
        //                CityMunName = dr["CityMunName"].ToString(),
        //                PSGCProvince = Convert.ToInt32(dr["PSGCProvince"]) // Populate the new property
        //            });
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }

        //}
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

        }

        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*When combobox municipality was clicked with different value it
       changes the combobox barangay that corresponds into municipality value in tbl_barangay*/
            cmb_barangay.Text = "";
            lbl_region.Text = "Region";
            lbl_province.Text = "Province";
            if (cmb_municipality.SelectedItem is MunicipalityItem selectedMunicipality)
            {
                // Load barangays based on selected municipality
                Barangay(selectedMunicipality.PSGCCityMun);

                // Fetch and set the province name and PSGCRegion
                var (provinceName, psgcRegion) = GetProvinceNameAndRegion(selectedMunicipality.PSGCProvince);
                if (provinceName != null)
                {
                    lbl_province.Text = provinceName;

                    // Fetch and set the region name based on PSGCRegion
                    string regionName = GetRegionName(psgcRegion);
                    if (regionName != null)
                    {
                        lbl_region.Text = regionName; // Assuming you have a textbox named txt_region
                    }

                    // Store the psgcRegion in a field for later use in SaveMasterlistEntry
                    this.psgcRegion = psgcRegion;
                }
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
            gr_similar.DataSource = null;
            txt_lastname.Focus();

        }
        // Add event handler to the municipality combobox
        public void InitializeComboboxes()
        {
            Municipality(); // Load municipalities
            cmb_municipality.SelectedIndexChanged += new EventHandler(cmb_municipality_SelectedIndexChanged);
            // Initialize barangay combobox if necessary, otherwise it will be populated upon municipality selection
        }

        private int psgcRegion; // Field to store PSGCRegion

        private void SaveMasterlistEntry()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_lastname.Text) || string.IsNullOrWhiteSpace(txt_firstname.Text) || string.IsNullOrWhiteSpace(txt_middlename.Text))
                {
                    XtraMessageBox.Show("Please fill the names before saving.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmb_municipality.Text) || string.IsNullOrWhiteSpace(cmb_barangay.Text))
                {
                    XtraMessageBox.Show("Please select a municipality and barangay before proceeding.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(dt_birth.Text))
                {
                    XtraMessageBox.Show("Please enter birth date before proceeding", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmb_sex.Text))
                {
                    XtraMessageBox.Show("Select sex before proceeding.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cmb_datasource.Text == "Select Data Source")
                {
                    XtraMessageBox.Show("Select datasource before proceeding.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                con.Open();

                var selectedSex = (SexItem)cmb_sex.SelectedItem;
                var selectedMarital = (MaritalItem)cmb_marital.SelectedItem;
                var selectedHealthStatus = (HealthStatusItem)cmb_healthstatus.SelectedItem;
                var selectedDataSource = (DataSourceItem)cmb_datasource.SelectedItem;

                // var selectedProvince = (Province)cmb_barangay.SelectedItem;
                var selectedMunicipality = (MunicipalityItem)cmb_municipality.SelectedItem;
                var selectedBarangay = (BarangayItem)cmb_barangay.SelectedItem;

                if (selectedMunicipality == null || selectedBarangay == null)
                {
                    XtraMessageBox.Show("Invalid municipality or barangay selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return;
                }

                // int psgcRegion = selectedMunicipality.PSGCProvince;
                string provinceName = lbl_province.Text;
                string regionName = lbl_region.Text;

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO tbl_masterlist (LastName, FirstName, MiddleName, ExtName, PSGCRegion, PSGCProvince, PSGCCityMun, PSGCBrgy, Address, " +
                                  "BirthDate, SexID, MaritalStatusID, IDTypeID, IDNumber, IDDateIssued, Pantawid, Indigenous, HealthStatusID, HealthStatusRemarks, DateTimeEntry, EntryBy, " +
                                  "DataSourceID, StatusID) " +
                                  "VALUES (@LastName, @FirstName, @MiddleName, @ExtName, @PSGCRegion, @PSGCProvince, @PSGCCityMun, @PSGCBrgy, @Address, " +
                                  "@BirthDate, @SexID, @MaritalStatusID, @IDTypeID, @IDNumber, @IDDateIssued, @Pantawid, @Indigenous, @HealthStatusID, @HealthStatusRemarks, @DateTimeEntry, @EntryBy, " +
                                  "@DataSourceID, @StatusID)";

                cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
                cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
                cmd.Parameters.AddWithValue("@MiddleName", txt_middlename.Text);
                cmd.Parameters.AddWithValue("@ExtName", txt_extname.Text);
                cmd.Parameters.AddWithValue("@PSGCRegion", psgcRegion);
                cmd.Parameters.AddWithValue("@PSGCProvince", selectedMunicipality.PSGCProvince);
                cmd.Parameters.AddWithValue("@PSGCCityMun", selectedMunicipality.PSGCCityMun);
                cmd.Parameters.AddWithValue("@PSGCBrgy", selectedBarangay.PSGCBrgy);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dt_birth.EditValue);
                cmd.Parameters.AddWithValue("@SexID", selectedSex.Id); // Assuming you have a value member set for cmb_sex
                cmd.Parameters.AddWithValue("@MaritalStatusID", selectedMarital.Id); // Assuming you have a value member set for cmb_maritalstatus
                cmd.Parameters.AddWithValue("@IDTypeID", 1); // Assuming you have a value member set for cmb_idtype
                cmd.Parameters.AddWithValue("@IDNumber", txt_id.Text);
                cmd.Parameters.AddWithValue("@IDDateIssued", dt_dateissued.EditValue);
                cmd.Parameters.AddWithValue("@Pantawid", ck_pantawid.Checked);
                cmd.Parameters.AddWithValue("@Indigenous", ck_indigenous.Checked);
                cmd.Parameters.AddWithValue("@HealthStatusID", selectedHealthStatus.Id); // Assuming you have a value member set for cmb_healthstatus
                cmd.Parameters.AddWithValue("@HealthStatusRemarks", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                cmd.Parameters.AddWithValue("@EntryBy", _username); // Assuming you want to track the user who made the entry
                cmd.Parameters.AddWithValue("@DataSourceID", selectedDataSource.Id); // Assuming you have a value member set for cmb_datasource
                cmd.Parameters.AddWithValue("@StatusID", 99); // Assuming you have a value member set for cmb_status

                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                XtraMessageBox.Show("Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form and refresh the display
                // clear();
                // display();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                con.Close();
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            var gridView = gr_similar.MainView as GridView;
            if (gridView.RowCount > 0)
            {
                if (MessageBox.Show("There are similar data's related, are you sure you want to continue?", "Related Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveMasterlistEntry();
                }
                return;
            }
            SaveMasterlistEntry();
        }

        //Load masterlist below
        private async Task LoadDataAsync()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                        m.ID,
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName,
                        MAX(m.Citizenship) as Citizenship,
                        MAX(m.MothersMaiden) as MothersMaiden,
                        MAX(lr.Region) as Region,
                        MAX(lp.ProvinceName) as Province,
                        MAX(lc.CityMunName) as Municipality,
                        MAX(lb.BrgyName) as Barangay,
                        MAX(m.Address) as Address,
                        MAX(m.BirthDate) as BirthDate          
                    FROM 
                        tbl_masterlist m
                    LEFT JOIN 
                        lib_region lr ON m.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN 
                        lib_province lp ON m.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN 
                        lib_city_municipality lc ON m.PSGCCityMun = lc.PSGCCityMun
                    LEFT JOIN 
                        lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                    WHERE 

                m.DateTimeDeleted IS NULL
                AND (@LastName IS NULL OR m.LastName LIKE @LastName)
                AND (@FirstName IS NULL OR m.FirstName LIKE @FirstName)
                AND (@MiddleName IS NULL OR m.MiddleName LIKE @MiddleName)
                AND (@ExtName IS NULL OR m.ExtName LIKE @ExtName)
                        
                    GROUP BY
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName";

                //cmd.CommandText = sqlQuery;
                // Add parameters for search filters
                cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(txt_lastname.Text) ? (object)DBNull.Value : "%" + txt_lastname.Text + "%");
                cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(txt_firstname.Text) ? (object)DBNull.Value : "%" + txt_firstname.Text + "%");
                cmd.Parameters.AddWithValue("@MiddleName", string.IsNullOrEmpty(txt_middlename.Text) ? (object)DBNull.Value : "%" + txt_middlename.Text + "%");
                cmd.Parameters.AddWithValue("@ExtName", string.IsNullOrEmpty(txt_extname.Text) ? (object)DBNull.Value : "%" + txt_extname.Text + "%");


                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //Await to reduce lag while loading large amount of datas
                await Task.Run(() => da.Fill(dt));
                //We are using DevExpress datagridview
                gr_similar.DataSource = dt;

                // Get the GridView instance
                GridView gridView = gr_similar.MainView as GridView;
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

        private void txt_extname_Leave(object sender, EventArgs e)
        {

        }
    }
}
