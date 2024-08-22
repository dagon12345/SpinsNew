using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.PrintPreviews;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class Payroll : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        public Payroll(string username)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            newApplicantToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            viewAttachmentsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            _username = username;
        }

        private void Payroll_Load(object sender, EventArgs e)
        {
            Municipality();
            Year();
            Signatories();
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
                ORDER BY 
                ProvinceName,
                m.CityMunName"; // Join with lib_province to get ProvinceName
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
        //Code below is to count the data if the column filter was changed
        /*public Form1()
{
    InitializeComponent();

    // Subscribe to the FilterChanged event
    GridView gridView = (GridView)gridControl1.MainView;
    gridView.FilterChanged += GridView_FilterChanged;
}

private void GridView_FilterChanged(object sender, EventArgs e)
{
    GridView gridView = (GridView)sender;

    // Get the count of rows that match the current filter
    int rowCount = gridView.DataRowCount;

    // Update your control with the row count
    UpdateRowCount(rowCount);
}

private void UpdateRowCount(int rowCount)
{
    // Format the row count with thousands separator
    string formattedRowCount = rowCount.ToString("N0");

    // Assign formatted row count to a label or any other control
    groupControl1.Text = $"Total Count: {formattedRowCount}";
}
*/
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

        public async Task Payrolls() //The query is all about delisted with payroll unclaimed filtered by year and the latest ID inputed into tbl_payroll_socpen
        {

            try
            {
                bool includePayrollStatusID = rbUnclaimed.Checked; // Example of how you might check radio button state
                // Assuming gridView is your GridView instance associated with gridPayroll
                GridView gridView = gridPayroll.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string query = @"WITH LatestPayroll AS(
     SELECT
         tps.ID,
         tps.MasterListID,
         tps.Amount AS LatestAmount,
         lp.Abbreviation AS LatestAbbreviation,
         tps.PayrollStatusID,
         ROW_NUMBER() OVER(PARTITION BY tps.MasterListID ORDER BY tps.ID) AS rn -- ORDER by tbl_socpen_payroll ID Ascending
        
    FROM
        tbl_payroll_socpen tps
    LEFT JOIN
        lib_period lp ON tps.PeriodID = lp.PeriodID
    WHERE

        /*Filter by Year and PeriodID which is not equal, meaning opposite
         ex: Search year 2024 and Period of 1st semester, since 1st semester was selected
        therefore 3q unclaimed is the one that shows instead!*/
        
        tps.Year = @Year
        AND tps.PeriodID != @PeriodID
        
    
),
PayrollData AS(
    SELECT
        m.IsVerified,
        tps.MasterListID,
        m.LastName,
        m.FirstName,
        m.MiddleName,
        m.ExtName,
        lb.BrgyName AS Barangay,
        IF(tps.Address = '' OR tps.Address IS NULL, '', REPLACE(tps.Address, '[^a-zA-Z0-9 ]', '')) AS Address,
        CONCAT(lb.BrgyName,
               IF(tps.Address = '' OR tps.Address IS NULL, '', CONCAT(', ', '[', REPLACE(tps.Address, '[^a-zA-Z0-9 ]', ''), ']'))) AS FullAddress,
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
        tps.EntryBy,
        CONCAT(lcm.CityMunName, ', ', lprov.ProvinceName) AS ProvinceMunicipality,
        CONCAT(lp.Months, ', ', '(', lp.Period, ' ', tps.Year, ')') AS PeriodMonth,
       CONCAT(lp.Months, ' ', tps.Year) AS HeaderPeriodYear,
       lit.Type,
        CASE
            WHEN LatestPayroll.PayrollStatusID = 2 THEN
                CONCAT('(', LatestPayroll.LatestAbbreviation, ' - ', LatestPayroll.LatestAmount, ')')
            ELSE
                ''
        END AS UnclaimedPayroll
    FROM
        tbl_payroll_socpen tps
    LEFT JOIN
        tbl_masterlist m ON tps.MasterListID = m.ID
    LEFT JOIN
        tbl_masterlist tm2 ON tps.ReplacementForID = tm2.ID
    LEFT JOIN
        lib_barangay lb ON tps.PSGCBrgy = lb.PSGCBrgy
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
        lib_province lprov ON tps.PSGCProvince = lprov.PSGCProvince
    LEFT JOIN
        lib_city_municipality lcm ON tps.PSGCCityMun = lcm.PSGCCityMun
    LEFT JOIN
        lib_id_type lit ON m.IDTypeID = lit.ID
    LEFT JOIN
    /* Since Ascending order was implemented
            the LatestPayroll.rn will show the top row with the value of number 1
            .The filter above is opposite instead of filter 1st semester period is selected
            then the 3q is the one that is seleceted depending on the filter applied.*/

        LatestPayroll ON LatestPayroll.MasterListID = tps.MasterListID AND LatestPayroll.rn = 1 AND Year = @Year 

    WHERE
        tps.PSGCCityMun = @PSGCCityMun
        AND tps.Year = @Year
        AND tps.PeriodID = @PeriodID
)
SELECT
    PayrollData.IsVerified as Verified,
    PayrollData.MasterListID,
    IFNULL(tat.AttachmentNames, 'None') AS AttachmentNames,
    IFNULL(tps2.UnclaimedAmounts, '') AS UnclaimedAmounts,
    PayrollData.LastName,
    PayrollData.FirstName,
    PayrollData.MiddleName,
    PayrollData.ExtName,
    CONCAT(
        IFNULL(PayrollData.LastName, ''), ', ',
        IFNULL(PayrollData.FirstName, ''), ' ',
        IFNULL(PayrollData.MiddleName, ''), ' ',
        IFNULL(PayrollData.ExtName, ''), ' ',
        IFNULL(PayrollData.UnclaimedPayroll, '')
    ) AS FullName,
    PayrollData.UnclaimedPayroll,
    PayrollData.Barangay,
    PayrollData.Address,
    PayrollData.FullAddress,
    PayrollData.BirthDate,
    PayrollData.Sex,
    PayrollData.HealthStatus,
    PayrollData.HealthStatusRemarks,
    PayrollData.IDNumber,
    PayrollData.Amounts,
    PayrollData.Year,
    PayrollData.Period,
    PayrollData.StatusPayroll,
    PayrollData.ClaimType,
    PayrollData.DateClaimed,
    PayrollData.PayrollType,
    PayrollData.PayrollTag,
    PayrollData.Status,
    PayrollData.Remarks,
    PayrollData.DateDeceased,
    PayrollData.PaymentMode,
    PayrollData.PayrollRemarks,
    PayrollData.LastName2,
    PayrollData.FirstName2,
    PayrollData.MiddleName2,
    PayrollData.ExtName2,
    PayrollData.DateTimeModified,
    PayrollData.ModifiedBy,
    PayrollData.DateTimeReplaced,
    PayrollData.ReplacedBy,
    PayrollData.DateTimeEntry,
    PayrollData.EntryBy,
    PayrollData.ProvinceMunicipality,
    PayrollData.PeriodMonth,
    PayrollData.HeaderPeriodYear,
    PayrollData.Type
FROM
    PayrollData
LEFT JOIN
    (SELECT MasterListID, GROUP_CONCAT(AttachmentName ORDER BY AttachmentName SEPARATOR ', ') AS AttachmentNames
     FROM tbl_attachments
     GROUP BY MasterListID) tat ON PayrollData.MasterListID = tat.MasterListID
LEFT JOIN
    (SELECT MasterListID, GROUP_CONCAT(CONCAT('(', lp.Abbreviation, ' - ',Amount, ')') ORDER BY Amount SEPARATOR ', ') AS UnclaimedAmounts
     FROM 
        tbl_payroll_socpen tps2
     LEFT JOIN 
        lib_period lp ON tps2.PeriodID = lp.PeriodID
     WHERE 
        PayrollStatusID = 2
        AND Year = @Year
        
     GROUP BY 
        MasterListID) tps2 ON PayrollData.MasterListID = tps2.MasterListID";


                // Add PayrollStatusID condition if not all statuses are included
                if (includePayrollStatusID)
                {
                    query += " AND tps.PayrollStatusID = @PayrollStatusID";
                }
                cmd.CommandText = query;

                var selectedItem = (dynamic)cmb_municipality.SelectedItem;
                int psgccitymun = selectedItem.PSGCCityMun;

                var selectedPeriod = (dynamic)cmb_period.SelectedItem;
                int periodID = selectedPeriod.PeriodID;

                cmd.Parameters.AddWithValue("@PSGCCityMun", psgccitymun);
                cmd.Parameters.AddWithValue("@Year", cmb_year.EditValue);
                cmd.Parameters.AddWithValue("@PeriodID", periodID);

                // Add PayrollStatusID parameter if not including all statuses
                if (includePayrollStatusID)
                {
                    cmd.Parameters.AddWithValue("@PayrollStatusID", lblValue.Text);
                }

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

                // Ensure that the DataTable is accessible in the PayrollPrintPreview form
                //PayrollPrintPreview payrollPrintPreview = new PayrollPrintPreview(this);
                //payrollPrintPreview.SetPayrollData(dt); // Pass the DataTable to the form
               // payrollPrintPreview.Show();

                for (int i = 0; i <= 100; i += 10)
                {
                    await Task.Delay(50); // Simulate a delay
                    this.Invoke(new Action(() => progressBarControl1.EditValue = i));
                }

               // dt.Columns.Add("FullName", typeof(string));
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

                   // row["FullName"] = $"{lastName}, {firstName} {middleName} {extName} {unclaimedAmounts}";
                   // row["FullName"] = $"{lastName}, {firstName} {middleName} {extName}";
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

                dt.Columns["Verified"].SetOrdinal(0);
                dt.Columns["FullName"].SetOrdinal(1);
                dt.Columns["Status Payroll"].SetOrdinal(16);
                dt.Columns["CurrentStatus"].SetOrdinal(21);
                dt.Columns["Replacement Of"].SetOrdinal(27);


                gridPayroll.DataSource = dt;

                if (gridView != null)
                {
                    gridView.BestFitColumns();

                    // Move the "Verified" column to the first position
                    gridView.Columns["Verified"].VisibleIndex = 0;

                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;

                    gridView.Columns["Status"].Visible = false;
                    gridView.Columns["Remarks"].Visible = false;
                    gridView.Columns["DateDeceased"].Visible = false;

                    gridView.Columns["StatusPayroll"].Visible = false;
                    gridView.Columns["ClaimType"].Visible = false;
                    gridView.Columns["UnclaimedAmounts"].Visible = false;
                    gridView.Columns["UnclaimedPayroll"].Visible = false;
                    gridView.Columns["MasterListID"].Visible = false;

                    gridView.Columns["LastName2"].Visible = false;
                    gridView.Columns["FirstName2"].Visible = false;
                    gridView.Columns["MiddleName2"].Visible = false;
                    gridView.Columns["ExtName2"].Visible = false;
                    gridView.Columns["FullAddress"].Visible = false;
                    gridView.Columns["ProvinceMunicipality"].Visible = false;
                    gridView.Columns["PeriodMonth"].Visible = false;


                    // Freeze the columns
                    //gridView.Columns["UnclaimedAmounts"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["Verified"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    gridView.Columns["FullName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                    //gridView.Columns["PayrollStatus"].Visible = false;
                    //gridView.Columns["Abbreviation"].Visible = false;
                    //gridView.Columns["PayrollID"].Visible = false;
                    //gridView.Columns["Amounts"].Visible = false;

                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                }

                gridView.OptionsCustomization.AllowFilter = false;
                // Update row count display
                UpdateRowCount(gridView);
                this.Invoke(new Action(() => progressBarControl1.EditValue = 100));
                DisableSpinner();
                //QueryPayroll();
               
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
            if(cmb_municipality.Text == "Select City/Municipality" || cmb_year.Text == "Select Year" || cmb_period.Text == "Select Period")
            {
                MessageBox.Show("Fill all the fields before searching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EnableSpinner();
            await Payrolls();
        }




        public DataTable Signatories()
        {
            DataTable signatoriesData = new DataTable();

            try
            {
                con.Open();  // Open the connection

                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, Position, Name FROM lib_signatories WHERE ID IN (1, 2, 3, 4, 5)";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(signatoriesData);

                return signatoriesData; // Return the DataTable after filling it with data
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Return null in case of an error
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


        private DataTable GetSignatoriesData()
        {
            return Signatories();
        }

        private void PrintReport(DataTable payrollData, DataTable signatoriesData)
        {
            PayrollPrintPreview payrollprintPreview = new PayrollPrintPreview(this);
            payrollprintPreview.SetPayrollData(payrollData);
            payrollprintPreview.SetSignatoriesData(signatoriesData); // Pass signatories data
            payrollprintPreview.Show();
        }

        private void newApplicantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView gridView = gridPayroll.MainView as GridView;
     
            if (gridView.SelectedRowsCount == 0)
            {
                MessageBox.Show("There Is Nothing To Be Printed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable payrollData = (DataTable)gridPayroll.DataSource; // Ensure this is the correct DataTable
            DataTable signatoriesData = GetSignatoriesData(); // Retrieve the signatories data
            if (payrollData == null || payrollData.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PrintReport(payrollData, signatoriesData);

        }
        private void radioButton()
        {
            // Check if the 'All Status' radio button is checked
            if (rbAllStatus.Checked)
            {
                lblValue.Text = "All";

                // Clear the GridView data source
                GridView gridView = gridPayroll.MainView as GridView;
                if (gridView != null)
                {
                    gridPayroll.DataSource = null;
                    // Update row count display
                    UpdateRowCount(gridView);
                }

                // Return early to avoid further processing
                return;
            }

            // Check if the 'Unclaimed' radio button is checked
            if (rbUnclaimed.Checked)
            {
                lblValue.Text = "2";
                // Clear the GridView data source
                GridView gridView = gridPayroll.MainView as GridView;
                if (gridView != null)
                {
                    gridPayroll.DataSource = null;
                    // Update row count display
                    UpdateRowCount(gridView);
                }

                return;
            }

            // Optionally, you might want to handle the case where no radio button is checked
            // e.g., lblValue.Text = "Default Value";

        }

        private void rbAllStatus_CheckedChangedAsync(object sender, EventArgs e)
        {
            radioButton();// Change value once triggered
        }

        private void cmb_municipality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbClaimed_CheckedChanged(object sender, EventArgs e)
        {
            radioButton();// Change value once triggered.
        }

        private void rbUnclaimed_CheckedChanged(object sender, EventArgs e)
        {
            radioButton();// Change value once triggered
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        Attachments attachmentsForm;
        private MasterList masterlistForm;
        private Payroll payrollForm;

        private void viewAttachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Attachments>().Any())
            {
                gridPayroll.Select();
                gridPayroll.BringToFront();
            }
            else
            {
                GridView gridView = gridPayroll.MainView as GridView;

                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to first", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                int id = Convert.ToInt32(row["MasterlistID"]);

                attachmentsForm = new Attachments(masterlistForm, payrollForm, _username); // Ensure this matches the constructor of Attachments
                attachmentsForm.DisplayID(id);
                attachmentsForm.Show();
            }
        }

    }
}
