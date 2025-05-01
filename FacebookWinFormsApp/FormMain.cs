using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private ChangingPictureBox m_ChangingPictureBoxUserPosts = null;
        private FacebookWrapper.LoginResult m_LoginResult = null;
        private FacebookWrapper.ObjectModel.User m_LoggedInUser = null;
        private List<ISocialNetworkFriend> m_FacebookFriends = null;
        private FriendsFacebookGameFacade m_GameFacade = null;

        public FormMain()
        {
            InitializeComponent();
            initializeUserControlChangingPictureBoxUserPosts();
            initializeUserControlFriendsFacebookGameFacade();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            removeTabPages();
        }

        private void removeTabPages()
        {
            tabControl1.TabPages.Remove(tabPageSocial);
            tabControl1.TabPages.Remove(tabPageSettings);
            tabControl1.TabPages.Remove(tabPageFeed);
            tabControl1.TabPages.Remove(tabPageGame);
        }

        private void initializeUserControlChangingPictureBoxUserPosts()
        {
            m_ChangingPictureBoxUserPosts = new ChangingPictureBox();
            panel3.Controls.Add(m_ChangingPictureBoxUserPosts);
            tabPageProfile.Controls.Add(m_ChangingPictureBoxUserPosts);
            m_ChangingPictureBoxUserPosts.Location = listBoxUserPosts.Location;
            m_ChangingPictureBoxUserPosts.Height = listBoxUserPosts.Height - 10;
            m_ChangingPictureBoxUserPosts.Visible = false;
        }

        private void initializeUserControlFriendsFacebookGameFacade()
        {
            m_GameFacade = new FriendsFacebookGameFacade();
            tabPageGame.Controls.Add(m_GameFacade);
            m_GameFacade.Top = panel8.Bottom + 30; ;
            m_GameFacade.Left = 30;
            m_GameFacade.Height = 450;
            m_GameFacade.Width = 1000;
            m_GameFacade.Visible = true;
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
            m_LoginResult = FacebookService.Login(
            "1171100321266493",
                /// requested permissions:
                "email",
            "public_profile",
            "user_birthday",
            "user_friends",
            "user_gender",
            "user_location",
            "user_posts",
            "user_likes",
            "user_events"
            );

            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                loadFriends();
                m_GameFacade.SetFacebookFriends(m_FacebookFriends);
                loadAppFeatures();
            }
            else
            {
                m_LoginResult = null;
            }
        }

        private void loadFriends()
        {
            if (checkBoxUseFakeFriends.Checked)
            {
                loadFakeFriends();
            }
            else
            {
                loadRealFriends();
            }
        }

        private void loadRealFriends()
        {
            List<User> friendsList = m_LoggedInUser.Friends.ToList();
            m_FacebookFriends = new List<ISocialNetworkFriend>();

            foreach (User friend in friendsList)
            {
                m_FacebookFriends.Add(new UserFriendAdapter(friend));
            }
        }

        private void loadFakeFriends()
        {
            m_FacebookFriends = FakeFriendsGenerator.sr_FakeFriends;
            FakeFriendsGenerator.GenerateEventsForFriends(m_LoggedInUser.Events.ToList());
        }

        private void loadAppFeatures()
        {
            hideLogInControls();
            addTabPages();
            showProfileControls();
            loadLoggedInUserProfileDetails();
            new Thread(loadListBoxes).Start();
        }

        private void loadListBoxes()
        {
            new Thread(showUserPosts).Start();
            new Thread(showUserEvents).Start();
            new Thread(showUserFriends).Start();
            new Thread(showUserLikedPages).Start();
            new Thread(showOnlineFriends).Start();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            clearAllListBoxes();
            resetUIAfterLogout();
        }

        private void clearAllListBoxes()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (Control control in tab.Controls)
                {
                    ListBox listBox = control as ListBox;

                    if (listBox != null)
                    {
                        listBox.Items.Clear();
                    }
                }
            }
        }

        private void resetUIAfterLogout()
        {
            m_LoginResult = null;
            m_LoggedInUser = null;
            buttonLogin.Visible = true;
            checkBoxUseFakeFriends.Visible = true;
            buttonLogout.Visible = false;
            m_ChangingPictureBoxUserPosts.Visible = false;
            panel3.Visible = false;
            removeTabPages();
        }

        private void showProfileControls()
        {
            panel3.Visible = true;
            m_ChangingPictureBoxUserPosts.Visible = true;
            m_ChangingPictureBoxUserPosts.BringToFront();
            buttonLogout.Enabled = true;
            buttonLogout.Visible = true;
        }

        private void hideLogInControls()
        {
            buttonLogin.Visible = false;
            checkBoxUseFakeFriends.Visible = false;
        }

        private void loadLoggedInUserProfileDetails()
        {
            userBindingSource.DataSource = m_LoggedInUser;

            labelHomeTown.Text = m_LoggedInUser.Location.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();

            DateTime birthday = DateTime.Parse(m_LoggedInUser.Birthday);
            int age = Utility.calculateAge(birthday);

            labelUserAge.Text = $"{age},";
        }

        private void addTabPages()
        {
            tabControl1.TabPages.Add(tabPageSocial);
            tabControl1.TabPages.Add(tabPageFeed);
            tabControl1.TabPages.Add(tabPageGame);
            tabControl1.TabPages.Add(tabPageSettings);
        }

        private void showUserLikedPages()
        {
            listBoxLikedPages.Invoke(new Action(() => listBoxLikedPages.DisplayMember = "Name"));

            foreach (Page page in m_LoggedInUser.LikedPages)
            {
                listBoxLikedPages.Invoke(new Action(() => listBoxLikedPages.Items.Add(page)));
            }
        }

        private void showUserFriends()
        {
            listBoxUserFriends.Invoke(new Action(() => listBoxUserFriends.DisplayMember = "FullName"));

            foreach (ISocialNetworkFriend friend in m_FacebookFriends)
            {
                listBoxUserFriends.Invoke(new Action(() => listBoxUserFriends.Items.Add(friend)));
            }
        }

        private void showOnlineFriends()
        {
            listBoxOnlineFriends.Invoke(new Action(() => listBoxOnlineFriends.DisplayMember = "FullName"));

            foreach (ISocialNetworkFriend friend in m_FacebookFriends)
            {
                if (friend.IsOnline)
                {
                    listBoxOnlineFriends.Invoke(new Action(() => listBoxOnlineFriends.Items.Add(friend)));
                }
            }
        }

        private void showUserPosts()
        {
            listBoxUserPosts.Invoke(new Action(() => listBoxUserPosts.DisplayMember = "Name"));

            foreach (Post post in m_LoggedInUser.Posts)
            {
                listBoxUserPosts.Invoke(new Action(() => listBoxUserPosts.Items.Add(post)));
            }
        }

        private void showUserEvents()
        {
            listBoxEvents.Invoke(new Action(() => listBoxEvents.DisplayMember = "Name"));

            foreach (Event ev in m_LoggedInUser.Events)
            {
                listBoxEvents.Invoke(new Action(() => listBoxEvents.Items.Add(ev)));
            }
        }

        private void listBoxUserPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Post post = listBoxUserPosts.SelectedItem as Post;

            if (post != null)
            {
                this.m_ChangingPictureBoxUserPosts.SetOnePictureURL(post.PictureURL);
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

        private void listBoxLikedPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page page = listBoxLikedPages.SelectedItem as Page;

            listBoxPagePosts.Items.Clear();
            listBoxPagePosts.DisplayMember = "Name";
            listBoxPostLikes.Items.Clear();
            listBoxPostComments.Items.Clear();

            if (page != null)
            {
                pictureBoxLikedPages.ImageLocation = page.PictureURL;

                try
                {
                    foreach (Post post in page.Posts)
                    {
                        listBoxUserPosts.Items.Add(post);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void listBoxUserFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISocialNetworkFriend friend = listBoxUserFriends.SelectedItem as ISocialNetworkFriend;

            if (friend != null)
            {
                Utility.setImageInPictureBoxFromObject(pictureBoxFriendProfilePicture, friend.ProfileImage);
                listBoxOnlineFriends.SelectedItem = null;

                foreach (object item in listBoxOnlineFriends.Items)
                {
                    if (friend == item)
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
            if (checkBoxFilterEventsByFriends.Checked)
            {
                ISocialNetworkFriend friend = listBoxUserFriends.SelectedItem as ISocialNetworkFriend;

                if (friend == null)
                {
                    MessageBox.Show("You have to select a friend!");
                    checkBoxFilterEventsByFriends.Checked = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageGame;
        }

        private void listBoxPagePosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Post post = listBoxPagePosts.SelectedItem as Post;

            listBoxPostLikes.Items.Clear();
            listBoxPostLikes.DisplayMember = "Name";
            listBoxPostComments.Items.Clear();
            listBoxPostComments.DisplayMember = "Message";

            if (post != null)
            {
                pictureBoxPagePosts.ImageLocation = post.PictureURL;

                try
                {
                    foreach (User user in post.LikedBy)
                    {
                        listBoxPostLikes.Items.Add(user);
                    }

                    foreach (Comment comment in post.Comments)
                    {
                        listBoxPostComments.Items.Add(comment);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void emailTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!emailTextBox.Text.Contains("@"))
            {
                e.Cancel = true;
                MessageBox.Show("Email must contain '@'.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonChoosePicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Image selectedImage = Image.FromFile(openFileDialog.FileName);
                    imageLargePictureBox.Image = selectedImage;
                }
            }
        }
    }
}