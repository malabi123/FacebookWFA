using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class FriendsFacebookGame
    {
        private int m_NumberOfRounds = 0;
        private bool m_IsNameEnabled = false;
        private bool m_IsBirthdayEnabled = false;
        private bool m_IsHometownEnabled = false;
        private eGameStatus m_GameStatus = eGameStatus.NotStarted;
        private string m_NameAnswer = string.Empty;
        private DateTime m_BirthdayAnswer;
        private string m_HometownAnswer = string.Empty;
        private Random m_Random = null;
        private int m_CurrentFriendIndex;
        private List<ISocialNetworkFriend> m_CopyFriendsList = null;

        public int Score { get; private set; } = 0;
        public int MaxScoreUntilNow { get; private set; } = 0;
        public int CurrentRound { get; private set; } = 0;

        public bool IsNameEnabled
        {
            get
            {
                return m_IsNameEnabled;
            }
            set
            {
                checkForGameNotStarted();
                m_IsNameEnabled = value;
            }
        }

        public bool IsBirthdayEnabled
        {
            get
            {
                return m_IsBirthdayEnabled;
            }
            set
            {
                checkForGameNotStarted();
                m_IsBirthdayEnabled = value;
            }
        }

        public bool IsHometownEnabled
        {
            get
            {
                return m_IsHometownEnabled;
            }
            set
            {
                checkForGameNotStarted();
                m_IsHometownEnabled = value;
            }
        }

        public int NumberOfRounds
        {
            get
            {
                return m_NumberOfRounds;
            }
            set
            {
                checkForGameNotStarted();

                if (isLegalNumberOfRounds(value))
                {
                    m_NumberOfRounds = value;
                }
            }
        }

        public bool IsGameOnGoing
        {
            get
            {
                return m_GameStatus == eGameStatus.Ongoing;
            }
        }

        public bool IsGameFinished
        {
            get
            {
                return m_GameStatus == eGameStatus.Finished;
            }
        }

        private void checkForGameNotStarted()
        {
            if (m_GameStatus != eGameStatus.NotStarted)
            {
                throw new Exception("Can't change this category now!");
            }
        }

        private void checkForGameOngoing()
        {
            if (m_GameStatus != eGameStatus.Ongoing)
            {
                throw new Exception("Can't Do this when game isn't ongoing");
            }
        }

        public FriendsFacebookGame(List<ISocialNetworkFriend> i_FriendsList)
        {
            m_CopyFriendsList = i_FriendsList.ToList();
        }

        public void StartGame()
        {
            if (m_GameStatus == eGameStatus.NotStarted)
            {
                if (!IsNameEnabled && !IsBirthdayEnabled && !IsHometownEnabled)
                {
                    throw new Exception("You have to choose at least one category between name, birthday and hometown!");
                }
                else
                {
                    if (!isLegalNumberOfRounds(NumberOfRounds))
                    {
                        throw new Exception($"You Must Choose legal Number of Rounds Between 1 and {m_CopyFriendsList.Count}");
                    }
                }

                m_GameStatus = eGameStatus.Ongoing;
                m_Random = new Random();
                chooseNextFriend();
            }
            else
            {
                throw new Exception("Game Alredy Started!");
            }
        }

        private void chooseNextFriend()
        {
            m_CurrentFriendIndex = m_Random.Next(0, m_CopyFriendsList.Count);
            CurrentRound++;
        }

        public object GetCurrentFriendImage()
        {
            checkForGameOngoing();

            return m_CopyFriendsList[m_CurrentFriendIndex].ProfileImage;
        }

        public string GetCurrentFriendFullName()
        {
            checkForGameOngoing();

            if (m_IsNameEnabled)
            {
                throw new Exception("PLease Dont Try To Cheat!");
            }

            return m_CopyFriendsList[m_CurrentFriendIndex].FullName;
        }

        public DateTime GetCurrentFriendBirthday()
        {
            checkForGameOngoing();

            if (m_IsBirthdayEnabled)
            {
                throw new Exception("PLease Dont Try To Cheat!");
            }

            return m_CopyFriendsList[m_CurrentFriendIndex].Birthday;
        }

        public string GetCurrentFriendHometown()
        {
            checkForGameOngoing();

            if (m_IsHometownEnabled)
            {
                throw new Exception("PLease Dont Try To Cheat!");
            }

            return m_CopyFriendsList[m_CurrentFriendIndex].Hometown;

        }

        public void SetNameAnswer(string i_answer)
        {
            checkForGameOngoing();

            if (!IsNameEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_NameAnswer = i_answer;
        }

        public void SetBirthdayAnswer(DateTime i_answer)
        {
            checkForGameOngoing();

            if (!IsBirthdayEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_BirthdayAnswer = i_answer;
        }

        public void SetHometownAnswer(string i_answer)
        {
            checkForGameOngoing();

            if (!IsHometownEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_HometownAnswer = i_answer;
        }

        public void Next()
        {
            checkForGameOngoing();
            updateScore();
            m_CopyFriendsList.Remove(m_CopyFriendsList[m_CurrentFriendIndex]);

            if (NumberOfRounds > CurrentRound)
            {
                chooseNextFriend();
            }
            else
            {
                m_GameStatus = eGameStatus.Finished;
            }
        }

        private void updateScore()
        {
            if (IsNameEnabled)
            {
                MaxScoreUntilNow++;

                if (m_NameAnswer.ToLower() == m_CopyFriendsList[m_CurrentFriendIndex].FullName.ToLower())
                {
                    Score++;
                    m_NameAnswer = string.Empty;
                }
            }

            if (IsBirthdayEnabled)
            {
                MaxScoreUntilNow++;

                if (m_BirthdayAnswer == m_CopyFriendsList[m_CurrentFriendIndex].Birthday)
                {
                    Score++;
                    m_BirthdayAnswer = DateTime.Today;
                }
            }

            if (IsHometownEnabled)
            {
                MaxScoreUntilNow++;

                if (m_HometownAnswer.ToLower() == m_CopyFriendsList[m_CurrentFriendIndex].Hometown.ToLower())
                {
                    Score++;
                    m_HometownAnswer = string.Empty;
                }
            }
        }

        private bool isLegalNumberOfRounds(int i_NumberOfRounds)
        {
            return (i_NumberOfRounds >= 1 && i_NumberOfRounds <= m_CopyFriendsList.Count);
        }
    }
}
