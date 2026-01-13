using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.People
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
            llEditPersonInfo.Enabled = false;
        }
        public clsPerson Person = new clsPerson();
        public bool IsUpdated = false;
        private void _ResetDefaultImage()
        {
            if (string.IsNullOrEmpty(Person.ImagePath))
                pbPersonImage.Image = (Person.Gender) ? Properties.Resources.male_user_128 : Properties.Resources.female_128;
        }
        public void _FillPersonDataInFields()
        {
            lblPersonID.Text = Person.PersonID.ToString();
            lblFullName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
            lblNationalNo.Text = Person.NationalNo;
            lblBirthDate.Text = Person.BirthDate.ToShortDateString();
            lblGendor.Text = Person.Gender ? "Male" : "Female";
            lblAddress.Text = Person.Address;
            lblPhone.Text = Person.Phone;
            lblEmail.Text = Person.Email;
            lblCountry.Text = clsCountries.GetCountryName(Person.NationalityID);
            if (!string.IsNullOrEmpty(Person.ImagePath))
                pbPersonImage.ImageLocation = Person.ImagePath;
            else
                _ResetDefaultImage();
        }
        public bool LoadPersonInfo(string NationalNo)
        {
            Person.NationalNo = NationalNo;
            if (!clsPerson.IsPersonExists(NationalNo)){
                return false;
            }
            Person = clsPerson.FindPersonWithNationalNo(Person.NationalNo);
            _FillPersonDataInFields();
            llEditPersonInfo.Enabled = true;
            return true;
        }
        public bool LoadPersonInfo(int personId)
        {
            Person.PersonID = personId;
            if (!clsPerson.IsPersonExists(personId))
            {
                return false;
            }
            Person = clsPerson.FindPersonWithID(personId);
            _FillPersonDataInFields();
            llEditPersonInfo.Enabled = true;
            return true;
        }
        private void _onChangeDetected(object sender, bool IsChangingDetected)
        {
            if (IsChangingDetected)
                if (LoadPersonInfo(Person.PersonID))
                    IsUpdated = true;
        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(clsPerson.enMode.Update, Person.PersonID);
            frm.RefreshPeopleEvent += _onChangeDetected;
            frm.ShowDialog();
        }
    }
}
