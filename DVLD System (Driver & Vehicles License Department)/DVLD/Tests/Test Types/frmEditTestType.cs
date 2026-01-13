using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        public frmEditTestType(int TypeID)
        {
            InitializeComponent();
            TestType = clsTestType.GetTestType(TypeID);
            _FillFields();
        }
        clsTestType TestType = new clsTestType();
        private void _FillFields()
        {
            lblTestTypeID.Text = TestType.TestTypeID.ToString();
            tbTitle.Text = TestType.Title;
            tbDescription.Text = TestType.Description;
            tbFees.Text = TestType.Fees.ToString();
        }
        private void _PrepareTestType()
        {
            TestType.Title = tbTitle.Text;
            TestType.Description = tbDescription.Text;
            TestType.Fees = Convert.ToDecimal(tbFees.Text);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _PrepareTestType();
            if (TestType.UpdateTestType())
                _FillFields();
            else
                MessageBox.Show("Cant Update!");
        }
    }
}
