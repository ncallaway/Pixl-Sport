using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class HotPotato:Rule
    {
        private float timeLimit;

        public HotPotato(GameManager M, Judgement judgement)
        {
            assignedJudgement = judgement;
            manager = M;
            timeLimit = 3000f;

        }

        public HotPotato(GameManager M)
        {
            ruleName = "Don't hold the ball too long";
            manager = M;
            timeLimit = 3000f;
        }

        public override void Check()
        {
            if (manager.Ball.Possessor != null)
            {
                if (manager.Ball.Possessor.TimeWithBall > timeLimit)
                {
                    manager.Ball.Possessor.HeldBall = manager.Ball;
                    Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);
                }
            }


        }




    }
}
