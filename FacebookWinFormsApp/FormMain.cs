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
        string m_AccessToken = "EAAQpHAqlOz0BO8cFFEy8TySDf2PrF70oh2RBUGb5da2AkIxVV9Kz4ZA98lip4jBqtyu1cGYDujHPugjtX0YHeCZABuWU53CVIPhZBrSnkjP6xCB6oPLx4ObBFNSt4AxqP69tj6sWnZCudzrW2jKNsZCK3hyXQL6Ph0KPDmaLsvtFZCX9ZB8y6FATxEMOAZDZD";


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
            /*m_LoginResult = FacebookService.Login(
                /// (This is Desig Patter's App ID. replace it with your own)
                "1171100321266493",//textBoxAppID.Text,
                /// requested permissions:
                "email",
                "public_profile",
                "user_hometown",
                "user_birthday",
                "user_friends",
                "user_gender",
                "user_location",
                "user_photos",
                "user_posts",
                "user_videos"


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

                DateTime birthday = DateTime.Parse(m_LoggedInUser.Birthday);
                int age = calculateAge(birthday);

                labelUserName.Text = $"{m_LoggedInUser.Name}, {age}";
                labelBirthday.Text = m_LoggedInUser.Birthday;
                labelHomeTown.Text = m_LoggedInUser.Location.Name;
                labelGender.Text = m_LoggedInUser.Gender.ToString();
                m_AccessToken = m_LoginResult.AccessToken; 
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

        private static int calculateAge(DateTime i_BirthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - i_BirthDate.Year;
 
            if (i_BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        private void buttonShowListAlbums_Click(object sender, EventArgs e)
        {
            listBoxAlbums.Items.Clear();
            listBoxAlbums.DisplayMember = "Name";

            foreach (Album album in m_LoggedInUser.Albums)
            {
                listBoxAlbums.Items.Add(album);
            }

            if (listBoxAlbums.Items.Count == 0)
            {
                MessageBox.Show("You have no friends :(");
            }
        }
    }
}
