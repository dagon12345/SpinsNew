using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.DataSets;
using SpinsNew.Forms;
using SpinsNew.Reports;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SpinsNew.PrintPreviews
{
    public partial class PayrollPrintPreview : Form
    {

        public PayrollPrintPreview(Payroll payroll)
        {
            InitializeComponent();
            payrollForm = payroll;
        }


        private DataTable payrollData;

        public void SetPayrollData(DataTable dt)
        {
            payrollData = dt;
        }

        Payroll payrollForm;
        private void PayrollPrintPreview_Load(object sender, EventArgs e)
        {
            try
            {
         
                if (payrollData != null && payrollData.Rows.Count > 0)
                {
                    PayrollDataSet ds = new PayrollDataSet(); // Replace with your actual DataSet class
                    ds.Tables[0].Merge(payrollData); // Merge data into DataSet

                    PayrollReporting rpt = new PayrollReporting(); // Replace with your actual Crystal Report class
                    rpt.SetDataSource(ds); // Set the DataSource of the report

                    // Determine the status text based on which radio button is checked
                    string statusText = "Status Payroll"; // Default value

                    if (payrollForm?.rbAllStatus?.Checked ?? false)
                    {
                        statusText = "All Status";
                    }
                    else if (payrollForm?.rbClaimed?.Checked ?? false)
                    {
                        statusText = "Claimed";
                    }
                    else if (payrollForm?.rbUnclaimed?.Checked ?? false)
                    {
                        statusText = "Unclaimed";
                    }

                    // Set the parameter value
                    rpt.SetParameterValue("pStatusText", statusText);


                    crystalReportViewer1.ReportSource = rpt; // Set the ReportSource of the CrystalReportViewer
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    MessageBox.Show("No data available to display in the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
   
        }
    }
}
