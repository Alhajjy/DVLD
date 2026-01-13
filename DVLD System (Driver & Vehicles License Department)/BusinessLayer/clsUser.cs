using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsUser
    {
        public enum enMode { Add, Update }
        public enMode mode { get; set; }
        private bool LoggedIn { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsActiveChecker { get { return IsActive; } }

        // constructors
        public clsUser()
        {
            LoggedIn = false;
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
        }
        private clsUser(int userId, int personId, string userName, string password, bool isActive)
        {
            this.UserID = userId;
            this.PersonID = personId;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
        }
        // shared methods
        private void _ResetUserInfo()
        {
            LoggedIn = false;

            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
        }
        public static bool IsUserExists(int userId)
        {
            return clsUserData.IsUserIDExists(userId);
        }
        public static bool IsPersonIsAUser(clsUser LoggedInUser, int personId, ref int userId)
        {
            if (CheckUsernameAndPassword(LoggedInUser.UserName, LoggedInUser.Password))
            {
                if (clsUserData.IsPersonIDExistsInUsersTable(ref userId, personId))
                    return true;
            }
            return false;
        }
        private bool _Authorize(string userName, string password)
        {
            if (clsUserData.CheckUserNamePassword(userName, password))
            {
                UserName = userName;
                return true;
            }
            return false;
        }
        public static bool CheckUsernameAndPassword(string userName, string password)
        {
            if (clsUserData.CheckUserNamePassword(userName, password))
                return true;
            return false;
        }
        // Login & Sign out
        public void SignOut()
        {
            _ResetUserInfo();
        }
        public bool Login(string userName, string password)
        {
            if (_Authorize(userName, password))
            {
                LoggedIn = true;
                if (this._GetUserByUserName(userName))
                    return true;
            }
            return false;
        }
        public static bool IsUserActive(string userName)
        {
            return clsUserData.IsUserActive(userName);
        }
        // crud operations
        public static DataTable GetUsers(clsUser LoggedInUser)
        {
            if (LoggedInUser?.IsActive != true) return null;
            return clsUserData.GetAllUsers();
        }
        private bool _GetUserByUserName(string username)
        {
            int userId = -1;
            int personId = -1;
            string userName = username;
            string password = "";
            bool isActive = false;

            if (clsUserData.GetUserInfoByUserName(ref userId, ref personId, userName, ref password, ref isActive))
            {
                UserID = userId;
                PersonID = personId;
                UserName = userName;
                Password = password;
                IsActive = isActive;
                return true;
            }

            return false;
        }
        public static clsUser GetUserByID(clsUser LoggedInUser, int userId)
        {
            int personID = -1;
            string userName = "";
            string password = "";
            bool isActive = false;

            if (LoggedInUser.IsActive)
            {
                if (clsUserData.GetUserInfoByUserID(userId, ref personID, ref userName, ref password, ref isActive))
                    return new clsUser(userId, personID, userName, password, isActive);
            }
            return null;
        }
        public static clsUser GetUserByID(int userId)
        {
            int personID = -1;
            string userName = "";
            string password = "";
            bool isActive = false;

            if (clsUserData.GetUserInfoByUserID(userId, ref personID, ref userName, ref password, ref isActive))
                return new clsUser(userId, personID, userName, password, isActive);
            return null;
        }
        private bool _AddUser()
        {
            int userId = -1;

            if (clsUserData.AddUser(ref userId, PersonID, UserName, Password, IsActive))
            {
                UserID = userId;
                return true;
            }
            return false;
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }
        public bool UpdatePassword(clsUser LoggedInUser, string newPassword)
        {
            if (LoggedInUser.IsActive)
                return clsUserData.UpdatePassword(UserID, newPassword);
            return false;
        }
        public bool Save(clsUser LoggedInUser)
        {
            if (!LoggedInUser.IsActive) return false;
            switch (mode)
            {
                case enMode.Add:
                    bool success = _AddUser();
                    mode = enMode.Update;
                    return success;
                case enMode.Update:
                    return _UpdateUser();
                default:
                    return false;
            }
        }
        public static bool DeleteUser(clsUser LoggedInUser, int userId)
        {
            if (!LoggedInUser.IsActive) return false;
            return clsUserData.DeleteUser(userId);
        }
    }
}
