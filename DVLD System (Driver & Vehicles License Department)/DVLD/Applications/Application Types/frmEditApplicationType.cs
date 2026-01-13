using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        public frmEditApplicationType(int TypeID)
        {
            InitializeComponent();
            clsApplicationType.AllApplicationTypes();
            _FillFields();
        }
        clsApplicationType ApplicationType = new clsApplicationType();
        private void _FillFields()
        {
            lblApplicationTypeID.Text = ApplicationType.ApplicationTypeID.ToString();
            tbTitle.Text = ApplicationType.ApplicationTypeTitle;
            tbFees.Text = ApplicationType.ApplicationTypeFees.ToString();
        }
        private void _PrepareApplicationType()
        {
            ApplicationType.ApplicationTypeTitle = tbTitle.Text;
            ApplicationType.ApplicationTypeFees = Convert.ToDouble(tbFees.Text);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _PrepareApplicationType();
            if (ApplicationType.UpdateApplicationType())
                _FillFields();
            else
                MessageBox.Show("Cant Update!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
