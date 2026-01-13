namespace DVLD.Applications.Local_Driving_License
{
    partial class frmAddUpdateLocalDrivingLicenseApplication
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPersonInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlPersonInfoWithFilters1 = new DVLD.People.ctrlPersonInfoWithFilters();
            this.tpAppInfo = new System.Windows.Forms.TabPage();
            this.cbClasses = new System.Windows.Forms.ComboBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSav = new System.Windows.Forms.Button();
            this.btnClos = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpPersonInfo.SuspendLayout();
            this.tpAppInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lbTitle.Location = new System.Drawing.Point(257, 21);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(549, 39);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "New Local Driving License Application";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpPersonInfo);
            this.tabControl1.Controls.Add(this.tpAppInfo);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 80);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(996, 544);
            this.tabControl1.TabIndex = 1;
            // 
            // tpPersonInfo
            // 
            this.tpPersonInfo.Controls.Add(this.btnNext);
            this.tpPersonInfo.Controls.Add(this.ctrlPersonInfoWithFilters1);
            this.tpPersonInfo.Location = new System.Drawing.Point(4, 25);
            this.tpPersonInfo.Name = "tpPersonInfo";
            this.tpPersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonInfo.Size = new System.Drawing.Size(988, 515);
            this.tpPersonInfo.TabIndex = 0;
            this.tpPersonInfo.Text = "Personel Info";
            this.tpPersonInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(855, 467);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(117, 42);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlPersonInfoWithFilters1
            // 
            this.ctrlPersonInfoWithFilters1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonInfoWithFilters1.Location = new System.Drawing.Point(-4, 0);
            this.ctrlPersonInfoWithFilters1.Name = "ctrlPersonInfoWithFilters1";
            this.ctrlPersonInfoWithFilters1.Person = null;
            this.ctrlPersonInfoWithFilters1.Size = new System.Drawing.Size(996, 506);
            this.ctrlPersonInfoWithFilters1.TabIndex = 0;
            this.ctrlPersonInfoWithFilters1.OnPersonSelected += new System.Action<int>(this.ctrlPersonInfoWithFilters1_OnPersonSelected);
            // 
            // tpAppInfo
            // 
            this.tpAppInfo.Controls.Add(this.cbClasses);
            this.tpAppInfo.Controls.Add(this.lblUserName);
            this.tpAppInfo.Controls.Add(this.pictureBox10);
            this.tpAppInfo.Controls.Add(this.label16);
            this.tpAppInfo.Controls.Add(this.lblAppFees);
            this.tpAppInfo.Controls.Add(this.pictureBox9);
            this.tpAppInfo.Controls.Add(this.label13);
            this.tpAppInfo.Controls.Add(this.pictureBox8);
            this.tpAppInfo.Controls.Add(this.label11);
            this.tpAppInfo.Controls.Add(this.lblAppDate);
            this.tpAppInfo.Controls.Add(this.pictureBox7);
            this.tpAppInfo.Controls.Add(this.label9);
            this.tpAppInfo.Controls.Add(this.lblApplicationID);
            this.tpAppInfo.Controls.Add(this.pictureBox5);
            this.tpAppInfo.Controls.Add(this.label6);
            this.tpAppInfo.Location = new System.Drawing.Point(4, 25);
            this.tpAppInfo.Name = "tpAppInfo";
            this.tpAppInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpAppInfo.Size = new System.Drawing.Size(988, 515);
            this.tpAppInfo.TabIndex = 1;
            this.tpAppInfo.Text = "ApplicationInfo";
            this.tpAppInfo.UseVisualStyleBackColor = true;
            // 
            // cbClasses
            // 
            this.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasses.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClasses.FormattingEnabled = true;
            this.cbClasses.Location = new System.Drawing.Point(287, 134);
            this.cbClasses.Name = "cbClasses";
            this.cbClasses.Size = new System.Drawing.Size(235, 26);
            this.cbClasses.TabIndex = 16;
            this.cbClasses.SelectedIndexChanged += new System.EventHandler(this.cbClasses_SelectedIndexChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(283, 217);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(50, 19);
            this.lblUserName.TabIndex = 14;
            this.lblUserName.Text = "[???]";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::DVLD.Properties.Resources.User_32__2;
            this.pictureBox10.Location = new System.Drawing.Point(231, 208);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(34, 38);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 13;
            this.pictureBox10.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(48, 217);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(147, 19);
            this.label16.TabIndex = 12;
            this.label16.Text = "Created By User:";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppFees.Location = new System.Drawing.Point(283, 176);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(53, 19);
            this.lblAppFees.TabIndex = 11;
            this.lblAppFees.Text = "[$$$]";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLD.Properties.Resources.money_32;
            this.pictureBox9.Location = new System.Drawing.Point(231, 167);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(34, 38);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 10;
            this.pictureBox9.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(48, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(149, 19);
            this.label13.TabIndex = 9;
            this.label13.Text = "Application Fees:";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD.Properties.Resources.Renew_Driving_License_32;
            this.pictureBox8.Location = new System.Drawing.Point(231, 122);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(34, 38);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 7;
            this.pictureBox8.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(48, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 19);
            this.label11.TabIndex = 6;
            this.label11.Text = "License Class";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDate.Location = new System.Drawing.Point(283, 88);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(113, 19);
            this.lblAppDate.TabIndex = 5;
            this.lblAppDate.Text = "[??/??/????]";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLD.Properties.Resources.Calendar_32;
            this.pictureBox7.Location = new System.Drawing.Point(231, 79);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(34, 38);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 4;
            this.pictureBox7.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(48, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 19);
            this.label9.TabIndex = 3;
            this.label9.Text = "Application Date:";
            // 
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationID.Location = new System.Drawing.Point(283, 45);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(50, 19);
            this.lblApplicationID.TabIndex = 2;
            this.lblApplicationID.Text = "[???]";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD.Properties.Resources.Number_32;
            this.pictureBox5.Location = new System.Drawing.Point(231, 36);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(34, 38);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "D. L. Appliation ID:";
            // 
            // btnSav
            // 
            this.btnSav.BackColor = System.Drawing.Color.White;
            this.btnSav.Enabled = false;
            this.btnSav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSav.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSav.Image = global::DVLD.Properties.Resources.Save_32;
            this.btnSav.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSav.Location = new System.Drawing.Point(871, 630);
            this.btnSav.Name = "btnSav";
            this.btnSav.Size = new System.Drawing.Size(133, 42);
            this.btnSav.TabIndex = 4;
            this.btnSav.Text = "Save";
            this.btnSav.UseVisualStyleBackColor = false;
            this.btnSav.Click += new System.EventHandler(this.btnSav_Click);
            // 
            // btnClos
            // 
            this.btnClos.BackColor = System.Drawing.Color.White;
            this.btnClos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClos.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClos.Location = new System.Drawing.Point(711, 630);
            this.btnClos.Name = "btnClos";
            this.btnClos.Size = new System.Drawing.Size(137, 42);
            this.btnClos.TabIndex = 3;
            this.btnClos.Text = "Close";
            this.btnClos.UseVisualStyleBackColor = false;
            this.btnClos.Click += new System.EventHandler(this.btnClos_Click);
            // 
            // frmAddUpdateLocalDrivingLicenseApplication
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1018, 679);
            this.Controls.Add(this.btnSav);
            this.Controls.Add(this.btnClos);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbTitle);
            this.Name = "frmAddUpdateLocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmAddUpdateLocalDrivingLicenseApplication_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpPersonInfo.ResumeLayout(false);
            this.tpAppInfo.ResumeLayout(false);
            this.tpAppInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCreatedByUser;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApplicationInfoNext;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private System.Windows.Forms.TabControl tcApplicationInfo;
        private System.Windows.Forms.TabPage tpApplicationInfo;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLocalDrivingLicebseApplicationID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPersonInfo;
        private System.Windows.Forms.TabPage tpAppInfo;
        private People.ctrlPersonInfoWithFilters ctrlPersonInfoWithFilters1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnClos;
        private System.Windows.Forms.Button btnSav;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbClasses;
    }
}