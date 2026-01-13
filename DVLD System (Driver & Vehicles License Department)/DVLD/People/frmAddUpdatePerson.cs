using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using Shared;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        // delegate & event
        public delegate void RefreshPeopleHandler(object sender, bool IsChangingDetected);
        public event RefreshPeopleHandler RefreshPeopleEvent;
        // Constructor
        public frmAddUpdatePerson(clsPerson.enMode mode, int PersonID = -1)
        {
            InitializeComponent();
            if (mode == clsPerson.enMode.Update)
            {
                Person = clsPerson.FindPersonWithID(PersonID);
            }
            else
            {
                Person = new clsPerson();
            }
            Person.Mode = mode;
        }
        // Fields
        DataTable dtCountries = new DataTable();
        bool IsCustomImageExists = false;
        bool ChangingDetected = false;
        clsPerson Person { get; set; }

        // ---> Resets
        private void _AddModeConfig()
        {
            dtpBirthDate.Value = DateTime.Now.AddYears(-19);
            IsCustomImageExists = false;
            rbMale.Checked = true;
            cbCountries.SelectedValue = clsCountries.GetCountryID("Syria");
        }
        private void _UpdateModeConfig(int PersonID)
        {
            lbTitle.Text = "Update Person";
            this.Text = "Update Person";
            // Fill Fields
            lbPersonId.Text = Person.PersonID.ToString();
            tbNationalNo.Text = Person.NationalNo;
            tbFirstName.Text = Person.FirstName;
            tbSecondName.Text = Person.SecondName;
            tbThirdName.Text = Person.ThirdName;
            tbLastName.Text = Person.LastName;
            dtpBirthDate.Value = Person.BirthDate;
            if (Person.Gender) rbMale.Checked = true;
            else rbFemale.Checked = true;
            tbAddress.Text = Person.Address;
            tbPhone.Text = Person.Phone;
            tbEmail.Text = Person.Email;
            cbCountries.SelectedValue = Person.NationalityID;
            if (!string.IsNullOrEmpty(Person.ImagePath))
            {
                IsCustomImageExists = true;
                pbImage.ImageLocation = Person.ImagePath;
            } else
                pbImage.Image = (rbFemale.Checked) ? Properties.Resources.female_128 : Properties.Resources.male_user_128;
        }
        private void _ResetDefaults()
        {
            // global
            try
            {
                BusinessLayer.clsCountries.GetCountries(ref dtCountries);
                cbCountries.DataSource = dtCountries;
                cbCountries.DisplayMember = "CountryName";
                cbCountries.ValueMember = "CountryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dtpBirthDate.MinDate = DateTime.Now.AddYears(-100);
            dtpBirthDate.MaxDate = DateTime.Now.AddYears(-18);

            if (Person.Mode == clsPerson.enMode.Add)
                _AddModeConfig();
            else
                _UpdateModeConfig(Person.PersonID);
            btnSave.Enabled = false;
        }
        // ---> Track and Modify
        private void _ResetDefaultImage(object sender, EventArgs e)
        {
            if (!IsCustomImageExists)
            {
                pbImage.Image = (rbFemale.Checked) ? Properties.Resources.female_128 : Properties.Resources.male_user_128;
            }
        }
        void _ValidateSaveButton(object sender, EventArgs e)
        {
            if (((Control)sender).Name == tbNationalNo.Name)
            {
                int ID = -1;
                if (Person.Mode == clsPerson.enMode.Update) ID = Convert.ToInt32(lbPersonId.Text);
                TextBox Temp = ((TextBox)sender);
            }

            bool hasChanged = true;
            if (Person.Mode == clsPerson.enMode.Update)
            {
                hasChanged = tbFirstName.Text != Person.FirstName ||
                                  tbSecondName.Text != Person.SecondName ||
                                    tbThirdName.Text != Person.ThirdName ||
                                    tbLastName.Text != Person.LastName ||
                                    tbNationalNo.Text != Person.NationalNo ||
                                    dtpBirthDate.Value != Person.BirthDate ||
                                    (((Person.Gender == true) && rbMale.Checked == false) || ((Person.Gender == false) && rbMale.Checked == true))
                                    || tbAddress.Text != Person.Address ||
                                    tbPhone.Text != Person.Phone ||
                                    tbEmail.Text != Person.Email ||
                                    (cbCountries.SelectedValue != null && Convert.ToInt32(cbCountries.SelectedValue) != Person.NationalityID) ||
                                    (pbImage.ImageLocation != Person.ImagePath);
            }
            bool isRequiredFieldsFilled = !string.IsNullOrWhiteSpace(tbFirstName.Text) &&
        !string.IsNullOrWhiteSpace(tbLastName.Text) &&
                          !string.IsNullOrWhiteSpace(tbNationalNo.Text);



            bool isValidNationalNo = false;
            if (tbNationalNo.Text != Person.NationalNo)
            {
                isValidNationalNo = !clsPerson.IsPersonExists(tbNationalNo.Text);
            }
            else if (!string.IsNullOrEmpty(tbNationalNo.Text))
            {
                isValidNationalNo = true;
            }
            bool savingAbility = isRequiredFieldsFilled && hasChanged && isValidNationalNo;
            btnSave.Enabled = savingAbility;
        }
        // ---> Save
        private bool _HandleImageSaving()
        {
            // delete the old image if exists.
            if (Person.ImagePath != pbImage.ImageLocation)
            {
                if (Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(Person.ImagePath);
                    }
                    catch (IOException IOX)
                    {
                        MessageBox.Show($"Error when deleting old image): {IOX}");
                    }
                }
            }

            // copy image to DVLD-Images
            if (!string.IsNullOrEmpty(pbImage.ImageLocation))
            {

                string SourceImageFile = pbImage.ImageLocation.ToString();

                if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                {
                    pbImage.ImageLocation = SourceImageFile;
                    Person.ImagePath = SourceImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error when copying image)");
                    return false;
                }
            }
            else
            {
                Person.ImagePath = "";
                MessageBox.Show($"done, with empty image)");
                return true;
            }
        }
        private bool _FillPersonObjectBeforeSaving()
        {
            // Collect Data
            if (Person.Mode == clsPerson.enMode.Update)
            {
                Person.PersonID = Convert.ToInt32(lbPersonId.Text);
            }
            else
                Person.PersonID = -1;
            Person.FirstName = tbFirstName.Text.Trim();
            Person.NationalNo = tbNationalNo.Text.Trim();
            Person.SecondName = tbSecondName.Text.Trim();
            Person.ThirdName = tbThirdName.Text.Trim();
            Person.LastName = tbLastName.Text.Trim();
            Person.BirthDate = dtpBirthDate.Value;
            Person.Gender = rbMale.Checked;
            Person.Address = tbAddress.Text.Trim();
            Person.Phone = tbPhone.Text.Trim();
            Person.Email = tbEmail.Text.Trim();
            if (cbCountries.SelectedValue != null && cbCountries.SelectedValue != DBNull.Value)
                Person.NationalityID = Convert.ToInt32(cbCountries.SelectedValue);

            if (!_HandleImageSaving())
                return false;

            return true;
        }
        private void _SetFeildAsRequired(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        // Events
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaults();
            // common functions to multiple events
            // reset Default Image
            rbFemale.CheckedChanged += _ResetDefaultImage;
            rbMale.CheckedChanged += _ResetDefaultImage;
            // set field as required
            tbFirstName.Validating += _SetFeildAsRequired;
            tbLastName.Validating += _SetFeildAsRequired;
            tbNationalNo.Validating += _SetFeildAsRequired;
            // validate Save Button
            tbNationalNo.TextChanged += _ValidateSaveButton;
            tbFirstName.TextChanged += _ValidateSaveButton;
            tbSecondName.TextChanged += _ValidateSaveButton;
            tbThirdName.TextChanged += _ValidateSaveButton;
            tbLastName.TextChanged += _ValidateSaveButton;
            rbMale.CheckedChanged += _ValidateSaveButton;
            dtpBirthDate.ValueChanged += _ValidateSaveButton;
            tbPhone.TextChanged += _ValidateSaveButton;
            tbEmail.TextChanged += _ValidateSaveButton;
            tbAddress.TextChanged += _ValidateSaveButton;
            cbCountries.SelectedIndexChanged += _ValidateSaveButton;
            pbImage.LocationChanged += _ValidateSaveButton;
            lklSetImage.LinkClicked += _ValidateSaveButton;
            lklRemoveImage.LinkClicked += _ValidateSaveButton;
        }
        private void lklRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsCustomImageExists)
            {
                pbImage.Image = (rbMale.Checked == true) ? Properties.Resources.male_user_128 : Properties.Resources.female_128;
                pbImage.ImageLocation = null;
                IsCustomImageExists = false;
            }
        }
        private void lklSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dlg.FileName;
                    pbImage.ImageLocation = filePath;
                    pbImage.Image = Image.FromFile(filePath);
                    IsCustomImageExists = true;
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            RefreshPeopleEvent?.Invoke(this, ChangingDetected);
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_FillPersonObjectBeforeSaving())
            {
                bool ChangeModeToUpdate = Person.Mode == clsPerson.enMode.Add;
                if (Person.Save())
                {
                    ChangingDetected = true;
                    if (ChangeModeToUpdate)
                        MessageBox.Show($"Added with ID {Person.PersonID}");
                    else
                    {
                        MessageBox.Show("Updated Successfully");
                    }
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show($"Error.. \n{clsShared.lastError}");
                }

                if (ChangeModeToUpdate)
                {
                    Person.Mode = clsPerson.enMode.Update;
                    _UpdateModeConfig(Person.PersonID);
                }
            }
            else
            {
                MessageBox.Show($"Error..\n Fill Fields Correct\n Check transfering data to object");
            }
        }
    }
}
