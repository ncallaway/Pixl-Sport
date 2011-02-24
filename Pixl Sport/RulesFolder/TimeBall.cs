using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixl_Sport;
namespace UserMenus
{
   
    class TimeBall:Rule
    {
        private float timeLimit;

        public TimeBall(GameManager M, Judgement judgement)
        {
            assignedJudgement = judgement;
            manager = M;
            timeLimit = 30;

        }

        public TimeBall(GameManager M)
        {
            ruleName = "Time Ball";
            manager = M;
            timeLimit = 30;
        }

        public TimeBall(GameManager M, int time)
        {
            manager = M;
            timeLimit = time;

            ruleName = "Time Ball  " + time;
        }

        public TimeBall(GameManager M, Judgement judgement, int seconds)
        {
            assignedJudgement = judgement;
            manager = M;
            timeLimit = seconds;

        }



        public override void  Check()
        {
            if (manager.SecTime % timeLimit == 0&& manager.Time % 1000 ==00)
            {
                if (manager.Ball.Possessor != null) Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);
                else Enforce(null, null);
            }
}
    }
}
