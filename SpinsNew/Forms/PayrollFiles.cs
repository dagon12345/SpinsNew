using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class PayrollFiles : DevExpress.XtraEditors.XtraForm
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        private PayrollHistory payrollHistoryForm;
        private Payroll _payrollForm;
        public PayrollFiles(PayrollHistory payroll, Payroll payrollForm)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            payrollHistoryForm = payroll;
            _payrollForm = payrollForm;
            //btn_left.shor = Keys.Control | Keys.P;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // For Two Keys
            if (keyData == (Keys.Control | Keys.Left))
            {
                btn_left.PerformClick();
                return true;
            }

            // For Two Keys
            if (keyData == (Keys.Control | Keys.Right))
            {
                btn_rotate.PerformClick();
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void PayrollFiles_Load(object sender, EventArgs e)
        {

            Municipality();
            Year();
            FillMunicipalityAndYear();
            DisplayFoldersInComboBoxEdit();
            BindEvents();
            list_pictures.Focus();

        }
        private void Clear()
        {
            cmb_selectfolder.Text = "Empty Folder";
            cmb_selectfolder.Properties.Items.Clear();
            pictureEdit1.Image = null;
            list_pictures.Items.Clear();
        }
        private void UpdateFolderComboBox()
        {
           // PeriodMethod();
            try
            {
                // Retrieve values from controls
                string year = cmb_year.SelectedItem?.ToString();
                string municipality = lbl_municipality.Text;
                string period = lblPeriod.Text;
            
                // Ensure all values are provided
                if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(municipality) && !string.IsNullOrEmpty(period))
                {
                    // Construct the folder path
                    string folderPath = Path.Combine(@"\\172.26.153.181\Payroll", year, period, municipality);

                    // Check if the directory exists
                    if (Directory.Exists(folderPath))
                    {
                        // Get all the directories (subfolders) in the folderPath
                        string[] folders = Directory.GetDirectories(folderPath);

                        // Clear existing items in the ComboBoxEdit
                        cmb_selectfolder.Properties.Items.Clear();

                        // Add folder names to the ComboBoxEdit
                        foreach (string folder in folders)
                        {
                            cmb_selectfolder.Properties.Items.Add(Path.GetFileName(folder)); // Add folder name only
                        }

                        // Optionally, set the first item as the selected value
                        if (cmb_selectfolder.Properties.Items.Count > 0)
                        {
                            cmb_selectfolder.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        // Clear the ComboBoxEdit if the directory doesn't exist
                        Clear();
                        // XtraMessageBox.Show("Folder path not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Clear the ComboBoxEdit if the required fields are not selected
                    Clear();
                }
            }
            catch 
            {
                // XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
        }
        private void MunicipalityMethod()
        {
            try
            {
                // Get the selected item from the ComboBox
                var selectedItem = cmb_municipality.SelectedItem as MunicipalityItem;

                if (selectedItem != null)
                {
                    // Set the label text to PSGCCityMun from the selected item
                    lbl_municipality.Text = selectedItem.PSGCCityMun.ToString();
                }
                else
                {
                    lbl_municipality.Text = "No municipality selected";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PeriodMethod()
        {
            try
            {
                // Get the selected item from the ComboBox
                var selectedItem = cmb_period.SelectedItem as PeriodItem;

                if (selectedItem != null)
                {
                    // Set the label text to PSGCCityMun from the selected item
                    lblPeriod.Text = selectedItem.PeriodID.ToString();
                }
                else
                {
                    lblPeriod.Text = "No Period selected";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Bind the UpdateFolderComboBox method to events
        private void BindEvents()
        {
            cmb_year.SelectedIndexChanged += (s, e) => UpdateFolderComboBox();
            lbl_municipality.TextChanged += (s, e) => UpdateFolderComboBox(); // Assuming lbl_municipality.Text changes
            lblPeriod.TextChanged += (s, e) => UpdateFolderComboBox(); // Assuming lblPeriod.Text changes
        }

        public void DisplayFoldersInComboBoxEdit()
        {
            try
            {
                // Retrieve values from controls
                string year = cmb_year.SelectedItem?.ToString();
                string municipality = lbl_municipality.Text;
                string period = lblPeriod.Text;

                // Ensure all values are provided
                if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(municipality) && !string.IsNullOrEmpty(period))
                {
                    // Construct the folder path
                    string folderPath = Path.Combine(@"\\172.26.153.181\Payroll", year, period, municipality);

                    // Check if the directory exists
                    if (Directory.Exists(folderPath))
                    {
                        // Get all the directories (subfolders) in the folderPath
                        string[] folders = Directory.GetDirectories(folderPath);

                        // Clear existing items in the ComboBoxEdit
                        cmb_selectfolder.Properties.Items.Clear();

                        // Add folder names to the ComboBoxEdit
                        foreach (string folder in folders)
                        {
                            // Add the folder name without the full path
                            cmb_selectfolder.Properties.Items.Add(Path.GetFileName(folder));
                        }

                        // Optionally, set the first item as the selected value
                        if (cmb_selectfolder.Properties.Items.Count > 0)
                        {
                            cmb_selectfolder.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                       // XtraMessageBox.Show("Folder path not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmb_selectfolder.Text = "Empty Folder";
                    }
                }
                else
                {
                    // XtraMessageBox.Show("Please ensure all required fields are selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmb_selectfolder.Text = "Empty Folder";
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_selectfolder.Text = "Empty Folder";
            }
        }

        //public void DisplayID(int municipality, int year, int period)
        //{
        //    // Display the ID in a label or textbox on your form
        //   // txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
        //    lbl_municipality.Text = municipality.ToString(); // Assuming lblID is a label on your form
        //    cmb_year.EditValue = year.ToString(); // Assuming lblID is a label on your form
        //    lblPeriod.Text = period.ToString(); // Assuming lblID is a label on your form
        //}

        public void DisplayID(int municipality, int year, int period)
        {
            // Display the ID in a label or textbox on your form
            // txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
            lbl_municipality.Text = municipality.ToString(); // Assuming lblID is a label on your form
            cmb_year.EditValue = year.ToString(); // Assuming lblID is a label on your form
            lblPeriod.Text = period.ToString(); // Assuming lblID is a label on your form
        }

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
                cmd.CommandText = "SELECT Id, Year FROM lib_year ORDER BY Id DESC"; // Specify the columns to retrieve
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
                ORDER BY 
                ProvinceName,
                m.CityMunName"; // Join with lib_province to get ProvinceName
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                //cmb_municipality.Properties.Items.Clear();

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
        //Load data from tables and display into textboxes
        public void FillMunicipalityAndYear()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @" SELECT 
                tp.ID,
                tp.MasterListID,
                lcm.CityMunName as Municipality,
                lcm.PSGCCityMun,
                tp.Year,
                lp.Period as Period,
                lp.PeriodID as PeriodID
            FROM 
                tbl_payroll_socpen tp
            LEFT JOIN
                lib_city_municipality lcm ON tp.PSGCCityMun = lcm.PSGCCityMun
            LEFT JOIN
                lib_period lp ON tp.PeriodID = lp.PeriodID
            WHERE 
                lcm.PSGCCityMun = @PSGCCityMun
                AND lp.PeriodID = @PeriodID";
              
                cmd.Parameters.AddWithValue("@PSGCCityMun", lbl_municipality.Text);
                cmd.Parameters.AddWithValue("@PeriodID", lblPeriod.Text);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //Await to reduce lag while loading large amount of datas
                da.Fill(dt);

                // Check if data is retrieved
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];


                    cmb_municipality.EditValue = row["Municipality"];
                    //lbl_municipality.Text = row["PSGCCityMun"].ToString();
                    //cmb_year.EditValue = row["Year"];

                    cmb_period.EditValue = row["Period"];
                    //lblPeriod.Text = row["PeriodID"].ToString();

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

        private void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is YearItem selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                LoadPeriodsForYear(selectedYear.Year);
            }
        }

        private void cmb_selectfolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected folder name
                string selectedFolder = cmb_selectfolder.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedFolder))
                {
                    // Construct the full folder path
                    string year = cmb_year.SelectedItem?.ToString();
                    string municipality = lbl_municipality.Text;
                    string period = lblPeriod.Text;
                    string folderPath = Path.Combine(@"\\172.26.153.181\Payroll", year, period, municipality, selectedFolder);

                    // Get all image files (e.g., .jpg) in the selected folder
                    string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");

                    // Clear the ListBoxControl before adding new items
                    list_pictures.Items.Clear();

                    // Add image file names to the ListBoxControl
                    foreach (string imageFile in imageFiles)
                    {
                        list_pictures.Items.Add(Path.GetFileName(imageFile)); // Add file name only, without the full path
                    }
                }
            }
            catch (Exception ex)
            {
                Clear();
            }
        }
        private void ListOfPictures()
        {
            // Get the selected image file name
            string selectedImage = list_pictures.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedImage))
            {
                // Construct the full image file path
                string year = cmb_year.SelectedItem?.ToString();
                string municipality = lbl_municipality.Text;
                string period = lblPeriod.Text;
                string selectedFolder = cmb_selectfolder.SelectedItem?.ToString();
                string imagePath = Path.Combine(@"\\172.26.153.181\Payroll", year, period, municipality, selectedFolder, selectedImage);

                // Check if the image file exists
                if (File.Exists(imagePath))
                {
                    // Load the image into PictureEdit
                    pictureEdit1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    //XtraMessageBox.Show("Image file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                }
            }
        }
        private void list_pictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListOfPictures();
                //if (!ck_default.Checked) // Not checked
                //{

                //    ListOfPictures();
                //}
                //else
                //{
                  
                //    ListOfPictures();
                //    Rotate();
                //}

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
        }

        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            MunicipalityMethod();

            

        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            PeriodMethod();
        }



        private void RotateRight()
        {
            Image image = pictureEdit1.Image.Clone() as Image;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureEdit1.Image.Dispose();
            pictureEdit1.Image = image;
        }
        private void RotateLeft()
        {
            Image image = pictureEdit1.Image.Clone() as Image;
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureEdit1.Image.Dispose();
            pictureEdit1.Image = image;
        }
        private void btn_rotate_Click(object sender, EventArgs e)
        {
            //Rotate image 90 degrees. Means left side
            //var bmp = new Bitmap(pictureEdit1.Image);

            //using (var gfx = Graphics.FromImage(bmp))
            //{
            //    gfx.Clear(Color.White);
            //    gfx.DrawImage(pictureEdit1.Image, 0, 0, pictureEdit1.Image.Width, pictureEdit1.Image.Height);
            //}

            //bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

            //pictureEdit1.Image = bmp;
            RotateRight();
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            RotateLeft();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            // Retrieve values from controls
            string year = cmb_year.SelectedItem?.ToString();
            string municipality = lbl_municipality.Text;
            string period = lblPeriod.Text;

            // Define the base path
            string basePath = @"\\172.26.153.181\Payroll";

            // Check and create year, period, and municipality folders if they don't exist
            string folderPath = Path.Combine(basePath, year, period, municipality);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Determine the incremented folder
            string destinationFolder = folderPath;
            int folderIncrement = 1;
            while (Directory.Exists(destinationFolder))
            {
                destinationFolder = Path.Combine(folderPath, $"{folderIncrement}");
                folderIncrement++;
            }

            // Create the incremented folder
            Directory.CreateDirectory(destinationFolder);

            // Open file dialog to select JPG files
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Loop through selected files and copy them to the incremented folder
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(fileName));
                        File.Copy(fileName, destinationPath, overwrite: true);
                    }

                    XtraMessageBox.Show("Files uploaded successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }
    }
}