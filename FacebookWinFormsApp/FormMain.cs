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
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        ChangingPictureBox m_ChangingPictureBoxPosts;
        ChangingPictureBox m_ChangingPictureBoxFriends;
        FacebookWrapper.LoginResult m_LoginResult;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        string m_AccessToken = "EAAQpHAqlOz0BO9wNvZB8rtBQeaj7iyCFkmt92ZAv33LB9um3rDkIi5zObRm0661VDwTkVZCdQ9DKJKqLDZCZAvZCGv955UNu66DdDUP0hYtH6wK3E5IQXQX77wi1c7oyBddTQWpU8c9QJCwHG39JqOC5x3mf4kTcC0yr2cVJElqH2kqXzCw7qWTnWeHwZDZD";

        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            tabControl1.TabPages.Remove(tabPage2);
            panel3.Visible = false;
            initializeUserControlChangingPictureBoxPosts();
            initializeUserControlChangingPictureBoxFriends();
            tabPage1.Text = "Profile";
            tabPage2.Text = "Social";
        }

        private void initializeUserControlChangingPictureBoxFriends()
        {
            m_ChangingPictureBoxFriends = new ChangingPictureBox();
            tabPage2.Controls.Add(m_ChangingPictureBoxFriends);

            Point newLocation = new Point(listBoxUserFriends.Location.X, listBoxUserFriends.Location.Y - 270);
            m_ChangingPictureBoxFriends.Location = newLocation;
            m_ChangingPictureBoxFriends.Height = listBoxUserFriends.Height;
        }

        private void initializeUserControlChangingPictureBoxPosts()
        {
            m_ChangingPictureBoxPosts = new ChangingPictureBox();
            panel3.Controls.Add(m_ChangingPictureBoxPosts);
            tabPage1.Controls.Add(m_ChangingPictureBoxPosts);

            m_ChangingPictureBoxPosts.Location = listBoxUserPosts.Location;
            m_ChangingPictureBoxPosts.Height = listBoxUserPosts.Height - 10;
            m_ChangingPictureBoxPosts.Visible = false;
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
                "user_videos",
                "user_likes"

                //user_link

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
            m_ChangingPictureBoxPosts.Visible = true;
            m_ChangingPictureBoxPosts.BringToFront();
            buttonLogin.BackColor = Color.LightGreen;
            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
            buttonLogout.Visible = true;
            tabControl1.TabPages.Add(tabPage2);
            buttonLogin.Left = buttonLogout.Left;

            showUserPosts();
            showUserFriends();
            showUserLikedPages();
        }

        private void showUserLikedPages()
        {
            ListBoxLikedPages.Items.Clear();
            ListBoxLikedPages.DisplayMember = "Name";

            foreach(Page page in m_LoggedInUser.LikedPages)
            {
                ListBoxLikedPages.Items.Add(page);
            }
            if (listBoxUserPosts.Items.Count == 0)
            {
                MessageBox.Show("You haven't liked any pages yet");
            }
        }

        private void showUserFriends()
        {
            listBoxUserFriends.Items.Clear();
            listBoxUserFriends.DisplayMember = "Name";

            /*foreach (User friend in m_LoggedInUser.Friends)
            {
                listBoxUserFriends.Items.Add(friend);
            }
            if (listBoxUserPosts.Items.Count == 0)
            {
                MessageBox.Show("You don't have any friends");
            }*/

            foreach (string friendName in File.ReadLines("C:\\Users\\pavel\\Source\\Repos\\FacebookWFA\\FacebookWinFormsApp\\Resources\\AllFriends.txt"))
            {
                listBoxUserFriends.Items.Add(friendName);
            }
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
            Post post = listBoxUserPosts.SelectedItem as Post;
            
            if (post != null)
            {
                this.m_ChangingPictureBoxPosts.SetOnePicture(post.PictureURL);
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

        private void ListBoxLikedPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page page= ListBoxLikedPages.SelectedItem as Page;

            if (page != null)
            {
                pictureBoxLikedPages.ImageLocation = page.PictureURL;
            }
        }
    }
}
