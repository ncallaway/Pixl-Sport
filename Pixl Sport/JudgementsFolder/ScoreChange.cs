using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class ScoreChange: Judgement
    {

        protected int scoreDelta;

        public ScoreChange(GameManager M)
        {
            Judged = JudgementType.Team;
            manager = M;
        
        }

        public ScoreChange(int amount)
        {
            scoreDelta = amount;
            Judged = JudgementType.Team;

        }

        public ScoreChange(GameManager M, int amount)
        {
            scoreDelta = amount;
            Judged = JudgementType.Team;
            manager = M;
        }

        public ScoreChange(GameManager M, int amount, bool RestartGame)
        {
            scoreDelta = amount;
            Judged = JudgementType.TeamMember;
            manager = M;
        }


        public override void Execute(Team T)
        {
            T.Score += scoreDelta;
            manager.SetupKickoff();
           
        }

        public override void Execute(TeamMember TM)
        {
            TM.Team.Score += scoreDelta;
        }


        public override void Execute(GameManager M)
        {
            throw new NotImplementedException("What are you doing? Don't call the Game Manager!!");

        }

    }
}
