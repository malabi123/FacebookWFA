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
        FacebookWrapper.LoginResult m_LoginResult;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        string m_AccessToken = "EAAQpHAqlOz0BOwYE5WXzZBdgWZAm9YZBZCwnYlarRVZBecQMK0hvF8v51CJLZCWKZBXasvny8pQwaZAtE2zZBdeunzJHSNDUDPnQCAPcQfWFbnZCsibmmeXOLZAJoY1IRF8R9ybq7LZAIp0En1WNM1JEypev3BNVJ94rLUzk8eivnduwG40F2kZAvk1yfG8YZCDgZDZD";


        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            tabControl1.TabPages.Remove(tabPageSocial);
            tabControl1.TabPages.Remove(tabPageSettings);
            tabControl1.TabPages.Remove(tabPageFeed);
            panel3.Visible = false;
            initializeUserControlChangingPictureBoxPosts();
            tabPageProfile.Text = "Profile";
            tabPageSocial.Text = "Social";
        }

        private void initializeUserControlChangingPictureBoxPosts()
        {
            m_ChangingPictureBoxPosts = new ChangingPictureBox();
            panel3.Controls.Add(m_ChangingPictureBoxPosts);
            tabPageProfile.Controls.Add(m_ChangingPictureBoxPosts);

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
                "user_likes",
                "user_events"

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
            tabControl1.TabPages.Add(tabPageSocial);
            tabControl1.TabPages.Add(tabPageSettings);
            tabControl1.TabPages.Add(tabPageFeed);
            buttonLogin.Left = buttonLogout.Left;

            showUserPosts();
            showUserFriends();
            showUserLikedPages();
            showOnlineFriends();
            setChangeSettings(); 
        }

        private void setChangeSettings()
        {
            textBoxChangeUsername.Text = m_LoggedInUser.Name;
            textBoxChangeHomeTown.Text = m_LoggedInUser.Location.Name;
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
            listBoxUserFriends.DisplayMember = "FullName";

            /*foreach (User friend in m_LoggedInUser.Friends)
            {
                listBoxUserFriends.Items.Add(friend);
            }
            if (listBoxUserPosts.Items.Count == 0)
            {
                MessageBox.Show("You don't have any friends");
            }*/

            foreach (FakeFacebookFriend fakeFriend in FakeFriendsGenerator.sr_FakeFriends)
            {
                listBoxUserFriends.Items.Add(fakeFriend);
            }
        }

        private void showOnlineFriends()
        {
            listBoxOnlineFriends.Items.Clear();
            listBoxOnlineFriends.DisplayMember = "FullName";

            foreach (FakeFacebookFriend fakeFriend in FakeFriendsGenerator.sr_FakeFriends)
            {
                if(fakeFriend.IsOnline)
                {
                    listBoxOnlineFriends.Items.Add(fakeFriend);
                }
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
                this.m_ChangingPictureBoxPosts.SetOnePictureURL(post.PictureURL);
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
            Page page = ListBoxLikedPages.SelectedItem as Page;

            if (page != null)
            {
                pictureBoxLikedPages.ImageLocation = page.PictureURL;
            }
        }

        private void listBoxUserFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            FakeFacebookFriend friend = listBoxUserFriends.SelectedItem as FakeFacebookFriend;

            if(friend != null)
            {
                pictureBoxFriendProfilePicture.Image = friend.ProfileImage;

                foreach(object item in listBoxOnlineFriends.Items)
                {
                    if(friend == item)
                    {
                        listBoxOnlineFriends.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void buttonEditProfile_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageSettings;
        }

        private void checkBoxFilterEventsByFriends_CheckedChanged(object sender, EventArgs e)
        {
            FakeFriendsGenerator.GenerateEventsForFriends(m_LoggedInUser.Events.ToList());

            if (checkBoxFilterEventsByFriends.Checked)
            {
                FakeFacebookFriend friend = listBoxUserFriends.SelectedItem as FakeFacebookFriend;

                if (friend == null)
                {
                    MessageBox.Show("You have to select a friend!");
                    checkBoxFilterEventsByFriends.Checked = false;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            
        }
    }
}
