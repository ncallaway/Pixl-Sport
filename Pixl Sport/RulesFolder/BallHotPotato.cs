using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixl_Sport;
namespace Pixl_Sport.RulesFolder
{
   
    class BallHotPotato:Rule
    {
        private float timeLimit;

        public BallHotPotato(GameManager M, Judgement judgement)
        {
            assignedJudgement = judgement;
            manager = M;
            timeLimit = 30;

        }

        public BallHotPotato(GameManager M, Judgement judgement, int seconds)
        {
            assignedJudgement = judgement;
            manager = M;
            timeLimit = seconds;

        }



        public override void  Check()
{          if(manager.SecTime % timeLimit==0 && manager.Ball.Possessor!=null) Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);
 	
}
    }
}
