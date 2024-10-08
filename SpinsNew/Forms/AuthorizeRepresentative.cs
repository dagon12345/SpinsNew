using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using SpinsNew.Data;
using SpinsNew.Libraries;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using SpinsWinforms.Forms;
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
    public partial class AuthorizeRepresentative : Form
    {

        MasterList masterlistForm;
        public AuthorizeRepresentative(EditApplicant editApplicant)
        {
            InitializeComponent();
        }

        private async void AuthorizeRepresentative_Load(object sender, EventArgs e)
        {
            await LoadDataEF();// Load the table
            await RelationshipEF();//Load the relationship
        }

        public void DisplayReference(int reference)
        {
            // Display the ID in a label or textbox on your form
            txt_reference.Text = reference.ToString(); // Assuming lblID is a label on your form
        }
        public void DisplayID(int id)
        {
            // Display the ID in a label or textbox on your form
            txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
        }

        public async Task LoadDataEF()
        {
            using (var context = new ApplicationDbContext())
            {
                int referenceCode = Convert.ToInt32(txt_reference.Text);
                var representative = await context.tbl_auth_representative
                    .Include(x => x.LibraryRelationships)
                    .Where(x => x.ReferenceCode == referenceCode)
                    .Select(x => new AuthorizeRepViewModel
                    {
                        Id  = x.Id,
                        LastName = x.LastName,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        ExtName = x.ExtName,
                        English = x.LibraryRelationships.Select(l => l.English).FirstOrDefault()
                    })
                    .ToListAsync();

                authorizeRepViewModelBindingSource.DataSource = representative;
                gridControl1.DataSource = authorizeRepViewModelBindingSource;

                if (representative != null)
                {
                    GridView gridView = gridControl1.MainView as GridView;
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();
                    // Ensure horizontal scrollbar is enabled
                   // gridView.OptionsView.ColumnAutoWidth = false;
                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                    // masterlistForm.UpdateRowCount(gridView);
                    UpdateRowCount(gridView);

                }

            }
        }

        public async Task RelationshipEF()
        {
            using(var context = new ApplicationDbContext())
            {
                var relationship = await context.lib_relationship
                    .AsNoTracking()
                    .ToListAsync();

                cmb_relationship.Properties.Items.Clear();
                foreach(var relationships in relationship)
                {
                    cmb_relationship.Properties.Items.Add(new LibraryRelationship
                    {
                        Id = relationships.Id,
                        English = relationships.English
                    });
                }
            }
        }
        private async Task DeleteEF()
        {
            using(var context = new ApplicationDbContext())
            {
                GridView gridView = gridControl1.MainView as GridView;
                int rowHandle = gridView.FocusedRowHandle;
                object idValue = gridView.GetRowCellValue(rowHandle, "Id");
                if(idValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    context.Remove(context.tbl_auth_representative.Single(x => x.Id == id));
                    await context.SaveChangesAsync();

                    Clear();
                    XtraMessageBox.Show("Data successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("No row is selected for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }
        }

        // Method to get the current row count from the GridView
        private int GetRowCount(GridView gridView)
        {
            return gridView.RowCount;
        }

        // Method to update the row count display and check the limit
        public void UpdateRowCount(GridView gridView)
        {
            // Calculate the row count
            int rowCount = gridView.RowCount;

            // Format the row count with thousands separator
            string formattedRowCount = rowCount.ToString("N0");

            // Assign formatted row count to groupControl1
            groupControl1.Text = $"Authorize Representatives Count: {formattedRowCount} (Max 3 only)";

            // Check if the row count exceeds the maximum allowed
            if (rowCount > 3)
            {
                // Display a prompt message
                MessageBox.Show("The maximum number of authorized representatives is 3. You cannot add more entries.", "Maximum Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Optionally, return here if you don't want to proceed further
                return;
            }
        }

        private async Task SaveEF()
        {
            using(var context = new ApplicationDbContext())
            {
                int masterListid = Convert.ToInt32(txt_id.Text);
                int referenceCode = Convert.ToInt32(txt_reference.Text);
                string lastName = txt_lastname.Text;
                string firstName = txt_firstname.Text;
                string middleName = txt_middlename.Text;
                string extName = txt_extname.Text;

                var selectedRelationship = (LibraryRelationship)cmb_relationship.SelectedItem;

                var representative = new TableAuthRepresentative
                {
                    MasterListId = masterListid,
                    ReferenceCode = referenceCode,
                    LastName = lastName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    ExtName = extName,
                    RelationshipId = selectedRelationship.Id,
                    DateTimeEntry = DateTime.Now,
                    ValidationTypeId = 3
                };

                context.tbl_auth_representative.Add(representative);
                await context.SaveChangesAsync();

                Clear();

                XtraMessageBox.Show("Added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }


        private void Clear()
        {
            txt_lastname.Text = "";
            txt_firstname.Text = "";
            txt_middlename.Text = "";
            txt_extname.Text = "";
            cmb_relationship.Text = "";
            txt_lastname.Focus();
        }

        private async void btn_edit_Click(object sender, EventArgs e)
        {

            // Check if adding another row exceeds the limit
            int currentRowCount = GetRowCount(gridView1); // Assuming gridView is the name of your GridView control

            if (currentRowCount >= 3)
            {
                MessageBox.Show("The maximum number of authorized representatives is 3. You cannot add more entries.", "Maximum Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Prevent further execution if the limit is reached
            }
            if (cmb_relationship.Text == "" || txt_lastname.Text == "" || txt_firstname.Text == "")
            {
                XtraMessageBox.Show("Please fill the Lastname, Firstname, and Relationship to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Proceed with saving the representative if the limit is not reached
            await SaveEF();
            await LoadDataEF();
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            //Deleting and refreshing once deleted.
            await DeleteEF();
            await LoadDataEF();
        }
    }
}
