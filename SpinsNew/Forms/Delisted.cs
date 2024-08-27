using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Data;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class Delisted : Form
    {
 
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        public Delisted(MasterList masterlist, string username)
        {
            InitializeComponent();
            masterlistForm = masterlist;// Execute the MasterListform.
            con = new MySqlConnection(cs.dbcon);
            this.KeyPreview = true; // Allows the form to receive key events before the focused control does
            // Handle the KeyDown event
            this.KeyDown += Delisted_KeyDown;
            _username = username;
        }

        private void Delisted_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl+W or Alt+F4 is pressed
            if ((e.Control && e.KeyCode == Keys.W) || (e.Alt && e.KeyCode == Keys.F4))
            {
                // Close the form
                this.Close();
            }
        }

        public void DisplayID(int id)
        {
            // Display the ID in a label or textbox on your form
            txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
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
                cmb_year.SelectedIndexChanged -= Cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
                cmb_year.SelectedIndexChanged += Cmb_year_SelectedIndexChanged;
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
        // Custom class to store Id and DataSource
        public class StatusItem
        {
            public int Id { get; set; }
            public string Status { get; set; }


            public override string ToString()
            {
                return Status; // Display DataSource in the ComboBox
            }
        }

        //Fill combobox reportsource
        public void Status()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Status FROM lib_status"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_status.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["Id"]);
                    string status = dr["Status"].ToString();

                    // Skip the items with ID 1 and 99
                    if (id == 1 || id == 99)
                    {
                        continue;
                    }

                    // Add DataSourceItem to the ComboBox
                    cmb_status.Properties.Items.Add(new StatusItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Status = dr["Status"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        // Custom class to store Id and DataSource
        public class ReportSourceItem
        {
            public int Id { get; set; }
            public string ReportSource { get; set; }


            public override string ToString()
            {
                return ReportSource; // Display DataSource in the ComboBox
            }
        }

        //Fill combobox reportsource
        public void ReportSource()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, ReportSource FROM lib_report_source"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_source.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_source.Properties.Items.Add(new ReportSourceItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ReportSource = dr["ReportSource"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void LoadDetails()
        {
            try
            {
                con.Open();
                MySqlCommand cmd0 = con.CreateCommand();
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
                        tbl_masterlist m
                    LEFT JOIN
                        lib_region lr ON m.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN
                        lib_province lp ON m.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN
                        lib_city_municipality lcm ON m.PSGCCityMun = lcm.PSGCCityMun
                    LEFT JOIN
                        lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                    wHERE
                        m.ID = @ID";

                cmd0.Parameters.AddWithValue("@ID", txt_id.Text);

                DataTable dt0 = new DataTable();
                MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0);
                //Await to reduce lag while loading large amount of datas
                //await Task.Run(() => da0.Fill(dt0));
                da0.Fill(dt0);

                // Check if data is retrieved
                if (dt0.Rows.Count > 0)
                {
                    DataRow row = dt0.Rows[0];
                    // Concatenate and populate the fullname label
                    lbl_fullname.Text = $"{row["LastName"]}, {row["FirstName"]} {row["MiddleName"]} {row["ExtName"]}";
                    // Concatenate and populate the address label
                    lbl_address.Text = $"{row["Region"]}, {row["ProvinceName"]}, {row["Municipality"]}, {row["Barangay"]}";

                    lbl_region.Text = row["PSGCRegion"].ToString();
                    lbl_province.Text = row["PSGCprovince"].ToString();
                    lbl_municipality.Text = row["PSGCCityMun"].ToString();
                    lbl_barangay.Text = row["PSGCBrgy"].ToString();
                    lbl_currentstatus.Text = row["StatusID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        private void Delisted_Load(object sender, EventArgs e)
        {
            LoadDetails();
            ReportSource();
            Status();
            //Period();
            Year();
        }

        private void cmb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_status.SelectedItem is StatusItem selectedItem)
            {
                // Enable dt_deceased if the selected status ID is 2, otherwise disable it
                dt_deceased.Enabled = selectedItem.Id == 2;
                dt_deceased.EditValue = null;
            }

            try
            {



                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Status FROM lib_status WHERE Status='" + cmb_status.EditValue + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    lbl_status.Text = dr["ID"].ToString();
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

        // Inside Cmb_year_SelectedIndexChanged
        private void Cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is YearItem selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                LoadPeriodsForYear(selectedYear.Year);
            }
        }
        private MasterList masterlistForm;// Call MasterList form
        //Updating the masterlist on given property.
        private void UpdateMaster()
        {
            try
            {
                con.Open();
                var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
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
                cmd.Parameters.AddWithValue("@IDNumber", txt_id.Text);

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

                    int statusIDAfter = Convert.ToInt32(lbl_status.Text); // Assuming lbl_sex.Text contains the updated SexID

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
                ExclusionDate = @ExclusionDate
            WHERE 
                ID = @ID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", txt_id.Text);
                    cmd.Parameters.AddWithValue("@StatusID", lbl_status.Text);

                    if (dt_deceased.EditValue == null)
                    {
                        cmd.Parameters.AddWithValue("@DateDeceased", DBNull.Value);
                    }
                    else
                    {
                        DateTime deceasedDate;
                        //cmd.Parameters.AddWithValue("@DateDeceased", dt_deceased.EditValue);
                        if (DateTime.TryParse(dt_deceased.EditValue.ToString(), out deceasedDate))
                        {
                            // Use the formatted date string
                            string formattedDate = deceasedDate.ToString("yyyy-MM-dd"); // Must be Year-Month-Date
                            cmd.Parameters.AddWithValue("@DateDeceased", formattedDate); 
                        }
                        else
                        {
                            // Handle the case where the date cannot be parsed (if needed)
                            throw new InvalidOperationException("Invalid date format.");
                        }
                    }

                    cmd.Parameters.AddWithValue("@Remarks", txt_remarks.EditValue);
                    cmd.Parameters.AddWithValue("@ExclusionBatch", $"{cmb_year.EditValue}-{selectedPeriod.Abbreviation}"); // Plus the abbreviation from period
                    cmd.Parameters.AddWithValue("@ExclusionDate", DateTime.Now);
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
                            logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                            if (column == "StatusID")
                            {
                                logCmd.Parameters.AddWithValue("@Log", $"Status changed from [{statusNameBefore}] to [{statusNameAfter}] Source: [{cmb_source.Text}]");
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
        private void InsertintoDelist()
        {
            try
            {

                con.Open();
                var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                var selectedSource = (ReportSourceItem)cmb_source.SelectedItem;
                var selectedYear = (YearItem)cmb_year.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO tbl_delisted (MasterListID, PSGCRegion, PSGCProvince, PSGCCityMun, PSGCBrgy, StatusID, StatusRemarks, PeriodID, Year, " +
                                  "ReportSourceID, DelistedBy, DateTimeDelisted) " +
                                  "VALUES (@MasterListID, @PSGCRegion, @PSGCProvince, @PSGCCityMun, @PSGCBrgy, @StatusID, @StatusRemarks, @PeriodID, @Year, " +
                                  "@ReportSourceID, @DelistedBy, @DateTimeDelisted)";

                cmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                cmd.Parameters.AddWithValue("@PSGCRegion", lbl_region.Text);
                cmd.Parameters.AddWithValue("@PSGCProvince", lbl_province.Text);
                cmd.Parameters.AddWithValue("@PSGCCityMun", lbl_municipality.Text);
                cmd.Parameters.AddWithValue("@PSGCBrgy", lbl_barangay.Text);
                cmd.Parameters.AddWithValue("@StatusID", lbl_status.Text);
                cmd.Parameters.AddWithValue("@StatusRemarks", txt_remarks.EditValue);
                cmd.Parameters.AddWithValue("@PeriodID", selectedPeriod.PeriodID);
                cmd.Parameters.AddWithValue("@Year", selectedYear.Year);
                cmd.Parameters.AddWithValue("@ReportSourceID", selectedSource.Id);
                cmd.Parameters.AddWithValue("@DelistedBy", _username);
                cmd.Parameters.AddWithValue("@DateTimeDelisted", DateTime.Now);

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                con.Close();
            }

        }
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (cmb_status.Text == "" || cmb_source.Text == "" || cmb_year.Text == "" || cmb_period.Text == "")
            {
                MessageBox.Show("Please fill all the fields before delisting", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToInt32(lbl_currentstatus.Text) != 1)
            {
                if (XtraMessageBox.Show("Are you sure you want to delist this beneficiary?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateMaster();
                    return;
                }
            }
            if (Convert.ToInt32(lbl_currentstatus.Text) is 1)
            {

                if (XtraMessageBox.Show("Are you sure you want to delist this beneficiary?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InsertintoDelist();
                    UpdateMaster();
                    return;
                }

            }

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_year_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
