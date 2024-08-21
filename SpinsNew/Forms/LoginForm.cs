using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using SpinsNew.Connection;
using Squirrel;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            //Updater();
            GitHubUpdater();
        }

        private void Updater()
        {
            try
            {
                using (var client1 = new WebClient())
                using (var stream = client1.OpenRead("http://www.google.com"))
                {

                    ////////////CHECK UPDATES
                    //lbl_internet.Invoke((MethodInvoker)delegate
                    //{
                    //    // Access lbl_internet here
                    //    lbl_internet.Text = "Checking for updates...";
                    //});

                    WebClient webClient = new WebClient();
                    var client = new WebClient();

                    if (!webClient.DownloadString("https://www.dropbox.com/scl/fi/rh7cf7gp0wvxymt9kxy44/Update.txt?rlkey=w9ta6284ny36up0xiq9ub8y7e&st=v47zh5qg&dl=1").Contains("1.0.0"))
                    {
                        XtraMessageBox.Show("Update available!");
                        //lbl_internet.Invoke((MethodInvoker)delegate
                        //{
                        //    // Access lbl_internet here
                        //    lbl_internet.Text = "Update available!";
                        //});

                        if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                //lbl_internet.Invoke((MethodInvoker)delegate
                                //{
                                //    // Access lbl_internet here
                                //    lbl_internet.Text = "Installing update, please wait....";
                                //});

                                if (File.Exists(@".\SpinsInstaller.msi")) { File.Delete(@".\SpinsInstaller.msi"); }
                                client.DownloadFile("https://www.dropbox.com/scl/fi/ggdfa7m3naiydw8kddwiv/SpinsInstaller.zip?rlkey=ovx09mtlfia6suiy7ejsmo2t2&st=nkgmz3bb&dl=1", @"SpinsInstaller.zip");
                                string zipPath = @".\SpinsInstaller.zip";
                                string extractPath = @".\";
                                ZipFile.ExtractToDirectory(zipPath, extractPath);


                                Process process = new Process();
                                process.StartInfo.FileName = "msiexec";
                                process.StartInfo.Arguments = String.Format("/i SpinsInstaller.msi");



                                process.Start();

                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                 XtraMessageBox.Show(ex.Message);
                            }
                        }
                        //else
                        //{
                        //    this.Hide();
                        //    frmSelectSection fs = new frmSelectSection();
                        //    fs.Show();
                        //}

                    }





                    //lbl_internet.Invoke((MethodInvoker)delegate
                    //{
                    //    // Access lbl_internet here
                    //    lbl_internet.Text = "Online";
                    //});


                }

            }
            catch(Exception ex)
            {

                XtraMessageBox.Show(ex.Message);
                //lbl_internet.Invoke((MethodInvoker)delegate
                //{
                //    // Access lbl_internet here
                //    lbl_internet.Text = "Local Network";
                //});

            }
        }
        private void GitHubUpdater()
        {
            try
            {
                using (var client1 = new WebClient())
                using (var stream = client1.OpenRead("http://www.google.com"))
                {
                    // Check for updates
                    WebClient webClient = new WebClient();
                    var client = new WebClient();

                    // Use the raw content URL to check for the latest version on GitHub
                    string latestVersion = webClient.DownloadString("https://github.com/dagon12345/SpinsNew/tree/master/SpinsNew/Updates/Update.txt").Trim();

                    // Replace "1.0.0" with your application's current version
                    if (!latestVersion.Contains("1.0.1"))
                    {
                        XtraMessageBox.Show("Update available!");

                        if (MessageBox.Show("New update available! Do you want to install it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                if (File.Exists(@".\SpinsInstaller.msi")) { File.Delete(@".\SpinsInstaller.msi"); }

                                // Download the installer from GitHub Releases
                                client.DownloadFile("https://github.com/dagon12345/SpinsNew/tree/master/SpinsNew/Updates/SpinsInstaller.zip", @"SpinsInstaller.zip");

                                string zipPath = @".\SpinsInstaller.zip";
                                string extractPath = @".\";
                                ZipFile.ExtractToDirectory(zipPath, extractPath);

                                Process process = new Process();
                                process.StartInfo.FileName = "msiexec";
                                process.StartInfo.Arguments = String.Format("/i SpinsInstaller.msi");

                                process.Start();
                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
          
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
