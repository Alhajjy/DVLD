using System;
using System.Windows.Forms;
using BusinessLayer;
using Microsoft.Win32;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        // Specify the Registry key and path
        string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
        string UsernameKey = "Username";
        string PasswordKey = "Password";
        private void _GetCredentialsFromWinRegistry()
        {
            try
            {
                // Read the value from the Registry
                string usernameValue = Registry.GetValue(keyPath, UsernameKey, null) as string;
                string passwordValue = Registry.GetValue(keyPath, PasswordKey, null) as string;


                if (usernameValue != null && passwordValue != null)
                {
                    tbUserName.Text = usernameValue;
                    tbPassword.Text = passwordValue;
                }
            }
            catch (Exception ex)
            {
                // log this error somewhere later
            }
        }
        private void _SaveCredentialsToWinRegistry()
        {
            string UserNameValue = tbUserName.Text.Trim();
            string PasswordValue = tbPassword.Text.Trim();

            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, UsernameKey, UserNameValue, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordKey, PasswordValue, RegistryValueKind.String);

                // MessageBox.Show($"Saved to the Registery:\n{UsernameKey} => {UserNameValue}\n{PasswordKey}  => {PasswordValue[0]}*******{PasswordValue[PasswordValue.Length - 1]}");
                // log this success somewhere later
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"An error occurred: {ex.Message}");
                // log this success somewhere later
            }
        }
        private void _DeleteCredentionalsFromWinRegistry()
        {
            string keyPathForDeleting = @"SOFTWARE\DVLD";
            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPathForDeleting, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue(UsernameKey);
                            key.DeleteValue(PasswordKey);
                        }
                        else
                        {
                            // log this error somewhere later
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // log this error somewhere later
            }
            catch (Exception ex)
            {
                // log this error somewhere later
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _GetCredentialsFromWinRegistry();
        }
        private void frmMain_SigningOut(object sender, bool Exit)
        {
            if (Exit)
                Application.Exit();
            else
                this.Show();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUserName.Text) ||
                string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("There are empty fields, please fill them.");
                return;
            }
            if (!clsGlobal.CurrentUser.Login(tbUserName.Text.Trim(), tbPassword.Text.Trim()))
            {
                MessageBox.Show("UserName or Password wrong!");
                return;
            }

            if (!clsUser.IsUserActive(tbUserName.Text.Trim()))
            {
                MessageBox.Show("Your Account is not active contact your admin!");
                return;
            }
            if (chkRememberMe.Checked)
                _SaveCredentialsToWinRegistry();
            else
                _DeleteCredentionalsFromWinRegistry();
            frmMain frm = new frmMain();
            frm.SigningOut += frmMain_SigningOut;
            this.Hide();
            frm.Show();
        }
    }
}
