
namespace SpinsNew.Forms
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MasterlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.lblUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.lblUserrole = new System.Windows.Forms.ToolStripStatusLabel();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.btnRefreshNew = new DevExpress.XtraEditors.SimpleButton();
            this.labelTarget = new System.Windows.Forms.Label();
            this.utilizationTextBox = new DevExpress.XtraEditors.TextEdit();
            this.labelActual = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textTarget = new DevExpress.XtraEditors.TextEdit();
            this.textActual = new DevExpress.XtraEditors.TextEdit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utilizationTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textActual.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1389, 33);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MasterlistToolStripMenuItem});
            this.optionToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_menu_40;
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.optionToolStripMenuItem.Text = "Menu";
            // 
            // MasterlistToolStripMenuItem
            // 
            this.MasterlistToolStripMenuItem.Image = global::SpinsNew.Properties.Resources.icons8_menu_40;
            this.MasterlistToolStripMenuItem.Name = "MasterlistToolStripMenuItem";
            this.MasterlistToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.MasterlistToolStripMenuItem.Text = "Masterlist";
            this.MasterlistToolStripMenuItem.Click += new System.EventHandler(this.MasterlistToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblName,
            this.toolStripSplitButton2,
            this.lblUsername,
            this.toolStripSplitButton1,
            this.lblUserrole});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1389, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblName
            // 
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 17);
            this.lblName.Text = "Name";
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripSplitButton2.DropDownButtonWidth = 0;
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(5, 20);
            this.toolStripSplitButton2.Text = "toolStripSplitButton1";
            // 
            // lblUsername
            // 
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(60, 17);
            this.lblUsername.Text = "Username";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripSplitButton1.DropDownButtonWidth = 0;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(5, 20);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // lblUserrole
            // 
            this.lblUserrole.Name = "lblUserrole";
            this.lblUserrole.Size = new System.Drawing.Size(50, 17);
            this.lblUserrole.Text = "Userrole";
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram3;
            this.chartControl1.Location = new System.Drawing.Point(7, 159);
            this.chartControl1.Name = "chartControl1";
            series5.Name = "Target";
            series6.Name = "Served";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
            this.chartControl1.Size = new System.Drawing.Size(1365, 577);
            this.chartControl1.TabIndex = 13;
            // 
            // btnRefreshNew
            // 
            this.btnRefreshNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshNew.ImageOptions.Image")));
            this.btnRefreshNew.Location = new System.Drawing.Point(1245, 130);
            this.btnRefreshNew.Name = "btnRefreshNew";
            this.btnRefreshNew.Size = new System.Drawing.Size(127, 23);
            this.btnRefreshNew.TabIndex = 20;
            this.btnRefreshNew.Text = "Refresh";
            this.btnRefreshNew.Click += new System.EventHandler(this.btnRefreshNew_Click);
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTarget.Location = new System.Drawing.Point(173, 43);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(80, 30);
            this.labelTarget.TabIndex = 14;
            this.labelTarget.Text = "Target:";
            // 
            // utilizationTextBox
            // 
            this.utilizationTextBox.EditValue = "-";
            this.utilizationTextBox.Location = new System.Drawing.Point(259, 119);
            this.utilizationTextBox.Name = "utilizationTextBox";
            this.utilizationTextBox.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.utilizationTextBox.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utilizationTextBox.Properties.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.utilizationTextBox.Properties.Appearance.Options.UseBackColor = true;
            this.utilizationTextBox.Properties.Appearance.Options.UseFont = true;
            this.utilizationTextBox.Properties.Appearance.Options.UseForeColor = true;
            this.utilizationTextBox.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.utilizationTextBox.Properties.ReadOnly = true;
            this.utilizationTextBox.Size = new System.Drawing.Size(271, 34);
            this.utilizationTextBox.TabIndex = 19;
            // 
            // labelActual
            // 
            this.labelActual.AutoSize = true;
            this.labelActual.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActual.Location = new System.Drawing.Point(11, 83);
            this.labelActual.Name = "labelActual";
            this.labelActual.Size = new System.Drawing.Size(242, 30);
            this.labelActual.TabIndex = 15;
            this.labelActual.Text = "Actual Per Head Count:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "Utilization:";
            // 
            // textTarget
            // 
            this.textTarget.EditValue = "-";
            this.textTarget.Location = new System.Drawing.Point(259, 41);
            this.textTarget.Name = "textTarget";
            this.textTarget.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.textTarget.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTarget.Properties.Appearance.ForeColor = System.Drawing.Color.Violet;
            this.textTarget.Properties.Appearance.Options.UseBackColor = true;
            this.textTarget.Properties.Appearance.Options.UseFont = true;
            this.textTarget.Properties.Appearance.Options.UseForeColor = true;
            this.textTarget.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textTarget.Properties.ReadOnly = true;
            this.textTarget.Size = new System.Drawing.Size(271, 34);
            this.textTarget.TabIndex = 16;
            // 
            // textActual
            // 
            this.textActual.EditValue = "-";
            this.textActual.Location = new System.Drawing.Point(259, 81);
            this.textActual.Name = "textActual";
            this.textActual.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.textActual.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textActual.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.textActual.Properties.Appearance.Options.UseBackColor = true;
            this.textActual.Properties.Appearance.Options.UseFont = true;
            this.textActual.Properties.Appearance.Options.UseForeColor = true;
            this.textActual.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textActual.Properties.ReadOnly = true;
            this.textActual.Size = new System.Drawing.Size(271, 34);
            this.textActual.TabIndex = 17;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1389, 761);
            this.Controls.Add(this.btnRefreshNew);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.utilizationTextBox);
            this.Controls.Add(this.labelActual);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textTarget);
            this.Controls.Add(this.textActual);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utilizationTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textActual.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MasterlistToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblUsername;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
        private System.Windows.Forms.ToolStripStatusLabel lblName;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripStatusLabel lblUserrole;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.SimpleButton btnRefreshNew;
        private System.Windows.Forms.Label labelTarget;
        private DevExpress.XtraEditors.TextEdit utilizationTextBox;
        private System.Windows.Forms.Label labelActual;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textTarget;
        private DevExpress.XtraEditors.TextEdit textActual;
    }
}