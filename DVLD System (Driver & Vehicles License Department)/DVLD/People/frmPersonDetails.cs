using System;
using System.Windows.Forms;
using Shared;

namespace DVLD.People
{
    public partial class frmPersonDetails : Form
    {
        public delegate void RefreshPeopleHandler(object sender, bool IsUpdated);
        public event RefreshPeopleHandler RefreshPeopleEvent;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonInfo1.LoadPersonInfo(PersonID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            RefreshPeopleEvent?.Invoke(this, ctrlPersonInfo1.IsUpdated);
            this.Close();
        }
    }
}
