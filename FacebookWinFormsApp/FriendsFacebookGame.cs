using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicFacebookFeatures
{
    public sealed class FriendsFacebookGame
    {
        private static readonly FriendsFacebookGame sr_GameInstance = new FriendsFacebookGame();
        private int m_NumberOfRounds = 0;
        private bool m_IsNameEnabled = false;
        private bool m_IsBirthdayEnabled = false;
        private bool m_IsHometownEnabled = false;
        private eGameStatus m_GameStatus = eGameStatus.NotStarted;
        private string m_NameAnswer = string.Empty;
        private DateTime m_BirthdayAnswer = DateTime.Today;
        private string m_HometownAnswer = string.Empty;
        private Random m_Random = null;
        private int m_CurrentFriendIndex = 0;
        private List<ISocialNetworkFriend> m_CopyFriendsList = null;

        public static FriendsFacebookGame GameInstance
        {
            get
            {
                return sr_GameInstance;
            }
        }
        public int Score { get; private set; } = 0;
        public int MaxScoreUntilNow { get; private set; } = 0;
        public int CurrentRound { get; private set; } = 0;

        private ScoringStrategy m_NameScoring;
        private ScoringStrategy m_HometownScoring;
        private ScoringStrategy m_BirthdayScoring;


        public List<ISocialNetworkFriend> CopyFriendsList
        {
            set
            {
                checkForGameNotStarted();

                if (value == null)
                {
                    throw new Exception("Must Enter Friends List!");
                }

                m_CopyFriendsList = value.ToList();
            }
        }

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
                m_NameScoring.SetIsEnable(value);
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
                m_BirthdayScoring.SetIsEnable(value);
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
                m_HometownScoring.SetIsEnable(value);
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
                checkForListOfFriendsSet();

                if (isLegalNumberOfRounds(value))
                {
                    m_NumberOfRounds = value;
                }
            }
        }

        private void checkForListOfFriendsSet()
        {
            if (m_CopyFriendsList == null || m_CopyFriendsList.Count == 0)
            {
                throw new Exception("FriendList Must be set with at least one friend to do that");
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

        private void checkForGameFinished()
        {
            if (m_GameStatus != eGameStatus.Finished)
            {
                throw new Exception("Can't Do this when game didn't finished yet");
            }
        }

        private FriendsFacebookGame()
        {
            setScoringStrategy();
        }

        private void setScoringStrategy()
        {
            m_NameScoring = new ScoringStrategy(this);
            m_HometownScoring = new ScoringStrategy(this);
            m_BirthdayScoring = new ScoringStrategy(this);
            m_NameScoring.AnswerScore = NameCalulateScore;
            m_BirthdayScoring.AnswerScore = BirthdayCalulateScore;
            m_HometownScoring.AnswerScore = HometownCalulateScore;
        }

        private bool NameCalulateScore(FriendsFacebookGame i_Game)
        {
            return m_NameAnswer.ToLower() == m_CopyFriendsList[m_CurrentFriendIndex].FullName.ToLower();
        }

        private bool BirthdayCalulateScore(FriendsFacebookGame i_Game)
        {
            return m_BirthdayAnswer == m_CopyFriendsList[m_CurrentFriendIndex].Birthday;
        }

        private bool HometownCalulateScore(FriendsFacebookGame i_Game)
        {
           return m_HometownAnswer.ToLower() == m_CopyFriendsList[m_CurrentFriendIndex].Hometown.ToLower();
        }

        public void StartGame()
        {
            if (m_GameStatus == eGameStatus.NotStarted)
            {
                checkForListOfFriendsSet();

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

        public void SetNameAnswer(string i_Answer)
        {
            checkForGameOngoing();

            if (!IsNameEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_NameAnswer = i_Answer;
        }

        public void SetBirthdayAnswer(DateTime i_Answer)
        {
            checkForGameOngoing();

            if (!IsBirthdayEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_BirthdayAnswer = i_Answer;
        }

        public void SetHometownAnswer(string i_Answer)
        {
            checkForGameOngoing();

            if (!IsHometownEnabled)
            {
                throw new Exception("you don't play on this category");
            }

            m_HometownAnswer = i_Answer;
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

        public void Quit()
        {
            m_GameStatus = eGameStatus.Finished;
        }

        public void ResetGame()
        {
            checkForGameFinished();

            m_NumberOfRounds = 0;
            m_IsNameEnabled = false;
            m_IsBirthdayEnabled = false;
            m_IsHometownEnabled = false;
            m_GameStatus = eGameStatus.NotStarted;
            m_NameAnswer = string.Empty;
            m_BirthdayAnswer = DateTime.Today;
            m_HometownAnswer = string.Empty;
            m_Random = null;
            m_CurrentFriendIndex = 0;
            m_CopyFriendsList = null;
            Score = 0;
            MaxScoreUntilNow = 0;
            CurrentRound = 0;
        }

        private void updateScore()
        {
            MaxScoreUntilNow += m_NameScoring.MaxPoints + 
                                m_BirthdayScoring.MaxPoints +
                                m_HometownScoring.MaxPoints;

            Score += m_NameScoring.CalculateScore() + 
                     m_BirthdayScoring.CalculateScore() + 
                     m_HometownScoring.CalculateScore();
        }

        private bool isLegalNumberOfRounds(int i_NumberOfRounds)
        {
            return (i_NumberOfRounds >= 1 && i_NumberOfRounds <= m_CopyFriendsList.Count);
        }
    }
}
