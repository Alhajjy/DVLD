using System;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmShowLicensesInfo : Form
    {
        public frmShowLicensesInfo(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);
        }
    }
}
