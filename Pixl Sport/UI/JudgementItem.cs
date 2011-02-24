using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

namespace UserMenus
{
    class JudgementItem : MenuItem
    {
        private RuleMenu target;
        private Judgement something;

        public JudgementItem(RuleMenu Target, Judgement Something)
        {
            target = Target;
            something = Something;
            id = something.Id;
            if (something.Id == null) id = "No ID selected";

        }

        public override void execute()
        {
            target.judgementTarget=something;
        }
    }
}
