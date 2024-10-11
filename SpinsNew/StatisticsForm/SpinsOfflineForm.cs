using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using SpinsNew.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.StatisticsForm
{
    public partial class SpinsOfflineForm : Form
    {
        private ITableGIS _tableGis;
        public SpinsOfflineForm(ITableGIS tableGIS)
        {
            InitializeComponent();
            _tableGis = tableGIS;
        }

        private void SpinsOfflineForm_Load(object sender, EventArgs e)
        {
            //Integrate search control into our grid control.
            txtSearch.Client = gridControl1;
        }

        private async Task SearchDates()
        {
            /*TODO: If clicked the search the footer will return double click
           and need to implement the exporting to excel...*/
            DateTime startDate = Convert.ToDateTime(dtFrom.EditValue);
            DateTime endDate = Convert.ToDateTime(dtTo.EditValue);
            var displayGis = await Task.Run(() => _tableGis.DisplayGisAsync(startDate, endDate));

            gisViewModelBindingSource.DataSource = displayGis;
            gridControl1.DataSource = gisViewModelBindingSource;
            GridView gridView = gridControl1.MainView as GridView;
            gridView.BestFitColumns();
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsBehavior.Editable = false;
            //Cleare previous summary items
            gridView.Columns["FullName"].Summary.Clear();
            // Ensure that the footer panel is visible
            gridView1.OptionsView.ShowFooter = true;
            gridView1.Columns["FullName"].Summary.AddRange(new GridSummaryItem[]
            {
               new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FullName", "{0:n0}")
            });
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtFrom.Text == "" || dtTo.Text == "")
            {
                XtraMessageBox.Show("Please fill the dates before searching.", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnSearch.Enabled = false;
            btnSearch.Text = "Please wait...";
            await SearchDates();
            btnSearch.Enabled = true;
            btnSearch.Text = "Search";

        }

        private void ExportToExcel(string filePath)
        {
            try
            {

                var exportOptions = new XlsxExportOptions
                {
                    ExportMode = XlsxExportMode.SingleFile,
                    // IncludeSummary = true, // This may not exist; verify in documentation
                    // Set other options as needed
                };

                // Perform the export from your grid control
                gridControl1.ExportToXlsx(filePath, exportOptions);

                // Notify the user that the export was successful
                XtraMessageBox.Show("Data exported successfully to " + filePath, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                XtraMessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView.RowCount <= 0)
            {
                XtraMessageBox.Show("There is nothing to export", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = $"Exported {Convert.ToDateTime(dtFrom.EditValue).ToString("MMM-dd-yyyy")} to {Convert.ToDateTime(dtTo.EditValue).ToString("MMM-dd-yyyy")}.xlsx";
            // Show the dialog and check if the user clicked Save
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Call the export function
                ExportToExcel(saveFileDialog.FileName);
            }
        }
    }
}
