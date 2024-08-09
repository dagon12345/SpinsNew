using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
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
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        MasterList masterlistForm;
        public AuthorizeRepresentative(EditApplicant editApplicant)
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
        }

        private void AuthorizeRepresentative_Load(object sender, EventArgs e)
        {
           LoadDataAsync();// Load the table
            Relationship();//Load the relationship
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
        // Custom class to store Id and English
        public class RelationshipItem
        {
            public int Id { get; set; }
            public string English { get; set; }

            public override string ToString()
            {
                return English; // Display DataSource in the ComboBox
            }
        }
        //Fill combobox relationship
        public void Relationship()
        {
            try
            {
                // Fetch data from the database and bind to ComboBox
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, English FROM lib_relationship"; // Specify the columns to retrieve
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                // Clear existing items in the ComboBoxEdit
                cmb_relationship.Properties.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    // Add DataSourceItem to the ComboBox
                    cmb_relationship.Properties.Items.Add(new RelationshipItem
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        English = dr["English"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void DeleteRepresentative()
        {
            //DELETE USERS FROM DATABASE.

            try
            {
                GridView gridView = gridControl1.MainView as GridView;
                int rowHandle = gridView.FocusedRowHandle; // Get the handle of the focused row
                object idValue = gridView.GetRowCellValue(rowHandle, "ID"); // Replace "ID" with the name of your ID column
                if (idValue != null)
                {
                    int id = Convert.ToInt32(idValue);
                    con.Open();
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM tbl_auth_representative WHERE id=" + id + "";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();

                    XtraMessageBox.Show("Data successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /*Clearing the form*/

                    con.Close();
                    LoadDataAsync();
                }
                else
                {
                    XtraMessageBox.Show("No row is selected for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
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

        private void SaveRepresentative()
        {
          
            try
            {
                con.Open();

                // Insert GIS information into database
                MySqlCommand insertCmd = con.CreateCommand();
                insertCmd.CommandType = CommandType.Text;
                insertCmd.CommandText = "INSERT INTO tbl_auth_representative (MasterListID, ReferenceCode, LastName, FirstName, MiddleName, ExtName, RelationshipID, DateTimeEntry, ValidationTypeID)" +
                    " VALUES (@MasterlistID, @ReferenceCode, @LastName, @FirstName, @MiddleName, @ExtName, @RelationshipID, @DateTimeEntry, @ValidationTypeID)";

                // Retrieve the selected item and get the PSGCCityMun Code.
                var selectedItem = (dynamic)cmb_relationship.SelectedItem;
                int relationshipId = selectedItem.Id;
                insertCmd.Parameters.AddWithValue("@MasterlistID", txt_id.EditValue);
                insertCmd.Parameters.AddWithValue("@ReferenceCode", txt_reference.EditValue);
                insertCmd.Parameters.AddWithValue("@LastName", txt_lastname.EditValue);
                insertCmd.Parameters.AddWithValue("@FirstName", txt_firstname.EditValue);
                insertCmd.Parameters.AddWithValue("@MiddleName", txt_middlename.EditValue);
                insertCmd.Parameters.AddWithValue("@ExtName", txt_extname.EditValue);
                insertCmd.Parameters.AddWithValue("@RelationshipID", relationshipId);
                insertCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);
                insertCmd.Parameters.AddWithValue("@ValidationTypeID", 0);
                insertCmd.ExecuteNonQuery();
                //this.Close();
                con.Close();
                LoadDataAsync();
                Clear();

                XtraMessageBox.Show("Added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDataAsync()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                // Modified SQL query with JOIN to get the relationship name
                cmd.CommandText = @"
        SELECT
            r.ID,
            r.LastName, 
            r.FirstName, 
            r.MiddleName, 
            r.ExtName, 
            lr.English AS RelationshipName
        FROM 
            tbl_auth_representative r
        INNER JOIN 
            lib_relationship lr ON r.RelationshipID = lr.Id
        WHERE 
            r.ReferenceCode = @ReferenceCode";

                // Use parameterized query to prevent SQL injection
                cmd.Parameters.AddWithValue("@ReferenceCode", txt_reference.Text);

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                gridControl1.DataSource = dt;
                //await Task.Run(() => da.Fill(dt));
                GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    gridView.Columns["ID"].Visible = false;
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();
                    // Hide the "ID" column
                    // Ensure horizontal scrollbar is enabled
                    gridView.OptionsView.ColumnAutoWidth = false;
                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

                     con.Close();
                }
                // masterlistForm.UpdateRowCount(gridView);
                UpdateRowCount(gridView);
               // con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
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

        private void btn_edit_Click(object sender, EventArgs e)
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
            SaveRepresentative();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DeleteRepresentative();
        }
    }
}
