using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private ChangingPictureBox m_ChangingPictureBoxUserPosts;
        private FacebookWrapper.LoginResult m_LoginResult;
        private FacebookWrapper.ObjectModel.User m_LoggedInUser;
        private FriendsFacebookGame m_Game = null;
        private string m_AccessToken = "EAAQpHAqlOz0BO0U0qHzPIC5lejD3UNWcYQk39bOTAAmZAzQsPoN1o2MQX9tqUZBgd0uFNqFxg5s54NDLjJOZCdeFmBaciZBDcGOEY4pfgYavidjdc2WKDBox2O9iaqh93oR4XmXgp4HcAzMWEe6nSyYhwenB7cptOQ6QVNe4DiMoKCIfFaZADvO7fnQZDZD";

        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 25;
            removeTabPages();
        }

        private void removeTabPages()
        {
            tabControl1.TabPages.Remove(tabPageSocial);
            tabControl1.TabPages.Remove(tabPageSettings);
            tabControl1.TabPages.Remove(tabPageFeed);
            tabControl1.TabPages.Remove(tabPagePlay);
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
            );*/
          
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                access_token = m_LoginResult.AccessToken;
                loadAppFeatures();
            }
        }

        private void loadAppFeatures()
        {
            initializeUserControlChangingPictureBoxUserPosts();
            addTabPages();
            showLoggedInUser();
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
            buttonLogin.Text = "Login";
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
            buttonLogin.Visible = true;
            buttonLogout.Visible = false;
            m_ChangingPictureBoxUserPosts.Visible = false;
            panel3.Visible = false;
            removeTabPages();
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
            loadLoggedInUserProfileDetails();

            panel3.Visible = true;
            m_ChangingPictureBoxUserPosts.Visible = true;
            m_ChangingPictureBoxUserPosts.BringToFront();

            buttonLogin.Visible = false;
            buttonLogout.Enabled = true;
            buttonLogout.Visible = true;
        }

        private void loadLoggedInUserProfileDetails()
        {
            userBindingSource.DataSource = m_LoggedInUser;

            labelHomeTown.Text = m_LoggedInUser.Location.Name;
            labelGender.Text = m_LoggedInUser.Gender.ToString();

            DateTime birthday = DateTime.Parse(m_LoggedInUser.Birthday);
            int age = calculateAge(birthday);
            labelUserAge.Text = $"{age},";
        }

        private void addTabPages()
        {
            tabControl1.TabPages.Add(tabPageSocial);
            tabControl1.TabPages.Add(tabPageFeed);
            tabControl1.TabPages.Add(tabPagePlay);
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

            //Not working due to api, instead we are using fake freinds
            /*foreach (User friend in m_LoggedInUser.Friends)
            {
                listBoxUserFriends.Invoke(new Action(() => listBoxUserFriends.Items.Add(friend)));
            } */           

            foreach (FakeFacebookFriend fakeFriend in FakeFriendsGenerator.sr_FakeFriends)
            {
                listBoxUserFriends.Invoke(new Action(() => listBoxUserFriends.Items.Add(fakeFriend)));
            }
        }

        private void showOnlineFriends()
        {
            listBoxOnlineFriends.Invoke(new Action(() => listBoxOnlineFriends.DisplayMember = "FullName"));

            foreach (FakeFacebookFriend fakeFriend in FakeFriendsGenerator.sr_FakeFriends)
            {
                if (fakeFriend.IsOnline)
                {
                    listBoxOnlineFriends.Invoke(new Action(() => listBoxOnlineFriends.Items.Add(fakeFriend)));
                }
            }
        }

        private void showUserPosts()
        {             
            listBoxUserPosts.Invoke(new Action(()=>listBoxUserPosts.DisplayMember = "Name"));

            foreach (Post post in m_LoggedInUser.Posts)
            {
                listBoxUserPosts.Invoke(new Action(() => listBoxUserPosts.Items.Add(post)));
            }
        }

        private void showUserEvents()
        {
            FakeFriendsGenerator.GenerateEventsForFriends(m_LoggedInUser.Events.ToList());

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
            FakeFacebookFriend friend = listBoxUserFriends.SelectedItem as FakeFacebookFriend;

            if (friend != null)
            {
                pictureBoxFriendProfilePicture.Image = friend.ProfileImage;
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
                updateGameScore();
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

            if (textBoxPlayNumberOfFriends.Text == string.Empty)
            {
                textBoxPlayNumberOfFriends.Text = "0";
            }

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

        private void updateGameScore()
        {
            labelScore.Text = $"Score: {m_Game.Score} / {m_Game.MaxScoreUntilNow}";
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            setAnswers();
            m_Game.Next();
            updateNextFriend();
            updateGameScore();

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
            updateGameScore();
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
