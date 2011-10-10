namespace Time_and_Attendance
{
    partial class TAA
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
            this.btnSrcFilePath = new System.Windows.Forms.Button();
            this.btnCreCompDetails = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSrcFilePath
            // 
            this.btnSrcFilePath.Location = new System.Drawing.Point(50, 33);
            this.btnSrcFilePath.Name = "btnSrcFilePath";
            this.btnSrcFilePath.Size = new System.Drawing.Size(135, 60);
            this.btnSrcFilePath.TabIndex = 0;
            this.btnSrcFilePath.Text = "Source File Path";
            this.btnSrcFilePath.UseVisualStyleBackColor = true;
            this.btnSrcFilePath.Click += new System.EventHandler(this.btnSrcFilePath_Click);
            // 
            // btnCreCompDetails
            // 
            this.btnCreCompDetails.Location = new System.Drawing.Point(50, 99);
            this.btnCreCompDetails.Name = "btnCreCompDetails";
            this.btnCreCompDetails.Size = new System.Drawing.Size(135, 60);
            this.btnCreCompDetails.TabIndex = 1;
            this.btnCreCompDetails.Text = "Create Company Details";
            this.btnCreCompDetails.UseVisualStyleBackColor = true;
            this.btnCreCompDetails.Click += new System.EventHandler(this.btnCreCompDetails_Click);
            // 
            // btnSettingsImport
            // 
            this.btnSettingsImport.Location = new System.Drawing.Point(50, 165);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(135, 60);
            this.btnSettingsImport.TabIndex = 2;
            this.btnSettingsImport.Text = "Settings and Import Data";
            this.btnSettingsImport.UseVisualStyleBackColor = true;
            this.btnSettingsImport.Click += new System.EventHandler(this.btnSettingsImport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSrcFilePath);
            this.groupBox1.Controls.Add(this.btnSettingsImport);
            this.groupBox1.Controls.Add(this.btnCreCompDetails);
            this.groupBox1.Location = new System.Drawing.Point(124, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 252);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // TAA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 363);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TAA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPP Time and Attendance";
            this.Load += new System.EventHandler(this.TAA_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSrcFilePath;
        private System.Windows.Forms.Button btnCreCompDetails;
        private System.Windows.Forms.Button btnSettingsImport;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

