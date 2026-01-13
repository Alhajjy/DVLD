using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
namespace DVLD.People
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
            dgvPeople.ContextMenuStrip = cmsPersonConfig;
            _SetDafaults();
        }
        DataTable _DT = clsPerson.People();
        private void _SetDafaults()
        {
            cbFilterBy.SelectedIndex = 0;
        }
        private void _RefreshTable()
        {
            _DT = clsPerson.People();
            dgvPeople.DataSource = _DT;
            lbRecords.Text = _DT.Rows.Count.ToString();
        }
        private void _Filter(object sender, EventArgs e)
        {
            _DT.DefaultView.RowFilter = string.Empty;
            if (tbFilteringText.Visible)
            {
                if (tbFilteringText.Text == string.Empty)
                    return;
            }
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    _DT.DefaultView.RowFilter = $"PersonID = '{tbFilteringText.Text}'";
                    return;
                case "National No.":
                    _DT.DefaultView.RowFilter = $"NationalNo LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "First Name":
                    _DT.DefaultView.RowFilter = $"FirstName LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Second Name":
                    _DT.DefaultView.RowFilter = $"SecondName LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Third Name":
                    _DT.DefaultView.RowFilter = $"ThirdName LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Last Name":
                    _DT.DefaultView.RowFilter = $"LastName LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Gender":
                    if (rbMaleFilter.Checked)
                        _DT.DefaultView.RowFilter = "Gender = 'Male'";
                    else if (rbFemaleFilter.Checked)
                        _DT.DefaultView.RowFilter = "Gender = 'Female'";
                    else
                        _DT.DefaultView.RowFilter = string.Empty;
                    return;
                case "Nationality":
                    _DT.DefaultView.RowFilter = $"NationalityID LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Phone":
                    _DT.DefaultView.RowFilter = $"Phone LIKE '%{tbFilteringText.Text}%'";
                    return;
                case "Email":
                    _DT.DefaultView.RowFilter = $"Email LIKE '%{tbFilteringText.Text}%'";
                    return;
            }
            _DT.DefaultView.RowFilter = string.Empty;
        }
        private void _onChangeDetected(object sender, bool IsChangingDetected)
        {
            if (IsChangingDetected)
                _RefreshTable();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            tbFilteringText.TextChanged += _Filter;
            rbFemaleFilter.CheckedChanged += _Filter;
            rbMaleFilter.CheckedChanged += _Filter;
        }
        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(clsPerson.enMode.Add);
            frm.RefreshPeopleEvent += _onChangeDetected;
            frm.ShowDialog();
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentCell == null && !dgvPeople.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a person first.");
                return;
            }
            int.TryParse(dgvPeople.CurrentRow.Cells[0].Value.ToString(), out int result);
            frmPersonDetails frm = new frmPersonDetails(result);
            frm.RefreshPeopleEvent += _onChangeDetected;
            frm.ShowDialog();
        }

        private void tsmiSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming soon!");
        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming soon!");
        }

        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(clsPerson.enMode.Add);
            frm.RefreshPeopleEvent += _onChangeDetected;
            frm.ShowDialog();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            string PersonID = dgvPeople.CurrentRow.Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show(
            $"Are You Sure You Want to Delete Person with '{PersonID}' ID ?",
            "Validation",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2
                        );
            if (result == DialogResult.OK)
            {
                //if (!clsPerson.DeletePerson(Convert.ToInt32(PersonID)))
                //{
                //    MessageBox.Show($"Deleting Failed.\n\n{clsShared.lastError}");
                //    return;
                //}
                _RefreshTable();
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(
                clsPerson.enMode.Update,
                Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value)
                );
            frm.RefreshPeopleEvent += _onChangeDetected;
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear previos filters
            _DT.DefaultView.RowFilter = string.Empty;
            // filterators visibility
            if (cbFilterBy.SelectedIndex == 0)
            {
                panel1.Visible = false;
                tbFilteringText.Visible = false;
            }
            else if (cbFilterBy.SelectedIndex == 7)
            {
                tbFilteringText.Visible = false;
                panel1.Left = 255;
                panel1.Visible = true;
                rbMaleFilter.Focus();
            }
            else
            {
                panel1.Visible = false;
                tbFilteringText.Visible = true;
                tbFilteringText.Focus();
            }
        }
    }
}
