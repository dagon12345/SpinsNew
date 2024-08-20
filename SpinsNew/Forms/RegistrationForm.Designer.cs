
namespace SpinsNew.Forms
{
    partial class RegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dt_birth = new DevExpress.XtraEditors.DateEdit();
            this.txt_middlename = new DevExpress.XtraEditors.TextEdit();
            this.txt_lastname = new DevExpress.XtraEditors.TextEdit();
            this.txt_firstname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txt_confirmpass = new DevExpress.XtraEditors.TextEdit();
            this.txt_password = new DevExpress.XtraEditors.TextEdit();
            this.txt_username = new DevExpress.XtraEditors.TextEdit();
            this.btn_register = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_birth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_birth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_middlename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_lastname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_firstname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_confirmpass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dt_birth);
            this.groupControl1.Controls.Add(this.txt_middlename);
            this.groupControl1.Controls.Add(this.txt_lastname);
            this.groupControl1.Controls.Add(this.txt_firstname);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Location = new System.Drawing.Point(13, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(235, 232);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Details";
            // 
            // dt_birth
            // 
            this.dt_birth.EditValue = null;
            this.dt_birth.Location = new System.Drawing.Point(17, 204);
            this.dt_birth.Name = "dt_birth";
            this.dt_birth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_birth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_birth.Size = new System.Drawing.Size(213, 20);
            this.dt_birth.TabIndex = 3;
            // 
            // txt_middlename
            // 
            this.txt_middlename.Location = new System.Drawing.Point(16, 151);
            this.txt_middlename.Name = "txt_middlename";
            this.txt_middlename.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_middlename.Size = new System.Drawing.Size(214, 20);
            this.txt_middlename.TabIndex = 2;
            // 
            // txt_lastname
            // 
            this.txt_lastname.Location = new System.Drawing.Point(16, 47);
            this.txt_lastname.Name = "txt_lastname";
            this.txt_lastname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_lastname.Size = new System.Drawing.Size(214, 20);
            this.txt_lastname.TabIndex = 0;
            // 
            // txt_firstname
            // 
            this.txt_firstname.Location = new System.Drawing.Point(16, 99);
            this.txt_firstname.Name = "txt_firstname";
            this.txt_firstname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_firstname.Size = new System.Drawing.Size(214, 20);
            this.txt_firstname.TabIndex = 1;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(16, 130);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 15);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "Middle Name";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(16, 26);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(56, 15);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "Last Name";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(17, 78);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(57, 15);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "First Name";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(16, 182);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 15);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Birth Date";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(16, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Username";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(16, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 15);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Password";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(16, 130);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 15);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "ConfirmPassword";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txt_confirmpass);
            this.groupControl2.Controls.Add(this.txt_password);
            this.groupControl2.Controls.Add(this.txt_username);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Location = new System.Drawing.Point(254, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(235, 232);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Credentials";
            // 
            // txt_confirmpass
            // 
            this.txt_confirmpass.Location = new System.Drawing.Point(16, 151);
            this.txt_confirmpass.Name = "txt_confirmpass";
            this.txt_confirmpass.Properties.UseSystemPasswordChar = true;
            this.txt_confirmpass.Size = new System.Drawing.Size(214, 20);
            this.txt_confirmpass.TabIndex = 2;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(16, 99);
            this.txt_password.Name = "txt_password";
            this.txt_password.Properties.UseSystemPasswordChar = true;
            this.txt_password.Size = new System.Drawing.Size(214, 20);
            this.txt_password.TabIndex = 1;
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(16, 47);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(214, 20);
            this.txt_username.TabIndex = 0;
            // 
            // btn_register
            // 
            this.btn_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_register.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btn_register.Location = new System.Drawing.Point(189, 250);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(126, 32);
            this.btn_register.TabIndex = 2;
            this.btn_register.Text = "Register";
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // RegistrationForm
            // 
            this.AcceptButton = this.btn_register;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 298);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(530, 551);
            this.MinimizeBox = false;
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrationForm";
            this.Load += new System.EventHandler(this.RegistrationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_birth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_birth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_middlename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_lastname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_firstname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_confirmpass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_username.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btn_register;
        private DevExpress.XtraEditors.TextEdit txt_middlename;
        private DevExpress.XtraEditors.TextEdit txt_lastname;
        private DevExpress.XtraEditors.TextEdit txt_firstname;
        private DevExpress.XtraEditors.DateEdit dt_birth;
        private DevExpress.XtraEditors.TextEdit txt_confirmpass;
        private DevExpress.XtraEditors.TextEdit txt_password;
        private DevExpress.XtraEditors.TextEdit txt_username;
    }
}