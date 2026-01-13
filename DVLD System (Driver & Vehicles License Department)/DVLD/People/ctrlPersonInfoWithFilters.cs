using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.People
{
    public partial class ctrlPersonInfoWithFilters : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int personId)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(personId);
            }
        }
        public clsPerson Person { get; set; }
        public ctrlPersonInfoWithFilters()
        {
            InitializeComponent();
            cbFilterBy.SelectedIndex = 0;
        }
        public bool LoadPersonInfo(int personId, bool isInitializing)
        {
            if (ctrlPersonInfo1.LoadPersonInfo(personId))
            {
                cbFilterBy.SelectedIndex = 1;
                tbFilterValue.Text = personId.ToString();
                if (!isInitializing)
                    OnPersonSelected(ctrlPersonInfo1.Person.PersonID);
                gbFilters.Enabled = false;
                return true;
            }
            return false;
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(clsPerson.enMode.Add, -1);
            frm.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string value = tbFilterValue.Text.Trim();
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Enter a Valid Value to search!");
                return;
            }
            if (cbFilterBy.SelectedIndex == 0)
            {
                if (!clsPerson.IsPersonExists(value)){
                    MessageBox.Show("Enter an Existing Person National No!");
                    return;
                }
                ctrlPersonInfo1.LoadPersonInfo(value);
            }
            else if (cbFilterBy.SelectedIndex == 1)
            {
                if (int.TryParse(tbFilterValue.Text, out int result) && clsPerson.IsPersonExists(result))
                    ctrlPersonInfo1.LoadPersonInfo(result);
                else
                    MessageBox.Show("Enter a Valid ID! (just using numbers)\nEnter an Existing Person ID");
            }
            OnPersonSelected(ctrlPersonInfo1.Person.PersonID);
        }
    }
}
