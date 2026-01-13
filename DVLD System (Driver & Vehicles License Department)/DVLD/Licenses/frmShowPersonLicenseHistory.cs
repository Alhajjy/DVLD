using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            ctrlPersonInfoWithFilters1.LoadPersonInfo(PersonID, true);
            if (clsDriver.GetDriverIDByPersonID(ref _driverID, PersonID))
                ctrlDriverLicenses1.LoadDriverLicenses(_driverID);
            else
                MessageBox.Show("Cant load licenses");
        }
        int _driverID = -1;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
