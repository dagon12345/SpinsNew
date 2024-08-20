using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Popups
{
    public partial class PayrollPopup : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        private MasterList masterlistForm;

        public PayrollPopup(MasterList masterListForm, string username) //Instantiate to make the gridcontrol from form masterlist work into the payrollpopup form.
        {
            InitializeComponent();
            this.masterlistForm = masterListForm; //Instantiate to make the gridcontrol from form masterlist work into the payrollpopup form.
            // masterlistForm = masterlist;// Execute the MasterListform.
            con = new MySqlConnection(cs.dbcon);
            _username = username;
        }


        private void PayrollPopup_Load(object sender, EventArgs e)
        {
            PopulateYear();
            PayrollTypeMethod();
            PayrollTagMethod();
            PaymentModeMethod();
        }
        // Custom class to store Id and Sex
        public class YearItem
        {
            public int Id { get; set; }
            public int Year { get; set; }
            public double MonthlyStipend { get; set; }

            public override string ToString()
            {
                return Year.ToString();
            }
        }
        // Fill combobox datasource
        public void PopulateYear()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, Year, MonthlyStipend FROM lib_year WHERE Active = 1"; // Specify the columns to retrieve
                MySqlDataReader reader = cmd.ExecuteReader();

                List<YearItem> yearItems = new List<YearItem>();

                while (reader.Read())
                {
                    yearItems.Add(new YearItem
                    {
                        Id = reader.GetInt32("Id"),
                        Year = reader.GetInt32("Year"),
                        MonthlyStipend = reader.GetInt32("MonthlyStipend")
                    });
                }


                reader.Close();
                con.Close();
                cmb_year.Properties.Items.Clear();
                cmb_year.Properties.Items.AddRange(yearItems);
                // Add the event handler for the SelectedIndexChanged event
                cmb_year.SelectedIndexChanged -= cmb_year_SelectedIndexChanged; // Ensure it's not added multiple times
                cmb_year.SelectedIndexChanged += cmb_year_SelectedIndexChanged;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Custom class to store Id and Sex
        public class PayrollTypeItem
        {
            public int PayrollTypeID { get; set; }
            public string PayrollType { get; set; }

            public override string ToString()
            {
                return PayrollType;
            }
        }
        // Fill combobox datasource
        public void PayrollTypeMethod()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PayrollTypeID, PayrollType FROM lib_payroll_type"; // Specify the columns to retrieve
                MySqlDataReader reader = cmd.ExecuteReader();

                List<PayrollTypeItem> typeItems = new List<PayrollTypeItem>();

                while (reader.Read())
                {
                    typeItems.Add(new PayrollTypeItem
                    {
                        PayrollTypeID = reader.GetInt32("PayrollTypeID"),
                        PayrollType = reader.GetString("PayrollType")
                        //MonthlyStipend = reader.GetInt32("MonthlyStipend")
                    });
                }


                reader.Close();
                con.Close();
                cmb_type.Properties.Items.Clear();
                cmb_type.Properties.Items.AddRange(typeItems);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Custom class to store Id and Sex
        public class PayrollTagItem
        {
            public int PayrollTagID { get; set; }
            public string PayrollTag { get; set; }

            public override string ToString()
            {
                return PayrollTag;
            }
        }
        // Fill combobox datasource
        public void PayrollTagMethod()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PayrollTagID, PayrollTag FROM lib_payroll_tag"; // Specify the columns to retrieve
                MySqlDataReader reader = cmd.ExecuteReader();

                List<PayrollTagItem> tagItems = new List<PayrollTagItem>();

                while (reader.Read())
                {
                    tagItems.Add(new PayrollTagItem
                    {
                        PayrollTagID = reader.GetInt32("PayrollTagID"),
                        PayrollTag = reader.GetString("PayrollTag")
                        //MonthlyStipend = reader.GetInt32("MonthlyStipend")
                    });
                }


                reader.Close();
                con.Close();
                cmb_tag.Properties.Items.Clear();
                cmb_tag.Properties.Items.AddRange(tagItems);

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
                //return Period;
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

        private void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmb_year.SelectedItem is YearItem selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                LoadPeriodsForYear(selectedYear.Year);
            }


            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Year, MonthlyStipend FROM lib_year WHERE Year=@Year";
                cmd.Parameters.AddWithValue("@Year", cmb_year.EditValue);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txt_monthlystipend.Text = dr["MonthlyStipend"].ToString();
                    cmb_period.EditValue = null; // Null the period if the year was changed.
                    txt_multiplier.EditValue = 0;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        // Custom class to store Id and Sex
        public class PaymentModeItem
        {
            public int PaymentModeID { get; set; }
            public string PaymentMode { get; set; }

            public override string ToString()
            {
                return PaymentMode;
            }
        }
        // Fill combobox datasource
        public void PaymentModeMethod()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PaymentModeID, PaymentMode FROM lib_payment_mode"; // Specify the columns to retrieve
                MySqlDataReader reader = cmd.ExecuteReader();

                List<PaymentModeItem> paymentItems = new List<PaymentModeItem>();

                while (reader.Read())
                {
                    paymentItems.Add(new PaymentModeItem
                    {
                        PaymentModeID = reader.GetInt32("PaymentModeID"),
                        PaymentMode = reader.GetString("PaymentMode")
                        //MonthlyStipend = reader.GetInt32("MonthlyStipend")
                    });
                }


                reader.Close();
                con.Close();
                cmb_payment.Properties.Items.Clear();
                cmb_payment.Properties.Items.AddRange(paymentItems);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void InsertIntoPayroll()
        {
            try
            {

                con.Open();
                var selectedYear = (YearItem)cmb_year.SelectedItem;
                var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                var selectedType = (PayrollTypeItem)cmb_type.SelectedItem;
                var selectedTag = (PayrollTagItem)cmb_tag.SelectedItem;
                var selectedMode = (PaymentModeItem)cmb_payment.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO tbl_payroll_socpen (MasterListID, PSGCRegion, PSGCProvince, PSGCCityMun, PSGCBrgy, Address, Amount, Year, PeriodID, PayrollStatusID," +
                    "PayrollTypeID, PayrollTagID, PaymentModeID, DateTimeEntry, EntryBy) " +
                    "VALUES (@MasterListID, @PSGCRegion, @PSGCProvince, @PSGCCityMun, @PSGCBrgy, @Address, @Amount, @Year, @PeriodID, @PayrollStatusID, @PayrollTypeID, @PayrollTagID," +
                    "@PaymentModeID, @DateTimeEntry, @EntryBy)";
                cmd.Parameters.Add("@MasterListID", MySqlDbType.Int32);
                cmd.Parameters.Add("@PSGCRegion", MySqlDbType.Int32);
                cmd.Parameters.Add("@PSGCProvince", MySqlDbType.Int32);
                cmd.Parameters.Add("@PSGCCityMun", MySqlDbType.Int32);
                cmd.Parameters.Add("@PSGCBrgy", MySqlDbType.Int32);
                cmd.Parameters.Add("@Address", MySqlDbType.TinyText);
                cmd.Parameters.Add("@Amount", MySqlDbType.Double);
                cmd.Parameters.Add("@Year", MySqlDbType.Int32);
                cmd.Parameters.Add("@PeriodID", MySqlDbType.Int32);
                cmd.Parameters.Add("@PayrollStatusID", MySqlDbType.Int32);
                cmd.Parameters.Add("@PayrollTypeID", MySqlDbType.Int32);
                cmd.Parameters.Add("@PayrollTagID", MySqlDbType.Int32);
                cmd.Parameters.Add("@PaymentModeID", MySqlDbType.Int32);
                cmd.Parameters.Add("@DateTimeEntry", MySqlDbType.DateTime);
                cmd.Parameters.Add("@EntryBy", MySqlDbType.String);

                GridView gridView = masterlistForm.gridControl1.MainView as GridView;

                double amount = 0;
                if (!string.IsNullOrEmpty(txt_amount.Text))
                {
                    amount = Convert.ToDouble(txt_amount.Text);
                }
           
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    DataRowView row = (DataRowView)gridView.GetRow(i);
                    if (row != null)
                    {
                        //This datatype and variables are from our table masterlist.
                        int id = Convert.ToInt32(row["ID"]);
                        int region = Convert.ToInt32(row["PSGCRegion"]);
                        int province = Convert.ToInt32(row["PSGCProvince"]);
                        int municipality = Convert.ToInt32(row["PSGCCityMun"]);
                        int barangay = Convert.ToInt32(row["PSGCBrgy"]);
                        string address = row["Address"].ToString();
                       
                        //Below is from our masterlist form.
                        cmd.Parameters["@MasterListID"].Value = id;
                        cmd.Parameters["@PSGCRegion"].Value = region;
                        cmd.Parameters["@PSGCProvince"].Value = province;
                        cmd.Parameters["@PSGCCityMun"].Value = municipality;
                        cmd.Parameters["@PSGCBrgy"].Value = barangay;
                        cmd.Parameters["@Address"].Value = address;

                        //Below is from our payroll form.
                        cmd.Parameters["@Amount"].Value = amount;
                        cmd.Parameters["@Year"].Value = selectedYear.Year;
                        cmd.Parameters["@PeriodID"].Value = selectedPeriod.PeriodID;
                        cmd.Parameters["@PayrollStatusID"].Value = 2;
                        cmd.Parameters["@PayrollTypeID"].Value = selectedType.PayrollTypeID;
                        cmd.Parameters["@PayrollTagID"].Value = selectedTag.PayrollTagID;
                        cmd.Parameters["@PaymentModeID"].Value = selectedMode.PaymentModeID;
                        cmd.Parameters["@DateTimeEntry"].Value = DateTime.Now;
                        cmd.Parameters["@EntryBy"].Value = _username;

                        cmd.ExecuteNonQuery();
                    }
                }

                con.Close();
                XtraMessageBox.Show("Payroll created successfully. Please proceed to the payroll form to generate reports", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                con.Close();
            }

        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            if (masterlistForm != null)
            {
                if(cmb_year.EditValue == null || cmb_period.EditValue == null || cmb_type.EditValue == null || cmb_tag.EditValue == null || cmb_payment.EditValue == null)
                {
                    XtraMessageBox.Show("Please fill all the dropboxes before proceeding.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(XtraMessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    InsertIntoPayroll();
                }
            }
            else
            {
                XtraMessageBox.Show("MasterList form is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_period.EditValue == null)
                {
                    MessageBox.Show("Please select a period.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                con.Open();
                var selectedPeriod = (PeriodItem)cmb_period.SelectedItem;
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Period, StipendMultiplier FROM lib_period WHERE Period=@Period";
                cmd.Parameters.AddWithValue("@Period", selectedPeriod.Period);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txt_multiplier.Text = dr["StipendMultiplier"].ToString();
                }
                else
                {
                    MessageBox.Show("No matching period found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_multiplier.Text = string.Empty;
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateAmount()
        {
            try
            {
                decimal monthlyStipend = 0;
                decimal multiplier = 0;

                if (!string.IsNullOrEmpty(txt_monthlystipend.Text))
                {
                    decimal.TryParse(txt_monthlystipend.Text, out monthlyStipend);
                }

                if (!string.IsNullOrEmpty(txt_multiplier.Text))
                {
                    decimal.TryParse(txt_multiplier.Text, out multiplier);
                }

                decimal amount = monthlyStipend * multiplier;
                txt_amount.Text = amount.ToString("0"); // Format as needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while calculating the amount: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_monthlystipend_EditValueChanged(object sender, EventArgs e)
        {
            UpdateAmount();
        }

        private void txt_multiplier_EditValueChanged(object sender, EventArgs e)
        {
            UpdateAmount();
        }
    }
}
