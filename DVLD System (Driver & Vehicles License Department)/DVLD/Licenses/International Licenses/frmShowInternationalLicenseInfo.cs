using System;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        public frmShowInternationalLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            ctrlDriverInternationalLicenseInfo1.LoadInternationalLicenseInfo(internationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
