
namespace SpinsNew.Forms
{
    partial class PayrollFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayrollFiles));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblPeriod = new DevExpress.XtraEditors.LabelControl();
            this.lbl_municipality = new DevExpress.XtraEditors.LabelControl();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_municipality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btn_upload = new DevExpress.XtraEditors.SimpleButton();
            this.list_pictures = new DevExpress.XtraEditors.ListBoxControl();
            this.cmb_selectfolder = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btn_left = new DevExpress.XtraEditors.SimpleButton();
            this.btn_rotate = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_pictures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_selectfolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.lblPeriod);
            this.groupControl1.Controls.Add(this.lbl_municipality);
            this.groupControl1.Controls.Add(this.cmb_period);
            this.groupControl1.Controls.Add(this.cmb_year);
            this.groupControl1.Controls.Add(this.cmb_municipality);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(220, 114);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Filter";
            // 
            // lblPeriod
            // 
            this.lblPeriod.Appearance.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPeriod.Appearance.Options.UseFont = true;
            this.lblPeriod.Location = new System.Drawing.Point(168, 5);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(35, 15);
            this.lblPeriod.TabIndex = 83;
            this.lblPeriod.Text = "Period";
            this.lblPeriod.Visible = false;
            // 
            // lbl_municipality
            // 
            this.lbl_municipality.Appearance.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_municipality.Appearance.Options.UseFont = true;
            this.lbl_municipality.Location = new System.Drawing.Point(79, 5);
            this.lbl_municipality.Name = "lbl_municipality";
            this.lbl_municipality.Size = new System.Drawing.Size(24, 15);
            this.lbl_municipality.TabIndex = 82;
            this.lbl_municipality.Text = "Mun";
            this.lbl_municipality.Visible = false;
            // 
            // cmb_period
            // 
            this.cmb_period.EditValue = "Select Period";
            this.cmb_period.Location = new System.Drawing.Point(5, 78);
            this.cmb_period.Name = "cmb_period";
            this.cmb_period.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_period.Size = new System.Drawing.Size(210, 20);
            this.cmb_period.TabIndex = 23;
            this.cmb_period.SelectedIndexChanged += new System.EventHandler(this.cmb_period_SelectedIndexChanged);
            // 
            // cmb_year
            // 
            this.cmb_year.EditValue = "Select Year";
            this.cmb_year.Location = new System.Drawing.Point(5, 52);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_year.Properties.DropDownRows = 15;
            this.cmb_year.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_year.Size = new System.Drawing.Size(210, 20);
            this.cmb_year.TabIndex = 22;
            // 
            // cmb_municipality
            // 
            this.cmb_municipality.EditValue = "Select City/Municipality";
            this.cmb_municipality.Location = new System.Drawing.Point(5, 26);
            this.cmb_municipality.Name = "cmb_municipality";
            this.cmb_municipality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_municipality.Properties.DropDownRows = 20;
            this.cmb_municipality.Properties.HideSelection = false;
            this.cmb_municipality.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            this.cmb_municipality.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_municipality.Size = new System.Drawing.Size(210, 20);
            this.cmb_municipality.TabIndex = 21;
            this.cmb_municipality.SelectedIndexChanged += new System.EventHandler(this.cmb_municipality_SelectedIndexChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl2.Controls.Add(this.list_pictures);
            this.groupControl2.Controls.Add(this.cmb_selectfolder);
            this.groupControl2.Location = new System.Drawing.Point(12, 132);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(220, 480);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Files";
            // 
            // btn_upload
            // 
            this.btn_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_upload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btn_upload.Location = new System.Drawing.Point(626, 26);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(95, 23);
            this.btn_upload.TabIndex = 2;
            this.btn_upload.Text = "Upload Files";
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // list_pictures
            // 
            this.list_pictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_pictures.Location = new System.Drawing.Point(5, 52);
            this.list_pictures.Name = "list_pictures";
            this.list_pictures.Size = new System.Drawing.Size(210, 423);
            this.list_pictures.TabIndex = 1;
            this.list_pictures.SelectedIndexChanged += new System.EventHandler(this.list_pictures_SelectedIndexChanged);
            // 
            // cmb_selectfolder
            // 
            this.cmb_selectfolder.EditValue = "Select Folder";
            this.cmb_selectfolder.Location = new System.Drawing.Point(5, 26);
            this.cmb_selectfolder.Name = "cmb_selectfolder";
            this.cmb_selectfolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_selectfolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_selectfolder.Size = new System.Drawing.Size(210, 20);
            this.cmb_selectfolder.TabIndex = 0;
            this.cmb_selectfolder.SelectedIndexChanged += new System.EventHandler(this.cmb_selectfolder_SelectedIndexChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.btn_upload);
            this.groupControl3.Controls.Add(this.btn_left);
            this.groupControl3.Controls.Add(this.btn_rotate);
            this.groupControl3.Controls.Add(this.pictureEdit1);
            this.groupControl3.Location = new System.Drawing.Point(238, 12);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(726, 600);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "Image";
            // 
            // btn_left
            // 
            this.btn_left.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_left.ImageOptions.Image")));
            this.btn_left.Location = new System.Drawing.Point(5, 29);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(195, 23);
            this.btn_left.TabIndex = 2;
            this.btn_left.Text = "Rotate Left (Ctrl + Left Arrow)";
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_rotate
            // 
            this.btn_rotate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_rotate.ImageOptions.Image")));
            this.btn_rotate.Location = new System.Drawing.Point(206, 29);
            this.btn_rotate.Name = "btn_rotate";
            this.btn_rotate.Size = new System.Drawing.Size(195, 23);
            this.btn_rotate.TabIndex = 1;
            this.btn_rotate.Text = "Rotate right  (Ctrl + Right Arrow)";
            this.btn_rotate.Click += new System.EventHandler(this.btn_rotate_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.Location = new System.Drawing.Point(5, 55);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(716, 540);
            this.pictureEdit1.TabIndex = 0;
            // 
            // PayrollFiles
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 624);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("PayrollFiles.IconOptions.Icon")));
            this.Name = "PayrollFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PayrollFiles";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PayrollFiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.list_pictures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_selectfolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ListBoxControl list_pictures;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_selectfolder;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_period;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_year;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_municipality;
        private DevExpress.XtraEditors.LabelControl lbl_municipality;
        private DevExpress.XtraEditors.LabelControl lblPeriod;
        private DevExpress.XtraEditors.SimpleButton btn_rotate;
        private DevExpress.XtraEditors.SimpleButton btn_left;
        private DevExpress.XtraEditors.SimpleButton btn_upload;
    }
}