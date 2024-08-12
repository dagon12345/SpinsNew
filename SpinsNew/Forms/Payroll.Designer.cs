﻿
namespace SpinsNew.Forms
{
    partial class Payroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payroll));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblValue = new DevExpress.XtraEditors.LabelControl();
            this.rbUnclaimed = new System.Windows.Forms.RadioButton();
            this.rbAllStatus = new System.Windows.Forms.RadioButton();
            this.rbClaimed = new System.Windows.Forms.RadioButton();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_municipality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.panel_spinner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlPayroll = new DevExpress.XtraEditors.GroupControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.gridPayroll = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newApplicantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).BeginInit();
            this.panel_spinner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).BeginInit();
            this.groupControlPayroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.lblValue);
            this.groupControl1.Controls.Add(this.rbUnclaimed);
            this.groupControl1.Controls.Add(this.rbAllStatus);
            this.groupControl1.Controls.Add(this.rbClaimed);
            this.groupControl1.Controls.Add(this.cmb_period);
            this.groupControl1.Controls.Add(this.cmb_year);
            this.groupControl1.Controls.Add(this.cmb_municipality);
            this.groupControl1.Controls.Add(this.btn_search);
            this.groupControl1.Location = new System.Drawing.Point(10, 31);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(838, 86);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Select";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(270, 61);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(26, 13);
            this.lblValue.TabIndex = 22;
            this.lblValue.Text = "value";
            this.lblValue.Visible = false;
            // 
            // rbUnclaimed
            // 
            this.rbUnclaimed.AutoSize = true;
            this.rbUnclaimed.Location = new System.Drawing.Point(160, 59);
            this.rbUnclaimed.Name = "rbUnclaimed";
            this.rbUnclaimed.Size = new System.Drawing.Size(73, 17);
            this.rbUnclaimed.TabIndex = 21;
            this.rbUnclaimed.Text = "Unclaimed";
            this.rbUnclaimed.UseVisualStyleBackColor = true;
            this.rbUnclaimed.CheckedChanged += new System.EventHandler(this.rbUnclaimed_CheckedChanged);
            // 
            // rbAllStatus
            // 
            this.rbAllStatus.AutoSize = true;
            this.rbAllStatus.Checked = true;
            this.rbAllStatus.Location = new System.Drawing.Point(17, 59);
            this.rbAllStatus.Name = "rbAllStatus";
            this.rbAllStatus.Size = new System.Drawing.Size(69, 17);
            this.rbAllStatus.TabIndex = 21;
            this.rbAllStatus.TabStop = true;
            this.rbAllStatus.Text = "All status";
            this.rbAllStatus.UseVisualStyleBackColor = true;
            this.rbAllStatus.CheckedChanged += new System.EventHandler(this.rbAllStatus_CheckedChangedAsync);
            // 
            // rbClaimed
            // 
            this.rbClaimed.AutoSize = true;
            this.rbClaimed.Location = new System.Drawing.Point(92, 59);
            this.rbClaimed.Name = "rbClaimed";
            this.rbClaimed.Size = new System.Drawing.Size(62, 17);
            this.rbClaimed.TabIndex = 21;
            this.rbClaimed.Text = "Claimed";
            this.rbClaimed.UseVisualStyleBackColor = true;
            this.rbClaimed.CheckedChanged += new System.EventHandler(this.rbClaimed_CheckedChanged);
            // 
            // cmb_period
            // 
            this.cmb_period.EditValue = "Select Period";
            this.cmb_period.Location = new System.Drawing.Point(439, 34);
            this.cmb_period.Name = "cmb_period";
            this.cmb_period.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_period.Size = new System.Drawing.Size(132, 20);
            this.cmb_period.TabIndex = 20;
            // 
            // cmb_year
            // 
            this.cmb_year.EditValue = "Select Year";
            this.cmb_year.Location = new System.Drawing.Point(302, 34);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_year.Properties.DropDownRows = 15;
            this.cmb_year.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_year.Size = new System.Drawing.Size(132, 20);
            this.cmb_year.TabIndex = 19;
            this.cmb_year.SelectedIndexChanged += new System.EventHandler(this.cmb_year_SelectedIndexChanged);
            // 
            // cmb_municipality
            // 
            this.cmb_municipality.EditValue = "Select City/Municipality";
            this.cmb_municipality.Location = new System.Drawing.Point(17, 34);
            this.cmb_municipality.Name = "cmb_municipality";
            this.cmb_municipality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_municipality.Properties.DropDownRows = 20;
            this.cmb_municipality.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_municipality.Size = new System.Drawing.Size(279, 20);
            this.cmb_municipality.TabIndex = 18;
            this.cmb_municipality.SelectedIndexChanged += new System.EventHandler(this.cmb_municipality_SelectedIndexChanged);
            // 
            // btn_search
            // 
            this.btn_search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.ImageOptions.Image")));
            this.btn_search.Location = new System.Drawing.Point(588, 30);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(123, 29);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "Search";
            this.btn_search.Click += new System.EventHandler(this.btn_search_ClickAsync);
            // 
            // panel_spinner
            // 
            this.panel_spinner.Controls.Add(this.pictureBox1);
            this.panel_spinner.Controls.Add(this.labelControl2);
            this.panel_spinner.Location = new System.Drawing.Point(10, 120);
            this.panel_spinner.Name = "panel_spinner";
            this.panel_spinner.Size = new System.Drawing.Size(177, 19);
            this.panel_spinner.TabIndex = 17;
            this.panel_spinner.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SpinsNew.Properties.Resources.spinner;
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(29, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(138, 13);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "Retrieving, please wait.......";
            // 
            // groupControlPayroll
            // 
            this.groupControlPayroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlPayroll.Controls.Add(this.searchControl1);
            this.groupControlPayroll.Controls.Add(this.gridPayroll);
            this.groupControlPayroll.Location = new System.Drawing.Point(10, 159);
            this.groupControlPayroll.Name = "groupControlPayroll";
            this.groupControlPayroll.Size = new System.Drawing.Size(838, 443);
            this.groupControlPayroll.TabIndex = 2;
            this.groupControlPayroll.Text = "Payroll Created";
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(17, 29);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(279, 20);
            this.searchControl1.TabIndex = 2;
            // 
            // gridPayroll
            // 
            this.gridPayroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPayroll.Location = new System.Drawing.Point(17, 60);
            this.gridPayroll.MainView = this.gridView1;
            this.gridPayroll.Name = "gridPayroll";
            this.gridPayroll.Size = new System.Drawing.Size(805, 378);
            this.gridPayroll.TabIndex = 1;
            this.gridPayroll.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 303;
            this.gridView1.GridControl = this.gridPayroll;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsFind.FindFilterColumns = "LastName;FirstName;MiddleName";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newApplicantToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // newApplicantToolStripMenuItem
            // 
            this.newApplicantToolStripMenuItem.Name = "newApplicantToolStripMenuItem";
            this.newApplicantToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newApplicantToolStripMenuItem.Text = "Print payroll";
            this.newApplicantToolStripMenuItem.Click += new System.EventHandler(this.newApplicantToolStripMenuItem_Click);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(10, 144);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(838, 10);
            this.progressBarControl1.TabIndex = 12;
            // 
            // Payroll
            // 
            this.AcceptButton = this.btn_search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 612);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupControlPayroll);
            this.Controls.Add(this.panel_spinner);
            this.Controls.Add(this.groupControl1);
            this.Name = "Payroll";
            this.Text = "Payroll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Payroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).EndInit();
            this.panel_spinner.ResumeLayout(false);
            this.panel_spinner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).EndInit();
            this.groupControlPayroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Panel panel_spinner;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.SimpleButton btn_search;
        private DevExpress.XtraEditors.GroupControl groupControlPayroll;
        private DevExpress.XtraGrid.GridControl gridPayroll;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_period;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_year;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_municipality;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newApplicantToolStripMenuItem;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblValue;
        public System.Windows.Forms.RadioButton rbAllStatus;
        public System.Windows.Forms.RadioButton rbUnclaimed;
        public System.Windows.Forms.RadioButton rbClaimed;
    }
}