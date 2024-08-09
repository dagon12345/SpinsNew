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

namespace SpinsNew.Forms
{
    public partial class Payroll : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public Payroll()
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
        }

        private void Payroll_Load(object sender, EventArgs e)
        {
            Municipality();
            Year();
            groupControlPayroll.Text = "Count of showed data: [0]";
            // Cast the MainView to GridView
            GridView gridView = gridPayroll.MainView as GridView;

            //if (gridView != null)
            //{
            //    gridView.RowStyle += gridView_RowStyle;
            //    // Subscribe to the CustomDrawFooterCell event
            //    gridView.CustomDrawFooterCell += GridView_CustomDrawFooterCell;
            //}

            //Integrate search control into our grid control.
            searchControl1.Client = gridPayroll;

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

        // For municipality combobox properties
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
        //Show the spinner
        private void EnableSpinner()
        {
            btn_search.Enabled = false;
            // btn_refresh.Enabled = false;
            panel_spinner.Visible = true;


        }
        private void cmb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_year.SelectedItem is YearItem selectedYear)
            {
                // MessageBox.Show($"Selected Year: {selectedYear.Year}");
                LoadPeriodsForYear(selectedYear.Year);
            }
        }
        // Method to update the row count display
        public void UpdateRowCount(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Assign formatted row count to txt_total (or any other control)
            groupControlPayroll.Text = $"Payroll List: {formattedRowCount}";
        }
        public async Task Payrolls() //The query is all about delisted with payroll unclaimed filtered by year and the latest ID inputed into tbl_payroll_socpen
        {

            try
            {
                // Assuming gridView is your GridView instance associated with gridPayroll
                GridView gridView = gridPayroll.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = @"WITH LatestPayroll AS (
                //   SELECT
                //       tps.MasterListID,
                //       tps.Amount AS LatestAmount,
                //       lp.Abbreviation AS LatestAbbreviation,
                //       ROW_NUMBER() OVER(PARTITION BY tps.MasterListID ORDER BY tps.ID DESC) AS rn,
                //       tps.Year,
                //       tps.PayrollStatusID
                //   FROM
                //       tbl_payroll_socpen tps
                //   LEFT JOIN
                //       lib_period lp ON tps.PeriodID = lp.PeriodID
                //   WHERE
                //       tps.Year = @Year
                //)
                //SELECT                
                //     CASE 
                //         WHEN LatestPayroll.PayrollStatusID = 2 THEN
                //             CONCAT('(', LatestPayroll.LatestAbbreviation, ' - ', LatestPayroll.LatestAmount, ')')
                //         ELSE
                //            ''
                //    END AS UnclaimedPayroll,
                //    tps.MasterlistID,
                //    m.LastName,
                //    m.FirstName,
                //    m.MiddleName,
                //    m.ExtName,

                //    lb.BrgyName AS Barangay,
                //    tps.Address,
                //    m.BirthDate,
                //    ls.Sex AS Sex,
                //    lhs.HealthStatus,
                //    m.HealthStatusRemarks,
                //    m.IDNumber,
                //    tps.Amount AS Amounts,
                //    tps.Year,
                //    lp.Period,
                //    lps.PayrollStatus AS StatusPayroll,
                //    lct.ClaimType AS ClaimType,
                //    tps.DateClaimedFrom AS DateClaimed,
                //    lpt.PayrollType AS PayrollType,
                //    lptg.PayrollTag AS PayrollTag,
                //    lstat.Status AS Status,
                //    m.Remarks,
                //    m.DateDeceased,
                //    lpm.PaymentMode,
                //    tps.Remarks AS PayrollRemarks,
                //    tm2.LastName AS LastName2,
                //    tm2.FirstName AS FirstName2,
                //    tm2.MiddleName AS MiddleName2,
                //    tm2.ExtName AS ExtName2,
                //    tps.DateTimeModified,
                //    tps.ModifiedBy,
                //    tps.DateTimeReplaced,
                //    tps.ReplacedBy,
                //    tps.DateTimeEntry,
                //    tps.EntryBy

                //FROM
                //    tbl_payroll_socpen tps
                //LEFT JOIN
                //    tbl_masterlist m ON tps.MasterListID = m.ID
                //LEFT JOIN
                //    tbl_masterlist tm2 ON tps.ReplacementForID = tm2.ID
                //LEFT JOIN
                //    lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                //LEFT JOIN
                //    lib_sex ls ON m.SexID = ls.Id
                //LEFT JOIN
                //    lib_health_status lhs ON m.HealthStatusID = lhs.ID
                //LEFT JOIN
                //    lib_period lp ON tps.PeriodID = lp.PeriodID
                //LEFT JOIN
                //    lib_payroll_status lps ON tps.PayrollStatusID = lps.PayrollStatusID
                //LEFT JOIN
                //    lib_claim_type lct ON tps.ClaimTypeID = lct.ClaimTypeID
                //LEFT JOIN
                //    lib_payroll_type lpt ON tps.PayrollTypeID = lpt.PayrollTypeID
                //LEFT JOIN
                //    lib_payroll_tag lptg ON tps.PayrollTagID = lptg.PayrollTagID
                //LEFT JOIN
                //    lib_status lstat ON m.StatusID = lstat.ID
                //LEFT JOIN
                //    lib_payment_mode lpm ON tps.PaymentModeID = lpm.PaymentModeID
                //LEFT JOIN
                //    LatestPayroll ON LatestPayroll.MasterListID = tps.MasterListID AND LatestPayroll.rn = 2
                //WHERE
                //    tps.PSGCCityMun = @PSGCCityMun
                //    AND tps.Year = @Year
                //    AND tps.PeriodID = @PeriodID";
                cmd.CommandText = @"
                SELECT
                    IFNULL(tps2.UnclaimedAmounts, '') AS UnclaimedAmounts,
                    m.LastName,
                    m.FirstName,
                    m.MiddleName,
                    m.ExtName,

                    lb.BrgyName AS Barangay,
                    tps.Address,
                    m.BirthDate,
                    ls.Sex AS Sex,
                    lhs.HealthStatus,
                    m.HealthStatusRemarks,
                    m.IDNumber,
                    tps.Amount AS Amounts,
                    tps.Year,
                    lp.Period,
                    lps.PayrollStatus AS StatusPayroll,
                    lct.ClaimType AS ClaimType,
                    tps.DateClaimedFrom AS DateClaimed,
                    lpt.PayrollType AS PayrollType,
                    lptg.PayrollTag AS PayrollTag,
                    lstat.Status AS Status,
                    m.Remarks,
                    m.DateDeceased,
                    lpm.PaymentMode,
                    tps.Remarks AS PayrollRemarks,
                    tm2.LastName AS LastName2,
                    tm2.FirstName AS FirstName2,
                    tm2.MiddleName AS MiddleName2,
                    tm2.ExtName AS ExtName2,
                    tps.DateTimeModified,
                    tps.ModifiedBy,
                    tps.DateTimeReplaced,
                    tps.ReplacedBy,
                    tps.DateTimeEntry,
                    tps.EntryBy

                FROM
                    tbl_payroll_socpen tps
                LEFT JOIN
                    tbl_masterlist m ON tps.MasterListID = m.ID
                LEFT JOIN
                    tbl_masterlist tm2 ON tps.ReplacementForID = tm2.ID
                LEFT JOIN
                    lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                LEFT JOIN
                    lib_sex ls ON m.SexID = ls.Id
                LEFT JOIN
                    lib_health_status lhs ON m.HealthStatusID = lhs.ID
                LEFT JOIN
                    lib_period lp ON tps.PeriodID = lp.PeriodID
                LEFT JOIN
                    lib_payroll_status lps ON tps.PayrollStatusID = lps.PayrollStatusID
                LEFT JOIN
                    lib_claim_type lct ON tps.ClaimTypeID = lct.ClaimTypeID
                LEFT JOIN
                    lib_payroll_type lpt ON tps.PayrollTypeID = lpt.PayrollTypeID
                LEFT JOIN
                    lib_payroll_tag lptg ON tps.PayrollTagID = lptg.PayrollTagID
                LEFT JOIN
                    lib_status lstat ON m.StatusID = lstat.ID
                LEFT JOIN
                    lib_payment_mode lpm ON tps.PaymentModeID = lpm.PaymentModeID

                LEFT JOIN
                    (SELECT
                        MasterListID,
                        GROUP_CONCAT(CONCAT('(', lp.Abbreviation, ' - ',Amount, ')') ORDER BY Amount SEPARATOR ', ') AS UnclaimedAmounts 
                    FROM 
                        tbl_payroll_socpen tps2
                    LEFT JOIN 
                        lib_period lp ON tps2.PeriodID = lp.PeriodID
                    WHERE
                        PayrollStatusID = 2 AND Year = @Year
                    GROUP BY
                        MasterListID) tps2
                ON
                    tps.MasterListID = tps2.MasterListID

                WHERE
                    tps.PSGCCityMun = @PSGCCityMun
                    AND tps.Year = @Year
                    AND tps.PeriodID = @PeriodID";
                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                cmd.Parameters.AddWithValue("@PSGCCityMun", psgccitymun);
                cmd.Parameters.AddWithValue("@Year", cmb_year.EditValue);
                cmd.Parameters.AddWithValue("@PeriodID", periodID);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                progressBarControl1.Properties.Maximum = 100;
                progressBarControl1.Properties.Minimum = 0;
                progressBarControl1.EditValue = 0;

                // Execute query and fill data
                await Task.Run(() =>
                {
                    da.Fill(dt);
                });

                for (int i = 0; i <= 100; i += 10)
                {
                    await Task.Delay(50); // Simulate a delay
                    this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                }

                dt.Columns.Add("FullName", typeof(string));
                dt.Columns.Add("Status Payroll", typeof(string));
                dt.Columns.Add("CurrentStatus", typeof(string));
                dt.Columns.Add("Replacement Of", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string statusPayroll = row["StatusPayroll"].ToString(); //Payroll Status
                    string claimType = row["ClaimType"].ToString();

                    //string payrollStatus = row["PayrollStatus"].ToString();
                    //string amountforUnclaimed = row["Amount"].ToString();
                    //string abbreviation = row["Abbreviation"].ToString();
                    //string unclaimedPayroll = row["UnclaimedPayroll"].ToString();
                    string unclaimedAmounts = row["UnclaimedAmounts"].ToString();

                    string status = row["Status"].ToString();
                    string dateDeceased = row["DateDeceased"]?.ToString();
                    string remarks = row["Remarks"]?.ToString();

                    string lastName2 = row["LastName2"].ToString();
                    string firstName2 = row["FirstName2"].ToString();
                    string middleName2 = row["MiddleName2"].ToString();
                    string extName2 = row["ExtName2"].ToString();

                    List<string> nameParts = new List<string>();
                    if (!string.IsNullOrEmpty(lastName2)) nameParts.Add(lastName2);
                    if (!string.IsNullOrEmpty(firstName2)) nameParts.Add(firstName2);
                    if (!string.IsNullOrEmpty(middleName2)) nameParts.Add(middleName2);
                    if (!string.IsNullOrEmpty(extName2)) nameParts.Add(extName2);

                    //string unclaimedPayrolls = string.Empty;

                    //if (!string.IsNullOrEmpty(abbreviation))
                    //{
                    //    unclaimedPayrolls = abbreviation;

                    //    if (!string.IsNullOrEmpty(amountforUnclaimed))
                    //    {
                    //        unclaimedPayrolls += $" - {amountforUnclaimed}";
                    //    }

                    //    unclaimedPayrolls = $"({unclaimedPayrolls})";
                    //}

                    string lastName = row["LastName"].ToString();
                    string firstName = row["FirstName"].ToString();
                    string middleName = row["MiddleName"].ToString();
                    string extName = row["ExtName"].ToString();

                    row["FullName"] = $"{lastName}, {firstName} {middleName} {extName} {unclaimedAmounts}";
                    //row["FullName"] = $"{lastName}, {firstName} {middleName} {extName}";
                    if (!string.IsNullOrEmpty(dateDeceased))
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["CurrentStatus"] = $"{status} ({remarks}) [{dateDeceased}]";
                        }
                        else
                        {
                            row["CurrentStatus"] = $"{status} [{dateDeceased}]";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(remarks))
                        {
                            row["CurrentStatus"] = $"{status} ({remarks})";
                        }
                        else
                        {
                            row["CurrentStatus"] = status;
                        }
                    }

                    row["Status Payroll"] = $"{statusPayroll} - {claimType}";
                    row["Replacement Of"] = string.Join(", ", nameParts);
                }

                dt.Columns["FullName"].SetOrdinal(1);
                dt.Columns["Status Payroll"].SetOrdinal(16);
                dt.Columns["CurrentStatus"].SetOrdinal(21);
                dt.Columns["Replacement Of"].SetOrdinal(27);

                gridPayroll.DataSource = dt;

                if (gridView != null)
                {
                    gridView.BestFitColumns();

                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;

                    gridView.Columns["Status"].Visible = false;
                    gridView.Columns["Remarks"].Visible = false;
                    gridView.Columns["DateDeceased"].Visible = false;

                    gridView.Columns["StatusPayroll"].Visible = false;
                    gridView.Columns["ClaimType"].Visible = false;
                    //gridView.Columns["Amount"].Visible = false;

                    gridView.Columns["LastName2"].Visible = false;
                    gridView.Columns["FirstName2"].Visible = false;
                    gridView.Columns["MiddleName2"].Visible = false;
                    gridView.Columns["ExtName2"].Visible = false;


                    // Freeze the columns
                    gridView.Columns["UnclaimedAmounts"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                    //gridView.Columns["PayrollStatus"].Visible = false;
                    //gridView.Columns["Abbreviation"].Visible = false;
                    //gridView.Columns["PayrollID"].Visible = false;
                    //gridView.Columns["Amounts"].Visible = false;

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                }
                // Update row count display
                UpdateRowCount(gridView);
                this.Invoke(new Action(() => progressBarControl1.EditValue = 100));
                DisableSpinner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }



        }
        //Hide the spinner
        private void DisableSpinner()
        {
            progressBarControl1.EditValue = 0; // Ensure the progress bar is full
            btn_search.Enabled = true; //Enable textbox once gridview was loaded successfully
                                       // btn_refresh.Enabled = true;
            panel_spinner.Visible = false; // Hide spinner when data was retrieved.
        }
        private async void btn_search_ClickAsync(object sender, EventArgs e)
        {
            EnableSpinner();
            await Payrolls();
        }
    }
}
