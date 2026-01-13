using System;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void _SaveCredentials()
        {
            string path = "credentials.txt";
            if (chkRememberMe.Checked)
            {
                string line = $"{tbUserName.Text}---{tbPassword.Text}";
                File.WriteAllText(path, line);
            }
            else
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {

            // credentials loading
            string path = "credentials.txt";
            if (File.Exists(path))
            {
                string line = File.ReadAllText(path).Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(new string[] { "---" }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        tbUserName.Text = parts[0];
                        tbPassword.Text = parts[1];
                        chkRememberMe.Checked = true;
                    }
                }
            }
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
            _SaveCredentials();
            if (!clsGlobal.CurrentUser.Login(tbUserName.Text, tbPassword.Text))
            {
                MessageBox.Show("UserName or Password wrong!");
                return;
            }

            if (!clsUser.IsUserActive(tbUserName.Text))
            {
                MessageBox.Show("Your Account is not active contact your admin!");
                return;
            }

            frmMain frm = new frmMain();
            frm.SigningOut += frmMain_SigningOut;
            this.Hide();
            frm.Show();
        }
    }
}
