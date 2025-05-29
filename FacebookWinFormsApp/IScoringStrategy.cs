using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class ScoringStrategy
    {
        public FriendsFacebookGame Game { private set; get; }
        public int MaxPoints { private set; get; } = 0;

        public void SetIsEnable(bool i_IsEnable)
        {
            if (i_IsEnable)
            {
                MaxPoints = 1;
            }
            else
            {
                MaxPoints = 0;
            }
        }

        public ScoringStrategy (FriendsFacebookGame i_Game)
        {
            Game = i_Game;
        }

        public Func<FriendsFacebookGame, bool> AnswerScore { get; set; }

        public int CalculateScore()
        {
            int score = 0;

            if (AnswerScore.Invoke(Game))
            {
                score = MaxPoints;
            }

            return score;
        }
        
    }
}
