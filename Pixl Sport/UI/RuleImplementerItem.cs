using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class RuleImplementerItem:MenuItem
    {
        private RuleMenu target;
        private GameManager manager;


        public RuleImplementerItem(GameManager M, RuleMenu Target)
        {
            target = Target;
            manager = M;
            id = "Add new rule";

        }

        public override void execute()
        {
            target.ruleTarget.SetJudgement(target.judgementTarget);
            manager.AddRule(target.ruleTarget);
            manager.MenuM.CloseMenu();
        }




    }
}
