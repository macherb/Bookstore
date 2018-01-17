namespace Bookstore
{
    partial class frmMember
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

        public void SetLoginName(string text)
        {
            txtLoginName.Text = text;
        }

        public void SetPassword(string text)
        {
            txtPassword.Text =  text;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.memberDataGridView = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.dtpJoinDate = new System.Windows.Forms.DateTimePicker();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.grpMemberStatus = new System.Windows.Forms.GroupBox();
            this.rdoInactive = new System.Windows.Forms.RadioButton();
            this.rdoActive = new System.Windows.Forms.RadioButton();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.grpContactMethod = new System.Windows.Forms.GroupBox();
            this.rdoTwitter = new System.Windows.Forms.RadioButton();
            this.rdoPhoneText = new System.Windows.Forms.RadioButton();
            this.rdoFacebook = new System.Windows.Forms.RadioButton();
            this.rdoEmail = new System.Windows.Forms.RadioButton();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblJoinDate = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblZipCode = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblSubscriptionID = new System.Windows.Forms.Label();
            this.cmbSubscriptionID = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.memberDataGridView)).BeginInit();
            this.grpMemberStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            this.grpContactMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // memberDataGridView
            // 
            this.memberDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memberDataGridView.Location = new System.Drawing.Point(12, 268);
            this.memberDataGridView.Name = "memberDataGridView";
            this.memberDataGridView.Size = new System.Drawing.Size(1029, 124);
            this.memberDataGridView.TabIndex = 21;
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(230, 398);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 23);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Location = new System.Drawing.Point(171, 398);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(53, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.Location = new System.Drawing.Point(112, 398);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(53, 23);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.Location = new System.Drawing.Point(54, 398);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(52, 23);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(12, 398);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(96, 60);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(100, 20);
            this.txtNumber.TabIndex = 0;
            // 
            // dtpJoinDate
            // 
            this.dtpJoinDate.Location = new System.Drawing.Point(96, 86);
            this.dtpJoinDate.Name = "dtpJoinDate";
            this.dtpJoinDate.Size = new System.Drawing.Size(200, 20);
            this.dtpJoinDate.TabIndex = 1;
            this.dtpJoinDate.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(96, 112);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 20);
            this.txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(96, 138);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 20);
            this.txtLastName.TabIndex = 3;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(96, 164);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 20);
            this.txtAddress.TabIndex = 4;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(96, 190);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(100, 20);
            this.txtCity.TabIndex = 5;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(272, 190);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(100, 20);
            this.txtState.TabIndex = 6;
            // 
            // txtZipCode
            // 
            this.txtZipCode.Location = new System.Drawing.Point(437, 190);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(100, 20);
            this.txtZipCode.TabIndex = 7;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(96, 216);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 20);
            this.txtPhone.TabIndex = 8;
            // 
            // grpMemberStatus
            // 
            this.grpMemberStatus.Controls.Add(this.rdoInactive);
            this.grpMemberStatus.Controls.Add(this.rdoActive);
            this.grpMemberStatus.Location = new System.Drawing.Point(12, 12);
            this.grpMemberStatus.Name = "grpMemberStatus";
            this.grpMemberStatus.Size = new System.Drawing.Size(136, 42);
            this.grpMemberStatus.TabIndex = 9;
            this.grpMemberStatus.TabStop = false;
            this.grpMemberStatus.Text = "Member Status";
            // 
            // rdoInactive
            // 
            this.rdoInactive.AutoSize = true;
            this.rdoInactive.Location = new System.Drawing.Point(67, 19);
            this.rdoInactive.Name = "rdoInactive";
            this.rdoInactive.Size = new System.Drawing.Size(63, 17);
            this.rdoInactive.TabIndex = 1;
            this.rdoInactive.TabStop = true;
            this.rdoInactive.Text = "Inactive";
            this.rdoInactive.UseVisualStyleBackColor = true;
            // 
            // rdoActive
            // 
            this.rdoActive.AutoSize = true;
            this.rdoActive.Location = new System.Drawing.Point(6, 19);
            this.rdoActive.Name = "rdoActive";
            this.rdoActive.Size = new System.Drawing.Size(55, 17);
            this.rdoActive.TabIndex = 0;
            this.rdoActive.TabStop = true;
            this.rdoActive.Text = "Active";
            this.rdoActive.UseVisualStyleBackColor = true;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(272, 242);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(100, 20);
            this.txtLoginName.TabIndex = 10;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(437, 242);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 11;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(96, 242);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 12;
            // 
            // picPhoto
            // 
            this.picPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picPhoto.Location = new System.Drawing.Point(941, 12);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(100, 50);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPhoto.TabIndex = 28;
            this.picPhoto.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.AutoSize = true;
            this.btnUploadImage.Location = new System.Drawing.Point(949, 68);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(92, 23);
            this.btnUploadImage.TabIndex = 20;
            this.btnUploadImage.Text = "Upload Image...";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // grpContactMethod
            // 
            this.grpContactMethod.Controls.Add(this.rdoTwitter);
            this.grpContactMethod.Controls.Add(this.rdoPhoneText);
            this.grpContactMethod.Controls.Add(this.rdoFacebook);
            this.grpContactMethod.Controls.Add(this.rdoEmail);
            this.grpContactMethod.Location = new System.Drawing.Point(154, 12);
            this.grpContactMethod.Name = "grpContactMethod";
            this.grpContactMethod.Size = new System.Drawing.Size(292, 42);
            this.grpContactMethod.TabIndex = 14;
            this.grpContactMethod.TabStop = false;
            this.grpContactMethod.Text = "Preferred Contact Method";
            // 
            // rdoTwitter
            // 
            this.rdoTwitter.AutoSize = true;
            this.rdoTwitter.Location = new System.Drawing.Point(229, 19);
            this.rdoTwitter.Name = "rdoTwitter";
            this.rdoTwitter.Size = new System.Drawing.Size(57, 17);
            this.rdoTwitter.TabIndex = 3;
            this.rdoTwitter.TabStop = true;
            this.rdoTwitter.Text = "Twitter";
            this.rdoTwitter.UseVisualStyleBackColor = true;
            // 
            // rdoPhoneText
            // 
            this.rdoPhoneText.AutoSize = true;
            this.rdoPhoneText.Location = new System.Drawing.Point(141, 19);
            this.rdoPhoneText.Name = "rdoPhoneText";
            this.rdoPhoneText.Size = new System.Drawing.Size(82, 17);
            this.rdoPhoneText.TabIndex = 2;
            this.rdoPhoneText.TabStop = true;
            this.rdoPhoneText.Text = "Phone/Text";
            this.rdoPhoneText.UseVisualStyleBackColor = true;
            // 
            // rdoFacebook
            // 
            this.rdoFacebook.AutoSize = true;
            this.rdoFacebook.Location = new System.Drawing.Point(62, 19);
            this.rdoFacebook.Name = "rdoFacebook";
            this.rdoFacebook.Size = new System.Drawing.Size(73, 17);
            this.rdoFacebook.TabIndex = 1;
            this.rdoFacebook.TabStop = true;
            this.rdoFacebook.Text = "Facebook";
            this.rdoFacebook.UseVisualStyleBackColor = true;
            // 
            // rdoEmail
            // 
            this.rdoEmail.AutoSize = true;
            this.rdoEmail.Location = new System.Drawing.Point(6, 19);
            this.rdoEmail.Name = "rdoEmail";
            this.rdoEmail.Size = new System.Drawing.Size(50, 17);
            this.rdoEmail.TabIndex = 0;
            this.rdoEmail.TabStop = true;
            this.rdoEmail.Text = "Email";
            this.rdoEmail.UseVisualStyleBackColor = true;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(12, 63);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 22;
            this.lblNumber.Text = "Number";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(12, 115);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(57, 13);
            this.lblFirstName.TabIndex = 24;
            this.lblFirstName.Text = "First Name";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(12, 141);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(58, 13);
            this.lblLastName.TabIndex = 25;
            this.lblLastName.Text = "Last Name";
            // 
            // lblJoinDate
            // 
            this.lblJoinDate.AutoSize = true;
            this.lblJoinDate.Location = new System.Drawing.Point(12, 89);
            this.lblJoinDate.Name = "lblJoinDate";
            this.lblJoinDate.Size = new System.Drawing.Size(52, 13);
            this.lblJoinDate.TabIndex = 23;
            this.lblJoinDate.Text = "Join Date";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 167);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 26;
            this.lblAddress.Text = "Address";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(12, 193);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(24, 13);
            this.lblCity.TabIndex = 27;
            this.lblCity.Text = "City";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(202, 193);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 13);
            this.lblState.TabIndex = 28;
            this.lblState.Text = "State";
            // 
            // lblZipCode
            // 
            this.lblZipCode.AutoSize = true;
            this.lblZipCode.Location = new System.Drawing.Point(378, 193);
            this.lblZipCode.Name = "lblZipCode";
            this.lblZipCode.Size = new System.Drawing.Size(50, 13);
            this.lblZipCode.TabIndex = 29;
            this.lblZipCode.Text = "Zip Code";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(12, 219);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(78, 13);
            this.lblPhone.TabIndex = 30;
            this.lblPhone.Text = "Phone Number";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(12, 245);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(73, 13);
            this.lblEmail.TabIndex = 33;
            this.lblEmail.Text = "Email Address";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Location = new System.Drawing.Point(202, 245);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(64, 13);
            this.lblLoginName.TabIndex = 31;
            this.lblLoginName.Text = "Login Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(378, 245);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 32;
            this.lblPassword.Text = "Password";
            // 
            // lblSubscriptionID
            // 
            this.lblSubscriptionID.AutoSize = true;
            this.lblSubscriptionID.Location = new System.Drawing.Point(248, 63);
            this.lblSubscriptionID.Name = "lblSubscriptionID";
            this.lblSubscriptionID.Size = new System.Drawing.Size(92, 13);
            this.lblSubscriptionID.TabIndex = 34;
            this.lblSubscriptionID.Text = "Subscription Type";
            // 
            // cmbSubscriptionID
            // 
            this.cmbSubscriptionID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubscriptionID.FormattingEnabled = true;
            this.cmbSubscriptionID.Items.AddRange(new object[] {
            "G",
            "PG",
            "PG-13",
            "R"});
            this.cmbSubscriptionID.Location = new System.Drawing.Point(346, 60);
            this.cmbSubscriptionID.Name = "cmbSubscriptionID";
            this.cmbSubscriptionID.Size = new System.Drawing.Size(100, 21);
            this.cmbSubscriptionID.TabIndex = 13;
            // 
            // frmMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 433);
            this.Controls.Add(this.lblSubscriptionID);
            this.Controls.Add(this.cmbSubscriptionID);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblZipCode);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblJoinDate);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.grpContactMethod);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.grpMemberStatus);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtZipCode);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.dtpJoinDate);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.memberDataGridView);
            this.Controls.Add(this.picPhoto);
            this.Name = "frmMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Form";
            this.Load += new System.EventHandler(this.frmMember_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memberDataGridView)).EndInit();
            this.grpMemberStatus.ResumeLayout(false);
            this.grpMemberStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            this.grpContactMethod.ResumeLayout(false);
            this.grpContactMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView memberDataGridView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.DateTimePicker dtpJoinDate;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.GroupBox grpMemberStatus;
        private System.Windows.Forms.RadioButton rdoInactive;
        private System.Windows.Forms.RadioButton rdoActive;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.PictureBox picPhoto;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.GroupBox grpContactMethod;
        private System.Windows.Forms.RadioButton rdoFacebook;
        private System.Windows.Forms.RadioButton rdoEmail;
        private System.Windows.Forms.RadioButton rdoPhoneText;
        private System.Windows.Forms.RadioButton rdoTwitter;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblJoinDate;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblZipCode;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblSubscriptionID;
        private System.Windows.Forms.ComboBox cmbSubscriptionID;
    }
}
