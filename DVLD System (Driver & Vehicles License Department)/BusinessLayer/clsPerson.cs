using System;
using System.Data;
using Shared;

namespace BusinessLayer
{
    public class clsPerson
    {
        public enum enMode { Add, Update }
        public enMode Mode = enMode.Add;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityID { get; set; }
        public string ImagePath { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }



        public clsPerson()
        {
            Mode = enMode.Add;

            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.BirthDate = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityID = -1;
            this.ImagePath = "";
        }
        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                            string LastName, DateTime BirthDate, bool Gender,
                             string Address, string Phone, string Email,
                            int NationalityID, string ImagePath)

        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.BirthDate = BirthDate;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityID = NationalityID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }
        public static clsPerson FindPersonWithID(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime BirthDate = DateTime.Now;
            bool Gender = true;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityID = 0;
            string ImagePath = "";



            if (DataAccessLayer.clsPeopleData.GetPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                             ref LastName, ref BirthDate, ref Gender, ref Address, ref Phone, ref Email,
                             ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, BirthDate, Gender, Address, Phone, Email, NationalityID, ImagePath);
            }
            else
            {
                return null;
            }

        }
        public static clsPerson FindPersonWithNationalNo(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime BirthDate = DateTime.Now;
            bool Gender = true;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityID = 0;
            string ImagePath = "";

            if (DataAccessLayer.clsPeopleData.GetPersonByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                             ref LastName, ref BirthDate, ref Gender, ref Address, ref Phone, ref Email,
                             ref NationalityID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, BirthDate, Gender, Address, Phone, Email, NationalityID, ImagePath);
            }
            else
            {
                return null;
            }

        }
        private bool _AddPerson()
        {
            int personID = -1;
            if (DataAccessLayer.clsPeopleData.InsertPerson(ref personID, NationalNo, FirstName, SecondName, ThirdName, LastName, BirthDate, Gender, Address, Phone, Email, NationalityID, ImagePath))
            {
                this.PersonID = personID;
                return true;
            }
            else { return false; }
        }
        private bool _EditPerson()
        {
            return DataAccessLayer.clsPeopleData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, BirthDate, Gender, Address, Phone, Email, NationalityID, ImagePath);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    bool success = _AddPerson();
                    Mode = enMode.Update;
                    return success;
                case enMode.Update:
                    return _EditPerson();
                default:
                    return false;
            }
        }
        public static DataTable People()
        {
            return DataAccessLayer.clsPeopleData.GetPeople();
        }
        public static bool DeletePerson(int ID)
        {
            return DataAccessLayer.clsPeopleData.DeletePerson(ID);
        }
        public static bool IsPersonExists(string NationalNo)
        {
            return DataAccessLayer.clsPeopleData.IsNationalNumberExists(NationalNo);
        }
        public static bool IsPersonExists(int perosnID)
        {
            return DataAccessLayer.clsPeopleData.IsPersonIDExists(perosnID);
        }
    }
}
