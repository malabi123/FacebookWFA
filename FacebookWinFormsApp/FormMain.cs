using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
        }

        FacebookWrapper.LoginResult m_LoginResult;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        string m_AccessToken = "EAAQpHAqlOz0BO2IBiTJCVYHPj3WnYUlzfuXZBYjNSkeLfsKmz5GdNBkogSF9PEqzTsIcwTPBoZBmnSJszELKDKoarYHfpQckxXesdoxLbxyklG0fOInE3zsHHqlsszWu9cR6tZCG68ik8Dw1HWf2uLoKZAQ0IcmBxxDG7Qcd9g9B7kKY9ZBUw04PFXAZDZD";
            
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns");

            if (m_LoginResult == null)
            {
                login();
            }
        }

        private void login()
        {
            m_LoginResult = FacebookService.Connect(m_AccessToken);
/*            m_LoginResult = FacebookService.Login(
                /// (This is Desig Patter's App ID. replace it with your own)
                "1171100321266493",//textBoxAppID.Text,
                /// requested permissions:
                "email",
                "public_profile",
                "user_hometown"
                /// add any relevant permissions
                );*/

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                buttonLogin.Text = $"Logged in as {m_LoggedInUser.Name}";
                buttonLogin.BackColor = Color.LightGreen;
                pictureBoxProfile.ImageLocation = m_LoggedInUser.PictureLargeURL;
                buttonLogin.Enabled = false;
                buttonLogout.Enabled = true;
                labelUserName.Text=  m_LoggedInUser.Name;
                labelHomeTown.Text = "Tel Aviv"; // m_LoggedInUser.Hometown.Name;
                m_AccessToken = m_LoginResult.AccessToken;
                //m_LoggedInUser.
                
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            buttonLogin.BackColor = buttonLogout.BackColor;
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
        }
    }
}
