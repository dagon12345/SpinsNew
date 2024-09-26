using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Libraries;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using System;
using System.Data;
using System.Linq;
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
        protected override async void OnLoad(EventArgs e)
        {
            await DataSourceEF();
            await SexEf();
            await MaritalEf();
            await HealthStatusEf();
            InitializeComboboxes();
            DisplayAge();
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
        private async Task SaveEF()
        {

     
            var selectedSex = (LibrarySex)cmb_sex.SelectedItem;
            var selectedMarital = (LibraryMaritalStatus)cmb_marital.SelectedItem;
            var selectedHealthStatus = (LibraryHealthStatus)cmb_healthstatus.SelectedItem;
            var selectedDataSource = (LibraryDataSource)cmb_datasource.SelectedItem;

            // var selectedProvince = (Province)cmb_barangay.SelectedItem;
            var selectedMunicipality = (MunicipalityItem)cmb_municipality.SelectedItem;
            var selectedBarangay = (BarangayItem)cmb_barangay.SelectedItem;

            using (var context = new ApplicationDbContext())
            {
                var masterlist = new MasterListModel
                {
                    LastName = txt_lastname.Text,
                    FirstName = txt_firstname.Text,
                    MiddleName = txt_middlename.Text,
                    ExtName = txt_extname.Text,
                    PSGCRegion = psgcRegion,
                    PSGCProvince = selectedMunicipality.PSGCProvince,
                    PSGCCityMun = selectedMunicipality.PSGCCityMun,
                    PSGCBrgy = selectedBarangay.PSGCBrgy,
                    Address = txt_address.Text,
                    BirthDate = Convert.ToDateTime(dt_birth.EditValue),
                    IDNumber = txt_id.Text,
                    IDDateIssued = Convert.ToDateTime(dt_dateissued.EditValue),
                    SexID = selectedSex.Id,
                    MaritalStatusID = selectedMarital.Id,
                    HealthStatusID = selectedHealthStatus.Id,
                    HealthStatusRemarks = txt_remarks.Text,
                    Pantawid = ck_pantawid.Checked,
                    Indigenous = ck_indigenous.Checked,
                    DataSourceId = selectedDataSource.Id,
                    DateTimeEntry = DateTime.Now,
                    EntryBy = _username,
                    StatusID = 99,
                    RegTypeId = 1
                    
                };
                context.tbl_masterlist.Add(masterlist);
                await context.SaveChangesAsync();

                Clear();
                XtraMessageBox.Show("Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private async void btn_save_Click(object sender, EventArgs e)
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


            var gridView = gr_similar.MainView as GridView;
            if (gridView.RowCount > 0)
            {
                if (MessageBox.Show("There are similar data's related, are you sure you want to continue?", "Related Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   await SaveEF();
                }
                return;
            }
            await SaveEF();
        }

        private async Task LoadDataAsync()
        {
            GridView gridView = gr_similar.MainView as GridView;
            // XtraMessageBox.Show("Hello");
            using (var context = new ApplicationDbContext())
            {
                var searchMasterlist = await context.tbl_masterlist
                    .Include(r => r.LibraryRegion)
                    .Include(p => p.LibraryProvince)
                    .Include(m => m.LibraryMunicipality)
                    .Include(b => b.LibraryBarangay)
                    .Include(s => s.LibraryStatus)
                    .Where(l => l.LastName.StartsWith(txt_lastname.Text.ToUpper())
                    && l.FirstName.StartsWith(txt_firstname.Text.ToUpper())
                    && l.MiddleName.StartsWith(txt_middlename.Text.ToUpper())
                    && l.ExtName.StartsWith(txt_extname.Text.ToUpper())
                    && l.DateTimeDeleted == null)
                    .Select(n => new MasterListViewModel 
                    { 
                        LastName = n.LastName,
                        FirstName = n.FirstName,
                        MiddleName = n.MiddleName,
                        ExtName = n.ExtName,
                        Municipality = n.LibraryMunicipality.CityMunName,
                        Barangay = n.LibraryBarangay.BrgyName,
                        Address = n.Address,
                        BirthDate = n.BirthDate,
                        Status = n.LibraryStatus.Status
                    })
                    .AsNoTracking()
                    .ToListAsync();

                masterListViewModelBindingSource.DataSource = searchMasterlist;
                gr_similar.DataSource = masterListViewModelBindingSource;

                // Auto-size all columns based on their content
                gridView.BestFitColumns();

                // Ensure horizontal scrollbar is enabled
                gridView.OptionsView.ColumnAutoWidth = false;

                // Disable editing
                gridView.OptionsBehavior.Editable = false;

            }
        }
        private void txt_extname_Leave(object sender, EventArgs e)
        {

        }
    }
}
