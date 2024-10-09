
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payroll));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btn_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.gb_details = new System.Windows.Forms.GroupBox();
            this.rbAllStatus = new System.Windows.Forms.RadioButton();
            this.cmb_municipality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rbUnclaimed = new System.Windows.Forms.RadioButton();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_barangay = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.gb_Update = new DevExpress.XtraEditors.GroupControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ck_all = new DevExpress.XtraEditors.CheckEdit();
            this.txt_remarks = new DevExpress.XtraEditors.TextEdit();
            this.cmb_claimtype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btn_unclaimed = new DevExpress.XtraEditors.SimpleButton();
            this.btn_claimed = new DevExpress.XtraEditors.SimpleButton();
            this.dt_to = new DevExpress.XtraEditors.DateEdit();
            this.dt_from = new DevExpress.XtraEditors.DateEdit();
            this.groupControlPayroll = new DevExpress.XtraEditors.GroupControl();
            this.btnArchive = new DevExpress.XtraEditors.SimpleButton();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.payrollViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbl_payroll_socpenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newApplicantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAttachmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printCertificateOfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claimedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unclaimedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.panel_spinner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.gb_details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_barangay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gb_Update)).BeginInit();
            this.gb_Update.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ck_all.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_remarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_claimtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).BeginInit();
            this.groupControlPayroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_payroll_socpenBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.panel_spinner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnSearch);
            this.groupControl1.Controls.Add(this.btn_refresh);
            this.groupControl1.Controls.Add(this.gb_details);
            this.groupControl1.Controls.Add(this.gb_Update);
            this.groupControl1.Location = new System.Drawing.Point(10, 31);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(838, 247);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Select";
            // 
            // btnSearch
            // 
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.Location = new System.Drawing.Point(39, 183);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search payroll";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_refresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.ImageOptions.Image")));
            this.btn_refresh.Location = new System.Drawing.Point(39, 212);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(121, 23);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // gb_details
            // 
            this.gb_details.Controls.Add(this.rbAllStatus);
            this.gb_details.Controls.Add(this.cmb_municipality);
            this.gb_details.Controls.Add(this.rbUnclaimed);
            this.gb_details.Controls.Add(this.cmb_year);
            this.gb_details.Controls.Add(this.cmb_period);
            this.gb_details.Controls.Add(this.cmb_barangay);
            this.gb_details.Location = new System.Drawing.Point(16, 24);
            this.gb_details.Name = "gb_details";
            this.gb_details.Size = new System.Drawing.Size(341, 153);
            this.gb_details.TabIndex = 25;
            this.gb_details.TabStop = false;
            // 
            // rbAllStatus
            // 
            this.rbAllStatus.AutoSize = true;
            this.rbAllStatus.Checked = true;
            this.rbAllStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllStatus.Location = new System.Drawing.Point(23, 14);
            this.rbAllStatus.Name = "rbAllStatus";
            this.rbAllStatus.Size = new System.Drawing.Size(63, 17);
            this.rbAllStatus.TabIndex = 21;
            this.rbAllStatus.TabStop = true;
            this.rbAllStatus.Text = "Default";
            this.rbAllStatus.UseVisualStyleBackColor = true;
            this.rbAllStatus.CheckedChanged += new System.EventHandler(this.rbAllStatus_CheckedChangedAsync);
            // 
            // cmb_municipality
            // 
            this.cmb_municipality.EditValue = "Select City/Municipality";
            this.cmb_municipality.Location = new System.Drawing.Point(23, 39);
            this.cmb_municipality.Name = "cmb_municipality";
            this.cmb_municipality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_municipality.Properties.DropDownRows = 20;
            this.cmb_municipality.Properties.HideSelection = false;
            this.cmb_municipality.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            this.cmb_municipality.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_municipality.Size = new System.Drawing.Size(303, 20);
            this.cmb_municipality.TabIndex = 0;
            this.cmb_municipality.SelectedIndexChanged += new System.EventHandler(this.cmb_municipality_SelectedIndexChanged);
            // 
            // rbUnclaimed
            // 
            this.rbUnclaimed.AutoSize = true;
            this.rbUnclaimed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUnclaimed.Location = new System.Drawing.Point(92, 14);
            this.rbUnclaimed.Name = "rbUnclaimed";
            this.rbUnclaimed.Size = new System.Drawing.Size(63, 17);
            this.rbUnclaimed.TabIndex = 21;
            this.rbUnclaimed.Text = "Unpaid";
            this.rbUnclaimed.UseVisualStyleBackColor = true;
            this.rbUnclaimed.CheckedChanged += new System.EventHandler(this.rbUnclaimed_CheckedChanged);
            // 
            // cmb_year
            // 
            this.cmb_year.EditValue = "Select Year";
            this.cmb_year.Location = new System.Drawing.Point(23, 93);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_year.Properties.DropDownRows = 15;
            this.cmb_year.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_year.Size = new System.Drawing.Size(303, 20);
            this.cmb_year.TabIndex = 2;
            this.cmb_year.SelectedIndexChanged += new System.EventHandler(this.cmb_year_SelectedIndexChanged);
            // 
            // cmb_period
            // 
            this.cmb_period.EditValue = "Select Period";
            this.cmb_period.Location = new System.Drawing.Point(23, 122);
            this.cmb_period.Name = "cmb_period";
            this.cmb_period.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_period.Size = new System.Drawing.Size(303, 20);
            this.cmb_period.TabIndex = 3;
            this.cmb_period.SelectedIndexChanged += new System.EventHandler(this.cmb_period_SelectedIndexChanged);
            this.cmb_period.Click += new System.EventHandler(this.cmb_period_Click);
            this.cmb_period.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmb_period_MouseClick);
            // 
            // cmb_barangay
            // 
            this.cmb_barangay.EditValue = "Select Barangay";
            this.cmb_barangay.Location = new System.Drawing.Point(23, 67);
            this.cmb_barangay.Name = "cmb_barangay";
            this.cmb_barangay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_barangay.Properties.DropDownRows = 15;
            this.cmb_barangay.Properties.HideSelection = false;
            this.cmb_barangay.Size = new System.Drawing.Size(303, 20);
            this.cmb_barangay.TabIndex = 1;
            this.cmb_barangay.EditValueChanged += new System.EventHandler(this.cmb_barangay_EditValueChanged);
            // 
            // gb_Update
            // 
            this.gb_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Update.Controls.Add(this.labelControl6);
            this.gb_Update.Controls.Add(this.labelControl5);
            this.gb_Update.Controls.Add(this.labelControl4);
            this.gb_Update.Controls.Add(this.labelControl3);
            this.gb_Update.Controls.Add(this.labelControl1);
            this.gb_Update.Controls.Add(this.ck_all);
            this.gb_Update.Controls.Add(this.txt_remarks);
            this.gb_Update.Controls.Add(this.cmb_claimtype);
            this.gb_Update.Controls.Add(this.btn_unclaimed);
            this.gb_Update.Controls.Add(this.btn_claimed);
            this.gb_Update.Controls.Add(this.dt_to);
            this.gb_Update.Controls.Add(this.dt_from);
            this.gb_Update.Location = new System.Drawing.Point(512, 24);
            this.gb_Update.Name = "gb_Update";
            this.gb_Update.Size = new System.Drawing.Size(321, 195);
            this.gb_Update.TabIndex = 24;
            this.gb_Update.Text = "Update Payroll Status";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(27, 125);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "Remarks:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 98);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Claim type:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(56, 72);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(16, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "To:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(44, 46);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "From:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(76, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Select Dates:";
            // 
            // ck_all
            // 
            this.ck_all.Location = new System.Drawing.Point(76, 144);
            this.ck_all.Name = "ck_all";
            this.ck_all.Properties.Caption = "Update all";
            this.ck_all.Size = new System.Drawing.Size(75, 20);
            this.ck_all.TabIndex = 5;
            // 
            // txt_remarks
            // 
            this.txt_remarks.EditValue = "";
            this.txt_remarks.Location = new System.Drawing.Point(76, 121);
            this.txt_remarks.Name = "txt_remarks";
            this.txt_remarks.Size = new System.Drawing.Size(225, 20);
            this.txt_remarks.TabIndex = 3;
            // 
            // cmb_claimtype
            // 
            this.cmb_claimtype.EditValue = "";
            this.cmb_claimtype.Location = new System.Drawing.Point(76, 95);
            this.cmb_claimtype.Name = "cmb_claimtype";
            this.cmb_claimtype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_claimtype.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_claimtype.Size = new System.Drawing.Size(225, 20);
            this.cmb_claimtype.TabIndex = 2;
            // 
            // btn_unclaimed
            // 
            this.btn_unclaimed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_unclaimed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_unclaimed.ImageOptions.Image")));
            this.btn_unclaimed.Location = new System.Drawing.Point(167, 170);
            this.btn_unclaimed.Name = "btn_unclaimed";
            this.btn_unclaimed.Size = new System.Drawing.Size(85, 23);
            this.btn_unclaimed.TabIndex = 5;
            this.btn_unclaimed.Text = "Unclaimed";
            this.btn_unclaimed.Click += new System.EventHandler(this.btn_unclaimed_Click);
            // 
            // btn_claimed
            // 
            this.btn_claimed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_claimed.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_claimed.ImageOptions.Image")));
            this.btn_claimed.Location = new System.Drawing.Point(76, 170);
            this.btn_claimed.Name = "btn_claimed";
            this.btn_claimed.Size = new System.Drawing.Size(85, 23);
            this.btn_claimed.TabIndex = 4;
            this.btn_claimed.Text = "Claimed";
            this.btn_claimed.Click += new System.EventHandler(this.btn_claimed_Click);
            // 
            // dt_to
            // 
            this.dt_to.EditValue = null;
            this.dt_to.Location = new System.Drawing.Point(76, 69);
            this.dt_to.Name = "dt_to";
            this.dt_to.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_to.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_to.Size = new System.Drawing.Size(225, 20);
            this.dt_to.TabIndex = 1;
            // 
            // dt_from
            // 
            this.dt_from.EditValue = null;
            this.dt_from.Location = new System.Drawing.Point(76, 43);
            this.dt_from.Name = "dt_from";
            this.dt_from.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_from.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_from.Size = new System.Drawing.Size(225, 20);
            this.dt_from.TabIndex = 0;
            this.dt_from.EditValueChanged += new System.EventHandler(this.dt_from_EditValueChanged);
            // 
            // groupControlPayroll
            // 
            this.groupControlPayroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlPayroll.Controls.Add(this.btnArchive);
            this.groupControlPayroll.Controls.Add(this.searchControl1);
            this.groupControlPayroll.Controls.Add(this.gridControl1);
            this.groupControlPayroll.Location = new System.Drawing.Point(10, 325);
            this.groupControlPayroll.Name = "groupControlPayroll";
            this.groupControlPayroll.Size = new System.Drawing.Size(838, 277);
            this.groupControlPayroll.TabIndex = 2;
            this.groupControlPayroll.Text = "Payroll Created";
            // 
            // btnArchive
            // 
            this.btnArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArchive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnArchive.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnArchive.ImageOptions.Image")));
            this.btnArchive.Location = new System.Drawing.Point(723, 26);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(115, 23);
            this.btnArchive.TabIndex = 9;
            this.btnArchive.Text = "Archive";
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(16, 33);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.FindDelay = 500;
            this.searchControl1.Size = new System.Drawing.Size(302, 20);
            this.searchControl1.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gridControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(16, 66);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(817, 206);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.FindDelay = 500;
            this.gridView1.OptionsFind.FindFilterColumns = "FindFilterColumns = \'LastName;FirstName;MiddleName\'";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // payrollViewModelBindingSource
            // 
            this.payrollViewModelBindingSource.DataSource = typeof(SpinsNew.ViewModel.PayrollViewModel);
            // 
            // tbl_payroll_socpenBindingSource
            // 
            this.tbl_payroll_socpenBindingSource.DataSource = typeof(SpinsNew.Models.PayrollModel);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.uploadToolStripMenuItem,
            this.ts_delete});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newApplicantToolStripMenuItem,
            this.viewAttachmentsToolStripMenuItem,
            this.printCertificateOfToolStripMenuItem});
            this.optionToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_menu_vertical_48;
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.optionToolStripMenuItem.Text = "Option";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // newApplicantToolStripMenuItem
            // 
            this.newApplicantToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_payroll_64;
            this.newApplicantToolStripMenuItem.Name = "newApplicantToolStripMenuItem";
            this.newApplicantToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.newApplicantToolStripMenuItem.Text = "Print payroll";
            this.newApplicantToolStripMenuItem.Click += new System.EventHandler(this.newApplicantToolStripMenuItem_Click);
            // 
            // viewAttachmentsToolStripMenuItem
            // 
            this.viewAttachmentsToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_attachment_16;
            this.viewAttachmentsToolStripMenuItem.Name = "viewAttachmentsToolStripMenuItem";
            this.viewAttachmentsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewAttachmentsToolStripMenuItem.Text = "View Attachments";
            this.viewAttachmentsToolStripMenuItem.Click += new System.EventHandler(this.viewAttachmentsToolStripMenuItem_Click);
            // 
            // printCertificateOfToolStripMenuItem
            // 
            this.printCertificateOfToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.claimedToolStripMenuItem,
            this.unclaimedToolStripMenuItem});
            this.printCertificateOfToolStripMenuItem.Image = global::SpinsNew.Properties.Resources._4212933_achievement_award_certificate_degrees_icon;
            this.printCertificateOfToolStripMenuItem.Name = "printCertificateOfToolStripMenuItem";
            this.printCertificateOfToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.printCertificateOfToolStripMenuItem.Text = "Print Certificate of Eligibility";
            // 
            // claimedToolStripMenuItem
            // 
            this.claimedToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_verify_48;
            this.claimedToolStripMenuItem.Name = "claimedToolStripMenuItem";
            this.claimedToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.claimedToolStripMenuItem.Text = "Regular";
            this.claimedToolStripMenuItem.Click += new System.EventHandler(this.claimedToolStripMenuItem_Click);
            // 
            // unclaimedToolStripMenuItem
            // 
            this.unclaimedToolStripMenuItem.Image = global::SpinsNew.Properties.Resources._299112_warning_shield_icon;
            this.unclaimedToolStripMenuItem.Name = "unclaimedToolStripMenuItem";
            this.unclaimedToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.unclaimedToolStripMenuItem.Text = "Unpaid";
            this.unclaimedToolStripMenuItem.Click += new System.EventHandler(this.unclaimedToolStripMenuItem_Click);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Image = global::SpinsNew.Properties.Resources._8725982_image_upload_icon;
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(144, 20);
            this.uploadToolStripMenuItem.Text = "Upload Attachments";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // ts_delete
            // 
            this.ts_delete.Image = global::SpinsNew.Properties.Resources.icons8_delete_48;
            this.ts_delete.Name = "ts_delete";
            this.ts_delete.Size = new System.Drawing.Size(88, 20);
            this.ts_delete.Text = " Delete All";
            this.ts_delete.Click += new System.EventHandler(this.ts_delete_Click);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(9, 309);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(838, 10);
            this.progressBarControl1.TabIndex = 12;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(733, 280);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(115, 23);
            this.btnExport.TabIndex = 23;
            this.btnExport.Text = "Export to excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel_spinner
            // 
            this.panel_spinner.BackColor = System.Drawing.Color.Transparent;
            this.panel_spinner.Controls.Add(this.pictureBox1);
            this.panel_spinner.Controls.Add(this.labelControl2);
            this.panel_spinner.Location = new System.Drawing.Point(9, 283);
            this.panel_spinner.Name = "panel_spinner";
            this.panel_spinner.Size = new System.Drawing.Size(278, 22);
            this.panel_spinner.TabIndex = 24;
            this.panel_spinner.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SpinsNew.Properties.Resources.triangle_spinner;
            this.pictureBox1.Location = new System.Drawing.Point(-5, -11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(71, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(160, 17);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "Processing, please wait.......";
            // 
            // Payroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(859, 612);
            this.Controls.Add(this.panel_spinner);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupControlPayroll);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Payroll";
            this.Text = "Payroll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Payroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.gb_details.ResumeLayout(false);
            this.gb_details.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_barangay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gb_Update)).EndInit();
            this.gb_Update.ResumeLayout(false);
            this.gb_Update.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ck_all.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_remarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_claimtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).EndInit();
            this.groupControlPayroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_payroll_socpenBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.panel_spinner.ResumeLayout(false);
            this.panel_spinner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControlPayroll;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_period;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_year;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_municipality;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newApplicantToolStripMenuItem;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.ToolStripMenuItem viewAttachmentsToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl gb_Update;
        private DevExpress.XtraEditors.DateEdit dt_from;
        private DevExpress.XtraEditors.DateEdit dt_to;
        private DevExpress.XtraEditors.SimpleButton btn_unclaimed;
        private DevExpress.XtraEditors.SimpleButton btn_claimed;
        private DevExpress.XtraEditors.CheckEdit ck_all;
        private DevExpress.XtraEditors.TextEdit txt_remarks;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_claimtype;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ToolStripMenuItem ts_delete;
        public System.Windows.Forms.RadioButton rbUnclaimed;
        public System.Windows.Forms.RadioButton rbAllStatus;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.BindingSource tbl_payroll_socpenBindingSource;
        private System.Windows.Forms.BindingSource payrollViewModelBindingSource;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.GroupBox gb_details;
        private DevExpress.XtraEditors.SimpleButton btn_refresh;
        private DevExpress.XtraEditors.SimpleButton btnArchive;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmb_barangay;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.ToolStripMenuItem printCertificateOfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claimedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unclaimedToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.Panel panel_spinner;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}