namespace Time_and_Attendance
{
    partial class TextFileName
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
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.cmbDateFormat = new System.Windows.Forms.ComboBox();
            this.txtprefix = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbError = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.grpTextfile = new System.Windows.Forms.GroupBox();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.txtTextFile = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.rdbnCurrent = new System.Windows.Forms.RadioButton();
            this.rdbnFrom = new System.Windows.Forms.RadioButton();
            this.rdbnAll = new System.Windows.Forms.RadioButton();
            this.label39 = new System.Windows.Forms.Label();
            this.grpTextfile.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(346, 12);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(59, 20);
            this.txtSuffix.TabIndex = 109;
            // 
            // cmbDateFormat
            // 
            this.cmbDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateFormat.FormattingEnabled = true;
            this.cmbDateFormat.Items.AddRange(new object[] {
            "--select--",
            "ddMMyy",
            "yyMMdd",
            "ddMMyyyy",
            "yyyyMMdd",
            "dd/MM/yyyy",
            "yyyy/MM/dd",
            "dd/MM/yy",
            "yy/MM/dd",
            "dd-MM-yyyy",
            "yyyy-MM-dd",
            "dd-MM-yy",
            "yy-MM-dd",
            "d/M/yyyy",
            "yyyy/M/d",
            "d/M/yy",
            "yy/M/d",
            "d-M-yyyy",
            "yyyy-M-d",
            "d-M-yy",
            "yy-M-d",
            "d/MM/yyyy",
            "yyyy/MM/d",
            "d/MM/yy",
            "yy/MM/d",
            "d-MM-yyyy",
            "yyyy-MM-d",
            "d-MM-yy",
            "yy-MM-d",
            "None(Single File)"});
            this.cmbDateFormat.Location = new System.Drawing.Point(229, 12);
            this.cmbDateFormat.Name = "cmbDateFormat";
            this.cmbDateFormat.Size = new System.Drawing.Size(111, 21);
            this.cmbDateFormat.TabIndex = 108;
            this.cmbDateFormat.SelectedIndexChanged += new System.EventHandler(this.cmbDateFormat_SelectedIndexChanged);
            // 
            // txtprefix
            // 
            this.txtprefix.Location = new System.Drawing.Point(163, 12);
            this.txtprefix.Name = "txtprefix";
            this.txtprefix.Size = new System.Drawing.Size(59, 20);
            this.txtprefix.TabIndex = 107;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(11, 15);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(128, 13);
            this.label38.TabIndex = 106;
            this.label38.Text = "File Name Format            :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "(Prefix)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 120;
            this.label2.Text = "(Suffix)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "(Date Format)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 122;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(173, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 123;
            this.btnCancel.Text = "Clear";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.ForeColor = System.Drawing.Color.DarkRed;
            this.lbError.Location = new System.Drawing.Point(18, 193);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 13);
            this.lbError.TabIndex = 124;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(329, 216);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 125;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "File Path :";
            // 
            // grpTextfile
            // 
            this.grpTextfile.Controls.Add(this.btnFilePath);
            this.grpTextfile.Controls.Add(this.txtTextFile);
            this.grpTextfile.Controls.Add(this.label4);
            this.grpTextfile.Enabled = false;
            this.grpTextfile.Location = new System.Drawing.Point(12, 120);
            this.grpTextfile.Name = "grpTextfile";
            this.grpTextfile.Size = new System.Drawing.Size(393, 62);
            this.grpTextfile.TabIndex = 127;
            this.grpTextfile.TabStop = false;
            this.grpTextfile.Text = "Single file Path";
            // 
            // btnFilePath
            // 
            this.btnFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePath.Location = new System.Drawing.Point(357, 24);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(30, 21);
            this.btnFilePath.TabIndex = 128;
            this.btnFilePath.Text = "\'\'\'";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // txtTextFile
            // 
            this.txtTextFile.BackColor = System.Drawing.Color.White;
            this.txtTextFile.Enabled = false;
            this.txtTextFile.Location = new System.Drawing.Point(66, 25);
            this.txtTextFile.Name = "txtTextFile";
            this.txtTextFile.Size = new System.Drawing.Size(285, 20);
            this.txtTextFile.TabIndex = 127;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.label40);
            this.panel1.Controls.Add(this.rdbnCurrent);
            this.panel1.Controls.Add(this.rdbnFrom);
            this.panel1.Controls.Add(this.rdbnAll);
            this.panel1.Controls.Add(this.label39);
            this.panel1.Location = new System.Drawing.Point(1, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 66);
            this.panel1.TabIndex = 128;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(161, 40);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(141, 20);
            this.dtpFrom.TabIndex = 121;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(10, 44);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(125, 13);
            this.label40.TabIndex = 120;
            this.label40.Text = "From Date                      :";
            // 
            // rdbnCurrent
            // 
            this.rdbnCurrent.AutoSize = true;
            this.rdbnCurrent.Checked = true;
            this.rdbnCurrent.Location = new System.Drawing.Point(308, 8);
            this.rdbnCurrent.Name = "rdbnCurrent";
            this.rdbnCurrent.Size = new System.Drawing.Size(85, 17);
            this.rdbnCurrent.TabIndex = 119;
            this.rdbnCurrent.TabStop = true;
            this.rdbnCurrent.Text = "Current Date";
            this.rdbnCurrent.UseVisualStyleBackColor = true;
            this.rdbnCurrent.CheckedChanged += new System.EventHandler(this.rdbnCurrent_CheckedChanged);
            // 
            // rdbnFrom
            // 
            this.rdbnFrom.AutoSize = true;
            this.rdbnFrom.Location = new System.Drawing.Point(228, 8);
            this.rdbnFrom.Name = "rdbnFrom";
            this.rdbnFrom.Size = new System.Drawing.Size(74, 17);
            this.rdbnFrom.TabIndex = 118;
            this.rdbnFrom.Text = "From Date";
            this.rdbnFrom.UseVisualStyleBackColor = true;
            this.rdbnFrom.CheckedChanged += new System.EventHandler(this.rdbnFrom_CheckedChanged);
            // 
            // rdbnAll
            // 
            this.rdbnAll.AutoSize = true;
            this.rdbnAll.Location = new System.Drawing.Point(161, 8);
            this.rdbnAll.Name = "rdbnAll";
            this.rdbnAll.Size = new System.Drawing.Size(60, 17);
            this.rdbnAll.TabIndex = 117;
            this.rdbnAll.Text = "All Files";
            this.rdbnAll.UseVisualStyleBackColor = true;
            this.rdbnAll.CheckedChanged += new System.EventHandler(this.rdbnAll_CheckedChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(10, 10);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(128, 13);
            this.label39.TabIndex = 116;
            this.label39.Text = "Date to Read                  :";
            // 
            // TextFileName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 251);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpTextfile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.cmbDateFormat);
            this.Controls.Add(this.txtprefix);
            this.Controls.Add(this.label38);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextFileName";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text File Name Format Settings";
            this.Load += new System.EventHandler(this.TextFileName_Load);
            this.grpTextfile.ResumeLayout(false);
            this.grpTextfile.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.ComboBox cmbDateFormat;
        private System.Windows.Forms.TextBox txtprefix;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpTextfile;
        private System.Windows.Forms.TextBox txtTextFile;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.RadioButton rdbnCurrent;
        private System.Windows.Forms.RadioButton rdbnFrom;
        private System.Windows.Forms.RadioButton rdbnAll;
        private System.Windows.Forms.Label label39;
    }
}