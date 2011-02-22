using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport.RulesFolder
{
    class NoPassing:Rule
    {
          public NoPassing(GameManager Manager, Judgement punishment)
        {
            ruleName = "Passing will not be tolerated";
            manager = Manager;
            assignedJudgement = punishment;
        }


        public override void Check()
        {
            if (manager.Ball.Passed)
            {
                manager.Ball.Passed = false;
                Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);
            }

        }

    }
}
