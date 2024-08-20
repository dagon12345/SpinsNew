
namespace SpinsNew.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txt_password = new DevExpress.XtraEditors.TextEdit();
            this.txt_username = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btn_login = new DevExpress.XtraEditors.SimpleButton();
            this.hyper_register = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.hyper_register);
            this.groupControl2.Controls.Add(this.btn_login);
            this.groupControl2.Controls.Add(this.txt_password);
            this.groupControl2.Controls.Add(this.txt_username);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Location = new System.Drawing.Point(191, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(356, 172);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Credentials";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(96, 76);
            this.txt_password.Name = "txt_password";
            this.txt_password.Properties.UseSystemPasswordChar = true;
            this.txt_password.Size = new System.Drawing.Size(214, 20);
            this.txt_password.TabIndex = 1;
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(96, 42);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(214, 20);
            this.txt_username.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(30, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Username";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(33, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 15);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Password";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(12, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(173, 172);
            this.pictureEdit1.TabIndex = 3;
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Location = new System.Drawing.Point(142, 125);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(103, 23);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // hyper_register
            // 
            this.hyper_register.Location = new System.Drawing.Point(96, 102);
            this.hyper_register.Name = "hyper_register";
            this.hyper_register.Size = new System.Drawing.Size(153, 13);
            this.hyper_register.TabIndex = 3;
            this.hyper_register.Text = "Don\'t have an account? Create.";
            this.hyper_register.Click += new System.EventHandler(this.hyper_register_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btn_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 193);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.groupControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(579, 232);
            this.MinimumSize = new System.Drawing.Size(579, 232);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txt_password;
        private DevExpress.XtraEditors.TextEdit txt_username;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyper_register;
        private DevExpress.XtraEditors.SimpleButton btn_login;
    }
}