using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class PayrollHistory : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public PayrollHistory(MasterList masterlist)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            // Set KeyPreview to true so the form can handle key events first
            this.KeyPreview = true;

            // Handle the KeyDown event
            this.KeyDown += PayrollHistory_KeyDown;
        }

        private void PayrollHistory_Load(object sender, EventArgs e)
        {
             LoadPayrollHistory();
        }
        //Display payroll history from tbl_payroll.
        public void LoadPayrollHistory()
        {
            try
            {


                con.Open();
                MySqlCommand cmd0 = con.CreateCommand();
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = @"SELECT 
                        s.ID,
                        s.MasterListID,
                        s.Year,
                        lp.Period,
                        lp.PeriodID,
                        lpr.ProvinceName as ProvinceName,
                        lcm.CityMunName as Municipality,
                        lcm.PSGCCityMun,
                        lb.BrgyName as Barangay,
                        s.Amount,
                        lps.PayrollStatus,
                        lct.ClaimType as ClaimType,
                        s.DateClaimedFrom,
                        lpt.PayrollType as PayrollType,
                        lpm.PaymentMode as PaymentMode,
                        s.Remarks,
                        tm.LastName,
                        tm.FirstName,
                        tm.MiddleName,
                        tm.ExtName,
                        tmn.LastName as LastNameReplacement,
                        tmn.FirstName as FirstNameReplacementFor,
                        tmn.MiddleName as MiddleNameReplacementFor,
                        tmn.ExtName as ExtNameReplacementFor
                    FROM
                        tbl_payroll_socpen s
                    LEFT JOIN
                        lib_period lp ON s.PeriodID = lp.PeriodID
                    LEFT JOIN
                        lib_province lpr ON s.PSGCProvince = lpr.PSGCProvince
                    LEFT JOIN
                        lib_city_municipality lcm ON s.PSGCCityMun = lcm.PSGCCityMun
                    LEFT JOIN
                        lib_barangay lb ON s.PSGCBrgy = lb.PSGCBrgy
                    LEFT JOIN
                        lib_payroll_status lps ON s.PayrollStatusID = lps.PayrollStatusID
                    LEFT JOIN
                        lib_claim_type lct ON s.ClaimTypeID = lct.ClaimTypeID
                    LEFT JOIN
                        lib_payroll_type lpt ON s.PayrollTypeID = lpt.PayrollTypeID
                    LEFT JOIN
                        lib_payment_mode lpm ON s.PaymentModeID = lpm.PaymentModeID
                    LEFT JOIN
                        tbl_masterlist tm ON s.MasterlistID = tm.ID
                    LEFT JOIN
                        tbl_masterlist tmn ON s.ReplacementForID = tmn.ID
                    WHERE 
                        s.MasterlistID = @MasterlistID
                    ORDER BY
                        s.ID DESC";

                cmd0.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

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
                    lbl_address.Text = $"{row["ProvinceName"]}, {row["Municipality"]}, {row["Barangay"]}";
                }
                // Get the GridView instance
                // Add a new column for concatenated Status and DateDeceased
                dt0.Columns.Add("Address", typeof(string));
                dt0.Columns.Add("Payroll Status", typeof(string));
                dt0.Columns.Add("Mode", typeof(string));
                dt0.Columns.Add("ReplacementOf", typeof(string));

                // Populate the new column with concatenated values
                foreach (DataRow row in dt0.Rows)
                {
                    string provinceName = row["ProvinceName"].ToString();
                    string municipality = row["Municipality"]?.ToString();
                    string barangay = row["Barangay"]?.ToString();
                    string payrollstatus = row["PayrollStatus"]?.ToString();
                    string claimtype = row["ClaimType"]?.ToString();
                    string payrollType = row["PayrollType"]?.ToString();
                    string paymentMode = row["PaymentMode"]?.ToString();
                    string lastnameReplacement = row["LastNameReplacement"]?.ToString();
                    string firstnameReplacement = row["FirstNameReplacementFor"]?.ToString();
                    string middlenameReplacement = row["MiddleNameReplacementFor"]?.ToString();
                    string extnameReplacement = row["ExtNameReplacementFor"]?.ToString();
                    //row["StatusCurrent"] = !string.IsNullOrEmpty(dateDeceased) ? $"{status} ({remarks}) [{dateDeceased}]" : status;

                    row["Address"] = $"{provinceName}, {municipality}, {barangay}";
                    row["Payroll Status"] = $"{payrollstatus} - {claimtype}";
                    row["Mode"] = $"{payrollType} - {paymentMode}";

                    // Build the ReplacementOf string conditionally
                    string replacementOf = $"{lastnameReplacement}";
                    if (!string.IsNullOrWhiteSpace(firstnameReplacement))
                    {
                        replacementOf += $", {firstnameReplacement}";
                    }
                    if (!string.IsNullOrWhiteSpace(middlenameReplacement))
                    {
                        replacementOf += $" {middlenameReplacement}";
                    }
                    if (!string.IsNullOrWhiteSpace(extnameReplacement))
                    {
                        replacementOf += $" {extnameReplacement}";
                    }
                    row["ReplacementOf"] = replacementOf.Trim();


                }

                // Move the new column to the 6th position
                dt0.Columns["Address"].SetOrdinal(3);
                dt0.Columns["Payroll Status"].SetOrdinal(4);
                dt0.Columns["Mode"].SetOrdinal(6);
                dt0.Columns["ReplacementOf"].SetOrdinal(16);

                //We are using DevExpress datagridview
                gridControl1.DataSource = dt0;

                GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();
                    // Hide the "ID" column
                    gridView.Columns["ID"].Visible = false;
                    gridView.Columns["ProvinceName"].Visible = false;
                    gridView.Columns["Municipality"].Visible = false;
                    gridView.Columns["Barangay"].Visible = false;
                    gridView.Columns["PayrollStatus"].Visible = false;
                    gridView.Columns["ClaimType"].Visible = false;
                    gridView.Columns["FirstName"].Visible = false;
                    gridView.Columns["LastName"].Visible = false;
                    gridView.Columns["MiddleName"].Visible = false;
                    gridView.Columns["ExtName"].Visible = false;
                    gridView.Columns["PayrollType"].Visible = false;
                    gridView.Columns["PaymentMode"].Visible = false;
                    gridView.Columns["LastNameReplacement"].Visible = false;
                    gridView.Columns["FirstNameReplacementFor"].Visible = false;
                    gridView.Columns["MiddleNameReplacementFor"].Visible = false;
                    gridView.Columns["ExtNameReplacementFor"].Visible = false;
                    //gridView.Columns["Type"].Visible = false;
                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                   // con.Close();
                }

               

                // Query to get the total sum of the Amount column
                MySqlCommand cmdSum = con.CreateCommand();
                cmdSum.CommandType = CommandType.Text;
                cmdSum.CommandText = @"SELECT SUM(s.Amount) FROM tbl_payroll_socpen s WHERE s.MasterlistID = @MasterlistID";
                cmdSum.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

                // Execute the query and retrieve the sum
                object result = cmdSum.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    // Update the label with the total amount formatted as Philippine Peso
                    CultureInfo phCulture = new CultureInfo("en-PH");
                    groupControl1.Text = $"Payroll History Total Amount: [{totalAmount.ToString("C", phCulture)}]";
                }
                else
                {
                    groupControl1.Text = "Payroll History Total Amount: [₱0.00]";
                }
                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }

        public void DisplayID(int id)
        {
            // Display the ID in a label or textbox on your form
            txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PayrollHistory_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl+W or Alt+F4 is pressed
            if ((e.Control && e.KeyCode == Keys.W) || (e.Alt && e.KeyCode == Keys.F4))
            {
                // Close the form
                this.Close();
            }
        }
        PayrollFiles payrollFilesForm;
        private PayrollHistory payrollFormHistory;
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<PayrollFiles>().Any())
            {
                payrollFilesForm.Select();
                payrollFilesForm.BringToFront();
            }
            else
            {
                // Create a new instance of EditApplicant form and pass the reference of Masterlist form
                payrollFilesForm = new PayrollFiles(payrollFormHistory);
                GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Please select a data to Edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Pass the ID value to the EditApplicant form
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
               // int id = Convert.ToInt32(row["MasterListID"]);
                int municipality = Convert.ToInt32(row["PSGCCityMun"]);
                int year = Convert.ToInt32(row["Year"]);
                int period = Convert.ToInt32(row["PeriodID"]);
                payrollFilesForm.DisplayID(municipality, year, period);
                payrollFilesForm.Show();




                //Below is to get the reference code under masterlist
                // Create a new instance of GISForm
                // GISviewingForm = new GISForm(this);
                // GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                //if (gridView.SelectedRowsCount == 0)
                //{
                //    MessageBox.Show("Please select a data to view", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return; // Exit the method without showing GISForm
                //}

                //// Pass the ID value to the GISForm
                ////DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                //string gis = row["GIS"].ToString();
                //string spbuf = row["SPBUF"].ToString();

                //if (string.IsNullOrWhiteSpace(gis))
                //{
                //    if (string.IsNullOrWhiteSpace(spbuf))
                //    {
                //        //MessageBox.Show("Both GIS and SPBUF are missing.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        // return; // Exit the method without showing GISForm
                //    }
                //    else
                //    {
                //        int spbufId = Convert.ToInt32(spbuf);
                //        EditApplicantForm.DisplaySPBUF(spbufId);
                //    }
                //}
                //else
                //{
                //    int gisId = Convert.ToInt32(gis);
                //    EditApplicantForm.DisplayGIS(gisId);
                //}

                //EditApplicantForm.Show();

            }
        }
    }
}
