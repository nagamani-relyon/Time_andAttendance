namespace Time_and_Attendance
{
    partial class frmMdbTableColumnsSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMdbTableColumnsSelect));
            this.lbFilePath = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbEmpId = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbInOut = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbIn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDate = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOut = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cmbDevId = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbSelectError = new System.Windows.Forms.Label();
            this.rdbnDateTime = new System.Windows.Forms.RadioButton();
            this.rdbnDateAndTime = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbnInAndOut = new System.Windows.Forms.RadioButton();
            this.rdbnInOut = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFilePath
            // 
            this.lbFilePath.AutoSize = true;
            this.lbFilePath.Location = new System.Drawing.Point(3, 12);
            this.lbFilePath.Name = "lbFilePath";
            this.lbFilePath.Size = new System.Drawing.Size(35, 13);
            this.lbFilePath.TabIndex = 0;
            this.lbFilePath.Text = "label1";
            // 
            // cboTable
            // 
            this.cboTable.ColumnWidth = 250;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(12, 77);
            this.cboTable.MultiColumn = true;
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(381, 134);
            this.cboTable.Sorted = true;
            this.cboTable.TabIndex = 1;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lbFilePath);
            this.panel1.Location = new System.Drawing.Point(12, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 33);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "MDB File Path : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Emp Id Refering Field        :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbEmpId);
            this.panel2.Location = new System.Drawing.Point(15, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 25);
            this.panel2.TabIndex = 16;
            // 
            // cmbEmpId
            // 
            this.cmbEmpId.FormattingEnabled = true;
            this.cmbEmpId.Location = new System.Drawing.Point(184, 2);
            this.cmbEmpId.Name = "cmbEmpId";
            this.cmbEmpId.Size = new System.Drawing.Size(194, 21);
            this.cmbEmpId.Sorted = true;
            this.cmbEmpId.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cmbInOut);
            this.panel3.Location = new System.Drawing.Point(15, 353);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 25);
            this.panel3.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "In_Out  Refering Field        :";
            // 
            // cmbInOut
            // 
            this.cmbInOut.FormattingEnabled = true;
            this.cmbInOut.Location = new System.Drawing.Point(184, 2);
            this.cmbInOut.Name = "cmbInOut";
            this.cmbInOut.Size = new System.Drawing.Size(194, 21);
            this.cmbInOut.Sorted = true;
            this.cmbInOut.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.Controls.Add(this.cmbIn);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(15, 382);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(378, 25);
            this.panel4.TabIndex = 18;
            // 
            // cmbIn
            // 
            this.cmbIn.FormattingEnabled = true;
            this.cmbIn.Location = new System.Drawing.Point(184, 2);
            this.cmbIn.Name = "cmbIn";
            this.cmbIn.Size = new System.Drawing.Size(194, 21);
            this.cmbIn.Sorted = true;
            this.cmbIn.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "In Time Refering Field        :";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.cmbDate);
            this.panel5.Location = new System.Drawing.Point(15, 325);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(378, 25);
            this.panel5.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Date Refering Field            :";
            // 
            // cmbDate
            // 
            this.cmbDate.FormattingEnabled = true;
            this.cmbDate.Location = new System.Drawing.Point(184, 1);
            this.cmbDate.Name = "cmbDate";
            this.cmbDate.Size = new System.Drawing.Size(194, 21);
            this.cmbDate.Sorted = true;
            this.cmbDate.TabIndex = 631;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.cmbOut);
            this.panel7.Location = new System.Drawing.Point(15, 411);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(378, 25);
            this.panel7.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Out Time Refering Field     :";
            // 
            // cmbOut
            // 
            this.cmbOut.FormattingEnabled = true;
            this.cmbOut.Location = new System.Drawing.Point(184, 2);
            this.cmbOut.Name = "cmbOut";
            this.cmbOut.Size = new System.Drawing.Size(194, 21);
            this.cmbOut.Sorted = true;
            this.cmbOut.TabIndex = 76;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel9.Controls.Add(this.cmbDevId);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Location = new System.Drawing.Point(15, 441);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(378, 25);
            this.panel9.TabIndex = 23;
            // 
            // cmbDevId
            // 
            this.cmbDevId.FormattingEnabled = true;
            this.cmbDevId.Location = new System.Drawing.Point(184, 2);
            this.cmbDevId.Name = "cmbDevId";
            this.cmbDevId.Size = new System.Drawing.Size(194, 21);
            this.cmbDevId.Sorted = true;
            this.cmbDevId.TabIndex = 71;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(3, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "In-Device Id Refering Field    :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Select Attendence Refering Table :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 278);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "MAP the columns of table to fields :";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(293, 502);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(97, 26);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbSelectError
            // 
            this.lbSelectError.AutoSize = true;
            this.lbSelectError.ForeColor = System.Drawing.Color.Red;
            this.lbSelectError.Location = new System.Drawing.Point(12, 509);
            this.lbSelectError.Name = "lbSelectError";
            this.lbSelectError.Size = new System.Drawing.Size(0, 13);
            this.lbSelectError.TabIndex = 27;
            this.lbSelectError.Visible = false;
            // 
            // rdbnDateTime
            // 
            this.rdbnDateTime.AutoSize = true;
            this.rdbnDateTime.Location = new System.Drawing.Point(6, 19);
            this.rdbnDateTime.Name = "rdbnDateTime";
            this.rdbnDateTime.Size = new System.Drawing.Size(77, 17);
            this.rdbnDateTime.TabIndex = 28;
            this.rdbnDateTime.TabStop = true;
            this.rdbnDateTime.Text = "Date_Time";
            this.rdbnDateTime.UseVisualStyleBackColor = true;
            this.rdbnDateTime.CheckedChanged += new System.EventHandler(this.rdbnDateTime_CheckedChanged);
            // 
            // rdbnDateAndTime
            // 
            this.rdbnDateAndTime.AutoSize = true;
            this.rdbnDateAndTime.Location = new System.Drawing.Point(98, 19);
            this.rdbnDateAndTime.Name = "rdbnDateAndTime";
            this.rdbnDateAndTime.Size = new System.Drawing.Size(77, 17);
            this.rdbnDateAndTime.TabIndex = 29;
            this.rdbnDateAndTime.TabStop = true;
            this.rdbnDateAndTime.Text = "Date, Time";
            this.rdbnDateAndTime.UseVisualStyleBackColor = true;
            this.rdbnDateAndTime.CheckedChanged += new System.EventHandler(this.rdbnDateAndTime_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbnDateAndTime);
            this.groupBox1.Controls.Add(this.rdbnDateTime);
            this.groupBox1.Location = new System.Drawing.Point(15, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 48);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date and Time";
            this.groupBox1.Leave += new System.EventHandler(this.groupBox1_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbnInAndOut);
            this.groupBox2.Controls.Add(this.rdbnInOut);
            this.groupBox2.Location = new System.Drawing.Point(209, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 48);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "In and Out Times";
            this.groupBox2.Leave += new System.EventHandler(this.groupBox2_Leave);
            // 
            // rdbnInAndOut
            // 
            this.rdbnInAndOut.AutoSize = true;
            this.rdbnInAndOut.Location = new System.Drawing.Point(98, 19);
            this.rdbnInAndOut.Name = "rdbnInAndOut";
            this.rdbnInAndOut.Size = new System.Drawing.Size(57, 17);
            this.rdbnInAndOut.TabIndex = 29;
            this.rdbnInAndOut.TabStop = true;
            this.rdbnInAndOut.Text = "In, Out";
            this.rdbnInAndOut.UseVisualStyleBackColor = true;
            this.rdbnInAndOut.CheckedChanged += new System.EventHandler(this.rdbnInAndOut_CheckedChanged);
            // 
            // rdbnInOut
            // 
            this.rdbnInOut.AutoSize = true;
            this.rdbnInOut.Location = new System.Drawing.Point(6, 19);
            this.rdbnInOut.Name = "rdbnInOut";
            this.rdbnInOut.Size = new System.Drawing.Size(55, 17);
            this.rdbnInOut.TabIndex = 28;
            this.rdbnInOut.TabStop = true;
            this.rdbnInOut.Text = "In_out";
            this.rdbnInOut.UseVisualStyleBackColor = true;
            this.rdbnInOut.CheckedChanged += new System.EventHandler(this.rdbnInOut_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Controls.Add(this.comboBox1);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(14, 468);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(378, 25);
            this.panel6.TabIndex = 32;
            this.panel6.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(185, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(191, 21);
            this.comboBox1.TabIndex = 73;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Out-Device Id Refering Field    :";
            // 
            // frmMdbTableColumnsSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 539);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbSelectError);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMdbTableColumnsSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Table and Columns";
            this.Load += new System.EventHandler(this.frmMdbTableColumnsSelect_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.ListBox cboTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDate;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbDevId;
        private System.Windows.Forms.ComboBox cmbEmpId;
        private System.Windows.Forms.ComboBox cmbInOut;
        private System.Windows.Forms.ComboBox cmbOut;
        private System.Windows.Forms.ComboBox cmbIn;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbSelectError;
        private System.Windows.Forms.RadioButton rdbnDateTime;
        private System.Windows.Forms.RadioButton rdbnDateAndTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbnInAndOut;
        private System.Windows.Forms.RadioButton rdbnInOut;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
    }
}