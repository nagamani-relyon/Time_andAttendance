namespace Time_and_Attendance
{
    partial class frmSqlTabColSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSqlTabColSelect));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbDatabase = new System.Windows.Forms.Label();
            this.lbServer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ListBox();
            this.lstcol = new System.Windows.Forms.ListBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbnInAndOut = new System.Windows.Forms.RadioButton();
            this.rdbnInOut = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbnDateAndTime = new System.Windows.Forms.RadioButton();
            this.rdbnDateTime = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cmbDevId = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDate = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbIn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbInOut = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbOut = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbEmpId = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbSelectError = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.lbDatabase);
            this.panel1.Controls.Add(this.lbServer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 52);
            this.panel1.TabIndex = 0;
            // 
            // lbDatabase
            // 
            this.lbDatabase.AutoSize = true;
            this.lbDatabase.Location = new System.Drawing.Point(125, 28);
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(0, 13);
            this.lbDatabase.TabIndex = 3;
            // 
            // lbServer
            // 
            this.lbServer.AutoSize = true;
            this.lbServer.Location = new System.Drawing.Point(125, 11);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(0, 13);
            this.lbServer.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ServerName       : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select Attendence Refering Table :";
            // 
            // cboTable
            // 
            this.cboTable.ColumnWidth = 130;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(6, 81);
            this.cboTable.MultiColumn = true;
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(377, 134);
            this.cboTable.Sorted = true;
            this.cboTable.TabIndex = 2;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // lstcol
            // 
            this.lstcol.FormattingEnabled = true;
            this.lstcol.Location = new System.Drawing.Point(367, 280);
            this.lstcol.Name = "lstcol";
            this.lstcol.Size = new System.Drawing.Size(20, 4);
            this.lstcol.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel6.Controls.Add(this.comboBox1);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(5, 467);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(378, 25);
            this.panel6.TabIndex = 42;
            this.panel6.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(185, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(193, 21);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbnInAndOut);
            this.groupBox2.Controls.Add(this.rdbnInOut);
            this.groupBox2.Location = new System.Drawing.Point(200, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 48);
            this.groupBox2.TabIndex = 41;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbnDateAndTime);
            this.groupBox1.Controls.Add(this.rdbnDateTime);
            this.groupBox1.Location = new System.Drawing.Point(6, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 48);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date and Time";
            this.groupBox1.Leave += new System.EventHandler(this.groupBox1_Leave);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 277);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "MAP the columns of table to fields :";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel9.Controls.Add(this.cmbDevId);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Location = new System.Drawing.Point(6, 440);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(378, 25);
            this.panel9.TabIndex = 38;
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
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Device Id Refering Field    :";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.cmbDate);
            this.panel5.Location = new System.Drawing.Point(6, 324);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(378, 25);
            this.panel5.TabIndex = 37;
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.Controls.Add(this.cmbIn);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(6, 381);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(378, 25);
            this.panel4.TabIndex = 35;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cmbInOut);
            this.panel3.Location = new System.Drawing.Point(6, 352);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 25);
            this.panel3.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "In_Out  Refering Field        :";
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
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.cmbOut);
            this.panel7.Location = new System.Drawing.Point(6, 410);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(378, 25);
            this.panel7.TabIndex = 36;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Out Time Refering Field     :";
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cmbEmpId);
            this.panel2.Location = new System.Drawing.Point(6, 293);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 25);
            this.panel2.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Emp Id Refering Field        :";
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
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(308, 498);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbSelectError
            // 
            this.lbSelectError.AutoSize = true;
            this.lbSelectError.ForeColor = System.Drawing.Color.Red;
            this.lbSelectError.Location = new System.Drawing.Point(2, 503);
            this.lbSelectError.Name = "lbSelectError";
            this.lbSelectError.Size = new System.Drawing.Size(0, 13);
            this.lbSelectError.TabIndex = 44;
            this.lbSelectError.Visible = false;
            // 
            // frmSqlTabColSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 526);
            this.Controls.Add(this.lbSelectError);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstcol);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSqlTabColSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Table and Field selection";
            this.Load += new System.EventHandler(this.frmSqlTabColSelect_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbDatabase;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox cboTable;
        private System.Windows.Forms.ListBox lstcol;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbnInAndOut;
        private System.Windows.Forms.RadioButton rdbnInOut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbnDateAndTime;
        private System.Windows.Forms.RadioButton rdbnDateTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox cmbDevId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbInOut;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbOut;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbEmpId;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbSelectError;
    }
}