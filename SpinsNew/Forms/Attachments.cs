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

        // Method to convert byte array to Image
        //private Image ByteArrayToImage(byte[] byteArrayIn)
        //{
        //    using (MemoryStream ms = new MemoryStream(byteArrayIn))
        //    {
        //        Image img = Image.FromStream(ms);
        //        return img;
        //    }
        //}
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void DisableProperty()
        {
            txt_attachmentname.Enabled = false;
            btn_save.Enabled = false;
        }
        public void ClickedData()
        {
            try
            {
                if (gridControl1 == null)
                {
                    XtraMessageBox.Show("Grid control is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                GridView gridView = gridControl1.MainView as GridView;
                if (gridView == null)
                {
                    XtraMessageBox.Show("Grid view is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gridView.RowCount == 0)
                {
                    return;
                }

                DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                if (row == null)
                {
                    XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(row["ID"]);

                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tbl_attachments WHERE Id=@id";
                cmd.Parameters.AddWithValue("@Id", id);
                DataTable dt = new DataTable();
                using (var da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    DisableProperty();
                    // Get the attachment name and file extension
                    string attachmentName = dr["AttachmentName"].ToString();
                    string fileExtension = Path.GetExtension(attachmentName).TrimStart('.').ToLower();

                        txt_attachmentname.EditValue = attachmentName;
                        fileData = (byte[])dr["AttachmentUrl"]; // Assuming the BLOB data is stored in the 'AttachmentUrl' column

                        // Handle PDF file
                        // Save the PDF to a temporary location and open it with a PDF viewer
                        string tempFilePath = Path.Combine(Path.GetTempPath(), attachmentName);
                        File.WriteAllBytes(tempFilePath, fileData);

                    // Open the PDF with a default viewer or load it into a PDF viewer control
                        pdfViewer1.CloseDocument();
                        pdfViewer1.LoadDocument(tempFilePath);
  

                }
                con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        //public void ClickedData()
        //{
        //    try
        //    {
        //        if (gridControl1 == null)
        //        {
        //            XtraMessageBox.Show("Grid control is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        GridView gridView = gridControl1.MainView as GridView;
        //        if (gridView == null)
        //        {
        //            XtraMessageBox.Show("Grid view is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        if (gridView.RowCount == 0)
        //        {
        //           // XtraMessageBox.Show("No data in the grid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
        //        if (row == null)
        //        {
        //            XtraMessageBox.Show("No row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // Pass the ID value to the EditApplicant form
        //        int id = Convert.ToInt32(row["ID"]);
        //        // GridView gridView = gridControl1.MainView as GridView;
        //        con.Open();
        //        MySqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT * FROM tbl_attachments WHERE Id=@id";
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        DataTable dt = new DataTable();
        //        using (var da = new MySqlDataAdapter(cmd))
        //        {
        //            da.Fill(dt);
        //        }
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            DisableProperty();

        //            txt_attachmentname.EditValue = dr["AttachmentName"].ToString();
        //            // Assuming the blob data is in the second column (index 1)
        //            byte[] blobData = (byte[])dr[3];

        //            //// Convert blob data to Image
        //            Image image = ByteArrayToImage(blobData);

        //            //// Display image in PictureBox
        //            pictureEdit1.Image = image;

        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}
        public void LoadImage()
        {
            try
            {
                // Assuming gridView is your GridView instance associated with gridControl1
                GridView gridView = gridControl1.MainView as GridView;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id,AttachmentName,MasterListID from tbl_attachments WHERE MasterListID=@MasterListID";
                cmd.Parameters.AddWithValue("@MasterListID", txt_id.Text);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                //await Task.Run(async () =>
                //{
                //    await Task.Run(() => da.Fill(dt));
                //});
                da.Fill(dt);
                //We are using DevExpress datagridview
                gridControl1.DataSource = dt;

                // GridView gridView = gridControl1.MainView as GridView;
                if (gridView != null)
                {
                    // Auto-size all columns based on their content
                    gridView.BestFitColumns();

                    // Hide the "ID" column
                    gridView.Columns["Id"].Visible = false;
                    gridView.Columns["MasterListID"].Visible = false;

                    // Ensure horizontal scrollbar is enabled
                    //gridView.OptionsView.ColumnAutoWidth = false;
                    // Disable editing
                    gridView.OptionsBehavior.Editable = false;

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
            //DELETE USERS FROM DATABASE.
            if (txt_attachmentname.Text == "")
            {
                XtraMessageBox.Show("Select data you want to delete", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (MessageBox.Show("Are you sure you want to delete this data?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GridView gridView = gridControl1.MainView as GridView;
                    con.Open();
                    // Pass the ID value to the EditApplicant form
                    DataRowView row = (DataRowView)gridView.GetRow(gridView.FocusedRowHandle);
                    int id = Convert.ToInt32(row["ID"]);
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM tbl_attachments WHERE id=" + id + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DeleteLogs();
                    XtraMessageBox.Show("Data successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    EnableProperties();// Enable property after saving.
                    pdfViewer1.CloseDocument(); //clear PDF
                    LoadImage();//Load image into gridcontrol once the data was deleted
                    LoadLogs(); //Refresh the logs

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

                // Determine file type and convert it to byte array
                string fileExtension = Path.GetExtension(selectedPdfPath).ToLower();
                byte[] fileData = null;

                // Handle different file types
                //if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                //{
                //    // Convert image to byte array
                //    fileData = GetImage();
                //    if (fileData == null || fileData.Length == 0)
                //    {
                //        XtraMessageBox.Show("Failed to convert the image to a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
                //else if (fileExtension == ".pdf")
                //{
                // Convert PDF to byte array
                fileData = File.ReadAllBytes(selectedPdfPath);
                if (fileData == null || fileData.Length == 0)
                {
                    XtraMessageBox.Show("Failed to load the PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //}
                //else
                //{
                //    XtraMessageBox.Show("Unsupported file type. Please upload an image or PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                // Insert the file data into the database
                using (MySqlConnection con = new MySqlConnection("Server=172.26.153.181;uid=spinsv3;Password=Pn#z800^*OsR6B0;Database=caraga-spins2;default command timeout=3600;Allow User Variables=True;"))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO tbl_attachments (AttachmentName, MasterlistId, AttachmentUrl) VALUES (@AttachmentName, @MasterlistId, @AttachmentUrl)", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@AttachmentName", txt_attachmentname.Text);
                        cmd.Parameters.AddWithValue("@MasterlistId", txt_id.Text);
                        cmd.Parameters.AddWithValue("@AttachmentUrl", fileData);
                        // cmd.Parameters.AddWithValue("@FileType", fileExtension); // Store file type for later retrieval
                        cmd.ExecuteNonQuery();
                    }
                }

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
            pdfViewer1.CloseDocument();//close then trigger the clicked data
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
            try
            {
                // Open file dialog   
                OpenFileDialog open = new OpenFileDialog();
                // File filters for images and PDFs
                open.Filter = "PDF Files (*.pdf)|*.pdf";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    // Check the size of the selected file
                    FileInfo fileInfo = new FileInfo(open.FileName);
                    long fileSizeInBytes = fileInfo.Length;
                    long fileSizeInKB = fileSizeInBytes / 1024;
                    long fileSizeInMB = fileSizeInKB / 1024;

                    // Check if file size exceeds 2MB
                    if (fileSizeInMB > 2)
                    {
                        MessageBox.Show("Please select a file that is less than 2MB in size.", "File Size Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method without further processing
                    }

                    //// Handle image files
                    //if (open.FilterIndex == 1)
                    //{
                    //    // Display image in picture box  
                    //    pictureEdit1.Image = new Bitmap(open.FileName);
                    //}
                    // Handle PDF files (consider using a different control for PDF preview if needed)

                    // Convert the file to a byte array
                    fileData = File.ReadAllBytes(open.FileName);



                    //if (open.ShowDialog() == DialogResult.OK)
                    //{
                    // Display selected PDF in PdfViewer or store path for later use
                    pdfViewer1.LoadDocument(open.FileName);
                    selectedPdfPath = open.FileName;
                    //}
                    // Store the byte array (fileData) in your database as LONG BLOB
                    //SaveToDatabase(fileData, fileInfo.Extension);

                    // Store the file path
                    //selectedFilePath = open.FileName;

                    // Enable and clear attachment name textbox  
                    EnableProperties();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        //private void UploadImage()
        //{
        //    try
        //    {
        //        // Open file dialog   
        //        OpenFileDialog open = new OpenFileDialog();
        //        // Image filters  
        //        open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

        //        if (open.ShowDialog() == DialogResult.OK)
        //        {
        //            // Check the size of the selected image
        //            FileInfo fileInfo = new FileInfo(open.FileName);
        //            long fileSizeInBytes = fileInfo.Length; // File size in bytes
        //            long fileSizeInKB = fileSizeInBytes / 1024; // File size in kilobytes
        //            long fileSizeInMB = fileSizeInKB / 1024; // File size in megabytes

        //            // Check if file size exceeds 2MB
        //            if (fileSizeInMB > 2)
        //            {
        //                MessageBox.Show("Please select an image file that is less than 2MB in size.", "File Size Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return; // Exit the method without further processing
        //            }

        //            // Display image in picture box  
        //            pictureEdit1.Image = new Bitmap(open.FileName);

        //            // Store the file path
        //            selectedFilePath = open.FileName;
        //            // Enable and clear attachment name textbox  
        //            EnableProperties();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}
        private void btn_upload_Click(object sender, EventArgs e)
        {
            //UploadImage();
            UploadFile();
        }
        //private void DownloadImage()
        //{
        //    // Check if PictureEdit contains an image
        //    if (pictureEdit1.Image != null)
        //    {
        //        // Open a SaveFileDialog to choose where to save the image
        //        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        //        {
        //            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
        //            saveFileDialog.Title = $"{lbl_fullname.Text} - {txt_attachmentname.Text}";
        //            saveFileDialog.FileName = $"{lbl_fullname.Text} - {txt_attachmentname.Text}";
        //            if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //            {
        //                // Determine the image format based on the selected file extension
        //                ImageFormat imageFormat = ImageFormat.Bmp;
        //                switch (saveFileDialog.FilterIndex)
        //                {
        //                    case 1:
        //                        imageFormat = ImageFormat.Jpeg;
        //                        break;
        //                    case 2:
        //                        imageFormat = ImageFormat.Png;
        //                        break;
        //                        //case 3:
        //                        //    imageFormat = ImageFormat.Bmp;
        //                        //    break;
        //                }

        //                // Save the image to the selected file path
        //                using (Image imageToSave = new Bitmap(pictureEdit1.Image)) // Create a copy to avoid potential issues
        //                {
        //                    imageToSave.Save(saveFileDialog.FileName, imageFormat);
        //                }

        //                XtraMessageBox.Show("Image saved successfully.");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        XtraMessageBox.Show("No image to export.");
        //    }
        //}

        private void DownloadFile()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF File|*.pdf";
                    saveFileDialog.Title = $"{lbl_fullname.Text} - {txt_attachmentname.Text}";
                    saveFileDialog.FileName = $"{lbl_fullname.Text} - {txt_attachmentname.Text}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Assuming 'fileData' is the byte array containing the PDF file data from the database
                        File.WriteAllBytes(saveFileDialog.FileName, fileData);
                        XtraMessageBox.Show("PDF saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //XtraMessageBox.Show("PDF saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }



        // Helper methods to check file type and handle file operations
        private bool IsImageFile(string fileExtension)
        {
            // Check if the file extension is one of the common image types
            return fileExtension.Equals("jpg", StringComparison.OrdinalIgnoreCase) ||
                   fileExtension.Equals("jpeg", StringComparison.OrdinalIgnoreCase) ||
                   fileExtension.Equals("png", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsPdfFile(string fileName)
        {
            // Check if the file name ends with .pdf
            return fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            //DownloadImage();
            DownloadFile();
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
