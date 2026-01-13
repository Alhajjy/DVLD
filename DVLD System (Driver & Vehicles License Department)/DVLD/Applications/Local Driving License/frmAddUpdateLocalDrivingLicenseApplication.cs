using System;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using Shared;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public delegate void RefreshTableHandler(object sender, bool IsChangingDetected);
        public event RefreshTableHandler RefreshTableEvent;
        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }
        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            LocalDrivingLicenseApp = clsLocalDrivingLicenseApp.FindLocalLicenseAppWithID(LocalDrivingLicenseAppID);
        }
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;
        clsLocalDrivingLicenseApp LocalDrivingLicenseApp = new clsLocalDrivingLicenseApp();
        DataTable LicenseClasses_dt = clsLicenseClass.GetLicenseClasses();
        // Changing detection is for delegate
        bool ChangingDetected = false;
        clsApplicationType ApplicationTypeInfo
        {
            get
            {
                return clsApplicationType.GetApplicationType((int)clsApplication.enApplicationType.NewDrivingLicense);
            }
        }

        private void _SetData()
        {
            lblAppDate.Text = DateTime.Now.ToString();
            lblAppFees.Text = ApplicationTypeInfo.ApplicationTypeFees.ToString();
            lblUserName.Text = clsGlobal.CurrentUser.UserName;
        }
        private void _LoadDataToForm()
        {
            ctrlPersonInfoWithFilters1.LoadPersonInfo(LocalDrivingLicenseApp.PersonID, true);
            lblApplicationID.Text = LocalDrivingLicenseApp.ApplicationID.ToString();
            lblAppDate.Text = LocalDrivingLicenseApp.ApplicationDate.ToString();
            cbClasses.SelectedIndex = LocalDrivingLicenseApp.LicenseClassID - 1;
            lblAppFees.Text = LocalDrivingLicenseApp.PaidFees.ToString();
            lblUserName.Text = LocalDrivingLicenseApp.CreatedByUserID.ToString();
        }
        private void _SaveBtnEnabling()
        {
            if (_Mode == enMode.Update)
            {
                btnSav.Enabled = cbClasses.SelectedIndex + 1 != LocalDrivingLicenseApp.LicenseClassID ? true : false;
            }
            else if (_Mode == enMode.Add)
            {
                if (clsPerson.IsPersonExists(LocalDrivingLicenseApp.PersonID))
                    btnSav.Enabled = true;
                else
                    btnSav.Enabled = false;
            }
        }
        private void _CollectDataToAppObject()
        {
            LocalDrivingLicenseApp.ApplicationDate = DateTime.Now;
            LocalDrivingLicenseApp.Status = clsApplication.enStatus.New;
            LocalDrivingLicenseApp.LastStatusDate = DateTime.Now;
            LocalDrivingLicenseApp.PaidFees = (decimal)ApplicationTypeInfo.ApplicationTypeFees;
            LocalDrivingLicenseApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            LocalDrivingLicenseApp.LicenseClassID = cbClasses.SelectedIndex + 1;
        }
        private void _UpdateModeConf()
        {
            if (_Mode == enMode.Update)
            {
                lbTitle.Text = "Update Local Driving License Application";
                _LoadDataToForm();
                btnSav.Enabled = false;
            }
        }
        private void _SetDefaults()
        {
            // global
            if (LicenseClasses_dt?.Rows.Count > 0)
            {
                cbClasses.DataSource = LicenseClasses_dt;
                cbClasses.DisplayMember = "ClassName";
                cbClasses.ValueMember = "LicenseClassID";
                cbClasses.SelectedIndex = _Mode == enMode.Add ? 2 : (int)LocalDrivingLicenseApp.ApplicationTypeID;
            }
            else
            {
                MessageBox.Show("No license classes available.");
            }
            if (_Mode == enMode.Add)
                _SetData();
            else
                _UpdateModeConf();

        }
        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetDefaults();
        }
        private void btnClos_Click(object sender, EventArgs e)
        {
            RefreshTableEvent?.Invoke(this, ChangingDetected);
            this.Close();
        }

        private void btnSav_Click(object sender, EventArgs e)
        {
            _CollectDataToAppObject();
            if (_Mode == enMode.Add && !clsLocalDrivingLicenseApp.DoesPersonAvailableToOpenApplication(LocalDrivingLicenseApp.PersonID, LocalDrivingLicenseApp.LicenseClassID))
            {
                MessageBox.Show("This Person already have an active application!");
                return;
            }
            if (LocalDrivingLicenseApp.Save())
            {
                ChangingDetected = true;
                if (_Mode == enMode.Add)
                {
                    MessageBox.Show("Application Added Sucessfully..");
                    // change to update mode
                    _Mode = enMode.Update;
                    _UpdateModeConf();
                }
                else
                {
                    MessageBox.Show("Application Updated Sucessfully..");
                    _LoadDataToForm();
                }
            }
            else
            {
                MessageBox.Show($"Error Occurred while saving the application.\nError: {clsShared.lastError}");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void ctrlPersonInfoWithFilters1_OnPersonSelected(int obj)
        {
            LocalDrivingLicenseApp.PersonID = obj;
            LocalDrivingLicenseApp.Person = clsPerson.FindPersonWithID(obj);
            _SaveBtnEnabling();
        }

        private void cbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SaveBtnEnabling();
        }
    }
}
