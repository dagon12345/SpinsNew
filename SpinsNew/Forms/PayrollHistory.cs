using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        private async Task LoadHistoryEF()
        {
            string idSearched = txt_id.Text;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var payrollHistory = await context.tbl_payroll_socpen
                        .Include(x => x.MasterListModel)
                        .Include(x => x.LibraryPeriod)
                        .Include(x => x.LibraryClaimType)
                        .Include(x => x.LibraryPayrollType)
                        .Include(x => x.ReplacementFor)
                        .Where(x => x.MasterListID == Convert.ToInt32(idSearched))
                        .Select(x => new PayrollViewModel
                        {
                            ID = x.ID,
                            FullName = $"{x.MasterListModel.LastName}, {x.MasterListModel.FirstName} {x.MasterListModel.MiddleName} {x.MasterListModel.ExtName}",
                            MasterListID = x.MasterListID,
                            PSGCCityMun = x.PSGCCityMun,
                            PeriodID = x.PeriodID,
                            Year = x.Year,
                            Period = x.LibraryPeriod.Period,
                            Address = $"{x.LibraryProvince.ProvinceName}, {x.LibraryMunicipality.CityMunName} {x.LibraryBarangay.BrgyName}",
                            Amount = x.Amount,
                            ClaimType = $"{x.LibraryPayrollStatus.PayrollStatus} - {x.LibraryClaimType.ClaimType}",
                            DateClaimedFrom = x.DateClaimedFrom,
                            PaymentMode = $"{x.LibraryPayrollType.PayrollType} - {x.LibraryPaymentMode.PaymentMode}",
                            Remarks = x.Remarks,
                            ReplacementOf = x.ReplacementFor != null ? $"{x.ReplacementFor.LastName}, {x.ReplacementFor.FirstName} {x.ReplacementFor.MiddleName} {x.ReplacementFor.ExtName}" : ""

                        })
                        .OrderByDescending(x => x.ID)
                        .ToListAsync();

                    if (payrollHistory != null)
                    {
                        lbl_address.Text = payrollHistory.First().Address;//Address through label.
                        lbl_fullname.Text = payrollHistory.First().FullName;//Fullname through label
                        payrollViewModelBindingSource.DataSource = payrollHistory;
                        gridControl1.DataSource = payrollViewModelBindingSource;

                        GridView gridView = gridControl1.MainView as GridView;

                        gridView.BestFitColumns();
                        gridView.OptionsView.ColumnAutoWidth = false;
                        gridView.OptionsBehavior.Editable = false;
                    }
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"An error occured: {ex.Message} ");
            }

        }
        protected async override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadHistoryEF();

        }
        private void PayrollHistory_Load(object sender, EventArgs e)
        {
            // LoadPayrollHistory();
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
        private Payroll payrollForm;
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
                payrollFilesForm = new PayrollFiles(payrollFormHistory, payrollForm);
                GridView gridView = gridControl1.MainView as GridView;

                // Check if any row is selected
                if (gridView.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Please select a data to Edit", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without showing EditApplicantForm
                }

                // Pass the ID value to the EditApplicant form
                PayrollViewModel row = (PayrollViewModel)gridView.GetRow(gridView.FocusedRowHandle);

                // int municipality = Convert.ToInt32(row["PSGCCityMun"]);
                // int year = Convert.ToInt32(row["Year"]);
                // int period = Convert.ToInt32(row["PeriodID"]);
                int municipality = row.PSGCCityMun;
                int year = row.Year;
                int period = row.PeriodID;
                payrollFilesForm.DisplayID(municipality, year, period);
                payrollFilesForm.ShowDialog();
            }
        }
    }
}
