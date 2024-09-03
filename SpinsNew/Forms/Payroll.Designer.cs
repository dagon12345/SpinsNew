
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
            this.btn_refresh = new DevExpress.XtraEditors.SimpleButton();
            this.gb_details = new System.Windows.Forms.GroupBox();
            this.rbAllStatus = new System.Windows.Forms.RadioButton();
            this.cmb_municipality = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rbUnclaimed = new System.Windows.Forms.RadioButton();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
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
            this.panel_spinner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControlPayroll = new DevExpress.XtraEditors.GroupControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridPayroll = new DevExpress.XtraGrid.GridControl();
            this.payrollViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView0 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMasterListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVerified = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarangay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBirthDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHealthStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHealthStatusRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPSGCBrgy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPSGCCityMun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPSGCProvince = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPSGCRegion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeriodID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollStatusID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClaimTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateClaimedFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateClaimedTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollTagID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentModeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModified = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTimeModified = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTimeEntry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntryBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tbl_payroll_socpenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newApplicantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAttachmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.gb_details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ck_all.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_remarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_claimtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties)).BeginInit();
            this.panel_spinner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).BeginInit();
            this.groupControlPayroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_payroll_socpenBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btn_refresh);
            this.groupControl1.Controls.Add(this.gb_details);
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Location = new System.Drawing.Point(10, 31);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(838, 226);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Select";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_refresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.ImageOptions.Image")));
            this.btn_refresh.Location = new System.Drawing.Point(16, 196);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(85, 23);
            this.btn_refresh.TabIndex = 9;
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
            this.gb_details.Location = new System.Drawing.Point(16, 26);
            this.gb_details.Name = "gb_details";
            this.gb_details.Size = new System.Drawing.Size(341, 164);
            this.gb_details.TabIndex = 25;
            this.gb_details.TabStop = false;
            // 
            // rbAllStatus
            // 
            this.rbAllStatus.AutoSize = true;
            this.rbAllStatus.Checked = true;
            this.rbAllStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllStatus.Location = new System.Drawing.Point(22, 29);
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
            this.cmb_municipality.Location = new System.Drawing.Point(22, 54);
            this.cmb_municipality.Name = "cmb_municipality";
            this.cmb_municipality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_municipality.Properties.DropDownRows = 20;
            this.cmb_municipality.Properties.HideSelection = false;
            this.cmb_municipality.Properties.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            this.cmb_municipality.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_municipality.Size = new System.Drawing.Size(303, 20);
            this.cmb_municipality.TabIndex = 18;
            this.cmb_municipality.SelectedIndexChanged += new System.EventHandler(this.cmb_municipality_SelectedIndexChanged);
            // 
            // rbUnclaimed
            // 
            this.rbUnclaimed.AutoSize = true;
            this.rbUnclaimed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUnclaimed.Location = new System.Drawing.Point(91, 29);
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
            this.cmb_year.Location = new System.Drawing.Point(21, 90);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_year.Properties.DropDownRows = 15;
            this.cmb_year.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_year.Size = new System.Drawing.Size(303, 20);
            this.cmb_year.TabIndex = 19;
            this.cmb_year.SelectedIndexChanged += new System.EventHandler(this.cmb_year_SelectedIndexChanged);
            // 
            // cmb_period
            // 
            this.cmb_period.EditValue = "Select Period";
            this.cmb_period.Location = new System.Drawing.Point(22, 126);
            this.cmb_period.Name = "cmb_period";
            this.cmb_period.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_period.Size = new System.Drawing.Size(303, 20);
            this.cmb_period.TabIndex = 20;
            this.cmb_period.SelectedIndexChanged += new System.EventHandler(this.cmb_period_SelectedIndexChanged);
            this.cmb_period.Click += new System.EventHandler(this.cmb_period_Click);
            this.cmb_period.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmb_period_MouseClick);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.ck_all);
            this.groupControl2.Controls.Add(this.txt_remarks);
            this.groupControl2.Controls.Add(this.cmb_claimtype);
            this.groupControl2.Controls.Add(this.btn_unclaimed);
            this.groupControl2.Controls.Add(this.btn_claimed);
            this.groupControl2.Controls.Add(this.dt_to);
            this.groupControl2.Controls.Add(this.dt_from);
            this.groupControl2.Location = new System.Drawing.Point(363, 26);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(332, 195);
            this.groupControl2.TabIndex = 24;
            this.groupControl2.Text = "Update Payroll Status";
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
            // panel_spinner
            // 
            this.panel_spinner.Controls.Add(this.pictureBox1);
            this.panel_spinner.Controls.Add(this.labelControl2);
            this.panel_spinner.Location = new System.Drawing.Point(10, 263);
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
            this.groupControlPayroll.Controls.Add(this.gridControl1);
            this.groupControlPayroll.Controls.Add(this.gridPayroll);
            this.groupControlPayroll.Location = new System.Drawing.Point(10, 304);
            this.groupControlPayroll.Name = "groupControlPayroll";
            this.groupControlPayroll.Size = new System.Drawing.Size(838, 298);
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
            this.gridControl1.Location = new System.Drawing.Point(16, 55);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(817, 238);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.FindDelay = 500;
            this.gridView1.OptionsFind.FindFilterColumns = "FindFilterColumns = \'LastName;FirstName;MiddleName\'";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridPayroll
            // 
            this.gridPayroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPayroll.DataSource = this.payrollViewModelBindingSource;
            this.gridPayroll.Location = new System.Drawing.Point(386, 26);
            this.gridPayroll.MainView = this.gridView0;
            this.gridPayroll.Name = "gridPayroll";
            this.gridPayroll.Size = new System.Drawing.Size(447, 45);
            this.gridPayroll.TabIndex = 1;
            this.gridPayroll.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView0});
            this.gridPayroll.Visible = false;
            this.gridPayroll.Click += new System.EventHandler(this.gridPayroll_Click);
            // 
            // payrollViewModelBindingSource
            // 
            this.payrollViewModelBindingSource.DataSource = typeof(SpinsNew.ViewModel.PayrollViewModel);
            // 
            // gridView0
            // 
            this.gridView0.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colMasterListID,
            this.colVerified,
            this.colFullName,
            this.colBarangay,
            this.colAddress,
            this.colBirthDate,
            this.colSex,
            this.colHealthStatus,
            this.colHealthStatusRemarks,
            this.colIdType,
            this.colPSGCBrgy,
            this.colPSGCCityMun,
            this.colPSGCProvince,
            this.colPSGCRegion,
            this.colAmount,
            this.colYear,
            this.colPeriodID,
            this.colPeriod,
            this.colPayrollStatusID,
            this.colClaimTypeID,
            this.colPayrollStatus,
            this.colDateClaimedFrom,
            this.colDateClaimedTo,
            this.colPayrollTypeID,
            this.colType,
            this.colPayrollTagID,
            this.colTag,
            this.colStatus,
            this.colPaymentModeID,
            this.colPaymentMode,
            this.colRemarks,
            this.colModified,
            this.colCreated,
            this.colModifiedBy,
            this.colDateTimeModified,
            this.colDateTimeEntry,
            this.colEntryBy});
            this.gridView0.DetailHeight = 303;
            this.gridView0.GridControl = this.gridPayroll;
            this.gridView0.Name = "gridView0";
            this.gridView0.OptionsBehavior.Editable = false;
            this.gridView0.OptionsCustomization.AllowGroup = false;
            this.gridView0.OptionsFind.FindFilterColumns = "LastName;FirstName;MiddleName";
            this.gridView0.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView0.OptionsView.ColumnAutoWidth = false;
            this.gridView0.OptionsView.ShowGroupPanel = false;
            this.gridView0.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colPSGCBrgy, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 39;
            // 
            // colMasterListID
            // 
            this.colMasterListID.FieldName = "MasterListID";
            this.colMasterListID.Name = "colMasterListID";
            this.colMasterListID.Width = 37;
            // 
            // colVerified
            // 
            this.colVerified.FieldName = "Verified";
            this.colVerified.Name = "colVerified";
            this.colVerified.Visible = true;
            this.colVerified.VisibleIndex = 0;
            // 
            // colFullName
            // 
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 67;
            // 
            // colBarangay
            // 
            this.colBarangay.FieldName = "Barangay";
            this.colBarangay.Name = "colBarangay";
            this.colBarangay.Visible = true;
            this.colBarangay.VisibleIndex = 2;
            // 
            // colAddress
            // 
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 3;
            this.colAddress.Width = 76;
            // 
            // colBirthDate
            // 
            this.colBirthDate.DisplayFormat.FormatString = "MMM-dd-yyyy";
            this.colBirthDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colBirthDate.FieldName = "BirthDate";
            this.colBirthDate.Name = "colBirthDate";
            this.colBirthDate.Visible = true;
            this.colBirthDate.VisibleIndex = 4;
            // 
            // colSex
            // 
            this.colSex.FieldName = "Sex";
            this.colSex.Name = "colSex";
            this.colSex.Visible = true;
            this.colSex.VisibleIndex = 5;
            // 
            // colHealthStatus
            // 
            this.colHealthStatus.FieldName = "HealthStatus";
            this.colHealthStatus.Name = "colHealthStatus";
            this.colHealthStatus.Visible = true;
            this.colHealthStatus.VisibleIndex = 6;
            // 
            // colHealthStatusRemarks
            // 
            this.colHealthStatusRemarks.FieldName = "HealthStatusRemarks";
            this.colHealthStatusRemarks.Name = "colHealthStatusRemarks";
            this.colHealthStatusRemarks.Visible = true;
            this.colHealthStatusRemarks.VisibleIndex = 7;
            // 
            // colIdType
            // 
            this.colIdType.FieldName = "IdType";
            this.colIdType.Name = "colIdType";
            this.colIdType.Visible = true;
            this.colIdType.VisibleIndex = 8;
            // 
            // colPSGCBrgy
            // 
            this.colPSGCBrgy.FieldName = "PSGCBrgy";
            this.colPSGCBrgy.Name = "colPSGCBrgy";
            this.colPSGCBrgy.Width = 84;
            // 
            // colPSGCCityMun
            // 
            this.colPSGCCityMun.FieldName = "PSGCCityMun";
            this.colPSGCCityMun.Name = "colPSGCCityMun";
            this.colPSGCCityMun.Width = 37;
            // 
            // colPSGCProvince
            // 
            this.colPSGCProvince.FieldName = "PSGCProvince";
            this.colPSGCProvince.Name = "colPSGCProvince";
            this.colPSGCProvince.Width = 37;
            // 
            // colPSGCRegion
            // 
            this.colPSGCRegion.FieldName = "PSGCRegion";
            this.colPSGCRegion.Name = "colPSGCRegion";
            this.colPSGCRegion.Width = 37;
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 9;
            this.colAmount.Width = 37;
            // 
            // colYear
            // 
            this.colYear.FieldName = "Year";
            this.colYear.Name = "colYear";
            this.colYear.Visible = true;
            this.colYear.VisibleIndex = 10;
            this.colYear.Width = 37;
            // 
            // colPeriodID
            // 
            this.colPeriodID.FieldName = "PeriodID";
            this.colPeriodID.Name = "colPeriodID";
            this.colPeriodID.Width = 35;
            // 
            // colPeriod
            // 
            this.colPeriod.FieldName = "Period";
            this.colPeriod.Name = "colPeriod";
            this.colPeriod.Visible = true;
            this.colPeriod.VisibleIndex = 11;
            // 
            // colPayrollStatusID
            // 
            this.colPayrollStatusID.FieldName = "PayrollStatusID";
            this.colPayrollStatusID.Name = "colPayrollStatusID";
            this.colPayrollStatusID.Width = 37;
            // 
            // colClaimTypeID
            // 
            this.colClaimTypeID.FieldName = "ClaimTypeID";
            this.colClaimTypeID.Name = "colClaimTypeID";
            this.colClaimTypeID.Width = 37;
            // 
            // colPayrollStatus
            // 
            this.colPayrollStatus.FieldName = "PayrollStatus";
            this.colPayrollStatus.Name = "colPayrollStatus";
            this.colPayrollStatus.Visible = true;
            this.colPayrollStatus.VisibleIndex = 12;
            // 
            // colDateClaimedFrom
            // 
            this.colDateClaimedFrom.DisplayFormat.FormatString = "MMM-dd-yyyy";
            this.colDateClaimedFrom.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateClaimedFrom.FieldName = "DateClaimedFrom";
            this.colDateClaimedFrom.Name = "colDateClaimedFrom";
            this.colDateClaimedFrom.Visible = true;
            this.colDateClaimedFrom.VisibleIndex = 13;
            this.colDateClaimedFrom.Width = 20;
            // 
            // colDateClaimedTo
            // 
            this.colDateClaimedTo.FieldName = "DateClaimedTo";
            this.colDateClaimedTo.Name = "colDateClaimedTo";
            this.colDateClaimedTo.Width = 20;
            // 
            // colPayrollTypeID
            // 
            this.colPayrollTypeID.FieldName = "PayrollTypeID";
            this.colPayrollTypeID.Name = "colPayrollTypeID";
            this.colPayrollTypeID.Width = 33;
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 14;
            // 
            // colPayrollTagID
            // 
            this.colPayrollTagID.FieldName = "PayrollTagID";
            this.colPayrollTagID.Name = "colPayrollTagID";
            this.colPayrollTagID.Width = 37;
            // 
            // colTag
            // 
            this.colTag.FieldName = "Tag";
            this.colTag.Name = "colTag";
            this.colTag.Visible = true;
            this.colTag.VisibleIndex = 15;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 16;
            // 
            // colPaymentModeID
            // 
            this.colPaymentModeID.FieldName = "PaymentModeID";
            this.colPaymentModeID.Name = "colPaymentModeID";
            this.colPaymentModeID.Width = 37;
            // 
            // colPaymentMode
            // 
            this.colPaymentMode.FieldName = "PaymentMode";
            this.colPaymentMode.Name = "colPaymentMode";
            this.colPaymentMode.Visible = true;
            this.colPaymentMode.VisibleIndex = 17;
            // 
            // colRemarks
            // 
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.Visible = true;
            this.colRemarks.VisibleIndex = 18;
            // 
            // colModified
            // 
            this.colModified.FieldName = "Modified";
            this.colModified.Name = "colModified";
            this.colModified.Visible = true;
            this.colModified.VisibleIndex = 19;
            // 
            // colCreated
            // 
            this.colCreated.FieldName = "Created";
            this.colCreated.Name = "colCreated";
            this.colCreated.Visible = true;
            this.colCreated.VisibleIndex = 20;
            // 
            // colModifiedBy
            // 
            this.colModifiedBy.FieldName = "ModifiedBy";
            this.colModifiedBy.Name = "colModifiedBy";
            this.colModifiedBy.Width = 52;
            // 
            // colDateTimeModified
            // 
            this.colDateTimeModified.FieldName = "DateTimeModified";
            this.colDateTimeModified.Name = "colDateTimeModified";
            this.colDateTimeModified.Width = 38;
            // 
            // colDateTimeEntry
            // 
            this.colDateTimeEntry.FieldName = "DateTimeEntry";
            this.colDateTimeEntry.Name = "colDateTimeEntry";
            this.colDateTimeEntry.Width = 37;
            // 
            // colEntryBy
            // 
            this.colEntryBy.FieldName = "EntryBy";
            this.colEntryBy.Name = "colEntryBy";
            this.colEntryBy.Width = 20;
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
            this.viewAttachmentsToolStripMenuItem});
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
            this.newApplicantToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.newApplicantToolStripMenuItem.Text = "Print payroll";
            this.newApplicantToolStripMenuItem.Click += new System.EventHandler(this.newApplicantToolStripMenuItem_Click);
            // 
            // viewAttachmentsToolStripMenuItem
            // 
            this.viewAttachmentsToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_attachment_16;
            this.viewAttachmentsToolStripMenuItem.Name = "viewAttachmentsToolStripMenuItem";
            this.viewAttachmentsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.viewAttachmentsToolStripMenuItem.Text = "View Attachments";
            this.viewAttachmentsToolStripMenuItem.Click += new System.EventHandler(this.viewAttachmentsToolStripMenuItem_Click);
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
            this.progressBarControl1.Location = new System.Drawing.Point(10, 288);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(838, 10);
            this.progressBarControl1.TabIndex = 12;
            // 
            // Payroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 612);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupControlPayroll);
            this.Controls.Add(this.panel_spinner);
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ck_all.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_remarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_claimtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_from.Properties)).EndInit();
            this.panel_spinner.ResumeLayout(false);
            this.panel_spinner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPayroll)).EndInit();
            this.groupControlPayroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payrollViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_payroll_socpenBindingSource)).EndInit();
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
        private DevExpress.XtraEditors.GroupControl groupControlPayroll;
        private DevExpress.XtraGrid.GridControl gridPayroll;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView0;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_period;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_year;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_municipality;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newApplicantToolStripMenuItem;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.ToolStripMenuItem viewAttachmentsToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl2;
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
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colMasterListID;
        private DevExpress.XtraGrid.Columns.GridColumn colPSGCBrgy;
        private DevExpress.XtraGrid.Columns.GridColumn colPSGCCityMun;
        private DevExpress.XtraGrid.Columns.GridColumn colPSGCProvince;
        private DevExpress.XtraGrid.Columns.GridColumn colPSGCRegion;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colYear;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriodID;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollStatusID;
        private DevExpress.XtraGrid.Columns.GridColumn colClaimTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollTagID;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentModeID;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTimeEntry;
        private DevExpress.XtraGrid.Columns.GridColumn colDateClaimedFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDateClaimedTo;
        private DevExpress.XtraGrid.Columns.GridColumn colEntryBy;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTimeModified;
        private DevExpress.XtraGrid.Columns.GridColumn colModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colVerified;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colBarangay;
        private DevExpress.XtraGrid.Columns.GridColumn colBirthDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSex;
        private DevExpress.XtraGrid.Columns.GridColumn colHealthStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colHealthStatusRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colIdType;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colTag;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentMode;
        private DevExpress.XtraGrid.Columns.GridColumn colModified;
        private DevExpress.XtraGrid.Columns.GridColumn colCreated;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.GroupBox gb_details;
        private DevExpress.XtraEditors.SimpleButton btn_refresh;
    }
}