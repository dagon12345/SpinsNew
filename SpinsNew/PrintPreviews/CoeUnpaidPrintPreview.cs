using SpinsNew.DataSets;
using SpinsNew.Forms;
using SpinsNew.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.PrintPreviews
{
    public partial class CoeUnpaidPrintPreview : Form
    {
        Payroll payrollForm;
        private DataTable payrollData;
        private DataTable signatoriesData;
        public CoeUnpaidPrintPreview(Payroll payroll)
        {
            InitializeComponent();
            payrollForm = payroll;
        }

        public void SetPayrollData(DataTable dt)
        {
            payrollData = dt;
        }
        public void SetSignatoriesData(DataTable dt)
        {
            signatoriesData = dt;
        }

        private void CoeUnpaidPrintPreview_Load(object sender, EventArgs e)
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

                    CertificateOfEligibilityUnpaid rpt = new CertificateOfEligibilityUnpaid(); // Replace with your actual Crystal Report class
                    rpt.SetDataSource(ds); // Set the DataSource of the report
                    foreach (DataRow row in signatoriesData.Rows)
                    {
                        int id = Convert.ToInt32(row["ID"]);
                        string name = row["Name"].ToString();
                        string position = row["Position"].ToString();

                        // Example of setting parameters based on ID
                        switch (id)
                        {
                            case 3:
                                rpt.SetParameterValue("ApprovedBy", name); // Assuming Name1 is a parameter in your report
                                rpt.SetParameterValue("Number1Position", position);
                                break;
                            case 1:
                                rpt.SetParameterValue("RecommendingApproval", name);
                                rpt.SetParameterValue("Number2Position", position);
                                break;
                            case 5:
                                rpt.SetParameterValue("PreparedBy", name);
                                rpt.SetParameterValue("Number3Position", position);
                                break;
                            default:
                                break;
                        }
                    }

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
