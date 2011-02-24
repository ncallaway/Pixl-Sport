using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class RuleItem : MenuItem
    {
        protected RuleMenu target;
        protected Rule something;
        

        public RuleItem(RuleMenu Target, Rule Something)
        {
            target = Target;
            something = Something;
            id = something.RuleName;

            if (something.RuleName == null) id = "No Rule Name designated";

        }

        public override void execute()
        {

            target.ruleTarget = something;

        }
    }




}
