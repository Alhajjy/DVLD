using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Users
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void _RefreshUsersTable()
        {
            dt = clsUser.GetUsers(clsGlobal.CurrentUser);
            dgvUsers.DataSource = dt;
            lbRecords.Text = dt.Rows.Count.ToString();
        }
        private void _RefreshTable(object sender)
        {
            _RefreshUsersTable();
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.RefreshPeopleEvent += _RefreshTable;
            frm.ShowDialog();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersTable();
            cbFilterBy.SelectedIndex = 0;
            dgvUsers.Columns[0].Width = 70;
            dgvUsers.Columns[1].Width = 70;
            dgvUsers.Columns[4].Width = 70;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentCell == null && !dgvUsers.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a user first.");
                return;
            }
            if (int.TryParse(dgvUsers.CurrentRow.Cells[0].Value.ToString(), out int result))
            {
                frmUserInfo frm = new frmUserInfo(result);
                frm.ShowDialog();
            }
        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.RefreshPeopleEvent += _RefreshTable;
            frm.ShowDialog();
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(dgvUsers.CurrentRow.Cells[0].Value.ToString(), out int result)) return;
            frmAddUpdateUser frm = new frmAddUpdateUser(result);
            frm.RefreshPeopleEvent += _RefreshTable;
            frm.ShowDialog();
        }
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            // Validation: ensure a valid row is selected
            if (dgvUsers.CurrentCell == null || dgvUsers.CurrentRow.IsNewRow)
            {
                MessageBox.Show("select a user first.");
                return;
            }

            if (!int.TryParse(dgvUsers.CurrentRow.Cells[0].Value?.ToString(), out int userId))
            {
                MessageBox.Show("Invalid user ID.");
                return;
            }

            // Confirmation before deletion
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the user with ID {userId}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            // Perform deletion
            if (clsUser.DeleteUser(clsGlobal.CurrentUser, userId))
            {
                MessageBox.Show("User deleted successfully!");
                _RefreshUsersTable();
            }
            else
            {
                MessageBox.Show($"Couldn't delete the user with ID {userId}!");
            }
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (int.TryParse(dgvUsers.CurrentRow.Cells[0].Value.ToString(), out int result))
            {
                frmChangePassword frm = new frmChangePassword(result);
                frm.ShowDialog();
                _RefreshUsersTable();
            }
        }

        private void tsmiSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is coming soon!");
        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is coming soon!");
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                dt.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lbRecords.Text = dt.Rows.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dt.DefaultView.RowFilter = "";
                lbRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lbRecords.Text = dt.Rows.Count.ToString();
        }
    }
}
