
namespace SpinsNew.Forms
{
    partial class AuthorizeRepresentative
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizeRepresentative));
            this.txt_reference = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.authorizeRepViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiddleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnglish = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txt_lastname = new DevExpress.XtraEditors.TextEdit();
            this.txt_firstname = new DevExpress.XtraEditors.TextEdit();
            this.txt_middlename = new DevExpress.XtraEditors.TextEdit();
            this.txt_extname = new DevExpress.XtraEditors.TextEdit();
            this.cmb_relationship = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txt_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_edit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_reference.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizeRepViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_lastname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_firstname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_middlename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_extname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_relationship.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_id.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_reference
            // 
            this.txt_reference.Enabled = false;
            this.txt_reference.Location = new System.Drawing.Point(459, 1);
            this.txt_reference.Name = "txt_reference";
            this.txt_reference.Properties.ReadOnly = true;
            this.txt_reference.Size = new System.Drawing.Size(113, 20);
            this.txt_reference.TabIndex = 0;
            this.txt_reference.Visible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(11, 160);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(573, 200);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Authorize Representatives";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(5, 24);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(22, 21);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.ToolTip = "Remove highlighted";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.authorizeRepViewModelBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(4, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(565, 173);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // authorizeRepViewModelBindingSource
            // 
            this.authorizeRepViewModelBindingSource.DataSource = typeof(SpinsNew.ViewModel.AuthorizeRepViewModel);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLastName,
            this.colFirstName,
            this.colMiddleName,
            this.colExtName,
            this.colEnglish});
            this.gridView1.DetailHeight = 303;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsFind.FindFilterColumns = "LastName;FirstName;MiddleName";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colLastName
            // 
            this.colLastName.FieldName = "LastName";
            this.colLastName.Name = "colLastName";
            this.colLastName.Visible = true;
            this.colLastName.VisibleIndex = 0;
            // 
            // colFirstName
            // 
            this.colFirstName.FieldName = "FirstName";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.Visible = true;
            this.colFirstName.VisibleIndex = 1;
            // 
            // colMiddleName
            // 
            this.colMiddleName.FieldName = "MiddleName";
            this.colMiddleName.Name = "colMiddleName";
            this.colMiddleName.Visible = true;
            this.colMiddleName.VisibleIndex = 2;
            // 
            // colExtName
            // 
            this.colExtName.FieldName = "ExtName";
            this.colExtName.Name = "colExtName";
            this.colExtName.Visible = true;
            this.colExtName.VisibleIndex = 3;
            // 
            // colEnglish
            // 
            this.colEnglish.FieldName = "English";
            this.colEnglish.Name = "colEnglish";
            this.colEnglish.Visible = true;
            this.colEnglish.VisibleIndex = 4;
            // 
            // txt_lastname
            // 
            this.txt_lastname.Location = new System.Drawing.Point(77, 27);
            this.txt_lastname.Name = "txt_lastname";
            this.txt_lastname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_lastname.Size = new System.Drawing.Size(148, 20);
            this.txt_lastname.TabIndex = 0;
            // 
            // txt_firstname
            // 
            this.txt_firstname.Location = new System.Drawing.Point(77, 55);
            this.txt_firstname.Name = "txt_firstname";
            this.txt_firstname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_firstname.Size = new System.Drawing.Size(148, 20);
            this.txt_firstname.TabIndex = 1;
            // 
            // txt_middlename
            // 
            this.txt_middlename.Location = new System.Drawing.Point(77, 84);
            this.txt_middlename.Name = "txt_middlename";
            this.txt_middlename.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_middlename.Size = new System.Drawing.Size(148, 20);
            this.txt_middlename.TabIndex = 2;
            // 
            // txt_extname
            // 
            this.txt_extname.Location = new System.Drawing.Point(321, 27);
            this.txt_extname.Name = "txt_extname";
            this.txt_extname.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_extname.Size = new System.Drawing.Size(148, 20);
            this.txt_extname.TabIndex = 3;
            // 
            // cmb_relationship
            // 
            this.cmb_relationship.Location = new System.Drawing.Point(321, 55);
            this.cmb_relationship.Name = "cmb_relationship";
            this.cmb_relationship.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_relationship.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_relationship.Size = new System.Drawing.Size(148, 20);
            this.cmb_relationship.TabIndex = 4;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txt_id);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.btn_edit);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txt_lastname);
            this.groupControl2.Controls.Add(this.cmb_relationship);
            this.groupControl2.Controls.Add(this.txt_reference);
            this.groupControl2.Controls.Add(this.txt_firstname);
            this.groupControl2.Controls.Add(this.txt_extname);
            this.groupControl2.Controls.Add(this.txt_middlename);
            this.groupControl2.Location = new System.Drawing.Point(11, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(573, 152);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Details";
            // 
            // txt_id
            // 
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(340, 1);
            this.txt_id.Name = "txt_id";
            this.txt_id.Properties.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(113, 20);
            this.txt_id.TabIndex = 36;
            this.txt_id.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(254, 55);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(58, 13);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "Relationship";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(235, 29);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 35;
            this.labelControl4.Text = "Extension Name";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 35;
            this.labelControl3.Text = "Middle Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "First Name";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Last Name";
            // 
            // btn_edit
            // 
            this.btn_edit.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btn_edit.Appearance.Options.UseFont = true;
            this.btn_edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_edit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit.ImageOptions.Image")));
            this.btn_edit.Location = new System.Drawing.Point(223, 123);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(148, 24);
            this.btn_edit.TabIndex = 5;
            this.btn_edit.Text = "Save";
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // AuthorizeRepresentative
            // 
            this.AcceptButton = this.btn_edit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 368);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(607, 407);
            this.MinimumSize = new System.Drawing.Size(607, 407);
            this.Name = "AuthorizeRepresentative";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorize Representative";
            this.Load += new System.EventHandler(this.AuthorizeRepresentative_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_reference.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizeRepViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_lastname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_firstname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_middlename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_extname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_relationship.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_id.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txt_reference;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txt_lastname;
        private DevExpress.XtraEditors.TextEdit txt_firstname;
        private DevExpress.XtraEditors.TextEdit txt_middlename;
        private DevExpress.XtraEditors.TextEdit txt_extname;
        private DevExpress.XtraEditors.ComboBoxEdit cmb_relationship;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btn_edit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.TextEdit txt_id;
        private System.Windows.Forms.BindingSource authorizeRepViewModelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colLastName;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn colMiddleName;
        private DevExpress.XtraGrid.Columns.GridColumn colExtName;
        private DevExpress.XtraGrid.Columns.GridColumn colEnglish;
    }
}