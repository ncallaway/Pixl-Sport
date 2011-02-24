using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport.JudgementsFolder
{
    class ScoreExchange: Judgement
    {
        
        public ScoreExchange(GameManager M)
        {
            Judged = JudgementType.Global;
            manager = M;
            
        
        }


        public override void Execute(GameManager M )
        {
            int temp = manager.Team1.Score;
            manager.Team1.Score = manager.Team2.Score;
            manager.Team2.Score = temp;

        }


        public override void Execute(TeamMember TM) { throw new NotImplementedException(); }

        public override void Execute(Team T) { throw new NotImplementedException(); }

    }
}
