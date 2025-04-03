using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class FriendsFacebookGame
    {
        private int m_NumberOfRounds = 0;
        private List<FakeFacebookFriend> m_CopyFriendsList = null;
        private int m_Score = 0;
        private Random m_Random = null;
        private int m_CurrentRound = 0;
        private eGameStatus m_GameStatus = eGameStatus.NotStarted;

        public bool IsNameEnabled { get; set; } = false;
        public bool IsBirthdayEnabled { get; set; } = false;
        public bool IsHometownEnabled { get; set; } = false;
        public int NumberOfRounds
        {
            get { return m_NumberOfRounds; }
            set
            {

            }
        }


        public FriendsFacebookGame(List<FakeFacebookFriend> i_FriendsList)
        {
            m_CopyFriendsList = i_FriendsList.ToList();
        }

        public void StartGame()
        {
            if (!IsNameEnabled && !IsBirthdayEnabled && !IsHometownEnabled)
            {
                throw new Exception("You have to choose at least one category!");
            }
            else
            {
               if(isLegalNumberOfRounds(NumberOfRounds))
                {

                }
            }
        }

        private bool isLegalNumberOfRounds(int i_NumberOfRounds)
        {
            return !(i_NumberOfRounds < 1 || i_NumberOfRounds > FakeFriendsGenerator.sr_FakeFriends.Count);
        }
    }
}
