using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        ChangingPictureBox m_ChangingPictureBoxUserPosts;
        FacebookWrapper.LoginResult m_LoginResult;
        FacebookWrapper.ObjectModel.User m_LoggedInUser;
        string m_AccessToken = "EAAQpHAqlOz0BOwYE5WXzZBdgWZAm9YZBZCwnYlarRVZBecQMK0hvF8v51CJLZCWKZBXasvny8pQwaZAtE2zZBdeunzJHSNDUDPnQCAPcQfWFbnZCsibmmeXOLZAJoY1IRF8R9ybq7LZAIp0En1WNM1JEypev3BNVJ94rLUzk8eivnduwG40F2kZAvk1yfG8YZCDgZDZD";
        private FriendsFacebookGame m_Game = null;

        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            removeTabPages();
            tabPageProfile.Text = "Profile";
            tabPageSocial.Text = "Social";
        }

        private void removeTabPages()
        {
            tabControl1.TabPages.Remove(tabPageSocial);
            tabControl1.TabPages.Remove(tabPageSettings);
            tabControl1.TabPages.Remove(tabPageFeed);
            tabControl1.TabPages.Remove(tabPagePlay);
            panel3.Visible = false;
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
                //m_AccessToken = m_LoginResult.AccessToken;
                loadAppFeatures();
            }
        }

        private void loadAppFeatures()
        {
            initializeUserControlChangingPictureBoxUserPosts();
            showLoggedInUser();
            addTabPages();
            showUserPosts();
            showUserFriends();
            showUserLikedPages();
            showOnlineFriends();
            setChangeSettings();
            showFeed();
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
            pictureBoxProfile.ImageLocation = m_LoggedInUser.PictureLargeURL;
            labelBirthday.Text = m_LoggedInUser.Birthday;
            labelHomeTown.Text = m_LoggedInUser.Location.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();
            DateTime birthday = DateTime.Parse(m_LoggedInUser.Birthday);
            int age = calculateAge(birthday);
            labelUserName.Text = $"{m_LoggedInUser.Name}, {age}";

            panel3.Visible = true;
            m_ChangingPictureBoxUserPosts.Visible = true;
            m_ChangingPictureBoxUserPosts.BringToFront();

            /*buttonLogin.Text = $"Logged in as {m_LoggedInUser.Name}";
            buttonLogin.BackColor = Color.LightGreen;
            buttonLogin.Enabled = false;
            buttonLogin.Left = buttonLogout.Left;*/
            buttonLogin.Visible = false;

            buttonLogout.Enabled = true;
            buttonLogout.Visible = true;           
        }

        private void addTabPages()
        {
            tabControl1.TabPages.Add(tabPageSocial);
            tabControl1.TabPages.Add(tabPageFeed);
            tabControl1.TabPages.Add(tabPagePlay);
            tabControl1.TabPages.Add(tabPageSettings);
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
            if (m_Game == null || m_Game.IsGameFinished)
            {
                m_Game = new FriendsFacebookGame(FakeFriendsGenerator.sr_FakeFriends);
            }
            
            if (tryStartGame())
            {
                loadGameUI();
                updateNextFriend();
                updateScore();
            }

        }

        private void loadGameUI()
        {
            textBoxAnswerName.Enabled = m_Game.IsNameEnabled;
            textBoxAnswerHometown.Enabled = m_Game.IsHometownEnabled;
            dateTimePickerAnswerBirthday.Enabled = m_Game.IsBirthdayEnabled;
            panelGameSettings.Enabled = false;
            panelGame.Visible = true;
            panelGame.Enabled = true;
            buttonNext.Visible = true;
            buttonEnd.Text = "End Game";
        }

        private bool tryStartGame()
        {
            bool isGameStarted;

            try
            {
                setGameSettings();
                m_Game.StartGame();
                isGameStarted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                isGameStarted = false;
            }

            return isGameStarted;
        }

        private void setGameSettings()
        {
            m_Game.IsNameEnabled = checkBoxPlayEnableName.Checked;
            m_Game.IsBirthdayEnabled = checkBoxPlayEnableBirthday.Checked;
            m_Game.IsHometownEnabled = checkBoxPlayEnableHometown.Checked;
            m_Game.NumberOfRounds = int.Parse(textBoxPlayNumberOfFriends.Text);
        }

        private void updateNextFriend()
        {
            pictureBoxGame.Image = m_Game.GetCurrentFriendImage();
            if (!m_Game.IsNameEnabled)
            {
                textBoxAnswerName.Text = m_Game.GetCurrentFriendFullName();
            }
            else
            {
                textBoxAnswerName.Text = string.Empty;
            }
            if (!m_Game.IsBirthdayEnabled)
            {
                dateTimePickerAnswerBirthday.Value = m_Game.GetCurrentFriendBirthday();
            }
            else
            {
                dateTimePickerAnswerBirthday.Value = DateTime.Today;
            }
            if (!m_Game.IsHometownEnabled)
            {
                textBoxAnswerHometown.Text = m_Game.GetCurrentFriendHometown();
            }
            else
            {
                textBoxAnswerHometown.Text = string.Empty;
            }
        }

        private void updateScore()
        {
            labelScore.Text = $"Score: {m_Game.Score} / {m_Game.MaxScoreUntilNow}";
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            setAnswers();
            m_Game.Next();
            updateNextFriend();
            updateScore();
            if (m_Game.CurrentRound == m_Game.NumberOfRounds) 
            {
                buttonNext.Visible = false;
            }
        }

        private void setAnswers()
        {
            if (m_Game.IsNameEnabled)
            {
                m_Game.SetNameAnswer(textBoxAnswerName.Text);
            }

            if (m_Game.IsBirthdayEnabled)
            {
                m_Game.SetBirthdayAnswer(dateTimePickerAnswerBirthday.Value);
            }
            
            if (m_Game.IsHometownEnabled)
            {
                m_Game.SetHometownAnswer(textBoxAnswerHometown.Text);
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            setAnswers();
            m_Game.Next();
            finishGame();
        }

        private void finishGame()
        {
            updateScore();
            m_Game = null;
            panelGame.Enabled = false;
            buttonNext.Visible = false;
            panelGameSettings.Enabled = true;
            buttonEnd.Text = "Game Ended";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPagePlay;
        }

        private void tabPagePlay_Leave(object sender, EventArgs e)
        {
            if (m_Game != null)
            {
                finishGame();
            }
        }

        private void showFeed()
        {
            listBoxFeed.Items.Clear();
            listBoxFeed.DisplayMember = "Name";

            List<Post> feed = getFeed();

            foreach (Post post in feed)
            {
                listBoxFeed.Items.Add(post);
            }
        }
            
        private List<Post> getFeed()
        {
            List<Post> latestFeed = new List<Post>();

            foreach (User friend in m_LoggedInUser.Friends)
            {
                foreach (Post post in friend.Posts)
                {
                    latestFeed.Add(post);
                }
            }

            return latestFeed;
        }

        private void listBoxFeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            Post post = listBoxFeed.SelectedItem as Post;

            if (post != null)
            {
                this.pictureBoxFeed.ImageLocation = post.PictureURL;
            }
        }
    }
}
