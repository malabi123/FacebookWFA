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
using System.Windows.Forms.VisualStyles;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        ChangingPictureBox m_ChangingPictureBoxPosts;
        FacebookWrapper.LoginResult m_LoginResult;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        string m_AccessToken = "EAAQpHAqlOz0BO8cFFEy8TySDf2PrF70oh2RBUGb5da2AkIxVV9Kz4ZA98lip4jBqtyu1cGYDujHPugjtX0YHeCZABuWU53CVIPhZBrSnkjP6xCB6oPLx4ObBFNSt4AxqP69tj6sWnZCudzrW2jKNsZCK3hyXQL6Ph0KPDmaLsvtFZCX9ZB8y6FATxEMOAZDZD";

        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            tabControl1.TabPages.Remove(tabPage2);
            panel3.Visible = false;
            initializeUserControlChangingPictureBoxPosts();
        }

        private void initializeUserControlChangingPictureBoxPosts()
        {
            /*m_ChangingPictureBoxPosts = new ChangingPictureBox();
            panel3.Controls.Add(m_ChangingPictureBoxPosts);
            tabPage1.Controls.Add(m_ChangingPictureBoxPosts);

            m_ChangingPictureBoxPosts.Location = listBoxUserPosts.Location;
            m_ChangingPictureBoxPosts.Height = listBoxUserPosts.Height - 10;
            m_ChangingPictureBoxPosts.Visible = false;*/
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
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

                //user_link
                //user_likes
                
                );*/

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                m_AccessToken = m_LoginResult.AccessToken;
                showLoggedInUser();             
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

        private void showLoggedInUser()
        {
            buttonLogin.Text = $"Logged in as {m_LoggedInUser.Name}";
            pictureBoxProfile.ImageLocation = m_LoggedInUser.PictureLargeURL;
            labelBirthday.Text = m_LoggedInUser.Birthday;
            labelHomeTown.Text = m_LoggedInUser.Location.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();
            DateTime birthday = DateTime.Parse(m_LoggedInUser.Birthday);
            int age = calculateAge(birthday);
            labelUserName.Text = $"{m_LoggedInUser.Name}, {age}";

            panel3.Visible = true;
            //m_ChangingPictureBoxPosts.Visible = true;
            //m_ChangingPictureBoxPosts.BringToFront();
            buttonLogin.BackColor = Color.LightGreen;
            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
            tabControl1.TabPages.Add(tabPage2);
            buttonLogin.Left = buttonLogout.Left;

            showUserPosts();
        }

        private void showUserPosts()
        {
            listBoxUserPosts.Items.Clear();
            listBoxUserPosts.DisplayMember = "Name";
            foreach (Post post in m_LoggedInUser.Posts)
            {
                listBoxUserPosts.Items.Add(post);
            }

            if (listBoxUserPosts.Items.Count == 0)
            {
                MessageBox.Show($"You haven't posted anthing yet{Environment.NewLine}Click here to create a new post");
                //button
            }

            
        }

        private void listBoxUserPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Post post =listBoxUserPosts.SelectedItem as Post;
            
            
            if (post != null)
            {
                this.changingPictureBox1.SetOnePicture(post.PictureURL);
            }
        }

        private void buttonPostStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Status postedStatus = m_LoggedInUser.PostStatus(textBoxNewPost.Text);
                MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
