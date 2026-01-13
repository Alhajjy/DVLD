namespace DVLD.People
{
    partial class frmPeople
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
            this.lbRecords = new System.Windows.Forms.Label();
            this.lbRecords1 = new System.Windows.Forms.Label();
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lbFilterBy = new System.Windows.Forms.Label();
            this.lbManagePeople = new System.Windows.Forms.Label();
            this.cmsPersonConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.pbAddPerson = new System.Windows.Forms.PictureBox();
            this.pbManagePeopleImage = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbFilteringText = new System.Windows.Forms.TextBox();
            this.rbMaleFilter = new System.Windows.Forms.RadioButton();
            this.rbFemaleFilter = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.cmsPersonConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManagePeopleImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbRecords
            // 
            this.lbRecords.AutoSize = true;
            this.lbRecords.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecords.Location = new System.Drawing.Point(88, 642);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(15, 16);
            this.lbRecords.TabIndex = 14;
            this.lbRecords.Text = "0";
            // 
            // lbRecords1
            // 
            this.lbRecords1.AutoSize = true;
            this.lbRecords1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecords1.Location = new System.Drawing.Point(4, 642);
            this.lbRecords1.Name = "lbRecords1";
            this.lbRecords1.Size = new System.Drawing.Size(78, 16);
            this.lbRecords1.TabIndex = 13;
            this.lbRecords1.Text = "# Records:";
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.AllowUserToOrderColumns = true;
            this.dgvPeople.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPeople.BackgroundColor = System.Drawing.Color.White;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.Location = new System.Drawing.Point(7, 264);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.ReadOnly = true;
            this.dgvPeople.Size = new System.Drawing.Size(1154, 359);
            this.dgvPeople.TabIndex = 12;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Gender",
            "Nationality",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(75, 231);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(164, 27);
            this.cbFilterBy.TabIndex = 11;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // lbFilterBy
            // 
            this.lbFilterBy.AutoSize = true;
            this.lbFilterBy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilterBy.Location = new System.Drawing.Point(3, 234);
            this.lbFilterBy.Name = "lbFilterBy";
            this.lbFilterBy.Size = new System.Drawing.Size(66, 19);
            this.lbFilterBy.TabIndex = 10;
            this.lbFilterBy.Text = "Filter By";
            // 
            // lbManagePeople
            // 
            this.lbManagePeople.AutoSize = true;
            this.lbManagePeople.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManagePeople.ForeColor = System.Drawing.Color.Red;
            this.lbManagePeople.Location = new System.Drawing.Point(471, 190);
            this.lbManagePeople.Name = "lbManagePeople";
            this.lbManagePeople.Size = new System.Drawing.Size(171, 25);
            this.lbManagePeople.TabIndex = 9;
            this.lbManagePeople.Text = "Manage People";
            // 
            // cmsPersonConfig
            // 
            this.cmsPersonConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowDetails,
            this.toolStripMenuItem1,
            this.tsmiAddNewPerson,
            this.tsmiEdit,
            this.tsmiDelete,
            this.toolStripMenuItem2,
            this.tsmiSendEmail,
            this.tsmiPhoneCall});
            this.cmsPersonConfig.Name = "contextMenuStrip1";
            this.cmsPersonConfig.Size = new System.Drawing.Size(209, 244);
            // 
            // tsmiShowDetails
            // 
            this.tsmiShowDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsmiShowDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiShowDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.tsmiShowDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiShowDetails.Name = "tsmiShowDetails";
            this.tsmiShowDetails.Size = new System.Drawing.Size(208, 38);
            this.tsmiShowDetails.Text = "ShowDetails";
            this.tsmiShowDetails.Click += new System.EventHandler(this.tsmiShowDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiAddNewPerson
            // 
            this.tsmiAddNewPerson.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiAddNewPerson.Image = global::DVLD.Properties.Resources.AddPerson_32;
            this.tsmiAddNewPerson.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAddNewPerson.Name = "tsmiAddNewPerson";
            this.tsmiAddNewPerson.Size = new System.Drawing.Size(208, 38);
            this.tsmiAddNewPerson.Text = "Add New Person";
            this.tsmiAddNewPerson.Click += new System.EventHandler(this.tsmiAddNewPerson_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiEdit.Image = global::DVLD.Properties.Resources.edit_32;
            this.tsmiEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(208, 38);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiDelete.Image = global::DVLD.Properties.Resources.Delete_32;
            this.tsmiDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(208, 38);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // tsmiSendEmail
            // 
            this.tsmiSendEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiSendEmail.Image = global::DVLD.Properties.Resources.send_email_32;
            this.tsmiSendEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiSendEmail.Name = "tsmiSendEmail";
            this.tsmiSendEmail.Size = new System.Drawing.Size(208, 38);
            this.tsmiSendEmail.Text = "Send Email";
            this.tsmiSendEmail.Click += new System.EventHandler(this.tsmiSendEmail_Click);
            // 
            // tsmiPhoneCall
            // 
            this.tsmiPhoneCall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiPhoneCall.Image = global::DVLD.Properties.Resources.call_32;
            this.tsmiPhoneCall.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiPhoneCall.Name = "tsmiPhoneCall";
            this.tsmiPhoneCall.Size = new System.Drawing.Size(208, 38);
            this.tsmiPhoneCall.Text = "Phone Call";
            this.tsmiPhoneCall.Click += new System.EventHandler(this.tsmiPhoneCall_Click);
            // 
            // pbAddPerson
            // 
            this.pbAddPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddPerson.Image = global::DVLD.Properties.Resources.Add_Person_72;
            this.pbAddPerson.Location = new System.Drawing.Point(1098, 206);
            this.pbAddPerson.Name = "pbAddPerson";
            this.pbAddPerson.Size = new System.Drawing.Size(63, 50);
            this.pbAddPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAddPerson.TabIndex = 18;
            this.pbAddPerson.TabStop = false;
            this.pbAddPerson.Click += new System.EventHandler(this.pbAddPerson_Click);
            // 
            // pbManagePeopleImage
            // 
            this.pbManagePeopleImage.Image = global::DVLD.Properties.Resources.People_400;
            this.pbManagePeopleImage.Location = new System.Drawing.Point(401, 15);
            this.pbManagePeopleImage.Name = "pbManagePeopleImage";
            this.pbManagePeopleImage.Size = new System.Drawing.Size(313, 172);
            this.pbManagePeopleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbManagePeopleImage.TabIndex = 17;
            this.pbManagePeopleImage.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(1045, 629);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 39);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbFilteringText
            // 
            this.tbFilteringText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFilteringText.Location = new System.Drawing.Point(255, 231);
            this.tbFilteringText.Name = "tbFilteringText";
            this.tbFilteringText.Size = new System.Drawing.Size(152, 27);
            this.tbFilteringText.TabIndex = 62;
            this.tbFilteringText.Visible = false;
            // 
            // rbMaleFilter
            // 
            this.rbMaleFilter.AutoSize = true;
            this.rbMaleFilter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbMaleFilter.Location = new System.Drawing.Point(3, 7);
            this.rbMaleFilter.Name = "rbMaleFilter";
            this.rbMaleFilter.Size = new System.Drawing.Size(47, 17);
            this.rbMaleFilter.TabIndex = 63;
            this.rbMaleFilter.TabStop = true;
            this.rbMaleFilter.Text = "Male";
            this.rbMaleFilter.UseVisualStyleBackColor = true;
            // 
            // rbFemaleFilter
            // 
            this.rbFemaleFilter.AutoSize = true;
            this.rbFemaleFilter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbFemaleFilter.Location = new System.Drawing.Point(56, 7);
            this.rbFemaleFilter.Name = "rbFemaleFilter";
            this.rbFemaleFilter.Size = new System.Drawing.Size(59, 17);
            this.rbFemaleFilter.TabIndex = 64;
            this.rbFemaleFilter.TabStop = true;
            this.rbFemaleFilter.Text = "Female";
            this.rbFemaleFilter.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbMaleFilter);
            this.panel1.Controls.Add(this.rbFemaleFilter);
            this.panel1.Location = new System.Drawing.Point(413, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 27);
            this.panel1.TabIndex = 65;
            this.panel1.Visible = false;
            // 
            // frmPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1169, 672);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbFilteringText);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbAddPerson);
            this.Controls.Add(this.pbManagePeopleImage);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.lbRecords1);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lbFilterBy);
            this.Controls.Add(this.lbManagePeople);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPeople";
            this.Text = "frmPeople";
            this.Load += new System.EventHandler(this.frmPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.cmsPersonConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAddPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManagePeopleImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbManagePeopleImage;
        private System.Windows.Forms.Label lbRecords;
        private System.Windows.Forms.Label lbRecords1;
        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lbFilterBy;
        private System.Windows.Forms.Label lbManagePeople;
        private System.Windows.Forms.ContextMenuStrip cmsPersonConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiSendEmail;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhoneCall;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.PictureBox pbAddPerson;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbFilteringText;
        private System.Windows.Forms.RadioButton rbMaleFilter;
        private System.Windows.Forms.RadioButton rbFemaleFilter;
        private System.Windows.Forms.Panel panel1;
    }
}