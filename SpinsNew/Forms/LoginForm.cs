using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using SpinsNew.Connection;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class LoginForm : Form
    {
        ConnectionString cs = new ConnectionString();
        MySqlConnection con = null;
        public LoginForm()
        {
            InitializeComponent();
            con = new MySqlConnection(cs.dbcon);
            //CheckForUpdates();
        
            //GitHubUpdater();
        }
 
        //private void Updater()
        //{
        //    string version = "1.0.3";
        //    lbl_version.Text = version;
        //    try
        //    {

        //            WebClient webClient = new WebClient();
        //            var client = new WebClient();
                  

        //            if (!webClient.DownloadString("https://www.dropbox.com/scl/fi/rh7cf7gp0wvxymt9kxy44/Update.txt?rlkey=w9ta6284ny36up0xiq9ub8y7e&st=m9y21lcp&dl=1").Contains(version))
        //            {
        //                if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    try
        //                    {
        //                        if (File.Exists(@".\SpinsInstallerNew.msi")) { File.Delete(@".\SpinsInstallerNew.msi"); }
        //                        client.DownloadFile("https://www.dropbox.com/scl/fi/li0gbjhtbqqmqnecvwd5m/SpinsInstallerNew.zip?rlkey=x1lpbi0r2klyxp34470mn71ah&st=utr4c79g&dl=1", @"SpinsInstallerNew.zip");
        //                        string zipPath = @".\SpinsInstallerNew.zip";
        //                        string extractPath = @".\";
        //                        ZipFile.ExtractToDirectory(zipPath, extractPath);


        //                        Process process = new Process();
        //                        process.StartInfo.FileName = "msiexec";
        //                        process.StartInfo.Arguments = String.Format("/i SpinsInstallerNew.msi");



        //                        process.Start();

        //                        Application.Exit();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                         XtraMessageBox.Show(ex.Message);
        //                    }
        //                }

        //            }
                

        //    }
        //    catch(Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}
        //private void GitHubUpdater()
        //{
        //    WebClient webClient = new WebClient();

        //    try
        //    {
        //        // Check for updates
        //        string latestVersion = webClient.DownloadString("https://github.com/dagon12345/SpinsNew/raw/master/SpinsNew/Updates/Update.txt").Trim();

        //        // Replace "1.0.0" with your application's current version
        //        if (!latestVersion.Contains("1.0.5"))
        //        {
        //            // Notify the user of the available update
        //            XtraMessageBox.Show("New Update available!");

        //            if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                try
        //                {
        //                    // Delete old installer if it exists
        //                    if (File.Exists(@".\SpinsInstaller.msi"))
        //                    {
        //                        File.Delete(@".\SpinsInstaller.msi");
        //                    }

        //                    // Download the installer from GitHub Releases (Use the raw URL for the ZIP file)
        //                    string downloadUrl = "https://github.com/dagon12345/SpinsNew/raw/master/SpinsNew/Updates/SpinsInstaller.zip";
        //                    webClient.DownloadFile(downloadUrl, @"SpinsInstaller.zip");

        //                    string zipPath = @".\SpinsInstaller.zip";
        //                    string extractPath = @".\";

        //                    // Ensure the downloaded file is a valid ZIP file
        //                    if (File.Exists(zipPath) && new FileInfo(zipPath).Length > 0)
        //                    {
        //                        try
        //                        {
        //                            ZipFile.ExtractToDirectory(zipPath, extractPath);

        //                            // Start the installer
        //                            Process process = new Process();
        //                            process.StartInfo.FileName = "msiexec";
        //                            process.StartInfo.Arguments = String.Format("/i {0}", Path.Combine(extractPath, "SpinsInstaller.msi"));
        //                            process.Start();

        //                            // Exit application after starting the installer
        //                            Application.Exit();
        //                        }
        //                        catch (InvalidDataException)
        //                        {
        //                            XtraMessageBox.Show("The ZIP file is invalid or corrupted. Please try downloading it again.");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        XtraMessageBox.Show("Failed to download the update. The file is either missing or empty.");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    XtraMessageBox.Show("An error occurred while updating: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show("An error occurred while checking for updates: " + ex.Message);
        //    }
        //}



        //private void GitHubUpdater()
        //{

        //        using (var client1 = new WebClient())
        //        using (var stream = client1.OpenRead("http://www.google.com"))
        //        {
        //            // Check for updates
        //            WebClient webClient = new WebClient();
        //            var client = new WebClient();

        //            // Use the raw content URL to check for the latest version on GitHub
        //            string latestVersion = webClient.DownloadString("https://github.com/dagon12345/SpinsNew/tree/master/SpinsNew/Updates/Update.txt").Trim();

        //            // Replace "1.0.0" with your application's current version
        //            if (!latestVersion.Contains("1.0.2"))
        //            {
        //                XtraMessageBox.Show("Update available!");

        //                if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    //try
        //                    //{
        //                        if (File.Exists(@".\SpinsInstaller.msi")) { File.Delete(@".\SpinsInstaller.msi"); }

        //                        // Download the installer from GitHub Releases
        //                        client.DownloadFile("https://github.com/dagon12345/SpinsNew/blob/master/SpinsNew/Updates/SpinsInstaller.zip", @"SpinsInstaller.zip");

        //                        string zipPath = @".\SpinsInstaller.zip";
        //                        string extractPath = @".\";
        //                        ZipFile.ExtractToDirectory(zipPath, extractPath);

        //                        Process process = new Process();
        //                        process.StartInfo.FileName = "msiexec";
        //                        process.StartInfo.Arguments = String.Format("/i SpinsInstaller.msi");

        //                        process.Start();
        //                        Application.Exit();
        //                    //}
        //                    //catch (Exception ex)
        //                    //{
        //                    //    XtraMessageBox.Show(ex.Message);
        //                    //}
        //                }
        //            }
        //        }


        //}

        private void Updater()
        {
            string version = "1.0.6";
           // lbl_version.Text = version;
            WebClient webClient = null;
            WebClient client = null;

            try
            {
                webClient = new WebClient();
                client = new WebClient();

                string updateInfoUrl = "https://www.dropbox.com/scl/fi/bh1ebdz0fkjeg2naan91v/Update.txt?rlkey=pfu89kxiv6ag9n2pmpgsg9tmd&st=9pc7svpy&dl=1";
                string updateContent = webClient.DownloadString(updateInfoUrl);

                if (!updateContent.Contains(version))
                {
                    if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            string installerPath = @".\SpinsInstallerNew.msi";
                            if (File.Exists(installerPath)) { File.Delete(installerPath); }

                            string zipUrl = "https://www.dropbox.com/scl/fi/00z27kx6e8mq2vpl440xz/SpinsInstallerNew.zip?rlkey=5mj2f7w0vp8323rds1bvvk7tq&st=1mkothyx&dl=1";
                            string zipPath = @".\SpinsInstallerNew.zip";
                            client.DownloadFile(zipUrl, zipPath);

                            string extractPath = @".\";
                            ZipFile.ExtractToDirectory(zipPath, extractPath);

                            Process process = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "msiexec",
                                    Arguments = "/i SpinsInstallerNew.msi",
                                    UseShellExecute = true
                                }
                            };

                            process.Start();
                            Application.Exit();
                        }
                        catch (WebException webEx)
                        {
                            XtraMessageBox.Show("Network error: " + webEx.Message);
                        }
                        catch (UnauthorizedAccessException uaEx)
                        {
                            XtraMessageBox.Show("Permission error: " + uaEx.Message);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show("An error occurred: " + ex.Message);
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                XtraMessageBox.Show("Network error: " + webEx.Message);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                webClient?.Dispose();
                client?.Dispose();
            }
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            //Updater();
        }

        //private async Task CheckForUpdates()
        //{
        //    using (var manager = new UpdateManager(@"C:\Users\DSWD\source\repos\SpinsNew\SpinsNew\Release"))
        //    {
        //        await manager.UpdateApp();
        //    }
        //}

        RegistrationForm registrationForm;
        private void hyper_register_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RegistrationForm>().Any())
            {
                registrationForm.Select();
                registrationForm.BringToFront();
            }
            else
            {
                registrationForm = new RegistrationForm();
                registrationForm.Show();
            }
        }
        private void Login()
        {
            try
            {
                //CODE FOR LOGIN FORM

                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Lastname, Firstname, Middlename, Username, Password, UserRole, IsActive FROM tbl_registered_users WHERE Username=@Username AND Password=@Password";
                cmd.Parameters.AddWithValue("@Username", txt_username.EditValue);
                cmd.Parameters.AddWithValue("@Password", txt_password.EditValue);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if(Convert.ToInt32(row["IsActive"]) == 0)
                    {
                        XtraMessageBox.Show("Account not yet activated. Please contact the administrator.", "Request for Activation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Retrieve user details
                    string lastName = row["Lastname"].ToString();
                    string firstName = row["Firstname"].ToString();
                    string username = row["Username"].ToString();
                    string userRole = row["UserRole"].ToString();

                    Dashboard dash = new Dashboard(lastName, firstName, username, userRole);
                    this.Hide();
                    dash.Show();
                    
                  
                }
                else
                {
                    XtraMessageBox.Show("Invalid login details", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Text == "")
            {
                XtraMessageBox.Show("Please fill all the box to login", "Fill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Login();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void crystalLink_Click(object sender, EventArgs e)
        {
            string url = "https://www.dropbox.com/scl/fi/pkc8mobyyll9296tup4je/Cyrstal-report-2019.EXE?rlkey=72gy11cz0jey6a4eafgufspeh&st=nt54574s&dl=1";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
