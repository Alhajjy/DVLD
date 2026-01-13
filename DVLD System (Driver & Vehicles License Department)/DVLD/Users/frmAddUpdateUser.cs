using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessLayer;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void RefreshPeopleHandler(object sender);
        public event RefreshPeopleHandler RefreshPeopleEvent;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _mode = enMode.Add;
        }
        public frmAddUpdateUser(int userId)
        {
            InitializeComponent();
            _UpdateModeConf(userId);
        }
        enum enMode { Add, Update }
        enMode _mode;
        clsUser User = new clsUser();
        private void _FillFieldsWithUserInfo()
        {
            lblUserID.Text = User.UserID.ToString();
            tbUserName.Text = User.UserName;
            tbPassword.Text = User.Password;
            tbConfirmPassword.Text = User.Password;
            chkIsActive.Checked = User.IsActiveChecker;
        }
        private void _FillUserInfoWithFields()
        {
            if (User.UserID != -1)
                User.mode = clsUser.enMode.Update;
            lblUserID.Text = User.UserID.ToString();
            User.UserName = tbUserName.Text;
            User.Password = tbPassword.Text;
            User.IsActive = chkIsActive.Checked;
        }
        private bool _UpdateModeConf(int userId, int personId = -1, bool isInitializing = false)
        {
            bool success = false;
            _mode = enMode.Update;
            // object
            if (personId == -1)
            {
                User = clsUser.GetUserByID(clsGlobal.CurrentUser, userId);
            }
            else
            {
                User.PersonID = personId;
            }
            success = ctrlPersonInfoWithFilter1.LoadPersonInfo(User.PersonID, true);
            // view
            lbTitle.Text = "Update User";
            if (!string.IsNullOrEmpty(User.UserName))
                _FillFieldsWithUserInfo();
            return success;
        }
        private void _EnableSaveBtn(object sender, EventArgs e)
        {
            if (
                //User.PersonID != -1 &&
                !string.IsNullOrEmpty(tbUserName.Text) &&
                !string.IsNullOrEmpty(tbPassword.Text) &&
                tbPassword.Text == tbConfirmPassword.Text
                ) { btnSave.Enabled = true; return; }
            btnSave.Enabled = false;
        }
        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            tc1.SelectedIndex = 1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _FillUserInfoWithFields();
            if (User.Save(clsGlobal.CurrentUser))
            {
                MessageBox.Show("User information saved successfully.", "Success", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnSave.Enabled = false;
        }
        private void ctrlPersonInfoWithFilter_OnPersonSelected(int obj)
        {
            int personId = obj;
            int userId = -1;

            if (clsUser.IsPersonIsAUser(clsGlobal.CurrentUser, personId, ref userId))
            {
                _UpdateModeConf(userId, personId);
            }
            else
            {
                if (_mode == enMode.Add)
                {
                    User.PersonID = personId;
                    ctrlPersonInfoWithFilter1.LoadPersonInfo(personId, true);
                }
                else
                {
                    ctrlPersonInfoWithFilter1.LoadPersonInfo(User.PersonID, true);
                }
            }
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            User.UserName = tbUserName.Text;
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            User.Password = tbPassword.Text;
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            User.IsActive = chkIsActive.Checked;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            tbUserName.TextChanged += _EnableSaveBtn;
            tbPassword.TextChanged += _EnableSaveBtn;
            tbConfirmPassword.TextChanged += _EnableSaveBtn;
            chkIsActive.CheckedChanged += _EnableSaveBtn;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            RefreshPeopleEvent?.Invoke(this);
        }

        private void frmAddUpdateUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshPeopleEvent?.Invoke(this);
        }
    }
}
