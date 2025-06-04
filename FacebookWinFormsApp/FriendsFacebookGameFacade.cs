using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public partial class FriendsFacebookGameFacade : UserControl
    {
        private FriendsFacebookGame m_Game = null;
        private List<ISocialNetworkFriend> m_FacebookFriends = null;

        public FriendsFacebookGameFacade()
        {
            InitializeComponent();
        }

        public void SetFacebookFriends(List<ISocialNetworkFriend> i_FacebookFriends)
        {
            m_FacebookFriends = i_FacebookFriends;
            textBoxPlayNumberOfFriends.Text = Math.Min(5, m_FacebookFriends.Count).ToString();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            m_Game = FriendsFacebookGame.GameInstance;

            if (tryStartGame())
            {
                loadGameUI();
                checkForLastRound();
                updateNextFriend();
                updateGameScore();
            }
        }

        private void loadGameUI()
        {
            panelCheckboxes.Enabled = false;
            textBoxPlayNumberOfFriends.Enabled = false;
            panelAnswerboxes.Enabled = true;

            buttonStartGame.Visible = false;
            buttonEnd.Visible = true;
            buttonEnd.Enabled = true;

            textBoxAnswerName.Enabled = m_Game.IsNameEnabled;
            textBoxAnswerHometown.Enabled = m_Game.IsHometownEnabled;
            dateTimePickerAnswerBirthday.Enabled = m_Game.IsBirthdayEnabled;

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
            m_Game.CopyFriendsList = m_FacebookFriends;
            m_Game.IsNameEnabled = checkBoxPlayEnableName.Checked;
            m_Game.IsBirthdayEnabled = checkBoxPlayEnableBirthday.Checked;
            m_Game.IsHometownEnabled = checkBoxPlayEnableHometown.Checked;
            m_Game.NumberOfRounds = int.Parse(textBoxPlayNumberOfFriends.Text);
        }

        private void updateNextFriend()
        {
            Utility.SetImageInPictureBoxFromObject(pictureBoxGame, m_Game.GetCurrentFriendImage());

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
            checkForLastRound();
        }

        private void checkForLastRound()
        {
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
            m_Game.Quit();
            finishGame();
        }

        private void finishGame()
        {
            updateGameScore();

            panelAnswerboxes.Enabled = false;
            panelCheckboxes.Enabled = true;
            textBoxPlayNumberOfFriends.Enabled = true;

            buttonNext.Visible = false;
            buttonStartGame.Visible = true;
            buttonEnd.Text = "Game Ended";
            buttonEnd.Enabled = false;

            m_Game.ResetGame();
        }

        private void textBoxPlayNumberOfFriends_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
