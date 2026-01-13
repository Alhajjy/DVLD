using System;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public clsUser User = new clsUser();
        public void LoadUserInfo(int userId = -1)
        {
            if (!clsUser.IsUserExists(userId)) return;

            if (userId == -1) User = clsGlobal.CurrentUser;
            else User = clsUser.GetUserByID(clsGlobal.CurrentUser, userId);

            if (ctrlPersonInfo1.LoadPersonInfo(User.PersonID))
            {
                lblUserID.Text = User.UserID.ToString();
                lblUserName.Text = User.UserName;
                lblIsActive.Text = (User.IsActiveChecker) ? "Yes" : "No";
            }
        }
    }
}
