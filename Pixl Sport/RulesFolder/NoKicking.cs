using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport.RulesFolder
{
    class NoKicking:Rule
    {
          public NoKicking(GameManager Manager, Judgement punishment)
        {
            ruleName = "Kicking will not be tolerated";
            manager = Manager;
            assignedJudgement = punishment;
        }


        public override void Check()
        {
            if (manager.Ball.Kicked)
            {
                manager.Ball.Kicked = false;
                Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);
            }

        }

    }
}
