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
        private DataTable signatoriesData;

        public void SetPayrollData(DataTable dt)
        {
            payrollData = dt;
        }

        public void SetSignatoriesData(DataTable dt)
        {
            signatoriesData = dt;
        }

        Payroll payrollForm;
        private string GetStatusText()
        {
            if (payrollForm?.rbAllStatus?.Checked ?? false)
                return "";
            if (payrollForm?.rbUnclaimed?.Checked ?? false)
                return "Unpaid";

            return ""; // Default value
        }
        private void PayrollPrintPreview_Load(object sender, EventArgs e)
        {
            try
            {
         
                if (payrollData != null && payrollData.Rows.Count > 0)
                {
                    PayrollDataSet ds = new PayrollDataSet(); // Replace with your actual DataSet class
                    SignatoriesDataSet sd = new SignatoriesDataSet(); // Replace with your actual DataSet class
                    ds.Tables[0].Merge(payrollData); // Merge data into DataSet

                    if (signatoriesData != null && signatoriesData.Rows.Count > 0)
                    {
                        sd.Tables["Signatories"].Merge(signatoriesData); // Merge signatories data into DataSet
                    }
                    else
                    {
                        MessageBox.Show("No signatories data available to display in the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    PayrollReporting rpt = new PayrollReporting(); // Replace with your actual Crystal Report class
                    rpt.SetDataSource(ds); // Set the DataSource of the report
                    foreach (DataRow row in signatoriesData.Rows)
                    {
                        int id = Convert.ToInt32(row["ID"]);
                        string name = row["Name"].ToString();
                        string position = row["Position"].ToString();

                        // Example of setting parameters based on ID
                        if (id == 1) // 1 is Division Chief
                        {
                            rpt.SetParameterValue("Division Chief", name); // Assuming Name1 is a parameter in your report
                            rpt.SetParameterValue("Number1Position", position); 
                        }
                        else if (id == 2) //2 is Certified Sign
                        {
                            rpt.SetParameterValue("Certified Sign", name); // Assuming Name2 is a parameter in your report
                            rpt.SetParameterValue("Number2Position", position);
                        }
                        else if (id == 3) //2 is Certified Sign
                        {
                            rpt.SetParameterValue("Regional Director", name); // Assuming Name2 is a parameter in your report
                            rpt.SetParameterValue("Number3Position", position);
                        }
                    }





                    // Set the parameter value
                    rpt.SetParameterValue("pStatusText", GetStatusText());


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
