
namespace SpinsNew
{
    partial class MasterList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterList));
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmb_municipality = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.panel_spinner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmb_status = new DevExpress.XtraEditors.ComboBoxEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbForms = new System.Windows.Forms.GroupBox();
            this.btnPayroll = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelist = new DevExpress.XtraEditors.SimpleButton();
            this.gbVerification = new System.Windows.Forms.GroupBox();
            this.btnUndoVerified = new DevExpress.XtraEditors.SimpleButton();
            this.btnVerify = new DevExpress.XtraEditors.SimpleButton();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetApplicant = new DevExpress.XtraEditors.SimpleButton();
            this.btnActivate = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelistBene = new DevExpress.XtraEditors.SimpleButton();
            this.gbPayroll = new System.Windows.Forms.GroupBox();
            this.btnViewPayroll = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreatePayroll = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewAttach = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.panel_spinner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_status.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbForms.SuspendLayout();
            this.gbVerification.SuspendLayout();
            this.gbActions.SuspendLayout();
            this.gbPayroll.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSplitContainer1.Grid = null;
            this.gridSplitContainer1.Location = new System.Drawing.Point(12, 152);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Size = new System.Drawing.Size(912, 570);
            this.gridSplitContainer1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(5, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1335, 290);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.BackColorChanged += new System.EventHandler(this.gridControl1_BackColorChanged);
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Preview.BackColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.Preview.BackColor2 = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.Preview.BorderColor = System.Drawing.Color.Transparent;
            this.gridView1.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Preview.Options.UseBackColor = true;
            this.gridView1.Appearance.Preview.Options.UseBorderColor = true;
            this.gridView1.Appearance.Preview.Options.UseFont = true;
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.DetailHeight = 303;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsFind.FindFilterColumns = "LastName;FirstName;MiddleName";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupControl2.Appearance.Options.UseBackColor = true;
            this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.cmb_municipality);
            this.groupControl2.Controls.Add(this.searchControl1);
            this.groupControl2.Controls.Add(this.panel_spinner);
            this.groupControl2.Controls.Add(this.cmb_status);
            this.groupControl2.Location = new System.Drawing.Point(15, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1344, 99);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl2_Paint);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(448, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 20;
            this.labelControl4.Text = "Select Status";
            this.labelControl4.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl4.ToolTipTitle = "Infor";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(5, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(123, 13);
            this.labelControl3.TabIndex = 19;
            this.labelControl3.Text = "Select City/Municipality";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl3.ToolTipTitle = "Infor";
            // 
            // cmb_municipality
            // 
            this.cmb_municipality.EditValue = "";
            this.cmb_municipality.Location = new System.Drawing.Point(5, 39);
            this.cmb_municipality.Name = "cmb_municipality";
            this.cmb_municipality.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_municipality.Properties.Appearance.Options.UseFont = true;
            this.cmb_municipality.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_municipality.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cmb_municipality.Properties.DropDownRows = 20;
            this.cmb_municipality.Properties.IncrementalSearch = true;
            this.cmb_municipality.Size = new System.Drawing.Size(438, 22);
            this.cmb_municipality.TabIndex = 2;
            this.cmb_municipality.EditValueChanged += new System.EventHandler(this.checkedComboBoxEdit1_EditValueChanged);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(4, 72);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchControl1.Properties.FindDelay = 500;
            this.searchControl1.Size = new System.Drawing.Size(439, 20);
            this.searchControl1.TabIndex = 4;
            this.searchControl1.QueryIsSearchColumn += new DevExpress.XtraEditors.QueryIsSearchColumnEventHandler(this.searchControl1_QueryIsSearchColumn_1);
            this.searchControl1.SelectedIndexChanged += new System.EventHandler(this.searchControl1_SelectedIndexChanged);
            // 
            // panel_spinner
            // 
            this.panel_spinner.Controls.Add(this.pictureBox1);
            this.panel_spinner.Controls.Add(this.labelControl2);
            this.panel_spinner.Location = new System.Drawing.Point(448, 70);
            this.panel_spinner.Name = "panel_spinner";
            this.panel_spinner.Size = new System.Drawing.Size(179, 19);
            this.panel_spinner.TabIndex = 15;
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
            // cmb_status
            // 
            this.cmb_status.EditValue = "";
            this.cmb_status.Location = new System.Drawing.Point(448, 39);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_status.Properties.Appearance.Options.UseFont = true;
            this.cmb_status.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_status.Properties.Items.AddRange(new object[] {
            "All Statuses",
            "Active",
            "Applicant",
            "Waitlisted",
            "Delisted"});
            this.cmb_status.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_status.Size = new System.Drawing.Size(209, 22);
            this.cmb_status.TabIndex = 3;
            this.cmb_status.SelectedIndexChanged += new System.EventHandler(this.cmb_status_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(10, 265);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(1344, 10);
            this.progressBarControl1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 281);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(243, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Double click for viewing, editing, and adding of GIS";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl1.ToolTipTitle = "Infor";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(12, 280);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(17, 17);
            this.pictureEdit1.TabIndex = 17;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(10, 303);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1344, 321);
            this.groupControl1.TabIndex = 18;
            this.groupControl1.Text = "Count";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gbForms);
            this.panel1.Controls.Add(this.gbVerification);
            this.panel1.Controls.Add(this.gbActions);
            this.panel1.Controls.Add(this.gbPayroll);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(15, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 152);
            this.panel1.TabIndex = 19;
            // 
            // gbForms
            // 
            this.gbForms.Controls.Add(this.btnPayroll);
            this.gbForms.Controls.Add(this.btnDelist);
            this.gbForms.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbForms.Location = new System.Drawing.Point(937, 5);
            this.gbForms.Name = "gbForms";
            this.gbForms.Size = new System.Drawing.Size(228, 137);
            this.gbForms.TabIndex = 5;
            this.gbForms.TabStop = false;
            this.gbForms.Text = "Forms";
            // 
            // btnPayroll
            // 
            this.btnPayroll.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnPayroll.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayroll.Appearance.Options.UseBackColor = true;
            this.btnPayroll.Appearance.Options.UseFont = true;
            this.btnPayroll.AppearanceDisabled.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnPayroll.AppearanceDisabled.Options.UseBackColor = true;
            this.btnPayroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayroll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPayroll.ImageOptions.Image")));
            this.btnPayroll.Location = new System.Drawing.Point(6, 20);
            this.btnPayroll.Name = "btnPayroll";
            this.btnPayroll.Size = new System.Drawing.Size(212, 54);
            this.btnPayroll.TabIndex = 0;
            this.btnPayroll.Text = "Payroll Form";
            this.btnPayroll.Click += new System.EventHandler(this.btnPayroll_Click);
            // 
            // btnDelist
            // 
            this.btnDelist.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnDelist.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelist.Appearance.Options.UseBackColor = true;
            this.btnDelist.Appearance.Options.UseFont = true;
            this.btnDelist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelist.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelist.ImageOptions.Image")));
            this.btnDelist.Location = new System.Drawing.Point(6, 77);
            this.btnDelist.Name = "btnDelist";
            this.btnDelist.Size = new System.Drawing.Size(212, 54);
            this.btnDelist.TabIndex = 0;
            this.btnDelist.Text = "Delisted And Replacements";
            this.btnDelist.Click += new System.EventHandler(this.btnDelist_Click);
            // 
            // gbVerification
            // 
            this.gbVerification.Controls.Add(this.btnUndoVerified);
            this.gbVerification.Controls.Add(this.btnVerify);
            this.gbVerification.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVerification.ForeColor = System.Drawing.Color.Black;
            this.gbVerification.Location = new System.Drawing.Point(705, 6);
            this.gbVerification.Name = "gbVerification";
            this.gbVerification.Size = new System.Drawing.Size(228, 137);
            this.gbVerification.TabIndex = 4;
            this.gbVerification.TabStop = false;
            this.gbVerification.Text = "Verification";
            // 
            // btnUndoVerified
            // 
            this.btnUndoVerified.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnUndoVerified.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndoVerified.Appearance.Options.UseBackColor = true;
            this.btnUndoVerified.Appearance.Options.UseFont = true;
            this.btnUndoVerified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUndoVerified.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUndoVerified.ImageOptions.Image")));
            this.btnUndoVerified.Location = new System.Drawing.Point(6, 76);
            this.btnUndoVerified.Name = "btnUndoVerified";
            this.btnUndoVerified.Size = new System.Drawing.Size(212, 54);
            this.btnUndoVerified.TabIndex = 0;
            this.btnUndoVerified.Text = "Undo Verified";
            this.btnUndoVerified.Click += new System.EventHandler(this.btnUndoVerified_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnVerify.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerify.Appearance.Options.UseBackColor = true;
            this.btnVerify.Appearance.Options.UseFont = true;
            this.btnVerify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerify.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnVerify.ImageOptions.Image")));
            this.btnVerify.Location = new System.Drawing.Point(6, 19);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(212, 54);
            this.btnVerify.TabIndex = 0;
            this.btnVerify.Text = "Verify";
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.btnDelete);
            this.gbActions.Controls.Add(this.btnSetApplicant);
            this.gbActions.Controls.Add(this.btnActivate);
            this.gbActions.Controls.Add(this.btnDelistBene);
            this.gbActions.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbActions.ForeColor = System.Drawing.Color.Black;
            this.gbActions.Location = new System.Drawing.Point(4, 6);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(229, 137);
            this.gbActions.TabIndex = 3;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(6, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(212, 22);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSetApplicant
            // 
            this.btnSetApplicant.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.btnSetApplicant.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetApplicant.Appearance.Options.UseBackColor = true;
            this.btnSetApplicant.Appearance.Options.UseFont = true;
            this.btnSetApplicant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetApplicant.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetApplicant.ImageOptions.Image")));
            this.btnSetApplicant.Location = new System.Drawing.Point(6, 47);
            this.btnSetApplicant.Name = "btnSetApplicant";
            this.btnSetApplicant.Size = new System.Drawing.Size(212, 22);
            this.btnSetApplicant.TabIndex = 0;
            this.btnSetApplicant.Text = "Set as Applicant";
            this.btnSetApplicant.Click += new System.EventHandler(this.btnSetApplicant_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnActivate.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.Appearance.Options.UseBackColor = true;
            this.btnActivate.Appearance.Options.UseFont = true;
            this.btnActivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActivate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivate.ImageOptions.Image")));
            this.btnActivate.Location = new System.Drawing.Point(6, 19);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(212, 22);
            this.btnActivate.TabIndex = 0;
            this.btnActivate.Text = "Activate";
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnDelistBene
            // 
            this.btnDelistBene.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.btnDelistBene.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelistBene.Appearance.Options.UseBackColor = true;
            this.btnDelistBene.Appearance.Options.UseFont = true;
            this.btnDelistBene.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelistBene.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelistBene.ImageOptions.Image")));
            this.btnDelistBene.Location = new System.Drawing.Point(6, 76);
            this.btnDelistBene.Name = "btnDelistBene";
            this.btnDelistBene.Size = new System.Drawing.Size(212, 22);
            this.btnDelistBene.TabIndex = 0;
            this.btnDelistBene.Text = "Delist (Del)";
            this.btnDelistBene.Click += new System.EventHandler(this.btnDelistBene_Click);
            // 
            // gbPayroll
            // 
            this.gbPayroll.Controls.Add(this.btnViewPayroll);
            this.gbPayroll.Controls.Add(this.btnCreatePayroll);
            this.gbPayroll.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPayroll.ForeColor = System.Drawing.Color.Black;
            this.gbPayroll.Location = new System.Drawing.Point(472, 6);
            this.gbPayroll.Name = "gbPayroll";
            this.gbPayroll.Size = new System.Drawing.Size(228, 137);
            this.gbPayroll.TabIndex = 2;
            this.gbPayroll.TabStop = false;
            this.gbPayroll.Text = "Payroll";
            // 
            // btnViewPayroll
            // 
            this.btnViewPayroll.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnViewPayroll.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPayroll.Appearance.Options.UseBackColor = true;
            this.btnViewPayroll.Appearance.Options.UseFont = true;
            this.btnViewPayroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewPayroll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnViewPayroll.ImageOptions.Image")));
            this.btnViewPayroll.Location = new System.Drawing.Point(6, 19);
            this.btnViewPayroll.Name = "btnViewPayroll";
            this.btnViewPayroll.Size = new System.Drawing.Size(212, 54);
            this.btnViewPayroll.TabIndex = 0;
            this.btnViewPayroll.Text = "View (Ctrl +P)";
            this.btnViewPayroll.Click += new System.EventHandler(this.btnViewPayroll_Click);
            // 
            // btnCreatePayroll
            // 
            this.btnCreatePayroll.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnCreatePayroll.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePayroll.Appearance.Options.UseBackColor = true;
            this.btnCreatePayroll.Appearance.Options.UseFont = true;
            this.btnCreatePayroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreatePayroll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCreatePayroll.ImageOptions.Image")));
            this.btnCreatePayroll.Location = new System.Drawing.Point(6, 75);
            this.btnCreatePayroll.Name = "btnCreatePayroll";
            this.btnCreatePayroll.Size = new System.Drawing.Size(212, 54);
            this.btnCreatePayroll.TabIndex = 0;
            this.btnCreatePayroll.Text = "Create";
            this.btnCreatePayroll.Click += new System.EventHandler(this.btnCreatePayroll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnViewAttach);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(239, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 137);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // btnNew
            // 
            this.btnNew.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnNew.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Appearance.Options.UseBackColor = true;
            this.btnNew.Appearance.Options.UseFont = true;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.Location = new System.Drawing.Point(6, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(212, 54);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Applicant";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnViewAttach
            // 
            this.btnViewAttach.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnViewAttach.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewAttach.Appearance.Options.UseBackColor = true;
            this.btnViewAttach.Appearance.Options.UseFont = true;
            this.btnViewAttach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewAttach.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnViewAttach.ImageOptions.Image")));
            this.btnViewAttach.Location = new System.Drawing.Point(6, 75);
            this.btnViewAttach.Name = "btnViewAttach";
            this.btnViewAttach.Size = new System.Drawing.Size(212, 54);
            this.btnViewAttach.TabIndex = 0;
            this.btnViewAttach.Text = "View Attachments (Ctrl + S)";
            this.btnViewAttach.Click += new System.EventHandler(this.btnViewAttach_Click);
            // 
            // MasterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1364, 636);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.groupControl2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MasterList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterList_FormClosing);
            this.Load += new System.EventHandler(this.MasterList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_municipality.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.panel_spinner.ResumeLayout(false);
            this.panel_spinner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_status.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbForms.ResumeLayout(false);
            this.gbVerification.ResumeLayout(false);
            this.gbActions.ResumeLayout(false);
            this.gbPayroll.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Panel panel_spinner;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmb_municipality;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_status;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnDelist;
        private DevExpress.XtraEditors.SimpleButton btnViewAttach;
        private System.Windows.Forms.GroupBox gbPayroll;
        private DevExpress.XtraEditors.SimpleButton btnViewPayroll;
        private DevExpress.XtraEditors.SimpleButton btnPayroll;
        private DevExpress.XtraEditors.SimpleButton btnCreatePayroll;
        private System.Windows.Forms.GroupBox gbActions;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSetApplicant;
        private DevExpress.XtraEditors.SimpleButton btnActivate;
        private DevExpress.XtraEditors.SimpleButton btnDelistBene;
        private System.Windows.Forms.GroupBox gbVerification;
        private DevExpress.XtraEditors.SimpleButton btnUndoVerified;
        private DevExpress.XtraEditors.SimpleButton btnVerify;
        private System.Windows.Forms.GroupBox gbForms;
    }
}

