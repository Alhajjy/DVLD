using System;
using System.Windows.Forms;
using Shared;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(int userId)
        {
            InitializeComponent();
            ctrlUserCard1.LoadUserInfo(userId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
