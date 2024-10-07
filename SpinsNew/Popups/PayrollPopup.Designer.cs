
namespace SpinsNew.Popups
{
    partial class PayrollPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayrollPopup));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel_spinner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_amount = new DevExpress.XtraEditors.TextEdit();
            this.txt_multiplier = new DevExpress.XtraEditors.TextEdit();
            this.txt_monthlystipend = new DevExpress.XtraEditors.TextEdit();
            this.btn_create = new DevExpress.XtraEditors.SimpleButton();
            this.cmb_payment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_period = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_tag = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmb_type = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmb_year = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel_spinner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_amount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_multiplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_monthlystipend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_payment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_tag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panel_spinner);
            this.groupControl1.Controls.Add(this.txt_amount);
            this.groupControl1.Controls.Add(this.txt_multiplier);
            this.groupControl1.Controls.Add(this.txt_monthlystipend);
            this.groupControl1.Controls.Add(this.btn_create);
            this.groupControl1.Controls.Add(this.cmb_payment);
            this.groupControl1.Controls.Add(this.cmb_period);
            this.groupControl1.Controls.Add(this.cmb_tag);
            this.groupControl1.Controls.Add(this.cmb_type);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cmb_year);
            this.groupControl1.Location = new System.Drawing.Point(13, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(344, 198);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Fill for payroll";
            // 
            // panel_spinner
            // 
            this.panel_spinner.Controls.Add(this.pictureBox1);
            this.panel_spinner.Controls.Add(this.labelControl5);
            this.panel_spinner.Location = new System.Drawing.Point(87, 173);
            this.panel_spinner.Name = "panel_spinner";
            this.panel_spinner.Size = new System.Drawing.Size(201, 19);
            this.panel_spinner.TabIndex = 16;
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
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(29, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(127, 13);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Creating, please wait.......";
            // 
            // txt_amount
            // 
            this.txt_amount.EditValue = "0";
            this.txt_amount.Location = new System.Drawing.Point(294, 82);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Properties.ReadOnly = true;
            this.txt_amount.Size = new System.Drawing.Size(46, 20);
            this.txt_amount.TabIndex = 9;
            this.txt_amount.Visible = false;
            // 
            // txt_multiplier
            // 
            this.txt_multiplier.EditValue = "0";
            this.txt_multiplier.Location = new System.Drawing.Point(294, 57);
            this.txt_multiplier.Name = "txt_multiplier";
            this.txt_multiplier.Properties.ReadOnly = true;
            this.txt_multiplier.Size = new System.Drawing.Size(46, 20);
            this.txt_multiplier.TabIndex = 9;
            this.txt_multiplier.Visible = false;
            this.txt_multiplier.EditValueChanged += new System.EventHandler(this.txt_multiplier_EditValueChanged);
            // 
            // txt_monthlystipend
            // 
            this.txt_monthlystipend.EditValue = "0";
            this.txt_monthlystipend.Location = new System.Drawing.Point(294, 35);
            this.txt_monthlystipend.Name = "txt_monthlystipend";
            this.txt_monthlystipend.Properties.ReadOnly = true;
            this.txt_monthlystipend.Size = new System.Drawing.Size(46, 20);
            this.txt_monthlystipend.TabIndex = 8;
            this.txt_monthlystipend.Visible = false;
            this.txt_monthlystipend.EditValueChanged += new System.EventHandler(this.txt_monthlystipend_EditValueChanged);
            // 
            // btn_create
            // 
            this.btn_create.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_create.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_create.ImageOptions.Image")));
            this.btn_create.Location = new System.Drawing.Point(142, 152);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(90, 24);
            this.btn_create.TabIndex = 7;
            this.btn_create.Text = "Create";
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // cmb_payment
            // 
            this.cmb_payment.Location = new System.Drawing.Point(87, 129);
            this.cmb_payment.Name = "cmb_payment";
            this.cmb_payment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_payment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_payment.Size = new System.Drawing.Size(201, 20);
            this.cmb_payment.TabIndex = 4;
            // 
            // cmb_period
            // 
            this.cmb_period.Location = new System.Drawing.Point(87, 59);
            this.cmb_period.Name = "cmb_period";
            this.cmb_period.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_period.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_period.Size = new System.Drawing.Size(201, 20);
            this.cmb_period.TabIndex = 1;
            this.cmb_period.SelectedIndexChanged += new System.EventHandler(this.cmb_period_SelectedIndexChanged);
            // 
            // cmb_tag
            // 
            this.cmb_tag.Location = new System.Drawing.Point(87, 106);
            this.cmb_tag.Name = "cmb_tag";
            this.cmb_tag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_tag.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_tag.Size = new System.Drawing.Size(201, 20);
            this.cmb_tag.TabIndex = 3;
            // 
            // cmb_type
            // 
            this.cmb_type.Location = new System.Drawing.Point(87, 82);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_type.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_type.Size = new System.Drawing.Size(201, 20);
            this.cmb_type.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(45, 128);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(37, 17);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "Mode";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(41, 61);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 17);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Period";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(57, 106);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(23, 17);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Tag";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(52, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Type";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(51, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Year";
            // 
            // cmb_year
            // 
            this.cmb_year.Location = new System.Drawing.Point(87, 36);
            this.cmb_year.Name = "cmb_year";
            this.cmb_year.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_year.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_year.Size = new System.Drawing.Size(201, 20);
            this.cmb_year.TabIndex = 0;
            this.cmb_year.SelectedIndexChanged += new System.EventHandler(this.cmb_year_SelectedIndexChanged);
            // 
            // PayrollPopup
            // 
            this.AcceptButton = this.btn_create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 219);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(385, 258);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(385, 258);
            this.Name = "PayrollPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll Pop-up";
            this.Load += new System.EventHandler(this.PayrollPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.panel_spinner.ResumeLayout(false);
            this.panel_spinner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_amount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_multiplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_monthlystipend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_payment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_period.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_tag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_year.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btn_create;
        public DevExpress.XtraEditors.ComboBoxEdit cmb_payment;
        public DevExpress.XtraEditors.ComboBoxEdit cmb_period;
        public DevExpress.XtraEditors.ComboBoxEdit cmb_tag;
        public DevExpress.XtraEditors.ComboBoxEdit cmb_type;
        public DevExpress.XtraEditors.ComboBoxEdit cmb_year;
        private DevExpress.XtraEditors.TextEdit txt_monthlystipend;
        private DevExpress.XtraEditors.TextEdit txt_multiplier;
        public DevExpress.XtraEditors.TextEdit txt_amount;
        private System.Windows.Forms.Panel panel_spinner;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}