using System;
using System.Windows.Forms;
using BusinessLayer;
using DVLD.People;

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        clsApplication Application = new clsApplication();
        void _FillFieldsWithInfo()
        {
            lblApplicationID.Text = Application.ApplicationID.ToString();
            lblStatus.Text = Application.Status.ToString();
            lblFees.Text = Application.PaidFees.ToString();
            lblType.Text = Application.ApplicationType.ApplicationTypeTitle.ToString();
            lblApplicant.Text = Application.Person.FullName.ToString();
            lblDate.Text = Application.ApplicationDate.ToString();
            lblStatus.Text = Application.Status.ToString();
            lblStatusDate.Text = Application.LastStatusDate.ToString();
            lblCreatedByUser.Text = Application.User.UserName.ToString();
        }
        public bool LoadAppInfo(int AppID)
        {
            if (!clsApplication.IsApplicationExists(AppID))
            {
                MessageBox.Show("Base Application Does`nt Exist!");
                return false;
            }
            else
            {
                Application = clsApplication.FindApplicationWithID(AppID);
                _FillFieldsWithInfo();
                return true;
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(Application.PersonID);
            frm.ShowDialog();
        }
    }
}
