using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class Attachments : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public string _username;
        // Class-level variable to store the binary file data
        private byte[] fileData;
        private string selectedPdfPath; // Store selected PDF path for later use
                                        // private string selectedFilePath; // Variable to store the selected file path
        private Payroll payroll;

        public Attachments(MasterList masterList, Payroll payroll, string username)
        {
            InitializeComponent();
            masterlistForm = masterList;
            payrollForm = payroll;
            con = new MySqlConnection(cs.dbcon);

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(btn_save_KeyDown);
            this.KeyDown += Attachments_KeyDown;
            _username = username;
        }

        private MasterList masterlistForm;// Call MasterList form
        private Payroll payrollForm; // Assuming you may use this later or remove if unnecessary
        private void Attachments_Load(object sender, EventArgs e)
        {
            //GenerateReferenceCode();
            LoadDetails(); // Load details into the details tab above.
            LoadImage(); // Load the image once form is initiated
            LoadLogs();
            ClickedData();

        }

        public void LoadLogs()
        {
            con.Open();
            MySqlCommand cmd0 = con.CreateCommand();
            cmd0.CommandType = CommandType.Text;
            cmd0.CommandText = @"SELECT 
                        m.ID,
                        lm.Log,
                        lt.Type as Type,
                        lm.User,
                        lm.DateTimeEntry
                    FROM
                        tbl_masterlist m
                    LEFT JOIN
                        log_masterlist lm ON m.ID = lm.MasterlistID
                    LEFT JOIN 
                        lib_log_type lt ON lm.LogType = lt.ID
                    WHERE 
                        m.ID = @MasterlistID
                        AND lm.LogType = 9
                    ORDER BY
                        lm.DateTimeEntry DESC";

            cmd0.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

            DataTable dt0 = new DataTable();
            MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0);
            //Await to reduce lag while loading large amount of datas
            //await Task.Run(() => da0.Fill(dt0));
            da0.Fill(dt0);
            //We are using DevExpress datagridview
            gv_logs.DataSource = dt0;
            // Get the GridView instance
            GridView gridView = gv_logs.MainView as GridView;
            if (gridView != null)
            {
                // Auto-size all columns based on their content
                gridView.BestFitColumns();
                // Hide the "ID" column
                gridView.Columns["ID"].Visible = false;
                gridView.Columns["Type"].Visible = false;
                // Ensure horizontal scrollbar is enabled
                gridView.OptionsView.ColumnAutoWidth = false;
                // Disable editing
                gridView.OptionsBehavior.Editable = false;

                con.Close();
            }

        }


        private void LoadAndDisplayPdf(string pdfPath)
        {
            // Assuming pdfViewer1 is your PDF viewer control
            if (File.Exists(pdfPath))
            {
                pdfViewer1.LoadDocument(pdfPath);
            }
            else
            {
                XtraMessageBox.Show("PDF file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClickedData()
        {
            try
            {
                GridView gridView = gridControl1.MainView as GridView;
                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);

                if (row == null)
                {
                    XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Retrieve the MasterListID and AttachmentName
                string masterListID = txt_id.Text; // Assuming txt_id contains the MasterListID
                string attachmentName = row["AttachmentName"].ToString();

                // Combine MasterListID with AttachmentName to form the filename
                // Assuming the AttachmentName does not include the extension
                string combinedName = $"{masterListID} {attachmentName}.pdf";

                // Build the path to the PDF file
                string pdfPath = $@"\\172.26.153.181\AttachmentsSupportingDocuments\{combinedName}";

                // Display the PDF in the PDF viewer
                LoadAndDisplayPdf(pdfPath);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }





        public void LoadImage()
        {

            con.Open();
            MySqlCommand cmd0 = con.CreateCommand();
            cmd0.CommandType = CommandType.Text;
            cmd0.CommandText = @"SELECT 
                        ID,
                        AttachmentName,
                        MasterListID
                    FROM
                        tbl_attachments
                    WHERE 
                        MasterListID = @MasterlistID";

            cmd0.Parameters.AddWithValue("@MasterlistID", txt_id.Text);

            DataTable dt0 = new DataTable();
            MySqlDataAdapter da0 = new MySqlDataAdapter(cmd0);
            //Await to reduce lag while loading large amount of datas
            //await Task.Run(() => da0.Fill(dt0));
            da0.Fill(dt0);
            //We are using DevExpress datagridview
            gridControl1.DataSource = dt0;
            // Get the GridView instance
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView != null)
            {
                // Auto-size all columns based on their content
                gridView.BestFitColumns();
                // Hide the "ID" column
                gridView.Columns["ID"].Visible = false;
                // Ensure horizontal scrollbar is enabled
                gridView.OptionsView.ColumnAutoWidth = false;
                // Disable editing
                gridView.OptionsBehavior.Editable = false;

                con.Close();
            }




        }
        public void DisplayID(int id)
        {
            // Display the ID in a label or textbox on your form
            txt_id.Text = id.ToString(); // Assuming lblID is a label on your form
        }
        public void LoadDetails()
        {
            try
            {
                con.Open();
                MySqlCommand cmd0 = con.CreateCommand();
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = @"SELECT 
                        m.LastName,
                        m.FirstName,
                        m.MiddleName,
                        m.ExtName,
                        m.PSGCRegion,
                        m.PSGCProvince,
                        m.PSGCCityMun,
                        m.PSGCBrgy,
                        m.StatusID,
                        lr.Region as Region,
                        lp.ProvinceName as ProvinceName,
                        lcm.CityMunName as Municipality,
                        lb.BrgyName as Barangay
                    FROM
                        tbl_masterlist m
                    LEFT JOIN
                        lib_region lr ON m.PSGCRegion = lr.PSGCRegion
                    LEFT JOIN
                        lib_province lp ON m.PSGCProvince = lp.PSGCProvince
                    LEFT JOIN
                        lib_city_municipality lcm ON m.PSGCCityMun = lcm.PSGCCityMun
                    LEFT JOIN
                        lib_barangay lb ON m.PSGCBrgy = lb.PSGCBrgy
                    wHERE
                        m.ID = @ID";

                cmd0.Parameters.AddWithValue("@ID", Convert.ToInt32(txt_id.Text));

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
                    lbl_address.Text = $"{row["Region"]}, {row["ProvinceName"]}, {row["Municipality"]}, {row["Barangay"]}";

                    //lbl_region.Text = row["PSGCRegion"].ToString();
                    //lbl_province.Text = row["PSGCprovince"].ToString();
                    //lbl_municipality.Text = row["PSGCCityMun"].ToString();
                    //lbl_barangay.Text = row["PSGCBrgy"].ToString();
                    //lbl_currentstatus.Text = row["StatusID"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

        }
        private void EnableProperties()
        {
            /*Clearing the form*/
            txt_attachmentname.Enabled = true;
            txt_attachmentname.EditValue = "";
            btn_save.Enabled = true;
            txt_attachmentname.Focus();
            //pictureEdit1.Image = null;
        }
        private void DeleteAttachment()
        {
            if (MessageBox.Show("Are you sure you want to delete this data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pdfViewer1.CloseDocument();
                try
                {
                    GridView gridView = gridControl1.MainView as GridView;
                    DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);

                    if (row == null)
                    {
                        XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int id = Convert.ToInt32(row["ID"]);

                    // Retrieve the file path for the attachment
                    string attachmentName = row["AttachmentName"].ToString();
                    string masterListID = txt_id.Text; // Assuming txt_id contains the MasterListID
                    string combinedName = $"{masterListID} {attachmentName}.pdf";
                    string pdfPath = $@"\\172.26.153.181\AttachmentsSupportingDocuments\{combinedName}";

                    // Delete the file from the folder
                    if (File.Exists(pdfPath))
                    {
                        File.Delete(pdfPath);
                    }
                    else
                    {
                        XtraMessageBox.Show("PDF file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Delete the record from the database
                
                        con.Open();
                        MySqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = "DELETE FROM tbl_attachments WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    

                    // Refresh the UI
                    DeleteLogs();
                    XtraMessageBox.Show("Data successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EnableProperties(); // Enable properties after saving.
                    pdfViewer1.CloseDocument(); // Clear PDF viewer
                    LoadImage(); // Load image into grid control once the data was deleted
                    LoadLogs(); // Refresh the logs
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }

        //private byte[] GetImage()
        //{
        //    if (pictureEdit1.Image == null)
        //        return null;

        //    using (MemoryStream mStream = new MemoryStream())
        //    {
        //        pictureEdit1.Image.Save(mStream, pictureEdit1.Image.RawFormat);
        //        return mStream.ToArray();
        //    }
        //}
        private void ScheduleFileDeletion(string filePath)
        {
            Task.Run(() =>
            {
                Thread.Sleep(5000); // Delay to allow other processes to release the file

                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle any exceptions that occur during deletion
                    XtraMessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
        private void InsertLogs()
        {
            try
            {

                con.Open();
                MySqlCommand logCmd = con.CreateCommand();
                logCmd.CommandType = CommandType.Text;
                logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);

                logCmd.Parameters.AddWithValue("@Log", $"[Attachments] Inserted an attachment named '{txt_attachmentname.EditValue}'.");
                logCmd.Parameters.AddWithValue("@Logtype", 9); // Assuming 1 is for update
                logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

                logCmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteLogs()
        {
            try
            {

                con.Open();
                MySqlCommand logCmd = con.CreateCommand();
                logCmd.CommandType = CommandType.Text;
                logCmd.CommandText = @"
                    INSERT INTO log_masterlist 
                    (MasterListID, Log, Logtype, User, DateTimeEntry) 
                    VALUES 
                    (@MasterListID, @Log, @Logtype, @User, @DateTimeEntry)";
                logCmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);

                logCmd.Parameters.AddWithValue("@Log", $"[Attachments] Deleted an attachment named '{txt_attachmentname.EditValue}'.");
                logCmd.Parameters.AddWithValue("@Logtype", 9); // Assuming 1 is for update
                logCmd.Parameters.AddWithValue("@User", _username); // Replace with the actual user
                logCmd.Parameters.AddWithValue("@DateTimeEntry", DateTime.Now);

                logCmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //public void InsertImage()
        //{
        //    // For image saving below.
        //    try
        //    {
        //        if (selectedFilePath == null)
        //        {
        //            XtraMessageBox.Show("Please upload an image before saving.", "Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        if (string.IsNullOrWhiteSpace(txt_attachmentname.Text) || pictureEdit1.Image == null)
        //        {
        //            XtraMessageBox.Show("Please enter the attachment name ", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        byte[] imgByte = GetImage();
        //        if (imgByte == null || imgByte.Length == 0)
        //        {
        //            XtraMessageBox.Show("Failed to convert the image to a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        using (MySqlConnection con = new MySqlConnection("Server=172.26.153.181;uid=spinsv3;Password=Pn#z800^*OsR6B0;Database=caraga-spins2;default command timeout=3600;Allow User Variables=True;"))
        //        {
        //            con.Open();
        //            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO tbl_attachments (AttachmentName, MasterlistId, AttachmentUrl) VALUES (@AttachmentName, @MasterlistId, @AttachmentUrl)", con))
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.Parameters.AddWithValue("@AttachmentName", txt_attachmentname.Text);
        //                cmd.Parameters.AddWithValue("@MasterlistId", txt_id.Text);
        //                cmd.Parameters.AddWithValue("@AttachmentUrl", imgByte);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        InsertLogs();//Insert logs from attachments to log_masterlist.

        //        XtraMessageBox.Show("Image uploaded successfully!", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // Dispose of the image to release the file
        //        if (pictureEdit1.Image != null)
        //        {
        //            pictureEdit1.Image.Dispose();
        //            pictureEdit1.Image = null;
        //        }

        //        // Schedule file deletion if necessary
        //        if (File.Exists(selectedFilePath))
        //        {
        //            ScheduleFileDeletion(selectedFilePath);
        //        }

        //        // Clear fields
        //        txt_attachmentname.EditValue = "";

        //        selectedFilePath = null; // Clear the stored file path



        //        LoadImage();//Load image into gridcontrol once the data was deleted
        //        LoadLogs(); //Refresh the logs
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
     

        public void InsertAttachment()
        {
            try
            {
                if (selectedPdfPath == null)
                {
                    XtraMessageBox.Show("Please upload a file before saving.", "File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_attachmentname.Text))
                {
                    XtraMessageBox.Show("Please enter the attachment name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert the file data into the database
                using (MySqlConnection con = new MySqlConnection("Server=172.26.153.181;uid=spinsv3;Password=Pn#z800^*OsR6B0;Database=caraga-spins2;default command timeout=3600;Allow User Variables=True;"))
                {
                    GridView gridView = gridControl1.MainView as GridView;
                    DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);

                    string attachmentName = txt_attachmentname.Text;
                    string masterListID = txt_id.Text;

                    con.Open();

                    MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM tbl_attachments WHERE AttachmentName = @AttachmentName AND MasterListID = @MasterListID", con);
                    checkCmd.Parameters.AddWithValue("@AttachmentName", attachmentName);
                    checkCmd.Parameters.AddWithValue("@MasterListID", masterListID);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        XtraMessageBox.Show("An attachment with this name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO tbl_attachments (AttachmentName, MasterlistId) VALUES (@AttachmentName, @MasterlistId)", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@AttachmentName", txt_attachmentname.Text);
                        cmd.Parameters.AddWithValue("@MasterlistId", txt_id.Text);
                        //cmd.Parameters.AddWithValue("@AttachmentUrl", fileData);
                        // cmd.Parameters.AddWithValue("@FileType", fileExtension); // Store file type for later retrieval
                        cmd.ExecuteNonQuery();

                        // Save the PDF file to network path with custom filename based on referenceCode
                        string fileName = Path.GetFileName(selectedPdfPath);
                        //string destinationPath = @"\\172.26.153.181\gis\" + referenceCode + ".pdf";
                        string destinationPath = @"\\172.26.153.181\AttachmentsSupportingDocuments\" + txt_id.Text + " " + txt_attachmentname.Text + ".pdf";
                        //string destinationPath = @"E:\SPInS Documents\" + lbl_reference.Text + ".pdf";
                        File.Copy(selectedPdfPath, destinationPath, true);



                    }
                }

                //GenerateReferenceCode();
                // Insert logs from attachments to log_masterlist
                InsertLogs();



                // Dispose of the image if it was an image upload
                //if (pictureEdit1.Image != null)
                //{
                //    pictureEdit1.Image.Dispose();
                //    pictureEdit1.Image = null;

                // Schedule file deletion if necessary
                if (File.Exists(selectedPdfPath))
                {
                    ScheduleFileDeletion(selectedPdfPath);
                }

                // Clear fields
                txt_attachmentname.EditValue = "";
                selectedPdfPath = null; // Clear the stored file path

                // Reload data
                // LoadImage(); // Load image into gridcontrol once the data is deleted
                // LoadLogs();  // Refresh the logs
                //}
                pdfViewer1.CloseDocument(); //clear PDF
                this.Close();
                XtraMessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_id_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //pdfViewer1.CloseDocument();//close then trigger the clicked data
            ClickedData(); //Click into gridcontrol
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DeleteAttachment();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            InsertAttachment();
        }


        private void UploadFile()
        {
    
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF Files|*.pdf";
                openFileDialog.Title = "Select a PDF File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display selected PDF in PdfViewer or store path for later use
                    pdfViewer1.LoadDocument(openFileDialog.FileName);
                    selectedPdfPath = openFileDialog.FileName;
                }
            

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            pdfViewer1.CloseDocument();
            UploadFile();
        }








        private void btn_export_Click(object sender, EventArgs e)
        {
            //DownloadImage();
            //DownloadFile();
        }


        private void btn_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btn_save.PerformClick();
            }
        }

        private void Attachments_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl+W or Alt+F4 is pressed
            if ((e.Control && e.KeyCode == Keys.W) || (e.Alt && e.KeyCode == Keys.F4))
            {
                // Close the form
                this.Close();
            }
        }

        private void Attachments_FormClosing(object sender, FormClosingEventArgs e)
        {
            pdfViewer1.CloseDocument();
        }
    }
}
