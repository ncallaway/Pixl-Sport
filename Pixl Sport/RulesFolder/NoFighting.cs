using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class NoFighting: Rule
    {
        public NoFighting(GameManager Manager, Judgement punishment)
        {
            ruleName = "Tackling will not be tolerated";
            manager = Manager;
            assignedJudgement = punishment;
        }

        public NoFighting(GameManager Manager)
        {
            ruleName = "No Tackling";
            manager = Manager;
        }


        public override void Check()
        {
            foreach (TeamMember TM in manager.Team1.Members) if (TM.Tackling) Enforce(TM.Team, TM);
            foreach (TeamMember TM in manager.Team2.Members) if (TM.Tackling) Enforce(TM.Team, TM);
        }


    }
}
