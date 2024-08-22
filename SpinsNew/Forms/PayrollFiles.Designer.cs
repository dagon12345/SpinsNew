﻿
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
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_municipality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.list_pictures = new DevExpress.XtraEditors.ListBoxControl();
            this.cmb_selectfolder = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
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
            this.groupControl1.Controls.Add(this.btn_search);
            this.groupControl1.Controls.Add(this.cmb_period);
            this.groupControl1.Controls.Add(this.cmb_year);
            this.groupControl1.Controls.Add(this.cmb_municipality);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(220, 132);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Filter";
            // 
            // lblPeriod
            // 
            this.lblPeriod.Appearance.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPeriod.Appearance.Options.UseFont = true;
            this.lblPeriod.Location = new System.Drawing.Point(180, 104);
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
            this.lbl_municipality.Location = new System.Drawing.Point(56, 5);
            this.lbl_municipality.Name = "lbl_municipality";
            this.lbl_municipality.Size = new System.Drawing.Size(24, 15);
            this.lbl_municipality.TabIndex = 82;
            this.lbl_municipality.Text = "Mun";
            this.lbl_municipality.Visible = false;
            // 
            // btn_search
            // 
            this.btn_search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.ImageOptions.Image")));
            this.btn_search.Location = new System.Drawing.Point(5, 104);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(123, 22);
            this.btn_search.TabIndex = 5;
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
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
            this.groupControl2.Location = new System.Drawing.Point(12, 150);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(220, 462);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Files";
            // 
            // list_pictures
            // 
            this.list_pictures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_pictures.Location = new System.Drawing.Point(5, 52);
            this.list_pictures.Name = "list_pictures";
            this.list_pictures.Size = new System.Drawing.Size(210, 405);
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
            this.groupControl3.Controls.Add(this.pictureEdit1);
            this.groupControl3.Location = new System.Drawing.Point(238, 12);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(726, 600);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "Image";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.Location = new System.Drawing.Point(5, 29);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(716, 566);
            this.pictureEdit1.TabIndex = 0;
            // 
            // PayrollFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 624);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
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
        public DevExpress.XtraEditors.SimpleButton btn_search;
        private DevExpress.XtraEditors.LabelControl lbl_municipality;
        private DevExpress.XtraEditors.LabelControl lblPeriod;
    }
}