using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class NoKicking:Rule
    {
          public NoKicking(GameManager Manager, Judgement punishment)
        {
            ruleName = "Kicking will not be tolerated";
            manager = Manager;
            assignedJudgement = punishment;
        }

              public NoKicking(GameManager Manager)
        {
            ruleName = "No Kicking";
            manager = Manager;
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
