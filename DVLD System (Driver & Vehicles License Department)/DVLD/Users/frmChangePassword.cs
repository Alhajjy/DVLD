using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
            User = clsGlobal.CurrentUser;
        }
        public frmChangePassword(int userId)
        {
            InitializeComponent();
            User = clsUser.GetUserByID(clsGlobal.CurrentUser, userId);
        }
        bool isFieldsFilled = false;
        bool isCurrentPasswordCorrect = false;
        bool isPasswordsTheSame = false;
        clsUser User = new clsUser();

        private bool _CheckConditions()
        {
            isFieldsFilled = (
                !string.IsNullOrEmpty(tbCurrentPassword.Text.Trim()) &&
                !string.IsNullOrEmpty(tbNewPassword.Text.Trim()) &&
                !string.IsNullOrEmpty(tbConfirmPassword.Text.Trim())
                );
            isCurrentPasswordCorrect = clsUser.CheckUsernameAndPassword(clsGlobal.CurrentUser.UserName, tbCurrentPassword.Text);
            isPasswordsTheSame = (tbConfirmPassword.Text == tbNewPassword.Text);
            return (isFieldsFilled && isCurrentPasswordCorrect && isPasswordsTheSame);
        }
        private void _SaveBtnEnabling(object sender, EventArgs e)
        {
            btnSave.Enabled = _CheckConditions();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (User.UpdatePassword(clsGlobal.CurrentUser, tbConfirmPassword.Text))
            {
                MessageBox.Show("Password Updated Successfully!");
                tbCurrentPassword.Text = "";
                tbNewPassword.Text = "";
                tbConfirmPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Couldn`t Update Password!");
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(User.UserID);
            // save btn enable
            tbCurrentPassword.TextChanged += _SaveBtnEnabling;
            tbNewPassword.TextChanged += _SaveBtnEnabling;
            tbConfirmPassword.TextChanged += _SaveBtnEnabling;
        }

        private void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()) || !clsUser.CheckUsernameAndPassword(clsGlobal.CurrentUser.UserName, tbCurrentPassword.Text))
            {
                errorProvider1.SetError(Temp, "Password is wrong!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (tbNewPassword.Text != tbConfirmPassword.Text)
            {
                errorProvider1.SetError(Temp, "Passwords are not the same!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }
    }
}
