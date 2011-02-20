using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class ScoreChange: Judgement
    {

        protected int scoreDelta;

        public ScoreChange()
        {
            Judged = JudgementType.Team;
        
        
        }

        public ScoreChange(int amount)
        {
            scoreDelta = amount;
            Judged = JudgementType.Team;

        }


        public override void Execute(Team T)
        {
            T.Score = scoreDelta;
            
        }

        public override void Execute(TeamMember TM)
        {
            throw new NotImplementedException("What are you doing? You can't add points to a Team Member!");

        }


        public override void Execute(GameManager M)
        {
            throw new NotImplementedException("What are you doing? Don't call the Game Manager!!");

        }

    }
}
